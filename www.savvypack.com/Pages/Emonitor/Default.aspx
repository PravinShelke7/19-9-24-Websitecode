<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Emonitor_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Emonitor-Global Manager</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        javascript: window.history.forward(1);
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
        <div id="AlliedLogo">
            <table width="100%">
                <tr style="background-color: #edf0f4;">
                    <td>
                        <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                        <asp:ImageButton ID="imgLogoff" Style="margin-left: 320px;" ImageUrl="~/Images/LogOff.gif"
                            runat="server" ToolTip="Log Off" Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=EMON" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" style="width: 840px;">
                    EMonitor - Data Manager
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left;">
                        <br />
                        <table width="98%" style="text-align: left;">
                            <tr class="PageSSHeading" style="height: 20px;">
                                <td class="TdHeading" style="width: 56px">
                                </td>
                                <td class="TdHeading" style="width: 223px">
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 56px">
                                    Report1
                                </td>
                                <td class="StaticTdLeft" style="width: 223px">
                                    <a href="DataManager.aspx" class="Link" target="_blank">GHG releases by SKU</a>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2">
                                <td class="StaticTdLeft" style="width: 56px">
                                    Report2
                                </td>
                                <td class="StaticTdLeft" style="width: 223px">                                    
                                    <a href="GHGTotal.aspx" class="Link" target="_blank">GHG releases Total</a>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
