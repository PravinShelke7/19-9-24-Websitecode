<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SalaryPrice.aspx.vb" Inherits="Charts_Default" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Salary Plus Benefits Chart</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="stylesheet" href="../../App_Themes/SkinFile/AlliedNew.css" />
 <style type="text/css">
        .E1CompModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../../Images/Econ1Fulcrum.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-16991293-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + 
'.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

    </script>

</head>
<body style="margin-top: 0px;">
    <form runat="server" id="form1" action="MaterialPrice.aspx">
        <div id="MasterContent">
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
            <div>
                <table class="E1MoldModule" id="E1Table" runat="server"  cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 380px">
                            <asp:HyperLink ID="hypPref" runat="server" NavigateUrl="~/Charts/ChartPreferences/ChartPreferences.aspx"
                                Text="Preferences" CssClass="Link" Target="_blank"></asp:HyperLink>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div id="divMainHeading" runat="server" style="width: 840px; font-size: 15px; font-family: Verdana">
                        <b>Salary Plus Benefits Chart For Postion: </b>
                        <asp:Label ID="mat" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="div1" runat="server" style="width: 840px; font-size: 15px; font-family: Verdana">
                        <b>Salary Plus Benefits Chart For Country: </b>
                        <asp:Label ID="Country" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="width: 100%; font-size: 13px; font-family: Verdana; height: 30px;">
                <td align="center">
                    <asp:Label ID="lblyear" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="PageSection1" style="text-align: center">
                        <table>
                            <tr>
                                <td>
                                    <div id="SalaryPrice" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <table style="font-family:Verdana;font-size:13px;background-color:Black;width:100% " border="0" cellpadding="3" cellspacing="3" style="width: 672px">
                        <tr style="height: 50px">
                            <td style="height: 50px; width:35%; color: White;font-size:13px;font-family:Verdana" align="right">
                                <b>Position and Country Selection:</b></td>
                            <td style="width:30%" valign="middle">
                                <asp:DropDownList ID="SPC1" runat="server" CssClass="NormalDropDown" Width="224px">
                                </asp:DropDownList>
                            </td>
                            <td align="left" style="width:35%" valign="middle">
                                <asp:DropDownList ID="CountryDropDown1" runat="server" CssClass="NormalDropDown" Width="150px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr style="height: 50px">
                            <td style="height: 50px; color: White;font-size:13px;font-family:Verdana" align="right">
                                <b>Position and Country Comparision:</b></td>
                            <td valign="middle">
                                <asp:DropDownList ID="SPC2" CssClass="NormalDropDown" Width="224px" runat="server">
                                </asp:DropDownList></td>
                            <td align="left" valign="middle">
                                <asp:DropDownList ID="CountryDropDown2" CssClass="NormalDropDown"  runat="server" Width="150px">
                                </asp:DropDownList>.
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 22px;color: White;font-size:13px;font-family:Verdana" align="right">
                                <b>Period:</b></td>
                            <td >
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td style="height: 20px">
                                            <asp:RadioButton Checked="false" GroupName="year" ID="a" AutoPostBack="true" runat="server"
                                                Text="1 year" ForeColor="White" />
                                        </td>
                                        <td style="height: 20px">
                                            <asp:RadioButton Checked="false" GroupName="year" ID="b" AutoPostBack="true" runat="server"
                                                Text="3 Year" ForeColor="White" />
                                        </td>
                                        <td style="height: 20px">
                                            <asp:RadioButton Checked="false" GroupName="year" ID="c" AutoPostBack="true" runat="server"
                                                Text="5 Year" ForeColor="White" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="left">
                                <input type="submit" value="Submit" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
         <input  name="Chart" type="hidden" value="1"  />
    </form>
</body>
</html>
