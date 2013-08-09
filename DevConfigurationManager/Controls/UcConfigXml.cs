using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using System.Xml.Linq;
using Domain.Extensions;

namespace DeveloperConfigurationManager.Controls
{
    public partial class UcConfigXml : UserControl
	{
		readonly ObservableCollection<string> _configUris;

		const string Expandstub = "ExpandStub";

		string _raw;
		XDocument _xml;

		public UcConfigXml(ObservableCollection<string> configXmlUris)
		{
			InitializeComponent();
			_configUris = configXmlUris;
			cbAddress.DataSource = _configUris;
		}

		static void RefreshTree(string key, XDocument xml, TreeView tvXml)
		{
			tvXml.Nodes.Clear();
			var root = tvXml.Nodes.Add(key);
			WalkLevel(xml.Root, root);
		}

		static bool IsLazyNode(TreeNode node)
		{
			return node.Nodes.Count == 1 && node.Nodes[0].Text == Expandstub;
		}

		static void WalkLevel(XElement xmlNode, TreeNode element, int? index = null)
		{
			var rootNode = index.HasValue == false
									? element.Nodes.Add(xmlNode.Name.LocalName)
									: element.Nodes[index.Value];

			if (xmlNode.HasAttributes)
			{
				var attribRoot = rootNode.Nodes.Add("Attributes");
				attribRoot.Tag = xmlNode;
				var font = rootNode.NodeFont ?? DefaultFont;
				attribRoot.NodeFont = new Font(font, FontStyle.Italic);
				foreach (var attrib in xmlNode.Attributes())
				{
					var attribNode = attribRoot.Nodes.Add(attrib.Name.LocalName);
					attribNode.Tag = attrib;
					if (StringExtensions.IsNullOrEmpty(attrib.Value))
					{
						var valNode = attribNode.Nodes.Add("(empty)");

						valNode.NodeFont = new Font(font, FontStyle.Italic);
					}
					else
					{
						attribNode.Nodes.Add(attrib.Value);
					}
				}
			}

			foreach (var tNode in xmlNode.Nodes().OfType<XText>())
			{
				var node = rootNode.Nodes.Add(tNode.Value);
				node.Tag = tNode;
			}

			foreach (var cNode in xmlNode.Nodes().OfType<XCData>())
			{
				var node = rootNode.Nodes.Add(cNode.Value);
				node.Tag = cNode;
			}

			if (xmlNode.Elements().Any())
			{
				var q = from c in xmlNode.Elements()
						  let name = c.Name.LocalName
						  let nameValue = c.GetAttribValOrNull("Name")
						  let value = c.GetAttribValOrNull("Value")
						  let isSpecial = IsSpecialElement(c)
						  orderby isSpecial descending, name, nameValue
						  select new { c, Name = name, NameValue = nameValue, IsSpecial = isSpecial };

				foreach (var child in q)
				{
					var name = child.Name
								  + (child.NameValue == null ? string.Empty : (" (" + child.NameValue + ")"));

					var childNode = rootNode.Nodes.Add(name);
					if (child.IsSpecial)
					{
						childNode.NodeFont = new Font(rootNode.NodeFont ?? DefaultFont, FontStyle.Bold);
					}

					childNode.Tag = child.c;
					childNode.Nodes.Add(Expandstub);
				}
			}
		}

		static bool IsSpecialElement(XElement element)
		{
			var value = element.GetAttribValOrNull("Value");
			if (StringExtensions.IsNullOrEmpty(value)) return false;

			return value.StartsWith("server") || value.StartsWith("http") || value.StartsWith(@"\\");
		}

		void BtnPopulateClick(object sender, EventArgs e)
		{
			if (cbAddress.Text.StartsWith("http", StringComparison.CurrentCultureIgnoreCase))
				GetXmlFromUri(cbAddress.Text);
		}

		void GetXmlFromUri(string p)
		{
			using (var wc = new System.Net.WebClient())
			{
				try
				{
					_raw = wc.DownloadString(p);
				}
				catch (WebException ex)
				{
					richTextBox1.AppendText(ex.Message);
					tabControl1.SelectTab(tbText);
					return;
				}
				
				richTextBox1.AppendText(_raw);
				_xml = XDocument.Parse(_raw);

			}

			_configUris.AddIfMissing(p);
			RefreshTree(p, _xml, tvXml);
			EagerExpand();
		}

		void EagerExpand()
		{
			var root = tvXml.Nodes[0];
			root.Expand();
			var configurationRoot = root.Nodes[0];
			configurationRoot.Expand();
			Debug.Assert(configurationRoot.Text == "Configuration");

			//var security=configurationRoot.Nodes.Cast<TreeNode>().First(n => n.Text == "Security");
			//security.Expand();
			var appSettings = configurationRoot.Nodes.Cast<TreeNode>().First(a => a.Text == "appSettings");
			appSettings.Expand();

		}

		void PopulateLazyNode(TreeNode node)
		{
			if (!IsLazyNode(node))
			{
				return;
			}

			node.Nodes[0].Remove();
			var parent = node.Parent;
			var index = node.Index;
			var nodeToExpand = (XElement)node.Tag;

			WalkLevel(nodeToExpand, parent, index);
		}

		void TvXmlBeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			if (IsLazyNode(e.Node))
			{
				this.PopulateLazyNode(e.Node);
				e.Node.Expand();
			}
		}

		void BtnClearClick(object sender, EventArgs e)
		{
			tvXml.Nodes.Clear();
		}

		void BtnLaunchClick(object sender, EventArgs e)
		{
			if (StringExtensions.IsNullOrEmpty(cbAddress.Text)) return;
			Process.Start(cbAddress.Text);
		}
	}
}
