<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain2.master" AutoEventWireup="false" CodeFile="EquipmentIN.aspx.vb" Inherits="Pages_Sustain2_Assumptions_EquipmentIN" title="S2-Equipment Assumptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain2ContentPlaceHolder" Runat="Server">
  <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/S2Comman.js"></script>
  <script type="text/JavaScript">
      function ShowEditPopWindow(Page, Id) {

          var width = 550;
          var height = 200;
          var left = (screen.width - width) / 2;
          var top = (screen.height - height) / 2;
          var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
          params += ', location=no';
          params += ', menubar=no';
          params += ', resizable=no';
          params += ', scrollbars=no';
          params += ', status=yes';
          params += ', toolbar=no';
          var Eid = document.getElementById(Id).value;
          Page = Page + "&EqId=" + Eid
          newwin = window.open(Page, 'Chat', params);
      }

      function ShowPopWindownew(el, Page, Id) {

          // find all elements that have the linkActive class
          var elems = document.querySelectorAll(".linkActive");

          // loop through them and ...
          for (var i = 0; i < elems.length; i++) {
              // remove the linkActive class
              elems[i].classList.remove('linkActive');
              elems[i].style.color = 'Black';
          }

          // now add the class to the link that was just clicked
          el.classList.add('linkActive');
          el.style.color = 'red';

          var width = 550;
          var height = 400;
          var left = (screen.width - width) / 2;
          var top = (screen.height - height) / 2;
          var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
          params += ', location=no';
          params += ', menubar=no';
          params += ', resizable=no';
          params += ', scrollbars=no';
          params += ', status=yes';
          params += ', toolbar=no';
          var Mid = document.getElementById(Id).value;
          Page = Page + "&EId=" + Mid
          newwin = window.open(Page, 'Chat', params);
          return false;
      }

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
     </script><div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />
         </div>
         </div> 
           <asp:HiddenField ID="hidBarrier" runat="server" />
         
         
</asp:Content>

