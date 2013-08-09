using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Domain;

namespace DeveloperConfigurationManager.Controls
{
    class ControlStreamHelper
	{
		internal static async Task<StreamOuts> RunStreaming(Func<CancellationToken, StreamingOuts> func, IEnumerable<Control> disables, RichTextBox listener, Control cancel, CancellationTokenSource cts = null)
		{
			var controls = disables as Control[] ?? disables.ToArray();

			foreach (var item in controls)
				item.InvokeSafeEnabled(false);
			StreamOuts result;

			using (cts = cts ?? new CancellationTokenSource())
				try
				{


					// ReSharper disable AccessToDisposedClosure
					using (var cancelObs = cancel.ToObservableClick().Subscribe(_ => cts.Cancel(true))) // ReSharper restore AccessToDisposedClosure
					{
						cancel.InvokeSafeEnabled(true);

						if(listener != null)
							cts.Token.Register(() => listener.InvokeSafeAppend("Cancellation requested" + Environment.NewLine));

						var streams = func(cts.Token);
						var psStream = streams as PsStreamingOuts;
						using (var errObs = listener.AppendObservable(streams.Errors))
						using (var outObs = listener.AppendObservable(streams.Outputs))
						{
							await streams.AwaitAsync();
							
										if (streams.ErrorEventCount == 0 && streams.OutputEventCount == 0)
										{
											if (listener != null)
												listener.InvokeSafeAppend("No output or error output" + Environment.NewLine);
										}
							
										if (listener != null && psStream != null) listener.InvokeSafeAppend("Exit code:" + psStream.ExitCode.Result + Environment.NewLine);

										Debug.WriteLine("Awaited task resumed");	
						}

						result = new StreamOuts
							{
								Errors = streams.GetError(),
								Output = streams.GetOutput(),
								ExitCode =
									cts.IsCancellationRequested || psStream == null || psStream.ExitCode.IsCompleted == false
										? 0
										: psStream.ExitCode.Result
							};
					}
				}
				finally
				{
					cancel.InvokeSafeEnabled(false);
					foreach (var item in controls)
						item.InvokeSafeEnabled(true);
				}

			return result;
		}

	}
}
