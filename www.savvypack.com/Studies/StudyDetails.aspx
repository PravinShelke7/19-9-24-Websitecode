<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="StudyDetails.aspx.vb" Inherits="Studies_StudyDetails" Title="Publication - Report Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/JavaScript" src="../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../JavaScripts/tip_balloon.js"></script>
    <div id="MnContentPage" runat="server" class="MnContentPage">
        <script type="text/javascript">
            function roll_over(img_name, img_src) {
                document[img_name].src = img_src;
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
                newwin = window.open(Page, 'PageName', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                }
            }
            function OpenBrochure() {

                var reportId = document.getElementById('<%=hdnRepId.ClientID%>').value;
                var Brochure = document.getElementById('<%=hdnBRPDF.ClientID%>').value;
                OpenNewWindow('../Studies2/' + Brochure, 'BRCPAGE');
                return false;
            }
            function OpenTOC() {
                var reportId = document.getElementById('<%=hdnRepId.ClientID%>').value;
                var toc = document.getElementById('<%=hdnTOCPDF.ClientID%>').value;
                OpenNewWindow('../Studies2/' + toc, 'TOCPAGE');
                return false;
            }
            function OpenOrderPage() {
                var reportEId = document.getElementById('<%=hdnRepEId.ClientID%>').value;
                OpenNewWindow('../ShoppingCart/Order.aspx?Id=' + reportEId, 'ORDERPAGE');
                return false;
            }
            function OpenSamplePage() {
                var reportEId = document.getElementById('<%=hdnRepEId.ClientID%>').value;
                OpenNewWindow('../Studies/SamplePagesMail.aspx?Id=' + reportEId, 'SAMPLEPAGE');
            }

            function BackPage() {

                window.open('multiclientstudy.aspx', '_self');
                return false;
            }

            function ConfirmMessage(typebtn) {
                var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;
                if (userId != "") {
                    if (typebtn == "OS") {
                        OpenOrderPage();
                        return false;
                    }
                    else if (typebtn == "VSP") {
                        OpenSamplePage();
                        return false;
                    }
                }
                else {
                    if (typebtn == "OS") {
                        OpenLoginPopup('OP');
                    }
                    else if (typebtn == "VSP") {
                        OpenLoginPopup('SP');
                    }
                    return false;
                }
            }

//            function AlertMessage(type) {
//                //var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
//                var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;
//                if (userId != "") {
//                    msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
//                    alert(msg);
//                    return false;
//                }
//                else {
//                    if (type == "L") {
//                        ShowPopWindow("../Users_Login/LoginS.aspx");
//                        return false;
//                    }

//                }
//            }

//            function ShowPopWindow(Page) {
//                //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
//                var width = 550;
//                var height = 200;
//                var left = (screen.width - width) / 2;
//                var top = (screen.height - height) / 2;
//                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
//                params += ', location=no';
//                params += ', menubar=no';
//                params += ', resizable=yes';
//                params += ', scrollbars=yes';
//                params += ', status=yes';
//                params += ', toolbar=no';
//                newwin = window.open(Page, 'Chat', params);
//                if (newwin == null || typeof (newwin) == "undefined") {
//                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
//                }
//                return false;
//            }


