<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tool.aspx.vb" Inherits="Pages_Market1_Tools_Tool"
    Title="Market1-Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript">
        javascript: window.history.forward(1); 
    
        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;


        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }
        
         function ShowPopWindow(Page,HidID) 
         {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            
            var width = 500;
            var height = 180;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var Hid = document.getElementById(HidID).value
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            Page = Page+'&hidValue='+Hid
//            alert(Page);
            newwin = window.open(Page, 'PopUp', params);
            
        }

        function checkUint(colSeq) {
           // alert(colSeq);
            if (document.getElementById("hidReportType").value == 'UNIFORM') {

                var str = document.getElementById("Column_" + colSeq).innerText;
                if (str.indexOf('CAGR') != -1) {
                    document.getElementById("lbl_" + colSeq).innerText = '(%)';
                }
                else {
                    var txt = document.getElementById("hidUnitShort").value;
                    //var txt = e.options[e.selectedIndex].value; 
                    document.getElementById("lbl_" + colSeq).innerText =  txt ;
                }

               
            }

        }
        
        function checkNumeric()
        {
            if(checkNumeric1('txtRw')==true)
            {
                if (checkNumeric1('txtCol')==true)
                    if(checkNumeric1('txtFilter')==true)
                        return true;
            }
           return false;
            
            
        }
       function checkNumeric1(id)
      {         
         var IsNumber=true;
         var ValidChars = "0123456789";
         var length;
         var rownum=document.getElementById(id).value;
         var i;                            
         {
              for (i = 0; i < rownum.length; i++) 
              { 
                  Char = rownum.charAt(i); 
                  if (ValidChars.indexOf(Char) == -1) 
                     {
                      IsNumber = false;
                      break;
                     }                    
              }            
              if(IsNumber==true)
              {              
                return true;
              }
              else
              {       
                  alert("Please enter valid number");  
                 return false;
              }
          }
      }
        
    function ShowPopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  

            newwin = window.open(Page, 'Chat', "");
            return false;

        }
    </script>

    <script type="text/javascript">


        function Message(Case, Flag, Type) 
        {
            
            if (Flag == 'CC') {
                var Case1 = document.getElementById(Case)
                
                if (Case1.value=="0")
                  {
                      alert("Please select " + Type + " Report");
                    return false;
                    }
                else
                    var msg = "You are going to create a copy of " + Type + " Report#" + Case1.value + ". Do you want to proceed?"
            }
            if (Flag == 'NC') {
                var msg = "You are going to create a new report. Do you want to proceed?"
            }

            if (Flag == 'DC') {
                var Case1 = document.getElementById(Case)
                 if (Case1.value=="0")
                   {
                       alert("Please select " + Type + " Report");
                    return false;
                    }
                else
                msg = "You are going to delete Report#" + Case1.value + ". Do you want to proceed?"
            }

                      if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

        }
        function Validation(type) {
            if (type = 'Prop') {
                var repText = document.getElementById("lnkReports").innerText;
                document.getElementById("hidPropRptDes").value = repText;
            }
            else if (type = 'Base') {
                var repText = document.getElementById("lnkBReports").innerText;
                document.getElementById("hidBaseRptDes").value = repText;
            }
            else if (type = 'PropRen') {
                var repText = document.getElementById("lnkReports").innerText;
                document.getElementById("hidPropRptDes").value = repText;
            }






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
                <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                    runat="server" />
            </div>
            <div>
                <table class="M1Module" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/GlobalManager.gif"
                                            Text="Global Manager" CssClass="ButtonWMarigin" PostBackUrl="~/Pages/Market1/Default.aspx"
                                            onmouseover="Tip('Return to Global Manager')" onmouseout="UnTip()" CausesValidation="false" />
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
            <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
                <tr style="height: 20px">
                    <td>
                        <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Market1 - Tools')"
                            onmouseout="UnTip()" style="width: 840px;">
                            Market1 - Tools
                        </div>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td>
                        <div id="ContentPagemargin" runat="server">
                            <div id="PageSection1" style="text-align: left">
                                <div id="div" runat="server" visible="true">
                                <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Source Base Reports
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                       <%-- <asp:DropDownList ID="ddlBCases" CssClass="DropDown" Width="96%" runat="server">
                                        </asp:DropDownList>--%>
                                        <asp:LinkButton ID="lnkBReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                        Width="300px" OnClientClick="return ShowPopup('../PopUp/ReportDetails.aspx?Des=lnkBReports&Id=hidBaseRpt&Type=Base');">Select Base Report</asp:LinkButton>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnCopyBcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                            OnClientClick="return Message('hidBaseRpt','CC','Base');" 
                                            onmouseover="Tip('Create a Copy of this Case')" onmouseout="UnTip()" />
                                        
                                    </td>
                                </tr>
                            </table>
                            <br />
                                    <table width="820px">
                                     
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;">
                                                Source Proprietary Reports
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                               <%-- <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="600px" runat="server">
                                                </asp:DropDownList>--%>
                                                <asp:LinkButton ID="lnkReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                        Width="300px" OnClientClick="return ShowPopup('../PopUp/ReportDetails.aspx?Des=lnkReports&Id=hidPropRpt&Type=Prop');">Select Proprietary Report</asp:LinkButton>
                                                <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                    Text="You currently have no Proprietary Report to display. You can create a Report with the Tools below."></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" style="height: 30px">
                                            <td>
                                                <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="ButtonWMarigin" OnClientClick="return Message('hidPropRpt','CC','Proprietary');"
                                                    onmouseover="Tip('Create a copy this Report')" onmouseout="UnTip()" Style="margin-left: 10px"
                                                    CausesValidation="false" />
                                                <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="ButtonWMarigin" onmouseover="Tip('Edit this Report')"
                                                    onmouseout="UnTip()" Style="margin-left: 10px; " OnClientClick="return Validation('Prop')" 
                                                    CausesValidation="false" />
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                                    OnClientClick="return Message('hidPropRpt','DC','Proprietary');" onmouseover="Tip('Delete this Report')"
                                                    onmouseout="UnTip()" Style="margin-left: 10px; height: 26px;" 
                                                    CausesValidation="false" />
                                                <asp:Button ID="btnCreate" runat="server" Text="Create Mixed Report" CssClass="ButtonWMarigin"
                                                    onmouseover="Tip('Create A New Report')" onmouseout="UnTip()" Style="margin-left: 10px"
                                                    CausesValidation="false" />
                                                 <asp:Button ID="btnCreateSpecialReport" runat="server" Text="Create Uniform Report" CssClass="ButtonWMarigin"
                                                    onmouseover="Tip('Create A Special Report')" onmouseout="UnTip()" Style="margin-left: 10px"
                                                    CausesValidation="false" />
                                                &nbsp;</td>
                                                
                                        </tr>
                                    </table>
                                </div>
                                <br />
                                <div id="divCreateSpecial" style="margin-top: 0px; margin-left: 2px;" runat="server" visible="false">
                                <table style="width: 600px;">
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                Special Report Details
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td align="right">
                                                Report Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecialReportName" runat="server" CssClass="LongTextBox" Style="text-align: left;"
                                                    MaxLength="25"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpecialReportName"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         <tr class="AlterNateColor2"  >
                                            <td align="right">
                                                Report Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlSpecialType"  CssClass="DropDown" Width="250px" runat="server" AutoPostBack="true">                                                
                                                <asp:ListItem Text="Create Report By RegionSet" Value="REGION"></asp:ListItem>
                                                <asp:ListItem Text="Create Report By Country" Value="CNTRY"></asp:ListItem>
                                                </asp:DropDownList>
                                               
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" >
                                            <td align="right">
                                                Select RegionSet:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegionSet"  CssClass="DropDown" Width="250px" runat="server" AutoPostBack="true">                                                                                               
                                                </asp:DropDownList>                                               
                                            </td>
                                        </tr>
                                         <tr class="AlterNateColor1" id="rowRegions" runat="server" visible="false">
                                            <td align="right">
                                                Select Region:                                             </td>
                                            <td>
                                                <asp:DropDownList ID="ddlRegions"  CssClass="DropDown" Width="150px" runat="server">                                                                                               
                                                </asp:DropDownList>                                               
                                            </td>
                                        </tr>
                                       <tr class="AlterNateColor2" >
                                            <td align="right">
                                                Select Unit:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlUnits"  CssClass="DropDown" Width="150px" runat="server">                                                                                               
                                                </asp:DropDownList>                                               
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td align="right">
                                                Report Columns:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecialCols" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                    MaxLength="5" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSpecialCols" runat="server"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2">
                                            <td align="right">
                                                Report Filters:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtSpecialFilters" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                    MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtSpecialFilters" runat="server"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         
                                       
                                          
                                        <tr class="AlterNateColor1">
                                            <td align="right">
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSpecialSubmit" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />
                                                <asp:Button ID="btnSpecialSubmit2" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" Visible="false" />
                                                &nbsp;<asp:Button ID="btnSpecialCancel" runat="server" CssClass="Button" Text="Cancel" Style="margin: 0 0 0 0;"
                                                    CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div id="divCreate" style="margin-top: 0px; margin-left: 2px;" runat="server" visible="false">
                                    <table style="width: 600px;">
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                Report Details
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td align="right">
                                                Report Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRptName" runat="server" CssClass="LongTextBox" Style="text-align: left;"
                                                    MaxLength="25"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtRep" runat="server" ControlToValidate="txtRptName"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        
                                        <tr class="AlterNateColor2" id="rowRows" runat="server" visible="true">
                                            <td align="right">
                                                Report Rows:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtRw" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                    MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtRw" runat="server" ControlToValidate="txtRw"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td align="right">
                                                Report Columns:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtCol" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                    MaxLength="5" ></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtCol" ControlToValidate="txtCol" runat="server"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2">
                                            <td align="right">
                                                Report Filters:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtFilter" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                    MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="reqtxtFilter" ControlToValidate="txtFilter" runat="server"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                         
                                       
                                          
                                        <tr class="AlterNateColor1">
                                            <td align="right">
                                            </td>
                                            <td>
                                                <asp:Button ID="btnSubmitt" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />
                                                <asp:Button ID="btnSubmitt2" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" Visible="false" />
                                                &nbsp;<asp:Button ID="btnCancle" runat="server" CssClass="Button" Text="Cancel" Style="margin: 0 0 0 0;"
                                                    CausesValidation="false" />
                                            </td>
                                        </tr>
                                    </table>
                                    <br />                                   
                                </div>
                                 <div id="divReportFrameWork" style="margin-top: 0px; margin-left: 4px;" runat="server"
                                        visible="true">
                                        <asp:Table ID="tblCAGR" runat="server" CellPadding="0" CellSpacing="1">
                                        </asp:Table>
                                        <br />
                                        <asp:Button ID="btnAddNew" runat="server" CssClass="Button" Text="Save" Style="margin: 0 0 0 0;"
                                            Visible="false"  />
                                  </div>
                                <br />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
               <asp:HiddenField ID="hidBaseRpt" runat="server" />
                        <asp:HiddenField ID="hidBaseRptDes" runat="server" />
            <asp:HiddenField ID="hidPropRpt" runat="server" />
            <asp:HiddenField ID="hidPropRptDes" runat="server" />
            <asp:HiddenField ID="hidReportType" runat="server" />
             <asp:HiddenField ID="hidUnitShort" runat="server" />
        </div>
        
    </form>

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

</body>
</html>
