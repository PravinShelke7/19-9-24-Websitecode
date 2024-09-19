<%@ Page Title="E2-Profit and Loss Statement with Depreciation " Language="VB" MasterPageFile="~/Masters/Econ2.master" AutoEventWireup="false" CodeFile="ResultsPLwithDEP.aspx.vb" Inherits="Pages_Econ2_Results_ResultsPLwithDEP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ2ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
      <script type="text/JavaScript" src="../../../JavaScripts/E2Comman.js"></script>
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
             <asp:TextBox ID="txthiddien" Style="visibility:hidden;" runat="server" Text="0"></asp:TextBox>
         </div>   
     </div>
       <asp:HiddenField ID="hdnVolume" runat="server" />
         <asp:HiddenField ID="hdnUnit" runat="server" />  
</asp:Content>


