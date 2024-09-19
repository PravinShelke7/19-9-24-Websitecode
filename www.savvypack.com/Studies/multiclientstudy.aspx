<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="multiclientstudy.aspx.vb" Inherits="Studies_multiclientstudy" Title="Publication - Reports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">    
    <table id="tblStudy" runat="server" style="margin-left: 5px;">
        <tr>
            <td>
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" style="background-color: #000000; color: White; height: 22px;
                width: 100%; font-weight: bold; font-size: 16px; text-align: center; margin-top: 2px;">
                Multi-client Packaging Studies
            </td>
        </tr>
        <tr>
            <td style="font-weight: bold; color: #336699; font-family: Optima; font-size: 10.5pt;">
                <p style="width: 700px; margin-top: 0px">
                    SavvyPack Corporation researches, writes, publishes, and markets multi-client studies
                    for the packaging industry. You can investigate and order these packaging studies
                    on this web site or you can contact us:</p>
                <p>
                    Phone: [1] 952-405-7500</p>
                <span style="margin-top: 0px;">Email: <a href="mailto:sales@savvypack.com" class="LinkStudy"
                    style="font-size: 14px">sales@savvypack.com</a> </span>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2" Width="99%">
                </asp:Table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnUserLoginPath" runat="server" />
</asp:Content>
