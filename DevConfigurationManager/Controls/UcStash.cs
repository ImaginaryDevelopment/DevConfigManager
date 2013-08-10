using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using DeveloperConfigurationManager.CrossCutting;
using Domain.Extensions;
using Domain.Helpers;
using Domain.Models;
using Domain.Models.Stash;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcStash : UserControl, ICanHaveAnIcon,IRefreshable, INotifyPropertyChanged
	{
		string _status;

		Icon _icon;

		readonly string _stashAuthority;

		readonly Func<ICanDownload> _stashDownloaderFunc;

		readonly Func<bool> _hasStoredCredentials;

		static IDictionary<string,StashRestUrl> GetStashes(string stashAuthority)
		{
			//"http://" + stashAuthority + "/rest/api/1.0/projects/DEVC/repos/hpx-current/pull-requests?state=OPEN"
			var uris = new Dictionary<string, StashRestUrl>
				{

				};
return uris;
		}

		public UcStash(string stashAuthority, Func<ICanDownload> stashDownloaderFunc, Func<bool> hasStoredCredentials)
		{
			_stashAuthority = stashAuthority;
			_stashDownloaderFunc = stashDownloaderFunc;
			_hasStoredCredentials = hasStoredCredentials;
			this.InitializeComponent();
			if (hasStoredCredentials()) label1.Visible = false;
			var uris = GetStashes(this._stashAuthority);

			if (uris.Any())
			{
				richTextBox1.AppendText("Using the following repositories" + Environment.NewLine);
				richTextBox1.AppendText(uris.Select(u => u.Key + ":" + u.Value).Delimit(Environment.NewLine), Color.Blue);
				richTextBox1.AppendText(Environment.NewLine);
				cbName.DataSource = uris.ToArray();
				cbName.DisplayMember = "Key";
				cbName.ValueMember = "Value";
				cbName.SelectedIndex = 0;
			}
			
		}

		[Obsolete("Designer only")]
		public UcStash()
		{
			InitializeComponent();

		}

		public async Task RefreshData()
		{
			btnPopulate.Enabled = false;
			btnPopulate.UseWaitCursor = true;
			var stash = new Domain.Adapters.Stash(new Logging(richTextBox1.AppendText), _stashDownloaderFunc());
			try
			{
				var l = await stash.GetOpenPullRequests(Environment.UserName, _stashAuthority, cbName.Items.Cast<KeyValuePair<string, StashRestUrl>>().ToArray());

				dgMergeReviews.DataSource = l;
				var reformatColumn =
					dgMergeReviews.Columns.Cast<DataGridViewColumn>().FirstOrDefault(
						c => LinqOp.PropertyOf(() => l.First().Link).Name == c.DataPropertyName);

				ReformatColumn(reformatColumn);
				Refreshed = DateTime.Now;
				Status = "Refreshed at " + Refreshed;
			}
			catch (WebException wex)
			{
				richTextBox1.AppendText(wex.Message, Color.Red);
			}

			btnPopulate.Enabled = true;
			btnPopulate.UseWaitCursor = false;
			if (Icon == null) LoadIcon();
		}

		async void UcStashLoad(object sender, EventArgs e)
		{
			try
			{
				await RefreshData();
				if (_hasStoredCredentials())
					await LoadIcon();
			}
			catch (Exception ex)
			{
				richTextBox1.AppendText(ex.Message + Environment.NewLine, Color.Red);
			}
		}

		async Task LoadIcon()
		{
			var url = _stashAuthority + "/favicon.ico";
			var uri = new Uri("http://" + url);
			try
			{
				var icon = await _stashDownloaderFunc().DownloadIcon(uri, true);
				Icon = icon;
			}
			catch (Exception ex)
			{
				richTextBox1.AppendText(ex.Message,Color.Red);

			}
			
		}

		void ReformatColumn(DataGridViewColumn reformatColumn)
		{
			dgMergeReviews.Columns.Remove(reformatColumn);
			dgMergeReviews.Columns.Add(new DataGridViewLinkColumn { DataPropertyName = reformatColumn.DataPropertyName, HeaderText = reformatColumn.HeaderText });
		}

		void DataGridView1CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			var row = dgMergeReviews.Rows[e.RowIndex];
			var cell = row.Cells[e.ColumnIndex];
			if (cell is DataGridViewLinkCell)
			{
				System.Diagnostics.Process.Start(cell.Value.ToString());
			}
		}

		async void BtnPopulateClick(object sender, EventArgs e)
		{
			if (_hasStoredCredentials())
			{
				label1.Visible = false;
			}
			await RefreshData();
			
		}

		void DataGridView1CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			var columnName = dgMergeReviews.Columns[e.ColumnIndex].DataPropertyName;
			
			var remainingColumn =
				dgMergeReviews.Columns.Cast<DataGridViewColumn>().FirstOrDefault(
					c => LinqOp.PropertyOf<StashPullRequestInfo>(info => info.RemainingReviewers).Name == c.DataPropertyName);

			if (remainingColumn != null && columnName == remainingColumn.DataPropertyName)
			{
				e.Value = ((IEnumerable<string>)e.Value).OrderBy(x => x).Delimit(", ");
				e.FormattingApplied = true;
			}

			var suggestedColumn = dgMergeReviews.Columns.Cast<DataGridViewColumn>().FirstOrDefault(
					c => LinqOp.PropertyOf<StashPullRequestInfo>(info => info.SuggestedReviewers).Name == c.DataPropertyName);
			if (suggestedColumn != null && columnName == suggestedColumn.DataPropertyName)
			{
				e.Value = ((IEnumerable<string>)e.Value).OrderBy(x => x).Delimit(", ");
				e.FormattingApplied = true;
			}
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
