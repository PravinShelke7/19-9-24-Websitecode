<%@ Page Language="VB" MasterPageFile="~/Masters/Schem1Chart.master" AutoEventWireup="false"
    CodeFile="CEnergyCharts2.aspx.vb" Inherits="Charts_Schem1Charts_CEnergyCharts2" Title="Schem1-Combine Customer Energy Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Schem1ContentPlaceHolder" runat="Server">

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
                            <table style="width: 56%">
                                <tr>
                                    <td>
                                        <b>Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="dropdown" Width="250">
                                            <asp:ListItem Text="Raw Materials" Value="RMERGY"></asp:ListItem>
                                            <asp:ListItem Text="Raw Materials Packaging" Value="RMPACKERGY"></asp:ListItem>
                                            <asp:ListItem Text="RM & Pack Transport" Value="RMANDPACKTRNSPTERGY"></asp:ListItem>
                                            <asp:ListItem Text="Process" Value="PROCERGY"></asp:ListItem>
                                            <asp:ListItem Text="Distribution Packaging" Value="DPPACKERGY"></asp:ListItem>
                                            <asp:ListItem Text="DP Transport" Value="DPTRNSPTERGY"></asp:ListItem>
                                            <asp:ListItem Text="Transport to Customer" Value="TRSPTTOCUS"></asp:ListItem>
                                            <asp:ListItem Text="Process Energy(Schem1)" Value="PROCERGYS2"></asp:ListItem>
                                            <asp:ListItem Text="Packaged Product Packaging(Schem1)" Value="PACKGEDPRODPACK"></asp:ListItem>
                                            <asp:ListItem Text="PPP Transport(Schem1)" Value="PPPTRNSPT"></asp:ListItem>
                                            <asp:ListItem Text="Packaged Product Transport(Schem1)" Value="PACKGEDPRODTRNSPT"></asp:ListItem>
                                            <asp:ListItem Text="Purchased Materials" Value="PURMATERIALERGY"></asp:ListItem>
                                            <asp:ListItem Text="Total Process" Value="TPROCERGY"></asp:ListItem>
                                            <asp:ListItem Text="Transportation" Value="TRNSPTERGY"></asp:ListItem>
                                            <asp:ListItem Text="Total Energy" Value="TOTALENERGY"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Conversion Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown" Width="200">
                                            <asp:ListItem Text="Total" Value="Total"></asp:ListItem>
                                            <asp:ListItem Text="Customer Total" Value="CTotal"></asp:ListItem>
                                            <asp:ListItem Text="Electricity" Value="Electricity"></asp:ListItem>
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
