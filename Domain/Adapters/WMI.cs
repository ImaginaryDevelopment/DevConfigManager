using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using Domain.Extensions;

namespace Domain.Adapters
{
	using Domain.Models.WMI;

	public class WMI
	{
		readonly string _computerName;

		readonly ManagementScope _scope;


		public WMI(string computerName)
		{
			_computerName = computerName;
			_scope = new ManagementScope(@"\\" + _computerName + @"\root\cimv2");
		}

		public IEnumerable<PropertyDataCollection> Query(string query)
		{
			using (var searcher = new ManagementObjectSearcher(_scope, new SelectQuery(query)))
			using (var disposable = searcher.Get())
			{
				var items = disposable.Cast<ManagementObject>();
				var props = items.Select(i => i.Properties);
				return props;
			}
		}

		public IEnumerable<W3wpProcess> GetW3wpInfo()
		{
			var props = this.Query("select * FROM Win32_Process WHERE name='w3wp.exe'");
			var q = from p in props.ToArray()
			        let commandLine = p.Get("CommandLine")
			        let config = commandLine.After("-h").After("\"").Before("\"")
			        let name = commandLine.AfterLast("\\").Before(".")
			        let cDt = ManagementDateTimeConverter.ToDateTime(p.Get("CreationDate"))
			        select
				        new W3wpProcess()
					        {
						        Config = config,
						        CreationDate = cDt,
						        Name = name,
						        OtherOperationCount = ulong.Parse(p.Get("OtherOperationCount")),
								  PageFaults = uint.Parse(p.Get("PageFaults")),
								  PageFileUsage = uint.Parse(p.Get("PageFileUsage")),
								  PeakPageFileUsage = uint.Parse(p.Get("PeakPageFileUsage")),
								  OtherTransferCount = ulong.Parse(p.Get("OtherTransferCount")),
								  PeakVirtualSize = ulong.Parse(p.Get("PeakVirtualSize")),
								  PeakWorkingSetSize = uint.Parse(p.Get("PeakWorkingSetSize")), 
								  PrivatePageCount = ulong.Parse(p.Get("PrivatePageCount")),
								  ProcessId = uint.Parse(p.Get("ProcessId")), 
								  ThreadCount = uint.Parse(p.Get("ThreadCount")),
								  VirtualSize = ulong.Parse(p.Get("VirtualSize")),
								  WorkingSetSize = ulong.Parse(p.Get("WorkingSetSize")),
								  WriteOperationCount = ulong.Parse(p.Get("WriteOperationCount")),
								  WriteTransferCount = ulong.Parse(p.Get("WriteTransferCount"))
					        };

			return q.ToArray();
		}

		public ulong GetPhysicalMemory()
		{
			var props = this.Query("select TotalPhysicalMemory from win32_computersystem");
			var raw = props.First().Get("TotalPhysicalMemory");
			return ulong.Parse(raw);
		}

		public IDictionary<string,dynamic> QueryOperatingSystem()
		{
			var props = this.Query("select * from win32_operatingsystem");
			var osInfo = props.First();
			return
				osInfo.Cast<PropertyData>().ToDictionary(s => s.Name, s => new { s.Value, s.Type, s.IsArray, s.IsLocal })
				.CastTo<IDictionary<string, dynamic>>();
		}
	}
}
