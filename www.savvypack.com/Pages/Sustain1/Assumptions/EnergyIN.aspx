<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain1.master" AutoEventWireup="false" CodeFile="EnergyIN.aspx.vb" Inherits="Pages_Sustain1_Assumptions_EnergyIN" Title="S1-Energy Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/S1Comman.js"></script>
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

        function ShowBulkMMPopWindow(Page) {
            document.getElementById("ctl00_Sustain1ContentPlaceHolder_lnkSelBulkModel").style.display = "none";
            var SCaseID = "<%=Session("S1CaseId")%>";
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
            Page = Page + "?PageNm=Energy+Assumptions&SCaseID=" + SCaseID;
            newwin = window.open(Page, 'BulkTool', '');

            return false;

        }
    </script>
     <style type="text/css">
        .divUpdateprogress {
            left: 410px;
            top: 200px;
            position: absolute;
        }
    </style>
    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <div style="text-align: left; vertical-align: top;">
                <asp:LinkButton ID="lnkSelBulkModel" runat="server" Text="Bulk Transfer" Visible="false" OnClientClick="return ShowBulkMMPopWindow('../BulkModelManagement.aspx');"></asp:LinkButton>
            </div>
            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
            <br />
            <table cellspacing="10">
                <tr>
                    <td style="width: 30%" valign="top" colspan="2">
                        <a href="AdditionalEnergyInfo.aspx" class="Link" target="_blank" style="font-weight: bold;">Addtional Energy Assumptions</a>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" valign="top">
                        <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                    </td>
                    <td valign="top">
                        <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" valign="top" colspan="2">
                        <asp:Table ID="tblComparision2" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                    </td>

                </tr>
                <tr>
                    <td style="width: 30%" valign="top" colspan="2">
                        <asp:Table ID="tblComparision3" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                    </td>

                </tr>
                <tr>
                    <td style="width: 30%" valign="top" colspan="2">
                        <asp:Table ID="tblComparision4" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                    </td>

                </tr>
            </table>
            <br />
        </div>
        <asp:Button ID="btnUpdateBulk" runat="server" Style="display: none;" />
        <asp:Button ID="btnUpdateBulk1" runat="server" Style="display: none;" />
    </div>
</asp:Content>

