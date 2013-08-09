using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	using System.Drawing;

	public interface ICanDownload
	{
		
		string Authorization { get; }

		Task<string> Download(Uri destination, string accept);

		Task<Icon> DownloadIcon(Uri destination,bool useAuthorization);
		Task<byte[]> Download(Uri destination, bool useAuthorization);
	}
}
