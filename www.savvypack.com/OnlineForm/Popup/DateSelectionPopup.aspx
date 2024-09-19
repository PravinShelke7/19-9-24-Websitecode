<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateSelectionPopup.aspx.vb"
    Inherits="OnlineForm_Popup_DateSelectionPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
<head runat="server">
    <title>Date Selection</title>
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
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
                    <div id="PageSection1" style="text-align: center">
                        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                        <div class="PageHeading" id="divMainHeading" style="width: 220px; margin-left :20px; text-align: center">
                            <asp:Label ID="Label1" Text="Date Selection" Font-Size="12" runat="server"></asp:Label>
                        </div>
                        <br />
                        <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 10px;
                            margin-right: 10px; height: 145px; overflow: auto;">
                            <asp:Table ID="tblDate" runat="server" Width="250px">
                            </asp:Table>
                        </div>
                        <br />
                        <%--<table style="margin-left: 160px;" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="150px" />
                                </td>
                            </tr>
                        </table>--%>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidProjectId" runat="server" />
    </form>
</body>
</html>
