using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text.RegularExpressions;
using Domain.Extensions;

namespace DeveloperConfigurationManager.Properties
{
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {

        public IDictionary<string, Uri> GetLinks()
        {
	        var dictionary = new Dictionary<string, Uri>();
			  if (Links == null)
				  Links = new StringCollection();

			  var regex = new System.Text.RegularExpressions.Regex(@"([a-z]+),\s*([a-z]+://.*)$", RegexOptions.IgnoreCase);
			  foreach (var i in Links.Cast<string>()
				  .Where(s => s.IsNullOrWhitespace() == false)
				  .Select(s => regex.Match(s)))
			  {
				  if (i == null || !i.Success) continue;
				  dictionary.Add(i.Groups[1].Value, new Uri(i.Groups[2].Value));

			  }
	        return dictionary;
        }
	}
}