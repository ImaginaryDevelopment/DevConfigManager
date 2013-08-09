using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using Domain.Extensions;
using Domain.Helpers;
using Disposable = System.Reactive.Disposables.Disposable;

namespace DeveloperConfigurationManager
{
    public static class ControlExtensions
	{
		public static void AppendText(this RichTextBox rch, string text, Color color)
		{
			var currentLength= rch.TextLength;
			var oldSelectionStart = rch.SelectionStart; //set the selection back to what it was previously if there was one.
			var oldSelectionLength = rch.SelectionLength;
			rch.AppendText(text);
			rch.Select(currentLength, text.Length);
			rch.SelectionColor = color;
			rch.DeselectAll();
			rch.Select(oldSelectionStart, oldSelectionLength);
		}
		public static void BindAsLink<T>(this DataGridView dgv, Action<DataGridViewLinkCell> onClick, params Expression<Func<T, object>>[] selectors)
		{
			dgv.DataBindingComplete += (sender, e) =>
				{
					if (dgv.Rows.Count > 0 && (dgv.Rows[0].DataBoundItem is T) == false)
					{
						throw new InvalidDataException(
							"Bound data is not of the expected type (" + typeof(T).Name + ") for datagridview(" + dgv.Name + ")");
					}

					foreach (var s in selectors)
					{
						var column = dgv.Columns[LinqOp.PropertyOf(s).Name];
						if (column == null) continue;
						var linkColumn = new DataGridViewLinkColumn()
							{
								DisplayIndex = column.DisplayIndex,
								HeaderText = column.HeaderText,
								Name = column.Name,
								DataPropertyName = column.DataPropertyName
							};
						dgv.Columns.Remove(column);
						dgv.Columns.Add(linkColumn);
					}
				};
			dgv.CellContentClick += (sender, e) =>
			{
				if (e.RowIndex < 0) return; //header
				var columnName = dgv.Columns[e.ColumnIndex].DataPropertyName;
				
				if (selectors.Any(s => LinqOp.PropertyOf(s).Name == columnName))
				{
					var cell = dgv[columnName, e.RowIndex] as DataGridViewLinkCell;
					if (cell != null)
					onClick(cell);
				}
			};
		}
		public static TResult GetValue<T, TResult>(this DataGridViewRow row, Expression<Func<T, TResult>> selector)
		{
			return row.Cells[LinqOp.PropertyOf(selector).Name].Value.CastTo<TResult>();
		}

		public static void SubscribeToCollectionChanged(this ListControl dest, INotifyCollectionChanged src)
		{
			src.ToObservableCollectionChanged().SubscribeOn(SynchronizationContext.Current).Subscribe(
				_ =>
				{
					dest.DataSource = null;
					dest.DataSource = src;
				});
		}

		public static IDisposable AppendObservable(this RichTextBox richTextBox, ObservableCollection<string> source)
		{
			if (richTextBox == null) return Disposable.Create(() => { });
			return source.AsSubscription().TakeWhile(_ => richTextBox.IsDisposed == false)
				.Subscribe(s => richTextBox.InvokeSafeAppend(s + Environment.NewLine));
		}

		public static IDisposable AppendObservableStream(this RichTextBox richTextBox, IObservable<string> source)
		{
			if (richTextBox == null) return Disposable.Create(() => { });
			return source
				.TakeWhile(_ => richTextBox.IsDisposed == false)
				.Subscribe(s => richTextBox.InvokeSafeAppend(s + Environment.NewLine));
		}

		public static IObservable<Unit> ToObservableOnDropDown(this ComboBox cb)
		{
			var observable =
				Observable.FromEventPattern<EventHandler, EventArgs>(h => cb.DropDown += h, h => cb.DropDown -= h).SubscribeOn(
					SynchronizationContext.Current).Select(_ => new Unit());

			return observable;
		}

		public static IObservable<Unit> ToObservableOnDropDownClosed(this ComboBox cb)
		{
			var observable =
				Observable.FromEventPattern<EventHandler, EventArgs>(h => cb.DropDownClosed += h, h => cb.DropDownClosed -= h).SubscribeOn(
					SynchronizationContext.Current).Select(_ => new Unit());

			return observable;
		}

