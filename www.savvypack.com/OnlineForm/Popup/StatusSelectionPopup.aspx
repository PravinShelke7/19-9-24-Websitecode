<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatusSelectionPopup.aspx.vb"
    Inherits="OnlineForm_Popup_AdvancePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Status Display Setting.</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table class="SavvyContentPage" id="ContentPage" runat="server">
        <tr style="height: 20px">
            <td>
                <div id="SavvyContentPagemargin" style="width: 100%; margin: 0 0 0 0">
                    <div id="SavvyPageSection1" style="text-align: center">
                        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                        <div class="PageHeading" id="divMainHeading" style="width: 460px; background-color: #F1F1F2;
                            text-align: center; height: 22px;">
                            <asp:Label ID="lblSort" Text="Status Display Setting" Font-Size="13" runat="server"></asp:Label>
                        </div>
                        <br />
                        <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: left;
                            margin-left: 10px; margin-right: 10px; height: 17px;width: 438px; overflow: auto;">
                            <asp:Label ID="lblSelection" runat="server" style="text-align: left; margin-left: 5px;"
                                Font-Size="10" Text="Sort by Project Status: In Order/ In Reverse Order:"></asp:Label>
                        </div>
                        <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 10px;
                            margin-right: 10px; height: 90px; width: 440px; overflow: auto;">
                            <asp:Table ID="tblStatus" runat="server" Width="440px" style="margin-top: 0px;">
                            </asp:Table>
                        </div>
                        <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: left;
                            margin-left: 88px; margin-right: 10px; height: 17px; width: 245px; overflow: auto;">
                            <asp:Label ID="lblDisplay" runat="server" Font-Size="10" style="margin-left: 5px;"
                                Text="Select Status type:"></asp:Label><br />
                        </div>
                        <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 85px;
                            margin-right: 10px; height: 150px; overflow: auto;">
                            <asp:Table ID="tblselection" runat="server" style="margin-top: 0px;" Width="250px">
                            </asp:Table>
                        </div>
                        <br />
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="65px" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidSortVal" runat="server" />
    </form>
</body>
</html>
