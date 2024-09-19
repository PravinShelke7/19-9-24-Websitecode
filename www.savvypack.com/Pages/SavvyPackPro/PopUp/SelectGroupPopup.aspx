<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectGroupPopup.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_PopupGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group selection popup</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function UserSearch(GrpNm, GrpID) {
            var hdnMasterGrpId = document.getElementById('<%= hdnMasterGrpId.ClientID%>').value;            
            var hidSkuID = document.getElementById('<%= hidSkuID.ClientID%>').value;
            var hidGrpInnerText = document.getElementById('<%= hidGrpInnerText.ClientID%>').value;
            var hidGrpID = document.getElementById('<%= hidGrpID.ClientID%>').value;

            GrpNm = GrpNm.replace(new RegExp("&#", 'g'), "'");

            window.opener.document.getElementById(hidGrpInnerText).innerText = GrpNm;
            //alert(GrpID + "||" + hdnMasterGrpId + "||" + hidSkuID + "||" + hidGrpID);
            PageMethods.InsertUpdateSkuConn(GrpID, hdnMasterGrpId, hidSkuID, hidGrpID, onSucceedAll, onErrorAll);
            //window.close();
        }

        function onSucceedAll(result) {

            if (result) {
                window.opener.document.getElementById('tabSkuManager_tabGrpManager_btnSrchGM').click();
                window.close();
            }

        }

        function onErrorAll(result) {
            alert("Something went wrong!");
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            <table class="PageSection1" id="PageSection1" runat="server" style="margin-top: 15px; width: 600px;">
                <tr style="height: 20px;">
                    <td colspan="3" style="font-size: 17px; font-weight: bold; text-align: center; margin-bottom: 5px; width: 100%; height: 25px; left: 10px; text-align: center;">Select Group
                    </td>
                </tr>
                <tr>
                    <td style="padding-left: 10px;">
                        <table style="width: 100%;">
                            <tr>
                                <td>
                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox
                                        ID="txtKeyword" runat="server" CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button
                                            ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                </td>
                            </tr>
                            <tr>
                                <td>
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
                    </td>
                </tr>
                <tr id="Tr3" style="height: 20px" runat="server">
                    <td id="Td3" runat="server" colspan="2">
                        <div style="width: 820px; height: 390px; overflow: auto; margin-left: 10px;">
                            <asp:Label ID="lblGroupNoFound" runat="server" Text="No Record Found." Visible="false"></asp:Label>
                            <asp:GridView Width="800px" CssClass="grdProject" runat="server" ID="grdGroupDetails"
                                DataKeyNames="MASTERGID" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                Font-Size="11px" Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" EnableModelValidation="True">
                                <FooterStyle BackColor="#CCCC99" />
                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Group Name" SortExpression="GRPNM">
                                        <ItemTemplate>
                                            <a href="#" onclick="UserSearch('<%#Container.DataItem("GRPNM")%>','<%#Container.DataItem("GRPID")%>')"
                                                class="Link">
                                                <%# Container.DataItem("GRPNM")%></a>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Des" SortExpression="GRPDES">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUserID" runat="server" Visible="false" Text='<%# bind("USERID")%>'></asp:Label>
                                            <asp:Label ID="lblGrpDes" runat="server" Text='<%# Bind("GRPDES")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Master Group" SortExpression="MASTERGRPNM">
                                        <ItemTemplate>
                                            <asp:Label ID="lblT" runat="server" Text='<%# Bind("MASTERGRPNM")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="40px" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Owner" SortExpression="OWNER">
                                        <ItemTemplate>
                                            <asp:Label ID="lblOwner" runat="server" Text='<%# Bind("OWNER")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Creation Date" SortExpression="CREATIONDATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCD" runat="server" Text='<%# Bind("CREATIONDATE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="True" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Update Date" SortExpression="UPDATEDATE">
                                        <ItemTemplate>
                                            <asp:Label ID="lblUpD" runat="server" Text='<%# Bind("UPDATEDATE")%>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="True" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
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
        <asp:HiddenField ID="hdnMasterGrpId" runat="server" />       
        <asp:HiddenField ID="hidSkuID" runat="server" />
        <asp:HiddenField ID="hidSortIdMGroup" runat="server" />
        <asp:HiddenField ID="hidGrpInnerText" runat="server" />
        <asp:HiddenField ID="hidGrpID" runat="server" />
    </form>
</body>
</html>
