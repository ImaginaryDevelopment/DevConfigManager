using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Stash
{
	public class StashRestUrl : IStashRepository
	{
		public string Authority { get; private set; }

		public string Project { get; private set; }

		public string Repository { get; set; }

		public StashRestUrl(string authority, string project, string repository)
		{
			
			if (authority.EndsWith("/"))
				throw new ArgumentException("authority should not end with slash");

			if (project.EndsWith("/"))
				throw new ArgumentException("project should not end with slash");
			if (repository.EndsWith("/"))
				throw new ArgumentException("repository should not end with slash");

			Authority = authority;
		
			Project = project;
			Repository = repository;
		}

		public Uri GetOpenReviewsUri(string scheme = "http")
		{
			//"http://" + stashAuthority + "/rest/api/1.0/projects/PROD/repos/pci-production/pull-requests?state=OPEN"
			return this.ToUri("pull-requests?state=OPEN", scheme);
		}

		public override string ToString()
		{
			//			"http://" + stashAuthority + "/rest/api/1.0/projects/DEVC/repos/hpx-current/pull-requests?state=OPEN"
			return Authority + "/rest/api/1.0/projects/" + Project + "/repos/" + Repository + "/";
		}

		Uri ToUri(string suffix, string scheme)
		{
			return new Uri(scheme + "://" + this + suffix);
		}

		public Uri ToUri(string scheme = "http")
		{
			return this.ToUri(string.Empty, scheme);
		}
		
	}
}
