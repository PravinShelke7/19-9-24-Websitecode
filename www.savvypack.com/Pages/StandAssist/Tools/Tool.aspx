<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tool.aspx.vb" Inherits="Pages_StandAssist_Tools_Tool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tools</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .SBAModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url(   '../../../Images/SavvyPackStructureAssistantR01.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>

    <script type="text/javascript">
        function CloseWindow() {
            window.open('', '_self', '');
            window.close();
        }
        function MessageG(Case, Flag) {




            if (Flag == 'UC') {
                var Case1 = document.getElementById(Case)
                //msg = "You are going to update the case description for case " + Case1.value + ". Do you want to proceed?"
                if (document.getElementById("txtGDES1").value.split(' ').join('').length == 0) {
                    alert("Descriptor1 cannot be blank");
                    return false;
                }
                else if (document.getElementById("txtGDES2").value.split(' ').join('').length == 0) {
                    alert("Descriptor2 cannot be blank");
                    return false;
                }
                else {

                    var createVal = document.getElementById("hidCreate").value;
                    // alert(createVal);
                    if (createVal == '0') {
                        msg = "You are going to update the description for Group " + Case1.value + ". Do you want to proceed?"
                    }
                    else {
                        msg = "You are going to create Group. Do you want to proceed?"
                    }

                }
            }


            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

        }
    </script>

    <script type="text/javascript">
        javascript: window.history.forward(1);
function ShowPopupUSR(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 600;
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
            return false;

        }
        function ShowPopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1200;
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
            return false;

        }

        function alertMessage() {
            alert('No Material for this Filter. Select another Filter');
        }
       function MakeVisible(Type, id) {
           // alert(Type);
            var CaseId = "";
            if (Type == "Prop") {
                CaseId = document.getElementById("<%=hidPropCase.ClientID %>").value;
            }
            else {
                CaseId = document.getElementById("<%=hidApprovedCase.ClientID %>").value;
            }
            if (CaseId == "0") {

                if (Type == "Prop") {
                    alert('Please select a Source Structure');
                }
                else {
                    alert('Please select a Source Structure');
                }
                return false;
            }
            else {

                objItemElement = document.getElementById(id)
                objItemElement.style.display = "inline"
                return false;
            }

        }
		
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
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
		
		function CreateMessage(Type, Flag) 
        {
          var CaseId="";
        
       if(Type=="Base")
         {
           CaseId= document.getElementById("<%=hidApprovedCase.ClientID %>").value;
         }
         else
         {
           CaseId = document.getElementById("<%=hidPropCase.ClientID %>").value;
         }
            
            if (CaseId == "0") 
            {
               if (Flag == 'NC') 
                    {
                           var msg = "You are going to create a new Structure. Do you want to proceed?"
                           if (confirm(msg)) 
                             {
                                   return true;
                             }
                            else 
                            {
                                return false;
                            }
                    }
                    {

                                if(Type=="Base")
                                 {
                                    alert('Please select a Structure');
                                 }
                                 else
                                 {
                                      alert('Please select a Structure');
                                 }
                         
                            return false;
                    }
                
            }
                else{
          var MaxCount="<%=Session("SBAMaxCaseCount")%>";       
          var TotalCount= document.getElementById("<%=hidTotalCaseCount.ClientID %>").value;

             if(eval(TotalCount)< eval(MaxCount))
             {
                  if (Flag == 'NC') 
                    {
                        var msg = "You are going to create a new Structure. Do you want to proceed?"
                    }
                    if (Flag == 'NCC') 
                    {
                        //var Case1 = document.getElementById(Case)
                        msg = "You are going to create a copy of Structure " + CaseId + ". Do you want to proceed?"
                    }
                     if (confirm(msg)) {
                        return true;
                    }
                    else {
                        return false;
                    }
               }
               else
               {
                   var msg;
                    if (Flag == 'NC') 
                    {
                      msg = "You cannot create a new Structure because you have reached your limit of "+MaxCount+" Structures.  Please contact SavvyPack Corporation (952-405-7500) to purchase more Structures.";
                    }
                    else if (Flag == 'NCC') 
                    {
                      msg = "You cannot copy this Structure because you have reached your limit of "+MaxCount+" Structures.  Please contact SavvyPack Corporation (952-405-7500) to purchase more Structures.";
                    }
                  
                  alert(msg);
                  return false;
               }
            }
        }
      function CreateMessageB(Case, Flag) 
        
        {
         var Case = document.getElementById("<%=hidApprovedCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
        if (Case == "0") {
                alert('Please select a Structure');
                return false;
            }
        else{
             var MaxCount="<%=Session("SBAMaxCaseCount")%>";       
            var TotalCount= document.getElementById("<%=hidTotalCaseCount.ClientID %>").value;
//             alert(MaxCount);
//             alert(TotalCount);
             if(eval(TotalCount)< eval(MaxCount))
             {
                  
                    if (Flag == 'NCC') 
                    {
                        //var Case1 = document.getElementById(Case)
                        msg = "You are going to create a copy of Structure " + Case + ". Do you want to proceed?"
                    }
                     if (confirm(msg)) {
                        return true;
                    }
                    else {
                        return false;
                    }
               }
               else
               {
                   var msg;
                    if (Flag == 'NCC') 
                    {
                      msg = "You cannot copy this Structure because you have reached your limit of "+MaxCount+" Structures.  Please contact SavvyPack Corporation (952-405-7500) to purchase more Structures.";
                    }
                  
                  alert(msg);
                  return false;
               }
            }
        }
		
		function CaseCopyCheck(fromCase, toCase) 
        {
            var Case = document.getElementById("<%=hidPropCase1.ClientID %>").value;
            //alert(Case);
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") 
            {
                alert('Please select a Structure');
                return false;
            }
            else
            {
            var Case1 = document.getElementById(fromCase)
            var Case2 = document.getElementById(toCase)
            var msg = "You are going to Transfer Variables of Structure " + Case1.value + " to Structure " + Case2.value + ". Do you want to proceed?"
                    if (confirm(msg)) 
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }

             }
             }


        function CaseCopyCheckB(fromCase, toCase) 
        {
               var Case = document.getElementById("<%=hidPropCase1.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") 
            {
                alert('Please select a Structure');
                return false;
            }
                else
            {
                var Case1 = document.getElementById(fromCase)
                var Case2 = document.getElementById(toCase)
                var msg = "You are going to Transfer Variables of Structure " + Case1.value + " to Structure " + Case2.value + ". Do you want to proceed?"
                if (confirm(msg)) 
                {
                    return true;
                }
                else 
                {
                    return false;
                }

        }
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

    <script type="text/javascript">


        function ShareMessage(Case, User, Flag) {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") {
                alert('Please select a Structure');
            }
            else {
                if (Flag == 'SC') {
                    var Case1 = document.getElementById(Case)
                    var Name = document.getElementById(User)
                    var msg = "You are going to Share Access to Structure " + Case1.value + " with User " + Name.value + ". Do you want to proceed?"
                }

                if (Flag == 'SCC') {
                    var Case1 = document.getElementById(Case)
                    var Name = document.getElementById(User)
                    var msg = "You are going to Share a Copy of Structure " + Case1.value + " with User " + Name.value + ". Do you want to proceed?"
                }

                if (Flag == 'SYN') {
                    var Case1 = document.getElementById(Case)
                    var msg = "You are going to synchronize Structure " + Case1.value + " from Econ1 to Sustain1. Do you want to proceed?"
                }

                if (confirm(msg)) {

                    return true;
                }
                else {

                    return false;
                }
            }

        }


        function Message(Case, Flag) {



            var Caseid = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(Caseid);
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Caseid == "0") {
                alert('Please select a Structure');
                return false;
            }
            else {

                if (Flag == 'NC') {
                    var msg = "You are going to Create a new Structure. Do you want to proceed?"
                }

                if (Flag == 'NCC') {
                    var Case1 = document.getElementById(Case)
                    msg = "You are going to create a Copy of Structure " + Caseid + ". Do you want to proceed?"
                }

                if (Flag == 'DC') {
                    var Case1 = document.getElementById(Case)
                    msg = "You are going to Delete Structure " + Caseid + ". Do you want to proceed?"
                }

                if (Flag == 'UC') {
                    var Case1 = document.getElementById(Case)
                    //msg = "You are going to update the case description for case " + Case1.value + ". Do you want to proceed?"
                    if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) {
                        alert("Structure Descriptor1 cannot be blank");
                        return false;
                    }
                    else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) {
                        alert("Structure Descriptor2 cannot be blank");
                        return false;
                    }
                    else {
                        msg = "You are going to update the Structure description for Structure " + Caseid + ". Do you want to proceed?"

                    }
                }


                if (confirm(msg)) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }



        function ShareCopyMessageNew(Case, User, Flag) {
            var Caseid = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            var UserN = ""
            UserN = document.getElementById("<%=hidUsernameD.ClientID %>").value;
            if (Caseid == "0") {
                alert('Please select a Structure');
            }
            else if (UserN == "") {
                alert('Please select a User');
                return false;
            }
            else {
                var TotalCount = document.getElementById("<%=hidTotalSCount.ClientID %>").value;
                var MaxCount = document.getElementById("<%=hidUserMaxCaseCount.ClientID %>").value;

                //          alert(TotalCount);
                //          alert(MaxCount);
                if (TotalCount != "" || MaxCount != "") {

                    if (eval(TotalCount) < eval(MaxCount)) {
                        if (Flag == 'SCC') {
                            //  var Case1 = document.getElementById(Case)
                            var Name = document.getElementById(User)
                            var msg = "You are going to Share a Copy of Structure " + Caseid + " with User " + Name.value + ". Do you want to proceed?"
                        }
                        if (confirm(msg)) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        var Name = document.getElementById(User)
                        var msg = "You are trying to Share a Copy with User " + Name.value + ".  " + Name.value + " has reached their limit of " + MaxCount + " Structures.  Please contact SavvyPack Corporation (952-405-7500) to purchase more Structures.";
                        alert(msg);
                        return false;
                    }
                }
                else {
                    var Name = document.getElementById(User)
                    var msg = "You are trying to share a copy with user " + Name.value + ".  " + Name.value + " does not have permissions to the appropriate module.  Please contact SavvyPack Corporation (952-405-7500) to purchase these permissions.";
                    alert(msg);
                    return false;
                }

            }
        }
                
		    
    </script>

    <script type="text/javascript">
        function ShareMessageNew(Case, User, Flag) {

            //alert(Flag);
            var Caseid = document.getElementById("<%=hidPropCase.ClientID %>").value;
            var UserN = ""
            UserN = document.getElementById("<%=hidUsernameD.ClientID %>").value;
            //alert(UserN);
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Caseid == "0") {
                alert('Please select a Structure');
                return false;
            }
            else if (UserN == "") {
                alert('Please select a User');
                return false;
            }
            else {
                var TotalCount = document.getElementById("<%=hidTotalShareCount.ClientID %>").value;
                var MaxCount = document.getElementById("<%=hidShareUserMaxCaseCount.ClientID %>").value;

                if (TotalCount != "" || MaxCount != "") {

                    if (eval(TotalCount) < eval(MaxCount)) {
                        if (Flag == 'SC') {
                            //var Case1 = document.getElementById(Case);
                            var Name = document.getElementById(User);

                            var msg = "You are going to Share Access to Structure " + Caseid + " with User " + Name.value + ". Do you want to proceed?"
                        }
                        if (confirm(msg)) {
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        var Name = document.getElementById(User)
                        var msg = "You are trying to Share a Access with User " + Name.value + ".  " + Name.value + " has reached their limit of " + MaxCount + " Structures.  Please contact SavvyPack Corporation (952-405-7500) to purchase more Structures.";
                        alert(msg);
                        return false;
                    }
                }
                else {
                    var Name = document.getElementById(User)
                    var msg = "You are trying to share a access with user " + Name.value + ".  " + Name.value + " does not have permissions to the appropriate module.  Please contact SavvyPack Corporation (952-405-7500) to purchase these permissions.";
                    alert(msg);
                    return false;
                }
            }
        }
    </script>

