<%@ Page Language="VB" MasterPageFile="~/Masters/MoldS1PopUp.master" AutoEventWireup="false"
    CodeFile="GetProdcuts.aspx.vb" Inherits="Pages_MoldSustain1_PopUp_GetProdcuts" Title="S1 Mold-Get Products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldContentPlaceHolder1" runat="Server">
    <script type="text/JavaScript">
        function ProductDet(ProductDes, FromtId) {

            var hypProdDes = document.getElementById('<%= hypProdDes.ClientID%>').value;
            var hidProdid = document.getElementById('<%= hidProdid.ClientID%>').value;
            var hidBtn = document.getElementById('<%= hidButton.ClientID%>').value;
            //alert(ProductDes);
            window.opener.document.getElementById(hypProdDes).innerText = ProductDes;
            window.opener.document.getElementById(hidProdid).value = FromtId
            window.opener.document.getElementById(hidBtn).click();
            window.close();
        }
    </script>
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Product De1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProdDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Product De2:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtProdDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
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
                        Product Des1
                    </td>
                    <td>
                        Product Des2
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 230px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdProducts" DataKeyNames="FormatId"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="FormatId" HeaderText="FormatId" SortExpression="FormatId"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Product Des1" SortExpression="FormatDe1">
                            <ItemTemplate>
                                <a href="#" onclick="ProductDet('<%#Container.DataItem("FormatDes")%>','<%#Container.DataItem("FormatId")%>')"
                                    class="Link">
                                    <%#Container.DataItem("FormatId")%>:<%#Container.DataItem("FormatDe1")%>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="50%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="FormatDe2" HeaderText="Product Des2" SortExpression="FormatDe2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hypProdDes" runat="server" />
        <asp:HiddenField ID="hidProdid" runat="server" />
        <asp:HiddenField ID="hidButton" runat="server" />
    </div>
</asp:Content>
