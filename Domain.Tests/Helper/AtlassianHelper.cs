namespace Domain.Tests.Helper
{
	using System;
	using System.Collections.Generic;
	using System.Windows.Forms;

	// TODO: make use credentials management for the domain instead of just in the unit testing
	public static class AtlassianHelper
	{
		public static string InitCredentials(Security security)
		{
			if (!string.IsNullOrEmpty(Properties.Settings.Default.atlassianPassword) && !string.IsNullOrEmpty(Properties.Settings.Default.crucibleAuthority))
				return security.Decrypt(Properties.Settings.Default.atlassianPassword);

			IDictionary<string, Action<string>> keyStorageLookup = new Dictionary<string, Action<string>>()
				{
					{"AtlassianPassword", s => Properties.Settings.Default.atlassianPassword = s},
					{"CrucibleAuthority", s => Properties.Settings.Default.crucibleAuthority = s},
				};

			using (var form = new Form())
			{
				int y = 0;
				foreach (var k in keyStorageLookup.Keys)
				{
					var isPassword = k.ToLowerInvariant().Contains("password");
					var lbl = new Label();

					form.Controls.Add(lbl);
					lbl.Text = k;
					lbl.Left = 10;
					lbl.Top = y + 10;

					var txt = new TextBox();
					form.Closing += (sender, e) => 
					{
						var setter = keyStorageLookup[k];
						if (isPassword)
							setter(security.Encrypt(txt.Text));
						else setter(txt.Text);
					};
					if (isPassword)
					{
						txt.UseSystemPasswordChar = true;
					}
					form.Controls.Add(txt);
					txt.Left = lbl.Right + 10;
					txt.Top = lbl.Top;
					y += Math.Max(lbl.Bottom, txt.Bottom) + 10;
					
				}
				
				var process = System.Diagnostics.Process.GetCurrentProcess();

				IWin32Window window = Control.FromHandle(process.MainWindowHandle);
				//form.Show();

				form.Activate();

				if (process.MainWindowHandle != IntPtr.Zero)
					form.Show(window);
				else
				{
					form.Show();
				}

				Application.Run(form);

				if (string.IsNullOrWhiteSpace(Properties.Settings.Default.atlassianPassword)) return null;

				Properties.Settings.Default.Save();
				return security.Decrypt(Properties.Settings.Default.atlassianPassword);
			}
		}
	}
}
