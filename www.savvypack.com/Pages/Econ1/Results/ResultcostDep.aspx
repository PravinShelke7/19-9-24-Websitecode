<%@ Page Title="E1-Manufacturing Cost with Depreciation" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="ResultcostDep.aspx.vb" Inherits="Pages_Econ1_Results_ResultcostDep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
              <table style="width:780px;">
                <tr>
                   
                    <td>
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td>
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
 <td>
                         <asp:Label id="lblSalesVolume" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
             </table>
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />         
         </div>   
     </div>
</asp:Content>
