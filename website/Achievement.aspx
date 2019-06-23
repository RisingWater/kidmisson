<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Achievement.aspx.cs" Inherits="Achievement" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/Achievement.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <asp:Repeater ID="Repeater1" runat="server" 
        DataSourceID="AchievementDataSource">
    <ItemTemplate>
        <div class="achievement">
            <div class="achievenment_block">
                <div class="achievenment_text"><%#Eval("Description")%></div>
            </div>
            <div class="achievenment_block">
                <div class="achievenment_progress_out">
                    <div class="achievenment_point"><%#Eval("Progress")%></div>
                    <div class="achievenment_progress_in" style=<%#Eval("ProgressBarStyle")%>></div>
                </div>
            </div>

            <div class="achievenment_block">
                <div class="achievenment_award">奖励积分:<%#Eval("Point")%>分</div>
                <div class="achievenment_button" style=<%#Eval("ButtonStyle")%>><a href=<%#Eval("ButtonUrl")%>>领取奖励</a></div>
            </div>
        </div>
    </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="AchievementDataSource" runat="server" 
        SelectMethod="GetAllAchievement" TypeName="XmlDatabase">
    </asp:ObjectDataSource>
</asp:Content>

