<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PricePopup.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_PricePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Price</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />   
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table width="100%" style="margin-top:5px;">
                <tr>
                    <td>                        
                        <div id="error">
                            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div id="ContentPagemargin" runat="server">
                            <div id="PageSection1" style="text-align: left; overflow: auto;">
                                <asp:Table ID="tblP" runat="server">
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
