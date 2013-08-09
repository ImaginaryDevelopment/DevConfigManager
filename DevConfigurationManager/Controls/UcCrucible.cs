using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using DeveloperConfigurationManager.CrossCutting;
using Domain.Adapters;
using Domain.Extensions;
using Domain.Helpers;
using Domain.Models;
using Domain.Models.Crucible;

namespace DeveloperConfigurationManager.Controls
{
    /// <summary>
	/// https://developer.atlassian.com/display/FECRUDEV/REST+API+Guide
	/// </summary>
	public partial class UcCrucible : UserControl, IRefreshable, INotifyPropertyChanged, ICanHaveAnIcon
	{
		string _status;

		readonly Func<string> _crucibleAuthorityGetter;

		readonly Func<bool> _hasStoredCredentials;

		readonly Func<ICanDownload> _downloaderFunc;

		readonly Profiler _profiler;

		Icon _icon;

		public UcCrucible(Func<string> crucibleAuthorityGetter, Func<bool> hasStoredCredentials, Func<ICanDownload> crucibleDownloaderFunc,Profiler profiler)
		{
			_crucibleAuthorityGetter = crucibleAuthorityGetter;
			_hasStoredCredentials = hasStoredCredentials;
			_downloaderFunc = crucibleDownloaderFunc;
			_profiler = profiler;
			InitializeComponent();
			
			tabControl1.TabPages.Remove(tpComments);
			tabControl1.TabPages.Remove(tpReviewers);
			
			dgComments.BindAsLink<ReviewComment>(DgCommentsCellContentClick, c => c.Id, c => c.ReviewItemId);
			dgReviews.BindAsLink<ReviewData>(cell => System.Diagnostics.Process.Start(cell.Value.ToString()), rd => rd.Link);

			if (_hasStoredCredentials())
			{
				label1.Visible = false;
			}
		}

		async Task LoadIcon()
		{
			var url = _crucibleAuthorityGetter() + "/favicon.ico";
			var uri = new Uri("http://" + url);
			try
			{
				var icon = await _downloaderFunc().DownloadIcon(uri, true);
				Icon = icon;
			}
			catch (Exception ex)
			{
				richTextBox1.AppendText(ex.Message + Environment.NewLine, Color.Red);
			}
		}

		void OnAwait(bool isResuming)
		{
			btnPopulate.Enabled = isResuming;
			btnPopulate.UseWaitCursor = !isResuming;
		}

		public async Task RefreshData()
		{
			if (_hasStoredCredentials())
			{
				label1.Visible = false;
			}

			if (StringExtensions.IsNullOrEmpty(_crucibleAuthorityGetter()))
			{
				tabControl1.SelectTab(tpText);
				richTextBox1.AppendText("No crucible authority found"+Environment.NewLine,Color.Red);
				return;
			}
			
			OnAwait(false);
			using (_profiler.Step())
			{
				
			
			IEnumerable<ReviewData> crucible;
				using (_profiler.Step("GetOpenReviews"))
				{
					crucible =
						await
						new Crucible(new Logging(richTextBox1.AppendText), _downloaderFunc()).GetOpenReviews(
							Environment.UserName, _crucibleAuthorityGetter());
				}

				var ordered = crucible.OrderByDescending(o => o.Participating).ThenBy(o => o.IsCompletedByMe).ThenBy(o => o.Id).ToArray();

			dgReviews.DataSource = ordered;
			var reformatColumn = dgReviews.Columns.Cast<DataGridViewColumn>().FirstOrDefault(c => c.DataPropertyName == "Link");
			if (reformatColumn != null)
				ReformatColumn(dgReviews, reformatColumn);
			}
			this.OnAwait(true);
			tabControl1.SelectTab(tpGrid);
			Refreshed = DateTime.Now;
			Status = "Refreshed at " + Refreshed;
#pragma warning disable 4014
			if (Icon == null) LoadIcon();
#pragma warning restore 4014

		}

		void ReformatColumn(DataGridView dg, DataGridViewColumn reformatColumn)
		{
			if (reformatColumn == null)
			{
				throw new NullReferenceException("reformatColumn");
			}

			if (reformatColumn is DataGridViewLinkColumn) return;
			dg.Columns.Remove(reformatColumn);
			dg.Columns.Add(new DataGridViewLinkColumn {
					DataPropertyName = reformatColumn.DataPropertyName,
					HeaderText = reformatColumn.HeaderText,
					Name = reformatColumn.Name,
				});

		}

		async void UcCrucibleLoad(object sender, EventArgs e)
		{
			if (_hasStoredCredentials())
			{
				OnAwait(false);
				await RefreshData();
				this.OnAwait(true);

			}
		}

		async void BtnPopulateClick(object sender, EventArgs e)
		{
			await this.RefreshData();
		}

		DataGridViewColumn TryGetColumn(Expression<Func<ReviewData, object>> selector)
		{
			return
				dgReviews.Columns.Cast<DataGridViewColumn>().FirstOrDefault(
					c => LinqOp.PropertyOf(selector).Name == c.DataPropertyName);
		}

		void DataGridView1CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var columnName = dgReviews.Columns[e.ColumnIndex].DataPropertyName;
			var participatingColumn = TryGetColumn(info => info.Participating);
			var isCompletedColumn = this.TryGetColumn(info => info.IsCompletedByMe);
			if (participatingColumn != null && isCompletedColumn != null && e.ColumnIndex == participatingColumn.Index)
			{
				// color row red if participating and not complete
				var participating = (bool)e.Value;
				var completed = (bool)dgReviews.Rows[e.RowIndex].Cells[isCompletedColumn.Index].Value;
				if (participating && !completed)
				{
					dgReviews.Rows[e.RowIndex].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Red };
				}
				else if (participating && completed)
				{
					dgReviews.Rows[e.RowIndex].DefaultCellStyle = new DataGridViewCellStyle { ForeColor = Color.Orange };
				}
			}
			var completedReviewersColumn = TryGetColumn(info => info.CompletedReviewers);

