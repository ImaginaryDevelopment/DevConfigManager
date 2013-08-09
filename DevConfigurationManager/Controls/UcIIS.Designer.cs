namespace DeveloperConfigurationManager.Controls
{
	partial class UcIIS
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
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnStatus = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnRestart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnKill = new System.Windows.Forms.Button();
			this.btnClean = new System.Windows.Forms.Button();
			this.btnListApps = new System.Windows.Forms.Button();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tpLog = new System.Windows.Forms.TabPage();
			this.btnClear = new System.Windows.Forms.Button();
			this.tpApps = new System.Windows.Forms.TabPage();
			this.btnSave = new System.Windows.Forms.Button();
			this.dgApps = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.mStartPool = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.recycleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.cmbServer = new System.Windows.Forms.ComboBox();
			this.btnCancel = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tpLog.SuspendLayout();
			this.tpApps.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgApps)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// richTextBox1
			// 
			this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.richTextBox1.Location = new System.Drawing.Point(6, 6);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(718, 336);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// btnStatus
			// 
			this.btnStatus.Location = new System.Drawing.Point(203, 3);
			this.btnStatus.Name = "btnStatus";
			this.btnStatus.Size = new System.Drawing.Size(69, 26);
			this.btnStatus.TabIndex = 8;
			this.btnStatus.Text = "&Status";
			this.btnStatus.UseVisualStyleBackColor = true;
			this.btnStatus.Click += new System.EventHandler(this.BtnStatusClick);
			// 
			// btnStart
			// 
			this.btnStart.Location = new System.Drawing.Point(327, 3);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(52, 26);
			this.btnStart.TabIndex = 7;
			this.btnStart.Text = "S&tart";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.BtnStartClick);
			// 
			// btnRestart
			// 
			this.btnRestart.Location = new System.Drawing.Point(137, 3);
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(60, 26);
			this.btnRestart.TabIndex = 6;
			this.btnRestart.Text = "&Restart";
			this.btnRestart.UseVisualStyleBackColor = true;
			this.btnRestart.Click += new System.EventHandler(this.BtnRestartClick);
			// 
			// btnStop
			// 
			this.btnStop.Location = new System.Drawing.Point(278, 3);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(43, 26);
			this.btnStop.TabIndex = 5;
			this.btnStop.Text = "Sto&p";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// btnKill
			// 
			this.btnKill.Location = new System.Drawing.Point(385, 3);
			this.btnKill.Name = "btnKill";
			this.btnKill.Size = new System.Drawing.Size(89, 26);
			this.btnKill.TabIndex = 10;
			this.btnKill.Text = "&Kill w3wp.exe";
			this.btnKill.UseVisualStyleBackColor = true;
			this.btnKill.Click += new System.EventHandler(this.BtnKillClick);
			// 
			// btnClean
			// 
			this.btnClean.Location = new System.Drawing.Point(480, 3);
			this.btnClean.Name = "btnClean";
			this.btnClean.Size = new System.Drawing.Size(99, 26);
			this.btnClean.TabIndex = 11;
			this.btnClean.Text = "&Clean Asp Cache";
			this.toolTip1.SetToolTip(this.btnClean, "Clean your Asp.net Temp Cache");
			this.btnClean.UseVisualStyleBackColor = true;
			this.btnClean.Click += new System.EventHandler(this.BtnCleanClick);
			// 
			// btnListApps
			// 
			this.btnListApps.Location = new System.Drawing.Point(6, 6);
			this.btnListApps.Name = "btnListApps";
			this.btnListApps.Size = new System.Drawing.Size(63, 26);
			this.btnListApps.TabIndex = 12;
			this.btnListApps.Text = "&List apps";
			this.toolTip1.SetToolTip(this.btnListApps, "List your iis configured apps");
			this.btnListApps.UseVisualStyleBackColor = true;
			this.btnListApps.Click += new System.EventHandler(this.BtnListAppsClick);
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tpLog);
			this.tabControl1.Controls.Add(this.tpApps);
			this.tabControl1.Location = new System.Drawing.Point(7, 35);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(749, 433);
			this.tabControl1.TabIndex = 13;
			// 
			// tpLog
			// 
			this.tpLog.Controls.Add(this.richTextBox1);
			this.tpLog.Controls.Add(this.btnClear);
			this.tpLog.Location = new System.Drawing.Point(4, 22);
			this.tpLog.Name = "tpLog";
			this.tpLog.Padding = new System.Windows.Forms.Padding(3);
			this.tpLog.Size = new System.Drawing.Size(741, 407);
			this.tpLog.TabIndex = 0;
			this.tpLog.Text = "Log";
			this.tpLog.UseVisualStyleBackColor = true;
			// 
			// btnClear
			// 
			this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnClear.Location = new System.Drawing.Point(6, 348);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(63, 26);
			this.btnClear.TabIndex = 17;
			this.btnClear.Text = "Cl&ear";
			this.toolTip1.SetToolTip(this.btnClear, "Clear Log");
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
			// 
			// tpApps
			// 
			this.tpApps.Controls.Add(this.btnSave);
			this.tpApps.Controls.Add(this.dgApps);
			this.tpApps.Controls.Add(this.btnListApps);
			this.tpApps.Location = new System.Drawing.Point(4, 22);
			this.tpApps.Name = "tpApps";
			this.tpApps.Padding = new System.Windows.Forms.Padding(3);
			this.tpApps.Size = new System.Drawing.Size(741, 407);
			this.tpApps.TabIndex = 1;
			this.tpApps.Text = "Applications";
			this.tpApps.UseVisualStyleBackColor = true;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(75, 6);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(63, 26);
			this.btnSave.TabIndex = 13;
			this.btnSave.Text = "Sa&ve";
			this.toolTip1.SetToolTip(this.btnSave, "List your iis configured apps");
			this.btnSave.UseVisualStyleBackColor = true;
			this.btnSave.Click += new System.EventHandler(this.BtnSaveClick);
			// 
			// dgApps
			// 
			this.dgApps.AllowUserToOrderColumns = true;
			this.dgApps.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgApps.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgApps.ContextMenuStrip = this.contextMenuStrip1;
			this.dgApps.Location = new System.Drawing.Point(6, 38);
			this.dgApps.Name = "dgApps";
			this.dgApps.Size = new System.Drawing.Size(729, 363);
			this.dgApps.TabIndex = 0;
			this.dgApps.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1CellFormatting);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.toolStripMenuItem1,
            this.mStartPool,
            this.stopToolStripMenuItem1,
            this.recycleToolStripMenuItem,
            this.statusToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(121, 120);
			this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.ContextMenuStrip1Opening);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(117, 6);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Enabled = false;
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
			this.toolStripMenuItem1.Text = "AppPool";
			// 
			// mStartPool
			// 
			this.mStartPool.Name = "mStartPool";
			this.mStartPool.Size = new System.Drawing.Size(120, 22);
			this.mStartPool.Text = "Start";
			this.mStartPool.Click += new System.EventHandler(this.MStartPoolClick);
			// 
			// stopToolStripMenuItem1
			// 
			this.stopToolStripMenuItem1.Name = "stopToolStripMenuItem1";
			this.stopToolStripMenuItem1.Size = new System.Drawing.Size(120, 22);
			this.stopToolStripMenuItem1.Text = "Stop";
			this.stopToolStripMenuItem1.Click += new System.EventHandler(this.StopToolStripMenuItem1Click);
			// 
			// recycleToolStripMenuItem
			// 
			this.recycleToolStripMenuItem.Name = "recycleToolStripMenuItem";
			this.recycleToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.recycleToolStripMenuItem.Text = "Recycle";
			this.recycleToolStripMenuItem.Click += new System.EventHandler(this.RecycleToolStripMenuItemClick);
			// 
			// statusToolStripMenuItem
			// 
			this.statusToolStripMenuItem.Name = "statusToolStripMenuItem";
			this.statusToolStripMenuItem.Size = new System.Drawing.Size(120, 22);
			this.statusToolStripMenuItem.Text = "Status";
			this.statusToolStripMenuItem.Click += new System.EventHandler(this.StatusToolStripMenuItemClick);
			// 
			// cmbServer
			// 
			this.cmbServer.FormattingEnabled = true;
			this.cmbServer.Items.AddRange(new object[] {
            "localhost",
            "dev1",
            "pqoappl1",
            "pqoweb1"});
			this.cmbServer.Location = new System.Drawing.Point(10, 3);
			this.cmbServer.Name = "cmbServer";
			this.cmbServer.Size = new System.Drawing.Size(121, 21);
			this.cmbServer.TabIndex = 14;
			this.cmbServer.Text = "localhost";
			this.toolTip1.SetToolTip(this.cmbServer, "Target Server");
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(678, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(63, 26);
			this.btnCancel.TabIndex = 15;
			this.btnCancel.Text = "Ca&ncel";
			this.toolTip1.SetToolTip(this.btnCancel, "Cancel the request");
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// UcIIS
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.cmbServer);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.btnClean);
			this.Controls.Add(this.btnKill);
			this.Controls.Add(this.btnStatus);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnRestart);
			this.Controls.Add(this.btnStop);
			this.Name = "UcIIS";
			this.Size = new System.Drawing.Size(755, 471);
			this.Load += new System.EventHandler(this.UcIISLoad);
			this.tabControl1.ResumeLayout(false);
			this.tpLog.ResumeLayout(false);
			this.tpApps.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgApps)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnStatus;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnRestart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnKill;
		private System.Windows.Forms.Button btnClean;
		private System.Windows.Forms.Button btnListApps;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpLog;
		private System.Windows.Forms.TabPage tpApps;
		private System.Windows.Forms.DataGridView dgApps;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.ComboBox cmbServer;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem mStartPool;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem recycleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem statusToolStripMenuItem;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnSave;
	}
}
