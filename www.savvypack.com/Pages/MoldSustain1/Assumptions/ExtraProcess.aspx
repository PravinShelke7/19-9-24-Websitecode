<%@ Page Language="VB" MasterPageFile="~/Masters/MoldSustain1.master" AutoEventWireup="false"
    CodeFile="ExtraProcess.aspx.vb" Inherits="Pages_MoldSustain1_Assumptions_ExtraProcess"
    Title="S1 Mold-Extra-process Waste" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldSustain1ContentPlaceHolder"
    runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/MoldS1Comman.js"></script>
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
        <div id="PageSection1" style="text-align: left">
            <br />
            <%-- <table cellspacing="0" cellpadding="0" style="border-collapse:collapse;width:100%">
                <tr>
                    <td style="padding-left:0px" valign="top">--%>
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="1">
            </asp:Table>
            <br />
            <%--</td>
                    <td valign="top" align="left" >
                       
                    </td> 
                </tr>
               </table> --%>
        </div>
    </div>
</asp:Content>
