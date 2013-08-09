using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Models
{
	public class SettableKeyValue
	{
		readonly Action<string> _setterAction;
		readonly string _key;
		string _value;

		public SettableKeyValue(Action<string> setterAction, string key, string value)
		{
			_setterAction = setterAction;
			_key = key;
			_value = value;
		}

		public string Value
		{
			get { return _value; }
			set { _setterAction(value);
				_value = value;
			}
		}

		[Key]
		[Display(Order = 1)]
		public string Key
		{
			get { return _key; }
		}
	}
}
