<%@ Page Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="PersonnelIN.aspx.vb" Inherits="Pages_Econ1_Assumptions_PersonnelIN" Title="E1-Personnel Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
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

        function ShowBulkMMPopWindow(Page) {
            document.getElementById("ctl00_Econ1ContentPlaceHolder_lnkSelBulkModel").style.display = "none";
            var SCaseID = "<%=Session("E1CaseId")%>";
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
            Page = Page + "?PageNm=Personnel&SCaseID=" + SCaseID;
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
                <asp:LinkButton ID="lnkSelBulkModel" runat="server" Text="Bulk Transfer" Visible="false"
                    OnClientClick="return ShowBulkMMPopWindow('../BulkModelManagement.aspx');"></asp:LinkButton>
            </div>
            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
            <br />
            <table cellspacing="2" style="width: 230px">
                <tr>
                    <td>
                        <asp:Label ID="lblPrefRatio" Text="Preferred Ratio:" runat="server" Style="font-family: Optima; font-size: 12px; height: 12px; width: 100px; margin-right: 0px; margin-top: 2px; margin-bottom: 2px; margin-left: 5px; font-weight: bold;"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrefRatio" runat="Server" CssClass="MediumTextBox" Style="margin-right: 50px;" Text="0" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>

            </table>
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
            <br />
        </div>
        <asp:HiddenField ID="hdnCountry" runat="Server" />
        <asp:Button ID="btnUpdateBulk" runat="server" Style="display: none;" />
        <asp:Button ID="btnUpdateBulk1" runat="server" Style="display: none;" />
    </div>
</asp:Content>

