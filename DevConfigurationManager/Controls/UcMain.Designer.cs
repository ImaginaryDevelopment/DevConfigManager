namespace DeveloperConfigurationManager.Controls
{
	partial class UcMain
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSave = new System.Windows.Forms.ToolStripMenuItem();
			this.mnuSaveAs = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pathsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.atlassianLoginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.configXmlStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.serverStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.gitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.miGitPath = new System.Windows.Forms.ToolStripMenuItem();
			this.junctionStoreToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabTitlesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.textToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.textIconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.iconToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.changeLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.whereAmIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.whereAreMySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tbGrid = new System.Windows.Forms.TabControl();
			this.tpDashboard = new System.Windows.Forms.TabPage();
			this.btnProfile = new System.Windows.Forms.Button();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnRCon = new System.Windows.Forms.Button();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnRun = new System.Windows.Forms.Button();
			this.ckMsBuild = new System.Windows.Forms.CheckBox();
			this.ckStartIis = new System.Windows.Forms.CheckBox();
			this.grpStep2 = new System.Windows.Forms.GroupBox();
			this.ckCleanJunctionEnabled = new System.Windows.Forms.CheckBox();
			this.grpCleanJunction = new System.Windows.Forms.GroupBox();
			this.rbObj = new System.Windows.Forms.RadioButton();
			this.rbBin = new System.Windows.Forms.RadioButton();
			this.rbBoth = new System.Windows.Forms.RadioButton();
			this.ckCleanAsp = new System.Windows.Forms.CheckBox();
			this.ckStopIIS = new System.Windows.Forms.CheckBox();
			this.cmbServer = new System.Windows.Forms.ComboBox();
			this.tpLinks = new System.Windows.Forms.TabPage();
			this.flpLinks = new System.Windows.Forms.FlowLayoutPanel();
			this.imageList1 = new System.Windows.Forms.ImageList(this.components);
			this.showRawJsonInLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.statusStrip1.SuspendLayout();
			this.menuStrip1.SuspendLayout();
			this.tbGrid.SuspendLayout();
			this.tpDashboard.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.grpStep2.SuspendLayout();
			this.grpCleanJunction.SuspendLayout();
			this.tpLinks.SuspendLayout();
			this.SuspendLayout();
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus});
			this.statusStrip1.Location = new System.Drawing.Point(0, 584);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(1070, 22);
			this.statusStrip1.TabIndex = 0;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// tslStatus
			// 
			this.tslStatus.Name = "tslStatus";
			this.tslStatus.Size = new System.Drawing.Size(39, 17);
			this.tslStatus.Text = "Ready";
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem,
            this.pathsToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1070, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuSave,
            this.mnuSaveAs,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// mnuSave
			// 
			this.mnuSave.Enabled = false;
			this.mnuSave.Name = "mnuSave";
			this.mnuSave.Size = new System.Drawing.Size(123, 22);
			this.mnuSave.Text = "&Save";
			// 
			// mnuSaveAs
			// 
			this.mnuSaveAs.Enabled = false;
			this.mnuSaveAs.Name = "mnuSaveAs";
			this.mnuSaveAs.Size = new System.Drawing.Size(123, 22);
			this.mnuSaveAs.Text = "Save &As...";
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
			this.exitToolStripMenuItem.Text = "E&xit";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "Edit";
			// 
			// refreshToolStripMenuItem
			// 
			this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
			this.refreshToolStripMenuItem.Size = new System.Drawing.Size(113, 22);
			this.refreshToolStripMenuItem.Text = "Refresh";
			this.refreshToolStripMenuItem.Click += new System.EventHandler(this.RefreshToolStripMenuItemClick);
			// 
			// pathsToolStripMenuItem
			// 
			this.pathsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.atlassianLoginToolStripMenuItem,
            this.configXmlStoreToolStripMenuItem,
            this.serverStoreToolStripMenuItem,
            this.gitToolStripMenuItem,
            this.junctionStoreToolStripMenuItem,
            this.tabTitlesToolStripMenuItem,
            this.showRawJsonInLogsToolStripMenuItem});
			this.pathsToolStripMenuItem.Name = "pathsToolStripMenuItem";
			this.pathsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.pathsToolStripMenuItem.Text = "Settings";
			// 
			// atlassianLoginToolStripMenuItem
			// 
			this.atlassianLoginToolStripMenuItem.Name = "atlassianLoginToolStripMenuItem";
			this.atlassianLoginToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.atlassianLoginToolStripMenuItem.Text = "Atlassian Login";
			this.atlassianLoginToolStripMenuItem.Click += new System.EventHandler(this.AtlassianLoginToolStripMenuItemClick);
			// 
			// configXmlStoreToolStripMenuItem
			// 
			this.configXmlStoreToolStripMenuItem.Name = "configXmlStoreToolStripMenuItem";
			this.configXmlStoreToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.configXmlStoreToolStripMenuItem.Text = "ConfigXmlStore";
			// 
			// serverStoreToolStripMenuItem
			// 
			this.serverStoreToolStripMenuItem.Name = "serverStoreToolStripMenuItem";
			this.serverStoreToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.serverStoreToolStripMenuItem.Text = "DatabaseServerStore";
			// 
			// gitToolStripMenuItem
			// 
			this.gitToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miGitPath});
			this.gitToolStripMenuItem.Name = "gitToolStripMenuItem";
			this.gitToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.gitToolStripMenuItem.Text = "Git.exe";
			this.gitToolStripMenuItem.Click += new System.EventHandler(this.GitToolStripMenuItemClick);
			// 
			// miGitPath
			// 
			this.miGitPath.Enabled = false;
			this.miGitPath.Name = "miGitPath";
			this.miGitPath.Size = new System.Drawing.Size(138, 22);
			this.miGitPath.Text = "CurrentPath";
			// 
			// junctionStoreToolStripMenuItem
			// 
			this.junctionStoreToolStripMenuItem.Name = "junctionStoreToolStripMenuItem";
			this.junctionStoreToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.junctionStoreToolStripMenuItem.Text = "JunctionStore";
			// 
			// tabTitlesToolStripMenuItem
			// 
			this.tabTitlesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.textToolStripMenuItem,
            this.textIconToolStripMenuItem,
            this.iconToolStripMenuItem});
			this.tabTitlesToolStripMenuItem.Name = "tabTitlesToolStripMenuItem";
			this.tabTitlesToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.tabTitlesToolStripMenuItem.Text = "Tab Titles";
			// 
			// textToolStripMenuItem
			// 
			this.textToolStripMenuItem.Name = "textToolStripMenuItem";
			this.textToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.textToolStripMenuItem.Text = "Text";
			// 
			// textIconToolStripMenuItem
			// 
			this.textIconToolStripMenuItem.Name = "textIconToolStripMenuItem";
			this.textIconToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.textIconToolStripMenuItem.Text = "Text + Icon";
			// 
			// iconToolStripMenuItem
			// 
			this.iconToolStripMenuItem.Name = "iconToolStripMenuItem";
			this.iconToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.iconToolStripMenuItem.Text = "Icon";
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeLogToolStripMenuItem,
            this.whereAmIToolStripMenuItem,
            this.whereAreMySettingsToolStripMenuItem,
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// changeLogToolStripMenuItem
			// 
			this.changeLogToolStripMenuItem.Name = "changeLogToolStripMenuItem";
			this.changeLogToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.changeLogToolStripMenuItem.Text = "ChangeLog";
			this.changeLogToolStripMenuItem.Click += new System.EventHandler(this.ChangeLogToolStripMenuItemClick);
			// 
			// whereAmIToolStripMenuItem
			// 
			this.whereAmIToolStripMenuItem.Name = "whereAmIToolStripMenuItem";
			this.whereAmIToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.whereAmIToolStripMenuItem.Text = "Where Am I?";
			this.whereAmIToolStripMenuItem.Click += new System.EventHandler(this.WhereAmIToolStripMenuItemClick);
			// 
			// whereAreMySettingsToolStripMenuItem
			// 
			this.whereAreMySettingsToolStripMenuItem.Name = "whereAreMySettingsToolStripMenuItem";
			this.whereAreMySettingsToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.whereAreMySettingsToolStripMenuItem.Text = "Where are my Settings?";
			this.whereAreMySettingsToolStripMenuItem.Click += new System.EventHandler(this.WhereAreMySettingsToolStripMenuItemClick);
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
			this.aboutToolStripMenuItem.Text = "About...";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.AboutToolStripMenuItemClick);
			// 
			// tbGrid
			// 
			this.tbGrid.Controls.Add(this.tpDashboard);
			this.tbGrid.Controls.Add(this.tpLinks);
			this.tbGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbGrid.Location = new System.Drawing.Point(0, 24);
			this.tbGrid.Name = "tbGrid";
			this.tbGrid.SelectedIndex = 0;
			this.tbGrid.Size = new System.Drawing.Size(1070, 560);
			this.tbGrid.TabIndex = 2;
			// 
			// tpDashboard
			// 
			this.tpDashboard.Controls.Add(this.btnProfile);
			this.tpDashboard.Controls.Add(this.richTextBox1);
			this.tpDashboard.Controls.Add(this.btnRCon);
			this.tpDashboard.Controls.Add(this.groupBox1);
			this.tpDashboard.Controls.Add(this.cmbServer);
			this.tpDashboard.Location = new System.Drawing.Point(4, 22);
			this.tpDashboard.Name = "tpDashboard";
			this.tpDashboard.Padding = new System.Windows.Forms.Padding(3);
			this.tpDashboard.Size = new System.Drawing.Size(1062, 534);
			this.tpDashboard.TabIndex = 0;
			this.tpDashboard.Text = "Dashboard";
			this.tpDashboard.UseVisualStyleBackColor = true;
			// 
			// btnProfile
			// 
			this.btnProfile.Location = new System.Drawing.Point(385, 8);
			this.btnProfile.Name = "btnProfile";
			this.btnProfile.Size = new System.Drawing.Size(75, 23);
			this.btnProfile.TabIndex = 20;
			this.btnProfile.Text = "Profiling";
			this.btnProfile.UseVisualStyleBackColor = true;
			this.btnProfile.Click += new System.EventHandler(this.btnProfile_Click);
			// 
			// richTextBox1
			// 
			this.richTextBox1.Location = new System.Drawing.Point(11, 35);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(449, 379);
			this.richTextBox1.TabIndex = 19;
			this.richTextBox1.Text = "";
			// 
			// btnRCon
			// 
			this.btnRCon.Location = new System.Drawing.Point(138, 6);
			this.btnRCon.Name = "btnRCon";
			this.btnRCon.Size = new System.Drawing.Size(75, 23);
			this.btnRCon.TabIndex = 18;
			this.btnRCon.Text = "RCon";
			this.btnRCon.UseVisualStyleBackColor = true;
			this.btnRCon.Click += new System.EventHandler(this.BtnRConClick);
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnRun);
			this.groupBox1.Controls.Add(this.ckMsBuild);
			this.groupBox1.Controls.Add(this.ckStartIis);
			this.groupBox1.Controls.Add(this.grpStep2);
			this.groupBox1.Controls.Add(this.ckStopIIS);
			this.groupBox1.Location = new System.Drawing.Point(789, 8);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(267, 252);
			this.groupBox1.TabIndex = 1;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "ChainAutomation";
			// 
			// btnRun
			// 
			this.btnRun.Location = new System.Drawing.Point(7, 223);
			this.btnRun.Name = "btnRun";
			this.btnRun.Size = new System.Drawing.Size(75, 23);
			this.btnRun.TabIndex = 5;
			this.btnRun.Text = "Run";
			this.btnRun.UseVisualStyleBackColor = true;
			this.btnRun.Click += new System.EventHandler(this.BtnRunClick);
			// 
			// ckMsBuild
			// 
			this.ckMsBuild.AutoSize = true;
			this.ckMsBuild.Checked = true;
			this.ckMsBuild.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckMsBuild.Location = new System.Drawing.Point(13, 149);
			this.ckMsBuild.Name = "ckMsBuild";
			this.ckMsBuild.Size = new System.Drawing.Size(63, 17);
			this.ckMsBuild.TabIndex = 4;
			this.ckMsBuild.Text = "MsBuild";
			this.ckMsBuild.UseVisualStyleBackColor = true;
			// 
			// ckStartIis
			// 
			this.ckStartIis.AutoSize = true;
			this.ckStartIis.Checked = true;
			this.ckStartIis.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckStartIis.Location = new System.Drawing.Point(13, 172);
			this.ckStartIis.Name = "ckStartIis";
			this.ckStartIis.Size = new System.Drawing.Size(64, 17);
			this.ckStartIis.TabIndex = 3;
			this.ckStartIis.Text = "Start IIS";
			this.ckStartIis.UseVisualStyleBackColor = true;
			// 
			// grpStep2
			// 
			this.grpStep2.Controls.Add(this.ckCleanJunctionEnabled);
			this.grpStep2.Controls.Add(this.grpCleanJunction);
			this.grpStep2.Controls.Add(this.ckCleanAsp);
			this.grpStep2.Location = new System.Drawing.Point(7, 43);
			this.grpStep2.Name = "grpStep2";
			this.grpStep2.Size = new System.Drawing.Size(231, 100);
			this.grpStep2.TabIndex = 2;
			this.grpStep2.TabStop = false;
			this.grpStep2.Text = "Step 2";
			// 
			// ckCleanJunctionEnabled
			// 
			this.ckCleanJunctionEnabled.AutoSize = true;
			this.ckCleanJunctionEnabled.Checked = true;
			this.ckCleanJunctionEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckCleanJunctionEnabled.Location = new System.Drawing.Point(6, 62);
			this.ckCleanJunctionEnabled.Name = "ckCleanJunctionEnabled";
			this.ckCleanJunctionEnabled.Size = new System.Drawing.Size(96, 17);
			this.ckCleanJunctionEnabled.TabIndex = 4;
			this.ckCleanJunctionEnabled.Text = "Clean Junction";
			this.ckCleanJunctionEnabled.UseVisualStyleBackColor = true;
			this.ckCleanJunctionEnabled.CheckedChanged += new System.EventHandler(this.CheckBox1CheckedChanged);
			// 
			// grpCleanJunction
			// 
			this.grpCleanJunction.Controls.Add(this.rbObj);
			this.grpCleanJunction.Controls.Add(this.rbBin);
			this.grpCleanJunction.Controls.Add(this.rbBoth);
			this.grpCleanJunction.Location = new System.Drawing.Point(108, 15);
			this.grpCleanJunction.Name = "grpCleanJunction";
			this.grpCleanJunction.Size = new System.Drawing.Size(111, 75);
			this.grpCleanJunction.TabIndex = 3;
			this.grpCleanJunction.TabStop = false;
			this.grpCleanJunction.Text = "Junction Cleans";
			// 
			// rbObj
			// 
			this.rbObj.AutoSize = true;
			this.rbObj.Location = new System.Drawing.Point(53, 42);
			this.rbObj.Name = "rbObj";
			this.rbObj.Size = new System.Drawing.Size(41, 17);
			this.rbObj.TabIndex = 2;
			this.rbObj.TabStop = true;
			this.rbObj.Text = "Obj";
			this.rbObj.UseVisualStyleBackColor = true;
			// 
			// rbBin
			// 
			this.rbBin.AutoSize = true;
			this.rbBin.Location = new System.Drawing.Point(7, 42);
			this.rbBin.Name = "rbBin";
			this.rbBin.Size = new System.Drawing.Size(40, 17);
			this.rbBin.TabIndex = 1;
			this.rbBin.TabStop = true;
			this.rbBin.Text = "Bin";
			this.rbBin.UseVisualStyleBackColor = true;
			// 
			// rbBoth
			// 
			this.rbBoth.AutoSize = true;
			this.rbBoth.Checked = true;
			this.rbBoth.Location = new System.Drawing.Point(7, 19);
			this.rbBoth.Name = "rbBoth";
			this.rbBoth.Size = new System.Drawing.Size(47, 17);
			this.rbBoth.TabIndex = 0;
			this.rbBoth.TabStop = true;
			this.rbBoth.Text = "Both";
			this.rbBoth.UseVisualStyleBackColor = true;
			// 
			// ckCleanAsp
			// 
			this.ckCleanAsp.AutoSize = true;
			this.ckCleanAsp.Checked = true;
			this.ckCleanAsp.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckCleanAsp.Location = new System.Drawing.Point(6, 38);
			this.ckCleanAsp.Name = "ckCleanAsp";
			this.ckCleanAsp.Size = new System.Drawing.Size(71, 17);
			this.ckCleanAsp.TabIndex = 1;
			this.ckCleanAsp.Text = "CleanAsp";
			this.ckCleanAsp.UseVisualStyleBackColor = true;
			// 
			// ckStopIIS
			// 
			this.ckStopIIS.AutoSize = true;
			this.ckStopIIS.Checked = true;
			this.ckStopIIS.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckStopIIS.Location = new System.Drawing.Point(7, 20);
			this.ckStopIIS.Name = "ckStopIIS";
			this.ckStopIIS.Size = new System.Drawing.Size(64, 17);
			this.ckStopIIS.TabIndex = 0;
			this.ckStopIIS.Text = "Stop IIS";
			this.ckStopIIS.UseVisualStyleBackColor = true;
			// 
			// cmbServer
			// 
			this.cmbServer.FormattingEnabled = true;
			this.cmbServer.Items.AddRange(new object[] {
            "localhost",
            "dev1",
            "pqoappl1",
            "pqoweb1"});
			this.cmbServer.Location = new System.Drawing.Point(11, 8);
			this.cmbServer.Name = "cmbServer";
			this.cmbServer.Size = new System.Drawing.Size(121, 21);
			this.cmbServer.TabIndex = 17;
			this.cmbServer.Text = "localhost";
			// 
			// tpLinks
			// 
			this.tpLinks.Controls.Add(this.flpLinks);
			this.tpLinks.Location = new System.Drawing.Point(4, 22);
			this.tpLinks.Name = "tpLinks";
			this.tpLinks.Padding = new System.Windows.Forms.Padding(3);
			this.tpLinks.Size = new System.Drawing.Size(1062, 534);
			this.tpLinks.TabIndex = 6;
			this.tpLinks.Text = "Links";
			this.tpLinks.UseVisualStyleBackColor = true;
			// 
			// flpLinks
			// 
			this.flpLinks.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpLinks.Location = new System.Drawing.Point(3, 3);
			this.flpLinks.Name = "flpLinks";
			this.flpLinks.Size = new System.Drawing.Size(1056, 528);
			this.flpLinks.TabIndex = 0;
			// 
			// imageList1
			// 
			this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
			this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
			this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
			// 
			// showRawJsonInLogsToolStripMenuItem
			// 
			this.showRawJsonInLogsToolStripMenuItem.Name = "showRawJsonInLogsToolStripMenuItem";
			this.showRawJsonInLogsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
			this.showRawJsonInLogsToolStripMenuItem.Text = "Show Raw Json in Logs";
			this.showRawJsonInLogsToolStripMenuItem.Click += new System.EventHandler(this.showRawJsonInLogsToolStripMenuItem_Click);
			// 
			// UcMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tbGrid);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.menuStrip1);
			this.Name = "UcMain";
			this.Size = new System.Drawing.Size(1070, 606);
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.tbGrid.ResumeLayout(false);
			this.tpDashboard.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.grpStep2.ResumeLayout(false);
			this.grpStep2.PerformLayout();
			this.grpCleanJunction.ResumeLayout(false);
			this.grpCleanJunction.PerformLayout();
			this.tpLinks.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel tslStatus;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.TabControl tbGrid;
		private System.Windows.Forms.TabPage tpDashboard;
		private System.Windows.Forms.TabPage tpLinks;
		private System.Windows.Forms.FlowLayoutPanel flpLinks;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pathsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem gitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem miGitPath;
		private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem mnuSave;
		private System.Windows.Forms.ToolStripMenuItem mnuSaveAs;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnRun;
		private System.Windows.Forms.CheckBox ckMsBuild;
		private System.Windows.Forms.CheckBox ckStartIis;
		private System.Windows.Forms.GroupBox grpStep2;
		private System.Windows.Forms.GroupBox grpCleanJunction;
		private System.Windows.Forms.RadioButton rbObj;
		private System.Windows.Forms.RadioButton rbBin;
		private System.Windows.Forms.RadioButton rbBoth;
		private System.Windows.Forms.CheckBox ckCleanAsp;
		private System.Windows.Forms.CheckBox ckStopIIS;
		private System.Windows.Forms.CheckBox ckCleanJunctionEnabled;
		private System.Windows.Forms.ToolStripMenuItem serverStoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem junctionStoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem atlassianLoginToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem configXmlStoreToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem changeLogToolStripMenuItem;
		private System.Windows.Forms.Button btnRCon;
		private System.Windows.Forms.ComboBox cmbServer;
		private System.Windows.Forms.ToolStripMenuItem whereAmIToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem whereAreMySettingsToolStripMenuItem;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnProfile;
		private System.Windows.Forms.ToolStripMenuItem tabTitlesToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem textToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem textIconToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem iconToolStripMenuItem;
		private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.ToolStripMenuItem showRawJsonInLogsToolStripMenuItem;
	}
}

