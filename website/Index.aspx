<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/Index.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        window.onload = function () {
            map_height = document.documentElement.clientHeight;
            map_height = map_height - 120;
            document.getElementById("main_view").style.height = map_height + "px";
        }
    </script> 
    <div id="main_view" class="main_view">
        <div class="pic_view">
            <div style="height:50px"></div>
            <div class="title_view">              
            </div>
        </div>
        <div class="login_object">
            <div class="login_button" onclick="JAVAscript:document.getElementById('MainContent_LoginButton').click();">登录</div>
            <asp:Button ID="LoginButton" runat="server" Text="登录" BorderStyle="None" style="display:none" OnClick="Login_Click" />
        </div>
    </div>
</asp:Content>

