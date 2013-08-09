using System;

namespace Domain.Extensions
{
	using System.Collections.Generic;
	using System.Linq;

	public static class StringExtensions
	{

		public static string Delimit(this IEnumerable<string> values, string delimiter)
		{
			return values.Aggregate((s1, s2) => s1 + delimiter + s2);
		}

		public static string EnsureStartsWith(this string src, string mayStartWith)
		{
			if (src == null)
				throw new ArgumentNullException("src");
			if (src.StartsWith(mayStartWith)) return src;
			return mayStartWith + src;
		}

		public static IEnumerable<string> Surround(this IEnumerable<string> values, string left, string right )
		{
			return values.Select(v => left + v + right);
		}

		public static string BeforeOrSelf(this string text, string delimiter)
		{
			if (text.Contains(delimiter) == false)
				return text;
			return text.Before(delimiter);
		}

		public static string After(this string text, string delimiter, StringComparison stringComparison)
		{
			return text.Substring(text.IndexOf(delimiter, stringComparison) + delimiter.Length);
		}

		public static string AfterLast(this string text, string delimiter)
		{
			return text.Substring(text.LastIndexOf(delimiter, System.StringComparison.Ordinal) + delimiter.Length);
		}

		public static string AfterLastOrSelf(this string text, string delimiter)
		{
			if (text.Contains(delimiter) == false)
				return text;
			return text.AfterLast(delimiter);
		}

		public static string Before(this string text, string delimiter)
		{
			return text.Substring(0, text.IndexOf(delimiter, System.StringComparison.Ordinal));
		}

		public static string Before(this string text, string delimiter, StringComparison stringComparison)
		{
			return text.Substring(0, text.IndexOf(delimiter, stringComparison));
		}

		public static string AfterOrSelf(this string text, string delimiter)
		{
			if (text.Contains(delimiter) == false)
				return text;
			return text.After(delimiter);
		}

		public static bool IsNullOrWhitespace(this string s)
		{
			return string.IsNullOrWhiteSpace(s);
		}

		public static bool IsNullOrEmpty(this string s)
		{
			return string.IsNullOrEmpty(s);
		}

		public static bool HasValue(this string s)
		{
			return !s.IsNullOrEmpty();
		}

		public static string After(this string text, string delimiter)
		{
			return text.Substring(text.IndexOf(delimiter) + delimiter.Length);
		}

		public static int StrComp(this string str1, string str2, bool ignoreCase)
		{
			return string.Compare(str1, str2, ignoreCase);
		}

		public static bool IsIgnoreCaseMatch(this string s, string comparisonText)
		{
			return s.StrComp(comparisonText, true) == 0;
		}

		public static string[] SplitLines(this string text)
		{
			return text.Split(new[] { "\r\n", "\n" }, StringSplitOptions.None);
		}
	}
}
