<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportDetails.aspx.vb" Inherits="Pages_Market1_PopUp_ReportDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Details</title>
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
        function CaseSearch(reportName, reportId,repType) {
            var hidRepdes = document.getElementById('<%= hidReportDes.ClientID%>').value
            var hidRepId = document.getElementById('<%= hidReportId.ClientID%>').value
            //alert(MatDes.length);
            window.opener.document.getElementById(hidRepdes).innerText = reportId + ":" + reportName;
            window.opener.document.getElementById(hidRepId).value = reportId;
            window.opener.document.getElementById('hidRptType').value = repType;
            window.close();

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
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <%--<div class="divHeader" style="font-size: 17px; font-weight: bold; text-align: center;
                margin-bottom: 10px; margin-top: 10px; width: 700px; height: 30px;">
                Report Selector                
            </div>--%>
            <br />
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td colspan="2">
                        <div class="PageHeading" id="divMainHeading" style="width: 100%; margin-left: 300px;">
                            Report Selector<br />
                            <br />
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Search:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtkey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                            width: 200px"></asp:TextBox>
                        &nbsp;<asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblPreport" runat="server" CssClass="CalculatedFeilds" Visible="true"
                Style="font-size: 16px; color: Red; margin-left: 10px;" Text="Currently you have no Reports defined. You can create a Report through ToolBox."></asp:Label>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="width: 720px; height: 360px; overflow: auto; margin-left: 10px;">
                <asp:GridView Width="700px" runat="server" ID="grdReportSearch" DataKeyNames="REPORTID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="REPORTID" HeaderText="REPORTID" SortExpression="REPORTID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Report Name" SortExpression="REPORTDES">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("REPORTNAME")%>','<%#Container.DataItem("REPORTID")%>','<%#Container.DataItem("RPTTYPE")%>')"
                                    class="Link">
                                    <%# Container.DataItem("REPORTID")%>:<%# Container.DataItem("REPORTNAME")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="450px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="RPTTYPE" HeaderText="Report Type" SortExpression="RPTTYPE">
                            <ItemStyle Width="150px" Wrap="true" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="center" SortExpression="Details"
                            ItemStyle-HorizontalAlign="Left" Visible="false" >
                            <ItemTemplate>
                                <asp:HyperLink Width="100px" ID="lnkRepDetails" class="Link" Target="_blank" Enabled="true"
                                    runat="server" Text="Get" AutoPostBack="false" NavigateUrl='<%# "~/Pages/Market1Sub/PopUp/RepDet.aspx?ReportId="+  Eval("REPORTID").toString()+"&Type="+Eval("RPTTYPE").toString()%>'></asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                            <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidReportId" runat="server" />
        <asp:HiddenField ID="hidReportDes" runat="server" />
        <asp:HiddenField ID="hidGrpId" runat="server" />
        <asp:HiddenField ID="hvUserGrd" runat="server" />
    </div>
    </form>
</body>
</html>
