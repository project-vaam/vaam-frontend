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

    protected async void Login2_LogginIn(object sender, LoginCancelEventArgs e)
    {
        e.Cancel = true;

        String username = (Login2.FindControl("UserName") as TextBox).Text;
        String password = (Login2.FindControl("Password") as TextBox).Text;

        var body = new { username = username, password = password };

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

                    LabelResult.Text = "Welcome back, " + username + ". <br />" + "Your token is: " + token.ToString() + "< br />" + "The token in Session is: " + sessionToken; 
                    LabelResult.ForeColor = System.Drawing.Color.Green;

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