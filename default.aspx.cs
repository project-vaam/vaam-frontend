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

                //graphSpinner.Visible = false;
                fetchMouldCount();
                fetchDesviation();

                //Graph RAW
                RadHtmlChart1.PlotArea.XAxis.Items.Add("DummyProcess1");
                RadHtmlChart1.PlotArea.XAxis.Items.Add("DummyProcess2");
                RadHtmlChart1.PlotArea.XAxis.Items.Add("DummyProcess3");

                ColumnSeries firstColumnSeries = new ColumnSeries();
                firstColumnSeries.SeriesItems.Add(40);
                firstColumnSeries.SeriesItems.Add(20, System.Drawing.Color.Green);
                firstColumnSeries.SeriesItems.Add(320);
                RadHtmlChart1.PlotArea.Series.Add(firstColumnSeries);
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

        protected async void fetchDesviation()
        {
            using (var httpClient = new HttpClient())
            {
                string token = (string)Session["sessionToken"];
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Debug.WriteLine(Constants.URL_BACKEND_CONNECTION + "/workflow-network/inductive-miner/processes/2?paths=0.5&activities=0.5&showDeviations=true");

                using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "/workflow-network/inductive-miner/processes/2?paths=0.5&activities=0.5&showDeviations=true").ConfigureAwait(false))
                {

                    var status = response.IsSuccessStatusCode;

                    if (status == true)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();

                        JObject mined = JsonConvert.DeserializeObject<JObject>(apiResponse);
                        desviosQty.InnerText = mined["numDeviations"].ToString();
                        Debug.WriteLine(mined["numDeviations"].ToString());
                        desviosQty.Visible = true;
                        desviosSpinner.Visible = false;
                    }
                    else
                    {
                        Debug.WriteLine("Something went bad.");
                    }
                }
            }
        }


        protected void Logout(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Session["sessionToken"] + "logged off");
            Session.Clear();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);


        }
    }
}