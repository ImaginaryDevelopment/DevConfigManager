using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using Domain;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcMsBuild : UserControl, INamed
	{
		CancellationTokenSource _cts;

		readonly IObservable<string> _junctionTargetNotifier;

		public UcMsBuild(string junctionTarget, IObservable<string> junctionTargetNotifier)
		{
			InitializeComponent();
			lblJunctionPath.Text = junctionTarget;
			this._junctionTargetNotifier = junctionTargetNotifier;
			this._junctionTargetNotifier.Subscribe(s =>
				{
					lblJunctionPath.Text = s;
				});
		}

		string GetTarget()
		{
			return (ckUseJunctionForMsBuild.Checked ? lblJunctionPath.Text : string.Empty) + txtMsTarget.Text;
		}

		public async Task RunMsBuild(CancellationToken ct)
		{
			var target = GetTarget();
			if (System.IO.File.Exists(target) == false)
				rtMsBuild.AppendText("Failed to start msbuild, target not found");
			btnMsBuild.Enabled = false;
			_msBuildCancellation = ct;
			try
			{
				var result = this.MsBuild(_msBuildCancellation);

				using (rtMsBuild.AppendObservable(result.Errors))
				using (rtMsBuild.AppendObservable(result.Outputs))
				{
					await result.AwaitAsync();
					
					Debug.WriteLine("ms build finished");
				}
			}
			catch (OperationCanceledException)
			{
				Debug.WriteLine("ms build cancelled");
			}
		finally
			{
				btnMsBuild.Enabled = true;
				_msBuildCancellation = CancellationToken.None;
			}

		}

		StreamingOuts MsBuild(CancellationToken ct)
		{
			var target = this.GetTarget();
			var m = nudMultiProcessor.Text.IsNullOrEmpty() ? string.Empty : ("/m:" + nudMultiProcessor.Value + " ");

			//HACK: need to use cancellation token instead
			var runnable = new Domain.Adapters.MsBuild(
				m + "\"" + target + "\" " + txtMsBuildArguments.Text, TimeSpan.FromMinutes(2));
			var result = Domain.Adapters.Process.RunRedirectedObservable(runnable, ct);

			//var result = await Domain.Adapters.Process.RunRedirected(runnable);
			return result;
		}

		CancellationToken _msBuildCancellation;

		async void BtnMsBuildClick(object sender, EventArgs e)
		{
			_cts = new CancellationTokenSource();

			await this.RunMsBuild(_cts.Token);
		}

		void BtnCancelClick(object sender, EventArgs e)
		{
			if (_cts != null)
			{
				_cts.Cancel(true);
				_cts.Dispose();
				_cts = null;
			}

		}
	}
}
