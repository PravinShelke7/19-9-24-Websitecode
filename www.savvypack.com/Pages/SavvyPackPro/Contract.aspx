<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contract.aspx.vb" Inherits="Pages_SavvyPackPro_Contract" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <title></title>
    <style type="text/css">
        .divUpdateprogress
        {
            left: 410px;
            top: 200px;
            position: absolute;
        }
    </style>
    <script type="text/JavaScript">
        function CheckSP(text) {

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }
    </script>
</head>
<%--<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <asp:Image ImageAlign="AbsMiddle" Width="1100px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
    <div>
        <div id="ContentPagemargin" runat="server">
            <div id="PageSection1" style="text-align: left;">
                <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                    <img id="loading-image" src="../../Images/Loading2.gif" alt="Loading..." width="100px"
                        height="100px" />
                </div>
                <b>Enter File name :</b>
                <asp:TextBox ID="txtName" CssClass="MediumTextBox" onchange="javascript:CheckSP(this);"
                    runat="server"></asp:TextBox><b>.PDF</b>
                <asp:Button ID="btnExport" runat="server" Text="Upload File" style=" margin-left :50px;"/>
                <br />
                <asp:Table ID="tblComparision" Width="2000px" runat="server" CellPadding="0" CellSpacing="2">
                </asp:Table>
            </div>
        </div>
    </div>
    </form>
</body>--%>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <asp:Image ImageAlign="AbsMiddle" Width="1100px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="divUpdateprogress_SavvyPro">
                        <table>
                            <tr>
                                <td>
                                    <img alt="" src="../../Images/Loading4.gif" height="50px" />
                                </td>
                                <td>
                                    <b style="color: Red;">Updating the Record</b>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <table style="width: 1100px; font-family: Verdana; font-size: 14px;">
                <tr valign="top" style="background-color: #dfe8ed;">
                    <td>
                        <asp:Panel ID="pnlRFPMng" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 26%;">
                                        <div id="divContact" style="margin-left: 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <b>Type Selector:</b>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="lnkSelRFP" runat="server" Text="Nothing Selected" Font-Bold="true"
                                                            CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectRFP.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>
                                                    </td>
                                                    <td  style="padding-left: 50px;">
                                                        <b>Vendor Selector:</b>
                                                    </td>
                                                    <td>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" Text="Nothing Selected" Font-Bold="true"
                                                            CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectRFP.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Panel ID="RfpDetail" runat="server" Visible="false">
                                            <table style="margin-left: 10px;">
                                                <tr>
                                                    <td>
                                                        <b>Type:</b>
                                                        <asp:Label ID="lblType" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 120px;">
                                                        <b>Number: </b>
                                                        <asp:Label ID="lblSelRfpID" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 120px;">
                                                        <b>Description:</b>
                                                        <asp:Label ID="lblSelRfpDes" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <table style="width: 1110px;">
                <tr class="AlterNateColor2">
                    <td style="padding-left: 10px; padding-top: 15px;">
                        <ajaxToolkit:TabContainer ID="tabContract" Height="550px" Width="1090px" runat="server"
                            ActiveTabIndex="0" AutoPostBack="true" Enabled="false">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Contract" ToolTip="Contract" ID="pnlContract">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlValidate" runat="server">
                                        <div id="PageSection1" style="text-align: left;">
                                            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                                                <img id="loading-image" src="../../Images/Loading2.gif" alt="Loading..." width="100px"
                                                    height="100px" />
                                            </div>
                                            <b>Enter File name :</b>
                                            <asp:TextBox ID="txtName" CssClass="MediumTextBox" onchange="javascript:CheckSP(this);"
                                                runat="server"></asp:TextBox><b>.PDF</b>
                                            <asp:Button ID="btnExport" runat="server" Text="Upload File" Style="margin-left: 50px;" />
                                            <br />
                                            <asp:Table ID="tblComparision" Width="2000px" runat="server" CellPadding="0" CellSpacing="2">
                                            </asp:Table>
                                        </div>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
                <tr id="Tr5" class="AlterNateColor3" runat="server">
                    <td id="Td5" class="PageSHeading" align="center" runat="server">
                        <asp:Label ID="lblFooter" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
            <asp:HiddenField ID="hidRfpID" runat="server" />
            <asp:HiddenField ID="hidRfpNm" runat="server" />
            <asp:Button ID="btnrefreshT" runat="server" Style="display: none;" />
            <asp:Button ID="btnrefresh" runat="server" Style="display: none;" />
            <asp:HiddenField ID="hidSurveyId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
    <asp:Button ID="btnRefreshVList" runat="server" Style="display: none;" />
    </form>
</body>
</html>
