using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
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
        JArray processesData;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["sessionToken"] == null)
            {
                Page.Response.Redirect("Login.aspx", true);
            }

            if (!IsPostBack)
            {
                mouldsQuantity.Visible = false;
                RadGridDesvios.Visible = false;
                RadHtmlChart1.Visible = false;

                mouldsSpinner.Visible = true;
                desviosSpinner.Visible = true;
                graphSpinner.Visible = true;

                fetchProcessData();

            }
        }

        protected async void fetchProcessData()
        {
            using (var httpClient = new HttpClient())
            {
                string token = (string)Session["sessionToken"];
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                Debug.WriteLine(Constants.URL_BACKEND_CONNECTION + "dashboard");

                using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "dashboard").ConfigureAwait(false))
                {
                    var status = response.IsSuccessStatusCode;

                    if (status == true)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        processesData = JsonConvert.DeserializeObject<JArray>(apiResponse);

                        SetMoulds();
                        FillTable();
                        FillChart();

                        int index = processesData.Last()["date"].ToString().LastIndexOf(".");
                        string date = processesData.Last()["date"].ToString().Substring(0, index);
                        dadosAtualizados.InnerText = "Dados Atualizados a: " + date;
                    }
                    else
                    {
                        Debug.WriteLine("Something went bad.");
                    }
                }
            }
        }

        private void SetMoulds()
        {

            var mouldsData = processesData.Last();
            if (mouldsData["description"].ToString() != "Número Moulds") return;

            mouldsQuantity.InnerText = mouldsData["value"].ToString();
            mouldsQuantity.Visible = true;
            mouldsSpinner.Visible = false;
        }

        private void FillTable()
        {

            DataTable table = new DataTable();

            DataColumn idColumn = table.Columns.Add("Processo");
            table.Columns.Add("Nº Desvios");

            table.PrimaryKey = new DataColumn[] { idColumn };

            foreach (var process in processesData)
            {


                if (process["process"] != null && process["description"].ToString() == "Desvios")
                {
                    table.Rows.Add(new object[] { process["process"]["name"], process["value"] });
                }

            }
            table.AcceptChanges();

            RadGridDesvios.DataSource = table;
            RadGridDesvios.Visible = true;
            desviosSpinner.Visible = false;
        }
        private void FillChart()
        {
            ColumnSeries firstColumnSeries = new ColumnSeries();

            foreach (var process in processesData)
            {
                if (process["process"] != null && process["description"].ToString() == "Duração média para a criação de um molde em milissegundos")
                {
                    Debug.WriteLine(process["process"]["name"]);
                    

                    decimal horas = Convert.ToDecimal(process["value"].ToString()) / 3600000;

                    Debug.WriteLine(horas);
                    RadHtmlChart1.PlotArea.XAxis.Items.Add(process["process"]["name"].ToString());
                    firstColumnSeries.SeriesItems.Add(decimal.Round(horas, 2, MidpointRounding.AwayFromZero));
                }
            }
            RadHtmlChart1.PlotArea.Series.Add(firstColumnSeries);

            graphSpinner.Visible = false;
            RadHtmlChart1.Visible = true;
        }

        protected void Logout(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Session["sessionToken"] + "logged off");
            Session.Clear();
            Page.Response.Redirect(Page.Request.Url.ToString(), true);


        }
    }
}