<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="History.aspx.cs" Inherits="History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
    <link href="Styles/History.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    <script type="text/javascript">
        window.onload = function () {
            map_height = document.documentElement.clientHeight;
            map_height = map_height - 100 - 88;
            document.getElementById("details").style.height = map_height + "px";
        }
    </script> 
    <div class="main_body">
        <div class="droplist">
            <asp:DropDownList ID="RecordSelector" runat="server" AutoPostBack="True" 
                DataSourceID="RecordSelectorDataSource" DataTextField="Description" 
                DataValueField="Id" CssClass="droplist">
            </asp:DropDownList>
        </div>
        <div id="details" class="detail_view">
            <asp:Repeater ID="DetailRepeater" runat="server" DataSourceID="DetailDataSource">
                <ItemTemplate>
                    <div class="item">
                        <div class="item_point">
                            <%# GetPoint(Convert.ToInt32(Eval("Point").ToString())) %>
                        </div>
                        <div class="item_desciprtion">
                            <%#Eval("Description")%>
                        </div>
                        <div class="item_time">
                            <%#Eval("Time")%>
                        </div>

                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
    <asp:ObjectDataSource ID="RecordSelectorDataSource" runat="server" 
        SelectMethod="GetHistoryRecords" TypeName="HistoryController">
        <SelectParameters>
            <asp:CookieParameter CookieName="userid" Name="userid" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="DetailDataSource" runat="server"
        SelectMethod="GetDetailsList" TypeName="RecordController">
        <SelectParameters>
            <asp:CookieParameter CookieName="userid" Name="userid" Type="String" />
            <asp:ControlParameter ControlID="RecordSelector" DefaultValue="0" 
                Name="record_id" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

