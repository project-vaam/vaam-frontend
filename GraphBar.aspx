<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GraphBar.aspx.cs" Inherits="GraphBar" %>

<!DOCTYPE html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <div>
              <telerik:RadHtmlChart runat="server" ID="RadHtmlChart1" Width="800px" Height="400px">
            <PlotArea>
                <Series>
                    <%--<telerik:ColumnSeries></telerik:ColumnSeries>--%>
                </Series>  
            </PlotArea>
            <Legend>
                <Appearance Position="Bottom">
                </Appearance>
            </Legend>
            <ChartTitle Text="Tempo de Processos">
                <Appearance Position="Top">
                </Appearance>
            </ChartTitle>
        </telerik:RadHtmlChart>
        </div>
    </form>
</body>
</html>
