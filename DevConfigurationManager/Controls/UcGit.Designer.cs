namespace DeveloperConfigurationManager.Controls
{
	partial class UcGit
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
            this.btnStatus = new System.Windows.Forms.Button();
            this.btnUnpushed = new System.Windows.Forms.Button();
            this.btnPush = new System.Windows.Forms.Button();
            this.btnPull = new System.Windows.Forms.Button();
            this.btnPullMaster = new System.Windows.Forms.Button();
            this.btnBash = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnBashRc = new System.Windows.Forms.Button();
            this.btnRefLog = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.dgJunction = new System.Windows.Forms.DataGridView();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.rtLog = new System.Windows.Forms.RichTextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgJunction)).BeginInit();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStatus
            // 
            this.btnStatus.Location = new System.Drawing.Point(3, 3);
            this.btnStatus.Name = "btnStatus";
            this.btnStatus.Size = new System.Drawing.Size(75, 23);
            this.btnStatus.TabIndex = 0;
            this.btnStatus.Text = "Status";
            this.btnStatus.UseVisualStyleBackColor = true;
            this.btnStatus.Click += new System.EventHandler(this.BtnStatusClick);
            // 
            // btnUnpushed
            // 
            this.btnUnpushed.Location = new System.Drawing.Point(84, 3);
            this.btnUnpushed.Name = "btnUnpushed";
            this.btnUnpushed.Size = new System.Drawing.Size(75, 23);
            this.btnUnpushed.TabIndex = 2;
            this.btnUnpushed.Text = "Unpushed";
            this.btnUnpushed.UseVisualStyleBackColor = true;
            this.btnUnpushed.Click += new System.EventHandler(this.BtnUnpushedClick);
            // 
            // btnPush
            // 
            this.btnPush.Location = new System.Drawing.Point(165, 3);
            this.btnPush.Name = "btnPush";
            this.btnPush.Size = new System.Drawing.Size(75, 23);
            this.btnPush.TabIndex = 3;
            this.btnPush.Text = "Push";
            this.btnPush.UseVisualStyleBackColor = true;
            this.btnPush.Click += new System.EventHandler(this.BtnPushClick);
            // 
            // btnPull
            // 
            this.btnPull.Location = new System.Drawing.Point(246, 3);
            this.btnPull.Name = "btnPull";
            this.btnPull.Size = new System.Drawing.Size(75, 23);
            this.btnPull.TabIndex = 4;
            this.btnPull.Text = "Pull Branch";
            this.btnPull.UseVisualStyleBackColor = true;
            this.btnPull.Click += new System.EventHandler(this.BtnPullClick);
            // 
            // btnPullMaster
            // 
            this.btnPullMaster.Location = new System.Drawing.Point(327, 3);
            this.btnPullMaster.Name = "btnPullMaster";
            this.btnPullMaster.Size = new System.Drawing.Size(75, 23);
            this.btnPullMaster.TabIndex = 5;
            this.btnPullMaster.Text = "Pull Master";
            this.btnPullMaster.UseVisualStyleBackColor = true;
            this.btnPullMaster.Click += new System.EventHandler(this.BtnPullMasterClick);
            // 
            // btnBash
            // 
            this.btnBash.Location = new System.Drawing.Point(408, 3);
            this.btnBash.Name = "btnBash";
            this.btnBash.Size = new System.Drawing.Size(75, 23);
            this.btnBash.TabIndex = 6;
            this.btnBash.Text = "Bash";
            this.btnBash.UseVisualStyleBackColor = true;
            this.btnBash.Click += new System.EventHandler(this.BtnBashClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(489, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnBashRc
            // 
            this.btnBashRc.Location = new System.Drawing.Point(570, 3);
            this.btnBashRc.Name = "btnBashRc";
            this.btnBashRc.Size = new System.Drawing.Size(75, 23);
            this.btnBashRc.TabIndex = 8;
            this.btnBashRc.Text = ".bashrc";
            this.btnBashRc.UseVisualStyleBackColor = true;
            this.btnBashRc.Click += new System.EventHandler(this.BtnBashRcClick);
            // 
            // btnRefLog
            // 
            this.btnRefLog.Location = new System.Drawing.Point(651, 3);
            this.btnRefLog.Name = "btnRefLog";
            this.btnRefLog.Size = new System.Drawing.Size(75, 23);
            this.btnRefLog.TabIndex = 9;
            this.btnRefLog.Text = "RefLog";
            this.btnRefLog.UseVisualStyleBackColor = true;
            this.btnRefLog.Click += new System.EventHandler(this.btnRefLog_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(3, 32);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(895, 367);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgJunction);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(887, 341);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Grid";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // dgJunction
            // 
            this.dgJunction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dgJunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgJunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgJunction.Location = new System.Drawing.Point(3, 3);
            this.dgJunction.Name = "dgJunction";
            this.dgJunction.Size = new System.Drawing.Size(881, 335);
            this.dgJunction.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.rtLog);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(887, 341);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // rtLog
            // 
            this.rtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtLog.Location = new System.Drawing.Point(3, 3);
            this.rtLog.Name = "rtLog";
            this.rtLog.Size = new System.Drawing.Size(881, 335);
            this.rtLog.TabIndex = 9;
            this.rtLog.Text = "";
            // 
            // UcGit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnRefLog);
            this.Controls.Add(this.btnBashRc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnBash);
            this.Controls.Add(this.btnPullMaster);
            this.Controls.Add(this.btnPull);
            this.Controls.Add(this.btnPush);
            this.Controls.Add(this.btnUnpushed);
            this.Controls.Add(this.btnStatus);
            this.Name = "UcGit";
            this.Size = new System.Drawing.Size(898, 399);
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgJunction)).EndInit();
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btnStatus;
		private System.Windows.Forms.Button btnUnpushed;
		private System.Windows.Forms.Button btnPush;
		private System.Windows.Forms.Button btnPull;
		private System.Windows.Forms.Button btnPullMaster;
		private System.Windows.Forms.Button btnBash;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnBashRc;
        private System.Windows.Forms.Button btnRefLog;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.DataGridView dgJunction;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.RichTextBox rtLog;
	}
}
