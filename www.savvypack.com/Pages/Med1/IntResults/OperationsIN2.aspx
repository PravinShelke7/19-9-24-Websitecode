﻿<%@ Page Title="Med1-Operating Results" Language="VB" MasterPageFile="~/Masters/Med1.master" AutoEventWireup="false" CodeFile="OperationsIN2.aspx.vb" Inherits="Pages_MedEcon1_IntResults_OperationsIN2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Med1ContentPlaceHolder" Runat="Server">
    
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
