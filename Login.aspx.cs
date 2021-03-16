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
            using (var response = await httpClient.PostAsync("http://localhost:8080/api/login/token", content))
            {
                string apiResponse = await response.Content.ReadAsStringAsync();
                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    var obj = JObject.Parse(apiResponse);

                    var token = (string)obj["token"];

                    //store token on session storage
                    //HttpContext.Session.SetString("sessionKey", token);

                    LabelResult.Text = "Welcome back, " + username + ". <br />" + "Your token is: " + token.ToString();
                    LabelResult.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    LabelResult.Text = "Someting went wrong";
                    LabelResult.ForeColor = System.Drawing.Color.Red;
                }





                /*
                try
                {

                    var body = new { username = username, password = password };

                    using (var client = new WebClient())
                    {
                        //LabelResult.Text = "";
                        var dataString = JsonConvert.SerializeObject(body);
                        client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

                        String responseStr = client.UploadString(new Uri("http://localhost:8080/api/login/token"), "POST", dataString); //localhost:8080/api/login/token

                        JObject jsonResponse = JObject.Parse(responseStr);

                        var token = jsonResponse["token"].ToString();           

                        LabelResult.Text = "Welcome back, " + username + ". <br />" + "Your token is: " + token.ToString();
                        LabelResult.ForeColor = System.Drawing.Color.Green;

                    }
                }

                catch (WebException exception)
                {
                    LabelResult.Text = "Someting went wrong, " + exception.Message;
                    LabelResult.ForeColor = System.Drawing.Color.Red;
                } */
            }
        }
    }
}