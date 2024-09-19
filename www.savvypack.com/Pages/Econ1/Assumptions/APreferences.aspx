<%@ Page Title="E1-Additional Preferences" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false"
    CodeFile="APreferences.aspx.vb" Inherits="Pages_Econ1_Assumptions_APreferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript">
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
            Page = Page + "?PageNm=Additional+Preferences&SCaseID=" + SCaseID;
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
            <div style="text-align:left; vertical-align:top;">
                <asp:LinkButton ID="lnkSelBulkModel" runat="server" Text="Bulk Transfer" Visible="false" OnClientClick="return ShowBulkMMPopWindow('../BulkModelManagement.aspx');"></asp:LinkButton>
            </div>
            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
            <br />
            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Energy Calculations
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdErgyCalc" GroupName="Unit1" runat="server" Text="Adjust Automatically" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdErgyCalcA" GroupName="Unit1" runat="server" Text="Use Capacity" />
                    </td>
                </tr>

            </table>
            <br />
            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Design Waste Calculations
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdDesignOld" GroupName="DESIGNW" runat="server" Text="Old" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdDesignNew" GroupName="DESIGNW" runat="server" Text="New" />
                    </td>
                </tr>

            </table>
        </div>
        <asp:Button ID="btnUpdateBulk" runat="server" Style="display: none;" />
        <asp:Button ID="btnUpdateBulk1" runat="server" Style="display: none;" />
    </div>
</asp:Content>

