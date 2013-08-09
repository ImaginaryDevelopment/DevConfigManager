using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Domain.Helpers;

namespace DeveloperConfigurationManager.CrossCutting
{
    public class Profiler
	{
		readonly string _name;

		public string Name
		{
			get
			{
				return _name;
			}
		}

		public DateTime Started { get; private set; }
		public DateTime Stopped { get; private set; }
		public TimeSpan? Elapsed { get { return Stopped > Started ? Stopped - Started :(TimeSpan?) null; } }
		public IReadOnlyList<Profiler> Children
		{
			get
			{
				return (IReadOnlyList<Profiler>)_children;
			}
		}

		IList<Profiler> _children = new List<Profiler>();

		public Profiler( [CallerMemberName] string name=null)
		{
			_name = name;
			Started = DateTime.UtcNow;
		}

		public IDisposable Step([CallerMemberName] string caller = null)
		{
			var child = new Profiler(caller);
			_children.Add(child);
			return new Disposable(child.Stop);
		}
		public void Stop()
		{
			if (Stopped == default(DateTime))
			{
				Stopped = DateTime.UtcNow;
			}
		}
		internal string Show(string indentor)
		{
			return Newtonsoft.Json.JsonConvert.SerializeObject(this);

		}

	
	}
}
