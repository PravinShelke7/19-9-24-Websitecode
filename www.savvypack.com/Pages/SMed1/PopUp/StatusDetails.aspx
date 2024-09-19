<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StatusDetails.aspx.vb" Inherits="Pages_MedSustain1_PopUp_StatusDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMed1-Case Status Details</title> 
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function CaseSearch(CaseDes, CaseId) {
             //alert(CaseId);
             var hidCasedes = document.getElementById('<%= hidCaseDes.ClientID%>').value;
             var hidCaseId = document.getElementById('<%= hidCaseId.ClientID%>').value;
             window.opener.document.getElementById(hidCasedes).innerText = CaseDes;
             window.opener.document.getElementById(hidCaseId).innerText = CaseId;
             window.close();
         }
         function CaseStatus(CaseId) {
             // alert('sud')
             var width = 500;
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
             var page = 'StatusDetails.aspx?CaseId=' + CaseId + ' ';
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
            
            <table cellspacing="0" style="text-align:center;width:90%">
            <tr>
                        <td align="center" style="font-family:Arial;font-size:14px;font-weight:bold;height:15px;">
                          
                        </td>
                  </tr>
                    <tr>
                        <td align="center" style="font-family:Arial;font-size:15px;font-weight:bold;">
                           <asp:label id="lblStatus" runat="server" ></asp:label>
                        </td>
                  </tr>
                   <tr>
                        <td align="center" style="font-family:Arial;font-size:14px;font-weight:bold;height:15px;">
                          
                        </td>
                  </tr>
                </table>
            
           <div style="width:750px;height:140px;overflow:auto;">
        
           <asp:GridView Width="750px" runat="server" ID="grdCaseStatus" DataKeyNames="CaseId" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="true"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="CaseId" HeaderText="CaseId" SortExpression="CaseId" Visible="false">  
                        </asp:BoundField>
                          <asp:BoundField DataField="STATUS" HeaderText="Status" SortExpression="STATUS" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="25%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                          <asp:BoundField DataField="DATED" HeaderText="Date" SortExpression="DATED" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="20%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                          <asp:BoundField DataField="ACTIONBY" HeaderText="Action By" SortExpression="ACTIONBY" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="25%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                          <asp:BoundField DataField="COMMENTS" HeaderText="Comments" SortExpression="COMMENTS" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="30%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                        
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidCaseid" runat="server" />
          <asp:HiddenField ID="hidCaseDes" runat="server" />
   </div>
    </form>
</body>
</html>
