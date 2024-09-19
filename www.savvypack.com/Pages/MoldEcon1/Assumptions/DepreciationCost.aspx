<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DepreciationCost.aspx.vb"
    Inherits="Pages_MoldEcon1_Assumptions_DepreciationCost" MasterPageFile="~/Masters/MoldEcon1.master"
    Title="E1 Mold-Depreciation Assumption" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldEcon1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/javascript" src="../../../javascripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/MoldE1Comman.js"></script>
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
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
            <asp:Table ID="tblSupportEqui" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
            <div>
                <asp:Table ID="tblGTotal" runat="server" CellPadding="0" CellSpacing="2">
                </asp:Table>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hdnMaxCount" runat="server" />
        <asp:HiddenField ID="hidcnt" runat="server" />
        <asp:HiddenField ID="hidDeprettl" runat="server" />
    </div>
</asp:Content>
