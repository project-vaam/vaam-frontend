<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeLine.aspx.cs" Inherits="TimeLine" %>

<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>Telerik ASP.NET Example</title>
     <link href="styles/base.css" rel="stylesheet" />
    <script src="/scripts/functions.js" type="text/javascript"></script>
    <script src="/scripts/autoload.js" type="text/javascript"></script>
</head>

<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <div class="demo-container" runat="server">
        <telerik:RadTimeline runat="server" ID="RadTimeline1" AlternatingMode="true" CollapsibleEvents="true" Orientation="Vertical">
            <ClientEvents OnDataBound="OnDataBound" />
            <WebServiceClientDataSource>
                <WebServiceSettings>
                    <Select Url="dummyData/events.json" DataType="JSON" />
                </WebServiceSettings>
                <Schema>
                    <Model>
                        <telerik:ClientDataSourceModelField DataType="Date" FieldName="date" />
                    </Model>
                </Schema>
                <SortExpressions>
                    <telerik:ClientDataSourceSortExpression FieldName="date" SortOrder="Desc" />
                </SortExpressions>
            </WebServiceClientDataSource>
        </telerik:RadTimeline>
    </div>
 
    </form>
</body>

</html>
