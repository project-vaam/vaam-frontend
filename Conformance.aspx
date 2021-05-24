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

            <telerik:RadButton RenderMode="Lightweight" ID="InductiveRadioBtn" runat="server" Text="Inductive Miner" ToggleType="Radio" OnClick="InductiveRadioBtn_Click"
                ButtonType="ToggleButton" GroupName="WorkflowType"></telerik:RadButton> 
            </div>

            <%-- Frequency Slider --%>
            <div id="thresholdField" runat="server">
                <h4 style="margin-top: 16px !important">Threshold</h4>
                <span>Complex to Simplistic</span>
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

            <div id="processDetailsContainer" runat="server">

                <%-- Time Interval --%>
                <h4 style="margin-top: 16px !important">Intervalo de Tempo:</h4>
                <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker1" Width="80%" runat="server" DateInput-Label="Data Inicial: ">
                </telerik:RadDatePicker>
                <telerik:RadDatePicker RenderMode="Lightweight" ID="RadDatePicker2" Width="80%" style="margin-top:6px;" runat="server" DateInput-Label="Data Final:&nbsp;&nbsp;">
                </telerik:RadDatePicker>
                <br />


                <%-- Moulds --%>
                <h4 style="margin-top: 0px !important">Molde:</h4>
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

            </div>
           
            <div id="modelContainer" runat="server">
                <%---------------- MODELO A COMPARAR ----------------%>
                <h3 style="text-align:center;margin-bottom: 0px;">Modelo a comparar</h3>

                <%-- Checkbox to compare process --%>
                <telerik:RadCheckBox runat="server" ID="RadCheckBoxProcessToCompare" style="margin:16px 0px 0px 8px;text-align: center;width: 100%;" Checked="false" Text="Comparar com outro Processo" OnClick="RadCheckBoxProcessToCompare_Click">
                </telerik:RadCheckBox>

                <%-- Process to Compare --%>
                <h4 style="margin-top: 16px !important">Selecione processo a comparar:</h4>
                 <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxProcessToCompare" AllowCustomText="true" runat="server" DataValueField="ID"
                    DataTextField="Text"
                    AutoPostBack="True"
                    Filter="Contains"
                    Width="400px">
                </telerik:RadComboBox>

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
            </div>
    
            <div id="inductiveContainer" runat="server">
                <div style="width: 100%; display: flex; justify-content: space-around; margin-top: 16px;">
                    <h4>Ativities</h4>
                    <h4>Paths</h4>
                </div>
                <div style="width: 100%; display: flex; justify-content: space-around; margin-top: 16px;">
                    <%-- Activities --%>

                    <div style="display:flex; flex-direction: column; align-items: center;">
                        <telerik:RadSlider RenderMode="Lightweight" ID="RadSliderActivities" runat="server" MinimumValue="0" MaximumValue="1000" Value="500"
                            Height="300px" Orientation="Vertical" OnValueChanged="RadSliderActivities_ValueChanged" AutoPostBack="true"
                            AnimationDuration="200" ThumbsInteractionMode="Free" Skin="Metro">
                        </telerik:RadSlider>
                        <b><asp:Label ID="labelSliderActivities" runat="server" Text="0.500" /></b>
                    </div>
                    <%-- Paths --%>
                    <div style="display:flex; flex-direction: column; align-items: center;">
                        <telerik:RadSlider RenderMode="Lightweight" ID="RadSliderPaths" runat="server" MinimumValue="0" MaximumValue="1000" Value="500"
                            Height="300px" Orientation="Vertical" OnValueChanged="RadSliderPaths_ValueChanged" AutoPostBack="true"
                            AnimationDuration="200" ThumbsInteractionMode="Free" Skin="Metro">
                        </telerik:RadSlider>
                        <b><asp:Label ID="labelSliderPathsValue" runat="server" Text="0.500" /></b>
                    </div>
                </div>

                <telerik:RadCheckBox runat="server" ID="RadCheckBoxShowDesviations" style="margin-top:16px;text-align: center;width: 100%;" Checked="true" Text="Incluir Desvios" AutoPostBack="false">
                </telerik:RadCheckBox>
            </div>

            <div id="showDiagram" style="display: flex; width: 100%; align-items: center; justify-content: center; margin-top: 16px; margin-bottom:32px;">
                 <%-- Button CREATE DropDown --%>
                <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Gerar Diagrama"   OnClick="ShowDiagram_Click" Height="60px" />
            </div>
        </div>
    </div>

     <script>

        var process = <%=processes%>

        console.log("debug here")
        console.log(process)

         if (process) {
             console.log("CALLING ....");
             generateConformance(process);
         } else {
             console.log("No data to generate graph");
         }
            
     </script>

</asp:Content>
