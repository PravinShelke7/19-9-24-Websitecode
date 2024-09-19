<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Masters/Schem1Chart.master"
    CodeFile="CEolCharts.aspx.vb" Inherits="Charts_Schem1Charts_CEolCharts" Title="Schem1-Combine Customer End Of Life Chart" %>

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
                                        <asp:DropDownList ID="ddltype" runat="server" CssClass="dropdown" Width="365px">
                                            <asp:ListItem Text="Product-Raw Materials" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="product-Production Waste" Value="2"></asp:ListItem>
                                            <asp:ListItem Text="Product-Finished Product" Value="3"></asp:ListItem>
                                            <asp:ListItem Text="Raw Material Packaging-Total Flow" Value="4"></asp:ListItem>
                                            <asp:ListItem Text="Raw Material Packaging-Waste" Value="5"></asp:ListItem>
                                            <asp:ListItem Text="Product Packaging-Total Flow" Value="6"></asp:ListItem>
                                            <asp:ListItem Text="Product Packaging-Waste" Value="7"></asp:ListItem>
                                            <asp:ListItem Text="Other Waste-Plant" Value="8"></asp:ListItem>
                                            <asp:ListItem Text="Other Waste-Office" Value="9"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Conversion Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown" Width="200" AutoPostBack="false">
                                            <asp:ListItem Text="Material Balance" Value="MatBalance"></asp:ListItem>
                                            <asp:ListItem Text="Material to Recycling" Value="MatRec"></asp:ListItem>
                                            <asp:ListItem Text="Material to Incineration" Value="MatInc"></asp:ListItem>
                                            <asp:ListItem Text="Material to Composting" Value="MatComp"></asp:ListItem>
                                            <asp:ListItem Text="Material to Landfill" Value="MatLandFill"></asp:ListItem>
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
