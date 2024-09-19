<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false" CodeFile="Order.aspx.vb" Inherits="ShoppingCart_Order" title="Shopping Cart - Order Allied Study" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/JavaScript" src="../JavaScripts/collapseableDIV.js"></script>
<script type="text/JavaScript" src="../JavaScripts/wz_tooltip.js"></script>
<script type="text/JavaScript" src="../JavaScripts/tip_balloon.js"></script>
<script type="text/JavaScript" src="../JavaScripts/Common.js"></script>
<script type="text/javascript">

        
</script>
  <div class="MnContentPage">
       <center>
        <div class="PageHeading">        
          <asp:Label ID="lblorder" runat="server" CssClass="NormalLabel"></asp:Label>
                     <br /><br />
        </div>
         <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">
        
            <tr>
                <td class="WebInnerTd">
                   <asp:Label ID="lblRefNumber" runat="server" CssClass="NormalLabel"></asp:Label>
                 
                </td>
             </tr>
        </table>
       
         <asp:Repeater id="rptOrder" runat="server">
            <HeaderTemplate>   
               <table style="width:400px;text-align:left" cellpadding="2" cellspacing="0">
                    
                    <tr style="height:20px;">
                        <td class="WebHeaderTd" style="width:200px;">
                            Item
                        </td>
                        <td class="WebHeaderTd" style="width:100px;">
                            Price
                        </td>
                        <td class="WebHeaderTd" style="width:100px;font;">
                            Quantity
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                    <tr style="height:20px;" class="AlterNateColor1">
                        <td class="WebInnerTd">
                            <asp:Label ID="lblDelFromat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DELFORMAT")%> '></asp:Label>                            
                        </td>
                        <td class="WebInnerTd">
                           US$ <%#DataBinder.Eval(Container.DataItem, "PRICE")%> 
                        </td>
                         <td class="WebInnerTd">
                            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="10" CssClass="MediumTextBox" Text='<%#DataBinder.Eval(Container.DataItem, "QTY")%>'></asp:TextBox>
                        </td>
                    </tr>
            
            </ItemTemplate>
             <AlternatingItemTemplate>
                    <tr style="height:20px;"  class="AlterNateColor2">
                        <td class="WebInnerTd">
                               <asp:Label ID="lblDelFromat" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "DELFORMAT")%> '></asp:Label>
                        </td>
                        <td class="WebInnerTd">
                           US$ <%#DataBinder.Eval(Container.DataItem, "PRICE")%> 
                        </td>
                         <td class="WebInnerTd">
                            <asp:TextBox ID="txtQuantity" runat="server" MaxLength="10" CssClass="MediumTextBox" Text='<%#DataBinder.Eval(Container.DataItem, "QTY")%>'></asp:TextBox>
                        </td>
                    </tr>
            
            </AlternatingItemTemplate>
             <FooterTemplate>                    
                    </table>
            </FooterTemplate>
            </asp:Repeater>
          
         <br />
        <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">

            <tr>
            <td class="WebInnerTd" align="center">
                <asp:Button id="btnAddToCart" runat="server" Text="ADD TO CART" CssClass="ButtonWMarigin" OnClientClick="return checkNumericAll();" />
                <input type="button" onclick="javascript:window.close();" class="ButtonWMarigin" value="KEEP SHOPPING" />
              
            </td>
            
            </tr>
        </table>
            </center>
            <br />
  </div>
</asp:Content>


