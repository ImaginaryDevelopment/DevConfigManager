namespace DeveloperConfigurationManager.Controls
{
	partial class UcJunction
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
            this.dgJunction = new System.Windows.Forms.DataGridView();
            this.cbJunctionTarget = new System.Windows.Forms.ComboBox();
            this.btnCleanBoth = new System.Windows.Forms.Button();
            this.btnCleanBin = new System.Windows.Forms.Button();
            this.btnObj = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbJunctionPath = new System.Windows.Forms.ComboBox();
            this.txtJunctionRelativeGitPath = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgJunction)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgJunction
            // 
            this.dgJunction.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCellsExceptHeader;
            this.dgJunction.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgJunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgJunction.Location = new System.Drawing.Point(3, 3);
            this.dgJunction.Name = "dgJunction";
            this.dgJunction.Size = new System.Drawing.Size(629, 195);
            this.dgJunction.TabIndex = 11;
            this.dgJunction.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1CellFormatting);
            // 
            // cbJunctionTarget
            // 
            this.cbJunctionTarget.FormattingEnabled = true;
            this.cbJunctionTarget.Location = new System.Drawing.Point(3, 10);
            this.cbJunctionTarget.Name = "cbJunctionTarget";
            this.cbJunctionTarget.Size = new System.Drawing.Size(156, 21);
            this.cbJunctionTarget.TabIndex = 10;
            // 
            // btnCleanBoth
            // 
            this.btnCleanBoth.Location = new System.Drawing.Point(168, 8);
            this.btnCleanBoth.Name = "btnCleanBoth";
            this.btnCleanBoth.Size = new System.Drawing.Size(102, 23);
            this.btnCleanBoth.TabIndex = 9;
            this.btnCleanBoth.Text = "Clean Bin+Obj folders";
            this.btnCleanBoth.UseVisualStyleBackColor = true;
            this.btnCleanBoth.Click += new System.EventHandler(this.Button1Click);
            // 
            // btnCleanBin
            // 
            this.btnCleanBin.Location = new System.Drawing.Point(84, 36);
            this.btnCleanBin.Name = "btnCleanBin";
            this.btnCleanBin.Size = new System.Drawing.Size(75, 23);
            this.btnCleanBin.TabIndex = 8;
            this.btnCleanBin.Text = "Clean Bin";
            this.btnCleanBin.UseVisualStyleBackColor = true;
            this.btnCleanBin.Click += new System.EventHandler(this.BtnCleanBinClick);
            // 
            // btnObj
            // 
            this.btnObj.Location = new System.Drawing.Point(3, 36);
            this.btnObj.Name = "btnObj";
            this.btnObj.Size = new System.Drawing.Size(75, 23);
            this.btnObj.TabIndex = 7;
            this.btnObj.Text = "Clean Obj";
            this.btnObj.UseVisualStyleBackColor = true;
            this.btnObj.Click += new System.EventHandler(this.BtnObjClick);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(350, 36);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 12;
            this.btnBrowse.Text = "BrowseTo";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.BtnBrowseClick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Location = new System.Drawing.Point(3, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(643, 227);
            this.tabControl1.TabIndex = 14;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.dgJunction);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(635, 201);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Grid";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.richTextBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(635, 201);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Log";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(3, 3);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(629, 195);
            this.richTextBox1.TabIndex = 9;
            this.richTextBox1.Text = "";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(571, 8);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 15;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbJunctionPath
            // 
            this.cbJunctionPath.FormattingEnabled = true;
            this.cbJunctionPath.Location = new System.Drawing.Point(276, 10);
            this.cbJunctionPath.Name = "cbJunctionPath";
            this.cbJunctionPath.Size = new System.Drawing.Size(289, 21);
            this.cbJunctionPath.TabIndex = 16;
            this.cbJunctionPath.Text = "C:\\Microsoft .Net 3.5 Framework\\Mortgageflex products";
            // 
            // txtJunctionRelativeGitPath
            // 
            this.txtJunctionRelativeGitPath.Location = new System.Drawing.Point(539, 39);
            this.txtJunctionRelativeGitPath.Name = "txtJunctionRelativeGitPath";
            this.txtJunctionRelativeGitPath.Size = new System.Drawing.Size(100, 20);
            this.txtJunctionRelativeGitPath.TabIndex = 17;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(458, 36);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 18;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // UcJunction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtJunctionRelativeGitPath);
            this.Controls.Add(this.cbJunctionPath);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.cbJunctionTarget);
            this.Controls.Add(this.btnCleanBoth);
            this.Controls.Add(this.btnCleanBin);
            this.Controls.Add(this.btnObj);
            this.Name = "UcJunction";
            this.Size = new System.Drawing.Size(649, 295);
            ((System.ComponentModel.ISupportInitialize)(this.dgJunction)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.DataGridView dgJunction;
		private System.Windows.Forms.ComboBox cbJunctionTarget;
		private System.Windows.Forms.Button btnCleanBoth;
		private System.Windows.Forms.Button btnCleanBin;
		private System.Windows.Forms.Button btnObj;
		private System.Windows.Forms.Button btnBrowse;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cbJunctionPath;
        private System.Windows.Forms.TextBox txtJunctionRelativeGitPath;
        private System.Windows.Forms.Button btnRefresh;

	}
}
