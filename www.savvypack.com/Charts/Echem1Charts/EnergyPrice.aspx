<%@ Page Language="VB"AutoEventWireup="false" CodeFile="EnergyPrice.aspx.vb" Inherits="Charts_Echem1Charts_EnergyPrice" 
title="Untitled Page" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Echem1-Energy Price Chart</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="stylesheet" href="../../App_Themes/SkinFile/AlliedNew.css"  />

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
    <form runat="server" id="form1">
        <div id="MasterContent">
            <div id="AlliedLogo">
                <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                    runat="server" />
            </div>
            <div>
                <table class="Echem1Module" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
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
                        <b>Energy Price Chart For: </b>
                        <asp:Label ID="mat" runat="server"></asp:Label>
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
                                    <div id="MaterialPrice" runat="server">
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <table style="background-color:Black;width:100%; color: White;" border="0" cellpadding="5" cellspacing="5">
                        <tr style="height: 50px;">
                            <td style="height: 22px;color:White;margin-left:100px;width:30%;font-size:13px;font-family:Verdana" align="right" >
                                <b>Energy Selection:</b></td>
                            <td style="width:30%" >
                                &nbsp;<asp:DropDownList ID="MPC" CssClass="NormalDropDown" Width="250px" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td align="center" style="width:40%" rowspan="2">
                                <input type="submit" value="Submit" /></td>
                        </tr>
                        <tr>
                            <td style="height: 22px;color: White;font-size:13px;font-family:Verdana" align="right">
                                <b>Period:</b></td>
                            <td>
                                <table cellpadding="0" cellspacing="0" border="0">
                                    <tr>
                                        <td>
                                            <asp:RadioButton Checked="false" GroupName="year" ID="a" AutoPostBack="true" runat="server"
                                                Text="1 year" ForeColor="White" />
                                        </td>
                                        <td>
                                            <asp:RadioButton Checked="false" GroupName="year" ID="b" AutoPostBack="true" runat="server"
                                                Text="3 Year" ForeColor="White" />
                                        </td>
                                        <td>
                                            <asp:RadioButton Checked="false" GroupName="year" ID="c" AutoPostBack="true" runat="server"
                                                Text="5 Year" ForeColor="White" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td align="right">
                            </td>
                        </tr>
                        <tr style="height:10px">
                            <td style="height: 22px;color: White;">
                                <b>
                                    <%--Energy Comparision:--%>
                                </b>
                            </td>
                            <td valign="middle">
                                &nbsp;<asp:DropDownList ID="MPC2" runat="server" Visible="false">
                                    <asp:ListItem Text="" Value="" Enabled="true"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                            </td>
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



