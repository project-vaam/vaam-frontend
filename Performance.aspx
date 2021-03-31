<%@ Page Language="C#"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Async="true" Inherits="Performance" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>



<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
   


    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/default.css" rel="stylesheet" />
    <script src="assets/scripts/functions.js"></script> 
   <script type="text/javascript">

     
       $(document).ready(function () {

           $("#width").val() = $(window).width();
           $("#height").val() = $(window).height();

       });
   </script>
</asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:HiddenField ID="width" runat="server" />
    <asp:HiddenField ID="height" runat="server" />

    <div>
        <asp:Label ID="APIResult" Text="" runat="server"></asp:Label>           
    </div>

</asp:Content>
