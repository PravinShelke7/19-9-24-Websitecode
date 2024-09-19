<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="Econ3Module.aspx.vb" Inherits="InteractiveServices_Econ3Module" title="Economic Analysis - Econ3" %>

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
           var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;

           if (userId != "") {
               msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
               alert(msg);
               return false;
           }
           else {
               if (type == "L") {
                   ShowPopWindow("../Users_Login/LoginS.aspx");
               }
               return true;
           }
       }
       function ConfirmMessage(typebtn) { 

        var userId= document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; //"<%=Session("UserId")%>";  
        //alert(userId);
        var message;

        if (typebtn == "OS") {

            message = "order";
        }

       

        if (userId != "") {
            return true;
        }
 
        else {
            msg = "--------------------------------------------------------------------------\n To " + message + ", you must first login.\n If you do not have an account, please create an account then login.\n--------------------------------------------------------------------------\n";
            alert(msg);
            return false;
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
                        Econ3 - the package manufacturing module</span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">
                        The SavvyPack<sup>&reg;</sup> System is a subscription based online software and content 
                        system for packaging. The system provides the capability to conduct economic 
                        and environmental analysis of ALL packaging types. Pre-researched data is available 
                        in the SavvyPack® database, which saves users valuable time and money. 
                       </font></font>
                    </span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">
                         ECON3 interfaces simultaneously with many ECON1 cases. 
                         You can compare assumptions, compare results, create graphics, calculate rate of return, 
                         and many others.
                        </font></font>
                    </span>
                </td>
            </tr>
          
            <tr>
                <td style="height: 10px">
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
            <%--<tr>
                <td>
               
                <asp:Image ID="imgBack" runat="Server" ImageUrl="~/Images/ArrowRight.jpg"/>
                 <asp:LinkButton ID="lnkBack" runat="Server" Text="Back to Interactive List" CssClass="InteractiveLink" style="font-size:14px" PostBackUrl="~/InteractiveServices/InteractiveServices.aspx"></asp:LinkButton>
                
                
                </td>
              </tr>--%>
        </table>
          <asp:HiddenField ID="hdnUserId" runat="Server" /> 
    </div>
</asp:Content>

