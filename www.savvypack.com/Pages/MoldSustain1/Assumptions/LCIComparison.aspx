<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LCIComparison.aspx.vb" Inherits="Pages_MoldSustain1_Assumptions_LCIComparison" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>LCI Comparison</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .S1MoldModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../../Images/S1MoldHeader.gif');
            height: 44px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/JavaScript">



        function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, 'LciNewWindow', params);

        }
    </script>
    <script language="javascript" type="text/javascript">
        function CountItems() {
            var lb = document.getElementById("lstLCI");
            var count = 0;
            for (var i = 0; i < lb.options.length; i++) {
                if (lb.options[i].selected) {
                    count++;
                }
            }
            if (count > 0)
                return true;
            else
                alert("Please Select at Least one LCI data!!");
            return false;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
       <tr>
                <td class="PageSHeading" align="center">
                    <table style="width: 845px; background-color: #edf0f4;">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        <div>
            <table class="S1MoldModule" id="S1Table" runat="server" cellpadding="0" cellspacing="0"
                style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <br />
                            <table width="80%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Select an effective date for the datasets you want to compare:
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlEffdate" CssClass="DropDown" Width="50%" runat="server"
                                            AppendDataBoundItems="true">
                                            <asp:ListItem Text="Latest Date" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="80%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Create a comparison in tabular form based on:
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlType" CssClass="DropDown" Width="40%" runat="server">
                                            <asp:ListItem Text="Energy" Value="Energy"></asp:ListItem>
                                            <asp:ListItem Text="Green House Gas" Value="GHG"></asp:ListItem>
                                            <asp:ListItem Text="Water" Value="WATER"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="80%" cellpadding="0" cellspacing="0">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;" colspan="2" on>
                                        Select the LCI datasets you want to compare:
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 210px; padding-left: 5px;">
                                        <asp:ListBox CssClass="DropDown" ID="lstLCI" runat="server" SelectionMode="Multiple"
                                            Width="200px" Height="150px" EnableViewState="true"></asp:ListBox>
                                    </td>
                                    <td valign="top" style="padding-top: 5px;">
                                        <b>Press and hold the control button to select multiple datasets for comparison
                                        </b>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td colspan="2">
                                        <asp:Button ID="btnType" runat="server" Text="Display Comparison" CssClass="ButtonWMarigin"
                                            OnClientClick="return CountItems();" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
