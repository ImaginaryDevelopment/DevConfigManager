namespace Domain.Extensions
{
	using System.Xml.Linq;

	public static class XExtensions
	{
		public static string GetAttribValOrNull(this XElement element, string name, XNamespace xNamespace = null)
		{
			var ns = xNamespace ?? XNamespace.None;
			var attribute = element.Attribute(ns + name);
			if (attribute == null)
			{
				return null;
			}
			return attribute.Value;
		}
	}
}
