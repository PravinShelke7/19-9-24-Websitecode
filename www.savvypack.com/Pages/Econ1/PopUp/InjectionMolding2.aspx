﻿<%@ Page Title="E1 - Injection Molding Assistant" Language="VB" MasterPageFile="~/Masters/E1Ink.master" AutoEventWireup="false" CodeFile="InjectionMolding2.aspx.vb" Inherits="Pages_Econ1_PopUp_InjectionMolding2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="E1InkContentPlaceHolder" Runat="Server">
<script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/SDistComman.js"></script>
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
       function ScrollOption(Id) {
           var rd = document.getElementById(Id)
           var div = document.getElementById("divVariable")

           if (rd.value == "1") {
               div.className = "DivScrolles";
           }
           else {
               div.className = "DivWScrolles";
           }

       }
     </script>
      <table cellpadding="0" border="0" cellspacing="0" style="border-collapse:collapse;text-align:center;padding-left:55px;">
            <tr>
                <td colspan="2"> 
                   <asp:Table ID="tblTab"  border="0" runat="server" CellPadding="0" CellSpacing="0"></asp:Table>
                </td>
            </tr>
       </table> 
       <div id="PageSection1" style="text-align:left;" >
          
               <table cellspacing="0" border="0" cellpadding="0" style="width:100%">
                <tr>
                  <%--  <td style="padding-left:5px" valign="top">
                       <asp:Table ID="tblFixComparision" runat="server" CellPadding="0" CellSpacing="2" style="border-collapse:collapse;"></asp:Table>
                       <input type="radio" checked="checked" name="rdScroll" id="rdScrollOn" onclick="ScrollOption('rdScrollOn');" value="1" />Scroll <input id="rdScrollOff" name="rdScroll" type="radio" onclick="ScrollOption('rdScrollOff');" value="0" />No Scroll

                    </td>--%>
                    <td valign="top" colspan="2">
                        <div id="divVariable" style="height:480px;">
                         <asp:Table ID="tblComparision" border="0" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                        </div>
                    </td> 
                </tr>
               </table> 
               
                <div style="margin-left:5px;margin-top:5px;">         
             <asp:Table ID="tblComparision1" runat="server" border="0" CellPadding="0" CellSpacing="2"></asp:Table>
              </div>
              <br />
         </div>
</asp:Content>

