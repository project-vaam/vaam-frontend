using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Web.UI;

public partial class Notes : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string[] folders = new string[] { "All (14)", "Personal (0)", "Work (0)" };

        // Populate the Desktop TreeView control
        RadTreeView tree = (RadTreeView)FolderNavigationControl.FindControl("rtvFolders");

        foreach (string folder in folders)
        {
            tree.Nodes.Add(new RadTreeNode(folder));
        }

        // Populate the Mobile RadMenu control

        RadMenu menu = (RadMenu)MobileNavigation.FindControl("popupMenu");
        foreach (string folder in folders)
        {
            menu.Items.Add(new RadMenuItem(folder));
        }
    }
}