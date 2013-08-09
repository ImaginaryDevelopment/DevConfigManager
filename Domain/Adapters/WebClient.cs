using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Adapters
{
	using System.Drawing;
	using System.IO;
	using System.Net;

	using Domain.Models;
	using Domain.Extensions;

	public class BasicAuthWebClient : ICanDownload
	{


		public BasicAuthWebClient(string username, string password)
		{
			
			Authorization = "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(username + ":" + password));
		}
		void UseAuth(WebClient wc)
		{
			if (Authorization.IsNullOrEmpty() == false)
				wc.Headers.Add("Authorization", Authorization);
		}
		public string Authorization { get; private set; }

		public Task<string> Download(Uri destination, string accept = null)
		{
			using (var wc = new System.Net.WebClient())
			{
				UseAuth(wc);
				if (accept.IsNullOrEmpty() == false)
					wc.Headers.Add("Accept", accept);
				return wc.DownloadStringTaskAsync(destination);
			}
		}

		public async Task<Icon> DownloadIcon(Uri destination,bool useAuth)
		{
			var bytes = await Download(destination,useAuth);
			using (var ms = new MemoryStream(bytes)) return new Icon(ms);
		}

		public Task<byte[]> Download(Uri destination,bool useAuth)
		{
			using (var wc = new System.Net.WebClient())
			{
				if (useAuth) UseAuth(wc);
				var result = wc.DownloadDataTaskAsync(destination);
				return result;
			}
		}
	}
}
