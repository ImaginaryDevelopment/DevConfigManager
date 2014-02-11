using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using Domain;
using Domain.Adapters;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcGit : UserControl, IRefreshable, INotifyPropertyChanged
	{
        
		string _junctionPath;
        
        string GitTargetPath {get
        {
            return System.IO.Path.Combine(_junctionPath, _junctionRelativePathTarget ?? string.Empty);
        }}
		[Obsolete("For designer only")]
		public UcGit()
		{
			InitializeComponent();
		}

        public UcGit(string junctionTarget, IObservable<string> junctionTargetNotifier, string junctionRelativeGitPath, IObservable<string> junctionRelativeGitPathNotifier)
		{
			
			this.InitializeComponent();
			_junctionPath = junctionTarget;
            _junctionRelativePathTarget = junctionRelativeGitPath;
			junctionTargetNotifier.Subscribe(s => _junctionPath = s);
            junctionRelativeGitPathNotifier.Subscribe(s => _junctionRelativePathTarget = s);

		}

		async Task<Git.StatusStreamOuts> GetStatus()
		{
            btnStatus.Enabled = false;
            
		    var git = new Git(Properties.Settings.Default.GitPath);
            var result = await this.Run(git.CheckStatus);
            btnStatus.Enabled = true;
			if (result == null) return null;
			int length = rtLog.InvokeSafe(s => s.TextLength);
			var newText = result.ToString().Replace("\\r\\n", Environment.NewLine).Replace("\\\"", "\"").Replace("\\t", "\t");
			rtLog.InvokeSafeAppend(newText);
			Refreshed = DateTime.Now;
			Status = "Refreshed at " + Refreshed;
            
			if (result.Branch.Length < 1)
			{
				return result;
			}
			if (length < rtLog.InvokeSafe(s => s.TextLength)) //make sure the textbox is longer than before
			{
				rtLog.InvokeSafe(rch =>
					{
						int i = length;
						while (i < rch.TextLength)
						{
							var branchText = rch.Find(result.Branch, i, RichTextBoxFinds.None);
							if (branchText == -1) break;
							rch.Select(branchText, result.Branch.Length);
							rch.SelectionColor = Color.Blue;
							i = branchText + result.Branch.Length;
						}
						
					});
			}
			return result;
		}

		async void BtnStatusClick(object sender, EventArgs e)
		{
			await this.GetStatus();
		}

		CancellationTokenSource _cts;

		string _status;

		async void BtnUnpushedClick(object sender, EventArgs e)
		{
		    var git = new Git(Properties.Settings.Default.GitPath);
            await RunGitStream(git.GetUnpushed);
		}

		async Task<T> Run<T>(Func<string, CancellationToken, Task<T>> func)
		{
			if (_cts != null)
			{
				return default(T);
			}

			if (_junctionPath.IsNullOrEmpty())
			{
				rtLog.InvokeSafeAppend("No path set" + Environment.NewLine);
				return default(T);
			}

			T result;
			try
			{
				using (_cts = new CancellationTokenSource())
				{
					_cts.Token.Register(() => Domain.Adapters.Process.Kill("ssh.exe", new CancellationToken())); //the kill spawned processes should not cancel
					_cts.Token.Register(() => Domain.Adapters.Process.Kill("git.exe", new CancellationToken())); //the kill spawned processes should not cancel
                    if (System.IO.Directory.Exists(GitTargetPath) == false)
                        throw new System.IO.DirectoryNotFoundException();
                    result = await func(GitTargetPath, _cts.Token);
				}
			}
			finally
			{
				_cts = null;
			}

			return result;
		}
		IEnumerable<Control> GetDisables()
		{
			return this.Controls.Cast<Control>().OfType<Button>().Where(b => b != btnCancel && b != btnBash).ToArray();
		}
		Task<StreamOuts> RunGitStream(Func<string, CancellationToken, StreamingOuts> func)
		{
			return 
				Run(
					(p, ct) =>
					ControlStreamHelper.RunStreaming(
						ct2 => func(p, ct2),
						GetDisables(),
						rtLog,
						btnCancel,
						_cts));
		}

		async void BtnPushClick(object sender, EventArgs e)
		{
		    var git = new Git(Properties.Settings.Default.GitPath);
            await RunGitStream(git.Push);
			//await UseGit(async s => await Domain.Adapters.Git.Push(s, TimeSpan.FromSeconds(3)));
		}

		async void BtnPullMasterClick(object sender, EventArgs e)
        {
            var git = new Git(Properties.Settings.Default.GitPath);
            await this.RunGitStream((s, ct) => git.Pull(s, "master", ct));
		}

		async void BtnPullClick(object sender, EventArgs e)
		{

			try
			{
				var status = await this.GetStatus();
				if (status == null)
				{
					rtLog.AppendText("GetStatus failed, cancelling branch pull");
					return;
				}
				if (status.Branch.IsNullOrEmpty())
				{
					rtLog.AppendText("not currently on a branch, pull cancelled");
					return;
				}
                var git = new Git(Properties.Settings.Default.GitPath);
				await this.RunGitStream((p, ct) => git.Pull(p, status.Branch, ct));
			}
			catch (AggregateException aex)
			{
				Debug.Assert(aex.InnerException is OperationCanceledException);
			}
		}

		void BtnBashClick(object sender, EventArgs e)
		{
            Domain.Adapters.Git.Bash(GitTargetPath);
			rtLog.AppendText(Domain.Adapters.Git.LastCommandText + Environment.NewLine);
			this.Status = Domain.Adapters.Git.LastCommandText;
		}

		public async Task RefreshData()
		{
			await this.GetStatus();
					
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
        string _junctionRelativePathTarget;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		public DateTime? Refreshed { get; private set; }

		void BtnBashRcClick(object sender, EventArgs e)
		{
			var bashrcPath = Domain.Adapters.Git.GetBashRcPath();
			if (System.IO.File.Exists(bashrcPath) == false) System.IO.File.Create(bashrcPath);
			System.Diagnostics.Process.Start(bashrcPath);
		}

        async void btnRefLog_Click(object sender, EventArgs e)
        {
            var git = new Git(Properties.Settings.Default.GitPath);
            var raw = await RunGitStream(git.GetRefLog);
        

            var output = raw.Output;
            dgJunction.DataSource = git.ParseRefLog(output).ToArray();
            
        }

	}
}
