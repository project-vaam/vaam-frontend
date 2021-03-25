<%@ Page Language="C#"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="TimeLine.aspx.cs" Async="true" Inherits="TimeLine" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/pages/timeline.css" rel="stylesheet" />
    <link href="assets/styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        
       
        <h5>Selecione o Molde</h5>
       
        <telerik:RadDropDownList RenderMode="Lightweight" ID="RadDropDownList1" DefaultMessage="Escolha um modulo" AutoPostBack="true"
            OnSelectedIndexChanged="GetMouldLifetimeEvents" runat="server">
        </telerik:RadDropDownList>
     </div>

    <div style="text-align:center">
        <h1><span id="currentMould" runat="server"></span></h1>
    </div>
    <hr />

    <div class="demo-container" runat="server">

        <telerik:RadTimeline runat="server" ID="RadTimeline1" CollapsibleEvents="true" AlternatingMode="true"
            DataDateField="startDate" 
            DataKeyNames="duration, endDate, Activity, Process, Part, ActivityUserEntry">
            <EventTemplate>
                <div class="k-card-header">
                    <div style="display: flex; flex-direction: row; justify-content:space-between">
                        <div style="display: flex; flex-direction: column;">
                        <h4 class="k-event-title">#= data.Activity.Name#</h4>
                        <h5 class="no-bold">Processo: <b class="k-event-subtitle">#= data.Process.Name #</b></h5>
                        </div>

                        <span class="k-event-collapse k-button k-button-icon k-flat">
                            <span class="k-icon k-i-arrow-chevron-right"></span>
                        </span>
                    </div>
                </div>
              
                <div class="k-card-body">
                    <div class="k-card-description" style="display:flex; flex-direction: column">
                        <span>Descrição do Processo: <b>#= data.Process.Description#</b></span>
                        <span>Começo do Processo: <b>#= kendo.toString(kendo.parseDate(data.Process.StartDate), "dd/MM/yyyy HH:mm:ss") #</b></span>
                        <span>Termino do Processo: <b>#= kendo.toString(kendo.parseDate(data.Process.EndDate), "dd/MM/yyyy HH:mm:ss") || "Ainda por terminar"#</b></span>

                        #if(data.Part) {#
                        <h5 class="top-flex-space">Parte do Molde</h5>
                        <span>Código: <b>#= data.Part.Code#</b></span>
                        <span>Descrição: <b>#= data.Part.Description#</b></span>
                        <span>TagRfid: <b>#= data.Part.TagRfid || "Não existente" #</b></span>
                        #}#                    

                        <h4 class="top-flex-space"> Histórico de Atividades </h4>
                        #if(data.ActivityUserEntry.length > 0) {#   
                        <table>
                            <tr>
	                        <th>Worker Name</th>
	                        <th>Worker Role</th>
	                        <th>Workstation Name</th>
	                        <th>Start Date</th>
	                        <th>End Date</th>
	                        <th>Duration</th>
                            </tr>

	                            # for (var i = 0; i < data.ActivityUserEntry.length; i++) { # 
		                        <tr>
			                        <td>#=data.ActivityUserEntry[i].User.Name#</td>
			                        <td>#=data.ActivityUserEntry[i].User.Role#</td>
                                    <td>#=data.ActivityUserEntry[i].Workstation != null ? data.ActivityUserEntry[i].Workstation.Name : "None" #</td>
			                        <td>#=kendo.toString(kendo.parseDate(data.ActivityUserEntry[i].StartDate), "dd/MM/yyyy HH:mm:ss")#</td>
			                        <td>#= kendo.toString(kendo.parseDate(data.ActivityUserEntry[i].EndDate), "dd/MM/yyyy HH:mm:ss") || "Ainda por terminar"#</td>
			                        <td>#= kendo.toString(data.ActivityUserEntry[i].TimeSpentMillis / 1000 / 60 , "0.00") # minutos</td>
		                        </tr>
	                        # } #
                        </table>
                        #} else { #
                        <span> Sem Histórico </span>
                        #}#
                    </div>
                </div>
 
                <div class="k-card-actions" style="display: flex; flex-direction: column">
                    <p>Começo: <strong>#= kendo.toString(data.startDate, "dd/MM/yyyy HH:mm:ss") #</strong></p>
                    <p>Termino: <strong>#= kendo.toString(data.endDate, "dd/MM/yyyy HH:mm:ss") || "Ainda por terminar"#</strong></p>
                    <p>Duração: 
                        # if(data.duration) {#
                        <strong>#= kendo.toString(data.duration / 60 / 60 / 60, "0.00") # horas</strong>
                         #} else {#
                        <strong>Ainda por Terminar</strong>
                        #}#
                    </p>
                </div>
            </EventTemplate>
            <WebServiceClientDataSource runat="server">
                <Schema>
                    <Model>
                        <%--<telerik:ClientDataSourceModelField DataType="Date" FieldName="endDate" />
                        <telerik:ClientDataSourceModelField DataType="Date" FieldName="startDate" />
                        <telerik:ClientDataSourceModelField DataType="Date" FieldName="Process.StartDate" />--%>
                    </Model>
                </Schema>
                <SortExpressions>
                    <telerik:ClientDataSourceSortExpression FieldName="startDate" SortOrder="Asc" />
                </SortExpressions>
            </WebServiceClientDataSource>
        </telerik:RadTimeline>
    </div>
</asp:Content>