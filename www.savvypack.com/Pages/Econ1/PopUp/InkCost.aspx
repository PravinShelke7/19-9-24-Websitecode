<%@ Page Title="Ink Cost Assistant" Language="VB" MasterPageFile="~/Masters/E1Ink.master" AutoEventWireup="false" CodeFile="InkCost.aspx.vb" Inherits="Pages_Econ1_PopUp_InkCost" %>

<asp:Content ID="Content1" ContentPlaceHolderID="E1InkContentPlaceHolder" Runat="Server">
   <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
    <script type="text/javascript">
    function ShowPopWindow(Page)  
    {
        var width = 960;
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
</script>
 <div id="PageSection1" style="text-align:left" >
          
            <br />
          
            <table cellspacing="10">
                <tr>
                    <td valign="top" colspan="2">
                         <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2" style="margin-left:10px;width:740px;"></asp:Table>
                    </td> 
                </tr>
              
             
             
             </table> 
         </div>
</asp:Content>

