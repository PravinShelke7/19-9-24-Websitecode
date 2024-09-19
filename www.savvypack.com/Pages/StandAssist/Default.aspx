<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_StructAssist_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Structure Manager</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .SBAModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url(   '../../Images/SavvyPackStructureAssistantR01.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>

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

    <script type="text/JavaScript">
        javascript: window.history.forward(1);
        
        function ShowNotesWindow(Page) {

            var width = 500;
            var height = 260;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';

            newwin = window.open(Page, 'Chat', params);


        }
        
           function openBNotesWindow() 
           {
                    
            var notes = document.getElementById("hidGNotes").value
            var grpId = document.getElementById("<%=hidGGrpId.ClientID %>").value;
            if (notes == "") {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=BGNOTES&File=NOTE&Spons=N");
            }
            else {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=BGNOTES&File=NOTE&Spons=N");
            }
            return false;
        }
          function openNotesWindow() {
            
              var notes = document.getElementById("hidNotes").value
            var caseId = document.getElementById("<%=hidApprovedCase.ClientID %>").value;
            var grpId = document.getElementById("<%=hidGGrpId.ClientID %>").value;
            if (notes == "") {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=BNOTES&File=NOTE&Spons=N");
            }
            else {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=BNOTES&File=NOTE&Spons=N");
            }
            return false;
        }
        
         function openPGNotesWindow(Type) 
         {
          if (Type == "PROP") {
            var notes = document.getElementById("hidGPNotes").value
            var grpId = document.getElementById("<%=hidGrpId.ClientID %>").value;
            if (notes == "") {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=GNOTES&File=NOTE&Spons=N");
            }
            else {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=GNOTES&File=NOTE&Spons=N");
            }
            }
            else
            {
               var notes = document.getElementById("hidCompGrpNotes").value
                var grpId = document.getElementById("<%=hidCGrpId.ClientID %>").value;
                if (notes == "") {

                    ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=CGNOTES&File=NOTE&Spons=N");
                }
                else {

                    ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=CGNOTES&File=NOTE&Spons=N");
                }
            }
            return false;
        }
        
         function openPNotesWindow(Type) 
         {
          if (Type == "PROP") {
            var notes = document.getElementById("hidPNotes").value
            var caseId = document.getElementById("<%=hidPropCase.ClientID %>").value;
            var grpId = document.getElementById("<%=hidGrpId.ClientID %>").value;
            
            if (notes == "") {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=PNOTES&File=NOTE&Spons=N");
            }
            else {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=PNOTES&File=NOTE&Spons=N");
            }
            }
            else
            {
                var notes = document.getElementById("hidCompNotes").value
                var caseId = document.getElementById("<%=hidCompnyCase.ClientID %>").value;
                var grpId = document.getElementById("<%=hidCGrpId.ClientID %>").value;

                if (notes == "") {
                    ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=CNOTES&File=NOTE&Spons=N");
                }
                else {
                    ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=CNOTES&File=NOTE&Spons=N");
                }
            }
            return false;   
        }
        
        function Count(text) 
        {
          var a=/\<|\>|\&#|\\/;
          if ((document.getElementById("txtCaseDe3").value.match(a) != null))  
          {
             alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Notes . Please choose alternative characters.");
             var object = document.getElementById(text.id)  //get your object
             object.focus(); //set focus to prevent jumping
             object.value = text.value.replace(new RegExp("<", 'g'), "");
             object.value = text.value.replace(new RegExp(">", 'g'), "");
             object.value = text.value.replace(/\\/g, '');
             object.value = text.value.replace(new RegExp("&#", 'g'), "");
             object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
             return false;          
          }
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 500; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength)
            {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }
        
          function CompCount(text) 
        {
          var a=/\<|\>|\&#|\\/;
          if ((document.getElementById("txtCompDes3").value.match(a) != null))  
          {
             alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Notes . Please choose alternative characters.");
             var object = document.getElementById(text.id)  //get your object
             object.focus(); //set focus to prevent jumping
             object.value = text.value.replace(new RegExp("<", 'g'), "");
             object.value = text.value.replace(new RegExp(">", 'g'), "");
             object.value = text.value.replace(/\\/g, '');
             object.value = text.value.replace(new RegExp("&#", 'g'), "");
             object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
             return false;          
          }
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 500; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength)
            {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }
        
        
        function CheckSP(text)
        {
          var a=/\<|\>|\&#|\\/;
          if ((document.getElementById("txtCaseDe1").value.match(a) != null)||(document.getElementById("txtCaseDe2").value.match(a) != null)||(document.getElementById("txtApp").value.match(a) != null))  
          {
             var object = document.getElementById(text.id)  //get your object
             if (text.id=="txtCaseDe1")
	         {
	     	  alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Descriptor1. Please choose alternative characters.");
	         }
	         else if (text.id=="txtCaseDe2")
	         {
	     	  alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Descriptor2. Please choose alternative characters.");
	         }
	         else if (text.id=="txtApp")
	         {
	     	  alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Application. Please choose alternative characters.");
	         }
             object.focus(); //set focus to prevent jumping
             object.value = text.value.replace(new RegExp("<", 'g'), "");
             object.value = text.value.replace(new RegExp(">", 'g'), "");
             object.value = text.value.replace(/\\/g, '');
             object.value = text.value.replace(new RegExp("&#", 'g'), "");
             object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
             return false;         
          }
        }
        
         function CompCheckSP(text)
        {
          var a=/\<|\>|\&#|\\/;
          if ((document.getElementById("txtCompDes1").value.match(a) != null)||(document.getElementById("txtCompDes2").value.match(a) != null)||(document.getElementById("txtCompApp").value.match(a) != null))  
          {
             var object = document.getElementById(text.id)  //get your object
             if (text.id=="txtCompDes1")
	         {
	     	  alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Descriptor1. Please choose alternative characters.");
	         }
	         else if (text.id=="txtCompDes2")
	         {
	     	  alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Descriptor2. Please choose alternative characters.");
	         }
	         else if (text.id=="txtCompApp")
	         {
	     	  alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Application. Please choose alternative characters.");
	         }
             object.focus(); //set focus to prevent jumping
             object.value = text.value.replace(new RegExp("<", 'g'), "");
             object.value = text.value.replace(new RegExp(">", 'g'), "");
             object.value = text.value.replace(/\\/g, '');
             object.value = text.value.replace(new RegExp("&#", 'g'), "");
             object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
             return false;         
          }
        }
        
        function openWindow() {
             var File = document.getElementById("hidFileName").value
            var grpId = document.getElementById("<%=hidGGrpId.ClientID %>").value;

            if (File == "") {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=NOTES&File=N&Spons=GRP");
            }
            else {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=N&GId=" + grpId + "&Type=NOTES&File=" + File + "&Spons=GRP");
            }
            return false;
            
        }
        function openWindowN() {
           
                window.open("Tools/ManageGroups.aspx");
                return false;

            }
            function openWindowU(page) 
            {
                window.open(page);
                return false;
            }

		  function openBWindow() {
            var File = document.getElementById("hidBFileName").value
            var grpId = document.getElementById("<%=hidGGrpId.ClientID %>").value;
            var caseId = document.getElementById("<%=hidApprovedCase.ClientID %>").value;

            if (File == "") {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=NOTES&File=N&Spons=STRUCT");
            }
            else {

                ShowNotesWindow("PopUp/NotesPopUp.aspx?Id=" + caseId + "&GId=" + grpId + "&Type=NOTES&File=" + File + "&Spons=STRUCT");
            }
            return false;
            
        }
        function ShowPopWindow(Page, Type) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1200;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height;
            params += ', top=' + top + ', left=' + left;
            params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';

            if (Type == "") {
                pn = Page + "&GrpId=0 ";
            }
            else if (Type == "BASE") {
                var grpId = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId + " ";
            }
            else if (Type == "PROP") {
                var grpId1 = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes1 = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId1 + " ";
            }
            newwin = window.open(pn, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;

        }
        function ShowPopup(Page, Type) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1165;
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
            if (Type == "") {
                pn = Page + "&GrpId=0 ";
            }
            else if (Type == "AP") {
                var grpId = document.getElementById("<%=hidGGrpId.ClientID %>").value;
                var grpDes = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId + " ";
            }
            else if (Type == "PRP") {
                var grpId1 = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes1 = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId1 + " ";
            }
              else if (Type == "CPRP") {
                var grpId2 = document.getElementById("<%=hidCGrpId.ClientID %>").value;
                var grpDes2 = document.getElementById("<%=hidCGrpDes.ClientID %>").value;
		        var CaseId = document.getElementById("<%=hidCompnyCase.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId2 + "&CaseID=" + CaseId + "";
            }
            //alert(pn);

            newwin = window.open(pn, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, 'NewWindow', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

        }
        function Message() {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
            {
                var statudId = document.getElementById("<%=hidPropCaseSt.ClientID %>").value;
                // alert(statudId);
                if (statudId != "") {
                    alert('You have already submitted this case for addition to approved cases. You can not submit this case again.');
                    return false;
                }
                {
                    msg = "You are going to submit case " + Case + " for addition to approved cases. Do you want to proceed?"

                    if (confirm(msg)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                }
            }

        }

       
        function MakeVisible1(id) {

            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
            else {
                objItemElement = document.getElementById(id)
                objItemElement.style.display = "inline"
                //                GetCaseDe();
                return false;
            }




        }
        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            GetCaseDe();
            return false;





        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }

        function GetCaseDe() {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
            else {
                var combo1 = document.getElementById("hidPropCase");

                var val = combo1.options[combo1.selectedIndex].text
                var CaseDe = val.split("=");

                document.getElementById("txtCaseDe1").value = trimAll(CaseDe[1].replace("UNIQUE FEATURES", ""));
                document.getElementById("txtCaseDe2").value = trimAll(CaseDe[2]);
            }
        }

        function GetCaseId() {

            objItemElement = document.getElementById("hidPropCase")

            document.getElementById("txtCaseid").value = objItemElement.value




            if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) {
                alert("Structure Descriptor1 cannot be blank");
                return false;
            }
            else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) {
                alert("Structure Descriptor2 cannot be blank");
                return false;
            }
            else {
                return confirm("Do you want to rename Structure " + objItemElement.value + " ?");

            }

            return false;


        }
        
        function GetCompCaseId() {

            objItemElement = document.getElementById("hidCompnyCase")

            document.getElementById("txtCaseid").value = objItemElement.value




            if (document.getElementById("txtCompDes1").value.split(' ').join('').length == 0) {
                alert("Structure Descriptor1 cannot be blank");
                return false;
            }
            else if (document.getElementById("txtCompDes2").value.split(' ').join('').length == 0) {
                alert("Structure Descriptor2 cannot be blank");
                return false;
            }
            else {
                return confirm("Do you want to rename Structure " + objItemElement.value + " ?");

            }

            return false;


        }

        function trimAll(sString) {
            while (sString.substring(0, 1) == ' ') {
                sString = sString.substring(1, sString.length);
            }
            while (sString.substring(sString.length - 1, sString.length) == ' ') {
                sString = sString.substring(0, sString.length - 1);
            }
            return sString;
        }

        function Help() {
//            var width = 800;
//            var height = 400;
//            var left = (screen.width - width) / 2;
//            var top = (screen.height - height) / 2;
//            var URL
//            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
//            params += ', location=yes';
//            params += ', menubar=yes';
//            params += ', resizable=yes';
//            params += ', scrollbars=yes';
//            params += ', status=yes';
//            params += ', toolbar=yes';
//            URL = "../StandAssist/help/StructureAssist_Inst.pdf"
//            newwin = window.open(URL, 'Chart', params);
var width = 800;
           var height = 600;
           var left = (screen.width - width) / 2;
           var top = (screen.height - height) / 2;
           var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
           params += ', location=yes';
           params += ', menubar=yes';
           params += ', resizable=yes';
           params += ', scrollbars=yes';
           params += ', status=yes';
           params += ', toolbar=yes';
           newwin = window.open("../StandAssist/help/StructureAssist_Inst.pdf", "SAI", params);
            return false

        }
        function ShowToolTip(ControlId, Message) {

           // alert('sud');
            document.getElementById(ControlId).onmouseover = function () { Tip(Message); };
           document.getElementById(ControlId).onmouseout = function () { UnTip(''); };

        }
         function ShowToolTipN(ControlId, Message) {

           //alert('sud');

           document.getElementById(ControlId).onmouseover = function () { Tip(Message); };
           document.getElementById(ControlId).onmouseout = function () { UnTip(''); };

       }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <center>

        <script type="text/javascript" src="../../JavaScripts/wz_tooltip.js"></script>

        <div id="MasterContent">
            <div>
                <table class="SBAModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px; padding-bottom: 15px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                            runat="server" ToolTip="Update" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif"
                                            runat="server" ToolTip="Log Off" Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=StandAssist" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                            runat="server" ToolTip="Instructions" Visible="true" OnClientClick="return Help();" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgChart" ImageAlign="Middle" ImageUrl="~/Images/ChartN.gif"
                                            runat="server" ToolTip="Charts" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgFeedback" ImageAlign="Middle" ImageUrl="~/Images/FeedbackN.gif"
                                            runat="server" ToolTip="Feedback" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                            runat="server" ToolTip="Notes" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                        Structure Manager</div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <div>
                                <table style="width:100%">
                                    <tr>
                                        <td>
                                            <div id="IsBSManage" runat="server" visible="false" style="margin: 0px 5px 5px 5px;">
                                                <asp:HyperLink ID="lnkAdminTool" runat="server" Text="Manage Public Structures" CssClass="Link"
                                                    Target="_blank" Font-Bold="true" NavigateUrl="~/Pages/StandAssist/Tools/AdminTool.aspx"></asp:HyperLink>
                                            </div>
                                        </td>
                                        <td>
                                            <div id="IsBManage" runat="server" style="margin: 0px 5px 5px 5px;">
                                                <asp:HyperLink ID="lnkBGroupTool" runat="server" Text="Manage Public Groups" CssClass="Link"
                                                    Font-Bold="true" NavigateUrl="~/Pages/StandAssist/Tools/ManageGroup.aspx" Target="_blank"></asp:HyperLink>
                                            </div>
                                        </td>
										   <td align="right" style="margin: 0px 5px 5px 5px;">
                                            <div id="ManageUser" runat="server">
                                                <asp:HyperLink ID="HyperLink1" runat="server" Text="Manage Structure Assistant Users" style="color:#cc0000;font-size:14px;" CssClass="Link"
                                                    Font-Bold="true" NavigateUrl="~/Pages/StandAssist/StructUsers.aspx" Target="_blank"></asp:HyperLink>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="PageHeading" style="width: 80%">
                                Search the Public Structure Library
                            </div>
                            <div id="IsAdmin" runat="server">
                                <table width="90%">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Select Groups Of Structures
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td id="td1" runat="server">
                                            <asp:LinkButton ID="lnkAllBGrps" Style="font-size: 14px; padding-left: 10px" runat="server"
                                                CssClass="Link" Width="300px" OnClientClick="return ShowPopup('PopUp/GetAllGroups.aspx?Des=lnkAllBGrps&Id=hidGGrpId&IdD=hidGGroupReportD&Type=BASE&PTYPE=DEFAULTB&File=hidFileName&Notes=hidGNotes&SNotes=hidNotes','AP');">All Structures</asp:LinkButton>
                                            <asp:ImageButton ID="btnNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                                Height="19px" Width="19px" OnClientClick="return openBNotesWindow();" runat="server"
                                                ToolTip="No Notes Available" Style="margin-left: 360px;" Visible="true" />
                                            <asp:ImageButton ID="btnSponsorM" ImageAlign="Middle" ImageUrl="~/Images/Wizard.gif"
                                                Height="19px" Width="19px" OnClientClick="return openWindow() ;" runat="server"
                                                ToolTip="No Sponsor message Available" Visible="true" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top: 10px;">
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Select Structures
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:LinkButton ID="lnkApprovedCase" Style="font-size: 14px; padding-left: 10px"
                                                runat="server" CssClass="Link" Width="300px" OnClientClick="return ShowPopup('PopUp/CaseDetails.aspx?Des=lnkApprovedCase&Id=hidApprovedCase&IdD=hidApprovedCaseD&SponsBy=hidSpons&Type=BASE&PTYPE=DEFAULTBS&File=hidBFileName&Notes=hidNotes','AP');">Select Structure</asp:LinkButton>
                                            <asp:Label ID="lblBcase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                Text="Currently you have no Base Cases defined. You can create a Case with the Admin Tools."></asp:Label>
                                            <asp:ImageButton ID="btnSNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                                Height="19px" Width="19px" OnClientClick="return openNotesWindow() ;" runat="server"
                                                ToolTip="No Notes Available" Visible="true" Style="margin-left: 360px; margin-top: 0px;" />
                                            <asp:ImageButton ID="btnSSMessage" ImageAlign="Middle" ImageUrl="~/Images/Wizard.gif"
                                                Height="19px" Width="19px" OnClientClick="return openBWindow() ;" runat="server"
                                                ToolTip="No Sponsor message Available" Visible="true" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnBCase" runat="server" CssClass="Button" Text="Start a Structure"
                                                Style="margin-left: 0px;" />
                                            <asp:Button ID="btnBCaseSearch" runat="server" Text="Case Search" CssClass="Button"
                                                OnClientClick="return ShowPopWindow('PopUp/CaseSearch.aspx?Id=hidApprovedCase','BASE');"
                                                Visible="false"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            
                             <br />
                            <div id="IsCompanyAdmin" runat="server">
                                <div class="PageHeading" style="width: 80%">
                                    Search the Company Structure Library
                                </div>
                                <table width="90%">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Select Groups Of Structures
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td id="td3" runat="server">
                                            <asp:LinkButton ID="lnkCompGrp" Style="font-size: 14px; padding-left: 10px" runat="server"
                                                CssClass="Link" Width="500px" OnClientClick="return ShowPopup('PopUp/GetAllGroups.aspx?Des=lnkCompGrp&Id=hidCGrpId&IdD=hidCGrpDes&Type=CPROP&PTYPE=DEFAULTCPPS&Notes=hidCompGrpNotes','CPRP');">All Structures</asp:LinkButton>
                                                <asp:ImageButton ID="btnCompGNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                                Height="19px" Width="19px" OnClientClick="return openPGNotesWindow('CPROP');"
                                                runat="server" ToolTip="No Notes Available" Style="margin-left: 160px;" Visible="true" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" id="trCGroup" runat="server" >
                                        <td>
                                            <asp:Button ID="btnCompGManage" runat="server" CssClass="Button" Text="Manage Groups"
                                                OnClientClick="return openWindowU('Tools/ManageGroups.aspx?Type=CPROP') ;" Style="margin-left: 0px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding-top:5px;">
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Select Structures
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:LinkButton ID="lnkCompCase" Style="font-size: 14px; padding-left: 10px" runat="server"
                                                CssClass="Link" Width="400px" OnClientClick="return ShowPopup('PopUp/CompanyCaseDetails.aspx?Des=lnkCompCase&Id=hidCompnyCase&IdD=hidCompnyCaseD&SponsBy=hidSpons&Type=CPROP&PTYPE=DEFAULTCPS&Notes=hidCompNotes','CPRP');">Select Structure</asp:LinkButton>
                                            <asp:ImageButton ID="btnCompNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                                Height="19px" Width="19px" OnClientClick="return openPNotesWindow('CPROP') ;"
                                                runat="server" ToolTip="No Notes Available" Visible="true" Style="margin-left: 260px;
                                                margin-top: 0px;" />
                                            <asp:Label ID="lblCPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                Text="Currently your company have no Company Structures defined. You can create a Structure in the Toolbox."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnCompCase" runat="server" CssClass="Button" Text="Start a Structure"
                                                Style="margin-left: 0px; width: 120px;" />
                                            <asp:Button ID="btnCompSRename" runat="server" Text="Rename" CssClass="Button" Style="margin-left: 20px;
                                                width: 80px;"></asp:Button>
                                            <asp:Button ID="btnCompSManage" runat="server" ToolTip="Manage Structure" CssClass="Button"
                                                OnClientClick="return openWindowU('Tools/CompanyAdmin.aspx') ;" Text="Manage Structures"
                                                Style="margin-left: 20px; width: 120px;" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <div style="margin-left: 20px; display: none" id="divComRename" runat="server">
                                                <table width="90%" style="padding-left: 20px">
                                                    <tr class="AlterNateColor1">
                                                        <td align="right">
                                                            <b>Structure Descriptor1:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompDes1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                                font-size: 11px; width: 230px" MaxLength="25" onChange="javascript:CompCheckSP(this);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr class="AlterNateColor1">
                                                        <td align="right">
                                                            <b>Structure Descriptor2:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompDes2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                                font-size: 11px; width: 230px" MaxLength="25" onChange="javascript:CompCheckSP(this);"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr class="AlterNateColor1">
                                                        <td align="right">
                                                            <b>Structure Notes:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompDes3" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                                font-size: 11px;" onChange="javascript:CompCount(this);" 
                                                                TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr class="AlterNateColor1">
                                                        <td align="right">
                                                            <b>Application:</b>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCompApp" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                                            MaxLength="25" Width="230px"  onChange="javascript:CompCheckSP(this);"></asp:TextBox>
                                                                
                                                        </td>
                                                    </tr>
                                                    <tr class="AlterNateColor1">
                                                        <td>
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnCompUpdate" runat="server" Text="Update" CssClass="Button" Style="margin-left: 0px;"
                                                                OnClientClick="return GetCompCaseId();"></asp:Button>
                                                            <asp:Button ID="btnCompCancel" runat="server" Text="Cancel" CssClass="Button" Style="margin-left: 0px;">
                                                            </asp:Button>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div class="PageHeading" style="width: 80%">
                                Design in Your Proprietary Space
                            </div>
                            <table width="90%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Select Groups Of Structures
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td id="td2" runat="server">
                                        <asp:LinkButton ID="lnkAllGrps" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="300px" OnClientClick="return ShowPopup('PopUp/GetAllGroups.aspx?Des=lnkAllGrps&Id=hidGrpId&IdD=hidGroupReportD&Type=PROP&PTYPE=DEFAULTP&Notes=hidGPNotes&SNotes=hidPNotes','PRP');">All Structures</asp:LinkButton>
                                        <asp:ImageButton ID="btnPGNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                            Height="19px" Width="19px" OnClientClick="return openPGNotesWindow('PROP');" runat="server"
                                            ToolTip="No Notes Available" Style="margin-left: 363px;" Visible="true" />
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnManageGroup" runat="server" CssClass="Button" Text="Manage Groups"
                                            OnClientClick="return openWindowU('Tools/ManageGroups.aspx?Type=PROP') ;" Style="margin-left: 0px;" />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="padding-top: 10px;">
                                    </td>
                                </tr>
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Select Structures
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:LinkButton ID="lnkPropCase" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="400px" OnClientClick="return ShowPopup('PopUp/CaseDetails.aspx?Des=lnkPropCase&Id=hidPropCase&IdD=hidPropCaseD&SponsBy=hidSpons&Type=PROP&PTYPE=DEFAULTPS&Notes=hidPNotes','PRP');">Select Structure</asp:LinkButton>
                                        <asp:ImageButton ID="btnPNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                            Height="19px" Width="19px" OnClientClick="return openPNotesWindow('PROP');" runat="server"
                                            ToolTip="No Notes Available" Style="margin-left: 262px;" Visible="true" />
                                            <%-- <asp:ImageButton ID="btnPNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                            Height="19px" Width="19px" OnClientClick="return openPNotesWindow();" runat="server"
                                            ToolTip="No Notes Available" Style="margin-left: 282px;" Visible="true" />--%>
                                        <asp:Label ID="lblPcase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                            Text="Currently you have no Proprietary Cases defined. You can create a Case in the Toolbox."></asp:Label>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnPCase" runat="server" CssClass="Button" Text="Start a Structure"
                                            Style="margin-left: 0px; width: 120px;" />
                                        <asp:Button ID="btRename" runat="server" Text="Rename" CssClass="Button" Style="margin-left: 20px;
                                            width: 80px;" ToolTip="Rename this Structure"></asp:Button>
                                        <asp:Button ID="btnToolBox" runat="server" CssClass="Button"
                                            OnClientClick="return openWindowU('Tools/Tool.aspx') ;" Text="Manage Structures"
                                            Style="margin-left: 20px; width: 120px;" />
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <div style="margin-left: 20px; display: none" id="divRename" runat="server">
                                            <table width="90%" style="padding-left: 20px">
                                                <tr class="AlterNateColor1">
                                                    <td align="right">
                                                        <b>Structure Descriptor1:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                            font-size: 11px; width: 230px" MaxLength="25" onChange="javascript:CheckSP(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="AlterNateColor1">
                                                    <td align="right">
                                                        <b>Structure Descriptor2:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                            font-size: 11px; width: 230px" MaxLength="25" onChange="javascript:CheckSP(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="AlterNateColor1">
                                                    <td align="right">
                                                        <b>Structure Notes:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaseDe3" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                            font-size: 11px;" onChange="javascript:Count(this);" TextMode="MultiLine" Height="100px"
                                                            Width="489px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="AlterNateColor1">
                                                    <td align="right">
                                                        <b>Structure Application:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtApp" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                                            MaxLength="25" Width="230px" onChange="javascript:CheckSP(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr class="AlterNateColor1">
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnUpdate" runat="server" Text="Rename" onmouseover="Tip('Rename this Structure')"
                                                            onmouseout="UnTip()" CssClass="Button" Style="margin-left: 0px;" OnClientClick="return GetCaseId();">
                                                        </asp:Button>
                                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Button" onmouseover="Tip('Cancel')"
                                                            onmouseout="UnTip()" Style="margin-left: 0px;"></asp:Button>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <table cellpadding="0" cellspacing="0" border="0" style="width: 90%; padding-left: 10px;
                                padding-right: 10px; margin-top: 5px;">
                            </table>
                            <br />
                            <br />
                        </div>
                    </div>
                </td>
            </tr>
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
        <%--<div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>--%>
        <div id="AlliedLogo">
          <table>
           <tr>
                <td class="PageSHeading" align="center">
                    <table style="width: 845px; background-color: #edf0f4;">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           </table>
        </div>        
    </center>
    <asp:HiddenField ID="txtCaseid" runat="server" />
    <asp:HiddenField ID="hidApprovedCase" runat="server" />
    <asp:HiddenField ID="hidPropCase" runat="server" />
    <asp:HiddenField ID="hidApprovedCaseD" runat="server" />
    <asp:HiddenField ID="hidSpons" runat="server" />
    <asp:HiddenField ID="hidPropCaseD" runat="server" />
    <asp:HiddenField ID="hidPropCaseSt" runat="server" />
    <asp:HiddenField ID="hidCaseID" runat="server" />
    <asp:HiddenField ID="hidGrpId" runat="server" />
    <asp:HiddenField ID="hidGrpDes" runat="server" />
    <asp:HiddenField ID="hidGroupReportD" runat="server" />
    <asp:HiddenField ID="hidRptDes" runat="server" />
    <asp:HiddenField ID="hidCompID" runat="server" />
    <asp:HiddenField ID="hidCompDes" runat="server" />
    <asp:HiddenField ID="hidFileName" runat="server" />
    <asp:HiddenField ID="hidBFileName" runat="server" />
    <asp:HiddenField ID="hidGGrpId" runat="server" />
    <asp:HiddenField ID="hidGGroupReportD" runat="server" />
    <asp:HiddenField ID="hidNotes" runat="server" />
    <asp:HiddenField ID="hidGNotes" runat="server" />
    <asp:HiddenField ID="hidPNotes" runat="server" />
    <asp:HiddenField ID="hidGPNotes" runat="server" />
    
    <asp:HiddenField ID="hidCaseId1" runat="server" />
     <asp:HiddenField ID="hidCaseId2" runat="server" />
     
     <asp:HiddenField ID="hidGrpId1" runat="server" />
     <asp:HiddenField ID="hidGrpId2" runat="server" />
     
       <asp:HiddenField ID="hidCGrpId" runat="server" />
    <asp:HiddenField ID="hidCGrpDes" runat="server" />
    <asp:HiddenField ID="hidCompnyCase" runat="server" />
    <asp:HiddenField ID="hidCompnyCaseD" runat="server" />
    <asp:HiddenField ID="hidCompGrpNotes" runat="server" />
    <asp:HiddenField ID="hidCompNotes" runat="server" />
    <asp:HiddenField ID="hidCaseId3" runat="server" />
    <asp:HiddenField ID="hidGrpId3" runat="server" />
    </form>
</body>
</html>
