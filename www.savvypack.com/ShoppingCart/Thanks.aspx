<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false" CodeFile="Thanks.aspx.vb" Inherits="ShoppingCart_Thanks" title="Shopping Cart - Order Added" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <div class="MnContentPage">
       <center>
        <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">
        
            <tr>
                <td class="WebInnerTd">
                   <asp:Label ID="lblRefNumber" runat="server" CssClass="NormalLabel"></asp:Label>
                 
                </td>
             </tr>
        </table>
        <div class="PageHeading">
            The Item Has Been Added to Your Cart.
        </div>
        
           <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">
        
            <tr>
                <td class="WebInnerTd">
                   Click the "REVIEW ORDER" button to review the contents of your cart.
                    Click the "KEEP SHOPPING" button to close the window and continue shopping.
                 
                </td>
             </tr>
        </table>
        
           <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">

            <tr>
            <td class="WebInnerTd" align="center">
                <asp:Button id="btnOrderReview" runat="server" Text="REVIEW ORDER" CssClass="ButtonWMarigin" PostBackUrl="~/ShoppingCart/OrderReview.aspx" />
                <input type="button" onclick="javascript:window.close();" class="ButtonWMarigin" value="KEEP SHOPPING" />
              
            </td>
            
            </tr>
        </table>
       </center>
  </div>
</asp:Content>

