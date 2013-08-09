namespace DeveloperConfigurationManager.Controls.Interfaces
{
	public interface ISaveable : INamed
	{
		void Save();
	}

	public interface ISaveAs : INamed
	{
		bool CanSave();
		void Save(string path);
	}
}
