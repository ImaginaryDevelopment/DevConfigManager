using System.IO;

namespace Domain.Extensions
{
	static class StreamExtensions
	{
		public static string ReadtoEndAndDispose(this StreamReader reader)
		{

			using (StreamReader r = reader)
			{
				return r.ReadToEnd();
			}
		}
	}
}
