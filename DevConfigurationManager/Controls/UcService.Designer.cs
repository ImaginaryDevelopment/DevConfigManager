namespace DeveloperConfigurationManager.Controls
{
	partial class UcService
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
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.dgServiceList = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.queryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tbLog = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnQuery = new System.Windows.Forms.Button();
			this.txtServiceTarget = new System.Windows.Forms.TextBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tbGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgServiceList)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.tbLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tbGrid);
			this.tabControl1.Controls.Add(this.tbLog);
			this.tabControl1.Location = new System.Drawing.Point(0, 49);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(858, 399);
			this.tabControl1.TabIndex = 15;
			// 
			// tbGrid
			// 
			this.tbGrid.Controls.Add(this.cbFilterColumn);
			this.tbGrid.Controls.Add(this.txtFilter);
			this.tbGrid.Controls.Add(this.dgServiceList);
			this.tbGrid.Location = new System.Drawing.Point(4, 22);
			this.tbGrid.Name = "tbGrid";
			this.tbGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tbGrid.Size = new System.Drawing.Size(850, 373);
			this.tbGrid.TabIndex = 1;
			this.tbGrid.Text = "Services";
			this.tbGrid.UseVisualStyleBackColor = true;
			// 
			// cbFilterColumn
			// 
			this.cbFilterColumn.FormattingEnabled = true;
			this.cbFilterColumn.Location = new System.Drawing.Point(7, 4);
			this.cbFilterColumn.Name = "cbFilterColumn";
			this.cbFilterColumn.Size = new System.Drawing.Size(137, 21);
			this.cbFilterColumn.TabIndex = 8;
			// 
			// txtFilter
			// 
			this.txtFilter.Location = new System.Drawing.Point(150, 4);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(100, 20);
			this.txtFilter.TabIndex = 7;
			// 
			// dgServiceList
			// 
			this.dgServiceList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgServiceList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgServiceList.ContextMenuStrip = this.contextMenuStrip1;
			this.dgServiceList.Location = new System.Drawing.Point(6, 29);
			this.dgServiceList.Name = "dgServiceList";
			this.dgServiceList.Size = new System.Drawing.Size(838, 338);
			this.dgServiceList.TabIndex = 0;
			this.dgServiceList.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgServiceList_CellFormatting);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.queryToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(111, 92);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.startToolStripMenuItem.Text = "Start";
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			// 
			// restartToolStripMenuItem
			// 
			this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
			this.restartToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.restartToolStripMenuItem.Text = "Restart";
			// 
			// queryToolStripMenuItem
			// 
			this.queryToolStripMenuItem.Name = "queryToolStripMenuItem";
			this.queryToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
			this.queryToolStripMenuItem.Text = "Query";
			// 
			// tbLog
			// 
			this.tbLog.Controls.Add(this.richTextBox1);
			this.tbLog.Location = new System.Drawing.Point(4, 22);
			this.tbLog.Name = "tbLog";
			this.tbLog.Padding = new System.Windows.Forms.Padding(3);
			this.tbLog.Size = new System.Drawing.Size(850, 373);
			this.tbLog.TabIndex = 0;
			this.tbLog.Text = "Log";
			this.tbLog.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(844, 367);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// btnQuery
			// 
			this.btnQuery.Location = new System.Drawing.Point(512, 3);
			this.btnQuery.Name = "btnQuery";
			this.btnQuery.Size = new System.Drawing.Size(75, 23);
			this.btnQuery.TabIndex = 10;
			this.btnQuery.Text = "Search";
			this.btnQuery.UseVisualStyleBackColor = true;
			this.btnQuery.Click += new System.EventHandler(this.BtnQueryClick);
			// 
			// txtServiceTarget
			// 
			this.txtServiceTarget.Location = new System.Drawing.Point(279, 5);
			this.txtServiceTarget.Name = "txtServiceTarget";
			this.txtServiceTarget.Size = new System.Drawing.Size(100, 20);
			this.txtServiceTarget.TabIndex = 6;
			this.txtServiceTarget.Text = "localhost";
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.Location = new System.Drawing.Point(780, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 16;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// UcService
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txtServiceTarget);
			this.Controls.Add(this.btnQuery);
			this.Controls.Add(this.tabControl1);
			this.Name = "UcService";
			this.Size = new System.Drawing.Size(858, 448);
			this.tabControl1.ResumeLayout(false);
			this.tbGrid.ResumeLayout(false);
			this.tbGrid.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgServiceList)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.tbLog.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tbGrid;
		private System.Windows.Forms.DataGridView dgServiceList;
		private System.Windows.Forms.TabPage tbLog;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnQuery;
		private System.Windows.Forms.TextBox txtServiceTarget;
		private System.Windows.Forms.TextBox txtFilter;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem restartToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem queryToolStripMenuItem;
		private System.Windows.Forms.ComboBox cbFilterColumn;
	}
}
