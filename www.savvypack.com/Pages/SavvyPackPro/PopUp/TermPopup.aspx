<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TermPopup.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_TermPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function ClosePage() {
            window.opener.document.getElementById('btnrefreshT').click();
            window.close();
        }

</script>
</head>

<body>
    <form id="form1" runat="server">
    <div>
        <table class="ContentPage" id="Table1" runat="server" style="margin-top: 15px; width: 600px;">
            <tr>
                <td>
                    <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                         <asp:Label ID="lblHeading" runat="server"></asp:Label>
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
                            <br />
                            <table style="text-align: left; width: 86%;">
                                <tr class="AlterNateColor1">
                                    <td style="width: 220px;">
                                        <asp:Label ID="lblItem" runat="server" Text="Item:" Font-Bold="True" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td class="style5">
                                        <asp:TextBox ID="txtI" runat="server" Width="74%" Font-Bold="True" CssClass="SavvyMediumTextBox"
                                            Font-Size="12px" Style="margin-left: 5px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 220px;">
                                        <asp:Label ID="lblTerm" runat="server" class="Link" Font-Size="12px" Text="Term:"
                                            Font-Bold="True"></asp:Label>
                                    </td>
                                    <td style="width: 2137px;">
                                        <asp:TextBox ID="txtT" runat="server" CssClass="SavvyMediumTextBox" TextMode="MultiLine"
                                            Style="margin-left: 5px;" MaxLength="1000" Width="75%" Height="80px" ToolTip=""></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 220px;">
                                    </td>
                                    <td style="width: 2137px;">
                                        <asp:Button ID="Add" runat="server" Text="Update" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidSpecId" runat="server" />
     <asp:HiddenField ID="hidRfpID" runat="server" />
    </form>
</body>
</html>
