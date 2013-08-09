namespace DeveloperConfigurationManager.Controls
{
	partial class UcConfigXml
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
			this.cbAddress = new System.Windows.Forms.ComboBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tbTree = new System.Windows.Forms.TabPage();
			this.tvXml = new System.Windows.Forms.TreeView();
			this.cbFilterType = new System.Windows.Forms.ComboBox();
			this.txtFilterCriteria = new System.Windows.Forms.TextBox();
			this.tbText = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnPopulate = new System.Windows.Forms.Button();
			this.btnClear = new System.Windows.Forms.Button();
			this.btnLaunch = new System.Windows.Forms.Button();
			this.tabControl1.SuspendLayout();
			this.tbTree.SuspendLayout();
			this.tbText.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbAddress
			// 
			this.cbAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbAddress.FormattingEnabled = true;
			this.cbAddress.Location = new System.Drawing.Point(3, 3);
			this.cbAddress.Name = "cbAddress";
			this.cbAddress.Size = new System.Drawing.Size(426, 21);
			this.cbAddress.TabIndex = 1;
			this.cbAddress.Text = "http://localhost/ConfigurationService/configuration.ashx";
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tbTree);
			this.tabControl1.Controls.Add(this.tbText);
			this.tabControl1.Location = new System.Drawing.Point(7, 55);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(802, 312);
			this.tabControl1.TabIndex = 16;
			// 
			// tbTree
			// 
			this.tbTree.Controls.Add(this.tvXml);
			this.tbTree.Controls.Add(this.cbFilterType);
			this.tbTree.Controls.Add(this.txtFilterCriteria);
			this.tbTree.Location = new System.Drawing.Point(4, 22);
			this.tbTree.Name = "tbTree";
			this.tbTree.Padding = new System.Windows.Forms.Padding(3);
			this.tbTree.Size = new System.Drawing.Size(794, 286);
			this.tbTree.TabIndex = 1;
			this.tbTree.Text = "Tree";
			this.tbTree.UseVisualStyleBackColor = true;
			// 
			// tvXml
			// 
			this.tvXml.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tvXml.Location = new System.Drawing.Point(6, 30);
			this.tvXml.Name = "tvXml";
			this.tvXml.Size = new System.Drawing.Size(782, 250);
			this.tvXml.TabIndex = 4;
			this.tvXml.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.TvXmlBeforeExpand);
			// 
			// cbFilterType
			// 
			this.cbFilterType.FormattingEnabled = true;
			this.cbFilterType.Location = new System.Drawing.Point(6, 3);
			this.cbFilterType.Name = "cbFilterType";
			this.cbFilterType.Size = new System.Drawing.Size(143, 21);
			this.cbFilterType.TabIndex = 3;
			// 
			// txtFilterCriteria
			// 
			this.txtFilterCriteria.Location = new System.Drawing.Point(158, 3);
			this.txtFilterCriteria.Name = "txtFilterCriteria";
			this.txtFilterCriteria.Size = new System.Drawing.Size(135, 20);
			this.txtFilterCriteria.TabIndex = 2;
			// 
			// tbText
			// 
			this.tbText.Controls.Add(this.richTextBox1);
			this.tbText.Location = new System.Drawing.Point(4, 22);
			this.tbText.Name = "tbText";
			this.tbText.Padding = new System.Windows.Forms.Padding(3);
			this.tbText.Size = new System.Drawing.Size(794, 286);
			this.tbText.TabIndex = 0;
			this.tbText.Text = "Log";
			this.tbText.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(788, 280);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// btnPopulate
			// 
			this.btnPopulate.Location = new System.Drawing.Point(734, 3);
			this.btnPopulate.Name = "btnPopulate";
			this.btnPopulate.Size = new System.Drawing.Size(75, 23);
			this.btnPopulate.TabIndex = 17;
			this.btnPopulate.Text = "Submit";
			this.btnPopulate.UseVisualStyleBackColor = true;
			this.btnPopulate.Click += new System.EventHandler(this.BtnPopulateClick);
			// 
			// btnClear
			// 
			this.btnClear.Location = new System.Drawing.Point(572, 3);
			this.btnClear.Name = "btnClear";
			this.btnClear.Size = new System.Drawing.Size(75, 23);
			this.btnClear.TabIndex = 18;
			this.btnClear.Text = "Clear";
			this.btnClear.UseVisualStyleBackColor = true;
			this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
			// 
			// btnLaunch
			// 
			this.btnLaunch.Location = new System.Drawing.Point(653, 3);
			this.btnLaunch.Name = "btnLaunch";
			this.btnLaunch.Size = new System.Drawing.Size(75, 23);
			this.btnLaunch.TabIndex = 19;
			this.btnLaunch.Text = "Launch";
			this.btnLaunch.UseVisualStyleBackColor = true;
			this.btnLaunch.Click += new System.EventHandler(this.BtnLaunchClick);
			// 
			// UcConfigXml
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnLaunch);
			this.Controls.Add(this.btnClear);
			this.Controls.Add(this.btnPopulate);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cbAddress);
			this.Name = "UcConfigXml";
			this.Size = new System.Drawing.Size(812, 370);
			this.tabControl1.ResumeLayout(false);
			this.tbTree.ResumeLayout(false);
			this.tbTree.PerformLayout();
			this.tbText.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.ComboBox cbAddress;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tbTree;
		private System.Windows.Forms.ComboBox cbFilterType;
		private System.Windows.Forms.TextBox txtFilterCriteria;
		private System.Windows.Forms.TabPage tbText;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnPopulate;
		private System.Windows.Forms.TreeView tvXml;
		private System.Windows.Forms.Button btnClear;
		private System.Windows.Forms.Button btnLaunch;
	}
}
