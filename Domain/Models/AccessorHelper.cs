using System;

namespace Domain.Models
{
	public class AccessorHelper<T>
	{
		public Func<T> Getter { get; private set; }

		public Action<T> Setter { get; private set; }

		public AccessorHelper(Func<T> getter, Action<T> setter)
		{
			Getter = getter;
			Setter = setter;
		}
	}
}
