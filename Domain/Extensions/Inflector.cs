using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
	using System.Text.RegularExpressions;
	/// <summary>
	/// Inflector NuGet package is not strong signed =(
	/// </summary>
	public static class Inflector
{
    readonly static List<Rule> _plurals;

    readonly static List<Rule> _singulars;

    readonly static List<string> _uncountables;

    static Inflector()
    {
        _plurals = new List<Rule>();
        _singulars = new List<Rule>();
        _uncountables = new List<string>();
        AddPlural("$", "s");
        AddPlural("s$", "s");
        AddPlural("(ax|test)is$", "$1es");
        AddPlural("(octop|vir|alumn|fung)us$", "$1i");
        AddPlural("(alias|status)$", "$1es");
        AddPlural("(bu)s$", "$1ses");
        AddPlural("(buffal|tomat|volcan)o$", "$1oes");
        AddPlural("([ti])um$", "$1a");
        AddPlural("sis$", "ses");
        AddPlural("(?:([^f])fe|([lr])f)$", "$1$2ves");
        AddPlural("(hive)$", "$1s");
        AddPlural("([^aeiouy]|qu)y$", "$1ies");
        AddPlural("(x|ch|ss|sh)$", "$1es");
        AddPlural("(matr|vert|ind)ix|ex$", "$1ices");
        AddPlural("([m|l])ouse$", "$1ice");
        AddPlural("^(ox)$", "$1en");
        AddPlural("(quiz)$", "$1zes");
        AddSingular("s$", string.Empty);
        AddSingular("(n)ews$", "$1ews");
        AddSingular("([ti])a$", "$1um");
        AddSingular("((a)naly|(b)a|(d)iagno|(p)arenthe|(p)rogno|(s)ynop|(t)he)ses$", "$1$2sis");
        AddSingular("(^analy)ses$", "$1sis");
        AddSingular("([^f])ves$", "$1fe");
        AddSingular("(hive)s$", "$1");
        AddSingular("(tive)s$", "$1");
        AddSingular("([lr])ves$", "$1f");
        AddSingular("([^aeiouy]|qu)ies$", "$1y");
        AddSingular("(s)eries$", "$1eries");
        AddSingular("(m)ovies$", "$1ovie");
        AddSingular("(x|ch|ss|sh)es$", "$1");
        AddSingular("([m|l])ice$", "$1ouse");
        AddSingular("(bus)es$", "$1");
        AddSingular("(o)es$", "$1");
        AddSingular("(shoe)s$", "$1");
        AddSingular("(cris|ax|test)es$", "$1is");
        AddSingular("(octop|vir|alumn|fung)i$", "$1us");
        AddSingular("(alias|status)es$", "$1");
        AddSingular("^(ox)en", "$1");
        AddSingular("(vert|ind)ices$", "$1ex");
        AddSingular("(matr)ices$", "$1ix");
        AddSingular("(quiz)zes$", "$1");
        AddIrregular("person", "people");
        AddIrregular("man", "men");
        AddIrregular("child", "children");
        AddIrregular("sex", "sexes");
        AddIrregular("move", "moves");
        AddIrregular("goose", "geese");
        AddIrregular("alumna", "alumnae");
        AddUncountable("equipment");
        AddUncountable("information");
        AddUncountable("rice");
        AddUncountable("money");
        AddUncountable("species");
        AddUncountable("series");
        AddUncountable("fish");
        AddUncountable("sheep");
        AddUncountable("deer");
        AddUncountable("aircraft");
    }

    private static void AddIrregular(string singular, string plural)
    {
        var objArray = new object[5];
        objArray[0] = "(";
        objArray[1] = singular[0];
        objArray[2] = ")";
        objArray[3] = singular.Substring(1);
        objArray[4] = "$";
        AddPlural(string.Concat(objArray), string.Concat("$1", plural.Substring(1)));
        var objArray1 = new object[5];
        objArray1[0] = "(";
        objArray1[1] = plural[0];
        objArray1[2] = ")";
        objArray1[3] = plural.Substring(1);
        objArray1[4] = "$";
        AddSingular(string.Concat(objArray1), string.Concat("$1", singular.Substring(1)));
    }

