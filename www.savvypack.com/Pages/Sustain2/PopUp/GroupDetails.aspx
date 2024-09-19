<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupDetails.aspx.vb" Inherits="Pages_Sustain2_PopUp_GroupDetails" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group Details</title>
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function GroupSearch() {
             window.opener.document.getElementById('btnRefresh').click();
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
       <div id="PageSection1" style="text-align:left;width:810px;">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
         <%--   <div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:650px;height:30px;">
                      Group Details
</div>--%>
    <table style="text-align:center;" >
                <tr>
                  <td style="height:10px;"></td>
                </tr>
                <tr>
                                    <td class="PageSHeading" style="font-size: 16px;width:800px;">
                                      Group Details                            
                                    </td>
                                </tr>
                                <tr>
                                  <td>
                                         <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" style="text-align:cente;font-size:16px;font-weight:bold;color:Red;"> </asp:Label>
                                  </td>
                                </tr>
               </table>
<div style="height:10px; width:650px" ></div>
            <table cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                        </td>
                  </tr>
                </table>
         
           <div style="width:830px;overflow:auto;">
     
           <asp:GridView Width="800px" runat="server" ID="grdGrpCases" DataKeyNames="GROUPID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" 
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle Height="25px" BackColor="#6B696B" Font-Size="12px"  Font-Bold="True" ForeColor="White" />
                  
                    <Columns>
                        <asp:BoundField DataField="GROUPID" HeaderText="GROUPID" SortExpression="GROUPID" Visible="false">  
                        </asp:BoundField>
                           <asp:TemplateField HeaderText="Group Name" HeaderStyle-HorizontalAlign="center" SortExpression="GROUPNAME" Visible="true" 
                                                ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkGrpId" runat="server" CssClass="Link" Text='<%# bind("CDES1")%>' OnClick="lnkGrpId_Click" ></asp:LinkButton>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="160px" Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>
                        
                          
                        <asp:BoundField DataField="GROUPDES" HeaderText="Group Description" SortExpression="GROUPDES">                          
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left"  CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                         <asp:BoundField DataField="CaseID" HeaderText="Case(s)" SortExpression="CaseID">                          
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left"  CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                       <asp:BoundField DataField="CREATIONDATE" HeaderText="Creation Date" SortExpression="CREATIONDATE">
                            <ItemStyle Width="80px" Wrap="true" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UPDATEDATE" HeaderText="Update Date" SortExpression="UPDATEDATE">
                            <ItemStyle Width="80px" Wrap="true"  CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>                                                 
                   
                        
                        
                        
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidReportId" runat="server" />
          <asp:HiddenField  ID="hidReportDes" runat="server" />
          <asp:HiddenField  ID="hidReportDes1" runat="server" />
          <asp:HiddenField ID="hvCaseGrd" runat="server" />
   </div>
    </form>
</body>
</html>