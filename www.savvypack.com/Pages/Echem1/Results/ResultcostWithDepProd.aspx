<%@ Page Language="VB" MasterPageFile="~/Masters/Echem1.master" AutoEventWireup="false" CodeFile="ResultcostWithDepProd.aspx.vb"
 Inherits="Pages_Echem1_Results_ResultcostWithDepProd" title="Echem1-Packaging Cost with depreciation Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Echem1ContentPlaceHolder" Runat="Server">
  <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
            <table style="width:500px;">
                <tr>
                     
                    <td>
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td>
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
             </table>
                <asp:Table ID="tblComparision" runat="server" CellPadding="1" CellSpacing="2"></asp:Table>
             <br />         
         </div>   
     </div>
</asp:Content>

