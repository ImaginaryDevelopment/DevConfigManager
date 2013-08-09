

namespace Domain.Extensions
{
	using System;
	using System.Collections.Generic;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Reactive.Linq;
	using System.Threading;
	using System.Threading.Tasks;

	static class ProcessExtensions
	{
		public static ObservableCollection<string> CreateConsoleObservable(this Process ps, Action<Process, DataReceivedEventHandler> addHandler, Action<Process, DataReceivedEventHandler> removeHandler,params string[] starters)
		{
			var list = new ObservableCollection<string>();
			
			var source =
				Observable.FromEventPattern<DataReceivedEventHandler, DataReceivedEventArgs>(
					h => addHandler(ps, h), h => removeHandler(ps, h)).Select(e => e.EventArgs.Data).StartWith(starters).TakeWhile(
						s => s != null).Subscribe(list.Add);
			return list;
		}

		public static async Task<StreamOuts> RunProcessRedirected(this Process ps, string arguments, TimeSpan timeout)
		{
			ps.StartInfo.Arguments = arguments;
			ps.StartInfo.RedirectStandardError = true;
			ps.StartInfo.RedirectStandardOutput = true;
			ps.StartInfo.UseShellExecute = false;
			string output = null;
			string errors = null;
			

			ps.Start();

			var outputTask = Task.Run(async () => output = await ps.StandardOutput.ReadToEndAsync());
			var tasks = new Task[]
				{
					outputTask,
					Task.Run(async () => errors = await ps.StandardError.ReadToEndAsync())
				};

			var outputTasks = Task.WhenAll(tasks);
			await Task.WhenAny(Task.Delay(timeout), outputTasks);
		
			if (ps.HasExited == false)
			{
				ps.Kill();
			}

#if LINQPAD
			//if (errors.Length > 0) Util.Highlight(errors).Dump("errors");
#endif
			
			return new StreamOuts() { Errors = errors, Output = output, ExitCode = ps.ExitCode };
		}
	}
}
