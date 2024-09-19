<%@ Page Title="Edist-Profit and Loss Statement with Depreciation " Language="VB" MasterPageFile="~/Masters/Distribution.master" AutoEventWireup="false" CodeFile="ResultsPLwithDEP.aspx.vb" Inherits="Pages_Distribution_Results_ResultsPLwithDEP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DistributionContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
              <table style="width:400px;">
                <tr>
                   <td>
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td>
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
             </table>
             <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />   
             <asp:TextBox ID="txthiddien" Style="visibility:hidden;" runat="server"></asp:TextBox>
         </div>   
     </div>
</asp:Content>


