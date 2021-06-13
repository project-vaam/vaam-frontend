using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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
            if (Session["sessionToken"] == null)
            {
                Page.Response.Redirect("Login.aspx", true);
            }

            if (!IsPostBack)
            {
                mouldsQuantity.Visible = false;
                mouldsSpinner.Visible = true;

                desviosQty.Visible = false;
                desviosSpinner.Visible = true;

                graphSpinner.Visible = false;
                fetchMouldCount();
                //fetchDesviation();
            }
        }

        protected async void fetchMouldCount()
        {
            using (var httpClient = new HttpClient())
            {
                string token = (string)Session["sessionToken"];
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Debug.WriteLine(Constants.URL_BACKEND_CONNECTION + "moulds");
                using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "moulds").ConfigureAwait(false))
                {

                    var status = response.IsSuccessStatusCode;

                    if (status == true)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        JArray mouldsArray = JsonConvert.DeserializeObject<JArray>(apiResponse);
                        mouldsQuantity.InnerText = mouldsArray.Count.ToString();
                        mouldsQuantity.Visible = true;
                        mouldsSpinner.Visible = false;
                    }
                    else
                    {
                        Debug.WriteLine("Something went bad.");
                    }
                }
            }
        }

        //protected async void fetchDesviation() { 

        //}


        protected void Logout(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Session["sessionToken"] + "logged off");
            Session.Clear();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);


        }
    }
}