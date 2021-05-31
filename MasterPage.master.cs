using System;
using Telerik.Web.UI;

public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["sessionToken"] != null)
        {
            loginButton.Visible = false;
        }
        else
        {
            logoutButton.Visible = false;
        }
    }

    protected void RadMenu1_ItemClick(object sender, RadMenuEventArgs e)
    {
        Telerik.Web.UI.RadMenuItem ItemClicked = e.Item;
        if(ItemClicked.Text == "Logout")
        {
            Logout();
        }
    }

    protected void Logout()
    {   
        
        System.Diagnostics.Debug.WriteLine(Session["sessionToken"] + "logged off");
        Session.Clear();
        Page.Response.Redirect("/Default.aspx", true);
    }

}