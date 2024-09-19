<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Videos.aspx.vb" Inherits="Videos" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SavvyPack Videos</title>
    <link href="App_Themes/SkinFile/Allied.css" rel="stylesheet" type="text/css" />
  
    <style type="text/css">
        .style1
        {
            font-weight: normal;
        }
        .style2
        {
            width: 24px;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function ShowPopWindow(Page) {
                //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
                var width = 800;
                var height = 700;
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

            }
    </script>
  
</head>
<body>
    <form runat="server">
     <center>
     <div style="height:15px;"></div>
        <div style="background-color: #F1F1F2;width:900px;">
        <table>
            <tr style="height: 110px; width: 800px; vertical-align: middle;margin-right:0px;">
                <td class="tdmlg5" align="center" colspan="1" style="width:500px;text-align:right;">
                </td>
            </tr>
        </table>
       
        <table id="tblVidepList" width="90%" >
         <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                   <br />
                    <span>
                        <font face="optima"><font size="4">
                          We would like to introduce our company and products to you with these short, two minute videos. 
                        </font></font>
                   </span>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
              <td style="height:30px;" colspan="2"></td>
            </tr>
            <tr>
              <td colspan="2">
                  <table style="color:Black;width:100%;font-size:Optima;font-size:15px;text-align:left ">
                      <tr>
                         <td style="font-weight:bold;" colspan="2">Company Introduction</td>
                         <td style="font-family:Optima;width:50%">"SavvyPack Corporation at a Glance" </td>
                         <td style="font-family:Optima;width:5%" align="left">
                          <asp:LinkButton ID="lnkVidM1" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                             <%--<a id="A2" class="LinkF" target="_blank" href="~/Video/CompanyIntroM.htm" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()"
                        runat="server">mp4</a>--%>
                         </td>
                         <td style="font-family:Optima;width:5%" align="left" >
                         <asp:LinkButton ID="lnkVidF1" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                             <%--<a id="A1" class="LinkF" target="_blank" href="~/Video/CompanyIntroF.htm" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()"
                        runat="server">flash</a>--%>
                        </td>
                         
                      </tr>
                      <tr>
                         <td style="font-weight:bold;" colspan="6">&nbsp;</td>
                         
                         
                      </tr>
                      <tr>
                          <td style="font-weight: bold;" colspan="2">
                              SavvyPack<span class="style1"><font size="2" face="Verdana"><sup>®</sup></font></span>
                              Analysis Service
                          </td>
                          <td style="font-family: Optima;">
                              &quot;Savvy<span><font size="2" face="Verdana">Pack<sup>®</sup></font></span> Analysis Service
                              at a Glance"
                          </td>
                          <td style="font-family: Optima;" align="left">
                           <asp:LinkButton ID="lnkVidM2" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                             <%-- <a id="A4" class="LinkF" target="_blank" href="~/Video/CompanyIntroM.htm" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')"
                                  onmouseout="UnTip()" runat="server">mp4</a>--%>
                          </td>
                          <td style="font-family: Optima;" align="left">
                             <%-- <a id="A3" class="LinkF" target="_blank" href="~/Video/CompanyIntroF.htm" onmouseover="Tip('plays on Adobe Flash player')"
                                  onmouseout="UnTip()" runat="server">flash</a>--%>
                                  <asp:LinkButton ID="lnkVidF2" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                          </td>
                      </tr>
                      <tr>
                         <td style="font-weight:bold;" class="style2">&nbsp;</td>
                         <td style="font-weight:bold;">Procurement</td>
                         <td style="font-family:Optima;"> 	&quot;Benefits for Procurement Professionals&quot;</td>
                         <td style="font-family:Optima;" align="left">
                             <asp:LinkButton ID="lnkVidM3" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                         </td>
                         <td style="font-family:Optima;" align="left" >
                          <asp:LinkButton ID="lnkVidF3" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                         </td>
                         
                      </tr>
                      <tr>
                         <td style="font-weight:bold;" class="style2">&nbsp;</td>
                         <td style="font-weight:bold;">Sales and Marketing</td>
                         <td style="font-family:Optima;"> 	&quot;Benefits for Sales and Marketing Professionals&quot;</td>
                         <td style="font-family:Optima;" align="left">
                             <asp:LinkButton ID="lnkVidM4" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>    
                         </td>
                         <td style="font-family:Optima;" align="left" >
                           <asp:LinkButton ID="lnkVidF4" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                         </td>
                         
                      </tr>
                        <tr>
                         <td style="font-weight:bold;height:10px;" colspan="6">&nbsp;</td>
                         
                         
                      </tr>
                        <tr>
                          
                          <td style="font-weight: bold;" colspan="2">
                              SavvyPack<span class="style1"><font size="2" face="Verdana"><sup>®</sup></font></span>
                              Investment Service
                          </td>
                          <td style="font-family: Optima;">
                              &quot;Savvy<span><font size="2" face="Verdana">Pack<sup>®</sup></font></span> Investment Service
                              at a Glance"
                          </td>
                          <td style="font-family: Optima;" align="left">
                           <asp:LinkButton ID="lnkVidM5" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                         </td>
                          <td style="font-family: Optima;" align="left">
                           <asp:LinkButton ID="lnkVidF5" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                          </td>
                      </tr>
                      
                       <tr>
                       <td style="font-weight:bold;" class="style2">&nbsp;</td>
                          <td style="font-weight: bold;">
                              Operations
                          </td>
                          <td style="font-family: Optima;">
                              "Benefits for Operations Professionals"
                          </td>
                          <td style="font-family: Optima;" align="left">
                           <asp:LinkButton ID="lnkVidM6" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                         </td>
                          <td style="font-family: Optima;" align="left">
                           <asp:LinkButton ID="lnkVidF6" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                          </td>
                      </tr>

                      <tr>
                         <td style="height:10px;" colspan="6">&nbsp;</td>
                         
                         
                      </tr>


                      <tr>
                         <td style="font-weight:bold;" colspan="2">SavvyPack Knowledgebase</td>
                         <td style="font-family:Optima;width:50%">"Contract Packager Knowledgebase at a Glance" </td>
                         <td style="font-family:Optima;width:5%" align="left">
                          <asp:LinkButton ID="lnkVidConM1" Text="mp4" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Apple QuickTime player and Windows Media Player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                             
                         </td>
                         <td style="font-family:Optima;width:5%" align="left" >
                         <asp:LinkButton ID="lnkVidConF1" Text="flash" runat="server"  CssClass="LinkF" onmouseover="Tip('plays on Adobe Flash player')" onmouseout="UnTip()">
                          </asp:LinkButton>
                            
                        </td>
                         
                      </tr>

                  </table>
              </td>
            </tr>
            <tr>
              <td style="height:20px;"></td>
              <td style="height:20px;"></td>
            </tr>
             <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                   <br />
                    <span>
                        <font face="Verdana"><font size="2">
                          Please 
                            <a href="mailto:sales@savvypack.com?Subject=Request from SavvyPack Video page" class="LinkStudy" style="font-size: 14px">
                         contact us</a>
                             for a complimentary demonstration of the benefits of our SavvyPack<sup>®</sup>  Services.
                        </font></font>
                       
                   </span>
                    <br />
                    <br />
                </td>
            </tr>
            
             <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <a href="mailto:sales@savvypack.com" class="LinkStudy" >sales@savvypack.com</a>&nbsp;&nbsp;&nbsp; 
                    [1] 952-405-7500</td>
            </tr>
            
             <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    &nbsp;</td>
            </tr>
            
        </table>
    </div>
     </center> 
   
    </form>
      <script type="text/JavaScript" src="JavaScripts/wz_tooltip.js"></script>
</body>
</html>
