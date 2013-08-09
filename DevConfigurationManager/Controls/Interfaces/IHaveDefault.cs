using System;
using System.Windows.Forms;

namespace DeveloperConfigurationManager.Controls.Interfaces
{
    public interface IHaveDefault
	{
		Button Accept { get; }

		event EventHandler AcceptButtonChangedEvent;
	}
}