<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Pages_MedSustain1_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMed1-Global Manager</title>
     <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
   
    <script type="text/JavaScript">
        javascript: window.history.forward(1);
        function Check() 
        {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
           
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
            {
                var statudId = document.getElementById("<%=hidPropCaseSt.ClientID %>").value;
                // alert(statudId);
                if (statudId != "" && statudId != "5") {
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
        function PropCaseCheck() {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            // alert(Case);
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
        }
        function Message() {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
           // alert(Case);
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
            var width = 800;
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
        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            //GetCaseDe();
            return false;





        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }

        function GetCaseDe() {

            var combo1 = document.getElementById("ddlPCase");
            var val = combo1.options[combo1.selectedIndex].text
            var CaseDe = val.split("=");

            document.getElementById("txtCaseDe1").value = trimAll(CaseDe[1].replace("UNIQUE FEATURES", ""));
            document.getElementById("txtCaseDe2").value = trimAll(CaseDe[2]);
        }

        function GetCaseId() {

            // objItemElement = document.getElementById("ddlPCase")

            document.getElementById("txtCaseid").value = document.getElementById("<%=hidPropCase.ClientID %>").value;




            if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) {
                alert("PACKAGING FORMAT cannot be blank");
                return false;
            }
            else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) {
                alert("UNIQUE FEATURES cannot be blank");
                return false;
            }
            else {
                return confirm("Do you want to rename Case " + document.getElementById("<%=hidPropCase.ClientID %>").value + " ?");

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
            var width = 800;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var URL
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            URL = "../Econ1/help/SavvyPackInstructions1.htm"
            newwin = window.open(URL, 'Chart', params);
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
        <table class="SMed1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>
                                <td >
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif" runat="server" ToolTip="Update" Visible="false" />  
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif" runat="server"  ToolTip="Log Off"  Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=S1"/>  
                                    
                                </td>  
                                <td>
                                    <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif" runat="server"  ToolTip="Instructions"  Visible="true" OnClientClick="return Help();"/>  
                                </td>  
                                <td>
                                    <asp:ImageButton ID="imgChart" ImageAlign="Middle" ImageUrl="~/Images/ChartN.gif" runat="server"  ToolTip="Charts"  Visible="false"/>  
                                </td> 
                                <td>
                                    <asp:ImageButton ID="imgFeedback" ImageAlign="Middle" ImageUrl="~/Images/FeedbackN.gif" runat="server"  ToolTip="Feedback"  Visible="false"/>  
                                </td> 
                                <td>
                                    <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/Notes.gif" runat="server"  ToolTip="Notes"  Visible="false"/>  
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
                     <div class="PageHeading" id="divMainHeading" style="width:840px;">
                         SMed1 - Global Manager
                     </div>
    
                   
                                    
                </td>
            </tr>
             <tr style="height:20px">
                <td>
                <div id="ContentPagemargin" runat="server">
                   <div id="PageSection1" style="text-align:left;" >
                           <br />
                            <div class="PageHeading" style="width:80%"><center>Select from Existing Cases</center></div>
                              <table width="90%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                        Select from Available Approved Cases 
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlBaseCases" CssClass="DropDown" Width="98%" runat="server" Visible="false" ></asp:DropDownList>
                                          <asp:LinkButton ID="lnkApprovedCase" Style="font-size: 14px;padding-left: 10px" runat="server" CssClass="Link" 
                                        Width="500px" OnClientClick="return ShowPopWindow('PopUp/CasesSearch.aspx?Des=lnkApprovedCase&Id=hidApprovedCase&IdD=hidApprovedCaseD','AP');">Select case</asp:LinkButton>
                                    </td>
                                 </tr> 
                                 <tr class="AlterNateColor1">
                                    <td>  
                                        <asp:Button ID="btnBCase" runat="server"  CssClass="Button" Text="Start an Approved Case" style="margin-left:0px;" />
                                        <asp:Button  ID="btnBCaseSearch" runat="server"  Text="Case Search" Visible="false"  CssClass="Button" OnClientClick="return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlBaseCases');"></asp:Button>

                                    </td>
                                 </tr>  
                                  <tr class="AlterNateColor1">
                                    <td>  
                                        
                                        
                                    </td>
                                 </tr>    
                            </table>    
                            <br />
                             <table width="90%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                       Select from Client Case Groups
                                    </td>
                                </tr>
                                  <tr class="AlterNateColor1">
                                     <td>
                                        <asp:DropDownList ID="ddlCaseGroup" CssClass="DropDown" Visible="false"  Width="98%" runat="server" AutoPostBack="true"></asp:DropDownList>
                                        <asp:LinkButton ID="lnkCaseGroup" Style="font-size: 14px;padding-left: 10px" runat="server" CssClass="Link" 
                                        Width="500px" OnClientClick="return ShowPopWindow('PopUp/CaseGroup.aspx?Des=lnkCaseGroup&Id=hidGroupId&IdD=hidGroupCaseD','PRP');">All Groups and All Cases</asp:LinkButton>
                                        
                                     </td>
                                </tr>
                                  </tr>
                                   <tr >
                                    <td style="padding-top:10px;">
                                      
                                    </td>
                                </tr>
                                 <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                        Select from Available Proprietary Client Cases
                                    </td>
                              
                                 <tr class="AlterNateColor1">
                                    <td>
                                        <%--<asp:DropDownList ID="ddlPCase" Visible="false"  CssClass="DropDown" Width="98%" runat="server"></asp:DropDownList>--%>
                                          <asp:LinkButton ID="lnkPropCase" Style="font-size: 14px;padding-left: 10px" runat="server" CssClass="Link" 
                                        Width="500px" OnClientClick="return ShowPopWindow('PopUp/CasesSearch.aspx?Des=lnkPropCase&Id=hidPropCase&IdD=hidPropCaseD','PRP');">Select case</asp:LinkButton>
                                        <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false" Text="You currently have no Proprietary Cases to display. You can create a Case with the Tools below."></asp:Label>
                                    </td>
                                 </tr> 
                                 <tr class="AlterNateColor1">
                                    <td style="display:inline">  
                                         <table>
                                    	<tr>
                                    	<td>
                                        	 <asp:Button ID="btnPCase" runat="server"  CssClass="Button" 
                                        	     Text="Start a Proprietary Case" style="margin-left:0px;" 
                                        	     OnClientClick="return PropCaseCheck();" Width="159px" />                                        
                                        </td>
                                        <td>
                                        	<asp:Button ID="btRename" runat="server" Text="Rename"  CssClass="Button" 
                                        	     OnClientClick="return PropCaseCheck();" Width="65px"></asp:Button> 
                                        </td>
                                        <td>
                                        	<asp:Button  ID="btnAdminSubmit" runat="server" style="display:none" Text="Submit" 
                                             OnClientClick="return Check();" 
                                             ToolTip="Submit for approval for addition to Approved cases" CssClass="Button"></asp:Button>                              
                                        </td>
                                        <td>
                                        	 <asp:Button  ID="btSerach" runat="server" Text="Case Search" Visible="false" 
                                        	     OnClientClick="return PropCaseCheck();"  CssClass="Button" Width="86px"></asp:Button>
                                        </td>
                                        <td>
                                         <asp:Button  ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return Message();" ToolTip="Submitted for approval for addition to Approved cases" CssClass="Button"></asp:Button>
                                         </td>
                                         </tr>
                                         </table>
                                    </td>
                                 </tr>
                                  <tr class="AlterNateColor1">
                                    <td>
                                         <div style="margin-left:20px;display:none" id="divRename" runat="server" >
                                            <table width="90%" style="padding-left:20px">
				                         <%--   <tr align="left">
				                                <td>
				                                    PACKAGING FORMAT 
				                                </td>
				                                 <td>
				                                    UNIQUE FEATURES 
				                                </td>
				                                <td>
				                                    <asp:HiddenField id="txtCaseid" runat="server" />
				                                </td>
				                                <td>
				                                    
				                                </td>
				                                 
				                            </tr>
				                               <tr align="left">
				                                <td>
				                                    <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" style="text-align:left;width:230px" MaxLength="25"></asp:TextBox>
				                                </td>
				                                 <td>
				                                    <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" style="text-align:left;width:230px" MaxLength="25"></asp:TextBox>
				                                </td>
				                                <td>
				                                    <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="Button" style="margin-left:0px;" OnClientClick="return GetCaseId();"></asp:Button>     	
				                                </td>
				                                 <td>
				                                     <asp:Button ID="Button1" runat="server" Text="Cancle"  CssClass="Button" OnClientClick="return MakeInVisible('divRename')" style="margin-left:0px;"></asp:Button> 
				                                </td>
				                                
				                            </tr>--%>

                                                  <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Packaging Format:</b>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;font-size:11px;
                                                width: 230px" MaxLength="25"></asp:TextBox>
                                        </td> 
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Unique Features:</b> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;font-size:11px;
                                                width: 230px" MaxLength="25"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            <b>Description:</b> 
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCaseDe3" runat="server" onKeyUp="javascript:Count(this);"
                                                    onChange="javascript:Count(this);" CssClass="MediumTextBox" Style="text-align: left;font-size:11px;"
                                                TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                        </td>
                                    </tr>
                                      <tr class="AlterNateColor1">
                                        <td>
                                        </td>
                                        <td>
                                          <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="Button" style="margin-left:0px;" OnClientClick="return GetCaseId();"></asp:Button>     	

                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel"  CssClass="Button"  style="margin-left:0px;"></asp:Button> 
                                        </td>
                                    </tr>
										 </table>
                                        </div>
                                    </td>
                                 </tr>   
                            </table>                         
                            <br />
                            <div class="PageHeading" style="width:80%"><center>Case Modification Tools</center></div>
                               <table width="90%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                            Toolbox
                                    </td>
                                 </tr>
                                  <tr class="AlterNateColor1">
                                    <td>  
                                        <asp:Button ID="btnToolBox" ToolTip="Manage Groups and Cases" runat="server"  CssClass="Button" Text="Go To Toolbox" style="margin-left:0px;" Visible="true"  />                                        
                                        
                                    </td>
                                 </tr>   
                                  <tr class="AlterNateColor1">
                                    <td>  
                                        
                                        
                                    </td>
                                 </tr>    
                                 
                            </table>                         
                            <br />
                    
                    
                    </div>  
               </div>
               </td>
            </tr>
            <tr class="AlterNateColor3">
             <td class="PageSHeading" align="center">
               <asp:Label ID="lblTag" runat="Server" ></asp:Label>
            </td>
           </tr>
        </table>
         <asp:HiddenField id="txtCaseid" runat="server" />
         <asp:HiddenField ID="hidApprovedCase" runat="server" />
         <asp:HiddenField ID="hidPropCase" runat="server" />

         <asp:HiddenField ID="hidApprovedCaseD" runat="server" />
         <asp:HiddenField ID="hidPropCaseD" runat="server" />
         <asp:HiddenField ID="hidPropCaseSt" runat="server" />
         <asp:HiddenField ID="hidGroupId" runat="server" />

                
         <asp:HiddenField ID="hidGroupCaseD" runat="server" />
    </form>
</body>
</html>
