<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DropDownTest.aspx.cs" Inherits="DropDownTest"  Async="true" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">  
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <asp:Label ID="selectedMould" Text="" Font-Size="Large" ForeColor="Green" runat="server" ></asp:Label>         
            <div>
                <telerik:RadDropDownList RenderMode="Lightweight" ID="RadDropDownList1" DefaultMessage="Escolha um modulo" AutoPostBack="true"
     OnSelectedIndexChanged="SelectedIndexChanged" runat="server">
                </telerik:RadDropDownList>
            </div>
        </asp:ScriptManager>
    </form>
</body>
</html>
