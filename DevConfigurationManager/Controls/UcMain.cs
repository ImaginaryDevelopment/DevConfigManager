using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using DeveloperConfigurationManager.Controls.Interfaces;
using DeveloperConfigurationManager.CrossCutting;
using DeveloperConfigurationManager.Properties;
using Domain.Adapters;
using Domain.Extensions;
using Domain.Helpers;
using Domain.Models;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcMain : UserControl, IDisposable, IHaveDefault
	{

		readonly AccessorHelper<string> _atlassianUserNameAccessor;

		readonly AccessorHelper<string> _atlassianPasswordAccessor;
        private readonly IDictionary<Type,Lazy<Control>> _dynamicChildren;

        readonly Profiler _profiler;

		public event EventHandler ExitRequest
		{
			add
			{
				this.exitToolStripMenuItem.Click += value;
			}

			remove
			{
				this.exitToolStripMenuItem.Click -= value;
			}
		}

		public UcMain(
			ObservableCollection<string> servers,
			ObservableCollection<string> junctions,
			ObservableCollection<string> configXmlUris,
			AccessorHelper<string> atlassianUserNameAccessor,
			AccessorHelper<string> atlassianPasswordAccessor,
			IEnumerable<Control> children,
            IDictionary<Type,Lazy<Control>> dynamicChildren,
            Profiler profiler,
            // ReSharper disable once ParameterTypeCanBeEnumerable.Local
			IDictionary<string, Uri> links)
		{
			InitializeComponent();

			//ninject does the guards! weeee


			foreach (var child in children)
			{
				AddTabPage(child);

			}

			this._atlassianUserNameAccessor = atlassianUserNameAccessor;
			this._atlassianPasswordAccessor = atlassianPasswordAccessor;
		    _dynamicChildren = dynamicChildren;
		    _profiler = profiler;

			InitializeServerStoreMenu(servers);
			InitializeJunctionStoreMenu(junctions);
			InitializeConfigXmlStoreMenu(configXmlUris);

			var tabRefreshes = tbGrid.ToObservableSelected().ObserveOn(SynchronizationContext.Current);

			tabRefreshes.Select(_ => tbGrid.SelectedTab.Controls.OfType<ISaveable>().SingleOrDefault()).Subscribe(
				sav => mnuSave.Enabled = sav != null);

			tabRefreshes.Select(_ => tbGrid.SelectedTab.Controls.OfType<ISaveAs>().SingleOrDefault())
				.Subscribe(sav => mnuSaveAs.Enabled = sav != null);


			tabRefreshes.Select(
				_ => tbGrid.SelectedTab.Controls.OfType<IRefreshable>().FirstOrDefault()).Subscribe(
					ir => tslStatus.Text = ir != null ? ir.Status : string.Empty);

			tabRefreshes.Subscribe(
				_ => tbGrid.SelectedTab.Controls.OfType<IRefreshable>().ToList().ForEach(ir => ir.RefreshData()));

			mnuSaveAs.ToObservableClick().Subscribe(s => OnMnuSaveAsClick());

			mnuSave.ToObservableClick().Subscribe(s => OnMnuSaveClick());

			Settings.Default.ToObservablePropertyChanged()
				.Where(s => s == LinqOp.PropertyOf(() => Settings.Default.GitPath).Name)
				.SubscribeOn(SynchronizationContext.Current).Subscribe(_ => miGitPath.Text = Settings.Default.GitPath);
			Settings.Default.ToObservablePropertyChanged().Where(
				s => s == LinqOp.PropertyOf(() => Settings.Default.Servers).Name).SubscribeOn(
					SynchronizationContext.Current).Subscribe(_ => this.InitializeServerStoreMenu(servers));

			miGitPath.Text = Settings.Default.GitPath;

			InitializeSites(links);

			foreach (var c in this.Childrens().OfType<IRefreshable>().OfType<INotifyPropertyChanged>().OfType<Control>())
			{
				var control = c;
				var inp = (INotifyPropertyChanged)c;
				var ir = (IRefreshable)c;
				inp.ToObservablePropertyChanged().Where(s => s == LinqOp.PropertyOf(() => ir.Status).Name).ObserveOn(SynchronizationContext.Current)
					.Subscribe(s =>
									  {
										  //tbGrid.InvokeSafe(tc=>
										  //	{tc.SelectedTab.Childrens().Contains(control)});
										  if (tbGrid.SelectedTab.Childrens().Contains(control))
											  tslStatus.Text = ir.Status;
									  });
			}

		}

        void AddTabPage(Control child)
        {
            var name = child.Name.StartsWith("Uc") ? child.Name.After("Uc") : child.Name;
            var tp = new TabPage { Text = name };
            tp.Controls.Add(child);
            child.Dock = DockStyle.Fill;
            tbGrid.TabPages.Add(tp);
            var hasDefaultButton = child as IHaveDefault;
            tbGrid.ImageList = imageList1;
            if (hasDefaultButton != null)
            {
                tbGrid.Selected += (sender, e) =>
                {
                    if (sender == hasDefaultButton)
                    {
                        if (AcceptButtonChangedEvent != null) AcceptButtonChangedEvent(sender, e);
                    }
                };
            }
            TryInitializeIcon(tp, name, child as ICanHaveAnIcon);

        }

		void TryInitializeIcon(TabPage tp, string name, ICanHaveAnIcon canHaveAnIcon)
		{
			if (canHaveAnIcon == null)
				return;
			Action updateIcon = () =>
				{

					if (canHaveAnIcon.Icon == null) return;

					if (imageList1.Images.ContainsKey(name) == false) imageList1.Images.Add(name, canHaveAnIcon.Icon);

					var index = imageList1.Images.IndexOfKey(name);
					tp.ImageIndex = index;

				};
			updateIcon();
			var notifier = canHaveAnIcon as INotifyPropertyChanged;
			if (notifier != null)
			{
				notifier.ToObservablePropertyChanged()
				        .Where(p => p == LinqOp.PropertyOf<ICanHaveAnIcon>(i => i.Icon).Name)
				        .Subscribe(_=>updateIcon());
			}

			

		}

		void InitializeConfigXmlStoreMenu(ObservableCollection<string> configXmlUris)
		{
			InitializeCollectionMenu(configXmlStoreToolStripMenuItem, configXmlUris);
		}

		static void AddCollectionMenus(ToolStripMenuItem parent, ObservableCollection<string> source, params string[] items)
		{
			foreach (var i in items)
			{
				var closure = i;
				parent.DropDownItems.Add(
					closure,
					null,
					(sender, e) =>
					{
						source.Remove(closure);
						parent.DropDownItems.Remove((ToolStripMenuItem)sender);
					});
			}
		}

		static void InitializeCollectionMenu(ToolStripMenuItem parent, ObservableCollection<string> children)
		{
			parent.DropDownItems.Clear();
			children.ToObservableCollectionChanged()
				.Where(c => c.EventArgs.Action == NotifyCollectionChangedAction.Add)
				.SubscribeOn(SynchronizationContext.Current)
				.Subscribe(e => AddCollectionMenus(parent, children, e.EventArgs.NewItems.Cast<string>().ToArray()));

			AddCollectionMenus(parent, children, children.ToArray());

		}

		void InitializeJunctionStoreMenu(ObservableCollection<string> junctions)
		{
			InitializeCollectionMenu(junctionStoreToolStripMenuItem, junctions);
		}

		void InitializeServerStoreMenu(ObservableCollection<string> servers)
		{
			InitializeCollectionMenu(serverStoreToolStripMenuItem, servers);
		}

		void InitializeSites(IEnumerable<KeyValuePair<string, Uri>> links)
		{
			foreach (var s in links)
			{
				var l = new LinkLabel { Text = s.Key };
				l.Links.Add(new LinkLabel.Link(0, s.Key.Length, s.Value));
				l.LinkClicked += (o, e) => System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
				flpLinks.Controls.Add(l);
			}
		}

		void GitToolStripMenuItemClick(object sender, EventArgs e)
		{
			string path;
			using (var ofd = new OpenFileDialog())
			{
				var programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
				if (programFiles.IsNullOrEmpty())
					programFiles = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
				if (System.IO.Directory.Exists(programFiles))
				{
					ofd.InitialDirectory = programFiles;
					var gitFolder = Path.Combine(programFiles, "Git");
					if (Directory.Exists(System.IO.Path.Combine(programFiles, "Git")))
					{
						ofd.InitialDirectory = gitFolder;
						var gitBin = Path.Combine(gitFolder, "bin");
						if (System.IO.Directory.Exists(gitBin))
							ofd.InitialDirectory = gitBin;
					}
				}
				ofd.DefaultExt = ".exe";
				ofd.Title = "Please locate Git.exe";
				if (ofd.ShowDialog(this) != DialogResult.OK)
					return;
				path = Path.GetFullPath(ofd.FileName);
			}

			Settings.Default.GitPath = path;


		}

		void RefreshToolStripMenuItemClick(object sender, EventArgs e)
		{

			tbGrid.SelectedTab.Controls.OfType<IRefreshable>().ToList().ForEach(ir => ir.RefreshData());
		}

		void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			MessageBox.Show(
				this, "Assembly Version:" + Assembly.GetExecutingAssembly().GetName().Version);
		}

		void OnMnuSaveClick()
		{
			var saveable = tbGrid.SelectedTab.Controls.OfType<ISaveable>().SingleOrDefault();
			if (saveable == null)
				return;
			saveable.Save();
		}

		void OnMnuSaveAsClick()
		{
			var saveable = tbGrid.SelectedTab.Controls.OfType<ISaveAs>().SingleOrDefault();
			if (saveable == null) return;
			if (saveable.CanSave() == false)
				return;
			using (var sfd = new SaveFileDialog())
			{
				sfd.Filter = "JSon|*.json";
				sfd.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
				if (sfd.ShowDialog(this) != DialogResult.OK)
					return;
				saveable.Save(sfd.FileName);
			}
		}

	

		void AtlassianLoginToolStripMenuItemClick(object sender, EventArgs e)
		{
			using (var f = new FrmLogin())
			{
				f.Text = "Atlassian Login";
				f.txtUsername.Text = this._atlassianUserNameAccessor.Getter();
				f.txtPwd.Text = this._atlassianPasswordAccessor.Getter();
				if (f.txtUsername.Text.IsNullOrWhitespace()) f.txtUsername.Text = Environment.UserName;
				var result = f.ShowDialog();
				if (result == DialogResult.Cancel) return;
				var userName = f.txtUsername.Text;
				var pwd = f.txtPwd.Text;
				this._atlassianUserNameAccessor.Setter(userName);
				this._atlassianPasswordAccessor.Setter(pwd);

			}
		}

		void ChangeLogToolStripMenuItemClick(object sender, EventArgs e)
		{
		    using (var stream =Assembly.GetExecutingAssembly().GetManifestResourceStream("DeveloperConfigurationManager.Changelog.txt"))
		    {
                if(stream!=null)
		        using (var r = new StreamReader(stream))
		        {
		            MessageBox.Show(r.ReadToEnd());
		        }
		    }
		}



		public Button Accept
		{
			get
			{
				var currentUc = this.tbGrid.SelectedTab.Controls.OfType<IHaveDefault>().FirstOrDefault();
				if (currentUc == null) return null;
				return currentUc.Accept;
			}
		}


		public event EventHandler AcceptButtonChangedEvent;

		private void BtnRConClick(object sender, EventArgs e)
		{
		    try
		    {
                RCmd.Prompt(cmbServer.Text);
		    }
		    catch (Exception ex)
		    {

		        MessageBox.Show(this, ex.ToString());
		    }
			
		}

		private void WhereAmIToolStripMenuItemClick(object sender, EventArgs e)
		{
			var path = Assembly.GetExecutingAssembly().Location;
			var folder = System.IO.Path.GetDirectoryName(path);
			System.Diagnostics.Process.Start(folder);
		}

		private void WhereAreMySettingsToolStripMenuItemClick(object sender, EventArgs e)
		{
			//app
			//C:\Users\bdimperio\AppData\Local\Apps\2.0\7L5B4HP4.5ET\PT3JHRHC.J2K\pays..tion_ae2f2bc29f081638_0001.0000_6e0e61aa39a555b9

			//data
			//C:\Users\bdimperio\AppData\Local\Apps\2.0\Data\AA721NYG.ORN\YXVG5L7J.57M\pays..tion_ae2f2bc29f081638_0001.0000_6e0e61aa39a555b9\Data\1.2012.41231.1
			var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
			System.Diagnostics.Process.Start(path);
		}

		private void btnProfile_Click(object sender, EventArgs e)
		{
			richTextBox1.Clear();

			var text = _profiler.Show("\t");

			if (text.IsNullOrEmpty())
			{
				richTextBox1.AppendText("No profiling data found");
			}
			else
			{
				var pretty = JsonPrettifier.PrettyPrint(text);
				richTextBox1.AppendText(pretty);
			}
		}

		private void showRawJsonInLogsToolStripMenuItem_Click(object sender, EventArgs e)
		{
			showRawJsonInLogsToolStripMenuItem.Checked = !showRawJsonInLogsToolStripMenuItem.Checked;
		}

        private void envDteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var control=_dynamicChildren[typeof (UcEnvDte)];
            AddTabPage(control.Value);
            
        }

	}
}
