<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="InteractiveKb.aspx.vb" Inherits="InteractiveServices_InteractiveKb"
    Title="Contract Packaging Knowledgebase" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
//        function ShowPopWindow(Page) {
//            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
//            var width = 550;
//            var height = 200;
//            var left = (screen.width - width) / 2;
//            var top = (screen.height - height) / 2;
//            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
//            params += ', location=no';
//            params += ', menubar=no';
//            params += ', resizable=yes';
//            params += ', scrollbars=yes';
//            params += ', status=yes';
//            params += ', toolbar=no';
//            newwin = window.open(Page, 'Chat', params);
//            if (newwin == null || typeof (newwin) == "undefined") {
//                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
//            }

//            return false;

//        }
//        function AlertMessage(type) {
//            //var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
//            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

//            if (userId != "") {
//                msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
//                alert(msg);
//                return false;
//            }
//            else {
//                if (type == "L") {
//                    ShowPopWindow("../Users_Login/LoginS.aspx");
//                    return false;
//                }

//            }
//        }
//        function ConfirmMessage(typebtn) {

//            // var userId= document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; 
//            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

//            var message;

//            if (typebtn == "OS") {

//                message = "order";
//            }



//            if (userId != "") {
//                OpenOrderPage();
//                return false;
//            }

//            else {
//                msg = "--------------------------------------------------------------------------\n To " + message + ", you must first login.\n If you do not have an account, please create an account using Login link.\n--------------------------------------------------------------------------\n";
//                alert(msg);
//                return false;
//            }

        //        }

        function ConfirmMessage(typebtn) {

            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;
            if (userId != "") {
                OpenOrderPage();
                return false;
            }
            else {
                OpenLoginPopup('OP');
                return false;
            }

        }

        function OpenOrderPage() {
            var reportId = document.getElementById('<%=hdnRepId.ClientID%>').value;
            OpenNewWindow('../ShoppingCart/Order.aspx?Id=' + reportId, 'KNWBASE');

            return false;
        }
        function OpenNewWindow(Page, PageName) {

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
        function OpenBrochure() {
            var reportId = document.getElementById('<%=hdnRepId.ClientID%>').value;
            OpenNewWindow('../Knowledgebase/ConPkgBrochure.pdf', 'BRCPAGEKW');
            return false;
        }

    </script>
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 4px; margin-left: 5px; color: #825f05;">
        Contract Packager Knowledgebase
    </div>
    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table cellspacing="7">
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span><font face="Verdana"><font size="2">The contract packager knowledgebase is the
                        only global database of contract packagers that includes actionable intelligence.
                    </font></font></span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span><font face="Verdana"><font size="2">Many of the companies in this database have
                        low profiles and can be difficult to locate even though they are key cogs in the
                        supply chain. This knowledgebase is a vital tool for locating a particular food
                        product manufacturer or a contract packager who fits your specific requirements.
                        This knowledgebase provides searchable information on companies that provide contract
                        manufacturing and/or contract packaging of food. </font></font></span>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td style="width: 60%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                    margin-top: 0px; text-align: justify;" valign="top" colspan="2">
                    <span><font face="Verdana"><font size="2">The contract packager knowledgebase isn't
                        just a list of names. It is an interactive tool that makes it possible for you to
                        search for a contract packager by : </font></font></span>
                    <ul>
                        <li><font face="Verdana"><font size="2">Product capabilities.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Process equipment.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Development capabilities.</font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Quality control.</font></font> </li>
                        <li><font face="Verdana"><font size="2">and many others.</font></font> </li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td style="width: 126%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                    margin-top: 0px; text-align: justify;" valign="top">
                    <br />
                    <span><font face="Verdana"><font size="2">Who benefits from this knowledgebase: </font>
                    </font></span>
                    <ul>
                        <li><font face="Verdana"><font size="2">Procurement Personnel.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Packaging and Processing Equipment Manufacturers.</font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Food Manufacturers.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Organic and Natural Food Marketers.</font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Supply Chain Participants. </font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Packaging Suppliers. </font></font></li>
                        <li><font face="Verdana"><font size="2">Analysts, Investors and Consultants. </font>
                        </font></li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td style="color: Black; font-family: Verdana; font-size: 12px; text-align: justify"
                    colspan="2">
                    <br />
                    To order, you must first login.<%-- <a onclick="return AlertMessage('L');"
                            id="lnkCreate" style="font-style: normal; font-family: Verdana; font-size: 12px;"
                            class="Link" href="#">login</a>.--%><br />
                    If you do not have an account, please create an account using Login link.<%-- <a onclick="return AlertMessage();" style="font-style: normal;
                            font-family: Verdana; font-size: 12px;" target="_blank" class="Link" href="../Users_Login/AddEditAccount.aspx">
                            create an account</a> then <a onclick="return AlertMessage('L');" style="font-style: normal;
                                font-family: Verdana; font-size: 12px;" class="Link" href="#">login</a>.--%>
                </td>
            </tr>
            <tr>
                <td align="left" style="width: 126%">
                    <asp:Label ID="lblfeatures" runat="Server" Text="Prices" Style="font-size: 18px;
                        color: #ff7b3c; margin-left: 5px; font-weight: bold"></asp:Label>
                    <ul style="font-size: 10.2pt; color: #000000; margin-top: 5px; font-family: Optima;">
                        <li style="margin-top: 0px;"><span style="font-weight: bold">Original Publication date:</span>
                            <asp:Label ID="lblPDate" runat="Server"></asp:Label>
                        </li>
                        <li style="margin-top: 3px;"><span style="font-weight: bold">Knowledgebase number:</span>
                            <asp:Label ID="lblKNo" runat="Server"></asp:Label>
                        </li>
                        <li style="margin-top: 3px;"><span style="font-weight: bold">Copyright:</span>
                            <asp:Label ID="lblCopyR" runat="Server"></asp:Label>
                        </li>
                        <li style="margin-top: 3px;"><span style="font-weight: bold">Annual Subscription - 3
                            users:</span>
                            <asp:Label ID="lblAnualSub" runat="Server"></asp:Label>
                        </li>
                    </ul>
                </td>
                <td align="center" style="vertical-align: middle">
                    <table width="150px">
                        <tr>
                            <td>
                                <asp:Label ID="lblCall" Text="Please call to order." Style="color: Red; font-size: 15px;
                                    font-weight: bold; display: none;" runat="Server"></asp:Label>
                                <asp:ImageButton ImageUrl="~/Images/OrderButton.jpg" ID="imgbtnOrder" runat="Server"
                                    ToolTip="Order Module" OnClientClick="return ConfirmMessage('OS');"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <%--  <asp:LinkButton ID="lnkPdf" runat="Server"  ToolTip="Environmental Analysis Brochure">
                                    <img id="Img2" src="../Images/download_brochure-1.jpg"  height="30" width="120" align="middle" border="0" runat="Server"  />
                                   </asp:LinkButton>--%>
                                <asp:ImageButton ImageUrl="~/Images/ViewBrochure.gif" ID="imgbtnVwBrch" Visible="false"
                                    runat="Server" OnClientClick="return OpenBrochure();" ToolTip="Knowledgebase Brochure">
                                </asp:ImageButton>
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
        margin-right: 7px; text-align: left;">
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
                        Call us at [1] 952-405-7500 or</span>
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
        <asp:Button ID="btnOrder" runat="server" Style="visibility: hidden;"></asp:Button>
    </div>
</asp:Content>
