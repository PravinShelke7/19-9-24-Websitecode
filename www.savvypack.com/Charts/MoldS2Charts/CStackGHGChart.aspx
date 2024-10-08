﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CStackGHGChart.aspx.vb"
    Inherits="Charts_MoldS2Charts_CStackGHGChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>S2 Mold-Combine GHG Stack Chart</title>
    <link rel="stylesheet" href="../../App_Themes/SkinFile/Econ3Style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: Verdana; font-size: 11px;">
        <table>
            <tr>
                <td align="center">
                    <asp:Image ID="Logo" ImageUrl="~/Images/Packaging_InformationLeft.gif" runat="server"
                        Height="87" Width="212" ToolTip="Allied Logo" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Image ID="line" ImageUrl="~/Images/Line.jpg" Width="750" Height="9" runat="server"
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
                            <table width="50%">
                                <tr>
                                    <td>
                                        <b>Conversion Type:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown" Width="200">
                                            <asp:ListItem Text="Total" Value="Total"></asp:ListItem>
                                            <asp:ListItem Text="Per Unit" Value="PUnit"></asp:ListItem>
                                            <asp:ListItem Text="Per Weight" Value="PWeight"></asp:ListItem>
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
                                        <asp:Button ID="bynUpdate" runat="server" Text="Update" Width="93px" Font-Names="Verdana" />
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
