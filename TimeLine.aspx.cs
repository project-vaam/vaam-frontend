using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class TimeLine : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sessionToken"] == null)
        {
           
            Page.Response.Redirect("Login.aspx", true);
        }
    
        
        if (!IsPostBack)
        {
            /*Task<List<LifetimeEvent>> task = */
            //RadTimeline1.DataSource = task.Result;
            //RadTimeline1.DataBind();
            callMoulds();
            //GetMouldLifetimeEvents();
            
        }
    }

    public async void GetMouldLifetimeEvents(object sender, DropDownListEventArgs e)
    {
        var events = new List<LifetimeEvent>();
        currentMould.InnerText = "Timeline do " + e.Text;
        //mouldTimeline.Text = "You selected " + e.Text;

        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "moulds/" + e.Text + "/events").ConfigureAwait(false))  //http://project-vaam.pt/api/login/token
            {
                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                    events = JsonConvert.DeserializeObject<List<LifetimeEvent>>(apiResponse);
                    foreach(var eventLife in events)
                    {
                        Debug.WriteLine(eventLife.Process.Name);
                    }
                    RadTimeline1.DataSource = events;
                    RadTimeline1.DataBind();
                }
                else
                {
                    Debug.WriteLine("Something went bad.");
                }
            }
        }
    }

    protected async void callMoulds()
    {
       
        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

         
            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "moulds").ConfigureAwait(false))
            {
                Debug.WriteLine(response);

                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);

                    JArray obj = JsonConvert.DeserializeObject<JArray>(apiResponse);

                    ArrayList itemsList = new ArrayList();

                    foreach (JObject item in obj)
                    {
                        string code = item.GetValue("code").ToString();
                        itemsList.Add(code);
                        Debug.WriteLine(code);
                        RadDropDownList1.DataSource = itemsList;
                        RadDropDownList1.DataBind();
                    }
                }
                else
                {
                    Debug.WriteLine("Something went bad.");
                }
            }
        }
    }

    [DataContract]
    public class LifetimeEvent
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public MouldPart Part { get; set; }
        [DataMember]
        public ProcessLifeEvent Process { get; set; }
        [DataMember]
        public ActivityLifeEvent Activity { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public int? Duration { get; set; }

    }

    [DataContract]
    public class ProcessLifeEvent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public String StartDate { get; set; }
        [DataMember]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public int NumberOfCases { get; set; }
        [DataMember]
        public int NumberOfActivities { get; set; }
    }

    [DataContract]
    public class ActivityLifeEvent
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    public class MouldPart
    {
        [DataMember]
        public string Code { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string TagRfid { get; set; }
    }

}