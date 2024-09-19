<%@ Page Language="VB" MasterPageFile="~/Masters/MoldSustain2.master" AutoEventWireup="false"
    CodeFile="PlantConfig.aspx.vb" Inherits="Pages_MoldSustain2_Assumptions_PlantConfig"
    Title="S2 Mold-Plant Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldSustain2ContentPlaceHolder"
    runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
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
    <div id="PageSection1" style="text-align: left">
        <br />
        <table border="0" cellpadding="0" cellspacing="0" style="width: 830px">
            <tr>
                <td valign="top" style="width: 10%">
                    <asp:Table ID="tblComparisionFix" runat="server" CellPadding="0" CellSpacing="2">
                    </asp:Table>
                </td>
                <td valign="top" style="width: 90%;">
                    <div style="width: 670px; overflow: auto; height: 330px;">
                        <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </div>
                </td>
            </tr>
        </table>
        <br />
    </div>
</asp:Content>
