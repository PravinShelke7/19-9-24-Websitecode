<%@ Page Title="WebConference Home" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="WebConfHome.aspx.vb" Inherits="WebConferenceN_WebConfHome" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="MnContentPage"> 
        <div class="PageHeading" id="divMainHeading" runat="server"  title="Web Conferences Home" style="width:500px;text-align:center;margin-left:65px;">
             Web Conferences Home
            <br />
        </div>
</div>
 <div id="ContentPagemargin1"  runat="server" style="vertical-align:top;margin-left:15px;margin-right:7px; ">  
       <table cellspacing="0" cellpadding="5" >
          <tr>
            <td style="height:12px"></td>
          </tr>
                <tr>
                   <td style="font-weight:500;color:Black ;font-family:verdana;font-size:12px;text-align:justify" >
                           
                                <span style="font-weight: normal;font-size: small"">Allied Development utilizes web conferencing 
                                extensively; for demonstrating its products and services, providing SavvyPack® 
                                System software and content support, assisting with sales activities, 
                                facilitating client communications, and many more. Web conferencing is an 
                                important communication tool between you and Allied Development personnel. It is 
                                very effective and saves time and money!
                            </span>
                           
   				  </td>
                </tr> 
                <tr>
                       <td style="font-weight:500;color:Black ;font-family:verdana;font-size:12px;text-align:justify" >
                               <span style="font-weight: normal;font-size: small">
                                    For example, Allied Development presents regularly scheduled web conferences on 
                                    a variety of topics. The subject of these demonstrations can be Allied 
                                    Development products or other topics of interest in the packaging world. We feel 
                                    strongly that examining topics in this way makes it easier to visualize the value 
                                    for your company. These demonstrations are open to the public. To begin the process of registering 
                                    for our next public web conference, click 
                                    <asp:LinkButton ID="lnkJoinConf" runat="Server" Text="Register for a Conference" ToolTip="Click here to Register for a Conference" CssClass="LinkSubMenuClick" style="font-size:12px;color:Black;font-weight:normal;font-style:italic;" PostBackUrl="~/WebConferenceN/WebConf.aspx" ></asp:LinkButton>
                                    .                      
                                    <br /><br />
                                    Allied Development also utilizes private web demonstrations extensively; for the purposes identified earlier including product demonstrations, technical support, and others.  Private demonstrations are typically scheduled specifically for one company and its employees.  A private demonstration is interactive, allowing for questions and lively interaction.  You can join a private web conference in this section of the web site, 
                                    utilizing instructions from your Allied Development representative. 
                                   </span>   
                                 
                        </td> 
                     </tr>   
      </table> 
                
    </div> 
                                           
 
</asp:Content>


