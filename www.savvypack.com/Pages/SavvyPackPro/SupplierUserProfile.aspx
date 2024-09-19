<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SupplierUserProfile.aspx.vb"
    Inherits="Pages_SavvyPackPro_SupplierUserProfile" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Suppliers User Profile</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        tr.row
        {
            background-color: #fff;
        }
        
        tr.row td
        {
        }
        
        tr.row:hover td, tr.row.over td
        {
            background-color: #eee;
        }
    </style>
    <style type="text/css">
        a.SavvyLink:link
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            color: Red;
            font-size: 11px;
        }
        
        .PSavvyModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background: url('../../Images/AdminHeader.gif') no-repeat;
            height: 45px;
            width: 1220px;
            text-align: center;
            vertical-align: middle;
        }
        
        .PageHeading
        {
            font-size: 18px;
            font-weight: bold;
        }
        
        .ContentPage
        {
            margin-top: 2px;
            margin-left: 1px;
            width: 1220px;
            background-color: #F1F1F2;
        }
        
        #SavvyPageSection1
        {
            background-color: #D3E7CB;
        }
        
        .AlterNateColor3
        {
            background-color: #D3DAD0;
            height: 20px;
            width: 1020px;
        }
        
        .PageSHeading
        {
            font-size: 12px;
            font-weight: bold;
        }
        
        .InsUpdMsg
        {
            font-family: Verdana;
            font-size: 12px;
            font-style: italic;
            color: Red;
            font-weight: bold;
        }
        
        .LongTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 320px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            background-color: #FEFCA1;
        }
        
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <div id="AlliedLogo">
        <asp:Image ImageAlign="AbsMiddle" Width="1020px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
            runat="server" />
    </div>
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <ajaxToolkit:TabContainer ID="tabBuyerManager" Height="750px" Width="1020px" runat="server" ActiveTabIndex="0"
        AutoPostBack="true">
        <ajaxToolkit:TabPanel runat="server" HeaderText="User Profile" ToolTip="User Profile"
            ID="tabUserProfile">
            <ContentTemplate>
                <asp:Panel ID="pnlUserProfile" runat="server">
                    <table class="ContentPage" id="tblUP" runat="server" style="margin-top: 15px;width:980px;">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" runat="server">
                                <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                                    Supplier User Profile
                                </div>
                            </td>
                        </tr>
                        <tr id="Tr2" style="height: 20px" runat="server">
                            <td id="Td2" runat="server">
                                <div id="ContentPagemargin" runat="server">
                                    <div id="PageSection1" style="text-align: left; height: 500px; width: 980px;">
                                        <br />
                                        <table width="98%" style="text-align: left;">
                                            <tr class="AlterNateColor1">
                                                <td style="width: 200px;">
                                                    <asp:Label ID="lblFirstname" runat="server" Text="First Name:" Font-Bold="True" Font-Size="12px"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblFname" runat="server" Font-Bold="True" Font-Size="12px" Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblLastName" runat="server" class="Link" Font-Size="12px" Text="Last Name:"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblLname" runat="server" class="Link" Font-Bold="True" Font-Size="12px"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblEmailAdr" runat="server" class="Link" Text="Email Address:" Font-Bold="True"
                                                        Font-Size="12px"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblEAdrs" runat="server" class="Link" Font-Bold="True" Font-Size="12px"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblPhoneN" runat="server" class="Link" Text="Phone Number:" Font-Bold="True"
                                                        Font-Size="12px"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblPN" runat="server" class="Link" Font-Bold="True" Font-Size="12px"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblMobileN" runat="server" class="Link" Text="Mobile Number:" Font-Bold="True"
                                                        Font-Size="12px"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblMN" runat="server" class="Link" Font-Bold="True" Font-Size="12px"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblStreetAdrs" runat="server" Font-Size="12px" class="Link" Text="Street Address:"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblSaddr" runat="server" class="Link" Font-Size="12px" Font-Bold="True"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblCountry" runat="server" class="Link" Font-Size="12px" Text="Country:"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblCtry" runat="server" class="Link" Font-Size="12px" Font-Bold="True"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblState" runat="server" Font-Size="12px" class="Link" Text="State:"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblS" runat="server" class="Link" Font-Size="12px" Font-Bold="True"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblCity" runat="server" class="Link" Font-Size="12px" Text="City:"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblC" runat="server" class="Link" Font-Size="12px" Font-Bold="True"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td>
                                                    <asp:Label ID="lblZipC" runat="server" class="Link" Font-Size="12px" Text="Zip Code:"
                                                        Font-Bold="True"></asp:Label>
                                                </td>
                                                <td style="width: 2137px;">
                                                    <asp:Label ID="lblZC" runat="server" class="Link" Font-Size="12px" Font-Bold="True"
                                                        Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Transfer Ownership" ToolTip="Transfer Ownership"
            ID="tabtabTOwnership">
            <ContentTemplate>
                <asp:Panel ID="tabTOwnership" runat="server">
                    <table class="ContentPage" id="tblTO" runat="server" style="margin-top: 15px;">
                        <tr id="Tr3" runat="server">
                            <td id="Td3" runat="server">
                                <div class="PageHeading" id="div1" runat="server" style="text-align: center;">
                                    Transferring Ownership
                                </div>
                            </td>
                        </tr>
                        <tr id="Tr4" runat="server" class="AlterNateColor3">
                            <td id="Td4" runat="server" align="center" class="PageSHeading">
                                <asp:Label ID="Label5" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Sharing Access" ToolTip="Structure Manager"
            ID="tabSAccess">
            <ContentTemplate>
                <asp:Panel ID="pnltabSAccess" runat="server">
                    <table class="ContentPage" id="tbltabSAccess" runat="server" style="margin-top: 15px;">
                        <tr>
                            <td>
                                <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                                    Sharing Access
                                </div>
                            </td>
                        </tr>
                        <tr class="AlterNateColor3">
                            <td class="PageSHeading" align="center">
                                <asp:Label ID="Label1" runat="Server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <div class="AlterNateColor3">
        <div class="PageSHeading" align="center" width="1020px">
            <asp:Label ID="lblFooter" runat="Server"></asp:Label>
        </div>
    </div>
    <asp:HiddenField ID="hidSortIdSMPM" runat="server" />
    <asp:HiddenField ID="hidSortIdSpec" runat="server" />
    <asp:HiddenField ID="hidPMGrpId" runat="server" />
    <asp:HiddenField ID="hidPMGrpNm" runat="server" />
    <asp:HiddenField ID="hidRowNum" runat="server" Value="2" />
    <asp:HiddenField ID="hidEqId" runat="server" />
    </form>
</body>
</html>
