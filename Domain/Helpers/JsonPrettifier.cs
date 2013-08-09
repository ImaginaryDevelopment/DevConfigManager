using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers
{
	/// <summary>
	/// http://stackoverflow.com/questions/4580397/json-formatter-in-c
	/// </summary>
	public static class JsonPrettifier
	{
		public static string PrettyPrint(string input,string indent="\t")
		{
			var indentDepth = 0;
			var quoted = false;
			var sb = new StringBuilder();
			for (var i = 0; i < input.Length; i++)
			{
				var ch = input[i];
				switch (ch)
				{
					case '{':
					case '[':
						sb.Append(ch);
						if (!quoted)
						{
							sb.AppendLine();
							Enumerable.Range(0, ++indentDepth).ToList().ForEach(item => sb.Append(indent));
						}
						break;
					case '}':
					case ']':
						if (!quoted)
						{
							sb.AppendLine();
							Enumerable.Range(0, --indentDepth).ToList().ForEach(item => sb.Append(indent));
						}
						sb.Append(ch);
						break;
					case '"':
						sb.Append(ch);
						bool escaped = false;
						var index = i;
						while (index > 0 && input[--index] == '\\')
							escaped = !escaped;
						if (!escaped)
							quoted = !quoted;
						break;
					case ',':
						sb.Append(ch);
						if (!quoted)
						{
							sb.AppendLine();
							Enumerable.Range(0, indentDepth).ToList().ForEach(item => sb.Append(indent));
						}
						break;
					case ':':
						sb.Append(ch);
						if (!quoted)
							sb.Append(" ");
						break;
					default:
						sb.Append(ch);
						break;
				}
			}
			return sb.ToString();
		}

		public static string Repeat(this string str, int count)
		{
			return new StringBuilder().Insert(0, str, count).ToString();
		}

		internal static bool IsEscaped(this string str, int index)
		{
			bool escaped = false;
			while (index > 0 && str[--index] == '\\') escaped = !escaped;
			return escaped;
		}

		internal static bool IsEscaped(this StringBuilder str, int index)
		{
			return str.ToString().IsEscaped(index);
		}
	}
}
