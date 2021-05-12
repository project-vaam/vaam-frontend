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
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Conformance : System.Web.UI.Page
{
    public string processes = "null"; // "null" because of cytoscape diagrams
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
            RadComboBoxProcessToCompare.Enabled = false;


            callProcesses();
           
            System.Threading.Thread.Sleep(500);
            callFilterInformation(null,null);
        }
    }

    /* Calendar DatePicker Settings*/
    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);

        RadDatePicker1.Culture = new System.Globalization.CultureInfo("pt-PT");

        RadDatePicker2.Culture = new System.Globalization.CultureInfo("pt-PT");


        RadDatePicker1.DatePopupButton.Visible = true;
        RadDatePicker1.ShowPopupOnFocus = true;
        RadDatePicker2.DatePopupButton.Visible = true;
        RadDatePicker2.ShowPopupOnFocus = true;

        RadDatePicker1.EnableScreenBoundaryDetection = true;
        RadDatePicker1.PopupDirection = (Telerik.Web.UI.DatePickerPopupDirection)Enum.ToObject(typeof(Telerik.Web.UI.DatePickerPopupDirection), 22);
        RadDatePicker2.EnableScreenBoundaryDetection = true;
        RadDatePicker2.PopupDirection = (Telerik.Web.UI.DatePickerPopupDirection)Enum.ToObject(typeof(Telerik.Web.UI.DatePickerPopupDirection), 22);

        CalendarAnimationType animationType = (CalendarAnimationType)Enum.Parse(typeof(CalendarAnimationType), "Fade");
        RadDatePicker1.ShowAnimation.Type = animationType;
        RadDatePicker1.HideAnimation.Type = animationType;
        RadDatePicker1.Calendar.FastNavigationSettings.ShowAnimation.Type = animationType;
        RadDatePicker1.Calendar.FastNavigationSettings.HideAnimation.Type = animationType;
        RadDatePicker2.ShowAnimation.Type = animationType;
        RadDatePicker2.HideAnimation.Type = animationType;
        RadDatePicker2.Calendar.FastNavigationSettings.ShowAnimation.Type = animationType;
        RadDatePicker2.Calendar.FastNavigationSettings.HideAnimation.Type = animationType;
    }

    protected void RadCheckBoxProcessToCompare_Click(object sender, EventArgs e)
    {
        RadComboBoxProcessToCompare.Enabled = (bool) RadCheckBoxProcessToCompare.Checked;

        if ((bool)!RadCheckBoxProcessToCompare.Checked) { 
            RadComboBoxProcessToCompare.SelectedValue = RadComboBoxProcess.SelectedValue;
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
                        RadComboBoxProcessToCompare.Items.Add(new RadComboBoxItem(text, value));
                    }
                }
                else
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                    Debug.WriteLine("Something went bad.");
                }
            }
        }
    }

    public async void callFilterInformation(object o, RadComboBoxSelectedIndexChangedEventArgs e)
    {
        /*  Add Value to Combobox to Compare */ 
        if(RadCheckBoxProcessToCompare.Checked == false) {
            RadComboBoxProcessToCompare.SelectedValue = RadComboBoxProcess.SelectedValue;
        }

        Debug.WriteLine("getting all info");
        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            if (RadComboBoxProcess.SelectedValue == "")
            {               
                RadComboBoxProcess.SelectedIndex = 0;               
            }
          
           

            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "conformance/" + RadComboBoxProcess.SelectedValue + "/filterInformation").ConfigureAwait(false))
            {

                var status = response.IsSuccessStatusCode;

                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();


                    JObject obj = JsonConvert.DeserializeObject<JObject>(apiResponse);


                    ArrayList moldesList = new ArrayList();
                    ArrayList activitiesList = new ArrayList();
                    ArrayList operatorsList = new ArrayList();


                    foreach (var item in obj["activities"])
                    {
                        
                        activitiesList.Add(item);
                        Debug.WriteLine(item);
                        RadComboBoxActivities.DataSource = activitiesList;
                        RadComboBoxActivities.DataBind();

                        RadComboBoxActivities2.DataSource = activitiesList;
                        RadComboBoxActivities2.DataBind();
                    }

                    foreach (var item in obj["moulds"])
                    {

                        moldesList.Add(item);
                        Debug.WriteLine(item);
                        RadComboBoxMoulds.DataSource = moldesList;
                        RadComboBoxMoulds.DataBind();

                        RadComboBoxMoulds2.DataSource = moldesList;
                        RadComboBoxMoulds2.DataBind();
                    }

                    foreach (var item in obj["resources"])
                    {

                        operatorsList.Add(item);
                        Debug.WriteLine(item);
                        RadComboBoxOperadores.DataSource = operatorsList;
                        RadComboBoxOperadores.DataBind();

                        RadComboBoxOperadores2.DataSource = operatorsList;
                        RadComboBoxOperadores2.DataBind();
                    }
                }
                else
                {

                    Debug.WriteLine("Something went badinfo.");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                }
            }            
        }
    }


    protected async void GetWorkFlows(string processID)
    {
        errorMessage.InnerText = "";
        showError.Visible = false;
  
       
        /* Modelo BASE Payload */
        var isEstimatedEnd = EstimatedCheckbox.Checked.ToString().ToLower();

        string endDate = null;
        string startDate = null;
        string[] moulds = null;
        string[] activities = null;
        string[] resources = null;

        if (RadDatePicker1.SelectedDate.HasValue && RadDatePicker2.SelectedDate.HasValue)
        {            
            startDate = RadDatePicker1.SelectedDate.Value.ToString("d-M-yyyy");

            endDate = RadDatePicker2.SelectedDate.Value.ToString("d-M-yyyy");
        }

        string mouldsString = RadComboBoxMoulds.Text.ToString();

        if (mouldsString != "")
        {
            moulds = mouldsString.Split(',').Select(p => p.Trim()).ToArray();
        }
     
        string activitiesString = RadComboBoxActivities.Text.ToString();
        if (activitiesString != "")
        {
            activities = activitiesString.Split(',').Select(p => p.Trim()).ToArray();
        }
       
        string resourcesString = RadComboBoxOperadores.Text.ToString();

        if (resourcesString != "")
        {
            resources = resourcesString.Split(',').Select(p => p.Trim()).ToArray();
        }

       
        var payload = new { isEstimatedEnd, moulds , resources, activities, startDate, endDate };

        Debug.WriteLine("Base Payload");
        Debug.WriteLine(JsonConvert.SerializeObject(payload).ToString());

        /* Modelo CASE Payload */
        endDate = null;
        startDate = null;
        moulds = null;
        string nodes = String.Empty;

        if (RadDatePicker3.SelectedDate.HasValue && RadDatePicker4.SelectedDate.HasValue)
        {
            startDate = RadDatePicker3.SelectedDate.Value.ToString("d-M-yyyy");

            endDate = RadDatePicker4.SelectedDate.Value.ToString("d-M-yyyy");
        }

        string mouldsString2 = RadComboBoxMoulds2.Text.ToString();

        if (mouldsString2 != "")
        {
            moulds = mouldsString2.Split(',').Select(p => p.Trim()).ToArray();
        }

        string activitiesString2 = RadComboBoxActivities2.Text.ToString();
       
        if (activitiesString2 != "")
        {
            activities = activitiesString2.Split(',').Select(p => p.Trim()).ToArray();
        }

        string resourcesString2 = RadComboBoxOperadores2.Text.ToString();

        if (resourcesString2 != "")
        {
            resources = resourcesString2.Split(',').Select(p => p.Trim()).ToArray();
        }
 

       
        using (var httpClient = new HttpClient())
        {
            /* TOKEN */
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string threshold = "", workflowURL = "conformance/performance/alpha-miner/model/";

            if (!AlphaRadioBtn.Checked)
            {
                workflowURL = "conformance/performance/heuristic-miner/model/";
                threshold = "?threshold=" + ThresholdSlider.Value.ToString().Replace(",", ".");
                //workflowURL = HeuristicRadioBtn.Checked ? "/conformance/performance/heuristic-miner/model/" : "workflow-network/inductive-miner/processes/";
            }

            /* CONFIG FETCH - PREPARE URL & CONTENT */
            string completeURL = workflowURL + processID + threshold;
            Debug.WriteLine(completeURL);
            HttpContent content = new StringContent(JsonConvert.SerializeObject(payload).ToString(), Encoding.UTF8, "application/json");

            /* FETCH DIAGRAM */
            using (var response = await httpClient.PostAsync(Constants.URL_BACKEND_CONNECTION + completeURL, content).ConfigureAwait(false))
            {
                var status = response.IsSuccessStatusCode;

                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.ReasonPhrase.ToString() == "No Content")
                    {
                        errorMessage.InnerText = "Os filtros escolhidos não tem dados.";
                        showError.Visible = true;
                    }
                    else
                    {
                        processes = apiResponse;
                        Debug.WriteLine(processes);

                        JObject obj = JsonConvert.DeserializeObject<JObject>(apiResponse);
                        if (obj["nodes"] != null)
                        {
                            nodes = obj["nodes"].ToString().Replace("\r\n", "");
                        }
                        //Debug.WriteLine("NODES READ:");
                        //Debug.WriteLine(nodes);
                    }



                                  
                }
                else
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Something went bad");
                    Debug.WriteLine(apiResponse);
                    errorMessage.InnerText = apiResponse;
                    showError.Visible = true;
                }
            }

            //Debug.WriteLine("RETRIEVING CASE MODEL...");
            /* CONFIG FETCH - PREPARE URL & CONTENT */
            completeURL = "conformance/performance/process/" + processID;
            //Debug.WriteLine(completeURL);

            //Debug.WriteLine("Case Payload");
            var payloadCase = new { isEstimatedEnd, moulds, resources, nodes, startDate, endDate };
            //Debug.WriteLine(JsonConvert.SerializeObject(payloadCase).ToString().Replace("\\", "").Replace("  ", "").Replace("\"[", "[").Replace("]\"", "]"));
            content = new StringContent(JsonConvert.SerializeObject(payloadCase).ToString().Replace("\\", "").Replace("  ", "").Replace("\"[", "[").Replace("]\"", "]"), Encoding.UTF8, "application/json");

            /* RETRIEVE CASE DIAGRAM */
            using (var response = await httpClient.PostAsync(Constants.URL_BACKEND_CONNECTION + completeURL, content).ConfigureAwait(false))
            {
                var status = response.IsSuccessStatusCode;

                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    if (response.ReasonPhrase.ToString() == "No Content")
                    {
                        errorMessage.InnerText = "Os filtros escolhidos não tem dados.";
                        showError.Visible = true;
                    }

                    Debug.WriteLine(response.ReasonPhrase.ToString());
                    Debug.WriteLine(apiResponse);

                    processes = "{\"case\": " + apiResponse + ", \"base\": " + processes + "}"; // Add Bases to total processes, so the diagram can be generated
                }
                else
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("Something went bad");
                    Debug.WriteLine(apiResponse);
                    errorMessage.InnerText = apiResponse;
                    showError.Visible = true;
                }
            }

            /* ******************** PROCESS TO COMPARE ******************** */
            if ((bool)RadCheckBoxProcessToCompare.Checked)
            {
                Debug.WriteLine("Process to Compare: " + Constants.URL_BACKEND_CONNECTION + "conformance/deviations/" + processID + "/with/" + RadComboBoxProcessToCompare.SelectedValue);
                string comparation;
                using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "conformance/deviations/" + processID + "/with/" + RadComboBoxProcessToCompare.SelectedValue).ConfigureAwait(false))
                {
                    var status = response.IsSuccessStatusCode;

                    if (status == true)
                    {
                        comparation = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(comparation);
                    }
                    else
                    {
                        Debug.WriteLine("Something went bad with getting a process comparation.");
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine(apiResponse);
                    }
                }
            }
        }
    }

    public void AlphaRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = false;
    }
    public void HeuristicRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = true;
    }
    public void InductiveRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = true;
    }
}

