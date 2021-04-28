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
            callProcesses();
            System.Threading.Thread.Sleep(500);
            callFilterInformation(null, null);
        }
    }

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

        //if (!tbDuration.Value.HasValue)
        //{
        //    tbDuration.Value = RadDatePicker1.ShowAnimation.Duration;
        //}
        //else
        //{
        //    RadDatePicker1.ShowAnimation.Duration = (int)tbDuration.Value.Value;
        //    RadDatePicker1.HideAnimation.Duration = (int)tbDuration.Value.Value;
        //    RadDatePicker1.Calendar.FastNavigationSettings.ShowAnimation.Duration = (int)tbDuration.Value.Value;
        //    RadDatePicker1.Calendar.FastNavigationSettings.HideAnimation.Duration = (int)tbDuration.Value.Value;

        //    RadDatePicker2.ShowAnimation.Duration = (int)tbDuration.Value.Value;
        //    RadDatePicker2.HideAnimation.Duration = (int)tbDuration.Value.Value;
        //    RadDatePicker2.Calendar.FastNavigationSettings.ShowAnimation.Duration = (int)tbDuration.Value.Value;
        //    RadDatePicker2.Calendar.FastNavigationSettings.HideAnimation.Duration = (int)tbDuration.Value.Value;
        //}
    }


    protected void ShowDiagram_Click(object sender, EventArgs e)
    {
        GetWorkFlows(RadDropDownList4.SelectedValue);
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
                        string text = item["name"].ToString();
                        RadDropDownList4.Items.Add(new DropDownListItem(text, value));
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

    public async void callFilterInformation(object sender, DropDownListEventArgs molde)
    {
        Debug.WriteLine("getting all info");
        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

           
            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "conformance/" + RadDropDownList4.SelectedValue  + "/filterInformation").ConfigureAwait(false))
            {

                var status = response.IsSuccessStatusCode;

                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();



                    JObject obj = JsonConvert.DeserializeObject<JObject>(apiResponse);



                    ArrayList moldesList = new ArrayList();
                    ArrayList activitiesList = new ArrayList();
                    ArrayList operatorsList = new ArrayList();

                    Debug.WriteLine(obj["resources"]);



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

                    Debug.WriteLine("Something went bad.");
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                }
            }
        }
    }



    /*Funcao do code behind para fazer os pedidos, tudo o resto pode ir com o caralho, alterar de forma a receber os novos inputs"*/
    protected async void GetWorkFlows(string processID) //chamar no load Page
    {
        errorMessage.InnerText = "";
        showError.Visible = false;



        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            string json = "{ \"isEstimatedEnd\":" + "false";
            json += " }";

        HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

            string threshold = "", workflowURL = "conformance/performance/alpha-miner/model/";

            if (!AlphaRadioBtn.Checked)
            {
                workflowURL = "conformance/performance/heuristic-miner/model/";
                threshold = "?threshold=" + ThresholdSlider.Value.ToString().Replace(",",".");
                //workflowURL = HeuristicRadioBtn.Checked ? "/conformance/performance/heuristic-miner/model/" : "workflow-network/inductive-miner/processes/";
            }


            string completeURL = workflowURL + processID + threshold;
            Debug.WriteLine(completeURL);

            using (var response = await httpClient.PostAsync(Constants.URL_BACKEND_CONNECTION + completeURL, content).ConfigureAwait(false))
            {
               

                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                           

                    processes = "{data: " + apiResponse + "}";
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
    }
    public void HeuristicRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = true;
    }
    public void InductiveRadioBtn_Click(object sender, EventArgs e)
    {
        thresholdField.Visible = true;
    }
    //public async void callWorkflows(RadDropDownList dropdownlist)
    //{

    //    displayProcess.Visible = true;
    //    currentProcess.InnerText = "Performance do processo " + dropdownlist.SelectedText;

    //    //resets the diagram
    //    DisplayError.InnerText = "";
    //    RadDiagram1.ShapesCollection.Clear();
    //    RadDiagram1.ConnectionsCollection.Clear();

    //    using (var httpClient = new HttpClient())
    //    {
    //        string token = (string)Session["sessionToken"];
    //        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


    //        using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "workflow-network/alpha-miner/processes/" + dropdownlist.SelectedValue).ConfigureAwait(false))
    //        {
    //            Debug.WriteLine(response);

    //            var status = response.IsSuccessStatusCode;
    //            if (status == true)
    //            {
    //                string apiResponse = await response.Content.ReadAsStringAsync();

    //                JObject jsonResponse = JObject.Parse(apiResponse);

    //                var nodes = jsonResponse["nodes"];
    //                var statistics = jsonResponse["statistics"];

    //                foreach (var node in nodes.Select((value, i) => new { i, value }))
    //                {
    //                    JToken meanDuration = new JObject();

    //                    foreach (var nodeStats in statistics["nodes"])
    //                    {
    //                        if (node.i == int.Parse(nodeStats["node"].ToString()))
    //                        {
    //                            meanDuration = nodeStats["meanDuration"];
    //                            break;
    //                        }
    //                    }


    //                    string diagramTitle = node.value + " ( " + displayDuration(meanDuration) + ")";
    //                    string color = shapesColor(meanDuration);

    //                    AddDiagramShape(node.i.ToString(), diagramTitle, color, RadDiagram1);

    //                }

    //                foreach (var relation in statistics["relations"].Select((value, i) => new { i, value }))
    //                {

    //                    foreach (var destination in relation.value["to"])
    //                    {
    //                        string color = "";
    //                        int width = 1;

    //                        JToken duration = new JObject();
    //                        duration = destination["meanDuration"];


    //                        if (int.Parse(duration["days"].ToString()) != 0) //demora muito tempo
    //                        {
    //                            color = "#FC0000";  //red
    //                            width = 5;

    //                        }
    //                        else if (int.Parse(duration["hours"].ToString()) != 0)//depende de quantas horas demora
    //                        {
    //                            if (int.Parse(duration["hours"].ToString()) >= 3)
    //                            {
    //                                color = "#FC0000";  //red, demora muito tempo
    //                                width = 10;
    //                            }
    //                            else if (int.Parse(duration["hours"].ToString()) >= 1 && int.Parse(duration["hours"].ToString()) <= 3) //demora entre 1 e 3 horas
    //                            {
    //                                color = "#FF7A7A"; //low red demora tempo media
    //                                width = 7;
    //                            }
    //                        }
    //                        else //apenas demora alguns minutos
    //                        {
    //                            color = "#FFCDCD"; //white  o tempo esta ok
    //                            width = 5;
    //                        }

    //                        ConnectDiagramShapes(relation.value["from"].ToString(), destination["node"].ToString(), displayDuration(destination["meanDuration"]), color, width, RadDiagram1);
    //                    }
    //                }
    //            }
    //            else
    //            {
    //                string apiResponse = await response.Content.ReadAsStringAsync();
    //                //int numericStatusCode = (int)response.StatusCode;
    //                //Debug.WriteLine(numericStatusCode);
    //                Debug.WriteLine(apiResponse);


    //                try
    //                {
    //                    DisplayError.InnerText = JObject.Parse(apiResponse)["message"].ToString();
    //                }
    //                catch (Exception ex)
    //                {
    //                    DisplayError.InnerText = apiResponse;
    //                }

    //                //DisplayError.InnerText = IsValidJson(apiResponse) ? JObject.Parse(apiResponse)["message"].ToString() : apiResponse;
    //            }
    //        }
    //    }
    //}

    //protected void AddDiagramShape(string shapeID, string contentText,string color, RadDiagram diagram)
    //{
    //    var shape = new DiagramShape()
    //    {
    //        Id = shapeID,
    //    };

    //    shape.ContentSettings.Text = contentText;

    //    //shape.Width = 150;
    //    shape.StrokeSettings.DashType = Telerik.Web.UI.Diagram.StrokeDashType.Solid;
    //    shape.StrokeSettings.Color = Color.Black.ToString();
    //    shape.StrokeSettings.Width = 1.2;

    //    shape.ContentSettings.Color = Color.White.ToString();
    //    shape.FillSettings.Color = color;
    //    diagram.ShapesCollection.Add(shape);
    //}

    //protected void ConnectDiagramShapes(string startShapeID, string endShapeID, string textConnection, string colorHEXConnection, int widthConnection, RadDiagram diagram)
    //{
    //    var connection = new DiagramConnection();

    //    /* Settings */
    //    connection.FromSettings.ShapeId = startShapeID;
    //    connection.ToSettings.ShapeId = endShapeID;       
    //    connection.StrokeSettings.Color = colorHEXConnection;

    //    connection.StrokeSettings.Width = widthConnection;

    //    /*  Params */
    //    if(widthConnection != -1) connection.StrokeSettings.Width = widthConnection;
    //    if(colorHEXConnection != string.Empty) connection.StrokeSettings.Color = colorHEXConnection;
    //    if(textConnection != string.Empty) connection.ContentSettings.Text = textConnection;

    //    diagram.ConnectionsCollection.Add(connection);
    //}

    //public string displayDuration(JToken duration)
    //{
    //    return (int.Parse(duration["days"].ToString()) > 0 ? duration["days"] + " d " : "") +
    //                        (int.Parse(duration["hours"].ToString()) > 0 ? duration["hours"] + " h " : "") +
    //                        (int.Parse(duration["minutes"].ToString()) > 0 ? duration["minutes"] + " min " : "") +
    //                        (int.Parse(duration["seconds"].ToString()) > 0 ? duration["seconds"] + " seg " : "") +
    //                        (int.Parse(duration["millis"].ToString()) > 0 ? duration["millis"] + " ms " : "");
    //}

    //public string shapesColor(JToken duration)
    //{
    //    string color = "";

    //    if (int.Parse(duration["days"].ToString()) != 0) //demora muito tempo
    //    {
    //        color = "#FC0000";  //red

    //    }
    //    else if (int.Parse(duration["hours"].ToString()) != 0)//depende de quantas horas demora
    //    {
    //        if (int.Parse(duration["hours"].ToString()) >= 3)
    //        {
    //            color = "#FC0000";  //red, demora muito tempo
    //        }
    //        else if (int.Parse(duration["hours"].ToString()) >= 1 && int.Parse(duration["hours"].ToString()) <= 3) //demora entre 1 e 3 horas
    //        {
    //            color = "#FF9494"; //low red demora tempo media
    //        }
    //    }
    //    else //apenas demora alguns minutos
    //    {
    //        color = "#FFFFFF"; //white  o tempo esta ok
    //    }

    //    return color;
    //}


    //public Tuple<string, int> connectionsStyle(JToken duration) //n funca assim :( Tuple n existe na nossa versao de c# shall we update ? :0
    //{
    //    string color = "";
    //    int width = 1;


    //    if (int.Parse(duration["days"].ToString()) != 0) //demora muito tempo
    //    {
    //        color = "#FC0000";  //red
    //        width = 5;

    //    }
    //    else if (int.Parse(duration["hours"].ToString()) != 0)//depende de quantas horas demora
    //    {
    //        if (int.Parse(duration["hours"].ToString()) >= 3)
    //        {
    //            color = "#FC0000";  //red, demora muito tempo
    //            width = 5;
    //        }
    //        else if (int.Parse(duration["hours"].ToString()) >= 1 && int.Parse(duration["hours"].ToString()) <= 3) //demora entre 1 e 3 horas
    //        {
    //            color = "#FF9494"; //low red demora tempo media
    //            width = 3;
    //        }
    //    }
    //    else //apenas demora alguns minutos
    //    {
    //        color = "#FFFFFF"; //white  o tempo esta ok
    //        width = 1;
    //    }

    //    return (color, width);
    //}    


}

