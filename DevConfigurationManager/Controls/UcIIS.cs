using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using DeveloperConfigurationManager.CrossCutting;
using Domain;
using Domain.Adapters;
using Domain.Extensions;
using Domain.Helpers;
using Domain.Models;
using Newtonsoft.Json;
using IisConfigModel = DeveloperConfigurationManager.Controls.Interfaces.IisConfigModel;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcIIS : UserControl, IRefreshable, ISaveable, ISaveAs, INotifyPropertyChanged
	{
		readonly Func<string, IAdministerIIS> _administrator;

		readonly Profiler _profiler;

		string _status;

		CancellationTokenSource _cts;

		public UcIIS(Func<string, IAdministerIIS> administrator,Profiler profiler )
		{
			_profiler = profiler;
			using (_profiler.Step("Constructor"))
			{
				
			
			_administrator = administrator;
			
			InitializeComponent();
			var localButtons = new[] { btnKill };
			cmbServer.ToObservableOnDropDown().Subscribe(
				_ => localButtons.ToList().ForEach(a => a.Enabled = false));
			cmbServer.ToObservableOnDropDownClosed().Subscribe(_ =>
				{
					if (StringExtensions.IsNullOrEmpty(cmbServer.Text) || cmbServer.Text == "localhost")
					{
						localButtons.ToList().ForEach(a => a.Enabled = true);
					}
				});
			cmbServer.ToObservableText()
				.Throttle(TimeSpan.FromMilliseconds(500))
				.ObserveOn(SynchronizationContext.Current)
				.SubscribeOn(SynchronizationContext.Current).Subscribe(text =>
					{
						if (StringExtensions.IsNullOrEmpty(text) || text == "localhost")
						{
							localButtons.ToList().ForEach(a => a.Enabled = true);
						}
						else
						{
							localButtons.ToList().ForEach(a => a.Enabled = false);
						}
					});
			}
		}

		public async Task StopIis()
		{
			await this.RunIisServerStream(IIS.ResetStop);
		}

		public async Task StartIis()
		{
			await this.RunIisServerStream(IIS.ResetStart);
		}

		public async Task Clean()
		{
			await this.RunIisServerStream(IIS.Clean);
		}

		protected IEnumerable<Control> GetTaskDisables()
		{
			return this.Controls.Cast<Control>().OfType<Button>().Where(b => b != btnCancel).ToArray();
		}

		async Task<StreamOuts> RunIisServerStream(Func<CancellationToken, string, StreamingOuts> func)
		{
			return await RunIisStream(ct => func(ct, cmbServer.Text));
		}

		async Task<StreamOuts> RunIisStream(Func<CancellationToken, StreamingOuts> func)
		{
			if (_cts != null)
			{
				return null;
			}

			StreamOuts result;
			try
			{
				using (_cts = new CancellationTokenSource())
				{
					//_cts.Token.Register(() => Domain.Adapters.Process.Kill("iisreset.exe", TimeSpan.FromSeconds(4)));
					result = await ControlStreamHelper.RunStreaming(
							func,
							this.GetTaskDisables(),
							richTextBox1,
							btnCancel,
							_cts);
				}
			}
			finally
			{
				_cts = null;
			}

			return result;
		}

		void Log(string text)
		{
			foreach (var line in StringExtensions.SplitLines(text))
				richTextBox1.InvokeSafeAppend(line);

			richTextBox1.InvokeSafeAppend(DateTime.Now.ToShortTimeString() + Environment.NewLine);
			richTextBox1.InvokeSafe(rch => rch.Select(rch.Text.Length - 1, 1));
		}

		async void BtnStopClick(object sender, EventArgs e)
		{
			await this.RunIisServerStream(IIS.ResetStop);
			this.tabControl1.SelectTab(tpLog);
		}

		async void BtnStartClick(object sender, EventArgs e)
		{
			await StartIis();
			this.tabControl1.SelectTab(tpLog);
		}

		async void BtnStatusClick(object sender, EventArgs e)
		{
			await this.RunIisServerStream(IIS.ResetStatus);
			this.tabControl1.SelectTab(tpLog);
		}

		async void BtnRestartClick(object sender, EventArgs e) { await this.RunIisServerStream(IIS.Restart); this.tabControl1.SelectTab(tpLog); }

		async void UcIISLoad(object sender, EventArgs e) { await this.RunIisServerStream(IIS.ResetStatus); }

		async void BtnKillClick(object sender, EventArgs e)
		{
			await ControlStreamHelper.RunStreaming(IIS.Kill, new[] { btnKill }, richTextBox1, btnCancel);
		}

		public async Task RefreshData()
		{
			await this.RunIisServerStream(IIS.ResetStatus);
			Refreshed = DateTime.Now;
			Status = "Refreshed at " + Refreshed;
		}


		public DateTime? Refreshed { get; private set; }

		async void BtnCleanClick(object sender, EventArgs e)
		{
			await this.Clean();
		}

		async void BtnListAppsClick(object sender, EventArgs e)
		{
			using (_profiler.Step())
			{
				if (_cts != null) return;
				var isLocal = StringExtensions.IsNullOrEmpty(cmbServer.Text) || cmbServer.Text == "localhost";
				var disables = new Control[]
					               {
						               richTextBox1, btnClean, btnKill, btnListApps, btnRestart, contextMenuStrip1, dgApps, btnStart,
						               btnStop, btnStatus, cmbServer
					               };
				try
				{

					using (_cts = new CancellationTokenSource())
					{
						disables.ToList().ForEach(d => d.Enabled = false);
						IEnumerable<IisAppInfo> data;
						if (isLocal)
						{
							data = await IIS.ListAppsFormatted(_cts.Token);
						}
						else
						{
							var iis = _administrator(cmbServer.Text);
							if (iis == null) return;
							using (_profiler.Step("iis.GetAppInfo"))
							{
								data = await iis.GetAppInfo();
							}
						}
						//var server = cmbServer.Text.IsIgnoreCaseMatch("localhost") ? null : cmbServer.Text;

					    var rr = new RemoteRegistry(cmbServer.Text);

						var source = from a in data.ToArray()
						             from v in a.Virtuals
                                     let expandedPath = isLocal ? Environment.ExpandEnvironmentVariables(v.PhysicalPath) : rr.ExpandEnvironmentVariables(v.PhysicalPath)
						             let exists =
							             isLocal
                                             ? System.IO.Directory.Exists(expandedPath)
                                             : !expandedPath.Contains("%")  //some environment variables on remote machine did not map
                                                && new NetworkShare(cmbServer.Text).DirectoryExists(expandedPath)
						             select
							             new IisConfigModel(
							             a.AppName,
							             a.AppPath,
							             a.Pool,
							             v.PhysicalPath,
							             async value => await RunIisStream(ct => IIS.SetVDirPath(a.AppName, v.VirDir, value, ct)),
							             v.VirDir,
							             exists);
						dgApps.DataSource = source.ToList();
						tabControl1.SelectedTab = tpApps;
					}
				}
				finally
				{
					_cts = null;
					disables.ToList().ForEach(d => d.Enabled = true);
				}
			}
		}

		void DataGridView1CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 1)
				return;
			const string columnName = "AppName";
			if (dgApps.Columns[e.ColumnIndex].Name != columnName)
				return;
			var currentRow = dgApps.Rows[e.RowIndex];
			FormatForExists(columnName, e, currentRow);
			FormatForGrouping(columnName, e, currentRow);
		}

		void FormatForExists(string columnName, DataGridViewCellFormattingEventArgs e, DataGridViewRow currentRow)
		{
			if (dgApps.Columns["Exists"].Visible == false) return;
			if (currentRow.Cells["Exists"].Value.CastTo<bool>()) return;
			currentRow.Cells[columnName].Style.ForeColor = Color.Red;

		}

		void FormatForGrouping(string columnName, DataGridViewCellFormattingEventArgs e, DataGridViewRow currentRow)
		{

			var previousRow = dgApps.Rows[e.RowIndex - 1];
			var areSame = currentRow.Cells[columnName].Value == previousRow.Cells[columnName].Value;
			if (areSame && previousRow.HasDefaultCellStyle == false)
				return;
			if (areSame && previousRow.HasDefaultCellStyle)
			{
				currentRow.DefaultCellStyle = previousRow.DefaultCellStyle;
			}

			if (previousRow.HasDefaultCellStyle == false) //not the same
			{
				currentRow.DefaultCellStyle = new DataGridViewCellStyle { BackColor = Color.LightGray };
			}
		}

		IEnumerable<IisConfigModel> Items
		{
			get
			{
				if (dgApps.DataSource == null)
					return Enumerable.Empty<IisConfigModel>();
				return dgApps.DataSource.CastTo<IEnumerable<IisConfigModel>>();
			}
		}

		public void Save()
		{

			var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var savePath = System.IO.Path.Combine(folder, "uciis.json");
			Save(savePath);
		}

		public bool CanSave()
		{
			if (Items == null)
			{
				Log("No app source");
				return false;
			}

			if (Items.Any() == false)
			{
				Log("No apps loaded");
				return false;
			}
			return true;
		}

		public string Status
		{
			get { return _status; }
			private set
			{
				if (_status != value)
				{
					_status = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		public void Save(string path)
		{
			if (CanSave() == false)
				return;
			System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(Items));
			Status = "Wrote apps configuration to " + path;
			Log("Wrote apps configuration to " + path);
		}

		void ContextMenuStrip1Opening(object sender, CancelEventArgs e)
		{
			if (this.dgApps.SelectedCells.Cast<DataGridViewCell>().Any() == false) e.Cancel = true;
		}

		DataGridViewRow GetSelectedRow()
		{
			var cell = this.dgApps.SelectedCells.Cast<DataGridViewCell>().First();
			var row = this.dgApps.Rows[cell.RowIndex];
			return row;
		}

		string GetRowCellValue(DataGridViewRow row, Expression<Func<IisConfigModel,object>> selector)
		{
			var columnName = LinqOp.PropertyOf(selector).Name;
			return row.Cells[columnName].Value.ToString();
		}

		void RunAppPoolGridCmd(Func<IAdministerIIS, string, string> cmdFunc)
		{
			using (_profiler.Step())
			{
				var iis = _administrator(cmbServer.Text);
				if (iis == null) return;
				var row = this.GetSelectedRow();
				tabControl1.SelectTab(tpLog);
				var cellValue = this.GetRowCellValue(row, icm => icm.Pool);
				var result = cmdFunc(iis, cellValue);

				richTextBox1.AppendText(Environment.NewLine + cellValue + ":" + result + Environment.NewLine);
			}
		}

		void MStartPoolClick(object sender, EventArgs e)
		{
			using (_profiler.Step())
			{
				this.RunAppPoolGridCmd((iis, poolName) => iis.StartAppPool(poolName));
			}
		}

		void RecycleToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (_profiler.Step())
			{
				this.RunAppPoolGridCmd((iis, poolName) => iis.RecycleAppPool(poolName));
			}
		}

		void StopToolStripMenuItem1Click(object sender, EventArgs e)
		{
			using (_profiler.Step())
			{
				this.RunAppPoolGridCmd((iis, poolName) => iis.StopAppPool(poolName));
			}
		}

		void StatusToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (_profiler.Step())
			{
				this.RunAppPoolGridCmd((iis, poolName) => iis.AppPoolStatus(poolName));
			}
		}

		void BtnClearClick(object sender, EventArgs e)
		{
			richTextBox1.Clear();
		}

		void BtnSaveClick(object sender, EventArgs e)
		{
			this.Save();
		}


	}
}
