<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="PersonInfo.aspx.cs" Inherits="PersonInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/PersonInfo.css" rel="stylesheet" type="text/css" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <div>
    <div class="main_body">
        <div class="head_image">
            <asp:Image ID="HeadImage" runat="server" Width="80px" Height="80px"/>
        </div>
        <div class="head_kid_info">
            <div class="head_kid_name">
                <asp:Label ID="NameLabel" class="text_name" runat="server" Text="名字"></asp:Label>
                <asp:Label ID="AgeLabel" class="text_age" runat="server" Text="x岁"></asp:Label>
            </div>
            <div class="head_kid_age">
                <asp:Label ID="SchoolLabel" class="text_age" runat="server" Text="xxx学校"></asp:Label>
                <asp:Label ID="GradeLabel" class="text_age" runat="server" Text="x年级"></asp:Label>
            </div>
        </div>
        <div style="height:20px">
        </div>
        <div>
        <div class="item">
            现有积分：<asp:Label ID="PointLabel" runat="server" Text="Label">0</asp:Label>分
        </div>
        <div style="height:5px">
        </div>
            <asp:Repeater ID="PocketItemReaper" runat="server" 
                DataSourceID="PocketDataSource" onitemcommand="PocketItemReaper_ItemCommand">
                <ItemTemplate>
                    <div class="item">
                        <%#Eval("Description")%>: <%#Eval("Number")%>
                        <div class="exchange_button" onclick=<%# GetJavaScript(Container.ItemIndex) %>>兑换奖励</div>
                        <asp:Button ID="exchange" runat="server" Text="exchange" style="display:none" 
                            CommandName="exchange" CommandArgument='<%#Eval("Id") %>'/>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
        <div class="history_botton" onclick="javascript:location.href='./History.aspx' ">历史记录</div>
        <asp:ObjectDataSource ID="PocketDataSource" runat="server" 
            SelectMethod="GetItemList" TypeName="PocketController">
            <SelectParameters>
                <asp:CookieParameter CookieName="userid" Name="userid" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
</asp:Content>

