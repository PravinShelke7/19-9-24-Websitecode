<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Echem1_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Echem1-Global Manager</title>
     <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
   
    <script type="text/JavaScript">
        javascript: window.history.forward(1); 
        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
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
            newwin = window.open(Page, 'Chat', params);
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

            var combo1 = document.getElementById("ddlPCase");
            var val = combo1.options[combo1.selectedIndex].text
            var CaseDe = val.split("=");
          
            document.getElementById("txtCaseDe1").value = trimAll(CaseDe[1].replace("UNIQUE FEATURES", ""));
            document.getElementById("txtCaseDe2").value = trimAll(CaseDe[2]);
        }

        function GetCaseId() {

            objItemElement = document.getElementById("ddlPCase")

            document.getElementById("txtCaseid").value = objItemElement.value




            if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) {
                alert("PACKAGING FORMAT cannot be blank");
                return false;
            }
            else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) {
                alert("UNIQUE FEATURES cannot be blank");
                return false;
            }
            else {
                return confirm("Do you want to rename Case " + objItemElement.value + " ?");

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

  (function() {
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
        <table class="Echem1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>
                                <td >
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif" runat="server" ToolTip="Update" Visible="false" />  
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif" runat="server"  ToolTip="Log Off"  Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=ECH1"/>  
                                    
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
                         Echem1 - Global Manager
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
                                        Select from Available Base Cases 
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlBaseCases" CssClass="DropDown" Width="98%" runat="server"></asp:DropDownList>
                                    </td>
                                 </tr> 
                                 <tr class="AlterNateColor1">
                                    <td>  
                                        <asp:Button ID="btnBCase" runat="server"  CssClass="Button" Text="Start a Base Case" style="margin-left:0px;" />
                                        <asp:Button  ID="btnBCaseSearch" runat="server" Text="Case Search" CssClass="Button" OnClientClick="return ShowPopWindow('PopUp/CaseSearch.aspx?Id=ddlBaseCases');"></asp:Button>

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
                                            <td class="PageSHeading" style="font-size: 14px;">
                                                Select from Client Case Groups
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                                <asp:DropDownList ID="ddlCaseGroup" CssClass="DropDown" Width="98%" runat="server"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-top: 10px;">
                                            </td>
                                        </tr>
                                    </table>
                                    
                             <table width="90%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                        Select from Available Proprietary Client Cases
                                    </td>
                                </tr>
                                 <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlPCase" CssClass="DropDown" Width="98%" runat="server"></asp:DropDownList>
                                        <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false" Text="You currently have no Proprietary Cases to display. You can create a Case with the Tools below."></asp:Label>
                                    </td>
                                 </tr> 
                                 <tr class="AlterNateColor1">
                                    <td>  
                                         <asp:Button ID="btnPCase" runat="server"  CssClass="Button" Text="Start a Proprietary Case" style="margin-left:0px;" />                                        
                                         <asp:Button ID="btRename" runat="server" Text="Rename"  CssClass="Button" OnClientClick="return MakeVisible('divRename')"></asp:Button>                               
                                         <asp:Button  ID="btSerach" runat="server" Text="Case Search" CssClass="Button" ></asp:Button>
                                    </td>
                                 </tr>
                                  <tr class="AlterNateColor1">
                                    <td>
                                         <div style="margin-left:20px;display:none" id="divRename">
                                            <table width="80%" style="padding-left:20px">
				                            <tr align="left">
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
				                                    <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SmallTextBox" style="text-align:left;width:230px" MaxLength="25"></asp:TextBox>
				                                </td>
				                                 <td>
				                                    <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="SmallTextBox" style="text-align:left;width:230px" MaxLength="25"></asp:TextBox>
				                                </td>
				                                <td>
				                                    <asp:Button ID="btnUpdate" runat="server" Text="Update"  CssClass="Button" style="margin-left:0px;" OnClientClick="return GetCaseId();"></asp:Button>     	
				                                </td>
				                                 <td>
				                                     <asp:Button ID="Button1" runat="server" Text="Cancel"  CssClass="Button" OnClientClick="return MakeInVisible('divRename')" style="margin-left:0px;"></asp:Button> 
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
                                        <asp:Button ID="btnToolBox" runat="server"  CssClass="Button" Text="Go To Toolbox" style="margin-left:0px;" />                                        
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
    </form>
</body>
</html>
