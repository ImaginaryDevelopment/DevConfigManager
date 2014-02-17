using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Models;
using Domain.Extensions;
using Domain.Helpers;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcDatabase : UserControl
	{
		readonly CancellationHelper _cancellationHelper;

		readonly ObservableCollection<string> _verifiedServers;
		readonly ObservableCollection<string> _verifiedDatabases = new ObservableCollection<string>();

		readonly ObservableCollection<string> _verifiedTables = new ObservableCollection<string>();

		DatabaseViewModel dbvm;

		static string BuildConnection(string server, string database)
		{
			if (StringExtensions.IsNullOrEmpty(database))
				return string.Format("Server={0};Trusted_Connection=True;", server);
			return string.Format("Server={0};Database={1};Trusted_Connection=True;", server, database);
		}

		public UcDatabase(ObservableCollection<string> servers)
		{
			InitializeComponent();
			lblCleanTable.Text = string.Empty;
			_verifiedServers = servers;

			_cancellationHelper = new CancellationHelper(() => btnPopulate.InvokeSafeEnabled(false), invokeFinally: () => btnPopulate.InvokeSafeEnabled(true));

			cbFilterColumn.ToObservableText().Subscribe(s => this.FilterDataGrid(s, txtFilterCriteria.InvokeSafeText()));
			cbTable.TextChanged += this.CbTableTextChanged;
		}

		void CbTableTextChanged(object sender, EventArgs e)
		{
			lblCleanTable.Text = this.CleanTableInput(cbTable.Text);
		}

		void OnException(Exception ex)
		{
			richTextBox1.InvokeSafeAppend(ex.Message + Environment.NewLine);
			tabControl1.InvokeSafe(tc => tc.SelectTab(tbLog));
		}

		static void TurnBlackWhileDropDownShows(ComboBox ui)
		{
			ui.DropDown += (sender, e) =>
			{
				if (ui.ForeColor != Color.Red) return;
				var oldColor = ui.ForeColor;
				ui.ForeColor = Color.Black;
				ui.ToObservableOnDropDownClosed().Take(1).Subscribe(_ => ui.ForeColor = oldColor);
			};
		}

		static void RefreshChildWhenMyVerifiedChanges(ObservableCollection<string> verified, Control ui, CancellationHelper ch, Action<Exception> onTaskException, Func<CancellationToken, Task> tryRefreshChildFunc = null)
		{
			if (tryRefreshChildFunc == null) return;
			verified.ToObservableCollectionChanged()
				.Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add)
				.Select(e => e.EventArgs.NewItems.Cast<string>()).ObserveOn(TaskPoolScheduler.Default)
				.Subscribe(
				_ =>
				{
					try
					{

						if (tryRefreshChildFunc != null)
						{
							var childTask = ch.ContinueWith(tryRefreshChildFunc);
							childTask.Wait();
							ui.ForeColor = Color.Blue;
						}

					}
					catch (Exception ex)
					{
						onTaskException(ex);
						ui.ForeColor = Color.Red;
					}

				});
		}

		static void WireSubscriptions(
			ComboBox ui,
			ObservableCollection<string> verified,
			CancellationHelper ch,
			Action<Exception> onTaskException,
			Func<CancellationToken, Task> tryRefreshChildFunc = null)
		{
			TurnBlackWhileDropDownShows(ui);

			ui.DataSource = verified;
			ui.DataSourceChanged += (sender, e) => ui.ForeColor = Color.Black;
			ui.TextChanged += (sender, e) => ui.ForeColor = Color.Black;

			RefreshChildWhenMyVerifiedChanges(verified, ui, ch, onTaskException, tryRefreshChildFunc);

			RefreshMeWhenMyVerifiedChanges(verified, ui, onTaskException);

			RefreshChildWhenUserChangesMe(ui, ch, onTaskException, tryRefreshChildFunc);


		}

		static void RefreshChildWhenUserChangesMe(Control ui, CancellationHelper ch, Action<Exception> onTaskException, Func<CancellationToken, Task> tryRefreshChildFunc)
		{
			if (tryRefreshChildFunc == null) return;
			ui.ToObservableText().Where(s => !ch.IsBusy).ObserveOn(TaskPoolScheduler.Default).SubscribeOn(SynchronizationContext.Current)
				.Subscribe(
				_ =>
				{
					try
					{


						var childTask = ch.RunAsync(tryRefreshChildFunc);
						childTask.Wait();
						ui.InvokeSafe(c => c.ForeColor = Color.Blue);

					}
					catch (Exception ex)
					{
						onTaskException(ex);
						ui.InvokeSafe(c => c.ForeColor = Color.Red);
					}

				});
		}

		static void RefreshMeWhenMyVerifiedChanges(ObservableCollection<string> verified, ComboBox ui, Action<Exception> onTaskException)
		{
			verified.ToObservableCollectionChanged()
				.Where(e => e.EventArgs.Action != NotifyCollectionChangedAction.Add)
				.SubscribeOn(SynchronizationContext.Current).Subscribe(
					_ =>
					{
						try
						{
							var oldText = ui.Text;
							ui.DataSource = null;

							ui.ResetBindings();

							ui.DataSource = verified;
							ui.Text = oldText;
							ui.ForeColor = verified.Contains(oldText, StringComparer.CurrentCultureIgnoreCase) ? Color.Blue : Color.Red;
						}
						catch (Exception ex)
						{
							onTaskException(ex);
						}
					});
		}

		async Task RefreshDatabases(CancellationToken ct)
		{
			if (StringExtensions.IsNullOrEmpty(cbServer.InvokeSafeText())) return;

			var p = await this.TryGetDb(ct, cbServer.InvokeSafeText(), string.Empty); // do not pass a database when querying what databases are there
			if (ct.IsCancellationRequested || p == null)
			{
				return;
			}
			string userDbText = cbDatabase.InvokeSafeText();
			cbDatabase.InvokeSafe(c => c.DataSource = null);
			Debug.WriteLine("RefreshDatabases attempting");
            try
            {
                using (p)
                {
                    var dbInfo = p.GetDatabases().ToArray();
                    var additions = from db in dbInfo
                                    let verified = _verifiedDatabases.FirstOrDefault(v => db.Equals(v, StringComparison.CurrentCultureIgnoreCase))
                                    orderby verified != null, db
                                    select verified ?? db;
                    var additionsEnumerated = additions.ToArray();
                    _verifiedServers.AddIfMissing(cbServer.InvokeSafeText());

                    cbDatabase.InvokeSafe(c =>
                    {
                        c.DataSource = additionsEnumerated;
                        c.Text = userDbText;
                        c.ForeColor = _verifiedServers.Contains(userDbText, StringComparer.CurrentCultureIgnoreCase)
                                              ? Color.Blue
                                              : Color.Black;
                    });

                    cbServer.InvokeSafe(c => c.ForeColor = Color.Blue);
                }
                Debug.WriteLine("RefreshDatabases finished");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Debug.WriteLine(ex.ToString());
                
            }
			
			//TODO: pre-populate from user memory and sort by user preferences?
		}

		async Task RefreshTables(CancellationToken ct)
		{
			if (StringExtensions.IsNullOrEmpty(cbServer.InvokeSafeText()) || StringExtensions.IsNullOrEmpty(cbDatabase.InvokeSafeText())) return;
			var p = await this.TryGetDb(ct, cbServer.InvokeSafeText(), cbDatabase.InvokeSafeText());
			if (ct.IsCancellationRequested) return;
			if (p == null) return;
			string userText = null;
			cbTable.InvokeSafe(
				c =>
				{
					userText = c.Text;
					c.DataSource = null;
				}); //fires a text changed?
			Debug.WriteLine("RefreshTables attempting");
            try
            {
                using (p)
                {
                    var tvInfo = p.GetTablesAndViews();
                    var additions = from tv in tvInfo.Select(tv => tv.SchemaName + "." + tv.TableName)
                                    let verified = _verifiedTables.Contains(tv, StringComparer.CurrentCultureIgnoreCase)
                                    orderby verified, tv
                                    select new { tv, verified };
                    var range = additions.ToArray();
                    cbDatabase.InvokeSafe(c => c.ForeColor = Color.Blue);
                    cbTable.InvokeSafe(
                        c =>
                        {
                            c.DataSource = range.Select(r => r.tv).ToArray();
                            c.Text = userText;
                            var rangeItem = range.FirstOrDefault(r => StringComparer.CurrentCultureIgnoreCase.Compare(r.tv, userText) == 0);
                            if (rangeItem == null)
                            {
                                c.ForeColor = Color.Red;
                            }
                            else
                            {
                                c.ForeColor = rangeItem.verified ? Color.Blue : Color.Black;
                            }
                        });
                }
                Debug.WriteLine("RefreshTables finished");
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                Debug.WriteLine(ex.ToString());
            }
			// TODO: pre-populate from user memory and sort by user preferences?
		}

		static PetaPoco.Database GetDb(string server, string database)
		{
			return new PetaPoco.Database(BuildConnection(server, database), "System.Data.SqlClient");
		}

		Task<PetaPoco.Database> TryGetDb(CancellationToken ct, string server, string database)
		{
			return Task.Run(
				() =>
				{
					try
					{
						var db = GetDb(server, database);

						return db;
					}
					catch (Exception ex)
					{
						this.OnException(ex);
						return null;
					}
				}, ct);
		}

		async Task RefreshData(CancellationToken cancellationToken)
		{
			if (cancellationToken.WaitHandle == null)
				throw new InvalidOperationException("cancellationToken");
			var server = cbServer.InvokeSafeText();
			if (StringExtensions.IsNullOrEmpty(server)) return;
			var database = cbDatabase.InvokeSafeText();

			var table = lblCleanTable.InvokeSafeText();

			if (cancellationToken.IsCancellationRequested) return;
			var p = await this.TryGetDb(cancellationToken, server, database);
			if (p == null) return;
			if (cancellationToken.IsCancellationRequested) return;
			if (cbServer.InvokeSafeText() != server) return;
			if (cbDatabase.InvokeSafeText() != database) return;

			if (cbTable.InvokeSafeText() != table.Replace("[", string.Empty).Replace("]", string.Empty)) return;
			Debug.WriteLine("RefreshData attempting");
			using (p)
			{
				if (cancellationToken.IsCancellationRequested) return;
				if (StringExtensions.IsNullOrEmpty(database) == false && StringExtensions.IsNullOrEmpty(table) == false)
				{
					try
					{
						var userFilter = rchWhere.InvokeSafeText();
						var userQuery = rchSelect.InvokeSafeText();
						if (string.IsNullOrWhiteSpace(userQuery) && StringExtensions.IsNullOrEmpty(table) == false)
							rchSelect.InvokeSafeText("select top 1000 *");
						var builtQuery = (string.IsNullOrWhiteSpace(userQuery) ? "select top 1000 *" : userQuery) + " from " + table;
						var values = p.Query<dynamic>(builtQuery + " " + userFilter);

						var valuesCast = values.ToArray().Cast<IDictionary<string, object>>().ToArray();
						var valuesBindable = com.bodurov.DataSourceCreator.ToDataSource(valuesCast);
						_verifiedServers.AddIfMissing(server, StringComparer.CurrentCultureIgnoreCase);
						_verifiedDatabases.AddIfMissing(database, StringComparer.CurrentCultureIgnoreCase);
						_verifiedTables.AddIfMissing(table);

						dbvm = new DatabaseViewModel(valuesBindable, server, database, table);

						dgData.InvokeSafe(dg =>
						{

							dg.DataSource = dbvm.Data;
							dg.Tag = dbvm;
							dg.Columns.OfType<DataGridViewImageColumn>().ToList().ForEach(c =>
							{
								dg.Columns.Remove(c);
								dg.Columns.Add(new DataGridViewTextBoxColumn() { DataPropertyName = c.DataPropertyName, Name = c.Name, DisplayIndex = c.DisplayIndex });
							});
						});
						tabControl1.InvokeSafe(tc => tc.SelectTab(tbGrid));
						if (dgData.InvokeSafe(dg => dg.Rows.Count) > 0)
						{
							UpdateDynamicFilterOptions();
						}
						cbTable.InvokeSafe(c => c.ForeColor = Color.Blue);
					}
					catch (Exception ex)
					{

						this.OnException(ex);
					}

				}
				Debug.WriteLine("RefreshData finished");

			}
		}

		void UpdateDynamicFilterOptions()
		{
			cbFilterColumn.InvokeSafe(
				cb =>
				{
					var oldText = cb.Text;
					cb.DataSource = null;
					cb.DataSource = dgData.Columns;
					var displayMember = LinqOp.PropertyOf<DataGridViewColumn>(dgc => dgc.HeaderText).Name;
					cb.DisplayMember = displayMember;
					var valueMember = LinqOp.PropertyOf<DataGridViewColumn>(dgc => dgc.DataPropertyName).Name;
					cb.ValueMember = valueMember;
					cb.Text = oldText;
				});

		}

		string CleanTableInput(string text)
		{
			if (text.Contains('[')) return text;

			if (text.Contains('.') == false) return "[" + text + "]";

			return text.Split('.').Surround("[", "]").Delimit(".");
		}

		async void BtnPopulateClick(object sender, EventArgs e)
		{
			await _cancellationHelper.RunAsync(this.RefreshData);
		}

		async void UcDatabase_Load(object sender, EventArgs e)
		{
			try
			{
				await _cancellationHelper.RunAsync(this.RefreshData);

				WireSubscriptions(
					cbServer, _verifiedServers, _cancellationHelper, this.OnException, this.RefreshDatabases);
				WireSubscriptions(
					cbDatabase, _verifiedDatabases, _cancellationHelper, this.OnException, this.RefreshTables);
				WireSubscriptions(cbTable, _verifiedTables, _cancellationHelper, this.OnException);
				txtFilterCriteria.ToObservableText().Subscribe(
					s =>
					{ FilterDataGrid(cbFilterColumn.InvokeSafeText(), s); });

				var refreshDbsTask = _cancellationHelper.RunAsync(this.RefreshDatabases);
				await refreshDbsTask;
				var refreshTablesTask = _cancellationHelper.RunAsync(this.RefreshTables);
				await refreshTablesTask;

			}
			catch (System.ComponentModel.Win32Exception win32Exception)
			{

				this.OnException(win32Exception);
			}

		}

		void FilterDataGrid(string column, string filter)
		{
			if (StringExtensions.IsNullOrEmpty(column) || StringExtensions.IsNullOrEmpty(filter))
			{
				if (dgData.Tag != null)
				{

					dgData.DataSource = dbvm.Data;
				}
				return;
			}

			var dataDyn = dbvm.Data as ICollection;

			if (dataDyn.Count < 1) return;

			var property = dbvm.ElementType.GetProperty(column);
			if (property == null) { return; }

			var filtered = dataDyn.CastTo<IEnumerable<object>>().Where(f => Regex.IsMatch(property.GetValue(f).ToString(), filter));
			var filteredEnumerated = filtered.ToList();
			dgData.DataSource = filteredEnumerated;
		}

		void DgDataCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var column = dgData.Columns[e.ColumnIndex];
			if (column.DataPropertyName == "VarName")
			{
				var dbValues = new[] { "appserver", "dbserver", "dbcaching", "fedbserver" };
				if (dbValues.Contains(e.Value.ToString(), StringComparer.CurrentCultureIgnoreCase))
				{
					e.CellStyle.ForeColor = Color.Blue;
				}
			}

		}
	}

}
