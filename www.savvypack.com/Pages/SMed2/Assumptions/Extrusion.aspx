<%@ Page Language="VB" MasterPageFile="~/Masters/SMed2.master" AutoEventWireup="false" CodeFile="Extrusion.aspx.vb" Inherits="Pages_MedSustain2_Assumptions_Extrusion" title="SMed2-Material Assumptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SMed2ContentPlaceHolder" Runat="Server">
<script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
<script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
<script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/SMed2Comman.js"></script>
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
                params += ', resizable=no';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                newwin = window.open(Page, 'Chat', params);

            }
     </script><div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
            <%--<div style="margin-left:10px;padding-top:5px;">
                <span class="PageSHeading">Date:</span><asp:Label ID="lblEffDate" CssClass="NormalLable" runat="server"></asp:Label>
            </div>--%>
            <br />
            <table cellspacing="10">
                <tr>
                    <td style="width:30%" valign="top" colspan="2">
                         <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                    </td> 
                </tr>
                <tr>
                   <td style="width:30%" valign="top" colspan="2">
                        <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                   </td> 
                </tr>             
             </table> 
         </div>
         </div>
</asp:Content>

