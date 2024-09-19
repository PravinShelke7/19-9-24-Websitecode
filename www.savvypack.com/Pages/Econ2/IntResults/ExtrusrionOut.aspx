<%@ Page Title="E2-Material and Structure Results" Language="VB" MasterPageFile="~/Masters/Econ2.master" AutoEventWireup="false" CodeFile="ExtrusrionOut.aspx.vb" Inherits="Pages_Econ2_IntResults_ExtrusrionOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ2ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />         
         </div>   
     </div>
</asp:Content>

