using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
	using System.Collections;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.Reactive;
	using System.Reactive.Linq;

	public static class EnumerableExtensions
	{
		/// <summary>
		/// http://stackoverflow.com/a/7072121/57883
		/// </summary>
		public static Type GetEnumerableType(this Type type)
		{
			if(type == null)
				throw new ArgumentNullException("type");
			if (type.IsArray) return type.GetElementType();

			if(type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>)) return type.GetGenericArguments()[0];

			var interfaceType =
				(from i in type.GetInterfaces()
				 where i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IEnumerable<>)
				 select i).FirstOrDefault();
			if(interfaceType == null)
				throw new ArgumentOutOfRangeException("type passed does not represent an enumerable type.","type");
			return interfaceType.GetEnumerableType();
		}

		public static void AddRange<T>(this IList<T> destination, IEnumerable<T> src)
		{
			foreach (var i in src) destination.Add(i);
		}

		public static void AddRangeIfMissing(this StringCollection dest, IEnumerable<string> items)
		{
			foreach (var i in items)
				if (dest.Cast<string>().Contains(i, StringComparer.CurrentCultureIgnoreCase) == false) dest.Add(i);

		}

		public static void AddRangeIfMissing<T>(this ICollection<T> dest, IEnumerable<T> items)
		{
			foreach (var item in items)
			{
				dest.AddIfMissing(item);
			}
		}

		public static void AddIfMissing<T>(this ICollection<T> dest, T item, IEqualityComparer<T> comparer)
		{
			if (dest.Contains(item, comparer)) return;
			dest.Add(item);
		}

		public static void AddIfMissing<T>(this ICollection<T> dest, T item)
		{
			if (dest.Contains(item)) return;
			dest.Add(item);
		}

		/// <summary>
		/// Returns all distinct elements of the given source, where "distinctness"
		/// is determined via a projection and the specified comparer for the projected type.
		/// </summary>
		/// <remarks>
		/// This operator uses deferred execution and streams the results, although
		/// a set of already-seen keys is retained. If a key is seen multiple times,
		/// only the first element with that key is returned.
		/// </remarks>
		/// <typeparam name="TSource">Type of the source sequence</typeparam>
		/// <typeparam name="TKey">Type of the projected element</typeparam>
		/// <param name="source">Source sequence</param>
		/// <param name="keySelector">Projection for determining "distinctness"</param>
		/// <param name="comparer">The equality comparer to use to determine whether or not keys are equal.
		/// If null, the default equality comparer for <c>TSource</c> is used.</param>
		/// <returns>A sequence consisting of distinct elements from the source sequence,
		/// comparing them by the specified key projection.</returns>
		public static IEnumerable<TSource> DistinctBy<TSource, TKey>(this IEnumerable<TSource> source,
			 Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			if (keySelector == null)
			{
				throw new ArgumentNullException("keySelector");
			}
			return DistinctByImpl(source, keySelector, comparer);
		}

		private static IEnumerable<TSource> DistinctByImpl<TSource, TKey>(
			IEnumerable<TSource> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
		{
#if !NO_HASHSET
			var knownKeys = new HashSet<TKey>(comparer);
			foreach (TSource element in source)
			{
				if (knownKeys.Add(keySelector(element)))
				{
					yield return element;
				}
			}
#else
            //
            // On platforms where LINQ is available but no HashSet<T>
            // (like on Silverlight), implement this operator using 
            // existing LINQ operators. Using GroupBy is slightly less
            // efficient since it has do all the grouping work before
            // it can start to yield any one element from the source.
            //

            return source.GroupBy(keySelector, comparer).Select(g => g.First());
#endif
		}

		/// <summary>
		/// if hot or cold, make sure we don't miss any items
		/// </summary>
		public static IObservable<T> AsSubscription<T>(this ObservableCollection<T> src)
		{
			var observable =
				src.ToObservableCollectionChanged().Where(s => s.EventArgs.Action == NotifyCollectionChangedAction.Add).SelectMany(
				s => s.EventArgs.NewItems.Cast<T>());
			return observable.StartWith(values:src.ToArray());
		}

		public static IObservable<T> AsObservableAccumulator<T>(this ObservableCollection<T> src)
		{
			return
				src.ToObservableCollectionChanged().Where(e => e.EventArgs.Action == NotifyCollectionChangedAction.Add).SelectMany(
					e => e.EventArgs.NewItems.Cast<T>());
		}

		static IObservable<EventPattern<NotifyCollectionChangedEventArgs>> ToObservableCollectionChanged<T>(
			this ObservableCollection<T> src)
		{
			var observable =
				Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
						h => src.CollectionChanged += h, h => src.CollectionChanged -= h);

			return observable;
		}

		public static IEnumerable<string> GroupLinesBy(this IEnumerable<string> text, string delimiter)
		{
			var sb = new StringBuilder();
			var empties = new StringBuilder();
			foreach (var item in text.SkipWhile(string.IsNullOrWhiteSpace))
			{
				if (item.StartsWith(delimiter) && sb.Length > 0)
				{
					yield return sb.ToString();
					sb.Clear();
				}

				if (string.IsNullOrWhiteSpace(item))
					empties.AppendLine(item);
				else
				{
					sb.AppendLine(item);
					empties.Clear();
				}
			}
			if (sb.Length > 0) yield return sb.ToString();
		}
	}
}