</head>
<body style="margin-top: 0px;">
    <%-- <center>--%>
    <form id="form1" runat="server">
    <center>
        <div id="MasterContent">
            <div>
                <table class="SBAModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse; margin-bottom: 12px">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                            runat="server" ToolTip="Update" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="btnGlobalManager" runat="server" ImageUrl="~/Images/Close.gif"
                                            OnClientClick="javascript:CloseWindow();" Text="Close Window" CssClass="ButtonWMarigin"
                                            onmouseover="Tip('Close this Window')" onmouseout="UnTip()" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                            runat="server" ToolTip="Instructions" Visible="false" />
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
                                        <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/Notes.gif"
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
        <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
            <tr style="height: 20px; text-align: left;">
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Tools')"
                        onmouseout="UnTip()" style="width: 840px;">
                        Tools
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left">
                            <br />
                            <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Source-Public Structure Library
                                    </td>
                                </tr>
                                <%--<tr class="AlterNateColor1">
                                <td>
                                    <asp:DropDownList ID="ddlBCases" CssClass="DropDown" Width="96%" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>--%>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <%--<asp:DropDownList ID="ddlPCase" Visible="false"  CssClass="DropDown" Width="98%" runat="server"></asp:DropDownList>--%>
                                        <asp:LinkButton ID="lnkApprovedCase" Style="font-size: 14px; padding-left: 10px"
                                            runat="server" CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/CaseDetails.aspx?Des=lnkApprovedCase&Id=hidApprovedCase&IdD=hidApprovedCaseD&SponsBy=hidSpons&Type=BASE&GrpId=0');">Select Structure</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnCopyBcase" runat="server" Text="Copy" CssClass="ButtonWMarigin" OnClientClick="return CreateMessageB('hidApprovedCase','NCC');" 
                                            onmouseover="Tip('Create a Copy of this Structure')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnBTransfer" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" 
                                            onmouseover="Tip('Transfer Variables from One Structure to Another Structure')"
                                            onmouseout="UnTip()" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                            <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Source-Proprietary Individual Library
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:LinkButton ID="lnkPropCase" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/CaseDetails.aspx?Des=lnkPropCase&Id=hidPropCase&IdD=hidPropCaseD&SponsBy=hidSpons&Type=PROP&GrpId=0');">Select Structure</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" style="height: 30px">
                                    <td>
                                        <asp:Button ID="btnCopyPcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                            OnClientClick="return CreateMessage('hidPropCase','NCC');" onmouseover="Tip('Create a Copy of this Structure')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnPTransfer" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                             onmouseover="Tip('Transfer Variables from One Structure to Another Structure')"
                                            onmouseout="UnTip()" Style="margin-left: 10px" />
                                        <asp:Button ID="btnPShareCase" runat="server" Text="Share Access" CssClass="ButtonWMarigin"
                                            onmouseover="Tip('Share Access of this structure with another user')"
                                            onmouseout="UnTip()" Style="margin-left: 10px" />
                                        <asp:Button ID="btnPShareCopy" runat="server" Text="Share a Copy" CssClass="ButtonWMarigin"
                                             onmouseover="Tip('Share a Copy of this Structure with Another User')"
                                            onmouseout="UnTip()" Style="margin-left: 10px" />
                                        <asp:Button ID="btnPRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                            onmouseover="Tip('Rename this Structure')" onmouseout="UnTip()" Style="margin-left: 10px" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                            OnClientClick="return Message('hidPropCase','DC');" onmouseover="Tip('Delete this Structure')"
                                            onmouseout="UnTip()" Style="margin-left: 10px" />
                                        <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                            OnClientClick="return CreateMessage('hidPropCase','NC');" onmouseover="Tip('Create A New Structure')"
                                            onmouseout="UnTip()" Style="margin-left: 10px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <table width="820px" id="tblG" runat="server" visible="false">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Group Structure
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:LinkButton ID="lnkPropGrps" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="300px" OnClientClick="return ShowPopup('../PopUp/GetAllGroups.aspx?Des=lnkPropGrps&Id=hidGrpId&IdD=hidGroupReportD&Type=PROP');">Select Groups</asp:LinkButton>
                                        <asp:Label ID="lblGroup" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                            Text="You currently have no Public Group to display. You can create a Group with the Tools below."></asp:Label>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" style="height: 30px">
                                    <td>
                                        <asp:Button ID="btnRenameGroup" runat="server" onmouseover="Tip('Rename this Group')"
                                            onmouseout="UnTip()" Text="Rename" CssClass="ButtonWMarigin" Style="margin-left: 10px" />
                                        <asp:Button ID="btnCreateGrp" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                            onmouseover="Tip('Create A New Public Group')" onmouseout="UnTip()" Style="margin-left: 10px" />
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <div style="display: none; margin-top: 40px;" id="divBCopy" runat="server">
                                <table width="820px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Target Structure
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <%--<asp:DropDownList ID="ddlTarget" CssClass="DropDown" Width="96%" runat="server">
                                        </asp:DropDownList>--%>
                                            <asp:LinkButton ID="lnkTargetPropCase" Style="font-size: 14px; padding-left: 10px"
                                                runat="server" CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/CaseDetails.aspx?Des=lnkTargetPropCase&Id=hidPropCase1&IdD=hidPropCaseD1&SponsBy=hidSpons&Type=PROP&GrpId=0');">Select Structure</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnBS2T" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                                OnClientClick="return CaseCopyCheckB('hidApprovedCase','hidPropCase1');" onmouseover="Tip('Transfer Variables to this Structure')"
                                                onmouseout="UnTip()" />
                                            <asp:Button ID="btnBS2TCancle" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                Style="margin-left: 10px" OnClientClick="return MakeInVisible('divBCopy');" onmouseover="Tip('Cancel')"
                                                onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="display: none; margin-top: 40px;" id="divCopy" runat="Server">
                                <table width="820px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Target Structure
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <%--<asp:DropDownList ID="ddlTarget1" CssClass="DropDown" Width="96%" runat="server">
                                        </asp:DropDownList>--%>
                                            <asp:LinkButton ID="lnkTarget1PropCase" Style="font-size: 14px; padding-left: 10px"
                                                runat="server" CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/CaseDetails.aspx?Des=lnkTarget1PropCase&Id=hidPropCase1&IdD=hidPropCaseD1&SponsBy=hidSpons&Type=PROP&GrpId=0');">Select Structure</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnPS2T" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                                OnClientClick="return CaseCopyCheck('hidPropCase','hidPropCase1');" onmouseover="Tip('Transfer Variables to this Structure')"
                                                onmouseout="UnTip()" />
                                            <asp:Button ID="btnTransferC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="display: none; margin-top: 40px;" id="divshareAccess" runat="server">
                                <table width="700px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Share with this User
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <%-- <td>
                                        <asp:DropDownList ID="ddlUsers" CssClass="DropDown" Width="30%" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>--%>
                                        <td>
                                            <asp:DropDownList ID="ddlUsers" CssClass="DropDown" Width="30%" runat="server" AutoPostBack="true"
                                                Visible="false">
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lnkUser1" Style="font-size: 14px; padding-left: 10px" runat="server"
                                                CssClass="Link" Width="500px" OnClientClick="return ShowPopupUSR('../PopUp/UserDetails.aspx?Des=lnkUser1&Id=hidUserId&IdD=hidUsernameD');">Select User</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnShareAccess" runat="server" Text="Share Access" CssClass="ButtonWMarigin"
                                                OnClientClick="return ShareMessageNew('hidPropCase', 'hidUsernameD', 'SC');"
                                                onmouseover="Tip('Share a Access with this User')" onmouseout="UnTip()" />
                                            <asp:Button ID="btnShareAccessC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="display: none; margin-top: 40px;" id="divsharecopy" runat="Server">
                                <table width="700px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Share with this User
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <%--  <td>
                                        <asp:DropDownList ID="ddlUser2" CssClass="DropDown" Width="30%" runat="server" AutoPostBack="true">
                                        </asp:DropDownList>

                                    </td>--%>
                                        <td>
                                            <asp:LinkButton ID="lnkUser2" Style="font-size: 14px; padding-left: 10px" runat="server"
                                                CssClass="Link" Width="500px" OnClientClick="return ShowPopupUSR('../PopUp/UserDetails.aspx?Des=lnkUser2&Id=hidUserIdShare&IdD=hidUsernameD');">Select User</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnShareCopy" runat="server" Text="Share a Copy" CssClass="ButtonWMarigin"
                                                OnClientClick="return ShareCopyMessageNew('hidPropCase', 'hidUsernameD', 'SCC');"
                                                onmouseover="Tip('Share a Copy with this User')" onmouseout="UnTip()" />
                                            <asp:Button ID="btnShareCopyC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="margin-top: 40px;" id="divModify" runat="server" visible="false">
                                <table width="700px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            Rename Structure Details
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Structure Descriptor1:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                width: 230px;" MaxLength="25" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Structure Descriptor2:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                width: 230px;" MaxLength="25" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Structure Notes:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaseDe3" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                                onchange="javascript:Count(this);" TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Structure Application:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtApp" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                                MaxLength="25" Width="230px" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                                OnClientClick="return Message('hidPropCase','UC');" onmouseover="Tip('Rename this Structure')"
                                                onmouseout="UnTip()" />
                                            <asp:Button ID="btnRenameC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="margin-top: 40px;" id="divGModify" runat="server" visible="false">
                                <table width="700px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            <asp:Label ID="lblGHeader" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Descriptor1:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGDES1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                width: 230px" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Descriptor2:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGDES2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                width: 230px" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Technical Description:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGDES3" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                                TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Application:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtGAPP" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                                TextMode="MultiLine" Height="100px" Width="230px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                                OnClientClick="return MessageG('hidGrpId','UC');" onmouseover="Tip('Rename this Group')"
                                                onmouseout="UnTip()" />
                                            <asp:Button ID="btnGCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
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
      <%--  <div id="AlliedLogo">
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
    <asp:HiddenField ID="hidBackcheck" Value="0" runat="server" />
    <asp:HiddenField ID="hidTotalCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalSCount" runat="server" />
    <asp:HiddenField ID="hidUserMaxCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalShareCount" runat="server" />
    <asp:HiddenField ID="hidShareUserMaxCaseCount" runat="server" />
    <br />
    <br />
    <asp:HiddenField ID="hidApprovedCase" runat="server" />
    <asp:HiddenField ID="hidPropCase" runat="server" />
    <asp:HiddenField ID="hidApprovedCaseD" runat="server" />
    <asp:HiddenField ID="hidPropCaseD" runat="server" />
    <asp:HiddenField ID="hidPropCaseSt" runat="server" />
    <asp:HiddenField ID="hidCaseID" runat="server" />
    <asp:HiddenField ID="hidSpons" runat="server" />
    <asp:HiddenField ID="hidGrpId" runat="server" />
    <asp:HiddenField ID="hidGrpDes" runat="server" />
    <asp:HiddenField ID="hidGroupReportD" runat="server" />
    <asp:HiddenField ID="hidPropCase1" runat="server" />
    <asp:HiddenField ID="hidPropCaseD1" runat="server" />
    <asp:HiddenField ID="hidUser" runat="server" />
    <asp:HiddenField ID="hidUserId" runat="server" />
     <asp:HiddenField ID="hidUserIdShare" runat="server" />
    <asp:HiddenField ID="hidUsername" runat="server" />
    <asp:HiddenField ID="hidUsernameD" runat="server" />
    <asp:HiddenField ID="hidCreate" runat="server" />
    
      <asp:HiddenField ID="hidCaseId1" runat="server" />
    <asp:HiddenField ID="hidCaseId2" runat="server" />
        <asp:HiddenField ID="hidCaseId3" runat="server" />
            <asp:HiddenField ID="hidCaseId4" runat="server" />
            
               <asp:HiddenField ID="hidUserId1" runat="server" />
               <asp:HiddenField ID="hidUserId2" runat="server" />
    </form>

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

</body>
</html>