			if (completedReviewersColumn != null && columnName == completedReviewersColumn.DataPropertyName)
			{
				var value = (IEnumerable<string>)e.Value;
				if (value.Any())
				{
					e.Value = value.OrderBy(x => x).Delimit(", ");
					e.FormattingApplied = true;
				}
			}

			var incompleteColumn = this.TryGetColumn(info => info.IncompleteReviewers);
			if (incompleteColumn != null && columnName == incompleteColumn.DataPropertyName)
			{
				var value = (IEnumerable<string>)e.Value;
				if (value.Any())
				{
					e.Value = value.OrderBy(x => x).Delimit(", ");
					e.FormattingApplied = true;
				}
			}
		}

		TResult GetReviewRowValue<TResult>(DataGridViewRow row, Expression<Func<ReviewData, TResult>> selector)
		{
			return row.GetValue(selector);
		}

		TResult GetCommentRowValue<TResult>(DataGridViewRow row, Expression<Func<ReviewComment, TResult>> selector)
		{
			return row.GetValue(selector);
		}

		async void ReviewersToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (mnuGrid.Tag == null) return;
			var srcRow = mnuGrid.Tag.CastTo<int>();

			var row = dgReviews.Rows[srcRow];
			var id = this.GetReviewRowValue(row, rd => rd.Id);
			await ShowReviewers(id);
		}

		async Task ShowReviewers(string id)
		{
			var l = new Crucible(new Logging(richTextBox1.AppendText), _downloaderFunc());

			OnAwait(false);

			var reviewers = await l.GetReviewReviewers(_crucibleAuthorityGetter(), id);

			dgReviewers.DataSource = reviewers.ToArray();
			tabControl1.SelectTab(tpReviewers);
			this.OnAwait(true);
		}

		void DgReviewsMouseDown(object sender, MouseEventArgs e)
		{
			mnuGrid.Tag = null;
			var hit = dgReviews.HitTest(e.X, e.Y);
			if (hit != null && hit.RowIndex >= 0) mnuGrid.Tag = hit.RowIndex;
		}

		void MnuGridOpening(object sender, CancelEventArgs e)
		{
			if (mnuGrid.Tag == null) e.Cancel = true;
			if (tabControl1.SelectedTab != tpGrid) e.Cancel = true;
		}

		async void CommentsToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (mnuGrid.Tag == null) return;
			var srcRow = mnuGrid.Tag.CastTo<int>();

			var row = dgReviews.Rows[srcRow];
			var id = this.GetReviewRowValue(row, rd => rd.Id);
			await ShowComments(id);
		}

		async Task ShowComments(string id)
		{
			var l = new Crucible(new Logging(richTextBox1.AppendText), _downloaderFunc());
			this.OnAwait(false);
			var reviewers = await l.GetReviewComments(_crucibleAuthorityGetter(), id);
			this.OnAwait(true);
			dgComments.DataSource =
				reviewers
				.OrderBy(r => r.User == Environment.UserName)
				.ThenByDescending(r => r.ReadStatus)
				.ThenBy(r => r.Id).ToArray();

			tabControl1.SelectTab(tpComments);
		}

		void DgCommentsCellContentClick(DataGridViewLinkCell cell)
		{
			string target = cell.Value.ToString();
			var review = GetCommentRowValue(cell.OwningRow, rc => rc.ReviewId);
			if (cell.OwningColumn.DataPropertyName == LinqOp.PropertyOf<ReviewComment>(rc => rc.Id).Name)
			{
				string commentId = this.GetCommentRowValue(cell.OwningRow, rc => rc.Id);
				//http://server:8060/cru/DEVCR-171#c1416
				//http://server/cru/PRODR-26#c1006
				target = "http://" + _crucibleAuthorityGetter() + "/cru/" + review + "#c" + StringExtensions.AfterOrSelf(StringExtensions.AfterOrSelf(commentId, ":"), "-");
			}
			else if (cell.OwningColumn.DataPropertyName == LinqOp.PropertyOf<ReviewComment>(rc => rc.ReviewItemId).Name)
			{

				string reviewItemId = this.GetCommentRowValue(cell.OwningRow, rc => rc.ReviewItemId);

				//http://server/cru/PRODR-26#CFR-16926
				target = "http://" + _crucibleAuthorityGetter() + "/cru/" + review + "#" + reviewItemId;
			}

			System.Diagnostics.Process.Start(target);
		}

		void TabControl1Selecting(object sender, TabControlCancelEventArgs e)
		{
			if (e.TabPage.Enabled == false) e.Cancel = true;
		}

		void DgReviewersDataSourceChanged(object sender, EventArgs e)
		{
			if(tabControl1.TabPages.Contains(tpReviewers))
				return;
			tabControl1.TabPages.Insert(1, tpReviewers);
		}

		void DgCommentsDataSourceChanged(object sender, EventArgs e)
		{
			if (tabControl1.TabPages.Contains(tpComments))
				return;
			tabControl1.TabPages.Insert(tabControl1.TabPages.Count - 1, tpComments);
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

		public DateTime? Refreshed { get; private set; }

		public Icon Icon
		{
			get
			{
				return _icon;
			}
			set
			{
				if (_icon != value)
				{
					_icon = value;
					OnPropertyChanged();
				}

			}
		}
	}
}
