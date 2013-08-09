using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using Domain.Adapters;
using Domain.Extensions;
using Domain.Models;
using Newtonsoft.Json;
using StringExtensions = DeveloperConfigurationManager.StringExtensions;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcEnvironment : UserControl, IRefreshable, ISaveable, INotifyPropertyChanged
	{
		string _status;

		void Log(string text)
		{
			
			foreach (var line in StringExtensions.SplitLines(text))
				richTextBox1.AppendText(line);
			richTextBox1.AppendText(DateTime.Now.ToShortTimeString() + Environment.NewLine);
			richTextBox1.Select(richTextBox1.Text.Length - 1, 1);
		}

		public UcEnvironment()
		{
			InitializeComponent();
			cbReadLevel.DataSource = Enum.GetValues(typeof(EnvironmentVariableTarget));
			cbReadLevel.SelectedItem = EnvironmentVariableTarget.Machine;
			cbWriteLevel.DataSource = Enum.GetValues(typeof(EnvironmentVariableTarget));
			cbWriteLevel.SelectedItem = EnvironmentVariableTarget.Machine;
			cbReadLevel.SelectedValueChanged += (sender, e) => this.RefreshData();
			dgEnvironment.EditMode = DataGridViewEditMode.EditOnF2;
			dgEnvironment.ShowEditingIcon = true;

			textBox1.ToObservableText().Subscribe(_ => RefreshData());

		}

		async protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			await RefreshData();
		}

		IEnumerable<SettableKeyValue> Items {get
		{
			if (dgEnvironment.DataSource == null)
				return Enumerable.Empty<SettableKeyValue>();
			return dgEnvironment.DataSource.CastTo<IEnumerable<SettableKeyValue>>();
		}} 

		void ScaffoldColumns()
		{
			var type = Items.GetType();
			var itemType = type.IsGenericType ? type.GetGenericArguments().First() : type.GetElementType();
			var props = itemType.GetProperties();
			var q = from p in props
					  let display =
						  p.GetCustomAttributes(typeof(DisplayAttribute), false).Cast<DisplayAttribute>().FirstOrDefault()
			        let name = (display != null ? display.GetShortName() ?? display.GetName() : null) ?? p.Name
			        orderby (display != null ? display.GetOrder() : null) ?? int.MaxValue
			        select new { Prop = p, Name = name, Display = display };

			foreach (var p in q.Where(pi => pi.Display == null || pi.Display.GetAutoGenerateField().GetValueOrDefault(true)))
			{

				dgEnvironment.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = p.Prop.Name, Name = p.Name, HeaderText = p.Name, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

			}
		}

#pragma warning disable 1998
		public async Task RefreshData()
#pragma warning restore 1998
		{
			var evars = Environment.GetEnvironmentVariables((EnvironmentVariableTarget)cbReadLevel.SelectedValue);
			var bindable = evars.Keys.Cast<string>().Select(
					k => new SettableKeyValue(s => EnvironmentVar.SetEnvironmentVariable(k, s, (EnvironmentVariableTarget)cbWriteLevel.SelectedValue), k, evars[k].ToString())).OrderBy(k => k.Key).ToList();
			if (StringExtensions.IsNullOrEmpty(textBox1.Text) == false)
			{
				bindable = bindable.Where(k => k.Key.ToLowerInvariant().Contains(textBox1.Text.ToLowerInvariant())).ToList();
			}
			if (dgEnvironment.Columns.Count == 0)
			{
				ScaffoldColumns();
			}

			dgEnvironment.DataSource = bindable;
			Refreshed = DateTime.Now;
			Status = "Refreshed at " + Refreshed;
			
		}

		public DateTime? Refreshed { get; private set; }


		public void Save()
		{

			var folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			var savePath = System.IO.Path.Combine(folder, "ucenv.json");
			Save(savePath);
		}

		public bool CanSave()
		{
			if (Items == null)
			{
				Log("No app source");
				return false;
			}

			if (Items.Any() == false)
			{
				Log("No apps loaded");
				return false;
			}
			return true;
		}

		public void Save(string path)
		{
			if (CanSave() == false)
				return;
			System.IO.File.WriteAllText(path, JsonConvert.SerializeObject(Items));
			Status = "Wrote apps configuration to " + path;
			Log("Wrote apps configuration to " + path);
		}

		public string Status
		{
			get { return _status; }
			private set
			{
				if (_status != value)
				{
					_status = value;
					OnPropertyChanged();
				}
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		protected void OnPropertyChanged([CallerMemberName] string name = null)
		{
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(name));
		}

		private void dgEnvironment_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.RowIndex < 0) return;
			var columnName = dgEnvironment.Columns[e.ColumnIndex].DataPropertyName;
			var row = dgEnvironment.Rows[e.RowIndex];
			switch(columnName)
			{
				case "Key":
					if (row.Cells[e.ColumnIndex].Value.ToString().StartsWith("psh", StringComparison.CurrentCultureIgnoreCase)) row.DefaultCellStyle.ForeColor = Color.Blue;
					break;
			}
		}

	}
}
