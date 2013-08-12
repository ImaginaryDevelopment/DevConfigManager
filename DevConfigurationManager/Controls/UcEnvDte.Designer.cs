namespace DeveloperConfigurationManager.Controls
{
	partial class UcEnvDte
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.lblSln = new System.Windows.Forms.Label();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpCommands = new System.Windows.Forms.TabPage();
			this.ckNameless = new System.Windows.Forms.CheckBox();
			this.ckNoBindings = new System.Windows.Forms.CheckBox();
			this.cmbFilterType = new System.Windows.Forms.ComboBox();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.btnCommands = new System.Windows.Forms.Button();
			this.tpSolutionExplorer = new System.Windows.Forms.TabPage();
			this.lblProjects = new System.Windows.Forms.Label();
			this.tcSolutionExplorer = new System.Windows.Forms.TabControl();
			this.tpSolutionTree = new System.Windows.Forms.TabPage();
			this.tvSolution = new System.Windows.Forms.TreeView();
			this.tpSolutionGrid = new System.Windows.Forms.TabPage();
			this.dgSolutionGrid = new System.Windows.Forms.DataGridView();
			this.btnUnload = new System.Windows.Forms.Button();
			this.btnSuspendResharper = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.tabControl1.SuspendLayout();
			this.tpCommands.SuspendLayout();
			this.tpSolutionExplorer.SuspendLayout();
			this.tcSolutionExplorer.SuspendLayout();
			this.tpSolutionTree.SuspendLayout();
			this.tpSolutionGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgSolutionGrid)).BeginInit();
			this.SuspendLayout();
			// 
			// dataGridView1
			// 
			this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Location = new System.Drawing.Point(6, 70);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.Size = new System.Drawing.Size(873, 276);
			this.dataGridView1.TabIndex = 0;
			this.dataGridView1.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1CellFormatting);
			// 
			// lblSln
			// 
			this.lblSln.AutoSize = true;
			this.lblSln.Location = new System.Drawing.Point(852, 8);
			this.lblSln.Name = "lblSln";
			this.lblSln.Size = new System.Drawing.Size(57, 13);
			this.lblSln.TabIndex = 3;
			this.lblSln.Text = "AllLaps.sln";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tpCommands);
			this.tabControl1.Controls.Add(this.tpSolutionExplorer);
			this.tabControl1.Location = new System.Drawing.Point(16, 24);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(893, 378);
			this.tabControl1.TabIndex = 9;
			this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.TabControl1SelectedIndexChanged);
			// 
			// tpCommands
			// 
			this.tpCommands.Controls.Add(this.btnSuspendResharper);
			this.tpCommands.Controls.Add(this.ckNameless);
			this.tpCommands.Controls.Add(this.ckNoBindings);
			this.tpCommands.Controls.Add(this.cmbFilterType);
			this.tpCommands.Controls.Add(this.txtFilter);
			this.tpCommands.Controls.Add(this.btnCommands);
			this.tpCommands.Controls.Add(this.dataGridView1);
			this.tpCommands.Location = new System.Drawing.Point(4, 22);
			this.tpCommands.Name = "tpCommands";
			this.tpCommands.Padding = new System.Windows.Forms.Padding(3);
			this.tpCommands.Size = new System.Drawing.Size(885, 352);
			this.tpCommands.TabIndex = 0;
			this.tpCommands.Text = "Commands";
			this.tpCommands.UseVisualStyleBackColor = true;
			// 
			// ckNameless
			// 
			this.ckNameless.AutoSize = true;
			this.ckNameless.Checked = true;
			this.ckNameless.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckNameless.Location = new System.Drawing.Point(395, 34);
			this.ckNameless.Name = "ckNameless";
			this.ckNameless.Size = new System.Drawing.Size(218, 17);
			this.ckNameless.TabIndex = 12;
			this.ckNameless.Text = "Show Commands with no localized name";
			this.ckNameless.UseVisualStyleBackColor = true;
			this.ckNameless.CheckedChanged += new System.EventHandler(this.CkNamelessCheckedChanged);
			// 
			// ckNoBindings
			// 
			this.ckNoBindings.AutoSize = true;
			this.ckNoBindings.Checked = true;
			this.ckNoBindings.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckNoBindings.Location = new System.Drawing.Point(395, 11);
			this.ckNoBindings.Name = "ckNoBindings";
			this.ckNoBindings.Size = new System.Drawing.Size(187, 17);
			this.ckNoBindings.TabIndex = 11;
			this.ckNoBindings.Text = "Show Commands with no bindings";
			this.ckNoBindings.UseVisualStyleBackColor = true;
			this.ckNoBindings.CheckedChanged += new System.EventHandler(this.CkNoBindingsCheckedChanged);
			// 
			// cmbFilterType
			// 
			this.cmbFilterType.FormattingEnabled = true;
			this.cmbFilterType.Location = new System.Drawing.Point(19, 43);
			this.cmbFilterType.Name = "cmbFilterType";
			this.cmbFilterType.Size = new System.Drawing.Size(121, 21);
			this.cmbFilterType.TabIndex = 10;
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(146, 43);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(100, 20);
			this.txtFilter.TabIndex = 9;
			// 
			// btnCommands
			// 
			this.btnCommands.Location = new System.Drawing.Point(6, 6);
			this.btnCommands.Name = "btnCommands";
			this.btnCommands.Size = new System.Drawing.Size(101, 23);
			this.btnCommands.TabIndex = 8;
			this.btnCommands.Text = "Get Commands";
			this.btnCommands.UseVisualStyleBackColor = true;
			this.btnCommands.Click += new System.EventHandler(this.BtnCommandsClick);
			// 
			// tpSolutionExplorer
			// 
			this.tpSolutionExplorer.Controls.Add(this.lblProjects);
			this.tpSolutionExplorer.Controls.Add(this.tcSolutionExplorer);
			this.tpSolutionExplorer.Controls.Add(this.btnUnload);
			this.tpSolutionExplorer.Location = new System.Drawing.Point(4, 22);
			this.tpSolutionExplorer.Name = "tpSolutionExplorer";
			this.tpSolutionExplorer.Padding = new System.Windows.Forms.Padding(3);
			this.tpSolutionExplorer.Size = new System.Drawing.Size(885, 352);
			this.tpSolutionExplorer.TabIndex = 1;
			this.tpSolutionExplorer.Text = "SolutionExplorer";
			this.tpSolutionExplorer.UseVisualStyleBackColor = true;
			// 
			// lblProjects
			// 
			this.lblProjects.AutoSize = true;
			this.lblProjects.Location = new System.Drawing.Point(10, 11);
			this.lblProjects.Name = "lblProjects";
			this.lblProjects.Size = new System.Drawing.Size(23, 13);
			this.lblProjects.TabIndex = 12;
			this.lblProjects.Text = "Fail";
			// 
			// tcSolutionExplorer
			// 
			this.tcSolutionExplorer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tcSolutionExplorer.Controls.Add(this.tpSolutionTree);
			this.tcSolutionExplorer.Controls.Add(this.tpSolutionGrid);
			this.tcSolutionExplorer.Location = new System.Drawing.Point(6, 32);
			this.tcSolutionExplorer.Name = "tcSolutionExplorer";
			this.tcSolutionExplorer.SelectedIndex = 0;
			this.tcSolutionExplorer.Size = new System.Drawing.Size(873, 314);
			this.tcSolutionExplorer.TabIndex = 11;
			// 
			// tpSolutionTree
			// 
			this.tpSolutionTree.Controls.Add(this.tvSolution);
			this.tpSolutionTree.Location = new System.Drawing.Point(4, 22);
			this.tpSolutionTree.Name = "tpSolutionTree";
			this.tpSolutionTree.Padding = new System.Windows.Forms.Padding(3);
			this.tpSolutionTree.Size = new System.Drawing.Size(865, 288);
			this.tpSolutionTree.TabIndex = 0;
			this.tpSolutionTree.Text = "Tree";
			this.tpSolutionTree.UseVisualStyleBackColor = true;
			// 
			// tvSolution
			// 
			this.tvSolution.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tvSolution.Location = new System.Drawing.Point(3, 3);
			this.tvSolution.Name = "tvSolution";
			this.tvSolution.Size = new System.Drawing.Size(859, 282);
			this.tvSolution.TabIndex = 10;
			this.tvSolution.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.tvSolution_BeforeExpand);
			// 
			// tpSolutionGrid
			// 
			this.tpSolutionGrid.Controls.Add(this.dgSolutionGrid);
			this.tpSolutionGrid.Location = new System.Drawing.Point(4, 22);
			this.tpSolutionGrid.Name = "tpSolutionGrid";
			this.tpSolutionGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tpSolutionGrid.Size = new System.Drawing.Size(865, 288);
			this.tpSolutionGrid.TabIndex = 1;
			this.tpSolutionGrid.Text = "Grid";
			this.tpSolutionGrid.UseVisualStyleBackColor = true;
			// 
			// dgSolutionGrid
			// 
			this.dgSolutionGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgSolutionGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgSolutionGrid.Location = new System.Drawing.Point(3, 3);
			this.dgSolutionGrid.Name = "dgSolutionGrid";
			this.dgSolutionGrid.Size = new System.Drawing.Size(859, 282);
			this.dgSolutionGrid.TabIndex = 0;
			// 
			// btnUnload
			// 
			this.btnUnload.Location = new System.Drawing.Point(778, 6);
			this.btnUnload.Name = "btnUnload";
			this.btnUnload.Size = new System.Drawing.Size(101, 23);
			this.btnUnload.TabIndex = 9;
			this.btnUnload.Text = "Unload All";
			this.btnUnload.UseVisualStyleBackColor = true;
			this.btnUnload.Click += new System.EventHandler(this.BtnUnloadClick);
			// 
			// btnSuspendResharper
			// 
			this.btnSuspendResharper.Location = new System.Drawing.Point(760, 11);
			this.btnSuspendResharper.Name = "btnSuspendResharper";
			this.btnSuspendResharper.Size = new System.Drawing.Size(119, 23);
			this.btnSuspendResharper.TabIndex = 13;
			this.btnSuspendResharper.Text = "Suspend Resharper";
			this.btnSuspendResharper.UseVisualStyleBackColor = true;
			this.btnSuspendResharper.Click += new System.EventHandler(this.btnSuspendResharper_Click);
			// 
			// UcEnvDte
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.lblSln);
			this.Name = "UcEnvDte";
			this.Size = new System.Drawing.Size(912, 405);
			this.Load += new System.EventHandler(this.UcEnvDteLoad);
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.tabControl1.ResumeLayout(false);
			this.tpCommands.ResumeLayout(false);
			this.tpCommands.PerformLayout();
			this.tpSolutionExplorer.ResumeLayout(false);
			this.tpSolutionExplorer.PerformLayout();
			this.tcSolutionExplorer.ResumeLayout(false);
			this.tpSolutionTree.ResumeLayout(false);
			this.tpSolutionGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgSolutionGrid)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.Label lblSln;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpCommands;
		private System.Windows.Forms.CheckBox ckNameless;
		private System.Windows.Forms.CheckBox ckNoBindings;
		private System.Windows.Forms.ComboBox cmbFilterType;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.Button btnCommands;
		private System.Windows.Forms.TabPage tpSolutionExplorer;
		private System.Windows.Forms.Button btnUnload;
		private System.Windows.Forms.TreeView tvSolution;
		private System.Windows.Forms.TabControl tcSolutionExplorer;
		private System.Windows.Forms.TabPage tpSolutionTree;
		private System.Windows.Forms.TabPage tpSolutionGrid;
		private System.Windows.Forms.DataGridView dgSolutionGrid;
		private System.Windows.Forms.Label lblProjects;
		private System.Windows.Forms.Button btnSuspendResharper;
	}
}
