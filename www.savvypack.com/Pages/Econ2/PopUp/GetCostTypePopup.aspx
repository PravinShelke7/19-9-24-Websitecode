<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetCostTypePopup.aspx.vb"
    Inherits="Pages_Econ2_PopUp_GetCostTypePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E2-Cost type details</title>
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
                            CheckSPMul("490");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">
        function PositionDet(CostDes, CostId) {

            var hypCostDes = document.getElementById('<%= hypCostDes.ClientID%>').value;
            var hidCostid = document.getElementById('<%= hidCostid.ClientID%>').value;
            //alert(PalletDes);
            window.opener.document.getElementById(hypCostDes).innerText = CostDes.replace(/##/g, '"');
            window.opener.document.getElementById(hidCostid).value = CostId
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
                        <b>Cost type Des1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCostDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <%-- <tr>
                        <td align="right">
                            <b>Position Des2:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtPosDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>--%>
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
                        Cost type Des1
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 230px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdCostType" DataKeyNames="costID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="costID" HeaderText="costID" SortExpression="costID" Visible="false">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Pos Des1" SortExpression="palletde1">
                            <ItemTemplate>
                                <a href="#" onclick="PositionDet('<%#Container.DataItem("costdes")%>','<%#Container.DataItem("costID")%>')"
                                    class="Link">
                                    <%#Container.DataItem("costID")%>:<%#Container.DataItem("costde1")%>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="50%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <%-- <asp:BoundField DataField="costde1" HeaderText="Cost Des" SortExpression="costde1" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>--%>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hypCostDes" runat="server" />
        <asp:HiddenField ID="hidCostid" runat="server" />
    </div>
    </form>
</body>
</html>
