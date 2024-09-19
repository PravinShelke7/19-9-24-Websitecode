<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaseApproval.aspx.vb" Inherits="ClientAdmin_CaseApproval" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title>Case Approval Tool</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <meta name="robots" content="noindex,nofollow" />

      <style type="text/css">
          .SingleLineTextBox
{
	font-family:Verdana;
	font-size:10px;
	width:120px;
	height:18px;
	margin-top:2px;
	margin-bottom:2px;
	margin-left:2px;
	margin-right:2px;
	border-right: #7F9DB9 1px solid;
	border-top: #7F9DB9 1px solid;
	border-left: #7F9DB9 1px solid;
	border-bottom: #7F9DB9 1px solid;
	text-align:right;
	background-color:#FEFCA1;
}
        .divUpdateprogress {
            left: 470px;
            top: 380px;
            position: absolute;
        }
    </style>

   <script type="text/javascript" language="javascript">

       function EvaluateCheck() {
           var caseid = "";
           var caseid2 = "";
           var caseid3 = "";
           var caseid4 = "";
           var caseid5 = "";
           var msg5 = "";
           var msg1 = "";
           var msg2 = "";
           var msg3 = "";
           var msg4 = "";
           var cnt = 0;
 var caseid6 = "";
           var msg6 = "";
           var appCnt = document.getElementById("hidApprove").value;
           var count = 0;

           var table = document.getElementById('<%=grdCase.ClientID %>');
           for (var i = 1; i < table.rows.length; i++) {
               var Row = table.rows[i];
               var CellValue = Row.cells[0];
               for (j = 0; j < CellValue.childNodes.length; j++) {
                   if (CellValue.childNodes[j].type == "checkbox") {
                       if (CellValue.childNodes[j].checked == true) {
                           count = count + 1;
                        
                           var userName = Row.cells[2].innerText;
                          
                           if (Row.cells[0].childNodes[1].innerHTML == 1) {
                               if (caseid == "") {
                                   caseid = Row.cells[5].innerText;
                                   msg1 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid = caseid + Row.cells[5].innerText;

                                   msg1 = msg1 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 3) {
                               if (caseid2 == "") {
                                   caseid2 = Row.cells[5].innerText;
                                   msg2 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid2 = caseid2 + Row.cells[5].innerText;
                                   msg2 = msg2 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           //Start
                           else if (Row.cells[0].childNodes[1].innerHTML == 2) {
                               if (caseid3 == "") {
                                   caseid3 = Row.cells[5].innerText;
                                   msg3 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid3 = caseid3 + Row.cells[5].innerText;
                                   msg3 = msg3 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 5) {
                               if (caseid4 == "") {
                                   caseid4 = Row.cells[5].innerText;
                                   msg4 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is a Sister Case.\n";

                               }
                               else {
                                   caseid4 = caseid4 + Row.cells[5].innerText;
                                   msg4 = msg4 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is a Sister Case.\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 6) {
                               
                               if (caseid5 == "") {
                                   caseid5 = Row.cells[5].innerText;
                                  
                                   msg5 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is already under Evaluation.\n";
                                   
                               }
                               else {
                                   caseid5 = caseid5 + Row.cells[5].innerText;
                                   msg5 = msg5 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is already under Evaluation.\n";
                               }
                           }
 else if (Row.cells[0].childNodes[1].innerHTML == 4) {
                              // alert(Row.cells[0].childNodes[1].innerHTML);
                               if (caseid6 == "") {
                                   caseid6 = Row.cells[5].innerText;
                                  
                                   msg6 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is already Disapproved.\n";
                                   
                               }
                               else {
                                   caseid6 = caseid6 + Row.cells[5].innerText;
                                   msg6 = msg6 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is already Disapproved.\n";
                               }
                           }
                           //End
                           cnt = cnt + 1;
                       }
                   }
               }
           }
           
           if (count == 0) {

               var msg = "Please select at least one case to Evaluate.";
               alert(msg);
               return false;
           }
           else if (count > appCnt) {
               msg = "You can not select more than " + appCnt + " cases to Evaluate."
               alert(msg);
               return false;
           }
           else if (msg2 && msg3) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg3 + " is/are already used for approval.\n" + msg4 + "\nSo you cannot Evaluate.\n" + msgLine + "\n Following:\n";
               msg = msg + msg2 + "is/are already Approved.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg3) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg3 + " is/are already used for approval.\n" + msg4 + "\nSo you cannot Evaluate.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg2) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n"
               msg = msg + msg2 + "is/are already Approved.\n" + msg4 + "\nSo you cannot Evaluate.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg4) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg4 + "\nSo you cannot Evaluate.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg5) {
            
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg5 + "\nSo you cannot Evaluate.\n" + msgLine;
               alert(msg);
               return false;
           }
else if (msg6) {
            
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg6 + "\nSo you cannot Evaluate.\n" + msgLine;
               alert(msg);
               return false;
           }
           else {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "You are going to Evaluate: \n"
               msg = msg + msg1 + "for the Evaluation case list.\n\n  Do you want to proceed?\n" + msgLine;
               // var msg = "You are going to create a COPY of case "+caseid+" . Do you want to proceed?"
               if (confirm(msg)) {
                   return true;
               }
               else {
                   return false;
               }

           }
       }


       function OpenGroupPopup(page) {
           var width = 500;
           var height = 250;
           var left = (screen.width - width) / 2;
           var top = (screen.height - height) / 2;
           var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';

           params += ', location=no';
           params += ', menubar=no';
           params += ', resizable=no';
           params += ', scrollbars=yes';
           params += ', status=yes';
           params += ', toolbar=no';
           newwin = window.open(page, 'Chat', params);
           if (newwin == null || typeof (newwin) == "undefined") {
               alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
           }

           return false;
       }

       function RefreshPageBar() {
       }

       function testData(page) {
           // alert('sud');
           var width = 800;
           var height = 250;
           var left = (screen.width - width) / 2;
           var top = (screen.height - height) / 2;
           var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
           params += ', location=no';
           params += ', menubar=no';
           params += ', resizable=no';
           params += ', scrollbars=no';
           params += ', status=yes';
           params += ', toolbar=no';
           newwin = window.open(page, 'Chat', params);
           if (newwin == null || typeof (newwin) == "undefined") {
               alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
           }

           return false;
       }

       function ShowWindow(Page) {
           //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
           var width = 900;
           var height = 600;
           var left = (screen.width - width) / 2;
           var top = (screen.height - height) / 2;
           var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
           params += ', location=no';
           params += ', menubar=no';
           params += ', resizable=yes';
           params += ', scrollbars=yes';
           params += ', status=yes';
           params += ', toolbar=no';
           newwin = window.open(Page, 'Chat', params);
       }

       function DisApproveCheck() {
           var caseid = "";
           var caseid2 = "";
           var msg1 = "";
           var msg2 = "";
           var cnt = 0;
           var delCnt = document.getElementById("hidDisApprove").value;
           var count = 0;
           var caseid3 = "";
           var msg3 = "";
           var caseid4 = "";
           var msg4 = "";
           var caseid5 = "";
           var msg5 = "";


           var table = document.getElementById('<%=grdCase.ClientID %>');
           for (var i = 1; i < table.rows.length; i++) {
               var Row = table.rows[i];
               var CellValue = Row.cells[0];
               for (j = 0; j < CellValue.childNodes.length; j++) {
                   if (CellValue.childNodes[j].type == "checkbox") {
                       if (CellValue.childNodes[j].checked == true) {
                           count = count + 1;
                           //alert(CellValue.childNodes[j].checked);
                           var userName = Row.cells[2].innerText;
                           if (Row.cells[0].childNodes[1].innerHTML == 3 || Row.cells[0].childNodes[1].innerHTML == 1) {
                               if (caseid == "") {
                                   caseid = Row.cells[5].innerText;
                                   msg1 = "- Case " + Row.cells[5].innerText + " for " + userName + "\n";

                               }
                               else {
                                   caseid = caseid + Row.cells[5].innerText;
                                   msg1 = msg1 + "- Case " + Row.cells[5].innerText + " for " + userName + "\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 4) {
                               if (caseid2 == "") {
                                   caseid2 = Row.cells[5].innerText;
                                   msg2 = "- Case " + Row.cells[5].innerText + " for " + userName + "\n";

                               }
                               else {
                                   caseid2 = caseid2 + Row.cells[5].innerText;
                                   msg2 = msg2 + "- Case " + Row.cells[5].innerText + " for " + userName + "\n";
                               }
                           }
                           //Start
                           else if (Row.cells[0].childNodes[1].innerHTML == 2) {
                               if (caseid3 == "") {
                                   caseid3 = Row.cells[5].innerText;
                                   msg3 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid3 = caseid3 + Row.cells[5].innerText;
                                   msg3 = msg3 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 5) {
                               if (caseid4 == "") {
                                   caseid4 = Row.cells[5].innerText;
                                   msg4 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is a Sister Case.\n";

                               }
                               else {
                                   caseid4 = caseid4 + Row.cells[5].innerText;
                                   msg4 = msg4 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is a Sister Case.\n";
                               }
                           }

 			 else if (Row.cells[0].childNodes[1].innerHTML == 6) 
			{
                               
                               if (caseid5 == "") {
                                   caseid5 = Row.cells[5].innerText;
                                  
                                   msg5 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is under Evaluation.\n";
                                   
                               }
                               else {
                                   caseid5 = caseid5 + Row.cells[5].innerText;
                                   msg5 = msg5 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is under Evaluation.\n";
                               }
                           }


                          
                           //End
                           cnt = cnt + 1;
                       }
                   }
               }
           }
           if (count == 0) {

               var msg = "Please select at least one case to DISAPPROVE.";
               alert(msg);
               return false;
           }
           else if (count > delCnt) {
               msg = "You cannot select more than " + delCnt + " cases for DISAPPROVE."
               alert(msg);
               return false;
           }
           else if (msg2 && msg3) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg3 + " is/are already used for approval.\n" + msg4 + "\nSo you cannot Disapprove.\n" + msgLine + "\n Following:\n";
               msg = msg + msg2 + "is/are already Disapproved.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg3) {
               var msgLine = "\n"// "--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg3 + " is/are already used for approval.\n" + msg4 + "\nSo you cannot Disapprove.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg2) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n"
               msg = msg + msg2 + "is/are already Disapproved.\n" + msg4 + "\nSo you cannot Disapprove.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg4) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg4 + "\nSo you cannot Disapprove.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg5) {
            
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg5 + "\nSo you cannot Evaluate.\n" + msgLine;
               alert(msg);
               return false;
           }

           else {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "You are going to DISAPPROVE: \n"
               msg = msg + msg1 + "Do you want to proceed?\n" + msgLine;
               // var msg = "You are going to create a COPY of case "+caseid+" . Do you want to proceed?"
               if (confirm(msg)) {
                   return true;
               }
               else {
                   return false;
               }

           }

       }

       function ApproveCheck() {
           var caseid = "";
           var caseid2 = "";
           var caseid3 = "";
           var caseid4 = "";
           var msg4 = "";
           var msg1 = "";
           var msg2 = "";
           var msg3 = "";
           var cnt = 0;
           var appCnt = document.getElementById("hidApprove").value;
           var count = 0;
           var caseid5 = "";
           var msg5 = "";

           var table = document.getElementById('<%=grdCase.ClientID %>');
           for (var i = 1; i < table.rows.length; i++) {
               var Row = table.rows[i];
               var CellValue = Row.cells[0];
               for (j = 0; j < CellValue.childNodes.length; j++) {
                   if (CellValue.childNodes[j].type == "checkbox") {
                       if (CellValue.childNodes[j].checked == true) {
                           count = count + 1;
                           //alert(CellValue.childNodes[j].checked);
                           var userName = Row.cells[2].innerText;
                           // alert(userName);
                           //alert(Row.cells[0].innerText);
                           //alert(Row.cells[0].childNodes[1].innerHTML);
                           //alert(CellValue.childNodes[0].innerText);
                           if (Row.cells[0].childNodes[1].innerHTML == 4 || Row.cells[0].childNodes[1].innerHTML == 1) {
                               if (caseid == "") {
                                   caseid = Row.cells[5].innerText;
                                   msg1 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid = caseid + Row.cells[5].innerText;

                                   msg1 = msg1 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 3) {
                               if (caseid2 == "") {
                                   caseid2 = Row.cells[5].innerText;
                                   msg2 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid2 = caseid2 + Row.cells[5].innerText;
                                   msg2 = msg2 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           //Start
                           else if (Row.cells[0].childNodes[1].innerHTML == 2) {
                               if (caseid3 == "") {
                                   caseid3 = Row.cells[5].innerText;
                                   msg3 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";

                               }
                               else {
                                   caseid3 = caseid3 + Row.cells[5].innerText;
                                   msg3 = msg3 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 5) {
                               if (caseid4 == "") {
                                   caseid4 = Row.cells[5].innerText;
                                   msg4 = "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is a Sister Case.\n";

                               }
                               else {
                                   caseid4 = caseid4 + Row.cells[5].innerText;
                                   msg4 = msg4 + "- Case " + Row.cells[5].innerText + " submitted by " + userName + "\n is a Sister Case.\n";
                               }
                           }
                           else if (Row.cells[0].childNodes[1].innerHTML == 6) {

                               if (caseid5 == "") {
                                   caseid5 = Row.cells[5].innerText;

                                   msg5 = "- Case " + Row.cells[5].innerText + " submitted by " + userName 

                               }
                               else {
                                   caseid5 = caseid5 + Row.cells[5].innerText;
                                   msg5 = msg5 + "- Case " + Row.cells[5].innerText + " submitted by " + userName 
                               }
                           }
                           //End
                           cnt = cnt + 1;
                       }
                   }
               }
           }
           if (count == 0) {

               var msg = "Please select at least one case to APPROVE.";
               alert(msg);
               return false;
           }
           else if (count > appCnt) {
               msg = "You can not select more than " + appCnt + " cases for APPROVE."
               alert(msg);
               return false;
           }
           else if (msg2 && msg3) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg3 + " is/are already used for approval.\n" + msg4 + "\nSo you cannot Approve.\n" + msgLine + "\n Following:\n";
               msg = msg + msg2 + "is/are already Approved.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg3) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg3 + " is/are already used for approval.\n" + msg4 + "\nSo you cannot Approve.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg2) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n"
               msg = msg + msg2 + "is/are already Approved.\n" + msg4 + "\nSo you cannot Approve.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg4) {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "Following: \n" + msg4 + "\nSo you cannot Approve.\n" + msgLine;
               alert(msg);
               return false;
           }
           else if (msg5) {

               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "You are going to APPROVE: \n"
               msg = msg + msg5 + "for the approved case list.\n\n  Do you want to proceed?\n" + msgLine;
               // var msg = "You are going to create a COPY of case "+caseid+" . Do you want to proceed?"
               if (confirm(msg)) {
                   return true;
               }
               else {
                   return false;
               }
           }
           else {
               var msgLine = "\n"//"--------------------------------------------------------------------------\n";
               var msg = msgLine + "You are going to APPROVE: \n"
               msg = msg + msg1 + "for the approved case list.\n\n  Do you want to proceed?\n" + msgLine;
               // var msg = "You are going to create a COPY of case "+caseid+" . Do you want to proceed?"
               if (confirm(msg)) {
                   return true;
               }
               else {
                   return false;
               }

           }
       }

       function clickButton(e, buttonid) {
           //alert(navigator.appName);
           var bt = document.getElementById(buttonid);
           if (bt) {

               if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                   if (event.keyCode == 13) {
                       document.getElementById(buttonid).focus();
                       // alert(buttonid);
                       //document.getElementById(buttonid).click();  

                   }
               }

           }

       } 
   </script>
</head>
<body>
    <form id="form1" runat="server">

           <div id="MasterContent" style="width:1800px;">
                 <div id="AlliedLogo">
            
          <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
        </div>  
              <div>
        <table class="E1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>
                                <td >
                                  
                                </td> 
                                         
                                                                                          
                                
                        </tr>
                    </table>
               </td>
            </tr>
        </table>
       </div>

        <table class="ContentPage" id="ContentPage" runat="server">
           
             <tr style="height:20px">
                <td>
                <div id="ContentPagemargin" runat="server" style="width: 1600px">
                   <div id="PageSection1" style="text-align:left;" >
                           <br />
                            
                              <table width="90%">
                                <tr style="height: 20px">
                                <td>
                                    <div class="divMargin" style="width: 1040px">
                                         <asp:ScriptManager ID="scrpt1" runat="server" AsyncPostBackTimeOut="4500"></asp:ScriptManager>
                                         <asp:UpdatePanel ID="upd1" runat="server">
                                              <ContentTemplate>
                                                               <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                                                     <ProgressTemplate>
                                                                                      <div class="divUpdateprogress">
                                                                                           <table style="margin-left:230px;">
                                                                                                  <tr>
                                                                                                      <td>
                                                                                                          <img alt="" src="../../../Images/Loading4.gif" height="50px" />
                                                                                                      </td>
                                                                                                      <td>
                                                                                                          <b style="color: Red;">Working...</b>
                                                                                                      </td>
                                                                                                  </tr>
                                                                                           </table>
                                                                                      </div>
                                                                     </ProgressTemplate>
                                                               </asp:UpdateProgress>
                                                               <table id="Table1" runat="server" style="width:100%">
                                                                      <tr style="height: 20px">
                                                                          <td>
                                                                              <div id="Div1" runat="server">
                                                                                   <div style="text-align: left;">
                                                                                        <br />
                                                                                        <div>
                                                                                             <table></table>
                                                                                        </div>
                                                                                        <table width="100%" style="margin-top: -17px;">
                                                                                               <tr class="AlterNateColor4">
                                                                                                   <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                                                                       Case Approval Tool
                                                                                                   </td>
                                                                                               </tr>
                                                                                              <%-- <tr class="AlterNateColor1">
                                                                                                   <td colspan="2">
                                                                                                       <asp:Label ID="lblMod" runat="server" Text="Module:" CssClass="NormalLabel" Style="margin-left: 10px;"></asp:Label>
                                                                                                       <asp:DropDownList ID="ddlmodule" runat="server" Width="120px" CssClass="DropDown" Style="margin-left: 15px;" AutoPostBack="true">
                                                                                                             <asp:ListItem Value="E1">E1</asp:ListItem>
                                                                                                             <asp:ListItem Value="S1">S1</asp:ListItem>
                                                                                                             <asp:ListItem Value="E2">E2</asp:ListItem>
                                                                                                             <asp:ListItem Value="S2">S2</asp:ListItem>
                                                                                                       </asp:DropDownList>
                                                                                                       <asp:Label ID="lblGroup" runat="server"  CssClass="NormalLbel" Width="120px" Style="margin-left: 100px;" Text='Selected Cases:'></asp:Label>
                                                                                                       <asp:LinkButton ID="LnkGroup" runat="server" Width="240px" Style="color: Black; font-family:Verdana; font-size: 12px; margin-left: 4px;"  ></asp:LinkButton>
                                                                                                   </td>
                                                                                               </tr>--%>
                                                                                               <tr class="AlterNateColor2">
                                                                                                   <td colspan="2">
                                                                                                       <table width="100%" cellspacing="0" cellpadding="0">
                                                                                                              <tr class="AlterNateColor2">
                                                                                                                  <td style="width: 140px;" align="right">
                                                                                                                      <asp:Label ID="lblPageSize" runat="server" Text="Page Size:" CssClass="NormalLabel" Style="margin-left: 5px; text-align: right;"></asp:Label>
                                                                                                                      <asp:DropDownList ID="ddlPageCount" runat="server" Width="55px" CssClass="DropDown" AutoPostBack="true">
                                                                                                                           <asp:ListItem Value="10">10</asp:ListItem>
                                                                                                                           <asp:ListItem Value="25">25</asp:ListItem>
                                                                                                                           <asp:ListItem Value="50">50</asp:ListItem>
                                                                                                                           <asp:ListItem Value="100">100</asp:ListItem>
                                                                                                                           <asp:ListItem Value="200">200</asp:ListItem>
                                                                                                                           <asp:ListItem Value="300">300</asp:ListItem>
                                                                                                                           <asp:ListItem Value="400">400</asp:ListItem>
                                                                                                                           <asp:ListItem Value="500">500</asp:ListItem>
                                                                                                                           <asp:ListItem Value="1000">1000</asp:ListItem>
                                                                                                                      </asp:DropDownList>
                                                                                                                  </td>
                                                                                                                  <td width="1px" style="background-color: White;">
                                                                                                                  </td>
                                                                                                                  <td>
                                                                                                                      <span style="margin-left: 0px; font-weight: bold; font-size: 11px;"><b>Cases Found =</b></span>
                                                                                                                      <asp:Label ID="lblCF" runat="server" Text="5" ForeColor="Red" Font-Bold="true" Font-Size="11px" Width="50px"></asp:Label>
                                                                                                                      <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel" Style="margin-left: 5px;"></asp:Label>
                                                                                                                      <asp:TextBox ID="txtKey" runat="server" CssClass="MediumTextBox" Style="text-align: left; width: 280px" MaxLength="100"></asp:TextBox>
                                                                                                                      <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" onmouseover="Tip('Search')" onmouseout="UnTip()" />
                                                                                                                  </td>
                                                                                                              </tr>
                                                                                                       </table>
                                                                                                   </td>
                                                                                               </tr>
                                                                                               <tr class="AlterNateColor1">
                                                                                                   <td colspan="3">
                                                                                                       <table>
                                                                                                              <tr>
                                                                                                                  <td>
                                                                                                                       <div style="width: 1550px; overflow: auto; height:500px; margin-left:5px;">
                                                                                                                            <asp:GridView CssClass="GrdUsers" runat="server" ID="grdCase" DataKeyNames="ID" AllowPaging="true" PageSize="11" AllowSorting="True" 
                                                                                                                            AutoGenerateColumns="False" Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" 
                                                                                                                            BorderStyle="None" BorderWidth="1px">
                                                                                                                                  <PagerSettings Position="Top" />
                                                                                                                                  <PagerTemplate>
                                                                                                                                                 <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First" style="color:Black;">First</asp:LinkButton>
                                                                                                                                                 <asp:Label ID="pmore" runat="server" Text="..."></asp:Label>
                                                                                                                                                 <asp:LinkButton ID="LinkButton2" runat="server" style="color:#284E98;" CommandName="Page" CommandArgument="Prev">Previous</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p0" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p1" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p2" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:Label ID="CurrentPage" runat="server" Text="Label" style="color:Red;"></asp:Label>
                                                                                                                                                 <asp:LinkButton ID="p4" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p5" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p6" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p7" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p8" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p9" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="p10" runat="server" style="color:Blue;">LinkButton</asp:LinkButton>
                                                                                                                                                 <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next" style="color:#284E98;">Next</asp:LinkButton>
                                                                                                                                                 <asp:Label ID="nmore" runat="server" Text="..."></asp:Label>
                                                                                                                                                 <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page" CommandArgument="Last" style="color:Black;">Last</asp:LinkButton>
                                                                                                                                  </PagerTemplate>
                                                                                                                                  <FooterStyle BackColor="#CCCC99" />
                                                                                                                                  <RowStyle BackColor="#F7F7DE" />
                                                                                                                                  <PagerStyle CssClass="AlterNateColor1" Font-Bold="true" HorizontalAlign="Left" Width="1800px"   />
                                                                                                                                  <Columns>
                                                                                                                                            <asp:TemplateField HeaderText="ID" Visible="false" SortExpression="ID">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="UserCASEID" runat="server" Text='<%# bind("ID")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="USERID" Visible="false" SortExpression="UserId">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblUserId" runat="server" Text='<%# bind("UserId")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="SELECT CASES">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                               <asp:Label ID="lblStatusID" runat="server" Text='<%# bind("STATUSID")%>' style="display:none" ></asp:Label>
                                                                                                                                                               <asp:CheckBox ID="chkIsAdded"  Enabled="true" runat="server" />
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="30px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="ACCESS SHARED?" SortExpression="SACCESS">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label  ID="lblShareAccess" Width="50px" runat="server" Text='<%# bind("SACCESS")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="50px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="USER" HeaderStyle-HorizontalAlign="Left" SortExpression="USERNAME">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblUser" Width="200px" runat="server" Text='<%# bind("USERNAME")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="PRODUCT FORMAT" HeaderStyle-HorizontalAlign="Left" SortExpression="PRODUCTDES">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblProdFormat"  Width="120px" runat="server" Text='<%# bind("PRODUCTDES")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="GROUPID" HeaderStyle-HorizontalAlign="Left" Visible="false" SortExpression="GROUPID">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblGroupID" Width="120px" runat="server" Text='<%# bind("GROUPID")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="STATUS" HeaderStyle-HorizontalAlign="center" SortExpression="STATUS" ItemStyle-HorizontalAlign="Left">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:LinkButton ID="lnkGroId" runat="server" Width="120px" style="color:Black ;Font-family:Verdana;font-size:12px;text-decoration:underline;" Text='<%# bind("STATUS")%>'></asp:LinkButton>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <HeaderStyle HorizontalAlign="Left" />
                                                                                                                                                  <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="MODEL ID" HeaderStyle-HorizontalAlign="Left" SortExpression="CASEID">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblCaseID" Width="60px" runat="server" Text='<%# bind("CASEID")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="60px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="DESCRIPTION 1" HeaderStyle-HorizontalAlign="Left" SortExpression="CASEDE1">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblPFormat" Width="180px" runat="server" Text='<%# bind("CASEDE1")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="DESCRIPTION 2" HeaderStyle-HorizontalAlign="Left" SortExpression="CASEDE2">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblUfeature" Width="180px" runat="server" Text='<%# bind("CASEDE2")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="LONG DESCRIPTION" HeaderStyle-HorizontalAlign="Left" Visible="true" SortExpression="CASEDE3">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lblCaseDes3"  Width="300px" runat="server" Text='<%# bind("CASEDE3")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="400px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                            <asp:TemplateField HeaderText="CREATION DATE" HeaderStyle-HorizontalAlign="Left" SortExpression="CREATIONDATE">
                                                                                                                                                  <ItemTemplate>
                                                                                                                                                                <asp:Label ID="lbCDate" runat="server" Width="75px" Text='<%# bind("CREATIONDATE")%>'></asp:Label>
                                                                                                                                                  </ItemTemplate>
                                                                                                                                                  <ItemStyle Width="75px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                            </asp:TemplateField>

                                                                                                                                             <asp:TemplateField HeaderText="LAST UPDATE" HeaderStyle-HorizontalAlign="Left" SortExpression="SERVERDATE">
                                                                                                                                                   <ItemTemplate>
                                                                                                                                                                 <asp:Label ID="lbUDate" Width="75px" runat="server" Text='<%# bind("SERVERDATE")%>'></asp:Label>
                                                                                                                                                   </ItemTemplate>
                                                                                                                                                   <ItemStyle Width="75px" Wrap="true" HorizontalAlign="Left" />
                                                                                                                                             </asp:TemplateField>
                                                                                                                                  </Columns>
                                                                                                                                  <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                                                                                  <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                                                                                  <AlternatingRowStyle BackColor="White" />
                                                                                                                            </asp:GridView>
                                                                                                                       </div>
                                                                                                                  </td>
                                                                                                              </tr>
                                                                                                       </table>
                                                                                                   </td>
                                                                                               </tr>
                                                                                               <tr class="AlternateColor1" style="height: 10px">
                                                                                                   <td colspan="4">
                                                                                                       <asp:Button ID="btnApprove" runat="server" Text="Approve" CssClass="ButtonWMarigin" OnClientClick="return ApproveCheck();" onmouseover="Tip('Approve Cases')"
                                                                                                       onmouseout="UnTip()" Style="margin-left: 10px" />
                                                                                                         <asp:Button ID="btnEvaluate" runat="server" Text="Evaluate" CssClass="ButtonWMarigin" OnClientClick="return EvaluateCheck();" onmouseover="Tip('Approve Cases')"
                                                                                                       onmouseout="UnTip()" Style="margin-left: 10px" />
                                                                                                       <asp:Button ID="btnDisApprove" runat="server" Text="Disapprove" CssClass="ButtonWMarigin" OnClientClick="return DisApproveCheck();" onmouseover="Tip('Disapprove Cases')"
                                                                                                       onmouseout="UnTip()" Style="margin-left: 10px;" />
                                                                                                   </td>
                                                                                               </tr>
                                                                                        </table>
                                                                                        <br />
                                                                                        <div>
                                                                                        </div>
                                                                                        <br />
                                                                                        <br />
                                                                                        <br />
                                                                                   </div>
                                                                              </div>
                                                                          </td>
                                                                      </tr>
                                                               </table>
                                                               <asp:HiddenField ID="hidCaseid1" runat="server" />
                                                               <asp:HiddenField ID="hidCaseid2" runat="server" />
                                                               <asp:HiddenField ID="hidGroupID" runat="server" />
                                                               <asp:HiddenField ID="hvUserGrd" runat="server" />
                                                               <asp:HiddenField ID="hidApprove" runat="server" />
                                                               <asp:HiddenField ID="hidDisApprove" runat="server" />
                                                               <asp:HiddenField ID="hidGroupName" runat="server" />
                                                               <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
                                              </ContentTemplate>
                                         </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr style="height: 50px">
                                 <td></td>
                            </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                                </td>
                            </tr>   
                            </table>    
                            <br />
                             </div>
                             </div>>
                             </td>
                             </tr>
                             </table>
                             

            
         <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
          </div>    
              
          
    </form>
    <script type="text/JavaScript" src="../Javascript/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../Javascript/tip_balloon.js"></script>
</body>
</html>
