using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
	internal static class ManagementExtensions
	{
		public static string Get(this PropertyDataCollection pdc, string key)
		{
			if (pdc == null) throw new NullReferenceException("PropertyDataCollection");
			var wrapper = pdc[key];
			if (wrapper.Value == null) return null;
			return wrapper.Value.ToString();
		}
	}
}
