using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
	public class AtlassianUser
	{
		public string Name { get; set; }

		public string EmailAddress { get; set; }

		public string Id { get; set; }

		public string DisplayName { get; set; }

		public bool Active { get; set; }

	}
}
