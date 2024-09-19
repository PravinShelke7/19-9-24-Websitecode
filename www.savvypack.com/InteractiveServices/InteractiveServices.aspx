<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="InteractiveServices.aspx.vb" Inherits="InteractiveServices_InteractiveServices" title="Interactive Services" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/javascript" language="javascript">
   function OpenNewWindow(Page,winName) {

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
            newwin = window.open(Page, winName, params);

        }
</script>
   <div style="height:22px;width:97.5%;font-weight:bold;font-size:23px;text-align:center;margin-top:2px;margin-left:5px;color:#825f05;">
      SavvyPack<sup>&reg;</sup> Interactive Services
     </div>
   <div id="ContentPagemargin" runat="server">    
          
            <div id="error" style="height:10px;">
               <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        
            <table cellspacing="9" >
                <tr>
                   <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify" >
                            <span><font face=Verdana><font size=2>SavvyPack Corporation has developed several 
                                unique interactive services for the packaging industry. You can purchase 
                                subscriptions online or by contacting the company at 952-405-7500. If you have 
                                any questions or you would like a demonstration of any of the services, please 
                                email us at<br />
                            <a style="TEXT-DECORATION: none;font-style:italic;font-weight:bold;font-size:13px;" class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a>.  </font> </span>
				  </td>
                </tr> 
             </table>             
         
   </div> 
   <div id="Div2"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; "> 
          <table cellspacing="2" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;width:680px;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                            <span style="margin-left:5px">Structure Assistant</span>    
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                            <br /><span><font face="Verdana">
                            <font size="2">
                                an Internet-enabled software and content system for designing package structures.
                            </font> </font> </span><br />
				  </td>
                </tr>                 
                <tr>
                   <td colspan="2">
                      <br />
                      <span style="font-weight:bold ;color:Black;font-family:Verdana;font-size:13px;">Structure Assistant</span> 
                      <asp:LinkButton ID="LinkButton6" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/SAModule.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>    
          </table>   
    </div>
    <div id="Div1"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; "> 
          <table cellspacing="2" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;width:100%;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                            <span style="margin-left:5px">Packaging Analysis Service</span>    
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                           <br /> <span><font face="Verdana">
                            <font size="2">
                                an Internet-enabled software and content system for conducting economic and environmental 
                                analysis with a highly efficient and integrated set of tools.
                            </font> </font> </span><br />
				  </td>
                </tr>                 
                <tr>
                   <td colspan="2">
                      <%--<span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Sustain2:</span> --%>
                      <br />
                      <span style="font-weight:bold ;color:Black;font-family:Verdana;font-size:13px;">Core modules</span> 
                      <asp:LinkButton ID="LinkButton4" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/PackAnalysisService.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>    
          </table>   
    </div> 
    <div id="ContentPagemargin1"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; ">  
          <table cellspacing="2" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;width:100%;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                            <span style="margin-left:5px">Environmental Life Cycle Analysis (LCA) Service</span>    
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                           <br /> <span><font face="Verdana">
                            <font size="2">
                                an Internet-enabled software and content system for conducting environmental impact analysis. For more information contact us directly or register for a scheduled web conference.
                            </font> </font> </span><br /><br />
				  </td>
                </tr>  
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Sustain1:</span> 
                      <span style="font-weight:normal ;color:Black;font-family:Verdana;font-size:13px;">the package manufacturing module </span> 
                      <asp:LinkButton ID="lnkModuleOne" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info" style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/EnvModule1.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>    
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Sustain2:</span> 
                      <span style="font-weight:normal ;color:Black;font-family:Verdana;font-size:13px;">the package filling module</span> 
                      <asp:LinkButton ID="lnkModuleTwo" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/EnvModule2.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>   
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Sustain3:</span> 
                      <span style="font-weight:normal ;color:Black;font-family:Verdana;font-size:13px;">package manufacturing data mining</span> 
                      <asp:LinkButton ID="LinkButton3" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/Sustain3Module.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>   
          </table>   
    </div> 
                                           
   <div id="dvEconomicAnalysis" runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; ">  
          <table cellspacing="2" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;width:100%;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                        <span style="margin-left:5px">Economic Analysis Service </span>   
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                            <br /><span><font face="Verdana">
                            <font size="2">
                               an Internet-enabled economic analysis system designed to assist packaging professionals from many functional groups. Two modules are available: 
                            </font> </font> </span><br /><br />
				  </td>
                </tr>  
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Econ1:</span> 
                      <span style="font-weight:normal ;color:Black;font-family:Verdana;font-size:13px;">the package manufacturing module </span> 
                      <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/EcoModule1.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>    
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Econ2:</span> 
                      <span style="font-weight:normal ;color:Black;font-family:Verdana;font-size:13px;">the package filling module</span> 
                      <asp:LinkButton ID="LinkButton2" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info" style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/EcoModule2.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>    
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Econ3:</span> 
                      <span style="font-weight:normal ;color:Black;font-family:Verdana;font-size:13px;">package manufacturing data mining</span> 
                      <asp:LinkButton ID="LinkButton5" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/Econ3Module.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>  
          </table>   
    </div> 
    
   <div id="dvInteractiveService" runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; ">  
          <table cellspacing="2" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;width:100%;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                        <span style="margin-left:5px">Interactive Knowledgebase Service </span> 
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                            <br /><span><font face="Verdana">
                            <font size="2">
                              an Internet-enabled proprietary database system of searchable information on companies in the packaging industry.
                            </font> </font> </span><br /><br />
				  </td>
                </tr>  
                <tr>
                   <td colspan="2">
                      <span style="font-weight:bold;color:Black;font-family:Verdana;font-size:13px;">Contract Packaging Knowledgebase </span> 
                      
                      <asp:LinkButton ID="lnkContractPackage" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:10px;" PostBackUrl="~/InteractiveServices/InteractiveKb.aspx" ></asp:LinkButton>
                   </td>
                   
                </tr>    
               
          </table>   
    </div>
    
   <div id="dvCompanyInfo" runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; ">  
          <table cellspacing="2" >   
                <tr>
                  <td style="height:20px"></td>
                </tr>            
                <tr>
                   <td >
                      <span style="font-weight:bold;color:#825f05;font-family:Verdana;font-size:14px;">Questions?</span> 
                   </td>                   
                </tr>    
                <tr>
                   <td>
                      <span style="font-weight:normal;color:Black;font-family:Verdana;font-size:13px;">Call us at 952-405-7500 or</span> <br />
                       <span style="font-weight:normal;color:Black;font-family:Verdana;font-size:13px;">email us at <a style="TEXT-DECORATION: none;font-style:italic;font-weight:bold" class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a></span>
                   </td>                   
                </tr>   
                <tr>
                  <td style="height:10px"></td>
                </tr>
               
          </table>   
    </div>
</asp:Content>

