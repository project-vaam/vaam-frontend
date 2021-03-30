<%@ Page Language="C#"  MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Performance.aspx.cs" Async="true" Inherits="Performance" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>


<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/styles/base.css" rel="stylesheet" />
    <link href="assets/styles/default.css" rel="stylesheet" />
    <script src="assets/scripts/functions.js"></script> 
</asp:Content>

 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>

        <asp:Label ID="APIResult" Text="" runat="server"></asp:Label>
             
        <telerik:RadDiagram ID="RadDiagram" runat="server" Selectable="false" Editable="false">
            <ShapesCollection>
                <telerik:DiagramShape Id="DiagramShape1" Width="100" Height="70" X="260" Y="100" Type="rectangle">
                    <ContentSettings Text="Parent" />
                    <FillSettings Color="#25a0da" />
                </telerik:DiagramShape>
                <telerik:DiagramShape Id="DiagramShape2" Height="70" X="60" Y="250" Type="circle">
                    <ContentSettings Text="Child 1" />
                    <FillSettings Color="#FFBE33" />
                </telerik:DiagramShape>
                <telerik:DiagramShape Id="DiagramShape3" Height="70" X="245" Y="250" Type="circle">
                    <ContentSettings Text="Child 2" />
                    <FillSettings Color="#FFBE33" />
                </telerik:DiagramShape>
                <telerik:DiagramShape Id="DiagramShape4" Height="70" X="440" Y="250" Type="circle">
                    <ContentSettings Text="Child 3" />
                    <FillSettings Color="#FFBE33" />
                </telerik:DiagramShape>
            </ShapesCollection>

            <ConnectionsCollection>
                <telerik:DiagramConnection StartCap="FilledCircle" EndCap="ArrowEnd">
                    <FromSettings Connector="Bottom" ShapeId="DiagramShape1" />
                    <ToSettings Connector="Top" ShapeId="DiagramShape2" />
                </telerik:DiagramConnection>

                <telerik:DiagramConnection StartCap="FilledCircle" EndCap="ArrowEnd">
                    <FromSettings Connector="Bottom" ShapeId="DiagramShape1" />
                    <ToSettings Connector="Top" ShapeId="DiagramShape3" />
                </telerik:DiagramConnection>

                 <telerik:DiagramConnection StartCap="FilledCircle" EndCap="ArrowEnd">
                    <FromSettings Connector="Bottom" ShapeId="DiagramShape1" />
                    <ToSettings Connector="Top" ShapeId="DiagramShape4" />
                </telerik:DiagramConnection>    </ConnectionsCollection>
        </telerik:RadDiagram>

    </div>
</asp:Content>
