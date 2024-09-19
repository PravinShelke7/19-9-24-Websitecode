<%@ Page Title="Web Conference - Confirm Order" Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master"
    AutoEventWireup="false" CodeFile="ConfirmOrder.aspx.vb" Inherits="WebConferenceN_ConfirmOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="JavaScript" src="https://seals.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>
    <script type="text/JavaScript">
        function ShowEditPopup(hid) {

            var ShipToID = document.getElementById('ctl00_ContentPlaceHolder1_' + hid).value
            var width = 500;
            var height = 480;
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
            newwin = window.open(Page, 'EditWebShpInfo', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function Showcardpopup() {
            var refnum = document.getElementById('ctl00_ContentPlaceHolder1_hdnWReffNumber').value;
            var width = 470;
            var height = 198;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var Page = "../Users_Login/UserCardDetails1.aspx?RefNum=" + refnum + "&Type=WOrder";
            newwin = window.open(Page, 'CardWeb', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

    </script>
    <div class="MnContentPage">
        <center>
            <div class="PageHeading">
                <asp:Label ID="lblMainHeader" runat="server"></asp:Label>
            </div>
        </center>
        <div style="font-family: Optima; font-size: 12px">
            <br />
            <asp:Label ID="lblSubHeader" runat="server"></asp:Label><br />
            <br />
        </div>
        <table style="width: 580px; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td class="WebInnerTd" style="width: 160px" align="right">
                    <b>Order Number:</b>
                </td>
                <td class="WebInnerTd">
                    <asp:Label ID="lblRefNumber" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="WebInnerTd" align="right">
                    <b>Sub Total:</b>
                </td>
                <td class="WebInnerTd">
                    US$<asp:Label ID="lblAmt" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="WebInnerTd" align="right">
                    <b>Sales Tax:</b>
                </td>
                <td class="WebInnerTd">
                    US$<asp:Label ID="lblTax" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="WebInnerTd" align="right" style="height: 21px" title="Shipping, Handling, and Credit Card Fees">
                    <b>CC S&H Fees:</b>
                </td>
                <td class="WebInnerTd" style="height: 21px">
                    US$<asp:Label ID="lblShippingCost" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="WebInnerTd" align="right">
                    <b>Order Amount:</b>
                </td>
                <td class="WebInnerTd">
                    US$<asp:Label ID="lblGrandTotal" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:Repeater ID="rptOrderReview" runat="server">
            <HeaderTemplate>
                <table style="text-align: left; width: 700px" cellpadding="2" cellspacing="1">
                    <tr style="height: 20px;">
                        <td class="WebHeaderTd" style="width: 20px;">
                            Qty
                        </td>
                        <td class="WebHeaderTd" style="width: 50px;">
                            Item Number
                        </td>
                        <td class="WebHeaderTd">
                            Item Description
                        </td>
                        <td class="WebHeaderTd" style="width: 100px;">
                            Delivery Format
                        </td>
                        <td class="WebHeaderTd" style="width: 50px;">
                            Page
                        </td>
                        <td class="WebHeaderTd" style="width: 50px;">
                            Unit Cost
                        </td>
                        <td class="WebHeaderTd" style="width: 50px;">
                            Subtotal
                        </td>
                        <td class="WebHeaderTd" style="width: 50px;">
                            Sales Tax
                        </td>
                        <td class="WebHeaderTd" style="width: 75px;" title="Shipping, Handling, and Credit Card Fees">
                            CC S&H Fees
                        </td>
                        <td class="WebHeaderTd" style="width: 50px;">
                            Total
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr style="height: 20px;" class="AlterNateColor1">
                    <td class="WebInnerTd" style="text-align: right; padding-right: 5px;">
                        <%#DataBinder.Eval(Container.DataItem, "QTY")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "ITEMNUMBER")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "ITEMDESCRIPTION")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "DELIVERYFORMAT")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "PAGE")%>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblUnitCost" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblSTax" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblShipCost" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr style="height: 20px;" class="AlterNateColor2">
                    <td class="WebInnerTd" style="text-align: right; padding-right: 5px;">
                        <%#DataBinder.Eval(Container.DataItem, "QTY")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "ITEMNUMBER")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "ITEMDESCRIPTION")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "DELIVERYFORMAT")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "PAGE")%>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblUnitCost" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblSubTotal" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblSTax" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblShipCost" runat="server"></asp:Label>
                    </td>
                    <td class="WebInnerTd">
                        $<asp:Label ID="lblTotal" runat="server"></asp:Label>
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <br />
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
                                <b>
                                    <asp:Label ID="lblHeading" runat="server" Style="margin-left: 159px;"></asp:Label>
                                    <asp:LinkButton ID="lnkHEdit" runat="server" OnClientClick="return ShowEditPopup('hdnUserID')"
                                        Style="color: orange; font-size: 12px; margin-left: 123px;">Edit</asp:LinkButton></b>
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
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Phone:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHPhone" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Fax:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHFax" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Street Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHStAdd" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                City:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHCity" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                State:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHState" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Zipcode:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHZip" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Country:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHCountry" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt1" runat="server" class="AlterNateColor4">
                            <td style="font-weight: bold; font-size: 12px;" class="TdHeading" colspan="2">
                                Bill To Address:
                                <asp:LinkButton ID="lnkBillTo" runat="server" OnClientClick="return ShowEditPopup('hdnBill')"
                                    Style="color: orange; padding-left: 333px;">Edit</asp:LinkButton>
                            </td>
                        </tr>
                        <tr id="trBt2" runat="server" class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblBillToName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt3" runat="server" class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblBillToEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt4" runat="server" class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Company Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblBillToComp" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt5" runat="server" class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Phone:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblphne" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt6" runat="server" class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Fax:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblFax" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt7" runat="server" class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Street Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblAdd" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt8" runat="server" class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                City:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCity" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt9" runat="server" class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                State:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblState" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt10" runat="server" class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Zip Code:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblZipCode" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trBt11" runat="server" class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Country:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCntry" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor4" runat="server" id="rowCardhead" visible="false">
                            <td style="font-weight: bold; font-size: 12px;" class="TdHeading" colspan="2">
                                Card Details:
                                <asp:LinkButton ID="lnkCardEdit" runat="server" OnClientClick="return Showcardpopup()"
                                    Style="color: orange; padding-left: 344px;">Edit</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" runat="server" id="rowCardtype" visible="false">
                            <td style="font-weight: bold; color: Red" class="WebInnerTd">
                                * Credit Card Type:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCCType" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" runat="server" id="rowCardNo" visible="false">
                            <td style="font-weight: bold; color: Red" class="WebInnerTd">
                                * Credit Card Number:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCC" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="Tr2" runat="server" visible="True">
                            <td style="font-weight: bold; color: Red" class="WebInnerTd">
                                * Security Code (CVV):
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblAuth_Code" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" runat="server" id="rowExpdate" visible="false">
                            <td style="font-weight: bold; color: Red" class="WebInnerTd">
                                * Expiration Date:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCCExp" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trAt1" runat="server" visible="false">
                            <td colspan="2">
                                <table cellpadding="0" cellspacing="2" style="margin-top: -1px;">
                                    <tr class="AlterNateColor4">
                                        <td style="font-weight: bold; font-size: 12px; width: 500px;" class="TdHeading" colspan="2">
                                            Attendee 1:
                                            <asp:LinkButton ID="lnkShipTo1" OnClientClick="return ShowEditPopup('hdnShp1')" runat="server"
                                                Style="color: orange; padding-left: 354px;">Edit</asp:LinkButton>
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode1" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo2" runat="server" OnClientClick="return ShowEditPopup('hdnShp2')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode2" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo3" runat="server" OnClientClick="return ShowEditPopup('hdnShp3')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode3" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo4" runat="server" OnClientClick="return ShowEditPopup('hdnShp4')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode4" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo5" runat="server" OnClientClick="return ShowEditPopup('hdnShp5')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode5" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo6" runat="server" OnClientClick="return ShowEditPopup('hdnShp6')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode6" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo7" runat="server" OnClientClick="return ShowEditPopup('hdnShp7')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode7" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo8" runat="server" OnClientClick="return ShowEditPopup('hdnShp8')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode8" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo9" runat="server" OnClientClick="return ShowEditPopup('hdnShp9')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode9" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                                            <asp:LinkButton ID="lnkShipTo10" runat="server" OnClientClick="return ShowEditPopup('hdnShp10')"
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
                                            Company Name:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCompNm10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Phone:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttphne10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Fax:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttFax10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Street Address:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttAdd10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            City:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttCity10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            State:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttState10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                            Zip Code:
                                        </td>
                                        <td align="left" class="WebInnerTd" style="width: 266px">
                                            <asp:Label ID="lblAttZipCode10" runat="server" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
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
                        <%--<tr class="AlterNateColor4" id="trC1" runat="server">
                            <td style="font-weight: bold; width: 33%; font-size: 12px;" class="TdHeading" colspan="2">
                                Card Holder Address:
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="trC2" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="trC3" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="trC4" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Phone:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHphne" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="trC5" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Fax:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHFax" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="trC6" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Street Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHAdd" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="trC7" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                City:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHCity" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="trC8" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                State:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHState" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="trC9" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Zip Code:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHZipCode" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="trC10" runat="server">
                            <td style="font-weight: bold; width: 33%" class="WebInnerTd">
                                Country:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCardHCntry" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>--%>
                        <tr class="AlterNateColor1">
                            <td colspan="2" style="width: 500px;" align="center" class="WebInnerTd">
                                <asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Charge Your Card" />
                                <asp:Button ID="btnOrderClose" Visible="false" runat="server" CssClass="ButtonWMarigin"
                                    Text="Close This Page" OnClientClick="javascript:window.close();" />
                                <asp:Button ID="btnOrderCloseFree" runat="server" CssClass="ButtonWMarigin" Text="Complete Registration" />
                            </td>
                        </tr>
                    </table>
                </td>
                <%-- <td style="width: 20%">
                    <script type="text/javascript">                        SiteSeal("https://seal.networksolutions.com/images/colorsealbasic.gif", "NETSB", "none");</script>
                </td>--%>
                <td style="width: 20%">
                    <%--   <script type="text/javascript">                        SiteSeal("https://seal.networksolutions.com/images/colorsealbasic.gif", "NETSB", "none");</script>--%>
                </td>
            </tr>
        </table>
		<asp:HiddenField ID="hidFName" runat="server" />
        <asp:HiddenField ID="hidLName" runat="server" />
        <asp:HiddenField ID="hidCompanyName" runat="server" />
        <asp:HiddenField ID="hidExpDate" runat="server" />
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
        <asp:HiddenField ID="hdnBill" runat="server" />
        <asp:HiddenField ID="hidlnkEdit" runat="server" />
        <asp:HiddenField ID="hdnWReffNumber" runat="server" />
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </div>
</asp:Content>
