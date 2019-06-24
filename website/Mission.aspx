<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Mission.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Mission.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="text_title">
        每周任务进度
    </div>
    <div class="weeky_progress_block">
        <div class="weeky_progress_out">
            <div class="weeky_progress_point"><asp:Label ID="weeky_point" runat="server" Text="50%"></asp:Label>/500</div>
            <div class="weeky_progress_in" ID="weeky_progress" runat="server" style="width:10%"></div>
        </div>
    </div>
    <div class="weeky_progress_block">
        <div class="weeky_award">
            <asp:Label ID="weeky_award_text" runat="server" Text="Label"></asp:Label>
        </div>
        <div class="weeky_award_button" ID="weeky_button" runat="server">
            <asp:HyperLink ID="GetAward" runat="server">领取奖励</asp:HyperLink>
        </div>
    </div>

    <div class="text_title">
        每日任务进度</div>
    <asp:Repeater ID="Repeater1" runat="server" 
        DataSourceID="DailyMissionDataSource">
    <ItemTemplate>
    <div class ="daily_mission_block">
        <div class="weeky_award"><%#Eval("MissionText")%></div>
        <div class="daily_mission_button" style=<%#Eval("ButtonStyle")%>><a href=<%#Eval("ButtonUrl")%>><%#Eval("ButtonText")%></a></div>
    </div>
    </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="DailyMissionDataSource" runat="server" 
        SelectMethod="GetDailyMissionState" TypeName="XmlDatabase"></asp:ObjectDataSource>
</asp:Content>
