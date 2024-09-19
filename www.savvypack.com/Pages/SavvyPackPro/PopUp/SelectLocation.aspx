<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectLocation.aspx.vb"
    Inherits="SelectLocation" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<script type="text/JavaScript">
    javascript: window.history.forward(1);
    function ShowPopWindow(Page) {
        //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
        var width = 630;
        var height = 450;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=yes';
        params += ', status=yes';
        params += ', toolbar=no';
        newwin = window.open(Page, 'Chat2', params);
        if (newwin == null || typeof (newwin) == "undefined") {
            alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
        }

        return false;

    }

    function ShowPopWindow1(Page) {
        //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
        var width = 630;
        var height = 450;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=yes';
        params += ', status=yes';
        params += ', toolbar=no';
        newwin = window.open(Page, 'Chat1', params);
        if (newwin == null || typeof (newwin) == "undefined") {
            alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
        }

        return false;

    }
</script>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 400px; text-align: center;">
                        <asp:Label ID="lblHeading" Text="Add a location Information" runat="server"></asp:Label>
                    </div>
                    <div id="error">
                        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <table width="90%">
                                <tr class="AlterNateColor1" id="trAnalysis" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label3" runat="server" Text="Add new company" Font-Bold="true" ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <%--<asp:TextBox ID="TextBox4" ToolTip="Create an easy-to-remember name for your project"
                                        CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>--%>
                                        <asp:LinkButton ID="lnkState" runat="server" Text="Add company" Style="color: Black;"
                                            OnClientClick="return ShowPopWindow('AddLocationPopup.aspx');"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="OR"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" id="tr1" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label1" runat="server" Text="Select existing company" Font-Bold="true"
                                            ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="lnkCity" runat="server" Text="Select Company" Style="color: Black;"
                                            OnClientClick="return ShowPopWindow1('SelectExistingCompPopup.aspx');"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 15%">
                                    </td>
                                    <td style="width: 88%">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <%--<tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>--%>
        </table>
    </div>
    </form>
</body>
</html>
