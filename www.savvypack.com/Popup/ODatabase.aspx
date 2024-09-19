<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ODatabase.aspx.vb" Inherits="Popup_ODatabase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SavvyPack Services</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <style type="text/css">
        .IEpblm
        {
            z-index: 100;
        }
        
        .ServicrList
        {
            background-color: #F5F5F5;
        }
        
        .Service
        {
            color: #000000;
            display: block;
            width: 100%;
            cursor: pointer;
            outline: none;
            font-family: Optima;
            font-size: 15px;
        }
        
        .Service:hover
        {
            color: #FF3300;
        }
        
        .nonService
        {
            color: Gray;
            display: block;
            width: 100%;
            cursor: default;
            outline: none;
            font-family: Optima;
        }
        .AlterNateColor1
        {
            background-color: #C0C9E7;
            height: 20px;
        }
        
        .AlterNateColor2
        {
            background-color: #D0D1D3;
            height: 20px;
        }
    </style>
    <script type="text/javascript">
        function ClosePop(serv) {
            var hidTid = document.getElementById('<%= hidId.ClientId %>');
            var ServId = document.getElementById(serv).getAttribute("CommandArgument");
            window.opener.document.getElementById('ctl00_hidTId').value = ServId;
            if (serv == 'lnkCont') {
                window.opener.document.getElementById('ctl00_btnCntr').click();
            }
            else {
                window.opener.document.getElementById('ctl00_btnMrkt').click();
            }

            window.close();
            return false;
        }
		
		function ErrorWindow(Page) {
            window.opener.location.href = Page;
            window.close();
        }
		
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table width="540px" style="text-align: center;" cellpadding="2" cellspacing="4">
            <tr>
                <td>
                    <asp:Image ID="SavvyLogo" runat="server" ImageUrl="~/Images/SavvyPack.png" Height="50px" />
                </td>
            </tr>
        </table>
        <table width="540px" class="ServicrList" style="text-align: center;" cellpadding="2"
            cellspacing="4">
            <tr>
                <td style="width: 540px;">
                    <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: center;
                        height: 23px; width: 540px; overflow: auto;">
                        <asp:Label ID="Label3" runat="server" Style="text-align: left; margin-left: 5px;"
                            Font-Size="18px" Text="Market Subscription"></asp:Label>
                    </div>
                    <asp:Table ID="tblMrkt" runat="server" Width="540px" Style="height: 50px; overflow: auto;">
                    </asp:Table>
                </td>
            </tr>
            <tr>
                <td style="width: 540px; overflow: auto;">
                    <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: center;
                        height: 23px; width: 540px; overflow: auto;">
                        <asp:Label ID="Label1" runat="server" Style="text-align: left; margin-left: 5px;"
                            Font-Size="18px" Text="Interactive Knowledgebases"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkCont" runat="server" Text="CONTRACT PACKAGER KNOWLEDGEBASE"
                        CssClass="nonService" Enabled="false"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidId" runat="server" />
    </form>
</body>
</html>
