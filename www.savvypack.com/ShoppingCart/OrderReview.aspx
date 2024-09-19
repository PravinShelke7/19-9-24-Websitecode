<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="OrderReview.aspx.vb" Inherits="ShoppingCart_OrderReview" Title="Shopping Cart - Order Review" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="MnContentPage">
        <center>
            <div class="PageHeading">
                REVIEW YOUR ORDER
            </div>
            Please review the following item(s) in your cart. Press the 'Proceed to Order' button
            when you are ready to continue with your order.
        </center>
        <table style="margin-left: 150px;" cellpadding="0" cellspacing="0">
            <tr>
                <td class="WebInnerTd">
                    <asp:Label ID="lblRefNumber" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="padding-left: 150px">
                    <asp:Repeater ID="rptOrderReview" runat="server">
                        <HeaderTemplate>
                            <table style="text-align: left; width: 590px" cellpadding="2" cellspacing="1">
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
                                    <%#DataBinder.Eval(Container.DataItem, "LINK")%>
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
                                    <%#DataBinder.Eval(Container.DataItem, "LINK")%>
                                </td>
                            </tr>
                        </AlternatingItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                    <table style="width: 590px; text-align: left" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="WebInnerTd" style="padding-left: 380px;" colspan="3">
                                <b>Order Total: US$<asp:Label ID="lblOrderTotal" runat="server"></asp:Label>
                                    <br />
                                </b>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnOrderProcedd" runat="server" Text="Proceed To Order" CssClass="ButtonWMarigin" />
                            </td>
                            <td>
                                <asp:Button ID="btnEmpty" runat="server" Text="Empty Your Cart" CssClass="ButtonWMarigin" />
                            </td>
                            <td>
                                <input type="button" onclick="javascript:window.close();" class="ButtonWMarigin"
                                    value="Keep Shopping" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
