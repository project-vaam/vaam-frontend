using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class Performance : System.Web.UI.Page
{

    RadDiagram RadDiagram1 = new RadDiagram();


    protected void Page_Load(object sender, EventArgs e)
    {
        var TheBrowserWidth = width.Value;
        var TheBrowserHeight = height.Value;

        callWorkflows();
      

        // General diagram settings
        RadDiagram1.Width = new System.Web.UI.WebControls.Unit(width.Value); 
        RadDiagram1.Height = new System.Web.UI.WebControls.Unit(height.Value);

        RadDiagram1.ShapeDefaultsSettings.Width = 140;
        RadDiagram1.ShapeDefaultsSettings.Height = 30;
        RadDiagram1.ShapeDefaultsSettings.StrokeSettings.Color = "#fff";
        Form.Controls.Add(RadDiagram1);

        // Layout settings
        RadDiagram1.LayoutSettings.Enabled = true;
        RadDiagram1.LayoutSettings.Type = Telerik.Web.UI.Diagram.LayoutType.Layered;
        RadDiagram1.LayoutSettings.Subtype = Telerik.Web.UI.Diagram.LayoutSubtype.Right;
        RadDiagram1.LayoutSettings.VerticalSeparation = 20;
        RadDiagram1.LayoutSettings.HorizontalSeparation = 30;

       
      
    }

    protected void AddDiagramShape(string shapeID, string contentText, RadDiagram diagram)
    {
        var shape = new DiagramShape()
        {
            Id = shapeID,
        };
        shape.ContentSettings.Text = contentText;
        //shape.ContentSettings.Color = contentColor;
        //shape.FillSettings.Color = backgroundColor;
        diagram.ShapesCollection.Add(shape);
    }

    protected void ConnectDiagramShapes(string startShapeID, string endShapeID, RadDiagram diagram)
    {
        var connection = new DiagramConnection();
        connection.FromSettings.ShapeId = startShapeID;
        connection.ToSettings.ShapeId = endShapeID;
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

                    var relations = jsonResponse["relations"];

                    APIResult.Text = relations.ToString();                 

                    foreach (var node in nodes.Select((value, i) => new { i, value }))
                    {
                        AddDiagramShape(node.i + "", node.value + " - " + node.i, RadDiagram1);                       
                                                               
                    }

                    foreach (var relation in relations.Select((value, i) => new { i, value }))
                    {
                                                                  
                        foreach (var destination in relation.value["to"].Select((value, i) => new { i, value }))
                        {                           
                            Debug.WriteLine(destination.value);
                            ConnectDiagramShapes(relation.value["from"].ToString(), destination.value.ToString(), RadDiagram1);
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
}