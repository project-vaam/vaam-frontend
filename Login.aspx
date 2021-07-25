<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="DummyTest"  Async="true" %>

<!DOCTYPE html>
<html xmlns='http://www.w3.org/1999/xhtml'>
<head runat="server">
    <title>VAAM</title>
    <meta name="viewport" content="initial-scale=1.0, minimum-scale=1, maximum-scale=1.0, user-scalable=no" />
    <link rel="icon" href="assets/images/Thumbnails/increase.png"/>
     <link href="assets/styles/loginStyles.css" rel="stylesheet" />
    <script src="assets/scripts/scripts.js"></script>
</head>

<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="RadScriptManager1" />
   

    <div id="rfd-demo-zone" class="demo-containers">
        <div class="validationSummary">
            <telerik:RadFormDecorator RenderMode="Lightweight" ID="RadFormDecorator1" runat="server" DecoratedControls="All"
                DecorationZoneID="rfd-demo-zone" ControlsToSkip="Zone"></telerik:RadFormDecorator>
 
            <div class="demo-container size-custom">
                <asp:Panel runat="server" ID="Panel2">
                    <div style="text-align:center">
                        <h1>VAAM</h1>
                    </div>
                    <hr />
                    <h2>Login</h2>
                    <asp:Login ID="Login" runat="server" Width="100%" EnableViewState="false" OnLoggingIn="LogginIn">                   
                        <LayoutTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
                                                </td>
                                                <td>
                                                    <div style="text-align:center">
                                                        <asp:TextBox ID="UserName" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                                            ErrorMessage="User Name is required." ToolTip="User Name is required." ValidationGroup="Login1"></asp:RequiredFieldValidator>
                                                    </div>
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
                                                <td align="center" colspan="2" style="color: Red;">
                                                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="5" style="text-align: right;">
                                                    <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Sign In" ValidationGroup="Login1"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </LayoutTemplate>
                    </asp:Login>
                </asp:Panel>
                 <asp:Label ID="LabelResult" Text="" Font-Size="Large" ForeColor="Red" runat="server"></asp:Label>
            </div>
        </div>
    </div>
 
    

   </form>
</body>
</html>
