<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CResultsplDep2.aspx.vb"
    Inherits="Pages_MoldEcon1_Results_CResultsplDep2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1 Mold-Profit and Loss Comparison With Depreciation</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
    <style type="text/css">
        .E1MoldModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../../Images/E1MoldHeader.gif');
            height: 44px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
       <div id="AlliedLogo">
            <table>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 845px; background-color: #edf0f4;">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>   
        <div>
            <table class="E1MoldModule" id="E1Table" runat="server" cellpadding="0" cellspacing="0"
                style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                        Econ1 Mold - Customer Profit and Loss Comparison With Depreciation
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
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
