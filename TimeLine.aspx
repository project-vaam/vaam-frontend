<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeLine.aspx.cs" Async="true" Inherits="TimeLine" %>

<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>Mould Lifetime</title>
    <link href="assets/styles/styles.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        
        <asp:Label ID="MouldsLifetimeLabel" Text="Aqui vai aparecer JSON da API" runat="server" />
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <div class="demo-container" runat="server">
        <script>
            function OnDataBound(sender, args) {
                sender.expand(sender.get_items()[2]);
            }
        </script>
        <telerik:RadTimeline runat="server" ID="RadTimeline1" CollapsibleEvents="true"
            DataDateField="EndDate" AlternatingMode="true"
            DataTitleField="Id"
            DataKeyNames="StartDate,Duration">
            <ClientEvents  OnDataBound="OnDataBound"/>
            <EventTemplate>
                <div class="k-card-header">
                    <h5 class="k-card-title">
                        <span class="k-event-title">#= data.Id #</span>
                          <span class="k-event-collapse k-button k-button-icon k-flat"><span class="k-icon k-i-arrow-chevron-right"></span></span>
                    </h5>
                    <h6 class="k-card-subtitle">by <strong>#= data.StartDate #</strong></h6>
                    <h6 class="k-card-subtitle">by <strong>#= data.EndDate #</strong></h6>
                    <h6 class="k-card-subtitle">by <strong>#= data.Duration #</strong></h6>
                </div>
               <%-- <div class="k-card-body">
                    <div class="k-card-description">
                        <div class="imageContainer">                                   
                            <img src="#= data.Cover #" class="k-card-image">
                        </div>
                    </div>
                </div>--%>
 
                <%--<div class="k-card-actions">
                    <a class="k-button k-flat k-primary" href="#= data.Url #" target="_blank">Buy on Amazon</a>
                </div>--%>
            </EventTemplate>
            <WebServiceClientDataSource runat="server">
                <Schema>
                    <Model>
                        <telerik:ClientDataSourceModelField DataType="Date" FieldName="EndDate" />
                    </Model>
                </Schema>
                <SortExpressions>
                    <telerik:ClientDataSourceSortExpression FieldName="EndDate" SortOrder="Desc" />
                </SortExpressions>
            </WebServiceClientDataSource>
        </telerik:RadTimeline>
    </div>
    </form>
</body>
</html>