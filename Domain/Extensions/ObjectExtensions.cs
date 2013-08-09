using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
	public static class ObjectExtensions
	{
		public static T CastTo<T>(this object o)
		{
			return (T)o;
		}

		public static IComparer<T> CreateComparer<T>(this T type, Func<T, T, int> comparerFunc)
			where T : class 
		{
			return new Domain.Helpers.LambdaComparer<T>(comparerFunc);
		}
	}
}
