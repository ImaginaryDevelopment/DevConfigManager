using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public enum PshConfigEndpoint
	{
		[Display(Name = @"jaxpdoappl1:8000")]
		Pdo,
		localhost

	}
	enum Roots
	{
		[Display(Name = @"c:\projects\trunk\hpx")]
		Hpx
	}
	enum PshConfigDb
	{
		localhost,
	}
	public class ConfigEndpoint {
		readonly string _key;
		readonly PshConfigEndpoint _value;
		readonly string _name;
		readonly string _full;
		readonly bool _isCurrent;

		public string Key
		{
			get { return _key; }
		}

		public PshConfigEndpoint Value
		{
			get { return _value; }
		}

		public string Name
		{
			get { return _name; }
		}

		public string Full
		{
			get { return _full; }
		}

		public bool IsCurrent
		{
			get { return _isCurrent; }
		}

		public ConfigEndpoint(string key, PshConfigEndpoint value, string name, string full, bool isCurrent)
		{
			_key = key;
			_value = value;
			_name = name;
			_full = full;
			_isCurrent = isCurrent;
		}
	}
}