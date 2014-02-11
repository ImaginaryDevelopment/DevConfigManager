using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Extensions;
namespace Domain.Adapters
{
	public class NetworkShare
	{
		readonly string _server;

		public NetworkShare(string server)
		{
			_server = server;
		}

		public string TranslateToNetworkPath(string absolutePath)
		{
            if (absolutePath.StartsWith("\\\\")) //would check for having _server but perhaps this is a case of iis bound to multiple host names?
            {
                return absolutePath;
            }
            
			var drive = absolutePath.Before(":\\");
			var relativePath = absolutePath.After(":\\");
			return string.Format(@"\\{0}\{1}$\{2}", _server, drive, relativePath);
		}

		public bool DirectoryExists(string absolutePath)
		{
			var networkPath = this.TranslateToNetworkPath(absolutePath);
			return System.IO.Directory.Exists(networkPath);
		}
	}
}
