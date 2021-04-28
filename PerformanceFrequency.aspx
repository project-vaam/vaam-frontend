﻿<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="PerformanceFrequency.aspx.cs" Async="true" Inherits="Performance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">



    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/default.css" rel="stylesheet" />
    <script src="assets/scripts/functions.js"></script>
    <!-- <script src="assets/scripts/graphs.js"></script> -->
    <script type="text/javascript" src="assets/scripts/graphs.js"></script>
    <link href="assets/styles/pages/performanceFrequency.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.18.2/cytoscape.min.js"></script>
     <script src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>

    <script type="text/javascript">
      //$(document).ready(function () {

      //    $("#width").val("Hello");
      //    $("#height").val('$(window).height()');

      // });
    </script>

    <style lang="css">
        .content-wrapper{
            height: 100%;
            min-height: 80vh;
            width: 100%;

            display: flex;
        }

        .diagram-wrapper{ 
            width: 100%;
            flex-grow: 2;
            display: flex;
        }

        .menu-wrapper{
            width: 400px;

            flex-shrink: 0;
            border: 1px solid dimgrey;

            border-radius: 0px 20px 20px 0px;
            padding: 16px;
        }
    </style>
</asp:Content>

<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="width" runat="server" />
    <asp:HiddenField ID="height" runat="server" />
        

    <div class="content-wrapper">
        <%------- DIAGRAM WINDOW ------%>
        <div class="diagram-wrapper">
            
            <%-- Title of Process --%>
            <div id="displayProcess" runat="server" style="text-align: center">
                <h1><span id="currentProcess" runat="server"></span></h1>
            </div>

           

            <%-- Diagram --%>
            <div id="cy" style="flex-grow:1; border: 1px solid dimgrey; border-radius: 20px 0px 0px 20px; ">              
                <h1 style=" text-align: center; margin-top: 25%;" id="showError" runat="server" >
                    <span id="errorMessage"  runat="server"  ></span>
                </h1>                          
            </div>

        </div>
        <%------- MENU ------%>
        <div class="menu-wrapper">

            <%-- Workflow Type --%>
            <h4>Tipo de Algoritmo</h4>
            <div style="display:flex;flex-direction: column;align-items:flex-start;">
            <telerik:RadButton RenderMode="Lightweight" ID="AlphaRadioBtn" runat="server" Text="Alpha Miner" ToggleType="Radio" Checked="true" OnClick="AlphaRadioBtn_Click"
                ButtonType="ToggleButton" GroupName="WorkflowType"></telerik:RadButton>

            <telerik:RadButton RenderMode="Lightweight" ID="HeuristicRadioBtn" runat="server" Text="Heuristic Miner" ToggleType="Radio" OnClick="HeuristicRadioBtn_Click"
                ButtonType="ToggleButton" GroupName="WorkflowType"></telerik:RadButton>

            <telerik:RadButton RenderMode="Lightweight" ID="InductiveRadioBtn" runat="server" Text="Inductive Miner" ToggleType="Radio" OnClick="InductiveRadioBtn_Click"
                ButtonType="ToggleButton" GroupName="WorkflowType"></telerik:RadButton> 
            </div>

            <%-- Frequency Slider --%>
            <div id="thresholdField" runat="server">
                <h4 style="margin-top: 16px !important">Threshold</h4>
                <span>Simplistic to Complex</span>
                <telerik:RadSlider RenderMode="Lightweight" ID="ThresholdSlider" runat="server" MinimumValue="0" MaximumValue="1" Value="0.5"
                    SmallChange="0.1" LargeChange="0.5" ItemType="tick" Height="70px" Width="400px" 
                    AnimationDuration="400" CssClass="TicksSlider" ThumbsInteractionMode="Free" Skin="Metro">
                </telerik:RadSlider>
            </div>

            <%-- Radio Type Diagram Buttons --%>
            <div style="display: flex; align-items: center; justify-content: center; width: 100%;margin-top: 16px;" id="tipoGrafico"  runat="server">
                <telerik:RadButton
                    CssClass="performance-btn"
                    Checked="true"
                    RenderMode="Lightweight"
                    ID="PerformanceBtn"
                    GroupName="DiagramMode"
                    runat="server"
                    Text="Performance"
                    AutoPostBack="false"
                    ToggleType="Radio"/>
                <telerik:RadButton
                    CssClass="frequency-btn"
                    RenderMode="Lightweight"
                    ID="FrequencyBtn"
                    GroupName="DiagramMode"
                    runat="server"
                    Text="Frequência"
                    AutoPostBack="false"
                    ToggleType="Radio"/>
            </div>
            
            <%-- DropDown --%>
            <h4 style="margin-top: 16px !important">Selecione o Processo</h4>
            <telerik:RadDropDownList
                ID="RadDropDownList4"
                runat="server"
                DataValueField="ID"
                DataTextField="Text">
            </telerik:RadDropDownList>

            <div id="showDiagram" style="display: flex; width: 100%; align-items: center; justify-content: center; margin-top: 16px;">
                 <%-- Button CREATE DropDown --%>
            <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Gerar Diagrama"   OnClick="ShowDiagram_Click" Height="60px" />
            </div>
           
            <div id="escalaCores" runat="server">
                 <h3 style="text-align:center;margin-bottom: 0px;">Escala de Cores</h3>
                 <div class="text-wrapper">
                     <h4>Estados</h4>
                     <h4>Setas</h4>
                 </div>
                 <div class="group-box-wrapper">
                     <div class="stages-text-wrapper">
                         <div class="scale-text"></div>
                         <div class="scale-text" id="level-4-state"></div>
                         <div class="scale-text" id="level-3-state"></div>
                         <div class="scale-text" id="level-2-state"></div>
                         <div class="scale-text" id="level-1-state"></div>
                         <div class="scale-text""></div>
                     </div>
                     <div class="group-box">
                        <div class="box-wrapper-stages"></div>
                        <div class="box-wrapper-arrows"></div>
                    </div>
                     <div class="stages-text-wrapper">
                         <div class="scale-text"></div>
                         <div class="scale-text" id="level-4-arrows"></div>
                         <div class="scale-text" id="level-3-arrows"></div>
                         <div class="scale-text" id="level-2-arrows"></div>
                         <div class="scale-text" id="level-1-arrows"></div>
                         <div class="scale-text"></div>
                     </div>
                 </div>
            </div>   
            
        </div>
    </div>

     <script>
                var process = <%=processes%>
                console.log("CALLING ....");
                console.log(process)
                generateGraph(process);   
     </script>

</asp:Content>
