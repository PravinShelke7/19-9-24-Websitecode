<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaseSearch.aspx.vb" Inherits="Pages_Sustain2_PopUp_CaseSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>S2-CaseDetails</title>
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
        function CaseSearch(CaseId) {
            var hidCaseid = document.getElementById('<%= hidCaseid.ClientID%>').value;
            window.opener.document.getElementById(hidCaseid).value = CaseId;
            window.close();
        }
        function CaseViewer(groupID) {
            var CaseDe1 = document.getElementById("txtCaseDe1")
            var CaseDe2 = document.getElementById("txtCaseDe2")
            var hidCaseid = document.getElementById('<%= hidCaseid.ClientID%>').value;
            var width = 800;
            var height = 500;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var URL
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            if (hidCaseid == "ddlPCase") {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=P&GrpID=" + groupID;
            }
            else {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=B";
            }
            //alert(URL);
            newwin = window.open(URL, 'CaseViewer', params);
            return false
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
                        <b>Case Des1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Case Des2:</b>
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
                        <asp:Button ID="btnCaseViewer" Text="Case Viewer" runat="server" CssClass="Button"
                            Visible="false" />
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
                        PACKAGE FORMAT
                    </td>
                    <td>
                        UNIQUE FEATURES
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 230px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdCaseSearch" DataKeyNames="CaseId"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="CaseId" HeaderText="CaseId" SortExpression="CaseId" Visible="false">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="PACKAGE FORMAT" SortExpression="CaseDe1">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("CaseId")%>')" class="Link">
                                    <%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDE1")%>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="50%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CaseDe2" HeaderText="UNIQUE FEATURES" SortExpression="CaseDe2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidCaseid" runat="server" />
    </div>
    </form>
</body>
</html>
