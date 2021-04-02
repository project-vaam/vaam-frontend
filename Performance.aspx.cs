using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Performance : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        //var TheBrowserWidth = width.Value;
        //var TheBrowserHeight = height.Value;

        callWorkflows();

        Debug.WriteLine("tou");
        Debug.WriteLine(width.Value);
        // General diagram settings
        //RadDiagram1.Width = 700;
        //RadDiagram1.Height = 800;


        RadDiagram1.ShapeDefaultsSettings.Width = 310;
        RadDiagram1.ShapeDefaultsSettings.Height = 30;

        // User Permition Settings
        RadDiagram1.Selectable = false;
        RadDiagram1.Pannable = false;
        RadDiagram1.Editable = false;

        // Layout settings
        RadDiagram1.LayoutSettings.Enabled = true;
        RadDiagram1.LayoutSettings.Type = Telerik.Web.UI.Diagram.LayoutType.Layered;
        RadDiagram1.LayoutSettings.Subtype = Telerik.Web.UI.Diagram.LayoutSubtype.Down;
        RadDiagram1.LayoutSettings.VerticalSeparation = 30;
        RadDiagram1.LayoutSettings.HorizontalSeparation = 30;


    }

    protected void AddDiagramShape(string shapeID, string contentText, RadDiagram diagram)
    {
        var shape = new DiagramShape()
        {
            Id = shapeID,
        };
        
        shape.ContentSettings.Text = contentText;


        //shape.Width = 150;
        shape.StrokeSettings.DashType = Telerik.Web.UI.Diagram.StrokeDashType.Solid;
        shape.StrokeSettings.Color = Color.Black.ToString();
        shape.StrokeSettings.Width = 1.2;

        shape.ContentSettings.Color = Color.White.ToString();
        //shape.FillSettings.Color = backgroundColor;
        diagram.ShapesCollection.Add(shape);
    }

    protected void ConnectDiagramShapes(string startShapeID, string endShapeID, string textConnection, string colorHEXConnection, int widthConnection, RadDiagram diagram)
    {
        var connection = new DiagramConnection();

        /* Settings */
        connection.FromSettings.ShapeId = startShapeID;
        connection.ToSettings.ShapeId = endShapeID;
        connection.StrokeSettings.Color = "#000";
        connection.StrokeSettings.Width = 1.2;

        /*  Params */

        if (widthConnection != -1) connection.StrokeSettings.Width = widthConnection;
        if(colorHEXConnection != string.Empty) connection.StrokeSettings.Color = colorHEXConnection;
        if(textConnection != string.Empty) connection.ContentSettings.Text = textConnection;

        diagram.ConnectionsCollection.Add(connection);
    }

    protected async void callWorkflows()
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

                    var nodes = jsonResponse["nodes"];

                    /* DEBUG JSON */
                    APIResult.Text = jsonResponse["relations"].ToString();

                    var statistics = jsonResponse["statistics"];

                                 
                    foreach (var node in nodes.Select((value, i) => new { i, value }))
                    {
                        JToken meanDuration = new JObject();

                        foreach(var nodeStats in statistics["nodes"])
                        {
                            if(node.i == int.Parse(nodeStats["node"].ToString()))
                            {
                                meanDuration = nodeStats["meanDuration"];
                                break;
                            }
                        }


                        string diagramTitle = node.value + " ( " + displayDuration(meanDuration) + ")";

                        AddDiagramShape(node.i.ToString(), diagramTitle, RadDiagram1);                       
                                                               
                    }

                    foreach (var relation in statistics["relations"].Select((value, i) => new { i, value }))
                    {
                                                                  
                        foreach (var destination in relation.value["to"])
                        {                           
                            Debug.WriteLine(destination["node"]);
                            ConnectDiagramShapes(relation.value["from"].ToString(), destination["node"].ToString(), displayDuration(destination["meanDuration"]), "",-1, RadDiagram1);
                        }                       
                    }
                }
                else
                {
                    Debug.WriteLine("Something went bad.");

                  
                }
            }
        }
    }

    //public static class Nodes
    //{
    //    public static Node Nadal = new Node() { Name = "R. Nadal", Id = "nadal", Color = "#f18100" };
    //    public static Node Djokovic = new Node() { Name = "N. Djokovic", Id = "djokovic", Color = "#8cb20f" };
    //    public static Node Youzhny = new Node() { Name = "M. Youzhny", Id = "youzhny", Color = "#ae5e08" };
    //    public static Node Murray = new Node() { Name = "A. Murray", Id = "murray", Color = "#d75234" };
    //    public static Node Wawrinka = new Node() { Name = "S. Wawrinka", Id = "wawrinka", Color = "#f8c43a" };
    //    public static Node Gasquet = new Node() { Name = "R. Gasquet", Id = "gasquet", Color = "#5f9fee" };
    //    public static Node Ferrer = new Node() { Name = "D. Ferrer", Id = "ferrer", Color = "#1958a6" };
    //    public static Node Robredo = new Node() { Name = "T. Robredo", Id = "Robredo", Color = "#6da000" };
    //}

    //public class Workflow
    //{
    //    public string Name { get; set; }

    //    public IList<Node> Node { get; set; }
    //}

    //public class Node
    //{
    //    public string Name { get; set; }
    //    public string Id { get; set; }
    //    public string Color { get; set; }
    //}

    public string displayDuration(JToken duration)
    {
        return (int.Parse(duration["days"].ToString()) > 0 ? duration["days"] + " d " : "") +
                            (int.Parse(duration["hours"].ToString()) > 0 ? duration["hours"] + " h " : "") +
                            (int.Parse(duration["minutes"].ToString()) > 0 ? duration["minutes"] + " min " : "") +
                            (int.Parse(duration["seconds"].ToString()) > 0 ? duration["seconds"] + " seg " : "") +
                            (int.Parse(duration["millis"].ToString()) > 0 ? duration["millis"] + " ms " : "");
    }
}
