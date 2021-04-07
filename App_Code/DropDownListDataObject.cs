using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;

/// <summary>
/// Summary description for DropDownListDataObject
/// </summary>
public class DropDownListDataObject : Page
{
   

    public DropDownListDataObject()
    {
             
    }


    public List<DropDownListDataItem> GetItems()
    {

        using (var client = new WebClient())
        {
                      
            client.Headers.Add(HttpRequestHeader.ContentType, "application/json");

            string token = (string)Session["sessionToken"];
            client.Headers[HttpRequestHeader.Authorization] = "Bearer " + token;

            String responseStr = client.DownloadString(new Uri(Constants.URL_BACKEND_CONNECTION + "processes"));

            Debug.WriteLine(responseStr);

            JArray jsonResponse = JsonConvert.DeserializeObject<JArray>(responseStr);
            List<DropDownListDataItem> itemsList = new List<DropDownListDataItem>();

            foreach (JObject item in jsonResponse)
            {
                int processId = (int)item["id"];
                string processName = item["name"].ToString();

                itemsList.Add(new DropDownListDataItem(processId, processName));
                Debug.WriteLine("Na func Lista com: " + itemsList.Count);

               
            }
            return itemsList;
        }


        /* tentatica com httpClient fuck the task man */


        //using (var httpClient = new HttpClient())
        //{

        //    //string token = (string)HttpContext.Current.Session["sessionToken"];
        //    string sessionID = Session["sessionToken"].ToString();
        //    //Debug.WriteLine("TOKEN" + token);

        //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessionID);


        //    using (var response = await httpClient.GetAsync(Constants.URL_BACKEND_CONNECTION + "processes").ConfigureAwait(false))
        //    {

        //        var status = response.IsSuccessStatusCode;
        //        if (status == true)
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();

        //            Debug.WriteLine(apiResponse);

        //            JArray obj = JsonConvert.DeserializeObject<JArray>(apiResponse);

        //            Debug.WriteLine(obj);

        //            foreach (JObject item in obj)
        //            {
        //                int processId = (int)item["id"];
        //                string processName = item["name"].ToString();

        //                itemsList.Add(new DropDownListDataItem(processId, processName));
        //                Debug.WriteLine("Na func Lista com: " + itemsList.Count);

        //            }
        //        }
        //        else
        //        {
        //            Debug.WriteLine(response.StatusCode);

        //        }
        //    }

        //}
    }

    //public async Task<List<DropDownListDataItem>> GetItemsAsync()
    //{

    //    await Task.Run(() => CallApi());


    //    Debug.WriteLine("Lista com: " + itemsList.Count);

    //    return itemsList;
    //}


    /*Exemplo do telerik*/
    //public static List<DropDownListDataItem> GetItems()
    //{
    //    List<DropDownListDataItem> itemsList = new List<DropDownListDataItem>();
    //    itemsList.Add(new DropDownListDataItem(1, "item 1"));
    //    itemsList.Add(new DropDownListDataItem(2, "item 2"));
    //    itemsList.Add(new DropDownListDataItem(3, "item 3"));
    //    return itemsList;
    //}

    public class DropDownListDataItem
    {
        private string _text;
        private int _id;
        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
            }
        }
        public int ID
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }
        public DropDownListDataItem(int id, string text)
        {
            _id = id;
            _text = text;
        }
    }
}