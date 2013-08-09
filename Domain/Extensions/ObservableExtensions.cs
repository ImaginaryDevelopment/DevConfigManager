using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extensions
{
	using System.Collections.Specialized;
	using System.Reactive;
	using System.Reactive.Linq;

	public static class ObservableExtensions
	{
		public static IObservable<EventPattern<NotifyCollectionChangedEventArgs>> ToObservableCollectionChanged(this INotifyCollectionChanged source)
		{
			if(source == null)
				throw new ArgumentNullException("source");
			var obs = Observable.FromEventPattern<NotifyCollectionChangedEventHandler, NotifyCollectionChangedEventArgs>(
				h => source.CollectionChanged += h, h => source.CollectionChanged -= h);
			return obs;
		}
	}
}
