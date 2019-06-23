<%@ Page Title="关于我们" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Award.aspx.cs" Inherits="About" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Award.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="award">
    <div class="award_block" style="background-color:#a65c00;color:#ffffff" onclick="javascript:location.href='./AwardGet.aspx?openid=1' ">
        <div class="award_image">
            <image src="image/bronze.png">
        </div>
        <div class="award_text">打开铜宝箱</div>
    </div>
</div>
<div class="award">
    <div class="award_block" style="background-color:#cccccc;color:#000000" onclick="javascript:location.href='./AwardGet.aspx?openid=2' ">
        <div class="award_image">
            <image src="image/sliver.png">
        </div>
        <div class="award_text">打开银宝箱</div>
    </div>
</div>
<div class="award">
    <div class="award_block" style="background-color:#ffde40;color:#000000" onclick="javascript:location.href='./AwardGet.aspx?openid=3' ">
        <div class="award_image">
            <image src="image/gold.png">
        </div>
        <div class="award_text">打开金宝箱</div>
    </div>
</div>
<div class="award">
    <div class="award_block" style="background-color:#79abcf;color:#ffffff" onclick="javascript:location.href='./AwardGet.aspx?openid=4' ">
        <div class="award_image">
            <image src="image/diamond.png">
        </div>
        <div class="award_text">打开白金宝箱</div>
    </div>
</div>
</asp:Content>
