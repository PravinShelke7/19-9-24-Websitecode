<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="ShoppingCart.aspx.vb" Inherits="ShoppingCart_ShoppingCart" Title="Shopping Cart - User Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script language="JavaScript" src="https://seals.networksolutions.com/siteseal/javascript/siteseal.js" type="text/javascript"></script>--%>
    <script language="JavaScript" src="https://seal.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>
    <style type="text/css">
        .SmallLabel
        {
            font-family: Optima;
            font-size: 10px;
            color: Green;
            width: 100px;
            padding-left: 10px;
        }
        .style1
        {
            width: 80%;
        }
    </style>
    <script type="text/javascript" language="javascript">

       

        function OpenNewWindow(Page, PageName) {

            var width = 750;
            var height = 620;
            var left = (screen.width - width) / 2;
            var top = 10; //(screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, PageName, params);
            return false;
        }
        function ShowPopWindow(Page, PageName) {
            var width = 750;
            var height = 620;
            var left = (screen.width - width) / 2;
            var top = 10; // (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, PageName, params);

        }
    </script>
    <div class="MnContentPage">
        <%--<div style="font-family:Optima;font-size:12px">
    Please enter your credit card information.<br />
        <br />
    </div>--%>
        <table style="width: 100%; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td class="WebInnerTd" style="width: 18%;">
                </td>
                <td class="WebInnerTd" style="width: 82%;">
                    <b>Order Number:</b>
                    <asp:Label ID="lblRefNumber" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="WebInnerTd">
                </td>
                <td class="WebInnerTd" colspan="2">
                    <b>Order Amount:</b> US$<asp:Label ID="lblAmt" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <%--<div style="font-family:Optima;font-size:12px">
        <b>Note:</b> Please resfresh the page to see updated profile.
    </div> --%>
        <table style="width: 100%; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td align="center" class="style1">
                    <table cellpadding="0" cellspacing="2" style="width: 600px">
                        <tr class="AlterNateColor4">
                            <td align="center" class="TdHeading" colspan="2">
                                <b>Order Information</b>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 23%" class="WebInnerTd">
                                User Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 23%" class="WebInnerTd">
                                User Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 23%" class="WebInnerTd">
                                Company Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCompName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr id="trAt1" runat="server">
                            <td colspan="2">
                                <asp:Table ID="tblShipAdd" runat="server" CellPadding="0" CellSpacing="2" Style="margin-top: -2px;
                                    margin-bottom: -2px; margin-left: -2px; margin-right: -2px;">
                                </asp:Table>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="aa" runat="server" visible="false">
                            <td style="font-weight: bold; width: 23%; color: Red" class="WebInnerTd">
                                * Ship To Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:DropDownList ID="ddlShipTo" runat="server" Style="font-family: Optima; font-size: 11px;"
                                    Width="300px" Visible="false">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkShpAdd" runat="server" CssClass="Link">Nothing Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 23%; color: Red" class="WebInnerTd" title="Name on the credit card and the billing address of the credit card">
                                * Bill To Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:DropDownList ID="ddlBillTo" runat="server" Style="font-family: Optima; font-size: 11px;"
                                    Width="300px" Visible="false">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkBillAdd" runat="server" CssClass="Link">Nothing Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <%--<tr class="AlterNateColor2" id="rowCardHolderAdd" runat="server" visible="True">
                            <td style="font-weight: bold; width: 23%; color: Red" class="WebInnerTd" title = " Usually, the same as Bill to Address." >
                                * Card Holder Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                  <asp:LinkButton ID="lnkCardHAdd" runat="server" CssClass="Link" >Nothing Selected</asp:LinkButton>
                            </td>
                        </tr>--%>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 23%; color: Red" class="WebInnerTd">
                                * Select Payment Type:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <input type="radio" runat="server" id="rdbCreditcard" checked="True" onclick="enableCrditRows(this)" />Pay
                                with Credit Card
                                <input type="radio" runat="server" id="rdbInvoice" onclick="disableCrditRows(this)" />Invoice
                                Me
                            </td>
                        </tr>
                       
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 23%" class="WebInnerTd">
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Continue Order Process"
                                    ToolTip="Continuing will not charge your credit card.You will have another chance to confirm or change your order." />
                            </td>
                        </tr>
                    </table>
                </td>
                <%-- <td style="width:20%">
               <script type="text/javascript">SiteSeal("https://seal.networksolutions.com/images/colorsealbasic.gif", "NETSB", "none");</script>           
                <!--
                SiteSeal Html Builder Code:
                Shows the logo at URL https://seal.networksolutions.com/images/dvsqgreen.gif
                Logo type is  ("NETDV")
                //-->

                <script language="JavaScript" type="text/javascript"> SiteSeal("https://seal.networksolutions.com/images/dvsqgreen.gif", "NETDV", "none");</script>

            </td>--%>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:HiddenField ID="hdnBillAddId" runat="server" />
                    <asp:HiddenField ID="hdnbilladdDes" runat="server" />

                    <asp:HiddenField ID="hdnShipAddId" runat="server" />
                    <asp:HiddenField ID="hdnShipAddNo" runat="server" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
