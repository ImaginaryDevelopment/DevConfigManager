namespace DeveloperConfigurationManager.Controls
{
	partial class UcCrucible
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
			this.tpGrid = new System.Windows.Forms.TabPage();
			this.dgReviews = new System.Windows.Forms.DataGridView();
			this.mnuGrid = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.reviewersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.commentsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tpReviewers = new System.Windows.Forms.TabPage();
			this.dgReviewers = new System.Windows.Forms.DataGridView();
			this.tpComments = new System.Windows.Forms.TabPage();
			this.dgComments = new System.Windows.Forms.DataGridView();
			this.tpText = new System.Windows.Forms.TabPage();
			this.richTextBox1 = new System.Windows.Forms.RichTextBox();
			this.btnPopulate = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.tpGrid.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgReviews)).BeginInit();
			this.mnuGrid.SuspendLayout();
			this.tpReviewers.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgReviewers)).BeginInit();
			this.tpComments.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dgComments)).BeginInit();
			this.tpText.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabControl1
			// 
			this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.tabControl1.Controls.Add(this.tpGrid);
			this.tabControl1.Controls.Add(this.tpReviewers);
			this.tabControl1.Controls.Add(this.tpComments);
			this.tabControl1.Controls.Add(this.tpText);
			this.tabControl1.Location = new System.Drawing.Point(3, 32);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(1091, 299);
			this.tabControl1.TabIndex = 20;
			this.tabControl1.Selecting += new System.Windows.Forms.TabControlCancelEventHandler(this.TabControl1Selecting);
			// 
			// tpGrid
			// 
			this.tpGrid.Controls.Add(this.dgReviews);
			this.tpGrid.Location = new System.Drawing.Point(4, 22);
			this.tpGrid.Name = "tpGrid";
			this.tpGrid.Padding = new System.Windows.Forms.Padding(3);
			this.tpGrid.Size = new System.Drawing.Size(1083, 273);
			this.tpGrid.TabIndex = 1;
			this.tpGrid.Text = "Grid";
			this.tpGrid.UseVisualStyleBackColor = true;
			// 
			// dgReviews
			// 
			this.dgReviews.AllowUserToOrderColumns = true;
			this.dgReviews.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgReviews.ContextMenuStrip = this.mnuGrid;
			this.dgReviews.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgReviews.Location = new System.Drawing.Point(3, 3);
			this.dgReviews.Name = "dgReviews";
			this.dgReviews.Size = new System.Drawing.Size(1077, 267);
			this.dgReviews.TabIndex = 4;
			this.dgReviews.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DataGridView1CellFormatting);
			this.dgReviews.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DgReviewsMouseDown);
			// 
			// mnuGrid
			// 
			this.mnuGrid.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.reviewersToolStripMenuItem,
            this.commentsToolStripMenuItem});
			this.mnuGrid.Name = "mnuGrid";
			this.mnuGrid.Size = new System.Drawing.Size(134, 48);
			this.mnuGrid.Opening += new System.ComponentModel.CancelEventHandler(this.MnuGridOpening);
			// 
			// reviewersToolStripMenuItem
			// 
			this.reviewersToolStripMenuItem.Name = "reviewersToolStripMenuItem";
			this.reviewersToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.reviewersToolStripMenuItem.Text = "Reviewers";
			this.reviewersToolStripMenuItem.Click += new System.EventHandler(this.ReviewersToolStripMenuItemClick);
			// 
			// commentsToolStripMenuItem
			// 
			this.commentsToolStripMenuItem.Name = "commentsToolStripMenuItem";
			this.commentsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.commentsToolStripMenuItem.Text = "Comments";
			this.commentsToolStripMenuItem.Click += new System.EventHandler(this.CommentsToolStripMenuItemClick);
			// 
			// tpReviewers
			// 
			this.tpReviewers.Controls.Add(this.dgReviewers);
			this.tpReviewers.Location = new System.Drawing.Point(4, 22);
			this.tpReviewers.Name = "tpReviewers";
			this.tpReviewers.Padding = new System.Windows.Forms.Padding(3);
			this.tpReviewers.Size = new System.Drawing.Size(1083, 273);
			this.tpReviewers.TabIndex = 2;
			this.tpReviewers.Text = "Reviewers";
			this.tpReviewers.UseVisualStyleBackColor = true;
			// 
			// dgReviewers
			// 
			this.dgReviewers.AllowUserToOrderColumns = true;
			this.dgReviewers.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgReviewers.ContextMenuStrip = this.mnuGrid;
			this.dgReviewers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgReviewers.Location = new System.Drawing.Point(3, 3);
			this.dgReviewers.Name = "dgReviewers";
			this.dgReviewers.Size = new System.Drawing.Size(1077, 267);
			this.dgReviewers.TabIndex = 5;
			this.dgReviewers.DataSourceChanged += new System.EventHandler(this.DgReviewersDataSourceChanged);
			// 
			// tpComments
			// 
			this.tpComments.Controls.Add(this.dgComments);
			this.tpComments.Location = new System.Drawing.Point(4, 22);
			this.tpComments.Name = "tpComments";
			this.tpComments.Size = new System.Drawing.Size(1083, 273);
			this.tpComments.TabIndex = 3;
			this.tpComments.Text = "Comments";
			this.tpComments.UseVisualStyleBackColor = true;
			// 
			// dgComments
			// 
			this.dgComments.AllowUserToOrderColumns = true;
			this.dgComments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dgComments.ContextMenuStrip = this.mnuGrid;
			this.dgComments.Dock = System.Windows.Forms.DockStyle.Fill;
			this.dgComments.Location = new System.Drawing.Point(0, 0);
			this.dgComments.Name = "dgComments";
			this.dgComments.Size = new System.Drawing.Size(1083, 273);
			this.dgComments.TabIndex = 6;
			this.dgComments.DataSourceChanged += new System.EventHandler(this.DgCommentsDataSourceChanged);
			// 
			// tpText
			// 
			this.tpText.Controls.Add(this.richTextBox1);
			this.tpText.Location = new System.Drawing.Point(4, 22);
			this.tpText.Name = "tpText";
			this.tpText.Padding = new System.Windows.Forms.Padding(3);
			this.tpText.Size = new System.Drawing.Size(1083, 273);
			this.tpText.TabIndex = 0;
			this.tpText.Text = "Log";
			this.tpText.UseVisualStyleBackColor = true;
			// 
			// richTextBox1
			// 
			this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTextBox1.Location = new System.Drawing.Point(3, 3);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new System.Drawing.Size(1077, 267);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = "";
			// 
			// btnPopulate
			// 
			this.btnPopulate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPopulate.Location = new System.Drawing.Point(1019, 3);
			this.btnPopulate.Name = "btnPopulate";
			this.btnPopulate.Size = new System.Drawing.Size(75, 23);
			this.btnPopulate.TabIndex = 21;
			this.btnPopulate.Text = "Refresh";
			this.btnPopulate.UseVisualStyleBackColor = true;
			this.btnPopulate.Click += new System.EventHandler(this.BtnPopulateClick);
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(833, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "Warning no credentials stored/found";
			// 
			// UcCrucible
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnPopulate);
			this.Controls.Add(this.tabControl1);
			this.Name = "UcCrucible";
			this.Size = new System.Drawing.Size(1097, 334);
			this.Load += new System.EventHandler(this.UcCrucibleLoad);
			this.tabControl1.ResumeLayout(false);
			this.tpGrid.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgReviews)).EndInit();
			this.mnuGrid.ResumeLayout(false);
			this.tpReviewers.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgReviewers)).EndInit();
			this.tpComments.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.dgComments)).EndInit();
			this.tpText.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tpGrid;
		private System.Windows.Forms.DataGridView dgReviews;
		private System.Windows.Forms.TabPage tpText;
		private System.Windows.Forms.RichTextBox richTextBox1;
		private System.Windows.Forms.Button btnPopulate;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ContextMenuStrip mnuGrid;
		private System.Windows.Forms.ToolStripMenuItem reviewersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem commentsToolStripMenuItem;
		private System.Windows.Forms.TabPage tpReviewers;
		private System.Windows.Forms.TabPage tpComments;
		private System.Windows.Forms.DataGridView dgReviewers;
		private System.Windows.Forms.DataGridView dgComments;
	}
}
