<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/Med1.master" AutoEventWireup="false" CodeFile="InjectionResult.aspx.vb" Inherits="Pages_MedEcon1_PopUp_InjectionResult" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Med1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
            <table style="width:1000px;">
                <tr>
                     
                    <td style="width:1050px;">
                        <asp:Label id="lblClampForce" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td></td><td></td>
                     <td style="width:1050px;">
                        <asp:Label id="lblShotWt" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td></td><td></td>
                     <td style="width:1050px;">
                        <asp:Label id="lblInjRate" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
             </table>
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />         
         </div>  
          <asp:HiddenField ID="hdnVolume" runat="server" />
         <asp:HiddenField ID="hdnUnit" runat="server" />   
     </div>
</asp:Content>
