using System;
using System.Text.RegularExpressions;

namespace DeveloperConfigurationManager
{
    static class StringExtensions
	{
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
			return text.Substring(text.LastIndexOf(delimiter) + delimiter.Length);
		}

		public static string AfterLastOrSelf(this string text, string delimiter)
		{
			if (text.Contains(delimiter) == false)
				return text;
			return text.AfterLast(delimiter);
		}

		public static string BeforeLast(this string text, string delimiter)
		{
			return text.Substring(0, text.LastIndexOf(delimiter));
		}

		public static string Before(this string text, string delimiter)
		{
			return text.Substring(0, text.IndexOf(delimiter));
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

		// Write custom extension methods here. They will be available to all queries.
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

		public static int StrComp(this String str1, String str2, bool ignoreCase)
		{
			return string.Compare(str1, str2, ignoreCase);
		}

		public static bool IsRegexMatch(this string s, string expression)
		{
			return Regex.IsMatch(s, expression);
		}

		public static bool IsRegexMatch(this string s, string expression,RegexOptions options)
		{
			return Regex.IsMatch(s, expression,options);
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
