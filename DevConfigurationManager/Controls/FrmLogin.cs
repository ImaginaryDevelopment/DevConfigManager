using System;
using System.Windows.Forms;

namespace DeveloperConfigurationManager.Controls
{
	public partial class FrmLogin : Form
	{
		
		public FrmLogin()
		{
			InitializeComponent();
			
		}

		void BtnSaveClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		void BtnCancelClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Cancel;
			this.Close();
		}

		void BtnUseClick(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.Yes;
			this.Close();
		}

	}
}
