<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConfirmationPopup.aspx.vb"
    Inherits="OnlineForm_Popup_ConfirmationPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Warning</title>
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">

        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }

        function ClosePopup() {
            //add the required functionality
            window.close();
        }
    </script>
    <style type="text/css">
        .style1
        {
            width:50%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td class="style1">
               
                    <table style="margin-left: 30px; width:82%;">
                        <tr style="height:15px; text-align: center;">
                            <td class="style1">
                                <asp:Label ID="Label17" runat="server" Font-Size="16" Font-Italic="true" ForeColor="Red"
                                    Font-Bold="true" Text="Warning"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height:90px;">
                            <td class="style1">
                                <asp:Label ID="Label1" runat="server" Style="margin-left: 3px;font-family:Optima;" Font-Bold="false"
                                    Font-Size="13" Text="This message does not belong to you as you are"></asp:Label>
                                <asp:Label ID="Label13" runat="server" Style="margin-left: 5px;font-family:Optima;" Font-Bold="false"
                                    Font-Size="13" Text="logged-in with different User. Please click OK to continue."></asp:Label>
                                
                            </td>
                        </tr>
                        <tr style="height: 50px; vertical-align: middle; text-align: center;">
                            <td class="style1">
                                <asp:Button ID="ok" runat="server" Text="OK" CommandName="OK" style="margin-left: 0px"
                                    Width="50px" />
                            </td>
                        </tr>
                    </table>
               
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
