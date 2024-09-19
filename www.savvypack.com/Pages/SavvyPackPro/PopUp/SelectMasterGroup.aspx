<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectMasterGroup.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_SelectMasterGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Master Group</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function MasterGrpSearch(Id, Name, MTypeID) {
            var hdnMGrpID = document.getElementById('<%= hdnMGrpID.ClientID%>').value;
            var hdnMgrpDes = document.getElementById('<%= hdnMgrpDes.ClientID%>').value;
            var hdnInnerLink = document.getElementById('<%= hdnInnerLink.ClientID%>').value;

            Name = Name.replace(new RegExp("&#", 'g'), "'");

            window.opener.document.getElementById(hdnMGrpID).value = Id;
            window.opener.document.getElementById(hdnInnerLink).innerText = Name;
            window.opener.document.getElementById(hdnMgrpDes).value = Name;          
            window.opener.document.getElementById('btnMasterSel').click();
            window.close();

        }
    </script>
    <style type="text/css">
        .LongTextBox {
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

        .style1 {
            width: 100%;
        }

        tr.row {
            background-color: #fff;
        }

            tr.row td {
            }

            tr.row:hover td, tr.row.over td {
                background-color: #eee;
            }

        a.SavvyLink:link {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:visited {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:hover {
            color: Red;
            font-size: 11px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div style="text-align: left; background-color: #D3E7CB;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            <table>
                <tr>
                    <td>
                        <table style="width: 100%;">
                            <tr style="height: 20px;">
                                <td id="Td2" runat="server">
                                    <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                                        Select Master Group
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top:10px;">
                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox
                                        ID="txtKey" runat="server" CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button
                                            ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding-top:10px;">
                                    <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right; font-size: 12px;"></asp:Label><asp:DropDownList ID="ddlPageCountC" runat="server"
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
                                    <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right; padding-left: 20px;"></asp:Label><asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel"
                                        ForeColor="Red" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <table class="style1">
                            <tr>
                                <td>
                                    <div style="width: 820px; height: 390px; overflow: auto; margin-left: 10px;">
                                        <asp:Label ID="lblMGroupNoFound" runat="server" Visible="False" Text="No records found."></asp:Label><asp:GridView
                                            Width="700px" CssClass="grdProject" runat="server" ID="grdMGroup" DataKeyNames="MGROUPID"
                                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                                            Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" Font-Names="Verdana"
                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                                                        <a href="#" onclick="MasterGrpSearch('<%#Container.DataItem("MGROUPID")%>','<%#Container.DataItem("DESPOS")%>','<%#Container.DataItem("MTYPEID")%>')"
                                                            class="SavvyLink">
                                                            <%# Container.DataItem("MGROUPNAME")%>
                                                        </a>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="140px" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" SortExpression="DESCRIPTION2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDesc" runat="server" Text='<%# Bind("DESCRIPTION2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="200px" Wrap="True" HorizontalAlign="Left" />
                                                </asp:TemplateField>                                               
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
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hdnMGrpID" runat="server" />
        <asp:HiddenField ID="hdnMgrpDes" runat="server" />
        <asp:HiddenField ID="hdnInnerLink" runat="server" />
        <asp:HiddenField ID="hidSortIdMGroup" runat="server" />
    </form>
</body>
</html>
