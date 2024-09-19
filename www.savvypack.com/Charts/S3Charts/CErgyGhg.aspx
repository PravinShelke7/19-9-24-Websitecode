<%@ Page Language="VB" MasterPageFile="~/Masters/S3Charts.master" AutoEventWireup="false"
    CodeFile="CErgyGhg.aspx.vb" Inherits="Charts_S3Charts_CErgyGhg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" runat="Server">

    <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../JavaScripts/tip_balloon.js"></script>

    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <br />
            <table style="width: 710px">
                <tr>
                    <td align="center" style="height: 50px;">
                        <asp:Label ID="lblHeading" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="18px">                
                        </asp:Label>
                        <br />
                        <asp:Label ID="lblHeadingS" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="14px">                
                        </asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 710px">
                <tr>
                    <td>
                        <div id="EnergyComp" runat="server">
                        </div>
                    </td>
                </tr>
            </table>
            <table style="width: 810px">
                <tr>
                    <td align="center" style="width: 100%;">
                        <div style="margin-left: 10px; margin-top: 10px; text-align: left;">
                            <table width="60%">
                                <tr id="Tr2" runat="server">
                                    <td>
                                        <b>Page Name:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPName" runat="server" CssClass="DropDown" Width="200" AutoPostBack="true">
                                            <asp:ListItem Text="Energy" Value="ERGY"></asp:ListItem>
                                            <asp:ListItem Text="Green House Gas" Value="GHG"></asp:ListItem>
                                            <asp:ListItem Text="Water" Value="WATER"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="Tr1" runat="server">
                                    <td>
                                        <b>Chart Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlChartType" runat="server" CssClass="DropDown" Width="200"
                                            AutoPostBack="true">
                                            <asp:ListItem Text="Regular Bar Chart" Value="RBC"></asp:ListItem>
                                            <asp:ListItem Text="Pie Chart" Value="PIE"></asp:ListItem>
                                            <asp:ListItem Text="Stacked Bar Chart" Value="SBAR"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr id="ROWCategoryset" runat="server" visible="false">
                                    <td>
                                        <b>Category Set:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCategorySet" runat="server" CssClass="DropDown" Width="200">
                                        </asp:DropDownList>
                                    </td>
                                    <td>
                                        <a href="../../Pages/Sustain3/Category/ManageCategory.aspx" class="Link" style="font-size: 13px;
                                            font-weight: bold">Manage Category Set</a>
                                    </td>
                                </tr>
                                <tr id="rwType" runat="server">
                                    <td>
                                        <b>Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="DropDown" Width="200">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Conversion Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCnType" runat="server" CssClass="DropDown" Width="200">
                                            <asp:ListItem Text="Total" Value="Total"></asp:ListItem>
                                            <asp:ListItem Text="Per Unit" Value="PUnit"></asp:ListItem>
                                            <asp:ListItem Text="Per Weight" Value="PWeight"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="bynUpdate" runat="server" Text="Update" Width="93px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
