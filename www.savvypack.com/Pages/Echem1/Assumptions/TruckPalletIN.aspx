﻿<%@ Page Title="Echem1-Pallet and Truck Configuration" Language="VB" MasterPageFile="~/Masters/Echem1.master" AutoEventWireup="false" CodeFile="TruckPalletIN.aspx.vb" Inherits="Pages_Echem1_Assumptions_TruckPalletIN" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Echem1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/Echem1Comman.js"></script>
    <script type="text/JavaScript">
        function ShowPopWindow(Page) {
            var width = 550;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);

        }
     </script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />
                <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />
                <asp:Table ID="tblComparision2" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />
         </div>   
     </div>
</asp:Content>

