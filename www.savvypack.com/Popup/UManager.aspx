<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UManager.aspx.vb" Inherits="Popup_UManager" %>

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
            font-size: small;
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
    </style>
    <script type="text/javascript">
        function ClosePop(serv) {
            var hidTid = document.getElementById('<%= hidId.ClientId %>');
            var ServId = document.getElementById(serv).getAttribute("CommandArgument");
            window.opener.document.getElementById('ctl00_hidTId').value = ServId;
            window.opener.document.getElementById('ctl00_btnServ').click();
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
                <td style="width: 845px; background-color: #edf0f4;">
                    <asp:Image ID="SavvyLogo" runat="server" ImageUrl="~/Images/SavvyPack.png" Height="50px" />
                </td>
            </tr>
        </table>
        <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: center;
            height: 23px; width: 540px; overflow: auto;">
            <asp:Label ID="Label3" runat="server" Style="text-align: left; margin-left: 5px;"
                Font-Size="18px" Text="The modules you are authorized to use are shown below:"></asp:Label>
        </div>
        <table width="540px" class="ServicrList" style="text-align: center;" cellpadding="2"
            cellspacing="4">
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkEcon1" runat="server" Text="ECON 1" CssClass="nonService"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkEcon2" runat="server" Text="ECON 2" CssClass="nonService"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkEcon3" runat="server" Text="ECON 3" CssClass="nonService"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkEcon4" runat="server" Text="ECON 4" CssClass="nonService"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkSustn1" runat="server" Text="SUSTAIN 1" CssClass="nonService"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkSustn2" runat="server" Text="SUSTAIN 2" CssClass="nonService"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkSustn3" runat="server" CssClass="nonService" Text="SUSTAIN 3"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkSustn4" runat="server" CssClass="nonService" Text="SUSTAIN 4"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:LinkButton ID="lnkSA" runat="server" CssClass="nonService" Text="STRUCTURE ASSISTANT"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidId" runat="server" />
    </form>
</body>
</html>
