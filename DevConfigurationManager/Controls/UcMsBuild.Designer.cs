namespace DeveloperConfigurationManager.Controls
{
	partial class UcMsBuild
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
			this.lblJunctionPath = new System.Windows.Forms.Label();
			this.ckUseJunctionForMsBuild = new System.Windows.Forms.CheckBox();
			this.txtMsTarget = new System.Windows.Forms.TextBox();
			this.rtMsBuild = new System.Windows.Forms.RichTextBox();
			this.btnMsBuild = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.txtMsBuildArguments = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.nudMultiProcessor = new System.Windows.Forms.NumericUpDown();
			this.btnCancel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.nudMultiProcessor)).BeginInit();
			this.SuspendLayout();
			// 
			// lblJunctionPath
			// 
			this.lblJunctionPath.AutoSize = true;
			this.lblJunctionPath.Location = new System.Drawing.Point(13, 5);
			this.lblJunctionPath.Name = "lblJunctionPath";
			this.lblJunctionPath.Size = new System.Drawing.Size(58, 13);
			this.lblJunctionPath.TabIndex = 18;
			this.lblJunctionPath.Text = "c:\\projects";
			// 
			// ckUseJunctionForMsBuild
			// 
			this.ckUseJunctionForMsBuild.AutoSize = true;
			this.ckUseJunctionForMsBuild.Checked = true;
			this.ckUseJunctionForMsBuild.CheckState = System.Windows.Forms.CheckState.Checked;
			this.ckUseJunctionForMsBuild.Location = new System.Drawing.Point(11, 61);
			this.ckUseJunctionForMsBuild.Name = "ckUseJunctionForMsBuild";
			this.ckUseJunctionForMsBuild.Size = new System.Drawing.Size(109, 17);
			this.ckUseJunctionForMsBuild.TabIndex = 17;
			this.ckUseJunctionForMsBuild.Text = "UseJunctionBase";
			this.ckUseJunctionForMsBuild.UseVisualStyleBackColor = true;
			// 
			// txtMsTarget
			// 
			this.txtMsTarget.Location = new System.Drawing.Point(13, 29);
			this.txtMsTarget.Name = "txtMsTarget";
			this.txtMsTarget.Size = new System.Drawing.Size(235, 20);
			this.txtMsTarget.TabIndex = 16;
			this.txtMsTarget.Text = "\\Solutions\\AllApps.sln";
			// 
			// rtMsBuild
			// 
			this.rtMsBuild.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.rtMsBuild.Location = new System.Drawing.Point(11, 233);
			this.rtMsBuild.Name = "rtMsBuild";
			this.rtMsBuild.Size = new System.Drawing.Size(852, 325);
			this.rtMsBuild.TabIndex = 15;
			this.rtMsBuild.Text = "";
			// 
			// btnMsBuild
			// 
			this.btnMsBuild.Location = new System.Drawing.Point(173, 57);
			this.btnMsBuild.Name = "btnMsBuild";
			this.btnMsBuild.Size = new System.Drawing.Size(75, 23);
			this.btnMsBuild.TabIndex = 14;
			this.btnMsBuild.Text = "MsBuild";
			this.btnMsBuild.UseVisualStyleBackColor = true;
			this.btnMsBuild.Click += new System.EventHandler(this.BtnMsBuildClick);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(13, 89);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(57, 13);
			this.label2.TabIndex = 13;
			this.label2.Text = "Arguments";
			// 
			// txtMsBuildArguments
			// 
			this.txtMsBuildArguments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtMsBuildArguments.Location = new System.Drawing.Point(76, 86);
			this.txtMsBuildArguments.Multiline = true;
			this.txtMsBuildArguments.Name = "txtMsBuildArguments";
			this.txtMsBuildArguments.Size = new System.Drawing.Size(787, 141);
			this.txtMsBuildArguments.TabIndex = 12;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(172, 5);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(15, 13);
			this.label1.TabIndex = 11;
			this.label1.Text = "m";
			// 
			// nudMultiProcessor
			// 
			this.nudMultiProcessor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.nudMultiProcessor.Location = new System.Drawing.Point(193, 3);
			this.nudMultiProcessor.Name = "nudMultiProcessor";
			this.nudMultiProcessor.Size = new System.Drawing.Size(120, 20);
			this.nudMultiProcessor.TabIndex = 10;
			this.nudMultiProcessor.Value = new decimal(new int[] {
            2,
            0,
            0,
            0});
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(254, 57);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 19;
			this.btnCancel.Text = "Cancel Build";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// UcMsBuild
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblJunctionPath);
			this.Controls.Add(this.ckUseJunctionForMsBuild);
			this.Controls.Add(this.txtMsTarget);
			this.Controls.Add(this.rtMsBuild);
			this.Controls.Add(this.btnMsBuild);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtMsBuildArguments);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.nudMultiProcessor);
			this.Name = "UcMsBuild";
			this.Size = new System.Drawing.Size(866, 561);
			((System.ComponentModel.ISupportInitialize)(this.nudMultiProcessor)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblJunctionPath;
		private System.Windows.Forms.CheckBox ckUseJunctionForMsBuild;
		private System.Windows.Forms.TextBox txtMsTarget;
		private System.Windows.Forms.RichTextBox rtMsBuild;
		private System.Windows.Forms.Button btnMsBuild;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtMsBuildArguments;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.NumericUpDown nudMultiProcessor;
		private System.Windows.Forms.Button btnCancel;
	}
}
