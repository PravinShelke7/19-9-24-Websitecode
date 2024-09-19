<%@ Page Language="VB" MasterPageFile="~/Masters/MoldSustain1.master" AutoEventWireup="false"
    CodeFile="MatSources.aspx.vb" Inherits="Pages_MoldSustain1_Assumptions_MatSources"
    Title="S1 Mold-Materials And Sources" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldSustain1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="PageSection1" style="text-align: left">
        <br />
        <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
        </asp:Table>
    </div>
</asp:Content>
