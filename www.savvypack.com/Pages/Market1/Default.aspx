<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Market1_Default" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>M1-Global Manager</title>
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
            return false;

        }
        function ShowPopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  

            newwin = window.open(Page, 'Chat', "");
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

        }
        function AlertMessage(Type) {
            var Page;
            if (Type == 'RegionSet')
                Page = "GeographicRegionSet.aspx";
            else if (Type == 'Tree')
                Page = "ProductTree.aspx";
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
            params = '';
            newwin = window.open(Page, 'Chat', params);
            return false;

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
        function Validation(type) 
        {
            if (type == 'Prop') {
                var repText = document.getElementById("lnkReports").innerText;
                document.getElementById("hidPropRptDes").value = repText;
            }
            else if (type == 'Base') {
                var repText = document.getElementById("lnkBReports").innerText;               
                document.getElementById("hidBaseRptDes").value = repText;
            }
            else if (type == 'PropRen') {
                var repText = document.getElementById("lnkReports").innerText;
                document.getElementById("hidPropRptDes").value = repText;
            }






        }

        function GetCaseDe() {

            var combo1 = document.getElementById("ddlPCase");
            var val = combo1.options[combo1.selectedIndex].text

            var CaseDe = val.split(":");

            document.getElementById("txtRptName").value = trimAll(CaseDe[1]);

        }

        function GetReportId() {

            objItemElement = document.getElementById("hidPropRpt")

           
            if (document.getElementById("txtRptName").value.split(' ').join('').length == 0) {
                alert("Report Name cannot be blank");
                return false;
            }

            else {
                return confirm("Do you want to rename report " + objItemElement.value + " ?");

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
            URL = "../Econ2/help/SavvyPackInstructions1.htm"
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
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>        
      
      <div>
        <table class="M1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>
                                
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif" runat="server"  ToolTip="Log Off"  Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=M1"/>  
                                    
                                </td>   
                                <td>
                                    <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif" runat="server"  ToolTip="Instructions"  Visible="true" OnClientClick="return Help();"/>  
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
                         Market1 - Global Manager
                     </div>
                <div style="text-align: right; margin-right: 10px">
                    <a onclick="return AlertMessage('RegionSet');" id="lnkCreate" style="font-style: normal; font-family: Verdana;
                        font-size: 12px;" class="Link" href="#">Geographic Region Set</a>
                        <span> &nbsp;&nbsp;&nbsp;</span>
                          <a onclick="return AlertMessage('Tree');" id="A1" style="font-style: normal; font-family: Verdana;
                        font-size: 12px;" class="Link" href="#">Product Tree</a>
                </div>
                   
                                    
                </td>
            </tr>
             <tr style="height:20px">
                <td>
                <div id="ContentPagemargin" runat="server">
                   <div id="PageSection1" style="text-align:left;" >
                           <br />
                            <div class="PageHeading" style="width:80%"><center>Select from Existing Reports</center></div>
                               <table width="80%">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Select from Available Base Reports
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <%-- <asp:DropDownList ID="ddlBaseCases" CssClass="DropDown" Width="80%" runat="server"></asp:DropDownList>--%>
                                    <asp:LinkButton ID="lnkBReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                        Width="300px" OnClientClick="return ShowPopup('PopUp/ReportDetails.aspx?Des=lnkBReports&Id=hidBaseRpt&Type=Base');">Select Base Report</asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnBCase" runat="server" CssClass="Button" Text="Start a Base Case" OnClientClick="return Validation('Base')"
                                        Style="margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="80%">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Select from Available Proprietary Client Reports
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td id="tdPropCases" runat="server">
                                    <asp:LinkButton ID="lnkReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                        Width="300px" OnClientClick="return ShowPopup('PopUp/ReportDetails.aspx?Des=lnkReports&Id=hidPropRpt&Type=Prop');">Select Proprietary Report</asp:LinkButton>
                                </td>
                            </tr>
                           <%--  <tr class="AlterNateColor1">
                                    <td>                                        
                                        <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false" Text="Currently you have no Proprietary Report to display. You can create a Report with the Tools below."></asp:Label>
                                    </td>
                                 </tr> --%>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnPCase" runat="server" CssClass="Button" Text="Start a Proprietary Case"
                                        Style="margin-left: 0px;" OnClientClick="return Validation('Prop')" />
                                    <asp:Button ID="btRename" runat="server" Text="Rename" CssClass="Button" OnClientClick="return Validation('PropRen')"
                                        Enabled="true"></asp:Button>
                                         <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false" Text="Currently you have no Proprietary Report to display. You can create a Report with the Tools below."></asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <div style="margin-left: 20px;" id="divRename" runat="server"  visible="false" >
                                        <table width="80%" style="padding-left: 20px">
                                            <tr align="left">
                                                <td>
                                                    <b>Report Name:</b>
                                                    <asp:HiddenField ID="hypReportId" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRptName" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                        width: 200px" MaxLength="25"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="Button" Style="margin-left: 0px;"
                                                        OnClientClick="return GetReportId();"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Button" 
                                                        Style="margin-left: 0px;"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>                       
                            <br />
                            <br />
                            <div class="PageHeading" style="width:80%"><center>Case Modification Tools</center></div>
                               <table width="80%">
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
                    
                      <asp:HiddenField ID="hidPropRpt" runat="server" />
                        <asp:HiddenField ID="hidPropRptDes" runat="server" />
                        
                         <asp:HiddenField ID="hidBaseRpt" runat="server" />
                        <asp:HiddenField ID="hidBaseRptDes" runat="server" />
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

