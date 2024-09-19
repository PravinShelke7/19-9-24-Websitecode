<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ComparisonDetails.aspx.vb"
    Inherits="Pages_StandAssist_PopUp_ComparisonDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Comparison Details</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function CaseSearch(reportName, reportId) {
            var hidRepdes = document.getElementById('<%= hidReportDes.ClientID%>').value
            var hidRepId = document.getElementById('<%= hidReportId.ClientID%>').value
            var hidRepdes1 = document.getElementById('<%= hidReportDes1.ClientID%>').value
            //alert(MatDes.length);
            reportName = reportName.replace(new RegExp("&#", 'g'), "'");
            window.opener.document.getElementById(hidRepdes).innerText = reportId + ":" + reportName;
            window.opener.document.getElementById(hidRepdes1).value = reportId + ":" + reportName;
            window.opener.document.getElementById(hidRepId).value = reportId;
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
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; width: 670px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <div class="divHeader" style="font-size: 17px; font-weight: bold; text-align: center;
                margin-bottom: 10px; margin-top: 10px; width: 650px; height: 30px;">
                Comparison Details
            </div>
            <div style="height: 40px; width: 650px">
            </div>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="width: 670px; height: 400px; overflow: auto;">
                <asp:GridView Width="650px" runat="server" ID="grdReportSearch" DataKeyNames="AssumptionId"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="AssumptionId" HeaderText="AssumptionId" SortExpression="AssumptionId"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Comparison Name" SortExpression="DESCRIPTION">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("DESCRIPTIONAPOS1")%>','<%#Container.DataItem("AssumptionId")%>')"
                                    class="Link">
                                    <%# Container.DataItem("AssumptionId")%>:<%# Container.DataItem("DESCRIPTION")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="300px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CaseIds" HeaderText="STRUCTURES" SortExpression="CaseIds">
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidReportId" runat="server" />
        <asp:HiddenField ID="hidReportDes" runat="server" />
        <asp:HiddenField ID="hidReportDes1" runat="server" />
    </div>
    </form>
</body>
</html>
