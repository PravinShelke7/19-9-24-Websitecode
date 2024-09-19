<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Price_CostPopup.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_Price_CostPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Price and Cost</title>
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
                        <asp:Label ID="Label1" text="Price and Cost" runat="server"></asp:Label>
                    </div>
                    <div id="error">
                        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <b>SKU:</b><asp:Label ID="lblSKU" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <span id="caseDe3" runat="server"><b>SKU Description:</b></span>
                                <asp:Label ID="lblSKUDesc" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr id="Tr3" style="height: 20px" runat="server">
                <td id="Td3" runat="server">
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left; height: 550px; width: 600px;">
                            <asp:Table Width="50%" ID="tblPC" runat="server">
                            </asp:Table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
