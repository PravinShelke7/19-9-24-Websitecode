<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PriceCostM.aspx.vb" Inherits="Pages_SavvyPackPro_Resultspl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Price and Cost Manager</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>
    <form id="form1" runat="server">
    <div id="ContentPagemargin">
        <div id="Div1">
            <asp:Image ImageAlign="AbsMiddle" Width="850px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPro.gif"
                runat="server" />
        </div>
        <div id="error">
            <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Material Assumption')"
                        onmouseout="UnTip()" style="width: 840px;">
                        <asp:Label ID="Label1" runat="server" Text="Price and Cost Manager"></asp:Label>
                    </div>
                    <table border="0" id="tblCaseDes" runat="server" cellpadding="0" cellspacing="0">
                        <tr style="height: 20px">
                            <td style="width: 400px;">
                            </td>
                            <td style="width: 350px; text-align: Left;">
                                <b>SKU:</b><asp:Label ID="lblSKU" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 20px">
                            <td style="width: 400px;">
                            </td>
                            <td colspan="2">
                                <span id="caseDe3" runat="server"><b>SKU Description:</b></span>
                                <asp:Label ID="lblSKUDesc" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div runat="server" style="text-align: left; width: 840px;">
            <div id="PageSection1" style="text-align: left;">
                <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                </asp:Table>
                <br />
                <asp:TextBox ID="txthiddien" Style="visibility: hidden;" runat="server" Text="0"></asp:TextBox>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