		public static IObservable<EventPattern<EventArgs>> ToObservableSelectedValueChanged(this ListControl box)
		{
			var obs =
				Observable.FromEventPattern<EventHandler, EventArgs>(
					h => box.SelectedValueChanged += h, h => box.SelectedValueChanged -= h).ObserveOn(SynchronizationContext.Current);
			return obs;
		}

		public static IObservable<string> ToObservableText(this Control c)
		{
			var txtChanged = Observable.FromEventPattern<EventHandler, EventArgs>(
				h => c.TextChanged += h, h => c.TextChanged -= h);
			return txtChanged.Throttle(TimeSpan.FromMilliseconds(800)).ObserveOn(SynchronizationContext.Current).Select(e => c.Text);
		}

		public static IObservable<string> ToObservablePropertyChanged(this INotifyPropertyChanged inpc)
		{
			var changed = Observable.FromEventPattern<PropertyChangedEventHandler, PropertyChangedEventArgs>(
				h => inpc.PropertyChanged += h, h => inpc.PropertyChanged -= h);

			return changed.Select(s => s.EventArgs.PropertyName);
		}

		public static IObservable<EventPattern<EventArgs>> ToObservableClick(this ToolStripItem mi)
		{
			var observable = Observable.FromEventPattern<EventHandler, EventArgs>(h => mi.Click += h, h => mi.Click -= h);
			return observable;
		}

		public static IObservable<EventPattern<EventArgs>> ToObservableClick(this Control c)
		{
			var observable = Observable.FromEventPattern<EventHandler, EventArgs>(h => c.Click += h, h => c.Click -= h);
			return observable;
		}

		public static IEnumerable<Control> Childrens(this Control c)
		{
			yield return c;
			foreach (var item in c.Controls.OfType<Control>())
				foreach (var child in item.Childrens())
					yield return child;
		}

		public static IObservable<EventPattern<TabControlEventArgs>> ToObservableSelected(this TabControl tc)
		{

			return
				Observable.FromEventPattern<TabControlEventHandler, TabControlEventArgs>(h => tc.Selected += h, h => tc.Selected -= h);
			//.SelectedIndexChanged
		}
		/// <summary>
		/// From BReusable
		/// </summary>
		/// <param name="control"></param>
		/// <param name="a"></param>
		public static void InvokeSafe(this Control control, Action a)
		{
			if (control.InvokeRequired)
				control.Invoke((EventHandler)delegate { a(); });
			else
				a();
		}
		public static void InvokeSafeAppend(this RichTextBox control, string text)
		{
			control.InvokeSafe(rch => rch.AppendText(text));
		}
		/// <summary>
		/// From BReusable
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="control"></param>
		/// <param name="doWhat"></param>
		public static void InvokeSafe<T>(this T control, Action<T> doWhat) where T : Control
		{
			Action a = () => doWhat(control);
			control.InvokeSafe(a);
		}

		public static TResult InvokeSafe<T, TResult>(this T control, Func<T, TResult> doWhat) where T : Control
		{
			TResult result = default(TResult);
			control.InvokeSafe(() => result = doWhat(control));
			return result;
		}

		public static string InvokeSafeText(this Control control)
		{
			string result = null;

			Action a = () => result = control.Text;
			control.InvokeSafe(a);
			return result;
		}

		/// <summary>
		/// From BReusable
		/// </summary>
		/// <param name="control"></param>
		/// <param name="text"></param>
		public static void InvokeSafeText(this Control control, String text)
		{
			Action a = () => control.Text = text;
			control.InvokeSafe(a);
		}
		/// <summary>
		/// From BReusable
		/// </summary>
		/// <param name="control"></param>
		/// <param name="enabled"></param>
		public static void InvokeSafeEnabled(this Control control, bool enabled)
		{
			Action a = () => control.Enabled = enabled;
			control.InvokeSafe(a);
		}
		/// <summary>
		/// From BReusable
		/// </summary>
		/// <param name="control"></param>
		/// <param name="text"></param>
		public static void InvokeSafeTextAdd(this Control control, string text)
		{
			control.InvokeSafe(() => control.Text += text);
		}
	}
}
