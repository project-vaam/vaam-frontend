using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Cytoscape_POC : System.Web.UI.Page
{
    public string processes = "";

    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (!IsPostBack)
        {         
            GetWorkFlows();
        }
    }

    protected async void GetWorkFlows()
    {   
        using (var httpClient = new HttpClient())
        {
            string token = (string)Session["sessionToken"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "workflow-network/alpha-miner/processes/25").ConfigureAwait(false))
            {
                Debug.WriteLine(response);

                var status = response.IsSuccessStatusCode;
                if (status == true)
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();

                    JObject jsonResponse = JObject.Parse(apiResponse);
                    Debug.WriteLine(jsonResponse);

                    processes = apiResponse;
                }
                else
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(apiResponse);
                    
                }
            }
        }
    }

    
}