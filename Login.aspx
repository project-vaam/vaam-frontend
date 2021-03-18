<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="DummyTest"  Async="true" %>

<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>Telerik ASP.NET Example</title>
     <link href="styles/loginStyles.css" rel="stylesheet" />
    <script src="scripts.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
    <telerik:RadSkinManager ID="RadSkinManager1" runat="server" ShowChooser="true" />
    <div id="rfd-demo-zone" class="demo-containers">
        <div class="validationSummary">
            <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecoratedControls="All"
                DecorationZoneID="rfd-demo-zone" ControlsToSkip="Zone"></telerik:RadFormDecorator>
 
            <div class="demo-container size-custom">
                <asp:Panel runat="server" ID="Panel2">
                    <p><strong>Login</strong></p>
                    <asp:Login ID="Login2" runat="server" Width="100%" EnableViewState="false" OnClick="LoginPost" OnLoggingIn="Login2_LogginIn">
                        <LayoutTemplate>
                            <table cellpadding="1" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <table cellpadding="0" width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                        ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Password" runat="server" TextMode="Password"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                                        ErrorMessage="Password is required." ToolTip="Password is required." ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me next time."></asp:CheckBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="2" style="color: Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2" style="text-align: right;">
                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="Login1"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>

                 
                    <asp:Label ID="LabelStatusLabel" Text="" Font-Size="Large" ForeColor="Green" runat="server"></asp:Label>
                    <asp:Label ID="LabelPassword" Text="" Font-Size="Large" ForeColor="Green" runat="server"></asp:Label>
                    <asp:Label ID="LabelResult" Text="" Font-Size="Large" ForeColor="Green" runat="server"></asp:Label>


                    <div style="margin-top: 5px;">
                        <asp:LoginName ID="LoginName1" runat="server" Width="300px" EnableViewState="false"></asp:LoginName>
                    </div>
                    <div style="margin-top: 5px;">
                        <asp:LoginStatus ID="LoginStatus1" runat="server" Width="300px" EnableViewState="false" Enabled="false"></asp:LoginStatus>
                    </div>
                </asp:Panel>
            </div>
        </div>
    </div>
 
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>

    </form>
</body>
</html>
