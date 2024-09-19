<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreviewTerms.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_PreviewTerms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Preview Terms</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="ContentPage" id="Table1" runat="server" style="margin-top: 15px; width: 600px;">
            <tr>
                <td>
                    <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                        <asp:Label text="Preview Terms" runat="server"></asp:Label>
                    </div>
                    <div id="error">
                        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr id="Tr3" style="height: 20px" runat="server">
                <td id="Td3" runat="server">
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left; height: 250px; width: 600px;">
                        <asp:Label ID="lbltermS" text="Standard Terms: " Font-Bold ="true"  Font-Size ="12" runat="server"></asp:Label>
                            <asp:Table Width="50%" ID="tblterm" runat="server">
                            </asp:Table>
                            <br />
                        <asp:Label ID="lbltermC" text="Customize Terms: " Font-Bold ="true" Font-Size ="12" runat="server"></asp:Label>
                            <asp:Table Width="50%" ID="tbltermC" runat="server">
                            </asp:Table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidSpecId" runat="server" />
    </form>
</body>
</html>
