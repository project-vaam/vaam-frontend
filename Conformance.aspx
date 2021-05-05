<%@ Page Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Conformance.aspx.cs" Async="true" Inherits="Conformance" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="headContent" ContentPlaceHolderID="head" runat="Server">

    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/default.css" rel="stylesheet" />
    <script src="assets/scripts/functions.js"></script>
    <script type="text/javascript" src="assets/scripts/graphs.js"></script>
    <link href="assets/styles/pages/conformance.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js" type="text/javascript"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/cytoscape/3.18.2/cytoscape.min.js"></script>
     <script src="https://code.jquery.com/jquery-1.11.3.min.js"></script>
     <script src="https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.29.1/moment.min.js"></script>

    <script type="text/javascript">

        /* Datepicker Clientside Config */
      <%--  function OnDateSelected(sender, eventArgs) {
            var date1 = sender.get_selectedDate();
            date1.setDate(date1.getDate() + 31);
            var datepicker = $find("<%= RadDatePicker2.ClientID %>");
            datepicker.set_maxDate(date1);
        }--%>

    </script>
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

            <telerik:RadButton RenderMode="Lightweight" ID="InductiveRadioBtn" runat="server" Text="Inductive Miner (??)" ToggleType="Radio" OnClick="InductiveRadioBtn_Click"
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
          

            <h3 style="text-align:center;margin-bottom: 0px;">Detalhes do Processo</h3>
            <%-- RadComboBox - Process --%>
            <h4 style="margin-top: 16px !important">Selecione o processo:</h4>
            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxProcess" AllowCustomText="true" runat="server" DataValueField="ID"
                DataTextField="Text"
                AutoPostBack="True"
                Filter="Contains"
                OnSelectedIndexChanged="callFilterInformation"
                Width="400px">
            </telerik:RadComboBox>

            <%-- Time Interval --%>
            <h4 style="margin-top: 16px !important">Intervalo de Tempo:</h4>
            <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker1" Width="80%" runat="server" DateInput-Label="Data Inicial: ">
            </telerik:RadDatePicker>
            <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker2" Width="80%" style="margin-top:6px;" runat="server" DateInput-Label="Data Final:&nbsp;&nbsp;">
            </telerik:RadDatePicker>
            <br />
            <%--<asp:CompareValidator ID="dateCompareValidator" runat="server" ControlToValidate="Raddatepicker2"
                            ControlToCompare="RadDatePicker1" Operator="GreaterThan" Type="Date" ErrorMessage="A Data Final tem que ser posterior à Inicial">
            </asp:CompareValidator>--%>

            <%-- Moulds --%>
            <h4 style="margin-top: 0px !important">Molde:</h4>
            <%--<telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxMoulds" runat="server" CheckBoxes="true" EnableCheckAllItemsCheckBox="true" Width="400" Skin="Bootstrap">
            </telerik:RadComboBox>--%>
            <telerik:RadComboBox 
                RenderMode="Lightweight"
                ID="RadComboBoxMoulds" 
                AllowCustomText="true" 
                Filter="Contains"
                AutoCompleteSeparator=","
                runat="server" 
                CheckBoxes="true"
                EnableCheckAllItemsCheckBox="true"
                Width="400px">
            </telerik:RadComboBox>

            <%-- Activities --%>
            <h4 style="margin-top: 16px !important">Atividades:</h4>
            <telerik:RadComboBox 
                RenderMode="Lightweight"
                ID="RadComboBoxActivities"
                runat="server"
                CheckBoxes="true" 
                EnableCheckAllItemsCheckBox="true" 
                AllowCustomText="true" 
                Filter="Contains"
                AutoCompleteSeparator=","
                Width="400">
            </telerik:RadComboBox>

            <%-- Operadores --%>
            <h4 style="margin-top: 16px !important">Operadores:</h4>
            <telerik:RadComboBox
                RenderMode="Lightweight" 
                ID="RadComboBoxOperadores"
                runat="server"
                CheckBoxes="true" 
                EnableCheckAllItemsCheckBox="true" 
                AllowCustomText="true" 
                Filter="Contains"
                AutoCompleteSeparator=","
                Width="400">
            </telerik:RadComboBox>
           
            <%---------------- MODELO A COMPARAR ----------------%>
            <h3 style="text-align:center;margin-bottom: 0px;">Modelo a comparar</h3>
            <%-- Time Interval --%>
            <h4 style="margin-top: 16px !important">Intervalo de Tempo:</h4>
            <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker3" Width="80%" runat="server" DateInput-Label="Data Inicial: ">
            </telerik:RadDatePicker>
            <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker4" Width="80%" style="margin-top:6px;" runat="server" DateInput-Label="Data Final:&nbsp;&nbsp;">
            </telerik:RadDatePicker>
            <br />


            <%-- Moulds --%>
            <h4 style="margin-top: 0px !important">Molde:</h4>
            <telerik:RadComboBox 
                RenderMode="Lightweight"
                ID="RadComboBoxMoulds2"
                runat="server" 
                CheckBoxes="true"
                EnableCheckAllItemsCheckBox="true"
                AllowCustomText="true" 
                Filter="Contains"
                AutoCompleteSeparator=","
                Width="400">
            </telerik:RadComboBox>

            <%-- Activities --%>
            <h4 style="margin-top: 16px !important">Atividades:</h4>
            <telerik:RadComboBox
                RenderMode="Lightweight"
                ID="RadComboBoxActivities2" 
                runat="server" 
                CheckBoxes="true" 
                EnableCheckAllItemsCheckBox="true" 
                AllowCustomText="true" 
                Filter="Contains"
                AutoCompleteSeparator=","
                Width="400">
            </telerik:RadComboBox>

            <%-- Operadores --%>
            <h4 style="margin-top: 16px !important">Operadores:</h4>
            <telerik:RadComboBox 
                RenderMode="Lightweight" 
                ID="RadComboBoxOperadores2" 
                runat="server"
                CheckBoxes="true"
                EnableCheckAllItemsCheckBox="true"
                AllowCustomText="true" 
                Filter="Contains"
                AutoCompleteSeparator=","
                Width="400">
            </telerik:RadComboBox>


            <telerik:RadCheckBox runat="server" ID="EstimatedCheckbox" style="margin-top:16px;text-align: center;width: 100%;" Checked="true" Text="Incluir Ativididades com Fim Estimado" AutoPostBack="false">
            </telerik:RadCheckBox>

            <div id="showDiagram" style="display: flex; width: 100%; align-items: center; justify-content: center; margin-top: 4px; margin-bottom:32px;">
                 <%-- Button CREATE DropDown --%>
            <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Gerar Diagrama"   OnClick="ShowDiagram_Click" Height="60px" />
            </div>
        </div>
    </div>

     <script>

        var process = <%=processes%>

        console.log("degubg here")
        console.log(process)

         if (process) {
             console.log("CALLING ....");
             generateConformance(process);
         } else {
             console.log("No data to generate graph");
         }
            
     </script>

</asp:Content>
