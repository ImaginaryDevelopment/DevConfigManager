using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Adapters
{
	using System.Threading;

	public class Junction
	{
		readonly string _path;

		public Junction(string path) {
			_path = path;
		}

		public StreamingOuts CleanObjAsync(CancellationToken ct) { return CleanAsync("OBJ", ct); }

		public StreamingOuts CleanBinAsync(CancellationToken timeout) { return CleanAsync("BIN", timeout); }

		public static string LastCommandText { get; set; }

		StreamingOuts CleanAsync(string name, CancellationToken ct)
		{
			return Process.Cmd(@"FOR /f %i in ('dir /D /S /B " + name + @"') DO del /F /S /Q ""%i\*.*""", ct, _path);
		}

		public IEnumerable<StreamingOuts> CleanObjBinAsync(CancellationToken ct)
		{
			var binResult = CleanBinAsync(ct);
			var objResult = CleanObjAsync(ct);
			yield return objResult;
			yield return binResult;

		}
		public IEnumerable<StreamingOuts> CleanObjBin(CancellationToken ct)
		{
			var binResult = CleanBinAsync(ct);
			var objResult = CleanObjAsync(ct);
			Task.WaitAll(Task.Run(() => binResult.AwaitAsync(), ct), Task.Run(() => objResult.AwaitAsync(), ct));

			yield return objResult;
			yield return binResult;
			
		}

		public bool Exists() { return System.IO.Directory.Exists(_path); }

		public async Task<StreamOuts> SetTarget(string nextTarget, CancellationToken ct)
		{
			if (System.IO.Directory.Exists(_path))
				Process.Cmd("rmdir " + _path, ct).Dump("removing junction");
			string parent = System.IO.Path.GetDirectoryName(_path);
			if (_path == System.IO.Path.Combine(parent, nextTarget))
				throw new InvalidOperationException("Junction can not be to itself");
			new { _path, parent, nextTarget }.Dump("linking");
			Debug.Assert(nextTarget != ".");
			Debug.Assert(nextTarget != "..");
			LastCommandText = parent + ">" + "mklink /j " + _path + " " + nextTarget;
			var cmdResult = Process.Cmd("mklink /j " + _path + " " + nextTarget, ct, parent);
			return await cmdResult.ToStreamOutsAsync();
		}

		public async Task<string> GetTarget(CancellationToken ct)
		{
			if (Exists() == false)
				return null;
			var parent = System.IO.Path.GetDirectoryName(_path);
			var info = (await new DirectoryCmd(parent).GetDirInfo(ct)).Directories;
			var target = info.First(d => d.Target != null && _path.EndsWith(d.Name, StringComparison.CurrentCultureIgnoreCase));
			if (target == null)
				return string.Empty;
			return target.Target;
		}
	}
}
