using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DummyTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected async void Login2_LogginIn(object sender, LoginCancelEventArgs e)
    {
        e.Cancel = true;

        String username = (Login2.FindControl("UserName") as TextBox).Text;
        String password = (Login2.FindControl("Password") as TextBox).Text;

        var body = new { username = username, password = password };

        var content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.PostAsync("http://localhost:8080/api/login/token", content))  //http://project-vaam.pt/api/login/token
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    var obj = JObject.Parse(apiResponse);

                    var token = (string)obj["token"];

                    Session.Add("sessionToken", token);
                    Session.Add("username", username);

                    var sessionToken = Session["sessionToken"];

                    System.Diagnostics.Debug.WriteLine("ola");

                    Response.Redirect("Default.aspx", false);
                }
                else
                {
                    LabelResult.Text = "Someting went wrong";
                    LabelResult.ForeColor = System.Drawing.Color.Red;
                }
            }
        }
    }
}