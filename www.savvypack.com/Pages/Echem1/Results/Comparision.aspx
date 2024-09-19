<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comparision.aspx.vb" Inherits="Pages_Echem1_Results_Comparision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Echem1-Profit and Loss Comparison</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
        <script type="text/JavaScript">
          
            
      function OpenNewWindow(Page,Title) {

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
            newwin = window.open(Page, 'NewWindowComparison' + Title, params);
            

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
    <form id="form2" runat="server">
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
                    
               </td>
            </tr>
        </table>
       </div>
            
      <div id="error">
            <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
          </div>
    
       <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                     <div class="PageHeading" id="divMainHeading" style="width:840px;">
                         <asp:Label ID="lblTitle" runat="server"></asp:Label>                         
                     </div>
    
                   
                                    
                </td>
            </tr>
             <tr style="height:20px">
                <td>
                <div id="ContentPagemargin" runat="server">
                   <div id="PageSection1" style="text-align:left;" >
                          <br />
                          <table width="80%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                        Select a Case to compare to Case <asp:Label ID="lblSessionCaseId" runat="server"></asp:Label>:        
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="80%" runat="server"></asp:DropDownList>
                                    </td>
                                 </tr> 
                                  
                          </table>
                          <br />
                          <table width="80%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                        Create a comparison in tabular form based on:        
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlType" CssClass="DropDown" Width="20%" runat="server">
                                            <asp:ListItem  Text="Total" Value="TOT"></asp:ListItem>
                                            <asp:ListItem  Text="Total Per Weight" Value="BYWT"></asp:ListItem>
                                            <asp:ListItem  Text="Total Per Unit" Value="BYVOL"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                 </tr> 
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnType" runat="server" Text="Display Comparison" CssClass="ButtonWMarigin" />
                                    </td>
                                </tr>
                                  
                          </table>
                          <br />
                          <table width="80%">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                        Create a comparison in graphical form with a:        
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlChrtType" CssClass="DropDown" Width="20%" runat="server">
                                          <asp:ListItem  Text="Regular Bar Chart" Value="RBC"></asp:ListItem>
                                          <asp:ListItem  Text="Stacked Bar Chart" Value="SBC"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                 </tr> 
                                  
                                   <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnChartType" runat="server" Text="Display Comparison" CssClass="ButtonWMarigin" />
                                    </td>
                                </tr>
                          </table>
                   </div>
                 </div>
                 </td>
             </tr>
          </table>
   
   </div>
   
    </form>
</body>

</html>
