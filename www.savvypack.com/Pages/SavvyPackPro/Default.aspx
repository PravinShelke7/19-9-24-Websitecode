<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Emonitor_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Buyer and Supplier Manager</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

    </script>
    <style type="text/css">
        .ULoginModuleSM
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url(../../Images/SavvyPackPRO.gif);
            height: 44px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        
        a.disabled
        {
            color: currentColor;
            cursor: not-allowed;
            opacity: 0.5;
            text-decoration: none;
        }
    </style>

   
    <script type="text/JavaScript">

        function RefreshPage() {
            window.location.reload();
        }

      
    </script>














</head>
<body>
    <form id="form1" runat="server">
    <%--<div id="ContentPagemargin1" runat="server" style="vertical-align: top; margin-left: 100px;
        margin-right: 100px;">--%>
        <center >
    <div id="MasterContent">
        <div id="AlliedLogo">
            <table class="ULoginModuleSM" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageUrl="~/Images/LogOff.gif" runat="server" ToolTip="Log Off"
                                        Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=IContract" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" style="width: 840px;">
                    Buyer and Supplier Manager
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left; height: 550px;">
                        <br />
                        <table width="98%" style="text-align: left;">
                            <tr class="PageSSHeading" style="height: 20px;">
                                <td class="TdHeading" style="width: 80px" colspan="2">
                                </td>
                            </tr>
                            <tr class="AlterNateColor2">
                                <td class="StaticTdLeft" style="width: 80px">
                                    <a href="RFPManager.aspx" class="Link" target="_blank">Buyer</a>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkBUser" runat="server" href="BuyerUserProfile.aspx" Text="User Profile"
                                        class="Link" target="_blank"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="display:none;">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnlManageSpecs" runat="server" href="ManageSpecs.aspx" Text="Tech Manager"
                                        class="Link" target="_blank"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkRFPManager" runat="server" href="CreateRFP.aspx" Text="Create RFP/RFI"
                                        target="_blank"></asp:LinkButton>
                                        <%-- Enabled="True" class="disabled"--%>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkSkuManager" runat="server" href="SkuManager.aspx" Text="SKU Manager"
                                        Enabled="true" class="disabled" target="_blank" ></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkConfRFP" runat="server" href="RFPManager.aspx" Text="Configure RFP/RFI"
                                        target="_blank" class="disabled" Enabled="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkIssueRFP" runat="server" href="IssueRFP.aspx" Text="Issue RFP/RFI"
                                        target="_blank" class="disabled" Enabled="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkRFPStatus" runat="server" href="CheckRFP_Status.aspx" Text="Check RFP/RFI Status"
                                        target="_blank" class="disabled" Enabled="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkAProposal" runat="server" href="AnalyzeProposals.aspx" Text="Analyze Proposals"
                                        target="_blank" class="disabled" Enabled="false"></asp:LinkButton>
                                </td>
                            </tr>
                            <%--<tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkContract" runat="server" href="Contract.aspx" Text="Contract"
                                        target="_blank" class="Link" Enabled="false"></asp:LinkButton>
                                </td>
                            </tr>--%>
                            <tr class="AlterNateColor2">
                                <td class="StaticTdLeft" style="width: 80px">
                                    <a href="SupplierDefault.aspx" class="Link" target="_blank">Supplier</a>
                                </td>
                                <td>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkSUser" runat="server" href="SupplierUserProfile.aspx" Text="User Profile"
                                        class="Link" target="_blank"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkSConfigRFP" runat="server" href="ConfigureRFP_Supplier.aspx"
                                        Text="Configure RFP/RFI" target="_blank" class="disabled"  Enabled="false"></asp:LinkButton>

                                </td>
                            </tr>
                            <%--<tr class="AlterNateColor1">
                                    <td class="StaticTdLeft" style="width: 80px"></td>
                                    <td>
                                        <asp:LinkButton ID="lnkCompPro" runat="server" href="" Text="Analyze Proposal" target="_blank"
                                            class="disabled" Enabled="false"></asp:LinkButton>
                                    </td>
                                </tr>--%>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkSubPro" runat="server" href="SubmitProposal.aspx" Text="Submit Proposal"
                                        target="_blank"  class="disabled" Enabled="false" ></asp:LinkButton>



                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="StaticTdLeft" style="width: 80px">
                                </td>
                                <td>
                                    <asp:LinkButton ID="lnkanaProp" runat="server" href="AnalyzeProposal_Supplier.aspx" Text="Analyze Proposal"
                                        target="_blank"  class="disabled"  Enabled="false"></asp:LinkButton>
                                </td>
                            </tr>

                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </center>
    
    
    </form>

         
</body>
</html>
