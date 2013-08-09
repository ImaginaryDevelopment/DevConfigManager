using System;
using System.Collections;
using System.Linq;

namespace DeveloperConfigurationManager.Models
{
    class DatabaseViewModel
	{
		Type GetDataSourceType(object datasource)
		{
			var enumerableType = datasource.GetType();
			var type = enumerableType.GetElementType() ?? enumerableType.GetGenericArguments().First();
			return type;
		}

		public IEnumerable Data { get; private set; }

		public string Server { get; private set; }

		public string Database { get; private set; }

		public string Table { get; private set; }

		public Type ElementType { get; private set; }
		public DatabaseViewModel(IEnumerable data, string server, string database, string table )
		{
			Data = data;
			Server = server;
			Database = database;
			Table = table;
			this.ElementType = this.GetDataSourceType(data);
		}
	}
}
