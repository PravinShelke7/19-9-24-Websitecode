<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false"
    CodeFile="PackAnalysisService.aspx.vb" Title="Packaging Analysis Service" Inherits="InteractiveServices_PackAnalysisService" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
     function roll_over(img_name, img_src)
       {
             document[img_name].src = img_src;
       }
        function ShowPopWindow(Page) {
           //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
           var width = 550;
           var height = 200;
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
           if (newwin == null || typeof (newwin) == "undefined") {
               alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
           }

           return false;

       }
        function AlertMessage(type) 
        {
           //var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value; 

           if (userId != "") {
               msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
               alert(msg);
               return false;
           }
           else {
               if (type == "L") 
               {
                   ShowPopWindow("../Users_Login/LoginS.aspx");
                    return false ;
               }
              
           }
       }
       function ConfirmMessage(typebtn) { 

        //var userId= document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; 
         var userId = document.getElementById('<%=hdnUserId.ClientID%>').value; 
         
       
        var message;

        if (typebtn == "OS") {

            message = "order";
        }

       

        if (userId != "") 
        {
             OpenOrderPage();
             return false;
        }
 
        else {
            msg = "--------------------------------------------------------------------------\n To " + message + ", you must first login.\n If you do not have an account, please create an account then login.\n--------------------------------------------------------------------------\n";
            alert(msg);
            return false;
        }

    }
    
     function OpenOrderPage()
   {
     var reportId=document.getElementById('<%=hdnRepId.ClientID%>').value;
     OpenNewWindow('../ShoppingCart/Order.aspx?Id=' + reportId ,'PACKGAN');
    
     return false;   
   }
   function OpenNewWindow(Page,PageName) {
       
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
           newwin = window.open(Page, PageName, params);
           //newwin.document.write('<html><head><title>window</title></head</HTML>');

           if (newwin == null || typeof (newwin) == "undefined") {
               alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
           }
       }

    </script>

    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 2px; margin-left: 5px; color: #825f05;">
        <i>SavvyPack</i><sup>&reg;</sup> Packaging Analysis System <span style="font-size: 13px;
            color: Black">(patent pending)</span>
    </div>
    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <div id="ContentPagemargin1" runat="server" style="vertical-align: top; margin-left: 7px;margin-top:5px;
            margin-right: 7px;">
            <table cellspacing="2">
                <tr>
                    <td colspan="2">
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                        text-align: justify;" valign="top" colspan="2">
                        <span>
                            <font face="Verdana"><font size="2">The <i>SavvyPack</i><sup>&reg;</sup>
                                Packaging Analysis System is the world's most sophisticated system for economic and environmental
                                analysis. The system includes:</font></font>
                        </span><br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Software:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">a true analytical tool specifically designed for packaging </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Content:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">detailed content specific to packaging developed for primary research
                                conducted since 1996. </span>
                    </td>
                </tr>
                
                <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Support:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">
                                 very knowledgeable packaging analysts are always available to provide support for the
                                 software AND CONTENT.
                                </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                       <span style="font-weight: normal; color: Black; font-family: Verdana;font-size: 13px;">
                        Core Modules:        
                        </span>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Econ1:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">the package manufacturing module for Economic Analysis </span>
                        <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                            ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 10px;"
                            PostBackUrl="~/InteractiveServices/EcoModule1.aspx"></asp:LinkButton>
                    </td>
                </tr>
                 <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Sustain1:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">the package manufacturing module for Environmental Analysis </span>
                        <asp:LinkButton ID="lnkModuleOne" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                            ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 10px;"
                            PostBackUrl="~/InteractiveServices/EnvModule1.aspx"></asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Econ2:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">the package filling module for Economic Analysis</span>
                        <asp:LinkButton ID="LinkButton2" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                            ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 10px;"
                            PostBackUrl="~/InteractiveServices/EcoModule2.aspx"></asp:LinkButton>
                    </td>
                </tr>
               
                <tr>
                    <td colspan="2">
                        <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 13px;">
                            Sustain2:</span> <span style="font-weight: normal; color: Black; font-family: Verdana;
                                font-size: 13px;">the package filling module for Environmental Analysis</span>
                        <asp:LinkButton ID="lnkModuleTwo" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                            ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 10px;"
                            PostBackUrl="~/InteractiveServices/EnvModule2.aspx"></asp:LinkButton>
                    </td>
                </tr>
                <tr style="height: 5px;">
                    <td colspan="2">
                    </td>
                </tr>
                <%--<tr>
                    <td style="color: Black; font-family: Verdana; font-size: 12px; text-align: justify"
                        colspan="2">
                        To order, you must first <a onclick="return AlertMessage('L');"
                            id="lnkCreate" style="font-style: normal; font-family: Verdana; font-size: 12px;"
                            class="Link" href="#">login</a>.<br />
                        If you do not have an account, please <a onclick="return AlertMessage();" style="font-style: normal;
                            font-family: Verdana; font-size: 12px;" target="_blank" class="Link" href="../Users_Login/AddEditUser.aspx?Mode=Add">
                            create an account</a> then <a onclick="return AlertMessage('L');" style="font-style: normal;
                                font-family: Verdana; font-size: 12px;" class="Link" href="#">login</a>.
                    </td>
                </tr>--%>
                <tr>
                   <td align="left" >
                   <br />
                    <asp:Label ID="lblfeatures" runat="Server" Text="Features" style="font-size:18px;color:#ff7b3c;margin-left:5px;font-weight:bold "></asp:Label>
                     <ul style="font-size:10.2pt;color:#000000;margin-top:5px;font-family:Optima;">                      
                        <li style="margin-top:0px;"><span style="font-weight:bold">Publication date:</span> <asp:Label ID="lblPDate"  runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Subscription number:</span> <asp:Label ID="lblSNo" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Copyright:</span> <asp:Label ID="lblCopyR" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Annual Subscription: </span> <asp:Label ID="lblPrimumSub1" runat="Server"></asp:Label></li>
                        </ul>  
                   </td>  
                        <td align="center"  style="vertical-align:middle ">     
                        <table>
                            <tr>
                                <td>                                
                                   <%--<asp:LinkButton ID="lnkOrder" runat="Server" onmouseover="roll_over('order_study_1', '../Images/Order2.gif')"  onmouseout="roll_over('order_study_1', '../Images/Order.gif')" ToolTip="Order Module One">
                                     <img id="Img1" src="../Images/Order.gif"  name="order_study_1" height="35" width="100" border="0" runat="Server"  />
                                   </asp:LinkButton>--%> 
                                    <asp:ImageButton ImageUrl="~/Images/OrderButton.jpg" ID="imgbtnOrder" Visible="false"  runat="Server" ToolTip="Order Packaging" OnClientClick="return ConfirmMessage('OS');">
                                    </asp:ImageButton>

                                </td>
                            </tr>
                                             
                           
                    </table>                 
                   </td>                 
                </tr>
            </table>
        </div>
    </div>
    <div id="dvCompanyInfo" runat="server" style="vertical-align: top; margin-left: 7px;margin-top:30px;
        margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <span style="font-weight: bold; color: #825f05; font-family: Verdana; font-size: 14px;">
                        Questions?</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        Call us at 952-405-7500 or</span>
                    <br />
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        email us at <a style="text-decoration: none; font-style: italic; font-weight: bold"
                            class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a></span>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <%--<tr>
                <td>
               
                <asp:Image ID="imgBack" runat="Server" ImageUrl="~/Images/ArrowRight.jpg"/>
                 <asp:LinkButton ID="lnkBack" runat="Server" Text="Back to Interactive List" CssClass="InteractiveLink" style="font-size:14px" PostBackUrl="~/InteractiveServices/InteractiveServices.aspx"></asp:LinkButton>
                
                
                </td>
              </tr>--%>

        </table>
         <asp:HiddenField ID="hdnUserId" runat="Server" /> 
           <asp:HiddenField ID="hdnRepId" runat="Server" />
    </div>
</asp:Content>
