using System.Diagnostics;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Domain.Extensions;
using Domain.Models;

namespace Domain.Adapters
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Text;
	using System.Threading;

	public static class Process
	{
		public static void Prompt(string args, string workingDirectory = null)
		{
			using (var ps = new System.Diagnostics.Process())
			{
				ps.StartInfo.FileName = "cmd.exe";
				ps.StartInfo.Arguments = "/c " + args;
				if (workingDirectory.IsNullOrEmpty() == false)
					ps.StartInfo.WorkingDirectory = workingDirectory;
				ps.Start();

			}
		}

		public static StreamingOuts Cmd(string args, CancellationToken ct, string workingDirectory = null)
		{
			Debug.Assert(args.Contains("/c") == false);

			return RunRedirectedObservable("cmd.exe", "/c " + args, ct, workingDirectory);
		}

		public static StreamingOuts RunRedirected(IRunnable options, CancellationToken ct)
		{
			return RunRedirectedObservable(options.Filename, options.Args, ct, options.WorkingDirectory);
		}

		public static StreamingOuts Kill(string processName, CancellationToken ct)
		{
			return Cmd("taskkill /IM " + processName + " /F", ct);
		}

		public static StreamingOuts RunRedirectedObservable(IRunnable options, CancellationToken ct)
		{
			return RunRedirectedObservable(options.Filename, options.Args, ct, options.WorkingDirectory);
		}

		public static PsStreamingOuts RunRedirectedObservable(string fileName, string args, CancellationToken ct, string input, string workingDirectory)
		{
			args.Dump("observable args");

			var startInfo = new ProcessStartInfo(fileName, args) { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, RedirectStandardInput = true };

			{
				if (workingDirectory.HasValue())
				{
					startInfo.WorkingDirectory = workingDirectory;
				}

				var ps = System.Diagnostics.Process.Start(startInfo);
				ps.EnableRaisingEvents = true;
				ct.Register(
					() =>
					{
						try
						{
							if (ps.HasExited) return;
						}
						catch (InvalidOperationException)
						{
							return;
						}

						Debug.WriteLine("killing ps " + ps.Id);
						ps.Kill();
						Debug.WriteLine("killed ps " + ps.Id);
					});
				ct.Register(ps.Dispose);

				var stdOut =
					ps.CreateConsoleObservable((proc, h) => proc.OutputDataReceived += h, (proc, h) => proc.OutputDataReceived -= h, fileName, args);

				var stdErr = ps.CreateConsoleObservable(
					(proc, h) => proc.ErrorDataReceived += h, (proc, h) => proc.ErrorDataReceived -= h, fileName, args);

				ps.StandardInput.Write(input);
				
				ps.BeginOutputReadLine();
				ps.BeginErrorReadLine();

				var result = new PsStreamingOuts(stdErr, stdOut, ps, ct);

				return result;
			}
		}

		public static PsStreamingOuts RunRedirectedObservable(
			string fileName, string args, CancellationToken ct, string workingDirectory = null)
		{
			args.Dump("observable args");
			
			var startInfo = new ProcessStartInfo(fileName, args) { CreateNoWindow = true, UseShellExecute = false, RedirectStandardOutput = true, RedirectStandardError = true, RedirectStandardInput = true };

			{
				if (workingDirectory.HasValue())
				{
					startInfo.WorkingDirectory = workingDirectory;
				}

				var ps = System.Diagnostics.Process.Start(startInfo);
				var processId = ps.Id;
				// var handle = ps.Handle;
				// var mainWindowHandle = ps.MainWindowHandle;

				ps.EnableRaisingEvents = true;
				ct.Register(
					() =>
					{
						var closure = ps;
						try
						{
							if (ps.HasExited == false)
							{
								var processAsserter = System.Diagnostics.Process.GetProcessById(processId);

								Debug.Assert(processAsserter.Id == ps.Id);
								if (closure.HasExited) return;
								Debug.WriteLine("killing ps " + ps.Id);
							}
						
						}
						catch (InvalidOperationException)
						{
							
						}

						try
						{
							closure.Kill();
							Debug.WriteLine("killed ps " + closure.Id);
						}
						catch (InvalidOperationException)
						{
							closure.Close();
						}
						
					});
				var stdErr = ps.CreateConsoleObservable(
					(proc, h) => proc.ErrorDataReceived += h, (proc, h) => proc.ErrorDataReceived -= h);

				var stdOut = ps.CreateConsoleObservable(
					(proc, h) => proc.OutputDataReceived += h, (proc, h) => proc.OutputDataReceived -= h, fileName, args);

				ps.BeginOutputReadLine();
				ps.BeginErrorReadLine();

				var result = new PsStreamingOuts(stdErr, stdOut, ps, ct);
			
				return result;
			}
		}
	}
}
