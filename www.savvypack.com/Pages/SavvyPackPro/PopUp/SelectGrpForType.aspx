<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectGrpForType.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_SelectGrpForType" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Group</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function GroupSearch() {
            window.opener.document.getElementById('btnRefreshCreate').click();
            window.close();
        }
    </script>
</head>
<body>
      <form id="form1" runat="server">
        <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
            <div id="PageSection1" style="text-align: left; width: 810px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>

                <table style="text-align: center;">
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                    <tr>
                        <td class="PageSHeading" style="font-size: 16px; width: 800px;">
                            <div style="width: 800px; text-align: center;">
                                Group Details   
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" Style="text-align: cente; font-size: 16px; font-weight: bold; color: Red;"> </asp:Label>
                        </td>
                    </tr>
                </table>
                <div style="height: 10px;"></div>
                <table cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                        </td>
                    </tr>
                </table>

                <div style="width: 800px; overflow: auto;">

                    <asp:GridView Width="780px" runat="server" ID="grdGrpCases" DataKeyNames="GROUPID" AutoGenerateSelectButton="false"
                        AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                        CellPadding="4" ForeColor="#333333" GridLines="None">
                        <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="AlterNateColor1" />
                        <AlternatingRowStyle CssClass="AlterNateColor2" />
                        <HeaderStyle Height="25px" BackColor="#6B696B" Font-Size="11px" Font-Bold="True" ForeColor="White" />

                        <Columns>
                            <asp:BoundField DataField="GROUPID" HeaderText="GROUPID" SortExpression="GROUPID" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="Group Name" HeaderStyle-HorizontalAlign="center" SortExpression="GROUPNAME" Visible="true"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkGrpId" runat="server" Width="150px"
                                        Style="color: Black; font-family: Verdana; font-size: 11px;" Text='<%# bind("CDES1")%>' OnClick="lnkGrpId_Click"></asp:LinkButton>                                    
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Width="160px" Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="GROUPNAME" HeaderText="Group Name" SortExpression="GROUPNAME" >
                                <ItemStyle Width="160px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                                <HeaderStyle HorizontalAlign="Left" Width="160px" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GROUPDES" HeaderText="Group Description" SortExpression="GROUPDES">
                                <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                                <HeaderStyle HorizontalAlign="Left" Width="250px" />
                            </asp:BoundField>

                            <asp:BoundField DataField="TypeID" HeaderText="ID(s)" SortExpression="TypeID">
                                <ItemStyle Width="210px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>

                            <asp:BoundField DataField="CREATIONDATE" HeaderText="Creation Date" SortExpression="CREATIONDATE">
                                <ItemStyle Width="80px" Wrap="true" CssClass="NormalLabel" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" SortExpression="UPDATEDATE">
                                <ItemStyle Width="80px" Wrap="true" CssClass="NormalLabel" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                </div>
                <br />
            </div>
            <asp:HiddenField ID="hidTypeId" runat="server" /> 
            <asp:HiddenField ID="hvCaseGrd" runat="server" />           
        </div>
    </form>
</body>
</html>
