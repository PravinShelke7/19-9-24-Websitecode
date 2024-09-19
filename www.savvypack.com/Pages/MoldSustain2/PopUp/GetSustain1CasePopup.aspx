<%@ Page Language="VB" MasterPageFile="~/Masters/MoldS2PopUp.master" AutoEventWireup="false"
    CodeFile="GetSustain1CasePopup.aspx.vb" Inherits="Pages_MoldSustain2_PopUp_GetSustain1CasePopup"
    Title="Sustain1 Mold Cases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldContentPlaceHolder1" runat="Server">
    <script type="text/JavaScript">
        function CaseDes(MatDes, MatId) {
            var hidCasedes = document.getElementById('<%= hidCasedes.ClientID%>').value
            var hidCaseid = document.getElementById('<%= hidCaseid.ClientID%>').value
            //alert(MatDes.length);
            window.opener.document.getElementById(hidCasedes).innerText = MatDes
            window.opener.document.getElementById(hidCaseid).value = MatId
            window.close();
        }
    </script>
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Case De1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Case De2:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                    </td>
                </tr>
            </table>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 400px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td>
                        Case Des1
                    </td>
                    <td>
                        Case Des2
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 230px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdCases" DataKeyNames="CaseId" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="CaseId" HeaderText="CaseId" SortExpression="CaseId" Visible="false">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Case Des1" SortExpression="Casede1">
                            <ItemTemplate>
                                <a href="#" onclick="CaseDes('<%#Container.DataItem("CASDES")%>','<%#Container.DataItem("CaseId")%>')"
                                    class="Link">
                                    <%#Container.DataItem("CaseId")%>:<%#Container.DataItem("Casede1")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="50%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="Casede2" HeaderText="Case Des2" SortExpression="Casede2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidCasedes" runat="server" />
        <asp:HiddenField ID="hidCaseid" runat="server" />
    </div>
</asp:Content>
