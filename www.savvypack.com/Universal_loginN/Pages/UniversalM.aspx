<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UniversalM.aspx.vb" Inherits="Universal_loginN_Pages_UniversalM" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Universal Manager</title>
     <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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

        function fnAnnouncement() {
         
           
            document.getElementById("btnAnnouncement").click();
            return true;
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
<body  style="margin-top:5px;">
    <form id="form1" runat="server">
      <div id="MasterContent">
      <asp:HiddenField ID="hvPermission1" runat="Server" />
      <asp:HiddenField ID="hvPermission2" runat="Server" />
      <asp:HiddenField ID="hvPermission3" runat="Server" />
      <div id="AlliedLogo">
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>        
      
       <div>
        <table class="ULoginModule1" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse" border="0">
                        <tr>                
                           
                               <td>
                                        <asp:Image ID="ImageButton1" ImageAlign="right"  ImageUrl="~/Images/Announcements3.gif" runat="server"  ToolTip="Announcements"  Visible="true"/>  
                                        <div style="position:absolute;width:125px;margin-top:5px;">
                                            <asp:LinkButton ID="lnkAnnouncement" runat="server" style="font-size:13px;color:#f3f5f7;text-decoration:none;font-weight:bold;font-family:Times New Roman;"></asp:LinkButton>
                                        </div>                                                               
                                </td> 
                                
                                 <td style="width:10px">
                                    
                                </td>  
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageAlign="right"  ImageUrl="~/Images/Home2.gif" runat="server"  ToolTip="Home"  Visible="true"/>  
                                </td>  
                                  <td style="width:10px">
                                    
                                </td>  
                                 <td>
                                    <asp:ImageButton ID="imgLogR" ImageAlign="right" ImageUrl="~/Images/Log-OnRenewal2.gif" runat="server"  ToolTip="Log-on Renewal"  Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogRenewal.aspx"/>  
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
           <%-- <tr>
                <td style="text-align:center;">
                            
                     <div class="PageHeading" id="divMainHeading" style="width:840px;">
                          <a href="../../Wizard/Default.aspx?UserId=<%=Session("UserId")%>&Type=MSG" target="_blank"><span class="PageHeading" style="width:840px;color:Red;text-align:center;text-decoration:none ">SavvyPack&reg; Announcements</span></a>
                     </div>                                    
                </td>
            </tr>--%>
             <tr>
                <td style="text-align:left;">                            
                  <br />              
                     <div class="PageHeading" id="div3" style="width:840px;">
                       
                         <table style="width: 100%;">
                        <tr>
                            <td style="width: 60%;">
                                 Universal Manager For Business Analysis 
                            </td>
                            <td style="width: 40%; font-size: 15px;">
                                <asp:Button ID="btnSavvy" runat="server" Width="240px" Style="color: Black; font-family: Verdana;
                                    font-size: 12px;" Text='Create/Edit a SavvyPack® Project' OnClientClick="return OpenNewWindow('../../OnlineForm/ProjectManager.aspx');"
                                    ToolTip="You can use SavvyPack® Project™ to submit requests for economic and/or environmental analyses to SavvyPack® Analysts. The tool is designed so you can submit your request with as little or as much information as you wish."></asp:Button>
                            </td>
                        </tr>
                    </table> 
                    
                     </div>                                 
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">  
                  <table style="width:840px;color:Black;text-align:left;font-size:15px;font-family:Optima;margin-top:5px; ">
                   <%-- <tr>
                      <td>
                       <asp:Label ID="lbSavvyPack" runat="Server" Text="The SavvyPack® System provides several modules for economic and environmental analysis: "></asp:Label>
                      </td>
                    </tr>--%>
                    <tr>
                       <td>
                        <ul>
                       <asp:Label ID="Label2" runat="Server" Text="The SavvyPack® System provides several modules for economic and environmental analysis: "></asp:Label>
                        <li style="margin-top:5px;">Econ1 - economic analysis from the perspective of the packaging manufacturer</li>
                        <li style="margin-top:5px;">Econ2 - economic analysis from the perspective of the packaging user </li>
                        <li style="margin-top:5px;">Sustain1 - environmental impact analysis from the perspective of the packaging manufacturer </li>
                        <li style="margin-top:5px;">Sustain2 - environmental impact analysis from the perspective of the packaging user</li>
                      
                    </ul> 
                      </td>
                    </tr>
                  </table>            
                </td>
            </tr>
            <tr>
                <td style="height: 121px">              
                   <div id="dvPermission1" runat="Server" style="display:none;">
                          
                           <table width="100%">
                                <tr>
                                    <td>
                                           <table width="45%">
                                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;">
                                                       The modules you are authorized to use are shown below:
                                                    </td>
                                                </tr>
                                                 <tr class="AlterNateColor1">
                                                    <td>
                                                        <asp:DropDownList ID="ddlModule" CssClass="DropDown" Width="90%" runat="server"></asp:DropDownList>
                                                    </td>
                                                 </tr> 
                                                  
                                                 <tr class="AlterNateColor2">
                                                    <td style="text-align:justify">  
                                                        <asp:Button ID="btnSAnalysis" runat="server"  CssClass="ButtonWMarigin" Text="Start Module"/>                                    

                                                    </td>
                                                 </tr>  
                                               
                                        </table>    
                                    </td>                                  
                                </tr>
                           </table>  
                    </div>  
            
               </td>
            </tr>
         
            
           
           
        </table>
      <table class="ContentPage" id="Table3" runat="server">
        <tr>
            <td style="text-align: left;">
                <br />
                <div class="PageHeading" id="div4" style="width: 840px;">
                    Market Subscription
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="divSubscription" runat="Server" style="display: none;">
                    <table width="100%">
                        <tr>
                            <td>
                                <table width="50%">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            The Subscriptions you are authorized to use are shown below:
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:DropDownList ID="ddlSubscp" CssClass="DropDown" Width="90%" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="text-align: justify">
                                            <asp:Button ID="btnSubscr" runat="server" CssClass="ButtonWMarigin" Text="Start Subscription" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
           <table class="ContentPage" id="Table1" runat="server">
           
             <tr>
                <td style="text-align:left;">                            
                  <br />              
                     <div class="PageHeading" id="div2" style="width:840px;">
                         Universal Manager For Knowledgebases
                     </div>                                 
                </td>
            </tr>
            <tr>
                <td style="text-align:left;">  
                  <table style="width:840px;color:Black;text-align:left;font-size:15px;font-family:Optima;margin-top:5px; ">
                    <tr> 
                      <td>
                       <asp:Label ID="Label1" runat="Server" Text="SavvyPack Corporation provides searchable, interactive Knowledgebases:"></asp:Label>
                      </td>
                    </tr>                   
                  </table>            
                </td>
            </tr>
            <tr>
                <td>              
                  
                     <div id="dvPermission2" runat="Server" style="display:none;">
                          
                           <table width="100%">
                                <tr>
                                    <td>
                                           <table width="50%">
                                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;">
                                                      The Knowledgebases you are authorized to use are shown below:
                                                    </td>
                                                </tr>
                                                 <tr class="AlterNateColor1">
                                                    <td>
                                                        <asp:DropDownList ID="ddlKBase" CssClass="DropDown" Width="90%" runat="server"></asp:DropDownList>
                                                    </td>
                                                 </tr> 
                                                  
                                                 <tr class="AlterNateColor2">
                                                    <td style="text-align:justify">  
                                                        <asp:Button ID="btnSSearch" runat="server"  CssClass="ButtonWMarigin" Text="Start Search"/>                                    

                                                    </td>
                                                 </tr>  
                                               
                                        </table>    
                                    </td>                                  
                                </tr>
                           </table>                    
                    </div>  
            
               </td>
            </tr>
        </table>
          <%--Sudhanshu--%>
            <table class="ContentPage" id="Table2" runat="server">
           
             <tr>
                <td style="text-align:left;">                            
                  <br />              
                     <div class="PageHeading" id="div1" style="width:840px;">
                        <%-- Universal Manager For On-line Multi-client Studies--%>
                     </div>                                 
                </td>
            </tr>
         
            <tr>
                <td style="display:none;">              
                  
                     <div id="dvPermission3" runat="Server" style="display:none;">
                          
                           <table width="100%">
                                <tr>
                                    <td>
                                           <table width="50%">
                                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;">
                                                      The Reports you are authorized to use are shown below:
                                                    </td>
                                                </tr>
                                                 <tr class="AlterNateColor1">
                                                    <td>
                                                        <asp:DropDownList ID="ddlReports" CssClass="DropDown" Width="90%" runat="server"></asp:DropDownList>
                                                    </td>
                                                 </tr> 
                                                  
                                                 <tr class="AlterNateColor2">
                                                    <td style="text-align:justify">  
                                                        <asp:Button ID="btnSReport" runat="server"  CssClass="ButtonWMarigin" Text="Begin Online Report"/>                                    

                                                    </td>
                                                 </tr>  
                                               
                                        </table>    
                                    </td>                                  
                                </tr>
                           </table>
                           
                            
                                                        
                          
                          
                    
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
