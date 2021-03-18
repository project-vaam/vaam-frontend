using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace TelerikWebAppResponsive
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (Session["sessionToken"] != null)
            {
                Label1.Text = "Olá " + Session["username"];
            }
            else
            {
                Label1.Text = "De momento nao se encontra logado";
            }
        }

        protected void Logout(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Session["sessionToken"] + "logged off");
            Session.Clear();
            Label2.Text = "Session Cleanned";
            Page.Response.Redirect(Page.Request.Url.ToString(), true);


        }
    }
}