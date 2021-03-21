using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class DropDownTest : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindToArrayList(RadDropDownList1);

        }
    }

    private void BindToArrayList(Telerik.Web.UI.RadDropDownList dropDownList)
    {
        ArrayList itemsList = new ArrayList();
        itemsList.Add("One");
        itemsList.Add("Two");
        itemsList.Add("Three");
        dropDownList.DataSource = itemsList;
        dropDownList.DataBind();
    }




    protected async void callMouldss(object sender, EventArgs e)
    {
        Debug.WriteLine("Calling API...");
        
        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            Debug.WriteLine("IM HERE");
            Debug.WriteLine("Debug");

            using (var response = await httpClient.GetAsync("http://localhost:8080/api/moulds").ConfigureAwait(false))
            {
                Debug.WriteLine(response);

                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                    MouldsListLabel.Text = apiResponse.ToString();

                    

                    //Debug.WriteLine(jsonResponse);

                    //var token = jsonResponse["code"].ToString();


                }
                else
                {
                    Debug.WriteLine("Something went bad.");
                }
            }
        }
    }
}

    