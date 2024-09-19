<%@ Page Title="E2 Mold-Customer Profit and Loss Statement" Language="VB" MasterPageFile="~/Masters/MoldEcon2.master"
    AutoEventWireup="false" CodeFile="Resultspl2.aspx.vb" Inherits="Pages_MoldEcon2_Results_Resultspl2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldEcon2ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/MoldE2Comman.js"></script>
    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <table style="width: 750px;">
                <tr>
                    <td style="width: 200px;">
                        <asp:Label ID="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td style="width: 200px;">
                        <asp:Label ID="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td style="width: 150px; text-align: right">
                        <asp:Label ID="lblNewSalesValue" runat="server" Style="font-family: Optima; font-size: 12px;
                            width: 100px; text-align: right; font-weight: bold"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewSaleValue" runat="Server" CssClass="MediumTextBox" Style="width: 74px;"></asp:TextBox>
                        <asp:DropDownList ID="ddlCustUnit" runat="server" CssClass="DropDownConT">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
            <asp:TextBox ID="txthiddien" Style="visibility: hidden;" runat="server" Text="0"></asp:TextBox>
        </div>
        <asp:HiddenField ID="hdnVolume" runat="server" />
        <asp:HiddenField ID="hdnUnit" runat="server" />
    </div>
</asp:Content>
