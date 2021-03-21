<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DropDownTest.aspx.cs" Inherits="DropDownTest"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">  
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <asp:Label ID="MouldsListLabel" Text="" Font-Size="Large" ForeColor="Green" runat="server" ></asp:Label>
          <asp:Button ID="callMoulds" runat="server" Text="Call Moulds" OnClick="callMouldss"></asp:Button>
            <div>
                <telerik:RadDropDownList RenderMode="Lightweight" ID="RadDropDownList1" EmptySelectionMessage="Escolher um modulo" runat="server">
                </telerik:RadDropDownList>
            </div>
        </asp:ScriptManager>
    </form>
</body>
</html>
