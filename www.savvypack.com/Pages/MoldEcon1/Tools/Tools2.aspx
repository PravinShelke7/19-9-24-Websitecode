<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tools2.aspx.vb" Inherits="Pages_MoldEcon1_Tools_Tools2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1 Mold-Tools</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        javascript: window.history.forward(1);
        function ShowPopupWindow(page) {
            newwin = window.open(page);
            return false;
        }
        function StatusMessage() {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
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
        function ShowPopWindow(PageN, Type) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 770;
            var height = 450;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var pn = "";
            //alert(pn);
            if (Type == "AP") {
                pn = PageN;
            }
            else {
                var grpId = document.getElementById("<%=hidGroupId.ClientID %>").value;
                //  alert(grpId);
                pn = PageN + "&GrpId=" + grpId + " ";
            }
            //alert(pn);
            newwin = window.open(pn, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
        function ShowUserWindow(PageN, Type) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 400;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var pn = "";

            newwin = window.open(PageN, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function MakeVisible(Type, id) {

            var CaseId = "";
            if (Type == "APP") {
                CaseId = document.getElementById("<%=hidApprovedCase.ClientID %>").value;
            }
            else {
                CaseId = document.getElementById("<%=hidPropCase.ClientID %>").value;
            }
            if (CaseId == "0") {
                if (Type == "APP") {
                    alert('Please select Source Approved Case');
                }
                else {
                    alert('Please select Source Proprietary Case');
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
        function test111() {

            myString = new String("*&^%$#@") //list of special char.
            if (myString.indexOf(document.getElementById("txtCaseDe2").value) != -1)
                alert("Special char not allowed");
            return false;


        }
        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 790; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
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

        function CaseCopyCheck(fromCase, toCase) 
        {
         var Case1="";
         var Case1Val="";
                                       
        if(fromCase=="APP")
        {
         // alert(document.getElementById("<%=hidApprovedCase.ClientID %>").value);
          Case1 = document.getElementById("<%=hidApprovedCase.ClientID %>").value;
          Case1Val=Case1;
        }
        else
        {
      Case1 = document.getElementById("<%=hidPropCase.ClientID %>").value;
          Case1Val=Case1;
        }
          
            var Case2 ="0";
            
            if(toCase=="hidTargetApp")
            {
            Case2=document.getElementById("<%=hidTargetApp.ClientID %>").value; 
            }
            else
            {
               Case2=document.getElementById("<%=hidTargetProp.ClientID %>").value; 
            }
            //alert(Case2);
            if(Case2=="0")
            {
            alert("Please select Source Proprietary Case");
            return false;
            }
            else
            {
            var msg = "You are going to transfer variables of case " + Case1Val + " to case " + Case2 + ". Do you want to proceed?"
            if (confirm(msg)) {
           // alert('Sud');
                return true;
            }
            else {
                return false;
            }
            }

        }
         function SyncMessage(Type,Case,Flag) 
        {
        var CaseId="";
         if(Type=="APP")
         {
           CaseId= document.getElementById("<%=hidApprovedCase.ClientID %>").value;
         }
         else
         {
           CaseId= document.getElementById("<%=hidPropCase.ClientID %>").value;
         }
         if(CaseId=="0")
         {
           if(Type=="APP")
         {
         alert('Please select Approved Case');
         }
         else
         {
           alert('Please select Proprietary Case');
         }
         return false;
         }
         else
         {
           

            if (Flag == 'SYN') {
                var Case1 = document.getElementById(Case)
                var msg = "You are going to synchronize case " + CaseId + " from Sustain1 Mold to Econ1 Mold. Do you want to proceed?"
            }
             if (Flag == 'RENAME') 
             {
              return true;
            }
            if (confirm(msg)) {

                return true;
            }
            else {

                return false;
            }
            }

        }
        function ShareMessage(Case, User, Flag) 
        {

            if (Flag == 'SC') {
                var Case1 = document.getElementById(Case)
                var Name = document.getElementById(User)
                var msg = "You are going to share access to case " + Case1.value + " with user " + Name.value + ". Do you want to proceed?"
            }

            if (Flag == 'SCC') {
                var Case1 = document.getElementById(Case)
                var Name = document.getElementById(User)
                var msg = "You are going to share a copy of case " + Case1.value + " with user " + Name.value + ". Do you want to proceed?"
            }

            if (Flag == 'SYN') {
                var Case1 = document.getElementById(Case)
                var msg = "You are going to synchronize case " + Case1.value + " from Sustain1 Mold to Econ1 Mold. Do you want to proceed?"
            }

            if (confirm(msg)) {

                return true;
            }
            else {

                return false;
            }

        }
        
        function ShareMessageNew(Case, User, Flag) 
        {

        var CaseId="";     
        var UserN=""   
           CaseId= document.getElementById("<%=hidPropCase.ClientID %>").value;       
           UserN= document.getElementById("<%=hidUserSA.ClientID %>").value;
           if(UserN=="")
           {
           alert("Please select User");
           return false;
           }
           else
           {

           var TotalCount=1;//document.getElementById("<%=hidTotalShareCount.ClientID %>").value;       
           var MaxCount=2;// document.getElementById("<%=hidShareUserMaxCaseCount.ClientID %>").value;
           
           if(TotalCount != "" || MaxCount != "")
          {
                         
                  if(eval(TotalCount) < eval(MaxCount))
                  {                 
                           if (Flag == 'SC')
                            {
                                    var Case1 = document.getElementById(Case)
                                    var Name = document.getElementById(User)
                                   var msg = "You are going to share access to case " + CaseId + " with user " + UserN + ". Do you want to proceed?"
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
                          var Name = document.getElementById(User)
                          var msg = "You are trying to share a access with user " + UserN + ".  " + UserN + " has reached their limit of "+MaxCount+" cases.  Please contact SavvyPack Corporation (952-405-7500) to purchase more cases.";
                          alert(msg);
                          return false;
                  }
            }
            else
            {
                         var Name = document.getElementById(User)
                          var msg = "You are trying to share a access with user " + UserN + ".  " + UserN + " does not have permissions to the appropriate module.  Please contact SavvyPack Corporation (952-405-7500) to purchase these permissions.";
                          alert(msg);
                          return false;
            }
            }

        }
        
        function ShareCopyMessageNew(Case, User, Flag) 
        {
          var TotalCount=1;//document.getElementById("<%=hidTotalSCount.ClientID %>").value;       
          var MaxCount=2;// document.getElementById("<%=hidUserMaxCaseCount.ClientID %>").value;
      var CaseId= document.getElementById("<%=hidPropCase.ClientID %>").value;
       var UserN= document.getElementById("<%=hidUserSC.ClientID %>").value;
           if(UserN=="")
           {
           alert("Please select User");
           return false;
           }
           else
           {


      
//          alert(TotalCount);
//          alert(MaxCount);
          if(TotalCount != "" || MaxCount != "")
          {
                         
                  if(eval(TotalCount) < eval(MaxCount))
                  {                 
                           if (Flag == 'SCC')
                            {
                                    var Case1 = document.getElementById(Case)
                                    var Name = document.getElementById(User)
                                    var msg = "You are going to share a copy of case " + CaseId + " with user " + UserN + ". Do you want to proceed?"
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
                          var Name = document.getElementById(User)
                          var msg = "You are trying to share a copy with user " + Name.value + ".  " + UserN + " has reached their limit of "+MaxCount+" cases.  Please contact SavvyPack Corporation (952-405-7500) to purchase more cases.";
                          alert(msg);
                          return false;
                  }
            }
            else
            {
                         var Name = document.getElementById(User)
                          var msg = "You are trying to share a copy with user " + Name.value + ".  " + UserN + " does not have permissions to the appropriate module.  Please contact SavvyPack Corporation (952-405-7500) to purchase these permissions.";
                          alert(msg);
                          return false;
            }
            }
             
        
        }


        function Message(Type,Flag) {


        var CaseId="";
         if(Type=="APP")
         {
           CaseId= document.getElementById("<%=hidApprovedCase.ClientID %>").value;
         }
         else
         {
           CaseId= document.getElementById("<%=hidPropCase.ClientID %>").value;
         }
if(CaseId=="0")
         {
           if(Type=="APP")
         {
         alert('Please select Approved Case');
         }
         else
         {
           alert('Please select Proprietary Case');
         }
         return false;
         }
         else
         {
           

            if (Flag == 'NC') {
                var msg = "You are going to create a new case. Do you want to proceed?"
            }

            if (Flag == 'NCC') {
                
                msg = "You are going to create a copy of case " + CaseId + ". Do you want to proceed?"
            }

            if (Flag == 'DC') {
             
                msg = "You are going to delete case " + CaseId + ". Do you want to proceed?"
            }

            if (Flag == 'UC') {
            
                                  if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) 
                                  {
                                        alert("PACKAGING FORMAT cannot be blank");
                                        return false;
                                  }
                                  else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) 
                                  {
                                        alert("UNIQUE FEATURES cannot be blank");
                                        return false;
                                  }
                                  else 
                                  {
                                       msg = "You are going to update the case description for case " + CaseId + ". Do you want to proceed?"

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
        
        function CreateMessage(Type, Flag) 
        {
        //alert(Type);
       
         var CaseId="";
         if(Type=="APP")
         {
           CaseId= document.getElementById("<%=hidApprovedCase.ClientID %>").value;
         }
         else
         {
           CaseId= document.getElementById("<%=hidPropCase.ClientID %>").value;
         }
         //alert(CaseId);
         if(CaseId=="0")
         {
              
                if (Flag == 'NC') 
                    {
                       var msg = "You are going to create a new case. Do you want to proceed?"
                         if (confirm(msg)) {
                        return true;
                    }
                    else {
                        return false;
                    }
                    }
                    else{
                 if(Type=="APP")
                 {
                 alert('Please select Approved Case');
                 }
                 else
                 {
                   alert('Please select Proprietary Case');
                 }
                 }
                 return false;
         }
         else
         {
        //  alert('Hi1');
          var MaxCount="<%=Session("E1MaxCaseCount")%>";       
          var TotalCount= document.getElementById("<%=hidTotalCaseCount.ClientID %>").value;
//             alert(MaxCount);
//             alert(TotalCount);
             if(eval(TotalCount)< eval(MaxCount))
             {
                  if (Flag == 'NC') 
                    {
                        var msg = "You are going to create a new case. Do you want to proceed?"
                    }
                    if (Flag == 'NCC') 
                    {
                        //var Case1 = document.getElementById(Case)
                        msg = "You are going to create a copy of case " + CaseId + ". Do you want to proceed?"
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
                      msg = "You cannot create a new case because you have reached your limit of "+MaxCount+" cases.  Please contact SavvyPack Corporation (952-405-7500) to purchase more cases.";
                    }
                    else if (Flag == 'NCC') 
                    {
                      msg = "You cannot copy this case because you have reached your limit of "+MaxCount+" cases.  Please contact SavvyPack Corporation (952-405-7500) to purchase more cases.";
                    }
                  
                  alert(msg);
                  return false;
               }
               }
        
        }

     
                		       
		        
		       

		       
		    
    </script>
    <style type="text/css">
        .E1MoldModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../../Images/E1MoldHeader.gif');
            height: 44px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
</head>
<body style="margin-top: 0px;">
    <%-- <center>--%>
    <form id="form1" runat="server">
    <div id="MasterContent">
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
        <div>
            <table class="E1MoldModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                        runat="server" ToolTip="Update" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/GlobalManager.gif"
                                        Text="Global Manager" CssClass="ButtonWMarigin" PostBackUrl="~/Pages/MoldEcon1/Default2.aspx"
                                        onmouseover="Tip('Return to Global Manager')" onmouseout="UnTip()" />
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
    <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
        <tr style="height: 20px">
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Econ1 Mold-Tools')"
                    onmouseout="UnTip()" style="width: 840px;">
                    Econ1 Mold - Tools
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left">
                        <br />
                        <div>
                            <table>
                                <tr>
                                    <td>
                                        <div id="IsAdmin" runat="server" visible="false" style="margin: 0px 5px 5px 5px;">
                                            <%--<asp:HyperLink ID="lnkAdminTool" runat="server" Text="Admin Tool" CssClass="Link"
                                    Font-Bold="true" NavigateUrl="~/Pages/Econ1/Tools/AdminTool.aspx"></asp:HyperLink>--%>
                                            <asp:LinkButton ID="lnkAdminmTool" runat="Server" CssClass="Link" Text="Admin Tool"
                                                Style="font-size: 13px; font-weight: bold;" OnClientClick="return ShowPopupWindow('../Tools/AdminTool.aspx');"></asp:LinkButton>
                                        </div>
                                    </td>
                                    <td>
                                        <div id="Div1" runat="server" style="margin: 0px 5px 5px 5px;">
                                            <%--  <asp:HyperLink ID="lnkGroupTool" runat="server" Text="Manage Groups" CssClass="Link"
                                    Font-Bold="true" NavigateUrl="~/Pages/Econ1/Tools/ManageGroups.aspx" ></asp:HyperLink>--%>
                                            <asp:LinkButton ID="lnkGroupmTool" runat="Server" CssClass="Link" Text="Manage Groups"
                                                Style="font-size: 13px; font-weight: bold;" OnClientClick="return ShowPopupWindow('../Tools/ManageGroups.aspx');"></asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <table width="820px">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Source Approved Cases
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:DropDownList ID="ddlBCases" CssClass="DropDown" Width="96%" Visible="false"
                                        runat="server">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lnkApprovedCase" Style="font-size: 14px; padding-left: 10px"
                                        runat="server" CssClass="Link" Width="500px" OnClientClick="return ShowPopWindow('../PopUp/CasesSearch.aspx?Des=lnkApprovedCase&Id=hidApprovedCase&IdD=hidApprovedCaseD','AP');">Select Case</asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnCopyBcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('APP','NCC');" onmouseover="Tip('Create a Copy of this Case')"
                                        onmouseout="UnTip()" />
                                    <asp:Button ID="btnBTransfer" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                        Style="margin-left: 10px" OnClientClick="return MakeVisible('APP','divBCopy');"
                                        onmouseover="Tip('Transfer Variables from One Case to Another Case')" onmouseout="UnTip()" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <table width="820px">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Source Proprietary Case
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="96%" Visible="false"
                                        runat="server">
                                    </asp:DropDownList>
                                    <asp:LinkButton ID="lnkPropCase" Style="font-size: 14px; padding-left: 10px" runat="server"
                                        CssClass="Link" Width="500px" OnClientClick="return ShowPopWindow('../PopUp/CasesSearch.aspx?Des=lnkPropCase&Id=hidPropCase&IdD=hidPropCaseD','PRP');">Select Case</asp:LinkButton>
                                    <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                        Text="You currently have no Proprietary Cases to display. You can create a Case with the Tools below."></asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="height: 30px">
                                <td>
                                    <asp:Button ID="btnCopyPcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('ddlPCases','NCC');" onmouseover="Tip('Create a Copy of this Case')"
                                        onmouseout="UnTip()" />
                                    <asp:Button ID="btnPTransfer" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                        OnClientClick="return MakeVisible('Prop','divCopy');" onmouseover="Tip('Transfer Variables from One Case to Another Case')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnPShareCase" runat="server" Text="Share Access" CssClass="ButtonWMarigin"
                                        OnClientClick="return MakeVisible('Prop','divshareAccess');" onmouseover="Tip('Share Access to this Case with Another User')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnPShareCopy" runat="server" Text="Share a Copy" CssClass="ButtonWMarigin"
                                        OnClientClick="return MakeVisible('Prop','divsharecopy');" onmouseover="Tip('Share a Copy of this Case with Another User')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnSynchronize" runat="server" Text="Synchronize" CssClass="ButtonWMarigin"
                                        OnClientClick="return SyncMessage('Prop', '', 'SYN');" onmouseover="Tip('Synchronize this Case from Sustain1 Mold to Econ1 Mold')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnPRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                        onmouseover="Tip('Rename this Case')" OnClientClick="return SyncMessage('Prop', '', 'RENAME');"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                        OnClientClick="return Message('Prop','DC');" onmouseover="Tip('Delete this Case')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('Prop','NC');" onmouseover="Tip('Create A New Case')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnSubmit" runat="server" onmouseout="UnTip()" onmouseover="Tip('Submitted for approval for addition to Approved cases')"
                                        Text="Submit" OnClientClick="return StatusMessage();" CssClass="Button"></asp:Button>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div style="display: none; margin-top: 40px;" id="divBCopy" runat="server">
                            <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Target Proprietary Case
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlTarget" CssClass="DropDown" Width="96%" runat="server" Visible="false">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkTargetApp" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowPopWindow('../PopUp/CasesSearch.aspx?Des=lnkTargetApp&Id=hidTargetApp&IdD=hidTargetAppD','PRP');">Select case</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnBS2T" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                            OnClientClick="return CaseCopyCheck('APP','hidTargetApp');" onmouseover="Tip('Transfer Variables from One Case to Another Case')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnBS2TCancle" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="display: none; margin-top: 40px;" id="divCopy" runat="server">
                            <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Target Proprietary Case
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlTarget1" CssClass="DropDown" Width="96%" runat="server"
                                            Visible="false">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkTargetProp" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowPopWindow('../PopUp/CasesSearch.aspx?Des=lnkTargetProp&Id=hidTargetProp&IdD=hidTargetPropD','PRP');">Select case</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnPS2T" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                            OnClientClick="return CaseCopyCheck('ddlPCases','hidTargetProp');" onmouseover="Tip('Transfer On Target')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnPS2TCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="display: none; margin-top: 40px;" id="divshareAccess" runat="Server">
                            <table width="700px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Share with this User
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlUsers" CssClass="DropDown" Width="30%" Visible="false" runat="server"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkUsers1" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowUserWindow('../PopUp/LicenseUser.aspx?Des=lnkUsers1&Id=hidUserSA','PRP');">Select User</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnShareAccess" runat="server" Text="Share Access" CssClass="ButtonWMarigin"
                                            OnClientClick="return ShareMessageNew('ddlPCases', 'ddlUsers', 'SC');" onmouseover="Tip('Share Access to this Case with Another User')"
                                            onmouseout="UnTip()" />
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
                                    <td>
                                        <asp:DropDownList ID="ddlUser2" CssClass="DropDown" Visible="false" Width="30%" runat="server"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lnkUser2" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowUserWindow('../PopUp/LicenseUser.aspx?Des=lnkUser2&Id=hidUserSC','PRP');">Select User</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnShareCopy" runat="server" Text="Share a Copy" CssClass="ButtonWMarigin"
                                            OnClientClick="return ShareCopyMessageNew('ddlPCases', 'ddlUser2', 'SCC');" onmouseover="Tip('Share a Copy of this Case with Another User')"
                                            onmouseout="UnTip()" />
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
                                        Rename Case Details
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Packaging Format Definition:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Unique Features Definition:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" onblur="return test111();" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 3:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe3" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                            TextMode="MultiLine" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);"
                                            Height="100px" Width="489px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                            OnClientClick="return Message('ddlPCases','UC');" onmouseover="Tip('Rename this Case')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnRenameC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
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
    </table>
    <asp:HiddenField ID="hidBackcheck" Value="0" runat="server" />
    <asp:HiddenField ID="hidTotalCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalSCount" runat="server" />
    <asp:HiddenField ID="hidUserMaxCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalShareCount" runat="server" />
    <asp:HiddenField ID="hidShareUserMaxCaseCount" runat="server" />
    <asp:HiddenField ID="hidApprovedCase" runat="server" />
    <asp:HiddenField ID="hidPropCase" runat="server" />
    <asp:HiddenField ID="hidApprovedCaseD" runat="server" />
    <asp:HiddenField ID="hidPropCaseD" runat="server" />
    <asp:HiddenField ID="hidPropCaseSt" runat="server" />
    <asp:HiddenField ID="hidGroupId" runat="server" />
    <asp:HiddenField ID="hidTargetApp" runat="server" />
    <asp:HiddenField ID="hidTargetAppD" runat="server" />
    <asp:HiddenField ID="hidTargetProp" runat="server" />
    <asp:HiddenField ID="hidTargetPropD" runat="server" />
    <asp:HiddenField ID="hidUserSA" runat="server" />
    <asp:HiddenField ID="hidUserSC" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
