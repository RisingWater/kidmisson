<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="AwardGet.aspx.cs" Inherits="AwardGet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/Award.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div style="height:30px"></div>
    <div style="margin:0px auto;width:96px">
        <asp:Image ID="BoxImage" runat="server" />
    </div>
    <div style="height:30px"></div>
    <div class="award_get_text">
        打开了<asp:Label ID="BoxName" runat="server" Text="铜宝箱"></asp:Label>，你获得了
    </div>
    <div style="height:10px"></div>
    <div class="award_get_text2">
        <asp:Label ID="ItemName" runat="server" Text="XX碎片*X"></asp:Label>
    </div>
    <div style="height:20px"></div>
    <div class="back_botton"><asp:HyperLink ID="BackButton" runat="server">返回</asp:HyperLink></div>
    
</asp:Content>

