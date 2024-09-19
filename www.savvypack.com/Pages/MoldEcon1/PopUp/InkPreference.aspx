<%@ Page Title="Ink Cost Preferences" Language="VB" MasterPageFile="~/Masters/MoldE1Ink.master"
    AutoEventWireup="false" CodeFile="InkPreference.aspx.vb" Inherits="Pages_MoldEcon1_PopUp_InkPreference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldE1InkContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
    <script type="text/javascript">
        function ShowPopWindow(Page) {
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
    <div id="PageSection1" style="text-align: left">
        <%-- <div style="text-align:center">
                <span class="PageHeading">Ink Cost Preferences</span>
            </div>--%>
        <br />
        <table cellspacing="10">
            <tr>
                <td style="width: 80%;" valign="top" colspan="2">
                    <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2" Style="margin-left: 10px;">
                    </asp:Table>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
