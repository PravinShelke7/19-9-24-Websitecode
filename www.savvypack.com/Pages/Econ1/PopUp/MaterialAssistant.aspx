<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MaterialAssistant.aspx.vb"
    Inherits="Pages_Econ1_MaterialAssistant" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Material Price Assistant</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <style type="text/css">
        .MatSmallTextBox {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 50px;
            background-color: #FEFCA1;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="AlliedLogo">
            <%-- <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />--%>
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
            <table class="E1Module" id="E1table" runat="server" cellpadding="0" cellspacing="0"
                style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px"></td>
                </tr>
            </table>
        </div>
        <div id="ContentPagemargin" style="width: 90%; margin: 0 0 0 0;">
            <div id="PageSection1" style="text-align: left">

                <div id="divMainHeading" style="text-align: center; font-weight: bold; font-size: large;">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                    <b style="margin-left: 150px; font-size: medium;">Price Book Effective Date:</b>
                    <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="true"></asp:DropDownList>
                </div>
                </br>
                
                <div id="divInfo" runat="server">
                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                </div>
                <div id="div1" style="text-align: center; margin-left: 20px;">
                    <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                    <asp:Table ID="tblHead" runat="server"></asp:Table>
                    <table>
                        <tr>
                            <td rowspan="9">
                                <asp:Table ID="tblData" runat="server"></asp:Table>
                            </td>
                            <td>
                                <table style="margin-top: -1350px;">
                                    <tr>
                                        <td>
                                            <asp:Table ID="tblCoat" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblOrder" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblCoil" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblQual" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblCoilWd" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblGen" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblTrimTol" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblPack" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                    <tr style="margin-top: 0px;">
                                        <td>
                                            <asp:Table ID="tblPackaging" runat="server"></asp:Table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
            <asp:HiddenField ID="hidMatId" runat="server" />
            <asp:HiddenField ID="hidBox" runat="server" />
            <asp:HiddenField ID="hidCoat" runat="server" />
            <asp:HiddenField ID="hidOrder" runat="server" />
            <asp:HiddenField ID="hidCoilWt" runat="server" />
            <asp:HiddenField ID="hidQual" runat="server" />
            <asp:HiddenField ID="hidCoilWd" runat="server" />
            <asp:HiddenField ID="hidGen" runat="server" />
            <asp:HiddenField ID="hidTrim" runat="server" />
            <asp:HiddenField ID="hidPack" runat="server" />
            <asp:HiddenField ID="hidPackg" runat="server" />
            <asp:HiddenField ID="hidSelBox" runat="server" />
            <asp:HiddenField ID="hidSelCoat" runat="server" />
            <asp:HiddenField ID="hidSelOrder" runat="server" />
            <asp:HiddenField ID="hidSelCoilWt" runat="server" />
            <asp:HiddenField ID="hidSelQual" runat="server" />
            <asp:HiddenField ID="hidSelCoilWd" runat="server" />
            <asp:HiddenField ID="hidSelGen" runat="server" />
            <asp:HiddenField ID="hidSelTrim" runat="server" />
            <asp:HiddenField ID="hidSelPack" runat="server" />
            <asp:HiddenField ID="hidSelPackg" runat="server" />
            <asp:HiddenField ID="hidValId" runat="server" />
        </div>
    </form>
</body>
</html>
