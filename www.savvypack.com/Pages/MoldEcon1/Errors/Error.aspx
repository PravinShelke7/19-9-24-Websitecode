﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="Pages_MoldEcon1_Errors_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Error</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        javascript: window.history.forward(1); 
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
            <table class="E1MoldModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                    </td>
                </tr>
            </table>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
            <tr style="height: 20px">
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Econ1 Mold-Tools')"
                        onmouseout="UnTip()" style="width: 840px;">
                         Econ1 Mold - Error
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left">
                            <div class="ErrorDiv" style="width: 100%">
                                <div id="divUpdate" runat="server" style="margin-bottom: 20px;">
                                    Please <b>
                                        <asp:HyperLink ID="hypPage" runat="server" Text="Click Here" CssClass="Link" Font-Size="16px"></asp:HyperLink></b>&nbsp;to
                                    go input page.
                                </div>
                                <table>
                                    <tr>
                                        <td>
                                            <b>Error Code:-</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <b>Error Message:-</b>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="error">
                                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