//            function ConfirmMessage(typebtn) {
//                var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;
//                if (userId != "") {
//                    if (typebtn == "OS") {
//                        OpenOrderPage();
//                        return false;
//                    }
//                    else if (typebtn == "VSP") {
//                        OpenSamplePage();
//                        return false;
//                    }
//                }
//                else {
//                    if (typebtn == "OS") {
//                        ShowPopWindow('../Users_Login/LoginS.aspx?Serv=OP');
//                    }
//                    else if (typebtn == "VSP") {
//                        ShowPopWindow('../Users_Login/LoginS.aspx?Serv=SP');
//                    }
//                    return false;
//                }
//            }
        </script>
        <table cellspacing="7" style="vertical-align: middle; width: 99%">
            <tr>
                <td colspan="2" style="text-align: right;">
                    <asp:Button ID="btnBack" runat="server" Text="Go Back" OnClientClick="return BackPage();" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblheader" runat="Server" Style="font-size: 18px; color: #825f05;
                        font-weight: bold; width: 100%"></asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="justify" colspan="2">
                    <div id="dvContentData" runat="Server" style="width: 100%;">
                    </div>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td colspan="2">
                </td>
            </tr>
           
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="Server" Text="Features" Style="font-size: 17px; color: #ff7b3c;
                        margin-left: 0px; font-weight: bold"></asp:Label>
                </td>
                <td align="center">
                    <asp:HyperLink Style="font-size: 14px; color: #ff7b3c; font-weight: bold; text-decoration: underline;"
                        ID="hypLic" runat="server" Text="License Definitions" NavigateUrl="~/Studies/LicDef.aspx"
                        Target="_blank"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <div id="divLineItems" runat="server">
                    </div>
                    <%-- <ul style="font-size:10.2pt;color:#000000;margin-left:14px;font-family:Optima;">                      
                        <li style="margin-top:0px;"><span style="font-weight:bold">Publication date:</span> <asp:Label ID="lblPDate"  runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Study number:</span> <asp:Label ID="lblSNo" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Copyright:</span> <asp:Label ID="lblCopyR" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold" onmouseover="Tip('The single user license is for one person and does not allow any copying, emailing, or re-distribution.')" onmouseout="UnTip()">Single User License - PDF:</span> <asp:Label ID="lblS1" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold" onmouseover="Tip('The single user license is for one person and does not allow any copying, emailing, or re-distribution.')" onmouseout="UnTip()">Single User License - Hardcopy:</span> <asp:Label ID="lblS2" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold" onmouseover="Tip('The corporate license allows copying, emailing, and re-distribution within the company and its 100% owned subsidiaries.')" onmouseout="UnTip()">Corporate License - PDF:</span> <asp:Label ID="lblC1" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold" onmouseover="Tip('The corporate license allows copying, emailing, and re-distribution within the company and its 100% owned subsidiaries.')" onmouseout="UnTip()">Corporate License - Hardcopy:</span> <asp:Label ID="lblC2" runat="Server"></asp:Label></li>
                                           
                        <li style="margin-top:3px;"><span style="font-weight:bold">Each Additional Copy - PDF:</span> <asp:Label ID="lblE1" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Each Additional Copy - Hardcopy: </span> <asp:Label ID="lblE2" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold">Each Additional Password:</span> <asp:Label ID="lblE3" runat="Server"></asp:Label></li>
                        <li style="margin-top:3px;"><span style="font-weight:bold"> Report Length:</span> <asp:Label ID="lblReportLength" runat="Server"></asp:Label></li>
                        
                    </ul> --%>
                </td>
                <td align="center" style="vertical-align: middle">
                    <table>
                        <tr>
                            <td>
                                <asp:ImageButton ImageUrl="~/Images/OrderStudy.gif" ID="imgbtnOrder" runat="Server"
                                    OnClientClick="return ConfirmMessage('OS');"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ImageUrl="~/Images/ViewSamplePages.gif" ID="imgbtnVwSample" runat="Server"
                                    OnClientClick="return ConfirmMessage('VSP');"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ImageUrl="~/Images/ViewBrochure.gif" ID="imgbtnVwBrch" runat="Server"
                                    OnClientClick="return OpenBrochure();"></asp:ImageButton>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:ImageButton ImageUrl="~/Images/ViewTOC.gif" ID="imgbtnVwToc" runat="Server"
                                    OnClientClick="return OpenTOC();"></asp:ImageButton>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div style="font-size: 17px; color: #ff7b3c; margin-left: 0px; font-weight: bold">
                        Questions?</div>
                    <div style="margin-top: 15px; font-size: 12pt; color: #000000; margin-left: 0px;
                        font-family: Optima;">
                        Call us at [1] 952-405-7500 or
                        <br />
                        Email us at <a href="mailto:sales@savvypack.com" class="LinkStudy">sales@savvypack.com</a>
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="height: 10px;">
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnUserId" runat="Server" />
        <asp:HiddenField ID="hdnRepId" runat="Server" />
        <asp:HiddenField ID="hdnRepEId" runat="Server" />
        <asp:HiddenField ID="hdnBRPDF" runat="Server" />
        <asp:HiddenField ID="hdnTOCPDF" runat="Server" />
        <asp:HiddenField ID="hdnSAMPDF" runat="Server" />
        <asp:HiddenField ID="hdnUserLoginPath" runat="server" />
        <asp:Button ID="btnOrder" runat="server" Style="visibility: hidden;"></asp:Button>
        <asp:Button ID="btnSmple" runat="server" Style="visibility: hidden;"></asp:Button>
    </div>
</asp:Content>
