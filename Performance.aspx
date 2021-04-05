<%@ Page Language="C#"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Async="true" Inherits="Performance"%>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="headContent" ContentPlaceHolderID="head" Runat="Server">
   


    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/default.css" rel="stylesheet" />
    <script src="assets/scripts/functions.js"></script> 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.3/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript">
      //$(document).ready(function () {

      //    $("#width").val("Hello");
      //    $("#height").val('$(window).height()');

      // });
    </script>
</asp:Content>
 
<asp:Content ID="bodyContent" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="width" runat="server" />
    <asp:HiddenField ID="height" runat="server" />

    <div>
        <h5>Selecione o Processo</h5>
       
        <%--<telerik:RadDropDownList RenderMode="Lightweight" ID="RadDropDownList1" DefaultMessage="Escolha um Processo" AutoPostBack="true"
            OnSelectedIndexChanged="callWorkflows" runat="server">
        </telerik:RadDropDownList>--%>

        <telerik:RadDropDownList
            id="RadDropDownList4"
            runat="server"
            datavaluefield="ID"
            datatextfield="Text"
            datasourceid="ObjectDataSource1"> 
           
        </telerik:RadDropDownList>
         <telerik:RadButton RenderMode="Lightweight" runat="server" Text="Select" ID="RadButton1"  OnClick="Button1_Click" />
            <p>
                <asp:Label runat="server" ID="Label2" />
            </p>

        <div id="displayProcess" runat="server" style="text-align:center">
            <h1><span id="currentProcess" runat="server"></span></h1>
            <hr />
        </div>

        <asp:ObjectDataSource ID="ObjectDataSource1" TypeName="DropDownListDataObject" SelectMethod="GetItems"
            runat="server"></asp:ObjectDataSource>
       
        <div id="DisplayError" runat="server" style="text-align:center">
            <h1><span id="Span1" runat="server"></span></h1>
        </div>

       <div class="demo-container" runat="server">
            <telerik:RadDiagram ID="RadDiagram1" runat="server"></telerik:RadDiagram>
        </div>
    </div>

</asp:Content>
