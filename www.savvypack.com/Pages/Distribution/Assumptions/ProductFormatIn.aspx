<%@ Page Title="Edist-Product Format Assumptions" Language="VB" MasterPageFile="~/Masters/Distribution.master" AutoEventWireup="false" CodeFile="ProductFormatIn.aspx.vb" Inherits="Pages_Distribution_Assumptions_ProductFormatIn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DistributionContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
      <script type="text/JavaScript" src="../../../JavaScripts/DistComman.js"></script>
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
              if(checkNumericAll())
            {
               newwin = window.open(Page, 'Chat', params);
            }

        }
     </script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />
                <div style="display:none;">
                    <asp:Button ID="btnHidden" runat="server" />
                </div>
         </div>   
     </div>
</asp:Content>

