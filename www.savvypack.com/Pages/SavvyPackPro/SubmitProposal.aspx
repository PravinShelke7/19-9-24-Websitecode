<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubmitProposal.aspx.vb"
    Inherits="Pages_SavvyPackPro_SubmitProposal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Submit Proposal</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function ShowPopUpRFP(Page) {

            var width = 980;
            var height = 420;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'SelType', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ClosePage() {
            window.opener.document.getElementById('btnRefresh').click();
        }


        function RefreshPage() {
            window.location.reload();
        }




    </script>

     <style type="text/css">
        tr.row {
            background-color: #fff;
        }

            tr.row td {
            }

            tr.row:hover td, tr.row.over td {
                background-color: #eee;
            }

        .breakword {
            word-wrap: break-word;
            word-break: break-all;
        }

        a.SavvyLink:link {
            font-family: Verdana;
            color: black;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:visited {
            font-family: Verdana;
            color: Black;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:hover {
            font-family: Verdana;
            color: Red;
            font-size: 11px;
        }

        .SingleLineTextBox {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 14px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }

        .AlternateColorAct1 {
            font-family: Verdana;
            background-color: #dfe8ed;
        }

        .MultiLineTextBoxG {
            font-family: Verdana;
            font-size: 10px;
            width: 320px;
            height: 50px;
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

        .SingleLineTextBox_G {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 15px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }

        .divUpdateprogress_SavvyPro {
            left: 410px;
            top: 400px;
            position: absolute;
        }        
    </style>

</head>
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
                                                            CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectType_Vendor.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <asp:Panel ID="RfpDetail" runat="server" Visible="false">
                                            <table style="margin-left: 10px;">
                                                <tr>
                                                    <td>
                                                        <b>Type: </b>
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
                                                    <td style="padding-left: 120px;">
                                                        <b>Buyer:</b>
                                                          <asp:Label ID="lblbuyer" runat="server"></asp:Label>
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
            <table style="width: 1100px;">
                <tr class="AlterNateColor2">
                    <td style="padding-left: 10px; padding-top: 15px;">
                        <ajaxToolkit:TabContainer ID="tabISumbitP" Height="450px" Width="1050px" runat="server"
                            ActiveTabIndex="0" AutoPostBack="true" Enabled="false">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Submit Proposal" ToolTip="Submit"
                                ID="tabValidate">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlValidate" runat="server" Width="919px">
                                        <table id="tblValidate" runat="server" style="margin-top: 15px;" class="ContentPage">
                                            <tr id="Tr2" runat="server">
                                                <td id="Td2" style="padding-top: 10px;" runat="server">
                                                    <div id="ContentPagemargin" class="ContentPage" style="overflow: auto; height: 291px;
                                                        width: 880px;" runat="server">
                                                        <asp:Label ID="lblNOVendor" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                            Text="No record found."></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblSP" runat="server" CssClass="CalculatedFeilds" Text="Submit Proposal to:"
                                                            Height="30px"></asp:Label>
                                                        <asp:Label ID="lblSubmitP" runat="server" CssClass="CalculatedFeilds"></asp:Label>
                                                        <br />
                                                        <asp:Label ID="lblDD" runat="server" CssClass="CalculatedFeilds" Text="Due Date:"
                                                            Height="30px"></asp:Label>
                                                        <asp:Label ID="lblDueD" runat="server" CssClass="CalculatedFeilds"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" runat="server">
                                                <td id="Td1" style="padding-left: 10px;" runat="server">
                                                    <asp:Button ID="Button4" runat="server" Text="Validate" Visible ="false"  CssClass="ButtonWMarigin" />
                                                </td>
                                            </tr>
                                            <tr id="Tr3" runat="server">
                                                <td id="Td3" style="padding-left: 10px;" runat="server">
                                                    <asp:Button ID="BtnAccept" runat="server" Text="Submit" CssClass="ButtonWMarigin" />
                                                    <asp:Button ID="BtnDecline" runat="server" Text="Decline" CssClass="ButtonWMarigin" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
                <tr id="Tr11" class="AlterNateColor3" runat="server">
                    <td id="Td11" class="PageSHeading" align="center" runat="server">
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
            <asp:HiddenField ID="hidRSOwnerEmailID" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
    <asp:Button ID="btnRefreshVList" runat="server" Style="display: none;" />
    </form>
</body>
</html>
