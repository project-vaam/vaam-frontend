<%@ Page Language="C#"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Async="true" Inherits="Performance" %>
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
        <asp:Label ID="APIResult" Text="" runat="server"></asp:Label>
        <div class="demo-container" runat="server">
            <telerik:RadDiagram ID="RadDiagram1" runat="server"></telerik:RadDiagram>
        </div>
    </div>

</asp:Content>
