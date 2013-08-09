using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls;
using DeveloperConfigurationManager.Properties;

namespace DeveloperConfigurationManager
{
	public partial class Form1 : Form, IDisposable
	{
		readonly IList<CancellationTokenSource> _cancellationTokens = new List<CancellationTokenSource>();

		[Obsolete("For designer only")]
		internal Form1()
		{
			InitializeComponent();
		}

		readonly UcMain _ucMain;

		public Form1(UcMain main)
		{
			InitializeComponent();

			Debug.Assert(Settings.Default != null);

			_ucMain = main;
			_ucMain.ExitRequest += this.OnExitRequest;
			this.Controls.Add(_ucMain);
			
			_ucMain.Dock = DockStyle.Fill;
		}

		void OnExitRequest(object sender, EventArgs e)
		{
			this.Close();
		}

		void Form1FormClosing(object sender, FormClosingEventArgs e)
		{
			foreach (var item in _cancellationTokens)
			{
				Debug.WriteLine("Cancelling tokens");
				try
				{

					item.Cancel(true);
				}
				catch (OperationCanceledException ocex)
				{
					Debug.WriteLine("ocex:" + ocex);
				}

			}
		}

	}
}
