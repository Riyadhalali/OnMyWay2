<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="start.aspx.cs" Inherits="OnMyWay2.Controllers.start" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" rel="stylesheet" />
    <script src="js/script.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-page">
            <div class="form">
                <asp:TextBox ID="txtusername" placeholder="username" required runat="server"></asp:TextBox>
                <asp:TextBox ID="txtpassword" placeholder="password" required TextMode="Password" runat="server"></asp:TextBox>
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                    <asp:ListItem>Chose your account type </asp:ListItem>
                  
                    <asp:ListItem>Admin</asp:ListItem>
                </asp:DropDownList>

                <asp:Button ID="btnLogin" runat="server" Text="Login" BackColor="Green" ForeColor="White" OnClick="btnLogin_Click" />
                <asp:Label ID="Label1" runat="server"></asp:Label>

                &nbsp;<br />
                <br />

                <asp:Label ID="timelable" runat="server" Text="timelable"></asp:Label>
                    <br />

                <asp:Label ID="status" runat="server" Text="conncetion_status"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>
