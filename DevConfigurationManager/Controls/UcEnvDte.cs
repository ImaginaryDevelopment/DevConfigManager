using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Domain.EnvDte;
using Domain.Extensions;
using Domain.Helpers;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcEnvDte : UserControl
	{
		const string _expandme = "expandme";

		readonly Lazy<Dte> _envDte;

		IEnumerable _data;

		public UcEnvDte(Func<Dte> envDte)
		{
			_envDte =new Lazy<Dte>(envDte);
			InitializeComponent();
			
		}

		void UpdateDynamicFilterOptions()
		{
			cmbFilterType.InvokeSafe(
				cb =>
				{
					var oldText = cb.Text;
					cb.DataSource = null;
					cb.DataSource = dataGridView1.Columns;
					var displayMember = LinqOp.PropertyOf<DataGridViewColumn>(dgc => dgc.HeaderText).Name;
					cb.DisplayMember = displayMember;
					var valueMember = LinqOp.PropertyOf<DataGridViewColumn>(dgc => dgc.DataPropertyName).Name;
					cb.ValueMember = valueMember;
					cb.Text = oldText;
				});

		}

		private void BtnCommandsClick(object sender, EventArgs e)
		{
			this._data = _envDte.Value.GetCommands().ToArray();
			
			dataGridView1.DataSource = this._data;

			UpdateDynamicFilterOptions();
		}

		void FilterDataGrid(string column, string filter)
		{
			if (dataGridView1.Tag != null && (Domain.Extensions.StringExtensions.IsNullOrEmpty(column) || Domain.Extensions.StringExtensions.IsNullOrEmpty(filter)))
			{
					dataGridView1.DataSource = this._data;
					return;
				
			}

			var dataDyn = this._data as ICollection<object>;

			if (dataDyn.Count < 1) return;
			var enumType = this._data.GetType().GetEnumerableType();
			
			IEnumerable<object> filtered = dataDyn;
			if (Domain.Extensions.StringExtensions.IsNullOrEmpty(column) == false && Domain.Extensions.StringExtensions.IsNullOrEmpty(filter) == false)
			{
				var property = enumType.GetProperty(column);
				if (property == null) { return; }
				if (property.PropertyType.IsAssignableFrom(typeof(List<string>)))
				{
					filtered = dataDyn.Where(f => property.GetValue(f).CastTo<IEnumerable<string>>().Any(v => Regex.IsMatch(v, filter, RegexOptions.IgnoreCase)));

				}
				else
				{
					filtered = dataDyn.Where(f => Regex.IsMatch(property.GetValue(f).ToString(), filter, RegexOptions.IgnoreCase));

				}
			}
            var nameProp = enumType.GetProperty(LinqOp.PropertyOf<Domain.EnvDte.DteCommand>(cd => cd.LocalizedName).Name);
			if (ckNameless.Checked == false && nameProp != null) filtered = filtered.Where(f => string.IsNullOrEmpty((string)nameProp.GetValue(f)) == false);
			var bindingsProp = enumType.GetProperty(commandDetailBindingsName);
			if (ckNoBindings.Checked == false && bindingsProp != null)
				filtered = filtered.Where(
					f =>
						{
							var val = bindingsProp.GetValue(f);
							return val != null && val.CastTo<IEnumerable<object>>().Any();
						});
			var filteredEnumerated = filtered.ToList();
			dataGridView1.DataSource = filteredEnumerated;
		}
		private void UcEnvDteLoad(object sender, EventArgs e)
		{
			try
			{
				lblSln.Text = _envDte.Value.GetSlnName();
				cmbFilterType.ToObservableText().Subscribe(s => this.FilterDataGrid(s, txtFilter.InvokeSafeText()));
				txtFilter.ToObservableText().Subscribe(
						s => FilterDataGrid(cmbFilterType.InvokeSafeText(), s));
			}
			catch (Exception)
			{
				this.Enabled = false;
			}
			

		}


		static readonly string commandDetailBindingsName = LinqOp.PropertyOf<DteCommand>(cd => cd.Bindings).Name;
		
		private void DataGridView1CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (dataGridView1.Columns[e.ColumnIndex].DataPropertyName == commandDetailBindingsName)
			{
				var cell = dataGridView1[e.ColumnIndex, e.RowIndex];
				var value =(IEnumerable<string>) cell.Value;
				if (value != null && value.Any())
				{
					
					e.Value = value.Delimit(",");
					e.FormattingApplied = true;
				}
				
			}
		}

		private void CkNoBindingsCheckedChanged(object sender, EventArgs e)
		{
			FilterDataGrid(cmbFilterType.Text, txtFilter.Text);
		}

		private void CkNamelessCheckedChanged(object sender, EventArgs e)
		{
			FilterDataGrid(cmbFilterType.Text, txtFilter.Text);
		}

		private void BtnUnloadClick(object sender, EventArgs e)
		{
			_envDte.Value.UnloadProjects();
		}
		
		private void RefreshSolutionExplorer()
		{
			var projects = _envDte.Value.GetProjects().ToArray();
			lblProjects.Text = projects.Length.ToString();
			tvSolution.Nodes.Clear();
			var sln = _envDte.Value.GetSolutionItem();
			tvSolution.Nodes.Add(sln.Name, sln.Name);
			var slnNode = tvSolution.Nodes[sln.Name];
			slnNode.Tag = sln;
			slnNode.Nodes.Add(_expandme);

			dgSolutionGrid.DataSource = projects;
			
		}

		private void TabControl1SelectedIndexChanged(object sender, EventArgs e)
		{
			if (tabControl1.SelectedTab == tpSolutionExplorer) RefreshSolutionExplorer();
		}

		private void tvSolution_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (e.Node.Nodes.Count==1 && e.Node.Tag != null && e.Node.Nodes[0].Text==_expandme)
			{
				var nodeToExpand = e.Node;
				e.Node.Nodes.Clear();
				var parentUI = nodeToExpand.Tag as EnvDTE.UIHierarchyItem;
				var items = _envDte.Value.GetChildren(parentUI);
				foreach (var i in items)
				{
					nodeToExpand.Nodes.Add(i.Name, i.Name);
					var child = nodeToExpand.Nodes[i.Name];
					child.Tag = i;
					child.Nodes.Add(_expandme);
				}
			}
		}

		private void btnSuspendResharper_Click(object sender, EventArgs e)
		{
			_envDte.Value.SuspendResharper();
		}
	}
}
