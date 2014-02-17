using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using Domain;
using Domain.Adapters;
using Domain.Extensions;
using Domain.Helpers;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcService : UserControl, IRefreshable, INotifyPropertyChanged, IHaveDefault
	{
		IEnumerable<Sc.ScQueryOutput> _queryResults;

		CancellationTokenSource _cts;

		string _status;

		public UcService()
		{
			InitializeComponent();

			txtFilter.ToObservableText().Subscribe(_ => FilterData());

			dgServiceList.MouseDown += dgServiceList_MouseDown;
			
			contextMenuStrip1.Items.Cast<ToolStripItem>().ToList().ForEach(cm =>
				cm.ToObservableClick().Subscribe(async e =>
					{
						//var src = contextMenuStrip1.SourceControl;
						if (contextMenuStrip1.Tag == null) 
							return;
						var srcRow = contextMenuStrip1.Tag.CastTo<int>();
						
						var row = dgServiceList.Rows[srcRow];
						await RunAction(txtServiceTarget.Text, cm.Text, row.Cells["ServiceName"].Value.ToString());
					}));
		}

		void dgServiceList_MouseDown(object sender, MouseEventArgs e)
		{
			contextMenuStrip1.Tag = null;
			var hit = dgServiceList.HitTest(e.X, e.Y);
			if (hit != null && hit.RowIndex >= 0) contextMenuStrip1.Tag = hit.RowIndex;
		}

		void FilterData()
		{

			if (txtFilter.Text.IsNullOrEmpty())
			{
				dgServiceList.DataSource = _queryResults.ToList();
				return;
			}

			var filter = txtFilter.Text;
			if (cbFilterColumn.Text.IsNullOrEmpty() || cbFilterColumn.Text == LinqOp.PropertyOf(() => _queryResults.First().ServiceName).Name)
			{
				if (filter.Contains("^") || filter.Contains("?"))
				{
					dgServiceList.DataSource =
						_queryResults.Where(sc => sc.ServiceName.IsRegexMatch(filter, RegexOptions.IgnoreCase)).ToList();
				}
				else
					dgServiceList.DataSource =
						_queryResults.Where(sc => sc.ServiceName.ToLowerInvariant().Contains(txtFilter.Text.ToLowerInvariant())).ToList();
			} else
			{
				var getter = _queryResults.GetType().GetEnumerableType().GetProperty(cbFilterColumn.Text).GetGetMethod();
				dgServiceList.DataSource =
					_queryResults.Where(
						sc => getter.Invoke(sc, null).ToString().ToLowerInvariant().Contains(txtFilter.Text.ToLowerInvariant())).ToList();
			}
		
	}


		async void BtnQueryClick(object sender, EventArgs e)
		{
			await this.RefreshData();

		}

		async Task<StreamOuts> RunQuiet(Func<CancellationToken, StreamingOuts> func)
		{
			if (_cts != null) return null;

			try
			{
				using (_cts = new CancellationTokenSource())
				{
					return await ControlStreamHelper.RunStreaming(
						func,
						new[] { btnQuery },
						null,
						btnCancel,
						_cts);
				}
			}
			finally
			{
				_cts = null;
			}
		}
		async Task<StreamOuts> RunAction(Func<CancellationToken, StreamingOuts> func)
		{
			if (_cts != null) return null;

			try
			{
				using (_cts = new CancellationTokenSource())
				{
					return await ControlStreamHelper.RunStreaming(
						func,
						new[] { btnQuery },
						richTextBox1,
						btnCancel,
						_cts);
				}
			}
			finally
			{
				_cts = null;
			}
		}
		async Task<StreamOuts> RunAction(string server, string action, string serviceName)
		{
			if (action.IsNullOrEmpty()) return null;
			tabControl1.SelectTab(tbLog);
			var result = RunAction(ct => Sc.Run(server, action, serviceName, ct));
			return await result;

		}

		private void dgServiceList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{

			if (dgServiceList.Columns[e.ColumnIndex].DataPropertyName == "ServiceName") StyleServiceName(e.RowIndex);
			if (dgServiceList.Columns[e.ColumnIndex].DataPropertyName == "State") StyleState(e.RowIndex);
		}

		void StyleState(int rowIndex)
		{
			var row = dgServiceList.Rows[rowIndex];
			var cell = row.Cells["State"];

			if (cell.Value == null) return;
			var value = cell.Value.ToString();
			if (value.IsRegexMatch("\\sRUNNING\\s"))
			{
				cell.Style.ForeColor = Color.Green;
				return;
			}
			if (value.IsRegexMatch("\\sSTOPPED\\s"))
			{
				cell.Style.ForeColor = Color.Red;
				return;
			}
			if (value.IsRegexMatch("\\sSTOP_PENDING\\s"))
			{
				cell.Style.ForeColor = Color.Orange;
				return;
			}
			if (value.IsRegexMatch("\\sSTART_PENDING\\s"))
			{
				cell.Style.ForeColor = Color.GreenYellow;
			}

		}

		void StyleServiceName(int rowIndex)
		{
            
			var row = dgServiceList.Rows[rowIndex];
			var cell = row.Cells["ServiceName"];

			if (cell.Value != null && IIS.IsServiceIisRelated (cell.Value.ToString()))
				cell.Style.ForeColor = Color.Blue;
            
		}

		private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
		{
			if (contextMenuStrip1.Tag == null) e.Cancel = true;
		}

		public async Task RefreshData()
		{
			dgServiceList.DataSource = null;
			Status = "Refreshing";
			var task = this.RunQuiet(ct => Sc.Query(txtServiceTarget.Text, ct));
			
			var streams = await task;

			if (task.IsCanceled || streams == null)
			{
				Status = "Refresh Cancelled";
				return;
			}
			_queryResults = Sc.TransformScQuery(streams.Output).ToList();
			if (_queryResults.Any())

			{
                var results=_queryResults.ToList();

			    var missing = IIS.FindMissingExpectedIisServices(results.Select(r => r.ServiceName)).ToArray();
                richTextBox1.InvokeSafeAppend("Missing services:"+missing.Delimit(","));
			    dgServiceList.DataSource = results;
				cbFilterColumn.DataSource = _queryResults.GetType().GetEnumerableType().GetProperties().Select(p => p.Name).ToList();
			}
			Refreshed = DateTime.Now;
			Status = "Refreshed at " + this.Refreshed.ToString();
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

		public DateTime? Refreshed { get; private set; }

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		public Button Accept
		{
			get
			{
				return this.btnQuery;
			}
		}

		public event EventHandler AcceptButtonChangedEvent;
	}
}
