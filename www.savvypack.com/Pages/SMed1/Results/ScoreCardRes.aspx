﻿<%@ Page Title="SMed1-Scorecard Results" Language="VB" MasterPageFile="~/Masters/SMed1.master" AutoEventWireup="false" CodeFile="ScoreCardRes.aspx.vb" Inherits="Pages_MedSustain1_Results_ScoreCardRes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SMed1ContentPlaceHolder" Runat="Server">
   <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/SMed1Comman.js"></script>
   <script type="text/JavaScript">
         function ShowPopWindow(Page) {
                //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
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
         </div>
         
         
   
   </div>
</asp:Content>
