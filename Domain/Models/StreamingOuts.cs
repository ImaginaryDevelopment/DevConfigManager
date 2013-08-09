namespace Domain
{
	using System;
	using System.Collections.ObjectModel;
	using System.Diagnostics;
	using System.Reactive.Linq;
	using System.Text;
	using System.Threading;
	using System.Threading.Tasks;

	using Domain.Extensions;

	public class PsStreamingOuts : StreamingOuts
	{
		public Process Ps { get; private set; }

		public Task<int> ExitCode { get; private set; }

		public PsStreamingOuts(
			ObservableCollection<string> errors, ObservableCollection<string> outputs, Process ps, CancellationToken ct)
			: base(errors,
				outputs,
				Task.Run(
					() =>
						{
							try
					{
						
							ps.WaitForExit();
					}
					catch (InvalidOperationException ioe)
					{
					}
						},
					ct),
				ct)
		{
			Ps = ps;

			ExitCode = Task.Run(
				async () =>
					{
						await this.AwaitAsync();
						if (ct.IsCancellationRequested == false)
							return ps.ExitCode;
						throw new OperationCanceledException(ct);
						
					},ct);
		}
	}

	public class PsStreamingOuts<T> : StreamingOuts<T>
	{
		public Task<int> ExitCode { get; private set; }
	
		public PsStreamingOuts(ObservableCollection<string> errors, ObservableCollection<string> outputs, Process ps, Task<T> task, CancellationToken ct)
			: base(errors, outputs, task, ct)
		{
			ExitCode = task.ContinueWith(_ => ps.ExitCode, ct);
		}
	}

	public class StreamingOuts<T> : StreamingOuts
	{
		public Task<T> Task { get; private set; }

		public StreamingOuts(ObservableCollection<string> errors, ObservableCollection<string> outputs, Task<T> task, CancellationToken ct)
			: base(errors, outputs, task, ct)
		{
			Task = task;
		}
	}

	public class StreamingOuts
	{
		readonly ObservableCollection<string> _errors;

		readonly StringBuilder _error = new StringBuilder();

		readonly ObservableCollection<string> _outputs;

		readonly Task _task;

		readonly StringBuilder _output = new StringBuilder();

		readonly CancellationToken _cancellationToken;

		protected CancellationToken CancellationToken
		{
			get
			{
				return _cancellationToken;
			}
		}

		public Task AwaitAsync()
		{
			 return this._task;
		}

		public StreamingOuts(ObservableCollection<string> errors, ObservableCollection<string> outputs, Task task, CancellationToken ct)
		{
			this._cancellationToken = ct;
			errors.AsObservableAccumulator().Subscribe(s =>
				{
					this.ErrorEventCount += 1;
					this._error.AppendLine(s);
				});
			this._errors = errors;

			this._outputs = outputs;

			this._outputs.AsObservableAccumulator().Subscribe(s =>
				{
					this.OutputEventCount += 1;
					this._output.AppendLine(s);
				});
			_task = task;
		}

		public async Task<string> ToOutputAsync()
		{
			if (this.AwaitAsync().IsCompleted == false)
				await this.AwaitAsync();
			if (CancellationToken.IsCancellationRequested == false) return this.GetOutput();
			throw new OperationCanceledException(CancellationToken);
		}

		public async Task<StreamOuts> ToStreamOutsAsync()
		{
			await this.AwaitAsync();
			if (CancellationToken.IsCancellationRequested == false)
				return new StreamOuts { Errors = this.GetError(), Output = this.GetOutput() };
			throw new OperationCanceledException(CancellationToken);
		}

		public StreamOuts ToStreamOuts()
		{
			this.AwaitAsync();
			if (CancellationToken.IsCancellationRequested == false)
				return new StreamOuts { Errors = this.GetError(), Output = this.GetOutput() };
			throw new OperationCanceledException(CancellationToken);
		}

		public int OutputEventCount { get; private set; }

		public int ErrorEventCount { get; private set; }

		public string GetOutput()
		{
			return this._output.ToString();
		}

		public string GetError()
		{
			return this._error.ToString();
		}

		public ObservableCollection<string> Errors
		{
			get
			{
				return this._errors;
			}
		}

		public ObservableCollection<string> Outputs
		{
			get
			{
				return this._outputs;
			}

		}
	}

}