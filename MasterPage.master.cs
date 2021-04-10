using System;

public partial class MasterPage : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["sessionToken"] != null)
        {         
            loginButton.Visible = false;
            // loginButton.Visible = true;
        }
        else
        {
            logoutButton.Visible = false;
        }
    }

    protected void Logout(object sender, EventArgs e)
    {   
        
        System.Diagnostics.Debug.WriteLine(Session["sessionToken"] + "logged off");
        Session.Clear();
        Page.Response.Redirect(Page.Request.Url.ToString(), true);
    }

}