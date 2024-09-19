<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConfirmationPopup.aspx.vb"
    Inherits="OnlineForm_Popup_ConfirmationPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title></title>
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 50%;
        }
    </style>
    <script language="javascript" type="text/javascript">

        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }

        function ClosePopup() {

            //add the required functionality
            window.opener.document.getElementById('btnClos').click();
            window.close();
        }
              

    </script>
</head>
<body style=" background-color : White ;">
    <form id="form1" runat="server">
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td class="style1">
                <table style="margin-left: 30px; width: 82%;">
                    <tr style="height: 60px;">
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server" Style="margin-left: 3px; font-family: Optima;"
                                Font-Bold="true" Font-Size="13" Text="Thank you for contacting us. Your request has been recieved."></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 50px; vertical-align: middle; text-align: center;">
                        <td class="style1">
                            <asp:Button ID="ok" runat="server" Text="OK" CommandName="OK" Style="margin-left: 0px"
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
