<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CasesSearch.aspx.vb" Inherits="Pages_Sustain2_PopUp_CasesSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>S2-CaseDetails</title> 
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function CaseSearch(CaseDes, CaseId, StatusId, Status) {
             // alert(CaseDes+' '+CaseId);
             var hidCasedes = document.getElementById('<%= hidCaseDes.ClientID%>').value;
             // alert(hidCasedes);
             var hidCaseId = document.getElementById('<%= hidCaseId.ClientID%>').value;
             var hidCaseIdD = document.getElementById('<%= hidCaseIdD.ClientID%>').value;
             //alert(hidCaseId);
             // alert(window.opener.document.getElementById(hidCasedes));
             //alert(window.opener.document.getElementById(hidCaseId));

             if (CaseId == "0") {

                 window.opener.document.getElementById(hidCasedes).innerText = "Select case";
                 window.opener.document.getElementById(hidCaseIdD).value = "Select case";

             }
             else {
                 window.opener.document.getElementById(hidCasedes).innerText = CaseDes + "   " + Status;
                 window.opener.document.getElementById(hidCaseIdD).value = CaseDes + "   " + Status;
             }

             window.opener.document.getElementById(hidCaseId).value = CaseId;

             if (hidCaseId = "hidPropCase") {
                 window.opener.document.getElementById("hidPropCaseSt").value = StatusId;
                  var Button = window.opener.document.getElementById("btnAdminSubmit");
                 if (Button != null)
                 {
                     if (StatusId == "5") 
                     {
                         Button.style.display = 'block';
                     }
                     else 
                     {
                         Button.style.display = 'none';
                     }
                 }

             }
             window.close();
         }
         function CaseStatus(CaseId,Owner) {
             // alert('sud')
             var width = 800;
             var height = 250;
             var left = (screen.width - width) / 2;
             var top = (screen.height - height) / 2;
             var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
             params += ', location=no';
             params += ', menubar=no';
             params += ', resizable=yes';
             params += ', scrollbars=yes';
             params += ', status=yes';
             params += ', toolbar=no';
             var page='StatusDetails.aspx?CaseId='+CaseId+'&Owner='+Owner+' ';
             newwin = window.open(page, 'CaseStatus', params);
             if (newwin == null || typeof (newwin) == "undefined") {
                 alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
             }

             return false;

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
             if (hidCaseid == "hidPropCase") {
                 URL = "CasesViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=P&GrpID=" + groupID;
             }
             else {
                 URL = "CasesViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=AP&GrpID=" + groupID;
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
    <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
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
                             <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" style="margin-left:0px" />
                             <asp:Button ID="btnCaseViewer" Text="Case Viewer" runat="server" CssClass="Button" Visible="false"  />
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
            
           <div style="width:750px;height:280px;overflow:auto;">
        
           <asp:GridView Width="700px" runat="server" ID="grdCaseSearch" DataKeyNames="CaseId" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="true"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="CaseId" HeaderText="CaseId" SortExpression="CaseId" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="PACKAGE FORMAT" SortExpression="CaseDe1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="CaseSearch('<%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDES")%>','<%#Container.DataItem("CaseId")%>','<%#Container.DataItem("STATUSID")%>','<%#Container.DataItem("STATUS")%>')" class="Link">
                                      <%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDE1")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="CaseDe2" HeaderText="UNIQUE FEATURES" SortExpression="CaseDe2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="20%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                           <asp:BoundField DataField="CaseOwner" HeaderText="Owner" SortExpression="CaseOwner" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="30%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">                              
                                <ItemTemplate>
                                   <a href="#" onclick="CaseStatus('<%#Container.DataItem("CaseId")%>','<%#Container.DataItem("CaseOwner")%>')" class="Link">
                                      <%#Container.DataItem("STATUS")%>
                                  </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="20%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                        
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidCaseid" runat="server" />
          <asp:HiddenField ID="hidCaseDes" runat="server" />
           <asp:HiddenField  ID="hidCaseidD" runat="server" />
   </div>
    </form>
</body>
</html>
