<%@ Page Title="E2 Mold-Profit and Loss Statement with Depreciation " Language="VB"
    MasterPageFile="~/Masters/MoldEcon2.master" AutoEventWireup="false" CodeFile="ResultsPLwithDEP.aspx.vb"
    Inherits="Pages_MoldEcon2_Results_ResultsPLwithDEP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldEcon2ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/MoldE2Comman.js"></script>
    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <table style="width: 400px;">
                <tr>
                    <td>
                        <asp:Label ID="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
            </table>
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
            <asp:TextBox ID="txthiddien" Style="visibility: hidden;" runat="server" Text="0"></asp:TextBox>
        </div>
    </div>
    <asp:HiddenField ID="hdnVolume" runat="server" />
    <asp:HiddenField ID="hdnUnit" runat="server" />
</asp:Content>
