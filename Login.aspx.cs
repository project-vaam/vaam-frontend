using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class DummyTest : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected async void LogginIn(object sender, LoginCancelEventArgs e)
    {
        e.Cancel = true;

        String username = (Login.FindControl("UserName") as TextBox).Text;
        String password = (Login.FindControl("Password") as TextBox).Text;

        var body = new { username, password };

        var content = new StringContent(JsonConvert.SerializeObject(body).ToString(), Encoding.UTF8, "application/json");
        using (var httpClient = new HttpClient())
        {
            using (var response = await httpClient.PostAsync(Constants.URL_BACKEND_CONNECTION + "login/token/", content))  
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