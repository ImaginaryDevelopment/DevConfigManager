using System.Collections.Generic;

namespace Domain.Helpers
{
	using Domain.Properties;

	public static class PetaPocoExtensions
	{
		public static IEnumerable<PetaPocoTableInfo> GetTablesAndViews(this PetaPoco.Database d)
		{
			return d.Query<PetaPocoTableInfo>(Resources.PetaPocoExtensions_GetTablesAndViews);
		}

		public static IEnumerable<string> GetDatabases(this PetaPoco.Database d)
		{
			return d.Query<string>(Resources.GetDatabases);
		}
	}

	public class PetaPocoTableInfo
	{
		public string TableName { get; set; }

		public string Type { get; set; }

		public string SchemaName { get; set; }
	}
}
