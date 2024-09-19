<%@ Page Language="vb" AutoEventWireup="false" CodeFile="Chart.aspx.vb" Inherits="MaterialPrice" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Material Chart </title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="stylesheet" href="../../App_Themes/SkinFile/AlliedNew.css" />
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
        .E1CompModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../Images/Econ1Fulcrum.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        .SBAModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../Images/SavvyPackStructureAssistantR5.gif');
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        .ContentPage
        {
            margin-top: 2px;
            background-color: #F1F1F2;
        }
        #PageSection1
        {
            background-color: #D3E7CB;
            margin-left: 0px;
        }
    </style>
</head>
<body style="margin-top: 0px; margin-left: 0px;">
    <form runat="server" id="form1" action="chart.aspx">
    <%--<div id="MasterContent">
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>
        <div>
            <table class="SBAModule" id="SBATable" runat="server" cellpadding="0" cellspacing="0"
                style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 380px">
                        <asp:HyperLink ID="hypPref" runat="server" NavigateUrl="~/Charts/ChartPreferences/ChartPreferences.aspx"
                            Text="Preferences" CssClass="Link" Target="_blank" Visible="false"></asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>--%>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div id="divMainHeading" runat="server" style="width: 640px; font-size: 11px; font-family: Verdana;
                    text-align: center; margin-top: 3px;">
                    <b>Chart For: </b>
                    <asp:Label ID="mat" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr style="width: 100%; font-size: 13px; font-family: Verdana; height: 5px;">
            <td align="center">
                <asp:Label ID="lblyear" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <div id="PageSection1" style="text-align: center;">
                    <table>
                        <tr>
                            <td>
                                <div id="MaterialPrice" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
