<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MasterGrpPopup.aspx.vb"
    Inherits="Pages_SavvyPackPro_Popup_MasterGrpPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Master Group</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table style="width: 100%; font-family: Verdana; font-size: 14px;">
            <tr valign="top" style="background-color: #dfe8ed;">
                <td>
                    <div class="PageHeading" id="div1" runat="server" style="text-align: center; height: 30px;">
                        Create Master Group
                    </div>
                    <table style="width: 100%;">
                        <tr id="trCreate" runat="server">
                            <td id="Td1" runat="server">
                                <table style="width: 97%; background-color: #7F9DB9; margin-left: 10px;">
                                    <tr>
                                        <td style="width: 30%;">
                                            <b>Master Group Name:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMGNm" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                width: 250px" MaxLength="25" onchange=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 30%;">
                                            <b>Description:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox TextMode="MultiLine" ID="txtMGDes" runat="server" CssClass="MediumTextBox"
                                                Style="text-align: left; height: 40px; width: 380px" onchange=""></asp:TextBox>
                                        </td>
                                    </tr>
                                    <%--<tr>
                                                                    <td style="width: 30%;">
                                                                        <b>Select Type:</b>
                                                                    </td>
                                                                    <td>
                                                                       <asp:DropDownList ID="ddlMType" CssClass="DropDown" Width="120px" runat="server">
                                                                       </asp:DropDownList>
                                                                    </td>
                                                                </tr>--%>
                                    <tr>
                                        <td style="width: 30%;">
                                        </td>
                                        <td>
                                            <asp:Button ID="btnCreateRFP" runat="server" Text="Create " CssClass="ButtonWMarigin"
                                                /><asp:Button ID="btnEditRFP" runat="server"
                                                    Text="Update " Visible="False" CssClass="ButtonWMarigin"  /><asp:Button
                                                        ID="btnCancel" runat="server" Text="Cancel" Visible="False" CssClass="ButtonWMarigin" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px;">
                                <table>
                                    <tr>
                                        <td>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox
                                                            ID="txtKey" runat="server" CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button
                                                                ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right;
                                                            font-size: 12px;"></asp:Label><asp:DropDownList ID="ddlPageCountC" runat="server"
                                                                Width="55px" CssClass="DropDown" AutoPostBack="True">
                                                                <asp:ListItem Value="1">10</asp:ListItem>
                                                                <asp:ListItem Value="2">25</asp:ListItem>
                                                                <asp:ListItem Value="3">50</asp:ListItem>
                                                                <asp:ListItem Value="4">100</asp:ListItem>
                                                                <asp:ListItem Value="5">200</asp:ListItem>
                                                                <asp:ListItem Value="6">300</asp:ListItem>
                                                                <asp:ListItem Value="7">400</asp:ListItem>
                                                                <asp:ListItem Value="8">500</asp:ListItem>
                                                                <asp:ListItem Value="9">1000</asp:ListItem>
                                                            </asp:DropDownList>
                                                        <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right;
                                                            padding-left: 20px;"></asp:Label><asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel"
                                                                ForeColor="Red" Font-Bold="True"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView Width="700px" CssClass="grdProject" runat="server" ID="grdMGroup" DataKeyNames="MGROUPID"
                                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateSelectButton="true"
                                                Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                Font-Names="Verdana" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="USERID" Visible="False" SortExpression="USERID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserID" Width="50px" runat="server" Text='<%# bind("USERID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Master Group Name" SortExpression="MGROUPNAME">
                                                        <ItemTemplate>
                                                            <%--  <a href="#" onclick="MasterGrpSearch('<%#Container.DataItem("MGROUPID")%>','<%#Container.DataItem("DESPOS")%>','<%#Container.DataItem("MTYPEID")%>')"
                                                            class="SavvyLink">
                                                            <%# Container.DataItem("MGROUPNAME")%>
                                                        </a>--%>
                                                            <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("MGROUPNAME")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" Width="140px" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Description" SortExpression="DESCRIPTION2">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblDesc2" runat="server" Text='<%# Bind("DESCRIPTION2")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="200px" Wrap="True" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Type" SortExpression="MTYPEID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTYPE" runat="server" Text='<%# Bind("MTYPEDESC")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="160px" Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Owner" SortExpression="OWNER">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblOwner" runat="server" Text='<%# Bind("OWNER")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="160px" Wrap="True" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" />
                                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <PagerSettings Position="Top" />
                                                <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                                    HorizontalAlign="Left" />
                                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidSortIdBSpec" runat="server" />
    <asp:HiddenField ID="hidMID" runat="server" />
    </form>
</body>
</html>
