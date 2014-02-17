namespace Domain.Adapters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using System.Xml.XPath;

    using Domain.Extensions;
    using Domain.Models;

    public static class IIS
    {
        public static StreamingOuts ResetStop(CancellationToken ct, string computerName = null)
        {
            return Reset("/stop", ct, computerName);
        }

        public static StreamingOuts StartSite(string siteName, CancellationToken ct)
        {
            return AppCmd("start site \"" + siteName + "\"", ct);
        }

        public static StreamingOuts ResetStart(CancellationToken ct, string computerName = null)
        {
            return Reset("/start", ct, computerName);
        }

        public static StreamingOuts ResetStatus(CancellationToken ct, string computerName = null)
        {
            return Reset("/status", ct, computerName);
        }

        public const string ProcessName = "w3wp.exe";

        public static StreamingOuts ListApps(CancellationToken ct)
        {
            return AppCmd("list apps /config /xml", ct);
        }

        public static StreamingOuts SetVDirPath(string appName, string vdir, string path, CancellationToken ct)
        {
            return AppCmd("set vdir \"" + appName + vdir + "\" -physicalPath:" + (path.Contains(" ") ? "\"" + path + "\"" : path), ct);
        }

        public static IEnumerable<IisAppInfo> ListAppsFormatted(StreamOuts listAppsOutputs)
        {
            var config = XDocument.Parse(listAppsOutputs.Output);
            var q = from siteApp in config.XPathSelectElements("appcmd/APP")
                    let appName = siteApp.Attribute(XNamespace.None + "APP.NAME").Value
                    from app in siteApp.XPathSelectElements("application")
                    let appPath = app.Attribute(XNamespace.None + "path").Value
                    let pool = app.GetAttribValOrNull("applicationPool")
                    let vd = app.XPathSelectElements("virtualDirectory[@path]")
                    let virtuals = vd.Select(v => new { VirDir = v.Attribute(XNamespace.None + "path").Value, PhysicalPath = v.Attribute(XNamespace.None + "physicalPath").Value })
                    let xvirtuals = virtuals.Select(v => new IisVirtualInfo(v.VirDir, v.PhysicalPath, v.PhysicalPath, null, Directory.Exists(v.PhysicalPath)))
                    select new IisAppInfo(appName, appPath, pool, xvirtuals);
            var allEntries = q.ToList();
            return allEntries;
        }

        private static readonly string[] IisServiceNames = { "WMSVC", "WAS", "W3SVC", "MsDepSvc", "WinRm", "msvsmon120" };
        public static IEnumerable<string> FindMissingExpectedIisServices(IEnumerable<string> serviceNames)
        {
            return IisServiceNames.Where(i => serviceNames.Any(sn => sn.IsIgnoreCaseMatch(i)) == false);

        }
        public static bool IsServiceIisRelated(string serviceName)
        {
            return IisServiceNames.Any(sn => sn.IsIgnoreCaseMatch(serviceName));
        }
        public static async Task<IEnumerable<IisAppInfo>> ListAppsFormatted(CancellationToken ct)
        {
            var cmdResult = ListApps(ct);
            var so = await cmdResult.ToStreamOutsAsync();

            return ListAppsFormatted(so);
        }

        public static PsStreamingOuts AppCmd(string arg, CancellationToken ct)
        {
            var winDir = Environment.ExpandEnvironmentVariables("%windir%");

            return Process.RunRedirectedObservable(Path.Combine(winDir, @"System32\inetsrv\appcmd.exe"), arg, ct);

        }

        static StreamingOuts Reset(string arg, CancellationToken ct, string computerName = null)
        {
            var args = computerName != null ? computerName + " " + arg : arg;

            var result = Process.RunRedirectedObservable("iisreset.exe", args, ct);

            return result;
        }

        public static StreamingOuts Clean(CancellationToken ct, string computerName = null)
        {
            var cmdResult = ResetStop(ct);

            var outputs = cmdResult.Outputs;
            var errors = cmdResult.Errors;
            //dos version: 
            //del /F /S /Q "C:\Windows\Microsoft.NET\Framework\v4.0.30319\Temporary ASP.NET Files\*.*"
            //del /F /S /Q "C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Temporary ASP.NET Files\*.*"
            //del /F /S /Q "\\%1\c$\Windows\Microsoft.NET\Framework\v2.0.50727\Temporary ASP.NET Files\*.*"
            var basePath = computerName != null && computerName != "127.0.0.1" && computerName != "localhost"
                           && computerName != Environment.MachineName
                               ? @"\\" + computerName + @"\c$\windows"
                               : System.Environment.ExpandEnvironmentVariables("%windir%");

            var paths = new[]
				{
				  @"Framework\v2.0.50727",
				  @"Framework\v4.0.30319",
				  @"Framework64\v2.0.50727",
				  @"Framework64\v4.0.30319" 
				};
            var task = Task.Run(async () =>
                    {
                        await cmdResult.AwaitAsync(); //make sure iis stop is done before starting the deletes
                        foreach (
                            var p in paths.Select(p => System.IO.Path.Combine(basePath, "Microsoft.NET", p, "Temporary ASP.NET Files")))
                        {
                            if (ct.IsCancellationRequested) return;
                            var expandedPath = Environment.ExpandEnvironmentVariables(p);
                            if (Directory.Exists(expandedPath) == false)
                            {
                                outputs.Add(expandedPath + " not found, skipped.");
                                continue;
                            }

                            foreach (var file in Directory.GetFiles(expandedPath))
                            {
                                if (ct.IsCancellationRequested) return;
                                File.Delete(file);
                            }
                            foreach (var dir in Directory.GetDirectories(expandedPath))
                            {
                                if (ct.IsCancellationRequested) return;
                                try
                                {
                                    Directory.Delete(dir, true);
                                    outputs.Add("deleted " + dir);
                                }
                                catch (IOException iEx)
                                {
                                    errors.Add("failed to delete:" + dir + ":" + iEx);
                                }
                                catch (UnauthorizedAccessException uEx)
                                {
                                    errors.Add("access denied to delete " + dir + ":" + uEx);
                                }
                            }
                        }
                    }, ct);
            var result = new StreamingOuts(errors, outputs, task, ct);
            return result;
        }

        public static bool IsStopped()
        {
            var processes = System.Diagnostics.Process.GetProcessesByName(ProcessName);
            processes.Dump("any w3");
            return processes.Any();
        }

        public static StreamingOuts Kill(CancellationToken ct)
        {
            return Domain.Adapters.Process.Kill(ProcessName, ct);
        }

        public static StreamingOuts Restart(CancellationToken ct, string computerName = null)
        {
            return Reset("/restart", ct, computerName);
        }
    }
}
