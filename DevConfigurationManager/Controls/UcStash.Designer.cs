namespace DeveloperConfigurationManager.Controls
{
	partial class UcStash
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
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tbGrid = new System.Windows.Forms.TabPage();
			this.dgMergeReviews = new System.Windows.Forms.DataGridView();
			this.tbText = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.cbName = new System.Windows.Forms.ComboBox();
			this.btnPopulate = new System.Windows.Forms.Button();
			this.cbAddress = new System.Windows.Forms.ComboBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tbGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgMergeReviews)).BeginInit();
			this.tbText.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tbGrid);
			this.tabControl1.Controls.Add(this.tbText);
			this.tabControl1.Location = new System.Drawing.Point(3, 47);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(870, 287);
			this.tabControl1.TabIndex = 19;
			// 
			// tbGrid
			// 
			this.tbGrid.Controls.Add(this.dgMergeReviews);
			this.tbGrid.Location = new System.Drawing.Point(4, 22);
			this.tbGrid.Name = "tbGrid";
			this.tbGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tbGrid.Size = new System.Drawing.Size(862, 261);
			this.tbGrid.TabIndex = 1;
			this.tbGrid.Text = "Merges";
			this.tbGrid.UseVisualStyleBackColor = true;
			// 
			// dgMergeReviews
			// 
			this.dgMergeReviews.AllowUserToOrderColumns = true;
			this.dgMergeReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgMergeReviews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgMergeReviews.Location = new System.Drawing.Point(3, 3);
			this.dgMergeReviews.Name = "dgMergeReviews";
			this.dgMergeReviews.Size = new System.Drawing.Size(856, 255);
			this.dgMergeReviews.TabIndex = 4;
			this.dgMergeReviews.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1CellContentClick);
			this.dgMergeReviews.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1CellFormatting);
			// 
			// tbText
			// 
			this.tbText.Controls.Add(this.richTextBox1);
			this.tbText.Location = new System.Drawing.Point(4, 22);
			this.tbText.Name = "tbText";
			this.tbText.Padding = new System.Windows.Forms.Padding(3);
			this.tbText.Size = new System.Drawing.Size(862, 261);
			this.tbText.TabIndex = 0;
			this.tbText.Text = "Log";
			this.tbText.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(856, 255);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// cbName
			// 
			this.cbName.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbName.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbName.Enabled = false;
			this.cbName.FormattingEnabled = true;
			this.cbName.Location = new System.Drawing.Point(3, 3);
			this.cbName.Name = "cbName";
			this.cbName.Size = new System.Drawing.Size(85, 21);
			this.cbName.TabIndex = 18;
			// 
			// btnPopulate
			// 
			this.btnPopulate.Location = new System.Drawing.Point(798, 3);
			this.btnPopulate.Name = "btnPopulate";
			this.btnPopulate.Size = new System.Drawing.Size(75, 23);
			this.btnPopulate.TabIndex = 20;
			this.btnPopulate.Text = "Refresh";
			this.btnPopulate.UseVisualStyleBackColor = true;
			this.btnPopulate.Click += new System.EventHandler(this.BtnPopulateClick);
			// 
			// cbAddress
			// 
			this.cbAddress.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbAddress.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbAddress.Enabled = false;
			this.cbAddress.FormattingEnabled = true;
			this.cbAddress.Location = new System.Drawing.Point(94, 3);
			this.cbAddress.Name = "cbAddress";
			this.cbAddress.Size = new System.Drawing.Size(698, 21);
			this.cbAddress.TabIndex = 21;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(693, 27);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 13);
			this.label1.TabIndex = 22;
			this.label1.Text = "Warning no credentials stored/found";
			// 
			// UcStash
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.cbAddress);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.cbName);
			this.Controls.Add(this.btnPopulate);
			this.Name = "UcStash";
			this.Size = new System.Drawing.Size(876, 337);
			this.Load += new System.EventHandler(this.UcStashLoad);
			this.tabControl1.ResumeLayout(false);
			this.tbGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgMergeReviews)).EndInit();
			this.tbText.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tbGrid;
		private System.Windows.Forms.TabPage tbText;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.ComboBox cbName;
		private System.Windows.Forms.Button btnPopulate;
		private System.Windows.Forms.DataGridView dgMergeReviews;
		private System.Windows.Forms.ComboBox cbAddress;
		private System.Windows.Forms.Label label1;
	}
}
