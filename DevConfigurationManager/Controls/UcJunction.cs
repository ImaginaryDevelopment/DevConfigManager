using System;
using System.Collections.ObjectModel;
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
using Domain.Models;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcJunction : UserControl, IRefreshable, INotifyPropertyChanged
	{
		readonly ObservableCollection<string> _savedJunctions;

		//readonly Latch _latch = new Latch();
		string _status;

		CancellationTokenSource _cts;

		public UcJunction(ObservableCollection<string> junctions, AccessorHelper<string> junctionTargetAccessor)
		{
			this._savedJunctions = junctions;
			InitializeComponent();
			cbJunctionTarget.DisplayMember = "Name";
			cbJunctionTarget.ValueMember = "Name";
			cbJunctionPath.DataSource = this._savedJunctions;
			cbJunctionPath.Text = junctionTargetAccessor.Getter();
			cbJunctionPath.ToObservableText().Subscribe(s => junctionTargetAccessor.Setter(s));

		}

		async protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
		    this.txtJunctionRelativeGitPath.Text = Properties.Settings.Default.JunctionRelativeGitPath;
			await this.RefreshData();

			cbJunctionTarget.SelectedValueChanged += this.JunctionTargetSelectedValueChanged;
			
			cbJunctionPath.TextChanged += (sender, args) =>
			{
				if (_cts != null)
					return;
// ReSharper disable ExplicitCallerInfoArgument
				OnPropertyChanged(LinqOp.PropertyOf(() => JunctionPath).Name);
// ReSharper restore ExplicitCallerInfoArgument
			};
		}

		string JunctionPath { get { return cbJunctionPath.Text; } }
		
		async void JunctionTargetSelectedValueChanged(object sender, EventArgs e)
		{
			if (_cts != null) return;
			bool doTextUpdate = false;
			using (_cts = new CancellationTokenSource())
			{
				var j = new Domain.Adapters.Junction(cbJunctionPath.Text);
					if (cbJunctionTarget.SelectedValue == null)
						return;
					var value = cbJunctionTarget.SelectedValue.ToString();
					if ((await j.GetTarget(_cts.Token)) != value)
					{
						await j.SetTarget(value, _cts.Token);
						doTextUpdate = true;
					}
			}

			_cts = null;
			await RefreshData();
			if (doTextUpdate)
			{
				this.Status = Domain.Adapters.Junction.LastCommandText;
			}
		}

		async Task CleanSomething(Func<Domain.Adapters.Junction, CancellationToken, StreamingOuts> func)
		{
			if (_cts != null) return;

			using (_cts = new CancellationTokenSource())
			{
				var j = new Domain.Adapters.Junction(this.cbJunctionPath.Text);
				await ControlStreamHelper.RunStreaming(
					ct => func(j, ct), new[] { btnBrowse, btnCleanBin, btnCleanBoth, btnObj }, this.richTextBox1, btnCancel, _cts);
			}

			_cts = null;
		}

		public async Task CleanObj()
		{
			await CleanSomething((junction, token) => junction.CleanObjAsync(token));
		}

		public async Task CleanBin()
		{
			await CleanSomething((j, ct) => j.CleanBinAsync(ct));
		}

		async void BtnObjClick(object sender, EventArgs e)
		{
			await CleanSomething((j, ct) => j.CleanObjAsync(ct));
		}

		async void BtnCleanBinClick(object sender, EventArgs e)
		{
			await CleanSomething((j, ct) => j.CleanBinAsync(ct));
		}

		async void Button1Click(object sender, EventArgs e)
		{
			await this.CleanBin();
			await this.CleanObj();
		}

		public string JunctionParent { get { return StringExtensions.IsNullOrEmpty(cbJunctionPath.Text) ? null : System.IO.Path.GetDirectoryName(cbJunctionPath.Text); } }

		public bool CheckParentExists() { return System.IO.Directory.Exists(JunctionParent); }

		public bool CheckJunctionExists() { return System.IO.Directory.Exists(JunctionPath); }

		public string Status
		{
			get
			{
				return _status;
			}

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

		public async Task RefreshData()
		{
			if (_cts != null) return;
            var git = new Git(Properties.Settings.Default.GitPath);
			if (StringExtensions.IsNullOrEmpty(cbJunctionPath.Text))
				return;
			this.cbJunctionTarget.Enabled = false;
			var target = System.IO.Path.GetDirectoryName(cbJunctionPath.Text);
			if (StringExtensions.IsNullOrEmpty(target) == false && System.IO.Directory.Exists(target))
			{
				var directoryCmd = new Domain.Adapters.DirectoryCmd(target);

				using (_cts = new CancellationTokenSource())
				{
					var dirInfo = await directoryCmd.GetDirInfo(_cts.Token);

					FullInfoAsync[] gitInfosAsync;
					try
					{

						gitInfosAsync =
                            git.GetGitInfo(directoryCmd.Directory, dirInfo.Directories, txtJunctionRelativeGitPath.Text, _cts.Token).Where(
								fi => fi.DirInfo.Name != "." && fi.DirInfo.Name != "..").ToArray();
						foreach (var item in gitInfosAsync)
						{
							if (item.GitInfo.Remotes != null)
							{
								await item.GitInfo.Remotes;
							}
						}
					}
					catch (Win32Exception wEx)
					{
						this.Status = wEx.Message;
						dgJunction.Columns.Clear();
						dgJunction.AutoGenerateColumns = true;
						var cbValues = dirInfo.Directories.Where(di => di.Name != "." && di.Name != "..").ToArray();
						dgJunction.DataSource = cbValues;
						cbJunctionTarget.DataSource = cbValues;
						_cts.Cancel(true);
						_cts = null;
						return;
					}
					if (gitInfosAsync.Any() == false)
					{
						_cts.Cancel(true);
						_cts = null;
						return;
					}
					FullInfo[] gitInfos = gitInfosAsync.Select(
						gia =>
						new FullInfo()
							{
								DirInfo = gia.DirInfo,
								GitInfo =
									new GitInfo()
										{
											IsGit = gia.GitInfo.IsGit,
											Remotes = gia.GitInfo.Remotes != null ? gia.GitInfo.Remotes.Result.ToString() : null
										}
							}).ToArray();
					
					var currentJunction =
						gitInfos.FirstOrDefault(
							gi => gi.GitInfo.IsGit && gi.DirInfo.Target != null && cbJunctionPath.Text.EndsWith(gi.DirInfo.Name));
					var q = gitInfos.Where(fi => fi.GitInfo.IsGit && fi.DirInfo.Target == null).Select(s => s.DirInfo);
					var dirInfos = q.ToArray();

					if (dirInfos.Length != cbJunctionTarget.Items.Count
						 || dirInfos.All(v => cbJunctionTarget.Items.Cast<DirInfo>().Any(di => di.Equals(v))) == false)
					{
						cbJunctionTarget.DataSource = dirInfos;
						if (currentJunction != null) cbJunctionTarget.SelectedItem = dirInfos.First(v => currentJunction.DirInfo.Target.EndsWith(v.Name, StringComparison.InvariantCultureIgnoreCase));
					}
					dgJunction.InvokeSafe(
						dg =>
						dg.DataSource =
						gitInfos.Where(g => g.GitInfo.IsGit).Select(
							s => new { s.DirInfo.Name, Remotes = DisplayRemotes(s.GitInfo.Remotes), s.DirInfo.Target }).ToList());
					this._savedJunctions.AddIfMissing(cbJunctionPath.Text);
					cbJunctionPath.DataSource = null;
					cbJunctionPath.DataSource = this._savedJunctions;
					Refreshed = DateTime.Now;
					Status = "Refreshed at " + Refreshed;
				}
				_cts = null;
				this.cbJunctionTarget.Enabled = true;
			}
			else
			{
				Refreshed = DateTime.Now;
				dgJunction.DataSource = null;
				Status = "Attempted refresh at " + Refreshed;
			}

		}

		private string DisplayRemotes(string remoteRaw)
		{
			if (remoteRaw == null) return null;
			remoteRaw = remoteRaw.Replace('\t', ' ');
			
			var lines = StringExtensions.SplitLines(remoteRaw);

			//test cases:
			//fails: 
			//github https://github.com/bdimperio/gitextensions.git (fetch)

			var remoteRegex = new Regex(@"(\w+)\s+(ssh://(\w|\.|')+@+\w+.\w+.\w+(?::\w+)?(?:\w|/|-|\.)*)\s*\((\w+)\)");
			var remotes =
				lines.Select(r => remoteRegex.Match(r)).Select(
						m => new { Name = m.Groups[1].Value, Uri = m.Groups[2].Value, Type = m.Groups[3].Value })
					.Where(
						n => StringExtensions.IsNullOrEmpty(n.Name) == false).OrderBy(o => o.Name).ToArray();
			for (int i = 0; i < remotes.Length - 1; i++)
			{
				if (remotes[i] == null) continue;
				if (remotes[i + 1].Name == remotes[i].Name && remotes[i + 1].Uri == remotes[i].Uri)
				{
					remotes[i + 1] = null;
					remotes[i] = new { remotes[i].Name, remotes[i].Uri, Type = "Both" };

				}

				//collapse items
			}
			remotes = remotes.Where(r => r != null).ToArray();
		    if (remotes.Any() == false)
		        return string.Empty;
			remoteRaw = remotes.Select(r => r.ToString()).Aggregate((s1, s2) => s1 + "\n" + s2);
			return remoteRaw;

		}

		private void BtnBrowseClick(object sender, EventArgs e)
		{
			using (var fbd = new FolderBrowserDialog())
			{
				if (CheckJunctionExists())
					fbd.SelectedPath = cbJunctionPath.Text;
				if (fbd.ShowDialog(this) == DialogResult.OK && StringExtensions.HasValue(fbd.SelectedPath) && fbd.SelectedPath != cbJunctionPath.Text)
				{
					cbJunctionPath.Text = fbd.SelectedPath;
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		void DataGridView1CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dgJunction.Columns[e.ColumnIndex].DataPropertyName != "Name")
				return;
			var row = dgJunction.Rows[e.RowIndex];
			if (row.Cells["Target"].Value != null)
				e.CellStyle.ForeColor = Color.Green;
		}

        private async void btnRefresh_Click(object sender, EventArgs e)
        {
            await RefreshData();
        }
	}
}
