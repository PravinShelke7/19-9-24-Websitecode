<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ComparisonTool.aspx.vb"
    Inherits="Pages_StandAssist_Tools_ComparisonTool" %>

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
            background-image: url( '../../../Images/SavvyPackStructureAssistantR01.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/JavaScript">
    function CheckSP(text) 
        {
            var a = /\<|\>|\&#|\\/;
            if ((document.getElementById("ComparisonName").value.match(a) != null)) 
            {
                alert("You cannot use the following COMBINATIONS of characters: < > \\  &# in Comparison Name . Please choose alternative characters.");
                var object = document.getElementById(text.id)  //get your object
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }
     function openWindowU(page) 
            {
                window.open(page);
                return false;
            }
    function CloseWindow() {
            window.open('', '_self', '');
            window.close();
        }
 function ShowPopup(Page) {            
            newwin = window.open(Page, 'Chat', "");
            return false;

        }

        function ShareMessageNew() {
                var Case1 = document.getElementById("<%=hidCompID.ClientID %>")
                var Name = document.getElementById("<%=Coworker.ClientID %>")               
                var msg = "You are going to share access to compariosn " + Case1.value + " with user " + Name.options[Name.selectedIndex].text + ". Do you want to proceed?"
           
            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }
            
        }

        function Message(Case, Flag, Type) {
       
            // alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_CaseComp").value);
          

            if (Flag == 'NC') 
            {
                 var modType="<%=Session("SavvyModType")%>"; 
                 var cnt=0;
                if(modType=="2")
                {
                               var SisterCases="<%=Session("E3SisterCases")%>"; 
                               var sisCases="";
                               var sisStatus="";
                               var statusId=SisterCases.split(',');
                              // alert(statusId[0]);
                             // alert(SisterCases);
                                var x = document.getElementById("CaseComp");
                                for (var i = 0; i < x.options.length; i++) {
                                    if (x.options[i].selected == true) {
                                      //  alert(x.options[i].value);
                                        if(statusId[i]=="5")
                                        {
                                         // alert(sisCases=x.options[i].value);
                                          if(cnt==0)
                                          {
                                            sisCases=x.options[i].value;
                                          }
                                          else
                                          {
                                            sisCases=sisCases+","+ x.options[i].value;
                                          }
                                          cnt=cnt+1;
                                          
                                        }
                                    }
                                }
                                //alert(sisCases);
                    if(sisCases=="")
                    {
                        var msg = "-----------------------------------------------------------------------\n"
                          msg = msg+"You are going to create a new Comparison. Do you want to proceed?\n-----------------------------------------------------------------------\n"
                    }
                    else
                    {
                         var msg = "-----------------------------------------------------------------------\n"
                         msg  = msg+" Following cases are Sister Case(s) " + sisCases + ".\nThe Sister Case(s) probably need to be upgraded to Approved cases in order for the comparision to make sense.\n\n You are going to create a new Comparison. Do you want to proceed?\n"
                         msg =msg+ "-----------------------------------------------------------------------\n"
                    }
                }
                else
                {
                //alert("SisterCases:"+sisCases);
                   var msg = "You are going to Create a new Comparison. Do you want to proceed?"
                }
            }

            if (Flag == 'DC') {
                var Case1 = document.getElementById(Case)
                if (Case1.value == "0") {
                    alert("Please select a Comparison");
                    return false;
                }
                else
                    msg = "You are going to Delete Comparison#" + Case1.value + ". Do you want to proceed?"
            }

            if (Flag == 'SC') {

                var Case1 = document.getElementById(Case)               
                if (Case1.value == "0" ) {
                    alert("Please Select a Comparison");
                    return false;
                }
                else
                    return true; 

            }

            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
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
                                        <asp:ImageButton ID="btnGlobalManager" runat="server" ImageUrl="~/Images/Close.gif" OnClientClick="javascript:CloseWindow();"
                                        Text="Close Window" CssClass="ButtonWMarigin" onmouseover="Tip('Close this Window')"
                                        onmouseout="UnTip()" />
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
    <div id="div1" style="margin-top: 0px; margin-left: 2px;" runat="server">
        <table width="845px">
            <tr class="AlterNateColor4">
                <td class="PageSHeading" style="font-size: 14px; padding-left: 10px;">
                    Source Comparison
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td align="left" >
                    <%-- <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="600px" runat="server">
                                                </asp:DropDownList>--%>
                    <asp:LinkButton ID="lnkComparison" Style="font-size: 14px; padding-left: 10px;" runat="server"
                        CssClass="Link" Width="300px" OnClientClick="return ShowPopup('ComparisonDetails.aspx?Des=lnkComparison&Id=hidCompID&Des1=hidCompDes');">Select  Comparison</asp:LinkButton>
                    <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                        Text="You currently have no Proprietary Comparison to display. You can create a Comparison with the Tools below."></asp:Label>
                </td>
            </tr>
            <tr class="AlterNateColor1" style="height: 30px">
               <td align="left">
                    <%--<asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="ButtonWMarigin" OnClientClick="return Message('ctl00_Econ3ContentPlaceHolder_hidCompID','CC','Proprietary');"
                    ToolTip="Create A copy Of Report" Style="margin-left: 10px" Width="60px" CausesValidation="false" />--%>
                    <asp:Button ID="btnShare" runat="server" Text="Share Access" CssClass="ButtonWMarigin"
                        Style="margin-left: 10px;" 
                        ToolTip="Share Access of this Comparison with another User"  Width="100px" CausesValidation="false" />
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="ButtonWMarigin" Style="margin-left: 10px;"
                        OnClientClick="return Message('hidCompID','SC','Proprietary');"
                        ToolTip="Edit Comparison" Width="60px" CausesValidation="false" />
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                        ToolTip="Delete Comparison" Width="60px" OnClientClick="return Message('hidCompID','DC','Proprietary');"
                        Style="margin-left: 10px;" CausesValidation="false" />
                    <asp:Button ID="btnCreate" runat="server" Text="Create Comparison" CssClass="ButtonWMarigin"
                        ToolTip="Create a New Comparison" Width="127px" Style="margin-left: 10px" CausesValidation="false" />
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="divCreateComp" style="margin-top: 10px; margin-left: 2px;" runat="server"
        visible="false">
        <table style="width: 600px;">
            <tr class="AlterNateColor4">
                <td class="PageSHeading" colspan="2">
                    Create a New Comparison
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td align="right">
                    Enter Comparison Name:
                </td>
                <td>
                    <asp:TextBox ID="ComparisonName" runat="server" MaxLength="25" onChange="javascript:CheckSP(this);" CssClass="LongTextBox" Style="text-align: left;"></asp:TextBox>
                    <asp:Label ID="CreateCompError" runat="server" Visible="false" ForeColor="red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr style="height: 20px" class="AlterNateColor2">
                <td style="width: 600px; padding-left: 15px;" colspan="2">
                    Hold control key to select multiple Structures.
                    <br />
                    Maximum 10 Structures per Comparison
                </td>
            </tr>
            <tr>
                <td style="width: 600px" colspan="2">
                    <asp:ListBox CssClass="DropDown" ID="CaseComp" EnableViewState="true" runat="server"
                        Enabled="true"  SelectionMode="Multiple" Width="600px"
                        Height="193px"></asp:ListBox>
                </td>
            </tr>
            <tr style="height: 50px" class="AlterNateColor1">
                <td style="width: 600px" colspan="2">
                    <%--  <input class="Button" type="button" value="Create Comparison" />--%>
                    <asp:Button ID="btnCreateComp" Text="Create Comparison" runat="server" OnClientClick="return Message('hidCompID','NC','Proprietary');"
                        class="Button" ToolTip="Create Comparison"/>
                    <%-- <input class="ResetButton" type="reset" value="Reset" />--%>
                    <asp:Button ID="btnCancel" Text="Cancel" runat="server" class="Button" ToolTip="Cancel Comparison"/>
                </td>
            </tr>
            <tr class="AlterNateColor2">
                <td colspan="2">
                    <div id="newcomparisiondiv" style="display: none">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <br />
                                </td>
                                <td>
                                    <asp:Button ID="StartComp" runat="server" CausesValidation="true" CssClass="Button"
                                        Text="Create Comparison" />
                                    <br />
                                </td>
                                <td align="left">
                                    <input type="button" class="Button" name="cancle" value="Cancel" onclick="return MakeInVisible('newcomparisiondiv')" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    <div id="divShareComp" style="margin-top: 10px; margin-left: 2px;" runat="server"
        visible="false">
        <table style="width: 600px;">
            <tr>
                <td style="padding-left: 10px;" class="AlterNateColor4">
                    <b>Share with this User</b>
                </td>
            </tr>
            <%-- <tr id="nodatatr2" runat="server">
                <td style="font-size: 15px; font-weight: bold; padding-left: 10px;">
                    &nbsp;There is no Proprietary Comparisons please start with new comaprison
                    <br />
                </td>
            </tr>--%>
            <%--  <tr id="headingtr1" runat="server">
                <td style="font-size: 15px">
                    &nbsp;&nbsp;Existing Comparisons
                </td>
            </tr>--%>
            <%-- <tr id="ddtr2" runat="server">
                <td style="height: 22px">
                    <asp:DropDownList ID="SharedComp" CssClass="DropDown" runat="server" Width="548px">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>--%>
            <%--  <tr id="headingtr2" runat="server" class="AlterNateColor1">
                <td style="font-size: 15px">
                    &nbsp;&nbsp; Share with this User
                </td>
            </tr>--%>
            <tr id="ddtr3" runat="server" class="AlterNateColor2">
                <td>
                    <asp:DropDownList ID="Coworker" CssClass="DropDown" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="buttontr3" runat="server" class="AlterNateColor1">
                <td>
                    <asp:Button ID="SharedButton" Text="Share Access" CssClass="Button" runat="server"
                        OnClientClick="return ShareMessageNew();" ToolTip="Share Access with this user" CausesValidation="False" />
                    <asp:Button ID="btnSCancel" Text="Cancel" tooltip="Cancel" CssClass="Button" runat="server" CausesValidation="False" />
                </td>
            </tr>
             
        </table>
        
    </div>
    
    <div id="divCreate" style="margin-top: 0px; margin-left: 2px;" runat="server" visible="false">
    </div>
      <%--<div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>--%>
        <div id="div2" style="margin-top: 0px; margin-left: 2px;" runat="server">
        <table width="845px">
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
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
    <asp:HiddenField ID="hidCompID" runat="server" />
    <asp:HiddenField ID="hidCompDes" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
