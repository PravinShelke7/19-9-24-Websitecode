<%@ Page Language="VB" MasterPageFile="~/Masters/MoldSustain2.master" AutoEventWireup="false"
    CodeFile="GhgRes2.aspx.vb" Inherits="Pages_MoldSustain2_Results_GhgRes2" Title="S2 Mold-Greenhouse Gas Results2 Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldSustain2ContentPlaceHolder"
    runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/MoldS2Comman.js"></script>
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
            <table cellspacing="2" style="width: 700px">
                <tr>
                    <td>
                        <asp:Label ID="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:Label ID="lblSalesVolUnit" runat="server" Style="font-family: Optima; font-size: 12px;
                            height: 12px; width: 100px; margin-right: 3px; margin-top: 2px; margin-bottom: 2px;
                            margin-left: 0px; text-align: right;"></asp:Label>
                    </td>
                    <td style="text-align: right;">
                        <asp:Label ID="lblNewSalesValue" runat="server" Style="font-family: Optima; font-size: 12px;
                            height: 12px; width: 100px; margin-right: 0px; margin-top: 2px; margin-bottom: 2px;
                            margin-left: 5px; text-align: right; font-weight: bold"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtNewSaleValue" runat="Server" CssClass="MediumTextBox" MaxLength="12"
                            Style="width: 84px;"></asp:TextBox>
                        <asp:DropDownList ID="ddlCustUnit" runat="server" CssClass="DropDownConT">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
        </div>
    </div>
</asp:Content>
