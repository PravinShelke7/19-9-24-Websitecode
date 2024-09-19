<%@ Page Language="VB"   MasterPageFile="~/Masters/SavvyPackMenu.master"  AutoEventWireup="false" CodeFile="ThankYouPage.aspx.vb" Inherits="ShoppingCart_ThankYouPage"  title="Shopping Cart - Order Added" %>

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
        <h1 > 
         THANK YOU 
         </h1>
        </div>
        
           <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">
        
            <tr>
                <td class="WebInnerTd">
                <h2  >
                &nbsp; &nbsp;&nbsp;
                &nbsp; &nbsp;&nbsp; &nbsp; 
                    Your order was completed successfully.
                    </h2>
                </td>
             </tr>
        </table>
        
           <table style="width:400px;text-align:left" cellpadding="0" cellspacing="0">

            <tr>
            <td class="WebInnerTd" align="center">
                
                <input type="button" onclick="javascript:window.close();" class="ButtonWMarigin" value="KEEP SHOPPING" />
              
            </td>
            
            </tr>
        </table>
       </center>
  </div>
</asp:Content>

