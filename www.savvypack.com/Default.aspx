<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <link href="css/bootstrap.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/style.css" rel="stylesheet" type="text/css" media="all" />
    <link href="css/prettyPhoto.css" rel="stylesheet" type="text/css" />
    <link href="css/font-awesome.css" rel="stylesheet" />
    <!-- //for bootstrap working -->
    <link href="//fonts.googleapis.com/css?family=Raleway:100,100i,200,300,300i,400,400i,500,500i,600,600i,700,800"
        rel="stylesheet" />
    <link href="//fonts.googleapis.com/css?family=Source+Sans+Pro:300,300i,400,400i,600,600i,700"
        rel="stylesheet" />
    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="js/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <title></title>
    <script type="text/javascript" src="js/jquery-1.12.3.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="tooltipster-master/dist/css/tooltipster.bundle.min.css" />
    <script type="text/javascript" src="tooltipster-master/dist/js/tooltipster.bundle.min.js"></script>
    <link rel="stylesheet" type="text/css" href="tooltipster-master/dist/css/plugins/tooltipster/sideTip/themes/tooltipster-sideTip-shadow.min.css" />
    <style type="text/css">
        .bs-example
        {
            margin: 20px;
        }
        .modal-content iframe
        {
            margin: 0 auto;
            display: block;
        }
    </style>
    <style type="text/css">
        body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            width: 620px;
            border: 3px solid #0DA9D0;
            padding: 0;
        }
        .modalPopup .header
        {
            background-color: #2FBDF1;
            height: 30px;
            color: White;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
        .modalPopup .body
        {
            min-height: 50px;
            padding: 5px;
            line-height: 30px;
            text-align: center;
            font-weight: bold;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.tooltip').tooltipster({
                theme: 'tooltipster-shadow',
                maxWidth: 290
            });
        });                  
    </script>
    <script type="text/javascript">
        function ShowModalPopup() {
            $get("video").src = "Default1.aspx"
            $find("mpe").show();
            return false;
        }
        function HideModalPopup() {

            $get("video").stop;
            $get("video").src = "";
            $find("mpe").hide();
            //window.close();
            return false;
        }
    </script>
    <style type="text/css">
        .testimonialNew
        {
            margin: 20px 0 0 0px;
            padding: 13px;
            font-style: normal;
            border-radius: 25px;
            border: outset #CCC;
            background-color: #ff9966;
        }
        .works_pr
        {
            padding: 5em 0;
            background-color: #ff9966;
            border-radius: 25px;
            border: outset #CCC;
            width: 340px;
            padding: 13px;
        }
        
        .works_pr1
        {
            padding: 5em 0;
            width: 340px;
            padding: 13px;
        }
        
        .NormalTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 170px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
        }
    </style>
    <script type="text/javascript">
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
    </script>
