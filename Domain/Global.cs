using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
	public static class Global
	{
		static object _dumper;

		public static void SetDump<T>(Func<T, T> newDumper)
		{
			_dumper = newDumper;
		}

		public static T Dump<T>(this T src, string message = null)
		{
			var dumper = _dumper as Func<T, T>;

			return dumper != null ? dumper(src) : src;
		}

		static Func<string, bool> _fileExists;

		public static Func<string, bool> FileExists
		{
			get
			{
				return _fileExists ?? System.IO.File.Exists;
			}
			set
			{
				_fileExists = value;
			}
		}
	}
}
