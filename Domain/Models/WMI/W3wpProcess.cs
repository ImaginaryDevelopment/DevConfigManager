using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.WMI
{
	public class W3wpProcess
	{
		public uint ProcessId { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public uint ThreadCount { get; set; }

		public string Name { get; set; }

		public string Config { get; set; }
		
		public DateTimeOffset CreationDate { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong OtherOperationCount { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong OtherTransferCount { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public uint PageFaults { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public uint PageFileUsage { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public uint PeakPageFileUsage { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong PeakVirtualSize { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public uint PeakWorkingSetSize { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong VirtualSize { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong PrivatePageCount { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong WorkingSetSize { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong WriteOperationCount { get; set; }
		[DisplayFormat(DataFormatString = "n0")]
		public ulong WriteTransferCount { get; set; }
	}
}
