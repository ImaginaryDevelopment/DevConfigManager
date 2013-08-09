using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Stash
{
	public interface IStashRepository
	{
		string Authority { get; }

		string Project { get;  }

		string Repository { get; }
	}
}
