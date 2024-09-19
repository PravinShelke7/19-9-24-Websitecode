<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Structure.aspx.vb" Inherits="Pages_SavvyPackPro_Structure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Structure Manager</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 166px;
        }
        .style2
        {
            width: 835px;
        }
        .style3
        {
            font-size: 12px;
            font-weight: bold;
            width: 1135px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" Width="1135px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPro.gif"
                runat="server" />
        </div>
        <table class="ContentPage" id="tblSB" runat="server" style="margin-top: 15px;">
            <tr>
                <td class="style2">
                    <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                        Structure Manager
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td class="style2">
                    <div style="text-align: left; background-color: #D3E7CB;">
                        <table style="width: 100%">
                            <tr>
                                <td class="style1">
                                </td>
                                <td style=" padding-left :250px;">
                                    <asp:Label ID="lblSelGrp" runat="server" Text="Group Selector:" Style="margin-left: 10px;
                                        font-size: 12px;" Font-Bold="true"></asp:Label>
                                    <asp:LinkButton ID="lnkSelGrp" runat="server" CssClass="SavvyLink" Text="Select Group"
                                        OnClientClick="return ShowGrpSelPopUp('PopUp/GrpSelector.aspx?');" Style="margin-left: 6px;"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td class="style1">
                                    <b>SKU:</b><asp:Label ID="lblSKUId1" runat="server" CssClass="NormalLable"></asp:Label>
                                </td>
                                <td style="width: 350px; text-align: Left;" colspan="3">
                                    <b>SKU:</b><asp:Label ID="lblSKUId2" runat="server" CssClass="NormalLable"></asp:Label>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td class="style1">
                                    <span id="Span1" runat="server"><b>SKU Description:</b></span>
                                    <asp:Label ID="lblSKUDesc1" runat="server" CssClass="NormalLable"></asp:Label>
                                </td>
                                <td colspan="2">
                                    <span id="caseDe3" runat="server"><b>SKU Description:</b></span>
                                    <asp:Label ID="lblSKUDesc2" runat="server" CssClass="NormalLable"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;" colspan="2">
                                    <div style="overflow: auto; height: 800px;">
                                        <asp:Label ID="lblMsgRFPManager" runat="server" Visible="false" Text="No records found."></asp:Label>
                                        <asp:GridView Width="800px" CssClass="grdProject" runat="server" ID="grdPpslMngr"
                                            DataKeyNames="GROUPID" AllowPaging="True" PageSize="100" AllowSorting="True"
                                            AutoGenerateColumns="False" Font-Size="11px" Font-Names="Verdana" CellPadding="4"
                                            ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                                            BorderStyle="None" BorderWidth="1px">
                                            <PagerSettings Position="Top" />
                                            <PagerTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                    Style="color: Black;">First</asp:LinkButton>
                                                <asp:Label ID="pmore" runat="server" Text="..."></asp:Label>
                                                <asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;" CommandName="Page"
                                                    CommandArgument="Prev">Previous</asp:LinkButton>
                                                <asp:LinkButton ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:Label ID="CurrentPage" runat="server" Text="Label" Style="color: Red;"></asp:Label>
                                                <asp:LinkButton ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next"
                                                    Style="color: #284E98;">Next</asp:LinkButton>
                                                <asp:Label ID="nmore" runat="server" Text="..."></asp:Label>
                                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page" CommandArgument="Last"
                                                    Style="color: Black;">Last</asp:LinkButton>
                                            </PagerTemplate>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                            <Columns>
                                            </Columns>
                                            <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
                                                HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr class="AlterNateColor3">
                <td class="style3" align="center">
                    <asp:Label ID="lblFooter" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
