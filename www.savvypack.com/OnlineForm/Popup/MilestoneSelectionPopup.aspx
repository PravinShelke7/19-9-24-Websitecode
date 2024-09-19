<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MilestoneSelectionPopup.aspx.vb"
    Inherits="OnlineForm_Popup_MilestoneSelectionPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
<head id="Head1" runat="server">
    <title>Milestone Display Setting</title>
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
                        <div class="PageHeading" id="divMainHeading" style="width: 425px; background-color: #F1F1F2;
                            text-align: center; height: 22px;">
                            <asp:Label ID="lblS" Text="Milestone Display Setting" Font-Size="13" runat="server">
                            </asp:Label>
                        </div>
                        <br />
                        <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: left;
                             margin-left: 88px; margin-right: 10px; height: 17px; Width:245px; overflow: auto;">
                            <asp:Label ID="Label3" runat="server" style="text-align: left; margin-left: 5px;"
                                Font-Size="11" Text="Sort based on:"></asp:Label>
                        </div>
                       <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 85px;
                            margin-right: 10px; height: 120px; overflow: auto; margin-top: 0px;">
                            <asp:Table ID="tbldate" runat="server" Width="250px" style="margin-top: 0px;">
                            </asp:Table>
                        </div>
                        <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: left;
                            margin-left: 88px; margin-right: 10px; height: 17px; overflow: auto; width :245px;">
                            <asp:Label ID="Label2" runat="server" Font-Size="11" style="margin-left: 5px;" Text="Sort by:">
                            </asp:Label><br />
                        </div>
                        
                        <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 85px;
                            margin-right:88px; height: 90px; overflow: auto;">
                             <asp:Table ID="tblDateSelection" runat="server" Width="250px">
                            </asp:Table>
                           
                        </div>
                        
                        <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; text-align: left;
                            margin-left: 88px; margin-right: 10px; height: 17px; overflow: auto; width:245px;">
                            <asp:Label ID="lbl" runat="server" Font-Size="11" style="margin-left: 5px;" Text="Select milestone type:">
                            </asp:Label><br />
                        </div>
                       
                        <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 85px;
                            margin-right: 10px; height: 120px; overflow: auto;">
                            <asp:Table ID="tblDateDisplay" runat="server" Width="250px">
                            </asp:Table>
                        </div>
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="65px" />
                        <%--<table style="margin-left: 40px;" align="center">
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="65px" />
                                </td>
                            </tr>
                        </table>--%>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidSortVal" runat="server" />
    <asp:HiddenField ID="hidSortValDate" runat="server" />
    <asp:HiddenField ID="hidSortValDate1" runat="server" />
    </form>
</body>
</html>
