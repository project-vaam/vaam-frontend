<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.Master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="TelerikWebAppResponsive.Default" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<asp:Content ID="Content0" ContentPlaceHolderID="head" Runat="Server">
    <link href="assets/styles/default.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

      <asp:Label ID="Label1" Text="Vai aparecer o nome do use caso ele exista" runat="server" />
      <asp:Label ID="Label2" Text="" runat="server" />

    <telerik:RadPageLayout runat="server" ID="RadPageLayout1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn CssClass="jumbotron">
                        <h1>H1 title, font size 36px</h1>
                        <h2>H2 Title, font size 30 px. Duis nibh dolor, rhoncus in euismod at, feugiat id magna.
                            <telerik:RadButton runat="server" ID="RadButton0" Text="NextPage Test" ButtonType="ToggleButton" Skin="Black" NavigateUrl="Grid.aspx"></telerik:RadButton>
                            
                            <telerik:RadLinkButton runat="server" ID="RadLinkButton1" Primary="true" Text="Time Line " ToolTip="Learn Аbout ASP.NET AJAX" NavigateUrl="TimeLine.aspx">
                                <Icon CssClass="rbOk"></Icon>
                            </telerik:RadLinkButton>

                            <telerik:RadLinkButton runat="server" ID="RadLinkButton2" Primary="true" Text="Login" ToolTip="Learn Аbout ASP.NET AJAX" NavigateUrl="Login.aspx">
                                <Icon CssClass="rbOk"></Icon>
                            </telerik:RadLinkButton>

                            <br />

                            <telerik:RadButton runat="server" ID="RadButton5" Primary="true" Text="Logout" RenderMode="Lightweight" onclick="Logout"></telerik:RadButton>

                          

                        </h2>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn HiddenMd="true" HiddenSm="true" HiddenXs="true">
                        <h3 id="teste">H3 text, font size 24 px </h3>
                        Ut aliquam elit eget quam tincidunt, et aliquam libero congue. Phasellus aliquet sed quam vitae dictum. Aliquam erat volutpat. Morbi accumsan a mi quis pretium. 
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <telerik:RadPageLayout runat="server" ID="Content1">
        <Rows>
            <telerik:LayoutRow>
                <Columns>
                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        <h4>H4 text font size 18 px.</h4>
                        <p><strong>Main content text font size 16px</strong>, aliquam turpis sed nisl mattis sagittis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut vitae sapien metus. In hac habitasse platea dictumst. Aenean velit mauris, lobortis eu lacinia sed</p>
                        <p>Nullam facilisis neque ut aliquet imperdiet. Mauris ut odio augue. Curabitur in mi ac odio vestibulum lobortis. </p>
                        <telerik:RadButton runat="server" ID="RadButton1" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        <h4>H4 text font size 18 px.</h4>
                        <p><strong>Main content text font size 16px</strong>, aliquam turpis sed nisl mattis sagittis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut vitae sapien metus. In hac habitasse platea dictumst. Aenean velit mauris, lobortis eu lacinia sed</p>

                        <p>Nullam facilisis neque ut aliquet imperdiet. Mauris ut odio augue. Curabitur in mi ac odio vestibulum lobortis. </p>
                        <telerik:RadButton runat="server" ID="RadButton2" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>

                    <telerik:LayoutColumn Span="4" SpanMd="12" SpanSm="12" HiddenXs="true">
                        <h4>H4 text font size 18 px.</h4>
                        <p><strong>Main content text font size 16px</strong>, aliquam turpis sed nisl mattis sagittis. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Ut vitae sapien metus. In hac habitasse platea dictumst. Aenean velit mauris, lobortis eu lacinia sed</p>

                        <p>Nullam facilisis neque ut aliquet imperdiet. Mauris ut odio augue. Curabitur in mi ac odio vestibulum lobortis. </p>
                        <telerik:RadButton runat="server" ID="RadButton3" Text="Button" ButtonType="SkinnedButton"></telerik:RadButton>
                    </telerik:LayoutColumn>
                </Columns>
            </telerik:LayoutRow>
        </Rows>
    </telerik:RadPageLayout>
</asp:Content>
