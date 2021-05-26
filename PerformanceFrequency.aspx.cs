using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Performance : System.Web.UI.Page
{
    public string processes = "null";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sessionToken"] == null)
        {

            Page.Response.Redirect("Login.aspx", true);
        }

        if (!IsPostBack)
        {
            thresholdField.Visible = false;
            displayProcess.Visible = false;
            escalaCores.Visible = false;
            tipoGrafico.Visible = true;
            inductiveContainer.Visible = false;
            callProcesses();
        } 
    }


    protected void ShowDiagram_Click(object sender, EventArgs e)
    {
        GetWorkFlows(RadComboBoxProcess.SelectedValue);
    }

    protected async void callProcesses()
    {

        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "processes").ConfigureAwait(false))
            {

                var status = response.IsSuccessStatusCode;

                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    JArray obj = JsonConvert.DeserializeObject<JArray>(apiResponse);


                    foreach (JObject item in obj)
                    {
                        string value = item["id"].ToString();
                        string text = item["id"].ToString() + " - " + item["name"].ToString();
                        RadComboBoxProcess.Items.Add(new RadComboBoxItem(text, value));

                        RadComboBoxProcess.SelectedIndex = 0;

                    }
                }
                else
                {
                    Debug.WriteLine("Something went bad.");
                }
            }
        }
    }

   
    protected async void GetWorkFlows(string processID) //chamar no load Page
    {
        errorMessage.InnerText = "";
        showError.Visible = false;

        if (PerformanceBtn.Checked && !InductiveRadioBtn.Checked)
        {
            escalaCores.Visible = true;
        }
        else
        {
            escalaCores.Visible = false;
        }

        
        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string threshold = "", workflowURL = "workflow-network/alpha-miner/processes/";
            
            string completeURL = "";

            if (!AlphaRadioBtn.Checked)
            {
                threshold = "?threshold=" + ThresholdSlider.Value.ToString().Replace(",",".");
                workflowURL = HeuristicRadioBtn.Checked ? "workflow-network/heuristic-miner/processes/" : "workflow-network/inductive-miner/processes/";                
            }

            if (InductiveRadioBtn.Checked)
            {
                string activities = "activities=" + labelSliderActivities.Text.Replace(",", ".");
                string paths = "paths=" + labelSliderPathsValue.Text.Replace(",", ".");
                string hasDesviations = "showDeviations=" + RadCheckBoxShowDesviations.Checked.ToString().ToLower();
                completeURL = workflowURL + processID + "?" + paths + "&" + activities + "&" + hasDesviations;
                Debug.WriteLine(completeURL);
            }
            else
            {
                 completeURL = workflowURL + processID + threshold;
                Debug.WriteLine(completeURL);
            }
            

            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + completeURL).ConfigureAwait(false))
            {
                //Debug.WriteLine(response);

                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(apiResponse);
                    
                    processes = "{isPerformance: '"+PerformanceBtn.Checked+"', data: "+apiResponse + "}";
                    Debug.WriteLine(processes);
                }
                else
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Something went bad");
                    errorMessage.InnerText = apiResponse;
                    showError.Visible = true;
                }
            }
        }
    }

    public void AlphaRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = false;
        tipoGrafico.Visible = true;
        escalaCores.Visible = true;
        inductiveContainer.Visible = false;
    }
    public void HeuristicRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = true;
        tipoGrafico.Visible = true;
        escalaCores.Visible = true;
        inductiveContainer.Visible = false;
    }
    public void InductiveRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = false;
        tipoGrafico.Visible = false;
        escalaCores.Visible = false;
        inductiveContainer.Visible = true;
    }

    /* Inductive Sliders */

    protected void RadSliderActivities_ValueChanged(object sender, EventArgs e)
    {
        labelSliderActivities.Text = (RadSliderActivities.Value / 1000).ToString("N3");
    }
    protected void RadSliderPaths_ValueChanged(object sender, EventArgs e)
    {
        labelSliderPathsValue.Text = (RadSliderPaths.Value / 1000).ToString("N3");
    }
}

