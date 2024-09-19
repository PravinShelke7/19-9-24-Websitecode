<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewManufactoryDetails.aspx.vb"
    Inherits="Pages_PopUp_ViewManufactoryDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Manufactory Details</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .TdHeadingNew
        {
            background-color: #58595B;
            color: White;
            font-family: Optima;
            font-size: 12px;
        }
    </style>
    <script type="text/JavaScript">
        function ClosePage() {

            window.opener.document.getElementById('btnGrid').click();
            window.close();
        }
                         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <div style="vertical-align: middle; font-weight: bold; text-align: center; font-size: large;">
                Manufactory Process Details</div>
            </br>
            <table cellpadding="0" cellspacing="2" width="400px" style="margin-left: 20px;">
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Manufactory Process Name:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td style="word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Order Size:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td style="word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblOSize" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Run Size:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td style="word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblRSize" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Table ID="tblManuf" runat="server">
            </asp:Table>
            <br />
        </div>
    </div>
    <asp:HiddenField ID="hidName" runat="server" />
    </form>
</body>
</html>
