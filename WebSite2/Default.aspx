<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="tutor.Default" validateRequest="false"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:TextBox ID="Problem" runat="server" TextMode="MultiLine" rows="5" width="420"/>
        </div>
        <div>
            <asp:TextBox ID="Code" runat="server" TextMode="MultiLine" rows="20" width="420"/>
        </div>
        <div>
            <asp:Button ID="cmdSubmit" runat="server" Text="Отправить" OnClick="cmdSubmit_Click" />
        </div>
        <div>
            <asp:Label ID="Label1" runat="server" />
        </div>
    </form>
</body>
</html>
