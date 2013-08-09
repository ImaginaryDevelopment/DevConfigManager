namespace Domain.Helpers
{
	using System;

	public class LambdaComparer<T> : System.Collections.IComparer, System.Collections.Generic.IComparer<T>
		where T : class
	{
		readonly Func<T, T, ComparerResult> _comparerFunc;

		public LambdaComparer(Func<T, T, ComparerResult> comparerFunc)
		{
			this._comparerFunc = comparerFunc;
		}

		public LambdaComparer(Func<T, T, int> comparerFunc)
		{
			this._comparerFunc = (left, right) =>
				{
					var raw = comparerFunc(left, right);
					if (raw == 0)
					{
						return ComparerResult.Equal;
					}

					if (raw > 0)
					{
						return ComparerResult.GreaterThan;
					}
					return ComparerResult.LessThan;
				};
		}

		public enum ComparerResult
		{
			Equal = 0,
			LessThan = -1,
			GreaterThan = 1
		}

		/// <summary>
		/// http://codebetter.com/davidhayden/2005/03/06/implementing-icomparer-for-sorting-custom-objects/
		/// </summary>
		public int Compare(object x, object y)
		{
			var left = x as T;
			if (!(x is T))
				throw new ArgumentException("x(left) is not of type " + typeof(T).Name);
			var right = y as T;
			if (!(y is T))
				throw new ArgumentException("y(right) is not of type " + typeof(T).Name);
			return Compare(left, right);
		}

		public int Compare(T left, T right)
		{
			if (left == null && right == null)
				return (int)ComparerResult.Equal;
			if (left == null)
				return (int)ComparerResult.LessThan;
			if (right == null)
				return (int)ComparerResult.GreaterThan;
			return (int)this._comparerFunc(left, right);
		}
	}
}
