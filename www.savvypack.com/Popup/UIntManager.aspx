<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UIntManager.aspx.vb" Inherits="Popup_UIntManager" %>

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
            font-weight: bold;
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
        <div style="height: 350px; overflow: auto;">
            <table width="540px" class="ServicrList" style="text-align: center;" cellpadding="2"
                cellspacing="4">
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEcon1" runat="server" Text="Econ1 - Package Manufacturing"
                            CssClass="nonService" Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkCompE1" runat="server" Text="Econ1-Package Manufacturing-Competitive Module"
                            CssClass="nonService" Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEcon2" runat="server" Text="Econ2 - Product Packaging" CssClass="nonService"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEcon3" runat="server" Text="Econ3 - Package Manufacturing"
                            CssClass="nonService" Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEcon4" runat="server" CssClass="nonService" Text="Econ4 - Product Packaging"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
             
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSustn1" runat="server" Text="Sustain1 - Package Manufacturing"
                            CssClass="nonService" Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkCompS1" runat="server" Text="Sustain1-Package Manufacturing-Competitive Module"
                            CssClass="nonService" Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSustn2" runat="server" Text="Sustain2 - Product Packaging"
                            CssClass="nonService" Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSustn3" runat="server" CssClass="nonService" Text="Sustain3 - Package Manufacturing"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSustn4" runat="server" CssClass="nonService" Text="Sustain4 - Product Packaging"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSA" runat="server" CssClass="nonService" Text="STRUCTURE ASSISTANT"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEchem" runat="server" CssClass="nonService" Text="EChem1- Chemical Manufacturing"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSchem" runat="server" CssClass="nonService" Text="SChem1- Chemical Manufacturing"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEdist" runat="server" CssClass="nonService" Text="EDistribution - Packaging User Perspective"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSdist" runat="server" CssClass="nonService" Text="SDistribution - Packaging User Perspective"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                    <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkMedEcon1" runat="server" CssClass="nonService" Text="Med1"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkMedEcon2" runat="server" CssClass="nonService" Text="Med2"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSMed1" runat="server" CssClass="nonService" Text="SMed1"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkSMed2" runat="server" CssClass="nonService" Text="SMed2"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkRetail" runat="server" CssClass="nonService" Text="Retail - Packaging User Perspective"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                
                 <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkMOLDE1" runat="server" CssClass="nonService" Text="Econ1 - Mold"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkMOLDE2" runat="server" CssClass="nonService" Text="Econ2 - Mold"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkMOLDS1" runat="server" CssClass="nonService" Text="Sustain1 - Mold"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkMOLDS2" runat="server" CssClass="nonService" Text="Sustain2 - Mold"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>

                  <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkEMonitor" runat="server" CssClass="nonService" Text="Emonitor"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                        <asp:LinkButton ID="lnkIContract" runat="server" CssClass="nonService" Text="iContract"
                            Enabled="false"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:HiddenField ID="hidId" runat="server" />
    </form>
</body>
</html>
