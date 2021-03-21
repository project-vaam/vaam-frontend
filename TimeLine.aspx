<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeLine.aspx.cs" Async="true" Inherits="TimeLine" %>

<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>Mould Lifetime</title>
    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/pages/timeline.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <div class="demo-container" runat="server">
        <telerik:RadTimeline runat="server" ID="RadTimeline1" CollapsibleEvents="true" AlternatingMode="true"
            DataDateField="startDate" 
            DataKeyNames="duration, endDate, Activity, Process, Part">
            <EventTemplate>
                <div class="k-card-header">
                    <div style="display: flex; flex-direction: row; justify-content:space-between">
                        <div style="display: flex; flex-direction: column;">
                        <h4 class="k-event-title">#= data.Activity.Name#</h4>
                        <h5 class="no-bold">Processo: <b class="k-event-subtitle">#= data.Process.Name #</b></h5>
                        </div>
                         <span class="k-event-collapse k-button k-button-icon k-flat"><span class="k-icon k-i-arrow-chevron-right"></span></span>
                    </div>
                </div>
                <div class="k-card-body">
                    <div class="k-card-description" style="display:flex; flex-direction: column">

                        <span>Descrição do Processo: <b>#= data.Process.Description#</b></span>
                        <span>Começo: <b>#= data.Process.startDate#</b></span>
                        <span>Termino: <b>#= data.Process.endDate || "Ainda por terminar"#</b></span>
                        #if(data.Part) {#
                        <h5 class="top-flex-space">Parte do Molde</h5>
                        <span>Código: <b>#= data.Part.Code#</b></span>
                        <span>Descrição: <b>#= data.Part.Description#</b></span>
                        <span>TagRfid: <b>#= data.Part.TagRfid || "Não existente" #</b></span>
                        #}#
                    </div>
                </div>
 
                <div class="k-card-actions" style="display: flex; flex-direction: column">
               
                    <p>Começo: <strong>#= kendo.toString(data.startDate, "dd/MM/yyyy HH:mm:ss") #</strong></p>
                    <p>Termino: <strong>#= kendo.toString(data.endDate, "dd/MM/yyyy HH:mm:ss") || "Ainda por terminar"#</strong></p>
                    
                    <p>Duração: 
                        # if(data.duration) {#
                        <strong>#= kendo.toString(data.duration / 60 / 60, "0.00") # horas</strong>
                         #} else {#
                        <strong>Ainda por Terminar</strong>
                        #}#
                    </p>
                </div>
            </EventTemplate>
            <WebServiceClientDataSource runat="server">
                <Schema>
                    <Model>
                        <telerik:ClientDataSourceModelField DataType="Date" FieldName="endDate" />
                        <telerik:ClientDataSourceModelField DataType="Date" FieldName="startDate" />
                    </Model>
                </Schema>
                <SortExpressions>
                    <telerik:ClientDataSourceSortExpression FieldName="startDate" SortOrder="Asc" />
                </SortExpressions>
            </WebServiceClientDataSource>
        </telerik:RadTimeline>
    </div>
    </form>
</body>
</html>