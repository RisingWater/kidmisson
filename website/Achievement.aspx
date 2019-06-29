<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Achievement.aspx.cs" Inherits="Achievement" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/Achievement.css" rel="stylesheet" type="text/css" />
    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Repeater ID="AchievementRepeater" runat="server" 
        DataSourceID="AchievementDataSource" 
        onitemcommand="AchievementRepeater_ItemCommand">
    <ItemTemplate>
        <div class="achievement">
            <div class="achievenment_block">
                <div class="achievenment_text"><%#Eval("Description")%></div>
            </div>
            <div class="achievenment_block">
                <div class="achievenment_progress_out">
                    <div class="achievenment_point"><%#Eval("Progress")%>/<%#Eval("Target")%></div>
                    <div class="achievenment_progress_in" style=<%# GetProgresBarStyle(Convert.ToInt32(Eval("Progress").ToString()), Convert.ToInt32(Eval("Target").ToString())) %>></div>
                </div>
            </div>

            <div class="achievenment_block">
                <div class="achievenment_award">奖励积分:<%#Eval("Award")%>分</div>
                <div class="achievenment_button" 
                    style=<%# GetButtonStyle(Convert.ToBoolean(Eval("Done").ToString()), Convert.ToInt32(Eval("Progress").ToString()), Convert.ToInt32(Eval("Target").ToString()))%>
                    onclick=<%# GetJavaScript(Container.ItemIndex) %>>
                    领取奖励
                </div>
                <asp:Button ID="getaward" runat="server" Text="getaward" style="display:none" 
                            CommandName="getaward" CommandArgument='<%#Eval("Id") %>'/>
            </div>
        </div>
    </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="AchievementDataSource" runat="server" 
        SelectMethod="GetAchievementGroupList" TypeName="AchievementController">
        <SelectParameters>
            <asp:CookieParameter CookieName="userid" Name="userid" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

