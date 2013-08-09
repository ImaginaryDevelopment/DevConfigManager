using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
	public class Disposable:IDisposable
	{
		readonly Action _onDispose;
		bool _disposed = false;
		public Disposable(Action onDispose)
		{
			_onDispose = onDispose;
		}


		public void Dispose()
		{
			lock (this)
			{
				if (!_disposed)
				{
					try
					{
						_onDispose();
					}
					finally
					{
						_disposed = true;
					}


				}
			}
			
			
		}
	}
}
