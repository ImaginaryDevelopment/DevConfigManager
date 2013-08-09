using System;

namespace Domain.Models.Stash
{
	public class StashUrl : IStashRepository
	{
		private StashRestUrl stashRestUrl;

		public string Authority { get; private set; }

		public string Project { get; private set; }

		public string Repository { get; private set; }

		public StashUrl(string authority, string project, string repository)
		{
			if (authority.EndsWith("/"))
				throw new ArgumentException("authority should not end with slash");
			
			if (project.EndsWith("/"))
				throw new ArgumentException("project should not end with slash");
			if(repository.EndsWith("/"))
				throw new ArgumentException("repository should not end with slash");
			Authority = authority;
			Project = project;
			Repository = repository;
			
		}

		public StashUrl(IStashRepository stashRestUrl)
			: this(stashRestUrl.Authority, stashRestUrl.Project, stashRestUrl.Repository)
		{
		}

		public override string ToString()
		{
			return Authority + "/projects/" + Project + "/repos/" + Repository + "/";
			// authority + /projects/DEVC/repos/hpx-current/

		}

		public Uri ToUri(string scheme = "http")
		{
			return new Uri(scheme + "://" + this);
		}
	}
}
