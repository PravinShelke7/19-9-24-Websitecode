<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaseSearch.aspx.vb" Inherits="Pages_StandAssist_PopUp_CaseSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stand Alone Barrier Assistant-CaseDetails</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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
            if (hidCaseid == "hidApprovedCase") {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=B&GrpID=" + groupID;
            }
            else if (hidCaseid == "hidPropCase") {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=P&GrpID=" + groupID;
            }
            else {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=C";
            }
            //alert(URL);
            newwin = window.open(URL, 'CaseViewer', params);
            return false
        }
         
         
    </script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

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
                        <b>Packaging Format:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Unique Features:</b>
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
                        <asp:Button ID="btnCaseViewer" Text="Structure Viewer" runat="server" CssClass="Button"
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
        <asp:HiddenField  ID="hidGrpId" runat="server" />
    </div>
    </form>
</body>
</html>
