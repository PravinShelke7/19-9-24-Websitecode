<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WebConfAtte.aspx.vb" Title="WebConference"
    Inherits="WebConferenceN_WebConfAtte" MasterPageFile="~/Masters/AlliedMasterMenu.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="JavaScript" src="https://seals.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>
    <script type="text/JavaScript">
        function ShowEditPopup(hid) {
            var ShipToID = document.getElementById('ctl00_ContentPlaceHolder1_' + hid).value
            var width = 500;
            var height = 430;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var Page = "../Users_Login/UserInfoEdit.aspx?ShipToId=" + ShipToID + "";
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
    </script>
    <div class="MnContentPage">
        <center>
            <%--<div class="PageHeading">
                <asp:Label ID="lblMainHeader" runat="server"></asp:Label>
            </div>--%>
        </center>
        <div style="font-family: Optima; font-size: 12px">
            <br />
            <asp:Label ID="lblSubHeader" runat="server"></asp:Label><br />
            <br />
        </div>
        <table style="width: 580px; text-align: left" cellpadding="2" cellspacing="2">
            <tr class="AlterNateColor4">
                <td align="center" class="TdHeading" colspan="3">
                    <b>Conference Details</b>
                </td>
            </tr>
            <tr class="AlterNateColor1" style="text-align: center">
                <td class="WebInnerTd">
                    <b>Conference Date</b>
                </td>
                <td class="WebInnerTd">
                    <b>Conference Time</b>
                </td>
                <td class="WebInnerTd">
                    <b>Conference Topic</b>
                </td>
            </tr>
            <tr class="AlterNateColor2" style="text-align: Left">
                <td class="WebInnerTd">
                    <asp:Label ID="lblCDate" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
                <td class="WebInnerTd">
                    <asp:Label ID="lblCTime" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
                <td class="WebInnerTd">
                    <asp:Label ID="lblCTopic" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 580px; text-align: left" cellpadding="2" cellspacing="2">
            <tr class="AlterNateColor4">
                <td align="center" class="TdHeading" colspan="4">
                    <b>Conference Credentials</b>
                </td>
            </tr>
            <tr class="AlterNateColor1" style="text-align: center">
                <td class="WebInnerTd">
                    <b>
                        <asp:Label ID="lblUNameText" runat="server" CssClass="NormalLabel"></asp:Label></b>
                </td>
                <td class="WebInnerTd">
                    <b>
                        <asp:Label ID="lblPWDtext" runat="server" CssClass="NormalLabel"></asp:Label></b>
                </td>
                <td class="WebInnerTd">
                    <b>Conference Phone No.</b>
                </td>
                <td class="WebInnerTd">
                    <b>Conference Access Code</b>
                </td>
            </tr>
            <tr class="AlterNateColor2" style="text-align: Left">
                <td class="WebInnerTd" style="width: 20%">
                    <asp:Label ID="lblUName" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
                <td class="WebInnerTd" style="width: 20%">
                    <asp:Label ID="lblUPWD" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
                <td class="WebInnerTd" style="width: 30%">
                    <asp:Label ID="lblPhn" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
                <td class="WebInnerTd" style="width: 30%">
                    <asp:Label ID="lblCCode" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <table style="width: 580px; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td style="width: 80%">
                    <table cellpadding="0" cellspacing="2" style="width: 100%">
                        <tr class="AlterNateColor4">
                            <td align="center" class="TdHeading" colspan="2">
                                <b>User Information</b>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Company Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCompName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trAt1" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 1:
                                            <asp:LinkButton ID="lnkShipTo1" Visible="false" OnClientClick="return ShowEditPopup('hdnShp1')"
                                                runat="server" Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt2" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 2:
                                            <asp:LinkButton ID="lnkShipTo2" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp2')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt3" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 3:
                                            <asp:LinkButton ID="lnkShipTo3" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp3')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt4" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 4:
                                            <asp:LinkButton ID="lnkShipTo4" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp4')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt5" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 5:
                                            <asp:LinkButton ID="lnkShipTo5" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp5')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt6" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 6:
                                            <asp:LinkButton ID="lnkShipTo6" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp6')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt7" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 7:
                                            <asp:LinkButton ID="lnkShipTo7" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp7')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt8" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 8:
                                            <asp:LinkButton ID="lnkShipTo8" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp8')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt9" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 9:
                                            <asp:LinkButton ID="lnkShipTo9" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp9')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="trAt10" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 10:
                                            <asp:LinkButton ID="lnkShipTo10" runat="server" Visible="false" OnClientClick="return ShowEditPopup('hdnShp10')"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttUser10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Email:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttEmail10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Country:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCntry10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                            </td>
                            <td align="left" class="WebInnerTd">
                                <%-- <asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Charge Your Card" />--%>
                                <asp:Button ID="btnOrderClose" runat="server" CssClass="ButtonWMarigin" Text="Done Registering"
                                    OnClientClick="javascript:window.close();" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20%">
                    <%-- <script type="text/javascript">                        SiteSeal("https://seal.networksolutions.com/images/colorsealbasic.gif", "NETSB", "none");</script>--%>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnShp1" runat="server" />
        <asp:HiddenField ID="hdnShp2" runat="server" />
        <asp:HiddenField ID="hdnShp3" runat="server" />
        <asp:HiddenField ID="hdnShp4" runat="server" />
        <asp:HiddenField ID="hdnShp5" runat="server" />
        <asp:HiddenField ID="hdnShp6" runat="server" />
        <asp:HiddenField ID="hdnShp7" runat="server" />
        <asp:HiddenField ID="hdnShp8" runat="server" />
        <asp:HiddenField ID="hdnShp9" runat="server" />
        <asp:HiddenField ID="hdnShp10" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </div>
</asp:Content>
