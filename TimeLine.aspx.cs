using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
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

public partial class TimeLine : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sessionToken"] == null)
        {
           
            Page.Response.Redirect("Default.aspx", true);
        }
    
        
        if (!IsPostBack)
        {
            /*Task<List<LifetimeEvent>> task = */GetMouldLifetimeEvents();
            //RadTimeline1.DataSource = task.Result;
            //RadTimeline1.DataBind();
        }
    }

    public async void GetMouldLifetimeEvents()
    {
        var events = new List<LifetimeEvent>();

        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "moulds/mouldA/events").ConfigureAwait(false))  //http://project-vaam.pt/api/login/token
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