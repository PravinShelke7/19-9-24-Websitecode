﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetProdcuts.aspx.vb" Inherits="Pages_Econ1_PopUp_GetProdcuts" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1-Get Products</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">
        function ProductDet(ProductDes, FromtId) {

            var hypPalDes = document.getElementById('<%= hypPalDes.ClientID%>').value;
            var hidPalid = document.getElementById('<%= hidPalid.ClientID%>').value;
            var hidBtn = document.getElementById('<%= hidButton.ClientID%>').value;
            //alert(ProductDes);
            window.opener.document.getElementById(hypPalDes).innerText = ProductDes;
            window.opener.document.getElementById(hidPalid).value = FromtId
            if (hidBtn != "") {
                window.opener.document.getElementById(hidBtn).click();
            }
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Product De1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPalDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Product De2:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtPalDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
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
        <asp:HiddenField ID="hypPalDes" runat="server" />
        <asp:HiddenField ID="hidPalid" runat="server" />
        <asp:HiddenField ID="hidButton" runat="server" />
    </div>
    </form>
</body>
</html>
