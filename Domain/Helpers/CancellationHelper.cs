using System;
using System.Threading.Tasks;

namespace Domain.Helpers
{
	using System.Threading;

	public class CancellationHelper
	{
		readonly Action _beforeInvoke;

		readonly Action _afterInvoke;

		readonly Action _invokeFinally;

		CancellationTokenSource _cts;

		Task _currentTask;

		public void RequestCancellation()
		{
			if (_cts != null)
				_cts.Cancel(true);
		}


		public CancellationHelper(Action beforeInvoke = null, Action afterInvoke = null, Action invokeFinally = null)
		{
			_beforeInvoke = beforeInvoke;
			_afterInvoke = afterInvoke;
			_invokeFinally = invokeFinally;
		}

		public Task ContinueWith(Func<CancellationToken, Task> func)
		{
			var currentTask = _currentTask;
			if (currentTask != null && _cts == null)
				throw new InvalidOperationException("Inconsistent state");
			if (currentTask != null) currentTask.Wait();
			return this.RunAsync(func);
		}

		public async Task RunAsync(params Func<CancellationToken, Task>[] funcs)
		{
			if (_cts != null)
			{
				if (_currentTask == null || _cts.Token.WaitHandle == null)
					throw new InvalidOperationException("_cts appears disposed");
				return;
			}
			using (_cts = new CancellationTokenSource())
			{
				try
				{

					if (_beforeInvoke != null) _beforeInvoke();
					if (_cts.IsCancellationRequested) return;
					try
					{
						foreach (var f in funcs)
						{
							_currentTask = f(_cts.Token);
							await _currentTask;
						}
						if (_afterInvoke != null) _afterInvoke();
					}
					finally
					{
						this.Finally(true);
					}

				}
				finally
				{
					this.Finally(false);
				}

			}
		}

		void Finally(bool invokeUserFinally)
		{
			if (_invokeFinally != null && invokeUserFinally) _invokeFinally();
			_currentTask = null;
			_cts = null;
		}

		public Task Run(params Func<CancellationToken, Task>[] funcs)
		{
			if (_cts != null)
			{
				throw new InvalidOperationException("CancellationHelper already in use");
			}

			using (_cts = new CancellationTokenSource())
			{
				try
				{

					if (_beforeInvoke != null) _beforeInvoke();
					try
					{
						foreach (var f in funcs)
						{
							var result = f(_cts.Token);

							_currentTask = result;

							if (result != null) result.Wait(_cts.Token);
						}
						if (_afterInvoke != null) _afterInvoke();
						return _currentTask;
					}
					finally
					{

						this.Finally(true);

					}

				}
				finally
				{
					this.Finally(false);
				}
			}
		}

		public T Run<T>(Func<CancellationToken, T> func)
		{
			if (_cts != null)
			{
				throw new InvalidOperationException("CancellationHelper already in use");
			}

			using (_cts = new CancellationTokenSource())
			{
				try
				{
					if (_beforeInvoke != null) _beforeInvoke();
					try
					{
						var result = func(_cts.Token);

						var task = result as Task;
						_currentTask = task;
						if (task != null) task.Wait(_cts.Token);

						if (_afterInvoke != null) _afterInvoke();
						return result;
					}
					finally
					{

						this.Finally(true);

					}
				}
				finally
				{
					this.Finally(false);
				}
			}
		}


		public bool IsBusy
		{
			get
			{
				return _cts != null || _currentTask != null;
			}
		}
	}
}
