<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Items.aspx.cs" Inherits="Items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/Items.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <div class="main_body">
        <div class="head_image">
            <asp:Image ID="headImage" runat="server"/>
        </div>
        <div class="head_kid_info">
            <div class="head_kid_name">
                <asp:Label ID="name" class="text_name" runat="server" Text="名字"></asp:Label>
                <asp:Label ID="age" class="text_age" runat="server" Text="x岁"></asp:Label>
            </div>
            <div class="head_kid_age">
                <asp:Label ID="school" class="text_age" runat="server" Text="xxx学校"></asp:Label>
                <asp:Label ID="grade" class="text_age" runat="server" Text="x年级"></asp:Label>
            </div>
        </div>
        <div style="height:20px">
        </div>
        <div>
        <div class="item">
            现有积分：<asp:Label ID="point" runat="server" Text="Label">0</asp:Label>分
        </div>
        <div style="height:5px">
        </div>
        <div class="item">
            动画碎片：<asp:Label ID="animate_piece" runat="server" Text="Label">0</asp:Label>个
        </div>
        <div class="item">
            玩具碎片：<asp:Label ID="toy_piece" runat="server" Text="Label">0</asp:Label>个
        </div>
        <div class="item">
            美食碎片：<asp:Label ID="food_piece" runat="server" Text="Label">0</asp:Label>个
        </div>
        <div class="item">
            陪玩碎片：<asp:Label ID="play_piece" runat="server" Text="Label">0</asp:Label>个
        </div>
        </div>
    </div>
</div>
</asp:Content>

