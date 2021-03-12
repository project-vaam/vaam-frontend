<%@ Page Title="Notes" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="notes.aspx.cs" Inherits="Notes" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Styles/notes.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="FolderContent" runat="Server">
    <nav:FolderNavigationControl runat="server" ID="FolderNavigationControl" />
    <nav:MobileNavigation runat="server" ID="MobileNavigation"></nav:MobileNavigation>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <telerik:RadNavigation runat="server" ID="notesNavigation" Skin="BlackMetroTouch" CssClass="secondaryMenu">
        <Nodes>
            <telerik:NavigationNode Text="New" SpriteCssClass="icon icon-Add-Circle"></telerik:NavigationNode>
            <telerik:NavigationNode Text="Delete"></telerik:NavigationNode>
            <telerik:NavigationNode CssClass="rightScndNav">
                <NodeTemplate>
                    <telerik:RadSearchBox runat="server" Skin="BlackMetroTouch" Width="340px" ID="mainSearch" CssClass="hidden" ShowLoadingIcon="false">
                    </telerik:RadSearchBox>
                    <span class="button searchBtn">
                        <span class="icon icon-Search"></span>
                    </span>
                </NodeTemplate>
            </telerik:NavigationNode>
        </Nodes>
    </telerik:RadNavigation>
    <div class="scroller">
        <!-- Page Content -->
    </div>
</asp:Content>
