<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="ConfirmOrder.aspx.vb" Inherits="ShoppingCart_ConfirmOrder" Title="Shopping Cart - Confirm Order" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="https://js.stripe.com/v3/"></script>
    <script language="JavaScript" src="https://seal.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>
    <style type="text/css">
        .btninvoice1 {
            background-color: ghostwhite;
            color: blue;
            padding: 3px 7px;
            text-decoration: none;
            border: 1px solid;
            border-block-color: black;
        }
    </style>
    <script type="text/JavaScript">
        function CloseWindow() {
                      
            window.close();
            self.close();
        }
        function ShowEditPopup(ShipToID) {

            if (ShipToID == null) {
                ShipToID = document.getElementById('ctl00_ContentPlaceHolder1_hidBillToID').value;
            }
            if (ShipToID.toString() == "LogUserID") {
                ShipToID = document.getElementById('ctl00_ContentPlaceHolder1_hdnUserID').value;
            }

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
            newwin = window.open(Page, 'EditShpInfo', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function Showcardpopup() {
            var refnum = document.getElementById('ctl00_ContentPlaceHolder1_hdnReffNumber').value;
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
            var Page = "../Users_Login/UserCardDetails1.aspx?RefNum=" + refnum + "&Type=Order";
            newwin = window.open(Page, 'Card', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

    </script>

   
    <div class="MnContentPage" style="padding-left:100px" >
        <center>
            <div class="PageHeading">
                <asp:Label ID="lblMainHeader" runat="server"></asp:Label>
            </div>
        </center>
        <div class="WebInnerTd">
            <br />
            <b>
                <asp:Label ID="lblMidHeader" runat="server"></asp:Label>
            </b>
        </div>
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
                <td class="WebInnerTd" align="right" style="height: 21px">
                    <b>
                        <asp:Label ID="lblshf2" runat="server" ToolTip="Shipping, Handling, and Credit Card Fees"> S&H Fees:</asp:Label>
                    </b>
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
                        <td class="WebHeaderTd" style="width: 50px;">
                            <asp:Label ID="lblshf1" runat="server" ToolTip="Shipping, Handling, and Credit Card Fees"> S&H Fees</asp:Label>
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
            <tr>
                <td style="width: 80%">
                    <table cellpadding="0" cellspacing="2" style="width: 100%">
                        <tr class="AlterNateColor4">
                            <td align="center" class="TdHeading" colspan="2">
                                <b>
                                    <asp:Label ID="lblHeading" runat="server" Text="Purchaser Address" Style="margin-left: 150px;"></asp:Label>
                                    <asp:LinkButton ID="lnkHEdit" runat="server" OnClientClick="return ShowEditPopup('LogUserID');"
                                        Style="color: orange; font-size: 12px; margin-left: 129px;">Edit</asp:LinkButton></b>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" class="WebInnerTd">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Company Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCompName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Phone:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHPhone" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Fax:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHFax" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Street Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHStAdd" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                City:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHCity" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                State:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHState" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Zipcode:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHZip" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 30%" class="WebInnerTd">
                                Country:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblHCountry" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor4">
                            <td style="font-weight: bold; font-size: 12px;" class="TdHeading" colspan="2">
                                Bill To Address:
                                <asp:LinkButton ID="lnkBillTo" runat="server" OnClientClick="return ShowEditPopup();"
                                    Style="color: orange; padding-left: 333px;">Edit</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblBillToName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblBillToEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Company Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblBillToComp" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Phone:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblphne" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Fax:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblFax" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Street Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblAdd" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                City:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCity" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                State:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblState" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Zip Code:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblZipCode" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold;" class="WebInnerTd">
                                Country:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCntry" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                                                <tr>
                            <td colspan="2">
                                <asp:Table ID="tblShipAdd" runat="server" Style="width: 100%">
                                </asp:Table>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">                            
                            <td colspan="2" align="center" class="WebInnerTd">
                                <%-- <button type="submit" class="ButtonWMarigin" >Checkout</button>--%>
                                <asp:Button type="Button" ID="btnOrder" runat="server" Visible ="false"  CssClass="ButtonWMarigin" Text="Charge Your Card" />
                                <asp:LinkButton ID="lnkbtninvoice" runat="server" CssClass ="btninvoice1" Visible ="false"  >Invoice Request</asp:LinkButton>
                                <asp:LinkButton ID="lnkClose" runat="server" CssClass ="btninvoice1" OnClientClick="CloseWindow()" Visible ="false"   >Close This Page</asp:LinkButton>

                               <%-- <asp:ImageButton ID="imginvoice" runat="server" Visible ="false"  ImageUrl="~/Images/OrderButton.png" />--%>
                                <asp:Button ID="btnOrderClose" Visible="false" runat="server" CssClass="ButtonWMarigin"
                                    Text="Close This Page" OnClientClick="CloseWindow()" />
                                <%--<asp:ImageButton ID="imgclose" runat="server" ImageUrl="~/Images/Close.gif"  Visible ="false" OnClientClick="javascript:window.close();" />--%>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20%">
                    <%--<script type="text/javascript">SiteSeal("https://seal.networksolutions.com/images/colorsealbasic.gif", "NETSB", "none");</script>--%>
                    <!--
                SiteSeal Html Builder Code:
                Shows the logo at URL https://seal.networksolutions.com/images/dvsqgreen.gif
                Logo type is  ("NETDV")
                //-->
                    <%--<script language="JavaScript" type="text/javascript"> SiteSeal("https://seal.networksolutions.com/images/dvsqgreen.gif", "NETDV", "none");</script>--%>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidFName" runat="server" />
        <asp:HiddenField ID="hidLName" runat="server" />
        <asp:HiddenField ID="hidCompanyName" runat="server" />
        <asp:HiddenField ID="hidExpDate" runat="server" />
        <asp:HiddenField ID="hidItemNum" runat="server" />
        <asp:HiddenField ID="hidBillToID" runat="server" />
        <asp:HiddenField ID="hidlnkEdit" runat="server" />
        <asp:HiddenField ID="hdnReffNumber" runat="server" />
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </div>
     <script>

         // var stripe = Stripe('pk_live_51JaOKiIWxVMfrD0UKlGuc6hh1PsTM4UxydVjwdKgJnGfMJG5uHQIe4fP0x9ioKx5OrIPVDr4IFvrONtTiXySml9V00zbAOrHXt');
         var stripe = Stripe('pk_test_51JaOKiIWxVMfrD0UCOedGnVgUMTLWiCuWGZm2bFjcDh4y5mxFqZd90cAF912gp2eEogIDum6hboNV7kCpR1gYuFh00LA7xoMRS');
         var form = document.getElementById("aspnetForm");
         form.addEventListener('submit', function (e) {
             e.preventDefault();
             stripe.redirectToCheckout({
                 sessionId: "<%= sessionId %>"
             });
         })
     </script>
</asp:Content>
