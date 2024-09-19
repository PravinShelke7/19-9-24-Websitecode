<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetSupplier.aspx.vb" Inherits="Pages_StandAssist_PopUp_GetSupplier" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stand Alone Barrier Assistant-Get Grades</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
      function OpenTDataSheet(SupId, matId, page, gradeid)
         {

           
            // alert(matId + ' ' + SupId + ' ' + gradeid);
                var width = 1050;
                var height = 600;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=yes';
                params += ', scrollbars=no';
                params += ', status=yes';
                params += ', toolbar=no';

                page = page + "?Sponsor=" + SupId + "&MatId=" + matId + "&GradeId=" + gradeid
                newwin = window.open(page, 'Chat11', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                }
           

            return false;
        }
        
     function SendEmail(email) {
           
            document.getElementById("hidMail").value = email;
           
            document.getElementById("btnSendEmail").click();
        }
        
        function GradeDet(Name,SupId,GradeDes, GradeId,flag) {
            var hidGradedes = document.getElementById('<%= hidGradedes.ClientID%>').value
            var hidGradeId = document.getElementById('<%= hidGradeId.ClientID%>').value
            var hidSupId = document.getElementById('<%= hidSupId.ClientID%>').value
            var hidImgId = document.getElementById('<%= hidImgId.ClientID%>').value
             var hidImgSet= document.getElementById('<%= hidImgSet.ClientID%>').value
            
          //  alert(GradeId);

            // alert(window.opener.document.getElementById(hidSGV).value + '@@@' + Weight + '###' + SG);

            // alert(hidGradeId);
            if (GradeId == "0") {
                window.opener.document.getElementById(hidGradedes).innerText =  GradeDes;
                  opener.ShowToolTip(hidGradedes, 'View Supplier Grade');
            }
            else {
                window.opener.document.getElementById(hidGradedes).innerText = Name + ' ' + GradeDes;
                  opener.ShowToolTip(hidGradedes, Name + ' ' + GradeDes);
            }
            window.opener.document.getElementById(hidGradeId).value = GradeId;
            window.opener.document.getElementById(hidSupId).value = SupId;

            if (GradeId == "0") {
            
                window.opener.document.getElementById(hidImgId).style.display = 'none';
                  window.opener.document.getElementById(hidImgSet).style.display = 'none';
            }
             else if (GradeId != "0" && flag == "False") 
            {
                 window.opener.document.getElementById(hidImgId).style.display = 'none';
                window.opener.document.getElementById(hidImgSet).style.display = 'inline';
             
            }
            else {
            //alert(hidGradedes);
            window.opener.document.getElementById(hidGradedes).style.width = "120px";
                window.opener.document.getElementById(hidImgId).style.display = 'inline';
                 window.opener.document.getElementById(hidImgSet).style.display = 'none';
            }
           
            window.close();
        }
        
        function OpenPopup(page) {

            var width = 1050;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(page, 'Chat11', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            //return false;
        }
     </script> 
</head> 
<body>
 <form id="form1" runat="server">
   <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0;height:350px;">
       <div id="PageSection1" style="text-align:center;height:100%;overflow:auto;">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            
          <br />
            <table cellspacing="10" style="">
                 <tr>
                    <td style="width: 30%" valign="top">
                        <asp:Table ID="tblSupplierGrade" runat="server" CellPadding="0" CellSpacing="2">                        
                        </asp:Table>                        
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label ID="lblPcase" runat="server" CssClass="CalculatedFeilds" Visible="true" style="font-size:16px;color:Red;"
              Text="There is no company entered for this Material."></asp:Label>
        <table cellspacing="10" style="">
                 <tr>
                    <td style="width: 30%" valign="top">
                        <asp:Table ID="tblNonSponser" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>                      
                    </td>
                </tr>
            </table>
        </div>
        <asp:Button ID="btnSendEmail" Text="Send Email" runat="server" style="display:none;" />
        <asp:HiddenField  ID="hidGradedes" runat="server" />
        <asp:HiddenField  ID="hidGradeId" runat="server" />
            <asp:HiddenField  ID="hidSG" runat="server" />

            <asp:HiddenField ID="hidMatID" runat="server" />
             <asp:HiddenField ID="hidSponsId" runat="server" />
             <asp:HiddenField ID="hidMail" runat="server" />
               <asp:HiddenField ID="hidSupId" runat="server" />
               <asp:HiddenField ID="hidImgId" runat="server" />
                <asp:HiddenField ID="hidImgSet" runat="server" />
   </div>
 </form>
</body>
</html>
