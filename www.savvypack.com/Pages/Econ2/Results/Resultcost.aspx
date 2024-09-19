<%@ Page Title="E2-Packaging Cost" Language="VB" MasterPageFile="~/Masters/Econ2.master" AutoEventWireup="false" CodeFile="Resultcost.aspx.vb" Inherits="Pages_Econ2_Results_Resultcost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ2ContentPlaceHolder" Runat="Server">
    
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


