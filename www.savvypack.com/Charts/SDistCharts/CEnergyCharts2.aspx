<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CEnergyCharts2.aspx.vb"
    Inherits="Charts_SDistCharts_CEnergyCharts2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SDist-Combine Customer Energy Bar Chart</title>
    <link rel="stylesheet" href="../../App_Themes/SkinFile/Econ3Style.css" />
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: Verdana; font-size: 11px;">
        <table>
            <tr>
                <td align="center">
                    <div id="AlliedLogo">
                        <table>
                            <tr>
                                <td class="PageSHeading" align="center">
                                    <table style="width: 845px; background-color: #edf0f4;">
                                        <tr>
                                            <td align="left">
                                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="line" ImageUrl="~/Images/Line.jpg" width="750" height="9" runat="server"
                        ToolTip="Allied Logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblsession" Text="Your Username and/or Password are not currently valid. Your session may have timed out. Please close your current windows accessing SavvyPack Corporation's Economic Service and sign in again"
                        Visible="false" runat="server"></asp:Label>
                </td>
        </table>
        <div id="Charts" runat="server">
            <a class="Logoff" href="../ChartPreferences/ChartPreferencesS.aspx" target="_blank">
                Chart Preferences</a>
            <br />
            <table style="width: 710px">
                <tr>
                    <td align="center" style="background-color: #5B5B5B; height: 50px;">
                        <asp:Label ID="lblHeading" runat="server" ForeColor="White" Font-Bold="true" Font-Size="18px">
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
                                            <asp:ListItem Text="Raw Materials" Value="RMERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Raw Materials Packaging" Value="RMPACKERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="RM & Pack Transport" Value="RMANDPACKTRNSPTERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Process" Value="PROCERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Distribution Packaging" Value="DPPACKERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="DP Transport" Value="DPTRNSPTERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Transport to Customer" Value="TRSPTTOCUS">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Process Energy(S2)" Value="PROCERGYS2">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Packaged Product Packaging(S2)" Value="PACKGEDPRODPACK">
                                            </asp:ListItem>
                                            <asp:ListItem Text="PPP Transport(S2)" Value="PPPTRNSPT">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Packaged Product Transport(S2)" Value="PACKGEDPRODTRNSPT">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Purchased Materials" Value="PURMATERIALERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Total Process" Value="TPROCERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Transportation" Value="TRNSPTERGY">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Total Energy" Value="TOTALENERGY">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Conversion Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown" Width="200">
                                            <asp:ListItem Text="Total" Value="Total">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Customer Total" Value="CTotal">
                                            </asp:ListItem>
                                            <asp:ListItem Text="Electricity" Value="Electricity">
                                            </asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <%-- <tr>
                            <td>
                                <b>Chart Type:</b>
                            </td>
                             <td align="left">
                                <asp:DropDownList ID="ddlChType" runat="server" CssClass="dropdown"  Width="200">
                                    <asp:ListItem  Text="Bar Chart" Value="Sargento_KraftPouchvsTray_Html3"></asp:ListItem>
                                </asp:DropDownList> 
                            </td>
                         </tr>--%>
                                <tr>
                                    <td>
                                    </td>
                                    <td align="left">
                                        <asp:Button id="bynUpdate" runat="server" Text="Update" Width="93px" Font-Names="Verdana" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    </form>
</body>
</html>
