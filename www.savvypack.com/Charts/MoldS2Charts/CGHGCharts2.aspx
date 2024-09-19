<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Masters/MoldS2Chart.master"
    CodeFile="CGHGCharts2.aspx.vb" Inherits="Charts_MoldS2Charts_CGHGCharts2" Title="S2 Mold-Combine GHG Bar Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldS2ChartContentPlaceHolder" runat="Server">
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
            <table style="width: 710px">
                <tr>
                    <td align="center" style="background-color: #5B5B5B; width: 100%;">
                        <div style="margin-left: 10px; margin-top: 10px; text-align: left; color: White">
                            <table style="width: 54%">
                                <tr>
                                    <td>
                                        <b>Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="dropdown" Width="250">
                                            <asp:ListItem Text="Raw Materials" Value="RMGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Raw Materials Packaging" Value="RMPACKGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="RM & Pack Transport" Value="RMANDPACKTRNSPTGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Process" Value="PROCGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Distribution Packaging" Value="DPPACKGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="DP Transport" Value="DPTRNSPTGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Transport to Customer" Value="TRSPTTOCUSGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Process (S2)" Value="PROCGRNHUSGASS2"></asp:ListItem>
                                            <asp:ListItem Text="Packaged Product Packaging(S2)" Value="PACKGEDPRODPACKGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="PPP Transport(S2)" Value="PPPTRNSPTGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Packaged Product Transport(S2)" Value="PACKGEDPRODTRNSPTGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Purchased Materials" Value="PURMATERIALGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Total Process" Value="PROCESSGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Transportation" Value="TRNSPTGRNHUSGAS"></asp:ListItem>
                                            <asp:ListItem Text="Total Greenhouse Gas" Value="TOTALENGRNHUSGAS"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Conversion Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown" Width="200">
                                            <%--<asp:ListItem Text="Total" Value="Total"></asp:ListItem>--%>
                                            <asp:ListItem Text="Customer GHG Total" Value="CTotal"></asp:ListItem>
                                            <asp:ListItem Text="Customer GHG Per Unit" Value="CPUnit"></asp:ListItem>
                                            <asp:ListItem Text="Customer GHG Per Weight" Value="CPWeight"></asp:ListItem>
                                            <%--<asp:ListItem Text="Gasoline" Value="Gasoline"></asp:ListItem>--%>
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
