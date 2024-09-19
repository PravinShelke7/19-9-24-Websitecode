<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportDetails.aspx.vb" Inherits="Pages_Market1_PopUp_ReportDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Report Details</title>
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function CaseSearch(reportName,reportId) 
         {   
              var hidRepdes = document.getElementById('<%= hidReportDes.ClientID%>').value
             var hidRepId = document.getElementById('<%= hidReportId.ClientID%>').value
             //alert(MatDes.length);
             window.opener.document.getElementById(hidRepdes).innerText = reportId+":"+reportName;
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
    <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:700px;height:30px;">
                      Report Details
</div>
<div style="height:40px;"></div>
            <table cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
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
           <div style="width:720px;height:400px;overflow:auto;">
        
           <asp:GridView Width="700px" runat="server" ID="grdReportSearch" DataKeyNames="REPORTID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="REPORTID" HeaderText="REPORTID" SortExpression="REPORTID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Report Name" SortExpression="CaseDe1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="CaseSearch('<%#Container.DataItem("REPORTNAME")%>','<%#Container.DataItem("REPORTID")%>')" class="Link">
                                      <%# Container.DataItem("REPORTID")%>:<%# Container.DataItem("REPORTNAME")%>
                                  </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="300px" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                           
                              <asp:BoundField DataField="RPTTYPE" HeaderText="Report Type" SortExpression="RPTTYPE">
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left" Width="200px" />
                        </asp:BoundField>
                        
                         <asp:TemplateField HeaderText="Details" HeaderStyle-HorizontalAlign="center" SortExpression="Details" ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>                             
                               <asp:HyperLink Width="150px" ID="lnkRepDetails" class="Link" Target="_blank"  Enabled="true" runat="server" Text="Get"  AutoPostBack="false"  NavigateUrl='<%# "~/Pages/Market1/PopUp/RepDet.aspx?ReportId="+  Eval("REPORTID").toString()+"&Type="+Eval("TYPE").toString()%>'></asp:HyperLink>
                          </ItemTemplate>
                              <HeaderStyle HorizontalAlign="Left" />
                             <ItemStyle Width="120px"  Wrap="true" HorizontalAlign="Left"  />   
                        </asp:TemplateField>
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidReportId" runat="server" />
           <asp:HiddenField  ID="hidReportDes" runat="server" />
          
   </div>
    </form>
</body>
</html>
