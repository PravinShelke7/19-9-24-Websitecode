<%@ Page  Title="Sponsor Information" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="InfoOnSponsor.aspx.vb" Inherits="InteractiveServices_InfoOnSponsor"  %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<style type="text/css">


.testimonial {
    margin: 0 0 0 30px;
    padding: 15px;
    position: relative;
    font-style: italic;
  
   /* -moz-border-radius: 25px;
  /*  -webkit-border-radius: 25px;*/ 
   
   /* -khtml-border-radius: 25px; /* for old Konqueror browsers */ 
   
    border-radius: 25px; /* future proofing */   
    border: outset #CCC; 
   background-color: #FEFCA1;
   
   
}       
.down-arrow {
    width: 0;
    height: 0;
    border-left: 0px solid transparent;
    border-right: 17px solid transparent;
    border-top: 17px solid black;
    margin: 0 0 0 55px;
}
   
    .style1
    {
        height: 103px;
        
    }
   
</style>
    <script type="text/javascript">
    function roll_over(img_name, img_src) {
        document[img_name].src = img_src;
    }
    </script>


    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
       
          <table cellspacing="7">
          <tr>
          <td>
               <div style="width:100%;font-weight:normal;font-size: 16px;text-align:justify;margin-top:2px;font-family:Verdana">
                    <table width="700px" border="0" cellpadding="2px" cellspacing="0px">
                      <tr>
                            <td style="width:60%;padding-top:10px;padding-right:10px;" valign="top" colspan="2">
                                   <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 15px;">
                        Why you should sponsor </span>
                        <br />
                        
                        <table>
                        
                        <tr>

                            <td style="width:60%;font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;height:20px;
                                text-align: justify" colspan="2">
                               
                            </td>
                            </tr>
                            <tr>                           
                            
                            <td style="width:60%;font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;margin-top:0px;text-align: justify;" valign="top" colspan="2" >
                                <span>
                                      
                                      <ul> <li>
                                           <font face="Verdana"><font size="2"> This tool is an excellent new tool for the packaging industry.EVERYONE we have shown it to really likes it. </font></font>
                                       </li>
                                       <li>
                                            <font face="Verdana"><font size="2">It is important that the top industry participants support and be 
                                            seen as supporting the industry with new capabilities .</font></font>                        
                                       </li>
                                         <li>
                                           <font face="Verdana"><font size="2">Other top industry suppliers have already committed.</font></font>
                                       </li>
                                       <li>
                                           <font face="Verdana"><font size="2">The Structure Assistant is an offshoot of an existing system that SavvyPack Corporation has already made a commericial success,assuring success with Structure Assistant.</font></font>
                                       </li>
                                     <li>
                                           <font face="Verdana"><font size="2">Sponsors receive a sponsorship flag, putting them in direct contact with package engineers.</font></font>
                                       </li>
                                       
                                       <li>
                                            <font face="Verdana"><font size="2">Sponsors market their materials, putting them first in structure design. </font></font>                        
                                       </li>
                                         <li>
                                           <font face="Verdana"><font size="2">Sponsors can sponsor structures, another unique and effective marketing tool. </font></font>
                                       </li>
                                       <li>
                                           <font face="Verdana"><font size="2">Sponsors receive timely notification of activity on their flag, materials, and structures. </font></font>
                                       </li>
                                                            
                                </ul>
                            </td>  
                      </tr>
                        </table>
                        
                      </td>
                      <td style="width:40%;padding-top:10px;padding-right:10px;" valign="top" colspan="2">
                          <table><tr><td style="width:40%; padding-left:5px; "> 
                                <table cellpadding="0" cellspacing="0" style="border-collapse:collapse;">
                                    <tr>
                                       <td> 
                                       
                                            <div id="dvConsulting" runat="Server" style="width:250px;height: 40px;text-align:justify;font-size:10px;font-weight:normal;font-family:Verdana;color:Black;" class="testimonial">
                                                     Major CPG ... the Structure Assistant is a compelling value and compelling capability for package design. 
                                                     <br />
                                                 </div>
                                                 <div class="down-arrow"></div>
                                                 <br />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                    
                                        <td class="style1">
                                        <br />
                                       
                                             <div id="dvPublication" runat="Server" style="width:250px; height: 40px;margin-top:5px;text-align:justify;font-size:10px;font-weight:normal;font-family:Verdana;color:Black;" class="testimonial">
                                                     Major Converter ... this tool is a must have for the novice and professional alike.
                                                     <br />
                                                 </div>
                                                 <div class="down-arrow"></div>
                                                 <br />
                                        </td>
                                        
                                    </tr>
                                    <tr>
                                    
                                        <td>
                                        <br />
                                      
                                            <div id="dvInteractiveService" runat="Server" style="width:250px;height: 40px;margin-top:5px;text-align:justify;font-size:10px;font-weight:normal;font-family:Verdana;color:Black;" class="testimonial">
                                                    Major Supplier ... the Structure Assistant provides package structures for virtually any product I can think of. 
                                                     <br />
                                                 </div>
                                                 <div class="down-arrow"></div>
                                        </td>
                                    </tr>
       
                                </table>
                            </td> </tr> </table>
                     </td>
                      
             </tr>
         </table>
                
         </div>


         <div id="dvCompanyInfo" runat="server" style="vertical-align: top; margin-left: 7px; margin-right: 7px;padding-top: 20px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                       Please call us at 952-405-7500 or email  us at <a style="text-decoration: none; font-style: italic; font-weight: bold"
                            class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a> </span>
                    <br />
                        <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                       if you are interested in sponsoring the Structure Assistant.</span>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
         </table>
          <asp:HiddenField ID="hdnUserId" runat="Server" /> 
           <asp:HiddenField ID="hdnRepId" runat="Server" />
    </div>
                          
                    
            </td>
          </tr>
        </table>
    </div>
</asp:Content>

