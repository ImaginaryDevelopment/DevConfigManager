using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using Domain.Extensions;
using Domain.Models;

namespace Domain.Adapters
{
	using System;
	using System.Collections.ObjectModel;
	using System.Linq;
	using System.Reactive.Linq;
	using System.Reactive.Subjects;
	using System.Threading;
	using System.Threading.Tasks;

	public class Git
	{
	    public Git(string gitPath)
	    {
	        GitPath = gitPath;
	    }
		public  string GitPath { get; private set; }

		public static string LastCommandText { get; set; }

		public class StatusStreamOuts
		{
			public StreamOuts StreamOuts { get; set; }

			public string Branch { get; set; }

			public override string ToString()
			{
				return Newtonsoft.Json.JsonConvert.SerializeObject(this);
			}
		}

		public static void Bash(string initialpath = null)
		{
			LastCommandText = initialpath + ">" + @"""C:\Program Files (x86)\Git\bin\sh.exe"" --login -i""";
			//C:\Windows\SysWOW64\cmd.exe /c ""C:\Program Files (x86)\Git\bin\sh.exe" --login -i"
			Process.Prompt(@"""C:\Program Files (x86)\Git\bin\sh.exe"" --login -i", initialpath);
		}

		public  PsStreamingOuts CheckRemotes(string path, CancellationToken ct)
		{
			return Cmd("remote -v", ct, path);
		}

		 PsStreamingOuts Cmd(string args, CancellationToken ct, string workingPath = null, string input=null)
		{
			if (string.IsNullOrEmpty(GitPath)) return null;
			LastCommandText = workingPath + ">" + GitPath + " " + args;
		    try
		    {
                if (input != null)
                    return Process.RunRedirectedObservable(GitPath, args, ct, input, workingPath);
                return Process.RunRedirectedObservable(GitPath, args, ct, workingPath);
		    }
		    catch (Win32Exception wex)
		    {
		        throw new FileNotFoundException("Could not run process:"+GitPath,wex);
		    }
			
		}

	    public StreamingOuts GetRefLog(string path,CancellationToken ct)
	    {
	        return Cmd("reflog", ct, path);
	        
	    }

        public IEnumerable<Tuple<string,int,string>> ParseRefLog(string refLog)
        {
            const string regex = @"(\w+) \w+@\{(\d+)}: (.*)";
            foreach (var l in refLog.SplitLines().Select(a => new { a, Match = Regex.Match(a, regex) }).Where(a => a.Match.Success))
            {
                Debug.Assert(l.a.Contains("HEAD"));
                yield return
                    Tuple.Create(l.Match.Groups[1].Value, int.Parse(l.Match.Groups[2].Value),
                        l.Match.Groups[3].Value);
            }
        }

	    public static string GetBashRcPath()
		{
			var profilePath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
			var gitrc = System.IO.Path.Combine(profilePath, ".bashrc");
			return gitrc;
		}
		

		public  async Task<StatusStreamOuts> CheckStatus(string path,  CancellationToken ct)
		{
			var branchRegex = new Regex("On branch (\\S+)");

			var output = await Cmd("status", ct, path).ToStreamOutsAsync();
			
			if (string.IsNullOrEmpty(output.Output))
			{
				return default(StatusStreamOuts);
			}

			var branch = branchRegex.Match(output.Output).Groups[1].Value;
			var result = new StatusStreamOuts() { StreamOuts = output, Branch = branch };
			return result;
		}

		public  IEnumerable<FullInfoAsync> GetGitInfo(string parent, IEnumerable<DirInfo> source,string parentRelativeGitFolder,  CancellationToken ct)
		{
			var q = from item in source
                    let effectivePath = System.IO.Path.Combine(parent, item.Name, parentRelativeGitFolder ?? string.Empty)
			          let isGit = System.IO.Directory.Exists(System.IO.Path.Combine(effectivePath, ".git"))
			          select
						  new FullInfoAsync()
					        {
						        DirInfo = item,
						        GitInfo =
							        isGit
										  ? (new GitInfoAsync { IsGit = true, Remotes = CheckRemotes(effectivePath, ct).ToOutputAsync() })
										  : new GitInfoAsync { IsGit = false, Remotes = null }
					        };
			return q.ToArray();
		}

		public  StreamingOuts Push(string path, CancellationToken ct)
		{
			return Cmd("push", ct, path);
		}



		public  StreamingOuts Pull(string path, string branch, CancellationToken ct)
		{
			var streams = Cmd("pull origin " + branch, ct, path, "yes" + Environment.NewLine);
			return streams;
		}

		public  StreamingOuts GetUnpushed(string path, CancellationToken ct)
		{
			var streams = Cmd("log --branches --not --remotes --oneline --simplify-by-decoration --decorate", ct, path);
			return streams;
		}
	}
}
