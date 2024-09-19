<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChartDetails.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_PopUp_ChartDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Users Chart Details</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function CaseSearch(reportName, reportId, type) {


            var hidRepdes = document.getElementById('<%= hidMatDes.ClientID%>').value
            var hidLnk = document.getElementById('<%= hidLnk.ClientID%>').value
            var hidRepId = document.getElementById('<%= hidMatId.ClientID%>').value
            var hidtype = document.getElementById('<%= hidtype.ClientID%>').value


            var str = reportName.replace(/##/g, "'");
            str = str.replace(/$#/g, '"');
            window.opener.document.getElementById(hidLnk).innerText = reportId + ":" + reportName.replace(new RegExp("&#", 'g'), "'");
            window.opener.document.getElementById(hidRepdes).value = reportId + ":" + reportName.replace(new RegExp("&#", 'g'), "'");
            //alert(window.opener.document.getElementById(hidRepdes).value);
            window.opener.document.getElementById(hidRepId).value = reportId;
            window.opener.document.getElementById(hidtype).value = type;

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
     <style type="text/css">
              
   .FixedHeader
        {
            position: absolute;
            margin-right: 19px;
            margin-left: 0px;
        }
        .ALNITEM
        {
            padding-left: 3px;
            padding-top: 10px;
            padding-bottom: 10px;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <div class="divHeader" style="font-size: 17px; font-weight: bold; text-align: center;
                margin-bottom: 20px; margin-top: 0px; width: 500px; height: 30px;">
                Users Chart Details
            </div>

            <table cellspacing="2">
            <tr>
            <td>&nbsp;</td>
            <td></td>
            </tr>
            <tr>
            <td></td>
            <td></td>
            </tr>
             <tr>
             
                <td style="width:500px">
                  <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                        Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtKey" runat="server" CssClass="SingleLineTextBox" Style="text-align: left;
                                        width: 280px" MaxLength="100" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin"/>
                </td>
                <td></td>
                </tr>
                <tr>
                    <td>
                <asp:Label ID="lblRecord" runat="server" CssClass="NormalLabel"
                                        Style="margin-left: 70px;" Font-Bold="True" ForeColor="Red"></asp:Label>               
                    
                    </td>
                </tr>
            </table>
            <%--    <table style="width:400px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td>
                        Report Name
                    </td>
                     
                </tr>
           </table>--%>
            <div style="width: 500px; height:520px; overflow: auto;">
                <asp:GridView Width="480px" runat="server" ID="grdChartDetails" DataKeyNames="USERCHARTID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                     <FooterStyle BackColor="#CCCC99" />
                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                    <Columns>
                        <asp:BoundField DataField="USERCHARTID" HeaderText="CHARTID" SortExpression="USERCHARTID"
                            Visible="false"></asp:BoundField>
                        
                        <asp:TemplateField HeaderText="Chart Name" SortExpression="USERCHARTID" >
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("REPORTNAME1")%>','<%#Container.DataItem("USERCHARTID")%>','<%#Container.DataItem("TYPE")%>')"
                                    class="Link">
                                    <%#Container.DataItem("USERCHARTID")%>:<%# Container.DataItem("REPORTNAME")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="TYPE" SortExpression="TYPE" >
                            <ItemTemplate>
                                 <asp:Label ID="lblType" runat="server" Text='<%# Bind("TYPE")%>'></asp:Label></ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                        <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>                                          
                    </Columns>
                       <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                    HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
               
            <br />
        </div>
        <asp:HiddenField ID="hidMatdes" runat="server" />
        <asp:HiddenField ID="hidMatid" runat="server" />
        <asp:HiddenField ID="hidLnk" runat="server" />
        <asp:HiddenField ID="hidchartgrp" runat="server" />
         <asp:HiddenField ID="hvUserGrd" runat="server" />
         <asp:HiddenField ID="hidtype" runat="server" />

    </div>
    </form>
</body>
</html>
