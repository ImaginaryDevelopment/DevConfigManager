namespace DeveloperConfigurationManager.Controls
{
	partial class UcEnvironment
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
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tbGrid = new System.Windows.Forms.TabPage();
			this.dgEnvironment = new System.Windows.Forms.DataGridView();
			this.tbLog = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.cbReadLevel = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.cbWriteLevel = new System.Windows.Forms.ComboBox();
			this.tabControl1.SuspendLayout();
			this.tbGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgEnvironment)).BeginInit();
			this.tbLog.SuspendLayout();
			this.SuspendLayout();
			// 
			// textBox1
			// 
			this.textBox1.Location = new System.Drawing.Point(60, 3);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(100, 20);
			this.textBox1.TabIndex = 2;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.tbGrid);
			this.tabControl1.Controls.Add(this.tbLog);
			this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new System.Drawing.Point(0, 0);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(707, 291);
			this.tabControl1.TabIndex = 14;
			// 
			// tbGrid
			// 
			this.tbGrid.Controls.Add(this.label2);
			this.tbGrid.Controls.Add(this.cbWriteLevel);
			this.tbGrid.Controls.Add(this.label1);
			this.tbGrid.Controls.Add(this.cbReadLevel);
			this.tbGrid.Controls.Add(this.dgEnvironment);
			this.tbGrid.Controls.Add(this.textBox1);
			this.tbGrid.Location = new System.Drawing.Point(4, 22);
			this.tbGrid.Name = "tbGrid";
			this.tbGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tbGrid.Size = new System.Drawing.Size(699, 265);
			this.tbGrid.TabIndex = 1;
			this.tbGrid.Text = "Grid";
			this.tbGrid.UseVisualStyleBackColor = true;
			// 
			// dgEnvironment
			// 
			this.dgEnvironment.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.dgEnvironment.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgEnvironment.Location = new System.Drawing.Point(6, 29);
			this.dgEnvironment.Name = "dgEnvironment";
			this.dgEnvironment.Size = new System.Drawing.Size(687, 230);
			this.dgEnvironment.TabIndex = 0;
			this.dgEnvironment.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgEnvironment_CellFormatting);
			// 
			// tbLog
			// 
			this.tbLog.Controls.Add(this.richTextBox1);
			this.tbLog.Location = new System.Drawing.Point(4, 22);
			this.tbLog.Name = "tbLog";
			this.tbLog.Padding = new System.Windows.Forms.Padding(3);
			this.tbLog.Size = new System.Drawing.Size(699, 265);
			this.tbLog.TabIndex = 0;
			this.tbLog.Text = "Log";
			this.tbLog.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(693, 259);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// cbReadLevel
			// 
			this.cbReadLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbReadLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbReadLevel.FormattingEnabled = true;
			this.cbReadLevel.Location = new System.Drawing.Point(572, 3);
			this.cbReadLevel.Name = "cbReadLevel";
			this.cbReadLevel.Size = new System.Drawing.Size(121, 21);
			this.cbReadLevel.TabIndex = 3;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(504, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(62, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Read Level";
			// 
			// label2
			// 
			this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(309, 6);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(61, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Write Level";
			// 
			// cbWriteLevel
			// 
			this.cbWriteLevel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cbWriteLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWriteLevel.FormattingEnabled = true;
			this.cbWriteLevel.Location = new System.Drawing.Point(377, 3);
			this.cbWriteLevel.Name = "cbWriteLevel";
			this.cbWriteLevel.Size = new System.Drawing.Size(121, 21);
			this.cbWriteLevel.TabIndex = 5;
			// 
			// UcEnvironment
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabControl1);
			this.Name = "UcEnvironment";
			this.Size = new System.Drawing.Size(707, 291);
			this.tabControl1.ResumeLayout(false);
			this.tbGrid.ResumeLayout(false);
			this.tbGrid.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgEnvironment)).EndInit();
			this.tbLog.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tbLog;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.TabPage tbGrid;
		private System.Windows.Forms.DataGridView dgEnvironment;
		private System.Windows.Forms.ComboBox cbReadLevel;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox cbWriteLevel;
	}
}
