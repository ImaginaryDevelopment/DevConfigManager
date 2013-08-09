using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Domain.Extensions;
using Microsoft.Win32;

namespace Domain.Adapters
{
	public class RemoteRegistry
	{
		readonly string _server;

		public RemoteRegistry(string server)
		{
			this._server = server;
		}
		public IEnumerable<string> GetIisComponents()
		{
			var rKey =
				RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _server).OpenSubKey("SOFTWARE").OpenSubKey("Microsoft").
					OpenSubKey("InetStp").OpenSubKey("Components");
			return rKey.GetValueNames().Where(x => rKey.GetValue(x).ToString() == "1").OrderBy(x => x);
		}

	    public string ExpandEnvironmentVariables(string text)
	    {
	        var result = text;
	        var envVarReg = new Regex(@"%(\w+)%");
	        var envVars = envVarReg.Matches(text).Cast<Match>().Select(a => a.Groups[1].Value).Distinct();
	        string systemRoot = null;
	        foreach (var m in envVars)
	        {
	            if (m.IsIgnoreCaseMatch("SystemDrive"))
	            {
	                if (systemRoot == null)
	                    systemRoot = GetSystemRoot();

                    result = result.Replace("%" + m + "%", systemRoot.Before(@"\"));
	            } else if (m.IsIgnoreCaseMatch("SystemRoot"))
	            {
                    if (systemRoot == null)
                        systemRoot = GetSystemRoot();
	                result = result.Replace("%" + m + "%", systemRoot);
	            }
	        }
	        return result;
	    }

	    string GetSystemRoot()
	    {
            using (var mKey =
                        RegistryKey.OpenRemoteBaseKey(RegistryHive.LocalMachine, _server)
                            .OpenSubKey("SOFTWARE")
                            .OpenSubKey("Microsoft")
                            .OpenSubKey("Windows NT")
                            .OpenSubKey("CurrentVersion"))
            {
                return (string)mKey.GetValue("SystemRoot", null);
                
            }
	    }
		public string Server
		{
			get
			{
				return this._server;
			}
		}
	}
}
