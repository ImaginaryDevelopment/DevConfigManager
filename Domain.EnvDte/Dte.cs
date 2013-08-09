using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.EnvDte
{
    using System.Runtime.InteropServices;

    using EnvDTE;

    public class Dte
    {
        // ReSharper disable once UnusedMember.Local
        const string SolutionExplorerWindow = "{3AE79031-E1BC-11D0-8F78-00A0C9110057}";
        // ReSharper disable once UnusedMember.Local
        const string SolutionFolder = "{66A26720-8FB5-11D2-AA7E-00C04F688DDE}";

        readonly EnvDTE.DTE _dte;
        public Dte()
        {
            _dte = (EnvDTE.DTE)Marshal.GetActiveObject("VisualStudio.DTE");
            if (_dte == null) throw new InvalidOperationException("Could not get a DTE");
        }
        public object GetDisplay(UIHierarchyItem item)
        {
            object display = item.ToString();
            var comType = ComHelper.GetTypeName(item.Object);
            display = new { ComType = comType, display };
            if (comType == "Project")
            {
                var p = item.Object as Project;

                if (p != null)
                    display = new { p.Kind, p.UniqueName, p.FileName, p.FullName, };


            }
            else if (comType == "_UIHierarchyItemMarshaler")
            {
                if (object.ReferenceEquals(item.Object, item))
                    return display;
                var uihi = (UIHierarchyItem)item.Object;
                ComHelper.GetTypeName(uihi.Object).Dump("Found child of uihi");
            }
            else
            {
                comType.Dump("Failed to unload");
            }
            return display;
        }
        public IEnumerable<UIHierarchyItem> GetChildItems(UIHierarchyItem parent, bool topdown)
        {

            if (parent.Collection.Count > 0)
                foreach (var item in parent.UIHierarchyItems.Cast<UIHierarchyItem>())
                {//.Where (uihi => uihi.Object is Project))
                    if (topdown)
                        yield return item;
                    foreach (var ic in GetChildItems(item, topdown))
                        yield return ic;
                    if (!topdown)
                        yield return item;
                }
        }
        public void UnloadProjects(bool topdown = false)
        {

            var uih = _dte.ActiveWindow.Object as UIHierarchy;
            var solutionItem = uih.UIHierarchyItems.Cast<UIHierarchyItem>().Single();
            var failures = new List<object>();
            var unloaded = new List<object>();
            foreach (var uiProject in GetChildItems(solutionItem, topdown))
            {
                object display;
                try
                {
                    display = GetDisplay(uiProject);
                }
                catch (InvalidCastException)
                {

                    continue;
                }

                //var comType = ComHelper.GetTypeName(uiProject.Object);

                uiProject.Select(vsUISelectionType.vsUISelectionTypeSelect);

                try
                {
                    _dte.ExecuteCommand("Project.UnloadProject");
                    unloaded.Add(display);
                }
                catch (COMException ex)
                {
                    if (object.ReferenceEquals(uiProject.Object, uiProject) == false) //assume project is already unloaded
                    {
                        failures.Add(display);
                    }

                }
            }
        }
    }
}
