<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SpecManager.aspx.vb" Inherits="Pages_SavvyPackPro_SpecManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Specification Manager</title>
     <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="AlliedLogo">
           <asp:Image ImageAlign="AbsMiddle" Width="1000px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPro.gif"
            runat="server" />
        </div>
        <table class="ContentPage" id="tblSB" runat="server" style="margin-top: 15px;">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                       Specification Manager
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div style="text-align: left; background-color: #D3E7CB;">
                        <table style="width: 100%">
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width:830px;">
                                               
                                            </td>
                                            <td>
                                                <asp:Label ID="lblSelGrp" runat="server" Text="Group Selector:" Style="margin-left: 10px;
                                                    font-size: 12px;" Font-Bold="true"></asp:Label>
                                                <asp:LinkButton ID="lnkSelGrp" runat="server" CssClass="SavvyLink" Text="Select Group"
                                                    OnClientClick="return ShowGrpSelPopUp('PopUp/GrpSelector.aspx?');" Style="margin-left: 6px;"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr id="trpagecountSMProposal" runat="server" visible="false">
                                <td style="font-size: 12px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-left: 10px;">
                                    <div style="overflow: auto; height: 460px;">
                                        <asp:Label ID="lblMsgRFPManager" runat="server" Visible="false" Text="No records found."></asp:Label>
                                        <asp:GridView Width="1120px" CssClass="grdProject" runat="server" ID="grdPpslMngr"
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
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblFooter" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
