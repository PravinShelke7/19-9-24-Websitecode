<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Buyer.aspx.vb" Inherits="Buyer" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <title>Buyer Manager</title>
</head>
<style type="text/css">
    .Initial
    {
        display: block;
        padding: 4px 18px 4px 18px;
        float: left;
        background-color: #C0C9E7;
        color: Black;
        font-weight: bold;
    }
    .Initial:hover
    {
        color: Gray;
        background: url("../Images/SelectedButton.png") no-repeat right top;
    }
    .Clicked
    {
        float: left;
        display: block;
        background-color: #D3E7CB;
        padding: 4px 18px 4px 18px;
        color: White;
        font-weight: bold;
        color: Black;
    }
</style>
<script type="text/JavaScript">
    javascript: window.history.forward(1);
    function ShowPopWindow(Page) {
        //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
        var width = 550;
        var height = 150;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=yes';
        params += ', status=yes';
        params += ', toolbar=no';
        newwin = window.open(Page, 'Chat', params);
        if (newwin == null || typeof (newwin) == "undefined") {
            alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
        }

        return false;

    }
</script>
<body style="width: 1350px">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <div id="Div1">
        <asp:Image ImageAlign="AbsMiddle" Width="850px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPro.gif"
            runat="server" />
    </div>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" style="width: 840px;">
                    Buyer Manager
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left;">
                        <br />
                        <table width="98%" style="text-align: left;">
                            <tr>
                                <td>
                                    <asp:LinkButton ID="lnkAdd" runat="server" Text="Add a location" Style="color: Black;"
                                        OnClientClick="return ShowPopWindow('Popup/SelectLocation.aspx');"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <asp:GridView Width="1700px" CssClass="grdProject" runat="server" ID="grdUserInfo"
                                    DataKeyNames="" AllowPaging="True" PageSize="100" AllowSorting="True" AutoGenerateColumns="False"
                                    Font-Size="11px" Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    EnableModelValidation="True">
                                    <FooterStyle BackColor="#CCCC99" />
                                    <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </tr>
                            <tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
        <%--  <tr>
            <td>
                <asp:LinkButton ID="lnkAdd" runat="server" Text="Add a location" Style="color: Black;"
                    OnClientClick="return ShowPopWindow('SelectLocation.aspx');"></asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView Width="1700px" CssClass="grdProject" runat="server" ID="grdUserInfo"
                    DataKeyNames="" AllowPaging="True" PageSize="100" AllowSorting="True" AutoGenerateColumns="False"
                    Font-Size="11px" Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    EnableModelValidation="True">
                    <FooterStyle BackColor="#CCCC99" />
                    <RowStyle BackColor="#F7F7DE" CssClass="row" />
                    <Columns>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>--%>
    </table>
    </form>
</body>
</html>
