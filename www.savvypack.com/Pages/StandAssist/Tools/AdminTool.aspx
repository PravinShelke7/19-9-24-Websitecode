<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminTool.aspx.vb" Inherits="Pages_StandAssist_Tools_AdminTool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Admin Tools</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .SBAModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../../Images/SavvyPackStructureAssistantR01.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        javascript: window.history.forward(1);
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
          if ((document.getElementById("txtCaseDe1").value.match(a) != null) || (document.getElementById("txtCaseDe2").value.match(a) != null) || (document.getElementById("txtApp").value.match(a) != null) || (document.getElementById("txtStructSM").value.match(a) != null) )  
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
	        else if (text.id=="txtStructSM")
	        {  
	     	 alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Structure Sponsor Message. Please choose alternative characters.");
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
        
        function CloseWindow() {
            window.open('', '_self', '');
            window.close();
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

        function MakeVisible(Type, id) {
            // alert(document.getElementById(id));

            CaseId = document.getElementById(Type).value;

            if (CaseId == "0" || CaseId == "") {

                alert('Please select Source Structure');

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

        function CaseCopyCheck(fromCase, toCase) {
            var Case1 = document.getElementById(fromCase).value;
            var Case2 = document.getElementById(toCase).value;
            if (Case2 == "") {
                alert('Please select Destination Structure');
                return false;
            }
            else {
                var msg = "You are going to transfer variables of Structure " + Case1 + " to Structure " + Case2 + ". Do you want to proceed?"
                if (confirm(msg)) {
                    return true;
                }
                else {
                    return false;
                }
            }

        }

        function ShareMessage(Case, User, Flag) {


            if (Flag == 'SYN') {
                var Case1 = document.getElementById(Case)
                var msg = "You are going to synchronize case " + Case1.value + " from Econ1 to Sustain1. Do you want to proceed?"
            }

            if (confirm(msg)) {

                return true;
            }
            else {

                return false;
            }

        }


        function Message(Case, Flag) {





            if (Flag == 'NC') {
                var msg = "You are going to create a new Structure. Do you want to proceed?"
            }

            if (Flag == 'NCC') {
                var Case1 = document.getElementById(Case)
                msg = "You are going to create a copy of Structure " + Case1.value + ". Do you want to proceed?"
            }

            if (Flag == 'DC') {
                CaseId = document.getElementById(Case).value;

                if (CaseId == "0" || CaseId == "") {

                    alert('Please select Source Structure');

                    return false;
                }
                else {

                    msg = "You are going to delete Structure " + CaseId + ". Do you want to proceed?"
                }

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
                    msg = "You are going to update the description for Structure " + Case1.value + ". Do you want to proceed?"

                }
            }


            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

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

        function CreateMessage(Case, Flag) {

            if (Flag == 'NC') {
                var msg = "You are going to create a new Structure. Do you want to proceed?"
            }
            if (Flag == 'NCC') {
                var Case1 = document.getElementById(Case).value;

                if (Case1 == "" || Case1 == "0") {
                    alert('Please select Source Structure');

                    return false;
                }
                else {
                    // alert(Case1);
                    msg = "You are going to create a copy of Structure " + Case1 + ". Do you want to proceed?"
                }

            }
            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }


            alert(msg);
            return false;

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
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
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
                <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Admin Tools')"
                    onmouseout="UnTip()" style="width: 840px;">
                    Admin Tools
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left">
 <table width="820px" style="display:none;">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Public Groups of Structures
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:LinkButton ID="lnkBaseGrps" Style="font-size: 14px; padding-left: 10px" runat="server"
                                        CssClass="Link" Width="300px" OnClientClick="return ShowPopup('../PopUp/GetAllGroups.aspx?Des=lnkBaseGrps&Id=hidGrpId&IdD=hidGroupReportD&Type=BASE');">Select Group</asp:LinkButton>
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
                        <table width="820px">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Public Structures
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:LinkButton ID="lnkBaseCase" Style="font-size: 14px; padding-left: 10px" runat="server"
                                        CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/CaseDetails.aspx?Des=lnkBaseCase&Id=hidBaseCase&IdD=hidBaseCaseD&SponsBy=hidSpons&Type=BASE&GrpId=0');">Select Structure</asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="height: 30px">
                                <td>
                                    <asp:Button ID="btnCopyPcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('hidBaseCase','NCC');" onmouseover="Tip('Create a Copy of this Structure')"
                                        onmouseout="UnTip()" />
                                    <asp:Button ID="btnPTransfer" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                         onmouseover="Tip('Transfer Variables from One Structure to Another Structure')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnPRename" runat="server" onmouseover="Tip('Rename this Structure')"
                                        onmouseout="UnTip()" Text="Rename" CssClass="ButtonWMarigin" Style="margin-left: 10px" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                        OnClientClick="return Message('hidBaseCase','DC');" onmouseover="Tip('Delete this Structure')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('hidBaseCase','NC');" onmouseover="Tip('Create A New Structure')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div style="display: none; margin-top: 40px;" runat="server"  id="divCopy">
                            <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Target Public Structure
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:LinkButton ID="lnkTransferCase" Style="font-size: 14px; padding-left: 10px"
                                            runat="server" CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/CaseDetails.aspx?Des=lnkTransferCase&Id=hidTransferCase&IdD=hidTransferCaseD&SponsBy=hidSpons&Type=BASE&GrpId=0');">Select Structure</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnPS2T" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                            OnClientClick="return CaseCopyCheck('hidBaseCase','hidTransferCase');" onmouseover="Tip('Transfer Variables to this Structure')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px"  onmouseover="Tip('Cancel')"
                                            onmouseout="UnTip()" />
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
                                        Structure Descriptor1:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25" onchange="javascript:CheckSP(this);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Structure Descriptor2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25" onchange="javascript:CheckSP(this);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Structure Notes:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe3" runat="server" CssClass="MediumTextBox" Style="text-align: left;" onchange="javascript:Count(this);" 
                                            TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Structure Application:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtApp" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                            MaxLength="25" Width="230px" onchange="javascript:CheckSP(this);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Structure SponsorBy:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkSponsored" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/SponsorDetails.aspx?Des=lnkSponsored&Id=hidSponsorId');">Select Sponsor</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Structure Sponsor Message:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtStructSM" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                            Width="230px" onchange="javascript:CheckSP(this);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                            OnClientClick="return Message('hidBaseCase','UC');" onmouseover="Tip('Rename this Structure')"
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
                                    <td align="right">
                                        SponsorBy:
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lnkGSP" Style="font-size: 14px; padding-left: 10px" runat="server"
                                            CssClass="Link" Width="500px" OnClientClick="return ShowPopup('../PopUp/SponsorDetails.aspx?Des=lnkGSP&Id=hidGSponsorId');">Select Sponsor</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Sponsor Message:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSPMessage" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                            Width="230px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGRename" runat="server" Text="Update" CssClass="ButtonWMarigin"
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
    </table>
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
    </center>
    <asp:HiddenField ID="hidBackcheck" Value="0" runat="server" />
    <asp:HiddenField ID="hidTotalCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalSCount" runat="server" />
    <asp:HiddenField ID="hidUserMaxCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalShareCount" runat="server" />
    <asp:HiddenField ID="hidShareUserMaxCaseCount" runat="server" />
    <asp:HiddenField ID="hidSponsorId" Value="0" runat="server" />
    <asp:HiddenField ID="hidGSponsorId" Value="0" runat="server" />
    <asp:HiddenField ID="hidCreate" Value="0" runat="server" />
    <asp:HiddenField ID="hidGrpId" runat="server" />
    <asp:HiddenField ID="hidGrpDes" runat="server" />
    <asp:HiddenField ID="hidGroupReportD" runat="server" />
    <asp:HiddenField ID="hidBaseCase" runat="server" />
    <asp:HiddenField ID="hidBaseCaseD" runat="server" />
    <asp:HiddenField ID="hidTransferCase" runat="server" />
    <asp:HiddenField ID="hidTransferCaseD" runat="server" />
    <asp:HiddenField ID="hidSpons" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
