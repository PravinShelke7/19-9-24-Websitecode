<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EquipmentAssistant.aspx.vb"
    Inherits="Pages_Econ1_EqAssistantInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Operating Parameter Assistant</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <style type="text/css">
        .MatSmallTextBox {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 150px;
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

        .tbl {
            width: 700px;
            margin-top: 20px;
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
                    <td style="padding-left: 410px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                        runat="server" ToolTip="Update" Visible="true" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif"
                                        runat="server" ToolTip="Log Off" Visible="false" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=E1" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                        runat="server" ToolTip="Instructions" Visible="false" OnClientClick="return Help();" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgChart" ImageAlign="Middle" ImageUrl="~/Images/ChartN.gif"
                                        runat="server" ToolTip="Charts" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgFeedback" ImageAlign="Middle" ImageUrl="~/Images/FeedbackN.gif"
                                        runat="server" ToolTip="Feedback" OnClientClick="return FeedBack();" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                        runat="server" ToolTip="Notes" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgWizard" ImageAlign="Middle" ImageUrl="~/Images/Wizard.gif"
                                        runat="server" ToolTip="Wizard" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgCompare" ImageAlign="Middle" ImageUrl="~/Images/CompareN.gif"
                                        runat="server" ToolTip="Comparison" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgInkCost" ImageAlign="Middle" ImageUrl="~/Images/AssistantButtonn.png"
                                        runat="server" ToolTip="Comparison" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgBarProp" ImageAlign="Middle" ImageUrl="~/Images/AssistantButtonn.png"
                                        runat="server" ToolTip="Comparison" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="ContentPagemargin" style="width: 1500px; margin: 0 0 0 0;">
            <div id="PageSection1">
                <div id="divMainHeading" style="text-align: left; font-weight: bold; font-size: large;">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                    <b style="padding-left: 300px;">Effective Date:</b>
                    <asp:DropDownList ID="ddlDate" runat="server" AutoPostBack="true"></asp:DropDownList>
                    <div style="margin-left: 20px; padding-top: 10px; font-size: medium;">
                        <asp:Label ID="lblEquipment" Text="Equipment Description:" runat="server" Style="width: 500px; padding-left: 100px;"></asp:Label>
                        <asp:Label ID="lblEquipDesc" runat="server" Style="font-weight: normal;"></asp:Label>
                        <asp:Label ID="lblPercTitle" runat="server" Text="Results : " Style="margin-left: 350px;"></asp:Label>
                        <asp:Label runat="server" Text="Waste and Downtime for Operating Parameter Screen" Style="font-weight: normal;"></asp:Label>
                    </div>
                    <%--<div id="div3" runat="server" style="width: 500px; font-size: 15px; font-family: Verdana">
                </div>--%>
                </div>
                </br >
            <div id="divInfo" runat="server">
                <asp:Label ID="lblInfo" runat="server"></asp:Label>
            </div>
                <div id="div1" style="margin-left: 20px;">
                    <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                    <asp:Table ID="tblHead" runat="server">
                    </asp:Table>
                    <div style="overflow: auto; display: flex; justify-content: space-between;">
                        <div style="float: left; width: 750px;">
                            <div style="text-align: center; font-size: 15px; display: none;">
                                <b>Charts Waste</b>
                                <br />
                                <b>Charts Downtime</b>
                            </div>
                            <asp:Table ID="tblJSeq" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblData" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblRunP" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblRunR" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblRunW" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblRegT" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblMainT" runat="server" CssClass="tbl">
                            </asp:Table>
                        </div>
                        <div style="float: right; width: 750px;">
                            <div style="text-align: center; font-size: 15px; border: 1px solid;">
                                <table style="text-align: left">
                                    <tr>
                                        <td><b>Waste Rate (%) :</b></td>
                                        <td>
                                            <asp:Label ID="lblWastePerc" runat="server"></asp:Label></td>
                                        <td style="font-size: 11px;"><b>
                                            <asp:Label ID="lblWasteForm" runat="server"></asp:Label></b></td>
                                    </tr>
                                    <tr>
                                        <td><b>Downtime (%) :</b></td>
                                        <td>
                                            <asp:Label ID="lblDowntimePerc" runat="server"></asp:Label></td>
                                        <td style="font-size: 11px;"><b>
                                            <asp:Label ID="lblDownTimeForm" runat="server"></asp:Label></b></td>
                                    </tr>
                                </table>
                            </div>
                            <asp:Table ID="tblJSeqR" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblMatRes" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblSSum" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblTSum" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblTSum2" runat="server" CssClass="tbl">
                            </asp:Table>
                            <asp:Table ID="tblMSum" runat="server" CssClass="tbl">
                            </asp:Table>
                        </div>
                    </div>

                    <table>
                        <tr>
                            <td rowspan="1">

                                <div style="margin-top: 20px;">
                                </div>
                                <div style="margin-top: 20px;">
                                </div>
                                <div style="margin-top: 20px;">
                                </div>
                                <div style="margin-top: 20px;">
                                </div>
                                <div style="margin-top: 20px;">
                                </div>
                            </td>
                            <td rowspan="1">
                                <div style="margin-top: -400px; padding-left: 150px;">
                                </div>
                                <div style="margin-top: 20px; padding-left: 150px;">
                                </div>
                                <div style="margin-top: 20px; padding-left: 150px;">
                                </div>
                                <div style="margin-top: 20px; padding-left: 150px;">
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="2">
                                <div style="padding-top: 0px;">
                                    <asp:Table ID="tblJSeq1" runat="server">
                                    </asp:Table>
                                </div>
                            </td>
                            <td>
                                <div style="margin-top: 20px; padding-left: 100px;">
                                </div>
                                <div style="margin-top: 20px; padding-left: 100px;">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                <br />
            </div>
            <asp:HiddenField ID="hidEqId" runat="server" />
            <asp:HiddenField ID="hidPWidth" runat="server" />
            <asp:HiddenField ID="hidHead" runat="server" />
            <asp:HiddenField ID="hidHdCol" runat="server" />
            <asp:HiddenField ID="hidHdCyl" runat="server" />
            <asp:HiddenField ID="hidRPProc" runat="server" />
            <asp:HiddenField ID="hidRPReg" runat="server" />
            <asp:HiddenField ID="hidRWProc" runat="server" />
            <asp:HiddenField ID="hidRWReg" runat="server" />
            <asp:HiddenField ID="hidRTProc" runat="server" />
            <asp:HiddenField ID="hidRTReg" runat="server" />
            <asp:HiddenField ID="hidRowNum" runat="server" />
            <asp:HiddenField ID="hidJRes" runat="server" />
            <asp:HiddenField ID="hidSelHead" runat="server" />
            <asp:HiddenField ID="hidSelHdCol" runat="server" />
            <asp:HiddenField ID="hidSelHdCyl" runat="server" />
            <asp:HiddenField ID="hidSelRPProc" runat="server" />
            <asp:HiddenField ID="hidSelRPReg" runat="server" />
            <asp:HiddenField ID="hidSelRWProc" runat="server" />
            <asp:HiddenField ID="hidSelRWReg" runat="server" />
            <asp:HiddenField ID="hidSelRTProc" runat="server" />
            <asp:HiddenField ID="hidSelRTReg" runat="server" />
            <asp:HiddenField ID="hidSelJRes" runat="server" />
            <asp:HiddenField ID="hidLayerId" runat="server" />
        </div>
    </form>
</body>
</html>
