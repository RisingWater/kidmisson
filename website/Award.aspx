<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Award.aspx.cs" Inherits="Award" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Award.css" rel="stylesheet" type="text/css" />
    <link href="Styles/sweetalert.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <asp:Repeater ID="AwardRepeater" runat="server" DataSourceID="AwardDataSource" 
        onitemcommand="AwardRepeater_ItemCommand">
        <ItemTemplate>
            <div class="award">
                <div class="award_block" style=<%# GetStyle(Container.ItemIndex) %>
                    onclick=<%# GetJavaScript(Container.ItemIndex)%> >
                    <div class="award_image">
                        <image src=<%# GetImage(Eval("AwardImageFileName").ToString()) %>>
                    </div>
                    <div class="award_text">
                        打开<%#Eval("Description") %></div>
                    <asp:Button ID="open" runat="server" Text="open" style="display:none" 
                        CommandName="open" CommandArgument='<%#Eval("Id") %>' />
                </div>
            </div>
        </ItemTemplate>
    </asp:Repeater>
    <asp:ObjectDataSource ID="AwardDataSource" runat="server" 
    SelectMethod="GetAwardBoxs" TypeName="AwardController">
        <SelectParameters>
            <asp:CookieParameter CookieName="userid" Name="userid" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
