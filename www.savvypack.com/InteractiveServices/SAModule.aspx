<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="SAModule.aspx.vb" Inherits="InteractiveServices_SAModule" Title="Structure Assistant" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .SmallTextBox
        {
            font-family: Verdana;
            font-size: 10Px;
            background-color: #ffffcc;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
        .Button
        {
            font-family: Optima;
            font-size: 11px;
            color: Black;
            height: 20px;
            margin-left: 5px;
        }
        .testimonial
        {
            margin: 0 0 0 30px;
            padding: 15px;
            position: relative;
            font-style: italic; /* -moz-border-radius: 25px;
  /*  -webkit-border-radius: 25px;*/ /* -khtml-border-radius: 25px; /* for old Konqueror browsers */
            border-radius: 25px; /* future proofing */
            border: outset #CCC;
            background-color: #FEFCA1;
        }
        .down-arrow
        {
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
        
        .testimonialNew
        {
            margin: 0 0 0 30px;
            padding: 13px;
            position: relative;
            font-style: normal;
            border-radius: 25px;
            border: outset #CCC;
            background-color: #ff9966;
        }
    </style>
    <script type="text/javascript">

        function OpenVideo() {
            window.open("Video.aspx");
            return false;
        }

        function roll_over(img_name, img_src) {
            document[img_name].src = img_src;
        }
        function checkPassword() {
            var email = document.getElementById('<%= txtEmail.ClientID%>').value;
            var FirstName = document.getElementById('<%= txtFName.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLName.ClientID%>').value;
            var errorMsg1 = "";
            var msg = "";
            var errorMsg = "";

            if (FirstName == "") {
                errorMsg1 += "\First Name can not be blank.\n";

            }
            if (LastName == "") {
                errorMsg1 += "\Last Name can not be blank.\n";

            }
            if (email == "") {
                errorMsg1 += "\Email Address can not be blank.\n";

            }
            if (errorMsg1 != "") {
                msg = "-----------------------------------------------------\n";
                msg += "Please correct the following problem(s).\n";
                msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }


            if (email == "") {

            }
            else {
                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                var address = email;
                if (reg.test(address) == false) {
                    errorMsg1 += "\Email Address is not in proper format.\n";
                    msg = "-----------------------------------------------------\n";
                    msg += "Please correct the following problem(s).\n";
                    msg += "-----------------------------------------------------\n";
                    errorMsg += alert(msg + errorMsg1 + "\n\n");
                    return false;
                }

                if (errorMsg1 != "") {
                    msg = "-----------------------------------------------------\n";
                    msg += "Please correct the following problem(s).\n";
                    msg += "-----------------------------------------------------\n";
                    errorMsg += alert(msg + errorMsg1 + "\n\n");
                    return false;
                }
                else {

                    return true;
                }

            }
        }
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
        //            // var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
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

        //            //var userId= document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; 
        //            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

        //            if (userId != "") {
        //                if (typebtn == "OS") {
        //                    OpenOrderPage();
        //                    return false;
        //                }
        //            }
        //            else {
        //                if (typebtn == "OS") {
        //                    ShowPopWindow('../Users_Login/LoginS.aspx?Serv=OP');
        //                }
        //                return false;
        //            }
        //        }

        function ConfirmMessage(typebtn) {

            //var userId= document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; 
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

            if (userId != "") {
                if (typebtn == "OS") {
                    OpenOrderPage();
                    return false;
                }
            }
            else {
                if (typebtn == "OS") {
                    OpenLoginPopup('OP');
                }
                return false;
            }
        }

        function OpenOrderPage() {
            var reportId = document.getElementById('<%=hdnRepId.ClientID%>').value;
            OpenNewWindow('../ShoppingCart/Order.aspx?Id=' + reportId, 'SA');

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

            OpenBrochureWindow('http://www.savvypack.com/email/SAMaterialStructure.pdf', 'SCRNSHOT');
            return false;
        }
        function OpenBrochureWindow(Page, PageName) {

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

        }

    </script>
    <%--<div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 2px; margin-left: 5px; color: #825f05;">
        <i>SavvyPack</i><sup>&reg;</sup> Economic Analysis System <span style="font-size: 13px;
            color: Black">(patent pending)</span>
    </div>--%>
    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table cellspacing="7">
            <tr>
                <td>
                    <div style="width: 100%; font-weight: normal; font-size: 16px; text-align: justify;
                        margin-top: 2px; font-family: Verdana">
                        <table border="0" cellpadding="2px" cellspacing="0px">
                            <tr>
                                <td style="width: 60%; padding-top: 10px; padding-right: 10px;" valign="top" colspan="2">
                                    <span style="font-weight: bold; color: Black; font-family: Verdana; font-size: 15px;">
                                        Structure Assistant </span>
                                    <br />
                                    <table>
                                        <tr>
                                            <td style="width: 60%; font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                                                text-align: justify" colspan="2">
                                                <br />
                                                <span><font face="Verdana"><font size="2">The Structure Assistant (SA) is the definitive
                                                    tool for designing packaging structures. </font></font></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                                                height: 20px; text-align: justify" colspan="2">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 60%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                                                margin-top: 0px; text-align: justify;" valign="top" colspan="2">
                                                <span><font face="Verdana"><font size="2">Features: </font></font></span>
                                                <ul>
                                                    <li><font face="Verdana"><font size="2">Provides access from any browser anywhere in
                                                        the world.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Provides Existing and Experimental structures
                                                        for virtually any packaging requirement.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Facilitates packaging structure design.</font></font>
                                                    </li>
                                                    <li><font face="Verdana"><font size="2">Provides highly accurate and highly granular
                                                        permeability data for every material.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Provides highly accurate and highly granular
                                                        permeability data for every structure.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Easy to design to a target permeability. </font>
                                                    </font></li>
                                                    <li><font face="Verdana"><font size="2">Provides immediate access to New Products from
                                                        Suppliers.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Controls and limits access to your content to
                                                        you alone.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Includes a ToolBox for collaboration and content
                                                        sharing with other employees.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Includes a dedicated Company Structure Library
                                                        integrated into the design process.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Provides extensive product and grade profiles
                                                        of suppliers and converters.</font></font> </li>
                                                    <li><font face="Verdana"><font size="2">Provides direct communication with Suppliers.</font></font>
                                                    </li>
                                                    <li><font face="Verdana"><font size="2">Provides Technical Data sheets published by
                                                        Suppliers and Converters.</font></font> </li>
                                                </ul>
                                            </td>
                                            <tr>
                                                <td style="width: 60%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                                                    margin-top: 0px; text-align: justify;" valign="top" colspan="2">
                                                    <br />
                                                    <span><font face="Verdana"><font size="2">Benefits: </font></font></span>
                                                    <ul>
                                                        <li><font face="Verdana"><font size="2">Saves time designing packaging structures.</font></font>
                                                        </li>
                                                        <li><font face="Verdana"><font size="2">Saves time by reducing testing of packaging
                                                            structures.</font></font> </li>
                                                        <li><font face="Verdana"><font size="2">Saves time with a dedicated Company Structure
                                                            Library.</font></font> </li>
                                                        <li><font face="Verdana"><font size="2">Save money by reducing the need to test packaging
                                                            structures.</font></font> </li>
                                                        <li><font face="Verdana"><font size="2">Improves design process through collaboration.
                                                        </font></font></li>
                                                        <li><font face="Verdana"><font size="2">Standardizes the design process throughout the
                                                            company. </font></font></li>
                                                        <li><font face="Verdana"><font size="2">Increases confidence. </font></font></li>
                                                        <li><font face="Verdana"><font size="2">Provides better structures. </font></font>
                                                        </li>
                                                    </ul>
                                                </td>
                                            </tr>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 40%; padding-top: 10px; padding-right: 10px;" valign="top" colspan="2">
                                    <table>
                                        <tr>
                                            <td style="width: 40%; padding-left: 5px;">
                                                <table cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
                                                    <tr>
                                                        <td>
                                                            <table>
                                                                <tr>
                                                                    <td style="width: 70px">
                                                                    </td>
                                                                    <td>
                                                                        <asp:ImageButton ID="img1" runat="server" ImageUrl="~/Images/SBA@Glance.JPG" Width="200px"
                                                                            OnClientClick="return OpenVideo();" Height="110px" />
                                                                    </td>
                                                                    <td>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 50px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <div id="dvConsulting" runat="Server" style="width: 250px; height: 40px; text-align: justify;
                                                                font-size: 10px; font-weight: normal; font-family: Verdana; color: Black;" class="testimonial">
                                                                Major CPG ... the Structure Assistant is a compelling value and compelling capability
                                                                for package design.
                                                                <br />
                                                            </div>
                                                            <div class="down-arrow">
                                                            </div>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1" valign="middle">
                                                            <br />
                                                            <div id="Div1" runat="Server" style="width: 250px; height: 136px; margin-top: 5px;
                                                                text-align: justify; font-size: 10px; font-weight: normal; font-family: Verdana;
                                                                color: Black;" class="testimonialNew">
                                                                <table>
                                                                    <tr>
                                                                        <td colspan="2">
                                                                            If you would like more information on Structure Assistant&#8482;, please provide
                                                                            your name and email address.
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 130px; font-size: 9px;" align="right">
                                                                            <b>First Name:</b>
                                                                        </td>
                                                                        <td style="width: 160px;" align="left">
                                                                            <asp:TextBox ID="txtFName" runat="server" MaxLength="25" CssClass="SmallTextBox"
                                                                                Width="160px" Height="12px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 130px; font-size: 9px;" align="right">
                                                                            <b>Last Name:</b>
                                                                        </td>
                                                                        <td style="width: 160px;" align="left">
                                                                            <asp:TextBox ID="txtLName" runat="server" MaxLength="25" CssClass="SmallTextBox"
                                                                                Width="160px" Height="12px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 130px; font-size: 9px;" align="right">
                                                                            <b>Email Address:</b>
                                                                        </td>
                                                                        <td style="width: 160px;" align="left">
                                                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" CssClass="SmallTextBox"
                                                                                Width="160px" Height="12px"></asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>
                                                                        </td>
                                                                        <td align="left">
                                                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="Button" OnClientClick="return checkPassword()" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="down-arrow">
                                                            </div>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style1">
                                                            <br />
                                                            <div id="dvPublication" runat="Server" style="width: 250px; height: 40px; margin-top: 5px;
                                                                text-align: justify; font-size: 10px; font-weight: normal; font-family: Verdana;
                                                                color: Black;" class="testimonial">
                                                                Major Converter ... this tool is a must have for the novice and professional alike.
                                                                <br />
                                                            </div>
                                                            <div class="down-arrow">
                                                            </div>
                                                            <br />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br />
                                                            <div id="dvInteractiveService" runat="Server" style="width: 250px; height: 40px;
                                                                margin-top: 5px; text-align: justify; font-size: 10px; font-weight: normal; font-family: Verdana;
                                                                color: Black;" class="testimonial">
                                                                Major Supplier ... the Structure Assistant provides package structures for virtually
                                                                any product I can think of.
                                                                <br />
                                                            </div>
                                                            <div class="down-arrow">
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <table>
                        <tr>
                            <td style="color: Black; font-family: Verdana; font-size: 12px; text-align: justify"
                                colspan="2">
                                <br />
                                To order, you must first login.
                                <%--<a onclick="return AlertMessage('L');"
                            id="lnkCreate" style="font-style: normal; font-family: Verdana; font-size: 12px;"
                            class="Link" href="#">login</a>.--%><br />
                                If you do not have an account, please create an account using Login link.
                                <%--<a onclick="return AlertMessage();" style="font-style: normal;
                            font-family: Verdana; font-size: 12px;" target="_blank" class="Link" href="../Users_Login/AddEditAccount.aspx">
                            create an account</a> then <a onclick="return AlertMessage('L');" style="font-style: normal;
                                font-family: Verdana; font-size: 12px;" class="Link" href="#">login</a>.--%>
                            </td>
                        </tr>
                    </table>
                    <table border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 380px; padding-top: 10px; padding-right: 10px;" valign="top">
                                <asp:Label ID="lblfeatures" runat="Server" Text="Prices" Style="font-size: 18px;
                                    color: #ff7b3c; margin-left: 5px; font-weight: bold"></asp:Label>
                                <ul style="font-size: 10.2pt; color: #000000; margin-top: 5px; font-family: Optima;">
                                    <li style="margin-top: 3px;"><span style="font-weight: bold">Delivery:</span>
                                        <asp:Label ID="Label1" runat="Server">Instantaneous</asp:Label></li>
                                    <li style="margin-top: 3px;"><span style="font-weight: bold">Product Number:</span>
                                        <asp:Label ID="lblSNo" runat="Server"></asp:Label></li><li style="margin-top: 3px;">
                                            <span style="font-weight: bold">Copyright: </span>
                                            <asp:Label ID="lblCopyR" runat="Server"></asp:Label></li>
                                    <li style="margin-top: 3px;"><span style="font-weight: bold">Annual License – 10 Users:
                                    </span>
                                        <asp:Label ID="lblSA1" runat="Server"></asp:Label></li>
                                    <li style="margin-top: 3px;"><span style="font-weight: bold">Annual License – 3 Users:</span>
                                        <asp:Label ID="lblSA2" runat="Server"></asp:Label></li>
                                    <li style="margin-top: 3px;"><span style="font-weight: bold">Annual License – Single
                                        User: </span>
                                        <asp:Label ID="lblSA3" runat="Server"></asp:Label></li>
                                </ul>
                            </td>
                            <td style="width: 280px; padding-left: 10px;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblCall" Text="Please call to order." Style="color: Red; font-size: 15px;
                                                font-weight: bold; display: none;" runat="Server"></asp:Label>
                                            <asp:ImageButton ImageUrl="~/Images/OrderButton.png" ID="imgbtnOrder" runat="Server"
                                                Style="display: block" OnClientClick="return ConfirmMessage('OS');" ToolTip="Available to Order">
                                            </asp:ImageButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:ImageButton ImageUrl="~/Images/ScreenShotButton.png" ID="imgbtnScreen" runat="Server"
                                                OnClientClick="return OpenBrochure();" ToolTip="ScreenShot SA"></asp:ImageButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <div id="dvCompanyInfo" runat="server" style="vertical-align: top; margin-left: 10px;
        text-align: left;">
        <table border="0" cellpadding="2px" cellspacing="0px">
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
            <%--<tr>
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
