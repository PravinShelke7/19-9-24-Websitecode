<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comparison.aspx.vb" Inherits="Pages_Sustain2_Results_Comparison" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
       
     <%-- <div id="AlliedLogo">
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>    --%>    
      
      <div>
        <table class="S2Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
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
                                        <asp:DropDownList ID="ddlType" CssClass="DropDown" Width="200px" runat="server">
                                            <asp:ListItem  Text="Energy Total" Value="TOT"></asp:ListItem>
                                            <asp:ListItem  Text="Energy Per Weight" Value="BYWT"></asp:ListItem>
                                            <asp:ListItem  Text="Energy Per Unit" Value="BYVOL"></asp:ListItem>
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
                                        <asp:Button ID="btnChartType" runat="server" Text="Display Comparison" CssClass="ButtonWMarigin"  />
                                    </td>
                                </tr>
                          </table>
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
    </form>
</body>
</html>
