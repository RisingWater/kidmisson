<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/Login.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="main_view">
        <div class="login_object login_text">用户名:</div>
        <div class="login_object">
            <asp:TextBox ID="UserNameTextBox" runat="server" CssClass="textbox"></asp:TextBox>
        </div>
        <div class="login_object login_text">密码:</div>
        <div class="login_object">
            <asp:TextBox ID="PasswordTextBox" runat="server" CssClass="textbox" TextMode="Password"></asp:TextBox>
        </div>
        <div class="login_object error_div">
            <asp:Label ID="ErrorMessageLabel" runat="server" Text="errormessage"></asp:Label>
        </div>
        <div class="login_object">
            <div class="login_button" onclick="JAVAscript:document.getElementById('MainContent_LoginButton').click();">登录</div>
            <asp:Button ID="LoginButton" runat="server" Text="登录" BorderStyle="None" style="display:none" OnClick="Login_Click" />
        </div>
    </div>
</asp:Content>

