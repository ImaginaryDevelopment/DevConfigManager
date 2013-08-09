using System;

namespace Domain.EnvDte
{
	using System.Collections.Generic;

	public class CommandDetail
	{
		readonly string _localizedName;

		readonly int _id;

		readonly IEnumerable<string> _bindings;

		public string LocalizedName
		{
			get
			{
				return this._localizedName;
			}
		}

		public int Id
		{
			get
			{
				return this._id;
			}
		}

		public IEnumerable<string> Bindings
		{
			get
			{
				return this._bindings;
			}
		}

		public CommandDetail(string localizedName, int id,IEnumerable<string>	bindings)
		{
			this._localizedName = localizedName;
			this._id = id;
			this._bindings = bindings;
		}
	}
}