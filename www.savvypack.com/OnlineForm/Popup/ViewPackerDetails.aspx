<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewPackerDetails.aspx.vb"
    Inherits="Pages_PopUp_ViewMaterialDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Material Details</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function ClosePage() {

            window.opener.document.getElementById('btnLoad').click();
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
                Material Details</div>
            <table cellpadding="0" cellspacing="2" width="400px" style="text-align: center; vertical-align: middle;
                margin-left: 50px;">
                <tr class="AlterNateColor1">
                    <td align="left" style="width: 50%;">
                        <b>
                            <asp:Label runat="server" Text="Packer's Name:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td align="left" style="width: 50%; word-wrap: normal; word-break: break-all;">
                        <b>
                            <asp:Label ID="lblName" runat="server" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left" style="width: 50%;">
                        <b>
                            <asp:Label runat="server" Text="Location:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td align="left" style="width: 50%; word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblLoc" runat="server" CssClass="SavvyPackLabel"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left" style="width: 50%;">
                        <b>
                            <asp:Label runat="server" Text="Capacity:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td align="left" style="width: 50%; word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblCap" runat="server" CssClass="SavvyPackLabel"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left" style="width: 50%;">
                        <b>
                            <asp:Label runat="server" Text="Lines:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td align="left" style="width: 50%; word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblLines" runat="server" CssClass="SavvyPackLabel"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left" style="width: 50%;">
                        <b>
                            <asp:Label runat="server" Text="Additional Information:" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td align="left" style="width: 50%; word-wrap: normal; word-break: break-all;">
                        <asp:Label ID="lblInfo" runat="server" CssClass="SavvyPackLabel"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
    </form>
</body>
</html>