    private static void AddPlural(string rule, string replacement)
    {
        _plurals.Add(new Rule(rule, replacement));
    }

    private static void AddSingular(string rule, string replacement)
    {
        _singulars.Add(new Rule(rule, replacement));
    }

    private static void AddUncountable(string word)
    {
        _uncountables.Add(word.ToLower());
    }

    private static string ApplyRules(List<Inflector.Rule> rules, string word)
    {
        string str = word;
        if (!_uncountables.Contains(word.ToLower()))
        {
            for (int i = rules.Count - 1; i >= 0; i--)
            {
                string str1 = rules[i].Apply(word);
                str = str1;
                if (str1 != null)
                {
                    break;
                }
            }
        }
        return str;
    }

    public static string Camelize(this string lowercaseAndUnderscoredWord)
    {
        return lowercaseAndUnderscoredWord.Pascalize().Uncapitalize();
    }

    public static string Capitalize(this string word)
    {
        return string.Concat(word.Substring(0, 1).ToUpper(), word.Substring(1).ToLower());
    }

    public static string Dasherize(this string underscoredWord)
    {
        return underscoredWord.Replace('\u005F', '-');
    }

    public static string Humanize(this string lowercaseAndUnderscoredWord)
    {
        return Regex.Replace(lowercaseAndUnderscoredWord, "_", " ").Capitalize();
    }

    private static string Ordanize(int number, string numberString)
    {
        int num = number % 100;
        if (num < 11 || num > 13)
        {
            int num1 = number % 10;
            switch (num1)
            {
                case 1:
                {
                    return string.Concat(numberString, "st");
                }
                case 2:
                {
                    return string.Concat(numberString, "nd");
                }
                case 3:
                {
                    return string.Concat(numberString, "rd");
                }
            }
            return string.Concat(numberString, "th");
        }
        else
        {
            return string.Concat(numberString, "th");
        }
    }

    public static string Ordinalize(this string numberString)
    {
        return Inflector.Ordanize(int.Parse(numberString), numberString);
    }

    public static string Ordinalize(this int number)
    {
        return Inflector.Ordanize(number, number.ToString());
    }

    public static string Pascalize(this string lowercaseAndUnderscoredWord)
    {
        string str = lowercaseAndUnderscoredWord;
        string str1 = "(?:^|_)(.)";
        return Regex.Replace(str, str1, (Match match) => match.Groups[1].Value.ToUpper());
    }

    public static string Pluralize(this string word)
    {
        return Inflector.ApplyRules(Inflector._plurals, word);
    }

    public static string Singularize(this string word)
    {
        return Inflector.ApplyRules(Inflector._singulars, word);
    }

    public static string Titleize(this string word)
    {
        string str = word.Underscore().Humanize();
        string str1 = "\\b([a-z])";
        return Regex.Replace(str, str1, (Match match) => match.Captures[0].Value.ToUpper());
    }

    public static string Uncapitalize(this string word)
    {
        return string.Concat(word.Substring(0, 1).ToLower(), word.Substring(1));
    }

    public static string Underscore(this string pascalCasedWord)
    {
        return Regex.Replace(Regex.Replace(Regex.Replace(pascalCasedWord, "([A-Z]+)([A-Z][a-z])", "$1_$2"), "([a-z\\d])([A-Z])", "$1_$2"), "[-\\s]", "_").ToLower();
    }

    private class Rule
    {
        private readonly Regex _regex;

        private readonly string _replacement;

        public Rule(string pattern, string replacement)
        {
            this._regex = new Regex(pattern, RegexOptions.IgnoreCase);
            this._replacement = replacement;
        }

        public string Apply(string word)
        {
            if (this._regex.IsMatch(word))
            {
                return this._regex.Replace(word, this._replacement);
            }
            else
            {
                return null;
            }
        }
    }
}

}
