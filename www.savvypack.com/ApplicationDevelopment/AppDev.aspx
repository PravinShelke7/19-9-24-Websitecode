<%@ Page Title="Application Development" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="AppDev.aspx.vb" Inherits="ApplicationDevelopment_AppDev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div style="height:22px;width:97.5%;font-weight:bold;font-size:23px;text-align:center;margin-top:2px;margin-left:5px;color:#825f05;">
     Application Development
     </div>
   <div style="margin-left: 50px; font-size: 13px; text-align: justify; margin-top: 10px;
        margin-left: 8px;margin-right: 7px; color: #666666;color: Black;">
       <br /> <span style="font-family: Verdana; color: Black">
        SavvyPack Corporation personnel work with your team to create and manage packaging centric web applications.
        </span>
     </div>
    <div id="Div1"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px;">  
          <table cellspacing="2" style="width:100%;" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                            <span style="margin-left:5px">Services</span>    
           
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                            <br /><span><font size="2" face="Verdana">
                                SavvyPack Corporation provides all of the services necessary to create and maintain 
                                a web application. Services include interfacing directly with an Allied 
                                Development Consultant, software and content development, along with hosting and 
                                maintaining the application.
                            </font> <asp:LinkButton ID="LinkButton4" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:5px;" PostBackUrl="~/ApplicationDevelopment/Services.aspx" ></asp:LinkButton> </span><br /><br />
				  </td>
                </tr>                 
                <tr>
                   <td colspan="2">
                     
                   </td>
                   
                </tr>    
          </table>   
    </div> 

    <div id="Div2"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px;">  
          <table cellspacing="2" style="width:100%;" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                            <span style="margin-left:5px">Benefits   </span>        
                       </div>
                   </td>
                </tr>   
                <tr>
                    <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                           <br /> <span><font face="Verdana">
                            <font size="2">
                             <%--Save time & money by submitting a high level job request to some one who undersatnds your needs/requirments and in turn can develpoe the application without the need for you to provide detailed packaging in format.--%>There 
                                are many benefits to working with SavvyPack Corporation that occur throughout the 
                                life of the application. Some of those benefits include the ease of development, 
                                the quality of the application, and the cost of development.
                            </font> </font> 
                            <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:5px;" PostBackUrl="~/ApplicationDevelopment/Benefits.aspx" ></asp:LinkButton> </span><br /><br />
				  </td>
                </tr>                 
                <tr>
                   <td colspan="2">
                     
                   </td>
                   
                </tr>    
          </table>   
    </div>


    <div id="Div3"  runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px;">  
          <table cellspacing="2" style="width:100%;" >
                 <tr>
                   <td colspan="2">
                       <div style="background-color:#58595B;color:White;height:22px;font-weight:bold;font-size:16px;text-align:left ;margin-top:2px;">
                            <span style="margin-left:5px">Examples</span>
                             </div>
                   </td>
                </tr>   
                <tr>
                <td style="font-weight:500;color:Black ;font-family:Optima;font-size:16px;text-align:justify;" valign="top"  colspan="2" >
                            <br /><span><font face="Verdana">
                            <font size="2">
                             See examples of web applications developed by SavvyPack Corporation. Examples include applications created for SavvyPack Corporation's portfolio of applications and applications created for clients.
                            </font> </font> 
                            <asp:LinkButton ID="LinkButton2" runat="Server" CssClass="InteractiveLink" Text="more Info.." ToolTip="Click here to get more info"  style="font-size:14px;margin-left:5px;" PostBackUrl="~/ApplicationDevelopment/Examples.aspx" ></asp:LinkButton>
                            </span><br /><br />
				  </td>
                </tr>                 
                <tr>
                   <td colspan="2">
                      
                   </td>
                   
                </tr>    
          </table>   
    </div>
      <div id="dvCompanyInfo" runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; ">  
          <table cellspacing="2" >   
                <tr>
                  <td style="height:10px"></td>
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

