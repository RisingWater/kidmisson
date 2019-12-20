<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Mission.aspx.cs" Inherits="_Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Mission.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sweetalert.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/sweetalert.min.js"></script>
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
        <div class="weeky_award_button" ID="weeky_button" runat="server" onclick="JAVAscript:document.getElementById('MainContent_GetAward').click();">领取奖励</div>
        <div class="weeky_award">
            <asp:Label ID="weeky_award_text" runat="server" Text="Label"></asp:Label>
        </div>
        <asp:Button ID="GetAward" runat="server" Text="领取奖励" BorderStyle="None" style="display:none" OnClick="WeekyAward_Click" />
    </div>

    <div class="text_title">
        每日任务进度</div>
    <asp:Repeater ID="DailyMissionRepeater" runat="server" 
        DataSourceID="DailyMissionDataSource" 
        onitemcommand="DailyMissionRepeater_ItemCommand">
    <ItemTemplate>
    <div class ="daily_mission_block">
        <div class="mission_imge">
            <img src=<%# GetMissionImage(Eval("ImageFileName").ToString()) %> />
        </div>
        <div class="mission_button" style=<%# GetButtonColorStyle(Convert.ToBoolean(Eval("Done").ToString())) %>
            onclick=<%# GetJavaScript(Container.ItemIndex) %>>
            <%# GetButtonText(Convert.ToBoolean(Eval("Done").ToString())) %>
        </div>
        <div class="mission_name"><%# Eval("Name") %></div>
        <div class="mission_detail"><%# Eval("Description") %></div>
        <asp:Button ID="completeMission" runat="server" Text="completeMission" style="display:none" 
            CommandName="completeMission" CommandArgument='<%#Eval("Id") %>'/>
    </div>
    </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="DailyMissionDataSource" runat="server" 
        SelectMethod="GetMissionList" TypeName="DailyMissionsController">
        <SelectParameters>
            <asp:CookieParameter CookieName="userid" Name="userid" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
