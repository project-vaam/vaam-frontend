using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

public partial class GraphBar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //Set the datasource of the HtmlChart and databind it
        //RadHtmlChart1.DataSource = GetData();
        //RadHtmlChart1.DataBind();

        //Create a "Lower Threshold" LineSeries programmatically
        //LineSeries lineSeries1 = new LineSeries();
        /* lineSeries1.Name = "Lower Threshold (Programmatic)";
         lineSeries1.LabelsAppearance.Visible = true;
         lineSeries1.TooltipsAppearance.Color = System.Drawing.Color.White;
         lineSeries1.TooltipsAppearance.DataFormatString = "{0}%";
         lineSeries1.MarkersAppearance.Visible = true;*/

        RadHtmlChart1.PlotArea.XAxis.Items.Add("tou");
        RadHtmlChart1.PlotArea.XAxis.Items.Add("tou");
        RadHtmlChart1.PlotArea.XAxis.Items.Add("tou");

        ColumnSeries firstColumnSeries = new ColumnSeries();
        firstColumnSeries.SeriesItems.Add(new CategorySeriesItem(1));
        firstColumnSeries.SeriesItems.Add(20, System.Drawing.Color.Green);
        firstColumnSeries.SeriesItems.Add(320);
        RadHtmlChart1.PlotArea.Series.Add(firstColumnSeries);

        /*ColumnSeries secondColumnSeries = new ColumnSeries();
        secondColumnSeries.SeriesItems.Add(new CategorySeriesItem(15));
        secondColumnSeries.SeriesItems.Add(25, System.Drawing.Color.Blue);
        secondColumnSeries.SeriesItems.Add(5);
        RadHtmlChart1.PlotArea.Series.Add(secondColumnSeries);*/
    }

    protected DataTable GetData()
    {
        DataTable dt = new DataTable();

        dt.Columns.Add("UpperThreshold");
        dt.Columns.Add("DummyData");
        for (int i = 0; i < 12; i++)
        {
            dt.Rows.Add(40, 30);
        }
        return dt;
    }
}