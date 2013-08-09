namespace DeveloperConfigurationManager.Controls
{
	partial class UcDatabase
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
			this.components = new System.ComponentModel.Container();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tbGrid = new System.Windows.Forms.TabPage();
			this.cbFilterColumn = new System.Windows.Forms.ComboBox();
			this.dgData = new System.Windows.Forms.DataGridView();
			this.txtFilterCriteria = new System.Windows.Forms.TextBox();
			this.tbLog = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.cbServer = new System.Windows.Forms.ComboBox();
			this.cbTable = new System.Windows.Forms.ComboBox();
			this.cbDatabase = new System.Windows.Forms.ComboBox();
			this.btnPopulate = new System.Windows.Forms.Button();
			this.bsTables = new System.Windows.Forms.BindingSource(this.components);
			this.rchWhere = new System.Windows.Forms.RichTextBox();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.rchSelect = new System.Windows.Forms.RichTextBox();
			this.lblCleanTable = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tbGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgData)).BeginInit();
			this.tbLog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bsTables)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tbGrid);
			this.tabControl1.Controls.Add(this.tbLog);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(802, 293);
			this.tabControl1.TabIndex = 15;
			// 
			// tbGrid
			// 
			this.tbGrid.Controls.Add(this.cbFilterColumn);
			this.tbGrid.Controls.Add(this.dgData);
			this.tbGrid.Controls.Add(this.txtFilterCriteria);
			this.tbGrid.Location = new System.Drawing.Point(4, 22);
			this.tbGrid.Name = "tbGrid";
			this.tbGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tbGrid.Size = new System.Drawing.Size(794, 267);
			this.tbGrid.TabIndex = 1;
			this.tbGrid.Text = "Grid";
			this.tbGrid.UseVisualStyleBackColor = true;
			// 
			// cbFilterColumn
			// 
			this.cbFilterColumn.FormattingEnabled = true;
			this.cbFilterColumn.Location = new System.Drawing.Point(9, 3);
			this.cbFilterColumn.Name = "cbFilterColumn";
			this.cbFilterColumn.Size = new System.Drawing.Size(143, 21);
			this.cbFilterColumn.TabIndex = 3;
			// 
			// dgData
			// 
			this.dgData.AllowUserToOrderColumns = true;
			this.dgData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgData.Location = new System.Drawing.Point(9, 29);
			this.dgData.Name = "dgData";
			this.dgData.Size = new System.Drawing.Size(782, 232);
			this.dgData.TabIndex = 0;
			this.dgData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgDataCellFormatting);
			// 
			// txtFilterCriteria
			// 
			this.txtFilterCriteria.Location = new System.Drawing.Point(158, 3);
			this.txtFilterCriteria.Name = "txtFilterCriteria";
			this.txtFilterCriteria.Size = new System.Drawing.Size(135, 20);
			this.txtFilterCriteria.TabIndex = 2;
			// 
			// tbLog
			// 
			this.tbLog.Controls.Add(this.richTextBox1);
			this.tbLog.Location = new System.Drawing.Point(4, 22);
			this.tbLog.Name = "tbLog";
			this.tbLog.Padding = new System.Windows.Forms.Padding(3);
			this.tbLog.Size = new System.Drawing.Size(794, 267);
			this.tbLog.TabIndex = 0;
			this.tbLog.Text = "Log";
			this.tbLog.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(788, 261);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// cbServer
			// 
			this.cbServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbServer.FormattingEnabled = true;
			this.cbServer.Location = new System.Drawing.Point(7, 3);
			this.cbServer.Name = "cbServer";
			this.cbServer.Size = new System.Drawing.Size(217, 21);
			this.cbServer.TabIndex = 0;
			this.cbServer.Text = "localhost";
			// 
			// cbTable
			// 
			this.cbTable.FormattingEnabled = true;
			this.cbTable.Location = new System.Drawing.Point(453, 3);
			this.cbTable.Name = "cbTable";
			this.cbTable.Size = new System.Drawing.Size(217, 21);
			this.cbTable.TabIndex = 2;
			this.cbTable.Text = "dbo.config";
			// 
			// cbDatabase
			// 
			this.cbDatabase.FormattingEnabled = true;
			this.cbDatabase.Location = new System.Drawing.Point(230, 3);
			this.cbDatabase.Name = "cbDatabase";
			this.cbDatabase.Size = new System.Drawing.Size(217, 21);
			this.cbDatabase.TabIndex = 1;
			this.cbDatabase.Text = "DbConfig";
			// 
			// btnPopulate
			// 
			this.btnPopulate.Location = new System.Drawing.Point(676, 1);
			this.btnPopulate.Name = "btnPopulate";
			this.btnPopulate.Size = new System.Drawing.Size(75, 23);
			this.btnPopulate.TabIndex = 16;
			this.btnPopulate.Text = "Submit";
			this.btnPopulate.UseVisualStyleBackColor = true;
			this.btnPopulate.Click += new System.EventHandler(this.BtnPopulateClick);
			// 
			// rchWhere
			// 
			this.rchWhere.Location = new System.Drawing.Point(4, 33);
			this.rchWhere.Name = "rchWhere";
			this.rchWhere.Size = new System.Drawing.Size(791, 64);
			this.rchWhere.TabIndex = 17;
			this.rchWhere.Text = "";
			// 
			// splitContainer1
			// 
			this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.splitContainer1.Location = new System.Drawing.Point(7, 30);
			this.splitContainer1.Name = "splitContainer1";
			this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer1.Panel1
			// 
			this.splitContainer1.Panel1.Controls.Add(this.tabControl1);
			// 
			// splitContainer1.Panel2
			// 
			this.splitContainer1.Panel2.Controls.Add(this.lblCleanTable);
			this.splitContainer1.Panel2.Controls.Add(this.rchSelect);
			this.splitContainer1.Panel2.Controls.Add(this.rchWhere);
			this.splitContainer1.Size = new System.Drawing.Size(802, 397);
			this.splitContainer1.SplitterDistance = 293;
			this.splitContainer1.TabIndex = 18;
			// 
			// rchSelect
			// 
			this.rchSelect.Location = new System.Drawing.Point(4, 5);
			this.rchSelect.Name = "rchSelect";
			this.rchSelect.Size = new System.Drawing.Size(615, 22);
			this.rchSelect.TabIndex = 18;
			this.rchSelect.Text = "";
			// 
			// lblCleanTable
			// 
			this.lblCleanTable.AutoSize = true;
			this.lblCleanTable.Location = new System.Drawing.Point(628, 8);
			this.lblCleanTable.Name = "lblCleanTable";
			this.lblCleanTable.Size = new System.Drawing.Size(71, 13);
			this.lblCleanTable.TabIndex = 19;
			this.lblCleanTable.Text = "lblCleanTable";
			// 
			// UcDatabase
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.splitContainer1);
			this.Controls.Add(this.btnPopulate);
			this.Controls.Add(this.cbDatabase);
			this.Controls.Add(this.cbTable);
			this.Controls.Add(this.cbServer);
			this.Name = "UcDatabase";
			this.Size = new System.Drawing.Size(812, 430);
			this.Load += new System.EventHandler(this.UcDatabase_Load);
			this.tabControl1.ResumeLayout(false);
			this.tbGrid.ResumeLayout(false);
			this.tbGrid.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgData)).EndInit();
			this.tbLog.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bsTables)).EndInit();
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			this.splitContainer1.Panel2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tbGrid;
		private System.Windows.Forms.DataGridView dgData;
		private System.Windows.Forms.TextBox txtFilterCriteria;
		private System.Windows.Forms.TabPage tbLog;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ComboBox cbServer;
		private System.Windows.Forms.ComboBox cbTable;
		private System.Windows.Forms.ComboBox cbDatabase;
		private System.Windows.Forms.Button btnPopulate;
		private System.Windows.Forms.ComboBox cbFilterColumn;
		private System.Windows.Forms.BindingSource bsTables;
		private System.Windows.Forms.RichTextBox rchWhere;
		private System.Windows.Forms.SplitContainer splitContainer1;
		private System.Windows.Forms.RichTextBox rchSelect;
		private System.Windows.Forms.Label lblCleanTable;
	}
}
