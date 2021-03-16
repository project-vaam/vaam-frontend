using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            try
            {
                /*
                WebClient client = new WebClient();           

                string url = "http://api.icndb.com/jokes/random?fbclid=IwAR0STGvv9kGc2T9TXhkMXTlASwG-NBP_5wmmHSZoyciDx-ill0ueyvnNzFg";

                Stream stream = client.OpenRead(url);
                StreamReader reader = new StreamReader(stream);
                JObject jObject = JObject.Parse(reader.ReadLine());

                Label1.Text = jObject.ToString(); */

                var body = new { username = "admin", password = "123" };

                using (var client = new WebClient())
                {
                    var dataString = JsonConvert.SerializeObject(body);
                    client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                    String responseStr = client.UploadString(new Uri("http://project-vaam.pt/api/login/token"), "POST", dataString);

                    JObject jsonResponse = JObject.Parse(responseStr);

                    var token = jsonResponse["token"].ToString();

                    Label1.Text = token.ToString();
                }


            }

            catch (Exception ex)
            {
                Label1.Text = ex.ToString();
            }
        }
    }
}