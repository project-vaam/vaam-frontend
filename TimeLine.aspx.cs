using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

public partial class TimeLine : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["sessionToken"] == null)
        {
           
            Page.Response.Redirect("Login.aspx", true);
        }
    
        
        if (!IsPostBack)
        {
            displayMould.Visible = false;
            RadTimeline1.Visible = false;
            callMoulds();
        }
    }
    public async void GetMouldLifetimeEvents(object sender, RadComboBoxSelectedIndexChangedEventArgs molde)
    {
        var events = new List<LifetimeEvent>();
        displayMould.Visible = true;    
        currentMould.InnerText = "Timeline do " + molde.Text;
        Debug.WriteLine("molde:" + molde.Text);

        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "moulds/" + molde.Text + "/eventsUsersWorkstations").ConfigureAwait(false)) 
            {
                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine("response:");
                    Debug.WriteLine(apiResponse);

                    try
                    {
                        DisplayError.InnerText = "";
                        events = JsonConvert.DeserializeObject<List<LifetimeEvent>>(apiResponse, new JsonSerializerSettings
                        {
                            MissingMemberHandling = MissingMemberHandling.Ignore // For Empty Arrays
                        });

                        foreach (var eventLife in events)
                        {
                            Debug.WriteLine(eventLife.AverageEventDurationForActivityMillis);
                        }

                        RadTimeline1.DataSource = events;
                        RadTimeline1.DataBind();


                        RadTimeline1.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        RadTimeline1.Visible = false;
                        //DisplayError.InnerText = "Timeline do " + molde.Text + " não se encontra disponível para visualização.";
                        DisplayError.InnerText = ex.Message;
                       
                    }
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
                        RadComboBoxMoldes.Items.Add(new RadComboBoxItem(code, code));
                        

                      
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
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? StartDate{ get; set; }
        [DataMember]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public long? Duration { get; set; }
        [DataMember]
        public float? AverageEventDurationForActivityMillis { get; set; }
        [DataMember]
        public WorkersActivity[] ActivityUserEntry { get;  set; }

    }

    [DataContract]
    public class ProcessLifeEvent
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? StartDate { get; set; }
        [DataMember]
        [JsonConverter(typeof(CustomDateTimeConverter))]
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


    [DataContract]
    public class WorkersActivity
    {
        [DataMember]
        public User User { get; set; }
        [DataMember]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? StartDate { get; set; }
        [DataMember]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime? EndDate { get; set; }
        [DataMember]
        public Workstation Workstation { get; set; }
        [DataMember]
        public long TimeSpentMillis { get; set; }
    }

    [DataContract]
    public class User
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        public String Role { get; set; }
        [DataMember]
        public String Email { get; set; }
    }
    [DataContract]
    public class Workstation
    {
        [DataMember]
        public String Name { get; set; }
    }

}

public class CustomDateTimeConverter : IsoDateTimeConverter
{
    public CustomDateTimeConverter()
    {
        base.DateTimeFormat = "dd-MM-yyyy HH:mm:ss.fff";
    }
}