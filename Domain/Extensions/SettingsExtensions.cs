namespace Domain.Extensions
{
	using System;
	using System.Collections.ObjectModel;
	using System.Collections.Specialized;
	using System.ComponentModel;
	using System.Linq;
	using System.Linq.Expressions;
	using System.Reactive.Linq;

	using Domain.Helpers;

	using System.Reactive;

	public static class SettingsExtensions
	{
		public static IObservable<EventPattern<PropertyChangedEventArgs>> ToObservable<T, TProperty>(this T settings,
			Expression<Func<T, TProperty>> expression)
			where T : System.Configuration.ApplicationSettingsBase
		{
			var name = LinqOp.PropertyOf(expression).Name;
			var obs = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
				h => settings.PropertyChanged += h, h => settings.PropertyChanged -= h);
			return obs.Where(e => e.EventArgs.PropertyName == name);
		}

		/// <summary>
		/// In order to easily create a master ObservableCollection so that events coming out are added, removed, etc...
		/// </summary>
		public static ObservableCollection<string> ToObservableCollectionWrapper<T>(
			this T settings, Expression<Func<T, StringCollection>> selector)
			where T : System.Configuration.ApplicationSettingsBase
		{

			var current = selector.Compile()(settings);
			if (current == null)
				throw new NullReferenceException("current");

			var result = new ObservableCollection<string>(current.Cast<string>());

			result.ToObservableCollectionChanged().Where(e => e.EventArgs.Action != NotifyCollectionChangedAction.Move).Subscribe
				(
					e =>
					{

						switch (e.EventArgs.Action)
						{
							case NotifyCollectionChangedAction.Reset:
								current.Clear();
								break;
							case NotifyCollectionChangedAction.Replace:
							case NotifyCollectionChangedAction.Remove:
								e.EventArgs.OldItems.Cast<string>().ToList().ForEach(current.Remove);
								break;
						}
						//on remove there will be no new things to add
						if (e.EventArgs.NewItems != null)
							current.AddRangeIfMissing(e.EventArgs.NewItems.Cast<string>());
						settings.Save();
					});
			return result;
		}
	}
}
