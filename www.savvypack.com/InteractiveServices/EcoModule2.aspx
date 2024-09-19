<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false"
    CodeFile="EcoModule2.aspx.vb" Inherits="InteractiveServices_EcoModule2" Title="Economic Analysis - Econ2" %>
 
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
          // var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
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
                   return false;
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
     OpenNewWindow('../ShoppingCart/Order.aspx?Id=' + reportId ,'ECON2');
    
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
        <i>SavvyPack</i><sup>&reg;</sup> Economic Analysis System <span style="font-size: 13px;
            color: Black">(patent pending)</span>
    </div>
    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table cellspacing="7">
            <tr>
                <td colspan="2">
                    <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 15px;">
                        Econ2 - the package filling module</span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">The packaging filling module is a combination of
                            software and content delivered to you through the Internet. It provides a broad
                            array of economic analysis capabilities, which can be used to analyze the economics
                            of the package filling industry. </font></font>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">To learn about the system, we recommend you request
                            a demonstration. Please call us at 952-405-7500 or email us at <a style="text-decoration: none;
                                font-style: italic; font-weight: normal" class="Link" href="mailto:sales@savvypack.com">
                                sales@savvypack.com</a>. We welcome the opportunity to conduct a 30 minute
                            web conference for you, which will provide a full understanding of what the system
                            does and how it will benefit you. </font></font>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">
                          Annual subscriptions to the SavvyPack<sup>&reg;</sup> system are of greatest value 
                            when you purchase the Total System, however, you can also purchase Individual Modules.  
                            The chart below compares features of the Total System and Individual Modules.
                        </font></font>
                    </span>
                </td>
            </tr>
             <tr>
                <td align="center" colspan="2">

                        <table style="border-collapse:collapse;border-color:Black;font-family:Verdana;color:Black;width:520px;text-align:left" border="1">
                        <tr style="height:20px;font-size:16px;font-family:Arial">
                            <td class="TdHeading">
                                <b>Features</b>
                            </td>
                            <td class="TdHeading"  style="width:150px;">
                                <b>Total System</b>
                            </td>
                            <td class="TdHeading"  style="width:150px;">
                                <b>Individual Modules</b>
                            </td>
                        </tr>
                        <tr>
                            <td class="TdHeading"><b>Work Scope</b></td>
                            <td class="TdHeading"></td>
                            <td class="TdHeading"></td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>All System Content</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                         <tr class="AlterNateColor2">
                            <td>Complete Value Chain Analysis</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>Complete Life Cycle Analysis</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                         <tr class="AlterNateColor2">
                            <td>Greatest Time Efficiency</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                          <tr class="AlterNateColor1">
                            <td>License Sharing</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                         <tr class="AlterNateColor2">
                            <td>Content Support Included</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                           <tr class="AlterNateColor1">
                            <td>Technical Support Included</td>
                            <td align="center">X</td>
                            <td align="center">X</td>
                        </tr>
                           <tr>
                            <td class="TdHeading"><b>Functions</b></td>
                            <td class="TdHeading"></td>
                            <td class="TdHeading"></td>
                        </tr>
                            <tr class="AlterNateColor2">
                            <td>Adjust Case Assumptions</td>
                            <td align="center">X</td>
                            <td align="center">X</td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>Case Connections Modules</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                         <tr class="AlterNateColor2">
                            <td>Create New Cases</td>
                            <td align="center">X</td>
                             <td align="center">X</td>
                        </tr>
                           <tr class="AlterNateColor1">
                            <td>Copy Cases</td>
                            <td align="center">X</td>
                             <td align="center">X</td>
                        </tr>
                           <tr class="AlterNateColor2">
                            <td>Share Cases</td>
                            <td align="center">X</td>
                             <td align="center">X</td>
                        </tr>
                           <tr class="AlterNateColor1">
                            <td>Synchronize Modules</td>
                            <td align="center">X</td>
                            <td></td>
                        </tr>
                           <tr class="AlterNateColor2">
                            <td>Print Results</td>
                            <td align="center">X</td>
                            <td align="center">X</td>
                        </tr>
                           <tr class="AlterNateColor1">
                            <td>Perform "What ifs"</td>
                            <td align="center">X</td>
                             <td align="center">X</td>
                        </tr>
                        
                           <tr>                        
                            <td class="TdHeading"><b>Graphic Interface</b></td>
                            <td class="TdHeading"></td>
                            <td class="TdHeading"></td>
                        </tr>
                            <tr class="AlterNateColor2">
                            <td>View Assumptions</td>
                            <td align="center">X</td>
                            <td align="center">X</td>
                        </tr>
                            <tr class="AlterNateColor1">
                            <td>View Results</td>
                            <td align="center">X</td>
                             <td align="center">X</td>
                        </tr>
                            <tr class="AlterNateColor2">
                            <td>View Charts</td>
                            <td align="center">X</td>
                            <td align="center">X</td>
                        </tr>
                            <tr class="AlterNateColor1">
                            <td>View Comparisons</td>
                            <td align="center">X</td>
                            <td align="center">X</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 14px;">
                        Benefits for specific functions:</span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">You can also use the following links to obtain documents
                            that explain the features and benefits of the system. </font></font>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 14px;
                    text-align: justify" colspan="2"><br />
                    <ol>
                        <li style="margin-top: 3px;"><a style="text-decoration: none; font-style: italic;
                            font-weight: bold; font-size: 14px;" class="Link" href="../EconMarketing/SavvyPack_Designers.pdf"
                            target="_blank">Design and development professionals</a> </li>
                        <li style="margin-top: 3px;"><a style="text-decoration: none; font-style: italic;
                            font-weight: bold; font-size: 14px;" class="Link" href="../EconMarketing/SavvyPack_Operations.pdf"
                            target="_blank">Operations professionals</a> </li>
                        <li style="margin-top: 3px;"><a style="text-decoration: none; font-style: italic;
                            font-weight: bold; font-size: 14px;" class="Link" href="../EconMarketing/SavvyPack_Procurement.pdf"
                            target="_blank">Purchasing and procurement professionals</a> </li>
                        <li style="margin-top: 3px;"><a style="text-decoration: none; font-style: italic;
                            font-weight: bold; font-size: 14px;" class="Link" href="../EconMarketing/SavvyPack_SalesMarketing.pdf"
                            target="_blank">Sales and Marketing professionals</a> </li>
                    </ol>
                </td>
            </tr>
             <tr style="height: 5px;">
                <td colspan="2">
                </td>
            </tr>
           <%-- <tr>
                <td style="color: Black; font-family: Verdana; font-size: 12px; text-align: justify"
                    colspan="2">
                    To order, you must first <a onclick="return AlertMessage('L');" id="lnkCreate"
                        style="font-style: normal; font-family: Verdana; font-size: 12px;" class="Link"
                        href="#">login</a>.<br />
                    If you do not have an account, please <a onclick="return AlertMessage();" style="font-style: normal;
                        font-family: Verdana; font-size: 12px;" target="_blank" class="Link" href="../Users_Login/AddEditUser.aspx?Mode=Add">
                        create an account</a> then <a onclick="return AlertMessage('L');" style="font-style: normal;
                            font-family: Verdana; font-size: 12px;" class="Link" href="#">login</a>.
                </td>
            </tr>--%>
            <tr>
                <td align="left">
                    <asp:Label ID="lblfeatures" runat="Server" Text="Features" Style="font-size: 18px;
                        color: #ff7b3c; margin-left: 5px; font-weight: bold"></asp:Label>
                    <ul style="font-size: 10.2pt; color: #000000; margin-top:5px; font-family: Optima;">
                        <li style="margin-top: 0px;"><span style="font-weight: bold">Publication date:</span>
                            <asp:Label ID="lblPDate" runat="Server"></asp:Label></li><li style="margin-top: 3px;">
                                <span style="font-weight: bold">Subscription number:</span>
                                <asp:Label ID="lblSNo" runat="Server"></asp:Label></li><li style="margin-top: 3px;">
                                    <span style="font-weight: bold">Copyright:</span>
                                    <asp:Label ID="lblCopyR" runat="Server"></asp:Label></li><li style="margin-top: 3px;">
                                        <span style="font-weight: bold">Annual Subscription: </span>
                                        <asp:Label ID="lblS1" runat="Server"></asp:Label></li> </ul>
                </td>
                <td align="center" style="vertical-align: middle">
                    <table>
                        <tr>
                            <td>
                                <%--  <asp:LinkButton ID="lnkOrder" runat="Server" onmouseover="roll_over('order_study_1', '../Images/Order2.gif')"  onmouseout="roll_over('order_study_1', '../Images/Order.gif')" ToolTip="Order Module Two">
                                     <img id="Img1" src="../Images/Order.gif"  name="order_study_1" height="35" width="100" border="0" runat="Server"  />
                                   </asp:LinkButton>--%>
                                <asp:ImageButton ImageUrl="~/Images/OrderButton.jpg" ID="imgbtnOrder" Visible="false"  runat="Server"  OnClientClick="return ConfirmMessage('OS');"
                                    ToolTip="Order Econ2"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <%-- <div id="dvBenefits" runat="server" style="vertical-align:top;margin-left:7px;margin-right:7px; ">  
      <table cellspacing="2">
         
      </table>
   </div>--%>
    <div id="dvCompanyInfo" runat="server" style="vertical-align: top; margin-left: 7px;
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
            <%-- <tr>
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
