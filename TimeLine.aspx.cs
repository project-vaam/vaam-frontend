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
            Task<List<LifetimeEvent>> task = GetMouldLifetimeEvents();
            //foreach(var elem in task.Result)
            //{
            //    aux += elem.Id.ToString();
            //}
            //MouldsLifetimeLabel.Text = aux;
            RadTimeline1.DataSource = task.Result;
            //RadTimeline1.DataSource = GetBooks();
            RadTimeline1.DataBind();
        }
    }

    public async Task<List<LifetimeEvent>> GetMouldLifetimeEvents()
    {
        Debug.WriteLine("Calling API...");
        var events = new List<LifetimeEvent>();

        //var content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json");

        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Debug.WriteLine("Debug");

            using (var response = await httpClient.GetAsync("http://project-vaam.pt/api/moulds/mouldA/events").ConfigureAwait(false))  //http://project-vaam.pt/api/login/token
            {
                Debug.WriteLine(response);

                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                    MouldsLifetimeLabel.Text = apiResponse.ToString();
                    events = JsonConvert.DeserializeObject<List<LifetimeEvent>>(apiResponse);
                    Debug.WriteLine("Response:");
                    Debug.WriteLine(events);


                }
                else
                {
                    Debug.WriteLine("Something went bad.");
                }
            }
        }

        return events;
    }

    [DataContract]
    public class LifetimeEvent
    {
        [DataMember]
        public int Id { get; set; }
        public string ProcessDescription { get; set; }
        public string ActivityName { get; set; }
        public string ActivityDescription { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public int Duration { get; set; }

    }


    public List<AmazonBook> GetBooks()
    {
        var books = new List<AmazonBook>();
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2013, 5, 1),
            Title = "Professional ASP.NET 4.5 in C# and VB",
            Author = "Jason N. Gaylord",
            Cover = "http://ecx.images-amazon.com/images/I/51MQP2rwsZL.jpg",
            Url = "http://www.amazon.com/Professional-ASP-NET-4-5-C-VB/dp/1118311825"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2013, 7, 15),
            Title = "Pro ASP.NET 4.5 in C#",
            Author = "Adam Freeman",
            Cover = "http://ecx.images-amazon.com/images/I/51h6duC3kmL.jpg",
            Url = "http://www.amazon.com/Pro-ASP-NET-4-5-Adam-Freeman/dp/143024254X"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2012, 10, 24),
            Title = "Beginning ASP.NET 4.5 in C#",
            Author = "Matthew MacDonald",
            Cover = "https://images-na.ssl-images-amazon.com/images/I/5188%2B7tmzIL._SX403_BO1,204,203,200_.jpg",
            Url = "https://www.amazon.com/Beginning-ASP-NET-Experts-Voice-Net-ebook/dp/B00FB2PZLG"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2014, 3, 24),
            Title = "Beginning ASP.NET 4.5.1: in C# and VB ",
            Author = "Imar Spaanjaars",
            Cover = "http://ecx.images-amazon.com/images/I/51xvkzeTRbL.jpg",
            Url = "http://www.amazon.com/Beginning-ASP-NET-4-5-1-Wrox-Programmer/dp/111884677X"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2013, 3, 20),
            Title = "Pro C# 5.0 and the .NET 4.5 Framework",
            Author = "Andrew Troelsen",
            Cover = "http://ecx.images-amazon.com/images/I/7165No4MIpL._SL1500_.jpg",
            Url = "http://www.amazon.com/Beginning-ASP-NET-Databases-Sandeep-Chanda/dp/1430243805"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2012, 7, 24),
            Title = "Ultra-Fast ASP.NET 4.5",
            Author = "Rick Kiessig",
            Cover = "http://ecx.images-amazon.com/images/I/51Pu1Z8pgsL.jpg",
            Url = "http://www.amazon.com/Ultra-Fast-ASP-NET-Experts-Voice-ASP-Net/dp/1430243384"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2016, 1, 28),
            Title = "ASP.NET 4.5 Unleashed",
            Author = "Stephen Walther",
            Cover = "http://ecx.images-amazon.com/images/I/41V4tb3L%2BFL.jpg",
            Url = "http://www.amazon.com/ASP-NET-4-5-Unleashed-Stephen-Walther/dp/067233688X"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2012, 12, 21),
            Title = "Pro ASP.NET MVC 4",
            Author = "Adam Freeman",
            Cover = "http://ecx.images-amazon.com/images/I/51mKVgdmZpL.jpg",
            Url = "http://www.amazon.com/Pro-ASP-NET-MVC-Adam-Freeman/dp/1430242361"
        });
        books.Add(new AmazonBook
        {
            ReleaseDate = new DateTime(2014, 2, 24),
            Title = "Professional C# 5.0 and .NET 4.5.1",
            Author = "Christian Nagel",
            Cover = "http://ecx.images-amazon.com/images/I/516-BPVWURL.jpg",
            Url = "http://www.amazon.com/Professional-C-5-0-NET-4-5-1/dp/1118833031"
        });

        return books;
    }
    public class AmazonBook
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string Cover { get; set; }
        public string Url { get; set; }
        public DateTime ReleaseDate { get; set; }
    }


}