</head>
<body style="overflow: auto;">
    <form id="form1" runat="server">
    <center>
        <div class="banner_bottom">
            <div id="Div2" runat="Server">
                &nbsp;<img src="Images/SavvyPackAnalyticalServicesLOGO.png" alt=" " class="img-responsive5" /></div>
            <div id="containermain" style="width: 60%; margin-left: -6.5%; margin-right: 0.5%;">
                <table style="margin-top: -5%; width: 80%;">
                    <col width="50%"/>
                    <col width="30%"/>
                    <tr>
                        <td>
                            <div>
                                <h3 class="tittle-w3ls">
                                    &nbsp;What is it?</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <h4>
                                            </h4>
                                            <p style="margin-left: 15px;">
                                                SavvyPack® Analytical Services is an extremely detailed modeling service that very
                                                accurately transforms ...
                                            </p>
                                        </div>
                                        <div>
                                        </div>
                                        <div class="col-md-6 banner_bottom_left">
                                            <br />
                                            <p style="margin-left: 9px; color: red;font-size:0.9em;">
                                                the real world to the virtual world
                                            </p>
                                            <%-- <asp:Label ID="Label1" runat="server" Text="The Real world  " Font-Bold="true" ForeColor="red"
                                                Style="margin-left: 25px; margin-bottom: 2px;" Font-Size="11"></asp:Label>
                                            <asp:Label ID="Label2" runat="server" Text="to " Font-Bold="true" ForeColor="red"
                                                Style="margin-left: 40px; margin-bottom: 2px;" Font-Size="11"></asp:Label>
                                            <asp:Label ID="Label3" runat="server" Text="The Virtual world" Font-Bold="true" ForeColor="red"
                                                Style="margin-left: 14px; margin-bottom: 2px;" Font-Size="11"></asp:Label>--%>
                                            <img src="Images/VirtualFactoryFloor-to-ComputerNEW.png" alt=" " class="img-responsive6" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 100px;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;How does it work?</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-61 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/SPAnalyticalServicesWithExtraCROPPED.png" alt=" " usemap="#SPanalytical" />
                                        </div>
                                        <div style="text-align: left;">
                                            <p style="margin-left: 20px; font-size:1.3em;">
                                                SavvyPack® Analytical Services utilizes a three-pronged approach in order to provide
                                                the most accurate and insightful economic and environmental analysis to the packaging
                                                industry.
                                            </p>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 100px;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;What does it provide?</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-6 banner_bottom_center">
                                            <h4>
                                            </h4>
                                            <p style="margin-left: 15px;">
                                                SavvyPack® Analytical Services provide sophisticated, fully integrated value chain
                                                and life cycle analysis.
                                            </p>
                                        </div>
                                        <div>
                                        </div>
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/puzzle piecesRS.png" alt=" " class="img-responsive3" style="padding-right: 60px;" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 100px;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;Business model</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-6 banner_bottom_left">
                                            <h4>
                                            </h4>
                                            <p style="margin-left: 15px; ">
                                                SavvyPack® Analytical Services is an annual subscription service that provides unlimited
                                                access to your assigned analyst and full access to all content and software.
                                            </p>
                                        </div>
                                        <div>
                                        </div>
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/subscribe_button.jpg" alt=" " class="img-responsive7" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 100px;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;Software configuration</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-62 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/3DBlocksEconomicEnvironModules.png" alt=" " style="margin-left: 85px;
                                                margin-top: 25px;" />
                                        </div>
                                        <div>
                                        </div>
                                        <div style="text-align: left; margin-left: 20px; margin-right: 20px">
                                            <p style="font-size:1.3em;">
                                                The software is configured to match the real world value chain, from raw materials
                                                to retail. The SavvyPack® software is the only software in the world to integrate
                                                value chain and life cycle analysis.
                                            </p>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div style="margin-top: 100px;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;Benefits</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-6 banner_bottom_left">
                                            <h4 style="margin-left: 15px;">
                                                Save Time</h4>
                                            <p style="margin-left: 15px;">
                                                Save time of internal resources, and time to market is faster.
                                            </p>
                                        </div>
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/clock.jpg" alt=" " class="img-responsive4" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="col-md-6 banner_bottom_left">
                                            <h4 style="margin-left: 15px;">
                                                Reduce Expenses</h4>
                                            <p style="margin-left: 15px;">
                                                SavvyPack® Analytical Services provide much lower cost than doing models internally.
                                            </p>
                                        </div>
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/bills%20coins%20images.jpg" alt=" " class="img-responsive" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="col-md-6 banner_bottom_left">
                                            <h4 style="margin-left: 15px;">
                                                Higher Quality Analysis</h4>
                                            <p style="margin-left: 15px;">
                                                SavvyPack® analysts only do modeling and are the very best at what they do.
                                            </p>
                                        </div>
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/analysis%20images.jpg" alt=" " class="img-responsive4" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                </div>
                                <div class="clearfix">
                                </div>
                            </div>
                            <div style="margin-top: 100px; text-align:center;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;Screen shots
                                </h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-9 banner_bottom_left">
                                            <img src="Images/ScreenShot_E2_Case_ManagerWithBorder.png" alt=" " class="img-responsive2" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="col-md-9 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/ScreenShotCombined.png" alt=" " class="img-responsive2" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </td>
                        <td style="width: 40%; vertical-align: top;">
                            <div id="Div1" runat="Server" class="testimonialNew" style="width: 320px; height: 170px;
                                text-align: justify; font-size: 11px; font-weight: normal; font-family: Verdana;
                                color: Black;">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            If you would like more information on SavvyPack® Analytical Services&#8482;, please
                                            provide your name and email address.
                                        </td>
                                    </tr>
                                    <tr style="padding-top: 15px;">
                                        <td style="font-size: 10px; padding-right: 5px;" align="right">
                                            <b>First Name:</b>
                                        </td>
                                        <td style="width: 0px;" align="left">
                                            <asp:TextBox ID="txtFName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10px; padding-right: 5px;" align="right">
                                            <b>Last Name:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtLName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 10px; padding-right: 5px;" align="right">
                                            <b>Email Address:</b>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" CssClass="NormalTextBox"></asp:TextBox>
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
                            <div id="Div3" runat="Server" class="testimonialNew2">
                                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                </cc1:ToolkitScriptManager>
                                <%--<asp:TextBox ID="txtUrl" runat="server" Width="300" Text="" />
                        <asp:Button ID="btnShow" runat="server" Text="Play Video" OnClientClick="return ShowModalPopup()" />--%>
                                <asp:LinkButton ID="lnkDummy" runat="server"></asp:LinkButton>
                                <asp:ImageButton ID="imgBut" runat="server" ImageUrl="~/Images/vid_Imagebutton.png"
                                    Width="310px" OnClientClick="return ShowModalPopup();" Height="180px" />
                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" BehaviorID="mpe" runat="server"
                                    PopupControlID="pnlPopup" TargetControlID="lnkDummy" BackgroundCssClass="modalBackground"
                                    CancelControlID="btnClose">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlPopup" runat="server" CssClass="modalPopup" Style="display: none">
                                    <div class="header">
                                        Savvypack Analytical Services
                                    </div>
                                    <div class="body">
                                        <iframe id="video" width="600" height="470" frameborder="0" allowfullscreen></iframe>
                                        <br />
                                        <br />
                                        <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="return HideModalPopup();" />
                                    </div>
                                </asp:Panel>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <br />
            <map id="m1" name="SPanalytical">
                <area class="tooltip" shape="rect" href="#" coords="0,105,194,185" title="SavvyPack® Analytical Services analysts are all packaging experts with years of experience in the packaging industry.  As a result, they speak your language and fully understand your business challenges."
                    alt="Down/Left" />
                <area class="tooltip" shape="rect" href="#" coords="286,14,483,90" title="SavvyPack® Analytical Services content was specifically developed for the packaging industry.  It includes all relevant data for doing economic and environmental analysis.  This includes raw material prices, inventory data, process data, structural data, and much more."
                    alt="Down/Right" />
                <area class="tooltip" shape="rect" href="#" coords="567,110,760,184" title="SavvyPack® Analytical Services software is an internally developed, proprietary software tool designed specifically for doing economic and environmental analysis of packaging.  It is completely unique in the packaging industry in terms of its design, accuracy, and scope."
                    alt="Up/Left" />
            </map>
        </div>
    </center>
    </form>
    <p style="background-color: #D3DAD0; height: 30px; text-align: center; margin-top: 0px;
        margin-bottom: 0px;">
        <asp:Label ID="lblTag" runat="Server"></asp:Label>
    </p>
</body>
</html>
