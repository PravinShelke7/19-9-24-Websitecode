<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Structure Assistant.aspx.vb"
    Inherits="Structure_Assistant" %>

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
    <link href="//fonts.googleapis.com/css?family=Source+Structure Assistantns+Pro:300,300i,400,400i,600,600i,700"
        rel="stylesheet" />
    <script src="js/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="js/jquery-2.2.3.min.js" type="text/javascript"></script>
    <script src="js/bootstrap.js" type="text/javascript"></script>
    <title>Structure Assistant | SavvyPack</title>
    <script type="text/javascript" src="js/jquery-1.12.3.min.js"></script>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" type="text/css" href="tooltipster-master/dist/css/tooltipster.bundle.min.css" />
    <script type="text/javascript" src="tooltipster-master/dist/js/tooltipster.bundle.min.js"></script>
    <link rel="stylesheet" type="text/css" href="tooltipster-master/dist/css/plugins/tooltipster/sideTip/themes/tooltipster-sideTip-shadow.min.css" />
    <script src="js/enquire.js" type="text/javascript"></script>
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

        // This loads JS files in the head element
        function loadJS(url) {
            
            var coord1 = document.getElementById('tooltip1');
            var coord2 = document.getElementById('tooltip2');
            var coord3 = document.getElementById('tooltip3');

            if (coord1 != null && coord2 != null && coord3 != null) {
                if (url == "7") {
                    document.getElementById('tooltip1').coords = '0%,80%,158%,140%';
                    document.getElementById('tooltip2').coords = '260%,0%,422%,60%';
                    document.getElementById('tooltip3').coords = '541%,80%,703%,140%';
                }
                else if (url == "6") {
                    document.getElementById('tooltip1').coords = '0%,75%,145%,120%';
                    document.getElementById('tooltip2').coords = '240%,0%,383%,50%';
                    document.getElementById('tooltip3').coords = '490%,75%,635%,120%';
                }
                else if (url == "8") {
                    document.getElementById('tooltip1').coords = '0%,85%,170%,145%';
                    document.getElementById('tooltip2').coords = '280%,0%,450%,60%';
                    document.getElementById('tooltip3').coords = '575%,85%,755%,145%';
                }
                else if (url == "9") {
                    document.getElementById('tooltip1').coords = '0%,90%,180%,155%';
                    document.getElementById('tooltip2').coords = '295%,0%,475%,70%';
                    document.getElementById('tooltip3').coords = '610%,90%,790%,155%';
                }
                else if (url == "10") {
                    document.getElementById('tooltip1').coords = '0%,95%,190%,165%';
                    document.getElementById('tooltip2').coords = '315%,0%,505%,75%';
                    document.getElementById('tooltip3').coords = '650%,95%,840%,165%';
                }
                else if (url == "11") {
                    document.getElementById('tooltip1').coords = '0%,95%,192%,170%';
                    document.getElementById('tooltip2').coords = '315%,0%,510%,75%';
                    document.getElementById('tooltip3').coords = '655%,95%,855%,170%';
                }
                else if (url == "12") {
                    document.getElementById('tooltip1').coords = '0%,95%,190%,170%';
                    document.getElementById('tooltip2').coords = '315%,0%,510%,75%';
                    document.getElementById('tooltip3').coords = '655%,95%,850%,170%';
                }
                else if (url == "13") {
                    document.getElementById('tooltip1').coords = '0%,95%,190%,170%';
                    document.getElementById('tooltip2').coords = '320%,0%,515%,75%';
                    document.getElementById('tooltip3').coords = '655%,95%,850%,170%';
                }
            }

        }

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
    <style type="text/css">
        .testimonialNew
        {
            margin: 20px 0 0 0px;
            padding: 13px;
            font-style: normal;
            border-radius: 25px;
            border: outset #CCC;
            background-color: #ff9966;
            position: fixed;
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
        
        .testimonial
        {
            margin-top: 250px;
           
            border-radius: 25px;
            border: outset #CCC;
             padding: 7px;
            padding-left:13px;
            width: 310px;
            height: 80px;
            position: fixed;
            background-color: #FEFCA1;
        }
        .works_Info
        {
            margin: 2.5% auto 0 auto;
            padding: 5em 0;
            background-color: #ff9966;
            border-radius: 25px;
            border: outset #CCC;
            padding: 13px;
            width: 310px;
            position: fixed;
        }
        
        .StructureAssistant
        {
            background-image: url("Images/laboratory-2815640_960_720.jpg");
            background-repeat: no-repeat;
            background-size: cover;
            background-attachment: fixed;
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
        }
        @media (max-width:480px)
        {
            .works_Info
            {
                position: relative;
                top: 0px;
                left: 0px;
            }
            .StructureAssistant
            {
                background-position: center center;
                background-repeat: no-repeat;
                margin: 0;
            }
            .testimonial
            {
                margin-top: 90px;
                margin-bottom: -1%;
                top: -60px;
                position: relative;
                padding: 0px;
                left: -3px;
                margin-left: auto;
                margin-right: auto;
                padding-top: 1em;
                padding-left: 1.5em;
                padding-bottom: 1.8em;
            }
            .img-responsive2
            {
               margin-left:4px;
                
            }
        }
        @media (min-width:1440px)
        {
            .InfoVide
            {
                display: block;
                margin: auto auto auto 14%;
            }
        }
    </style>

<script async src="//pagead2.googlesyndication.com/pagead/js/adsbygoogle.js"></script>
<script>

  (adsbygoogle = window.adsbygoogle || []).push({

    google_ad_client: "ca-pub-6818579714884920",

    enable_page_level_ads: true

  });

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class=" StructureAssistant ">
        <div style="background-color: rgba(255,255,255,0.5);">
            <div class=" container-fluid">
            <div class="row" style="margin-top: -0.3%; padding-left: 3%; padding-right: 2%;">
                <div class="col-lg-9">
                    <div class="row" style="margin-right: -2%;">
                        <div class="col-lg-3">
                            <img src="Images/SavvyPackSA.png" alt=" " class="img-responsiveL" />
                        </div>
                        <div class="col-lg-9 center-block">
                            <div>
                                <h3 class="tittle-w3ls">
                                    &nbsp;What is it?</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-6 banner_bottom_grid help">
                                            <h4>
                                            </h4>
                                            <p style="margin-left: 15px;">
                                                SavvyPack<sub>®</sub> SA is an extremely detailed modeling service that very accurately
                                                transforms ...
                                            </p>
                                        </div>
                                        <div>
                                        </div>
                                        <div class="col-md-6 banner_bottom_left" style="margin-top: -4%;">
                                            <br />
                                            <img src="Images/Laboratory-to-prediction.png" alt=" " class="img-responsive6" />
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
                                        <div class="col-md-61">
                                            <br />
                                            <img src="Images/Structure_Assistant1.png" alt=" " style="margin-left: auto; margin-top: 2%;
                                                margin-right: auto; margin-bottom: 0px; display: block; max-width: 100%;" usemap="#SPanalytical" />
                                        </div>
                                        <div style="text-align: left;">
                                            <p style="margin-left: 20px; font-size: 1.3em; padding-right: 2%; padding-left: 2%;
                                                padding-top: -2%">
                                                SavvyPack<sub>®</sub> SA utilizes a three-pronged approach in order to provide the
                                                most accurate and insightful performance property modeling to the packaging industry.
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
                                                SavvyPack<sub>®</sub> SA provides sophisticated, fully integrated performance property
                                                modeling.
                                            </p>
                                            <p style="margin-left: 15px;">
                                                Our sister application, SavvyPack<sub>®</sub> Analytical Service, provides sophisticated,
                                                fully integrated value chain and life cycle analysis.
                                            </p>
                                        </div>
                                        <div>
                                        </div>
                                        <div class="col-md-6">
                                            <br />
                                            <img src="Images/puzzle piecesRS.png" alt=" " class="img-responsive3" style="margin-left: auto;
                                                margin-top: 0px; margin-right: auto; margin-bottom: 0px; float: left;" />
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
                                            <p style="margin-left: 15px;">
                                                SavvyPack<sub>®</sub> SA is an annual subscription service that provides unlimited
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
                                        <div class="col-md-6 banner_bottom_left">
                                            <br />
                                            <div class="col-md-6 banner_bottom_left">
                                                <h4 style="margin-left: 69.3%; font-size: larger;">
                                                    Analytical</h4>
                                            </div>
                                            <img src="Images/analytical.png" alt=" " style="margin-left: 4%; margin-right: auto;
                                                margin-bottom: 5%; display: block; width: 100%; height: 100%;" />
                                        </div>
                                        <div class="col-md-6 banner_bottom_left">
                                            <br />
                                            <div class="col-md-6 banner_bottom_left" style="margin-bottom: -2px;">
                                                <h4 style="margin-left: 69.3%; font-size: larger;">
                                                    Empirical</h4>
                                            </div>
                                            <img src="Images/Empirical Observation.jpg" alt=" " style="margin-left: auto; margin-right: auto;
                                                margin-bottom: 6%; display: block; width: 90%; height: 90%;" />
                                        </div>
                                        <div style="text-align: left;">
                                            <p style="margin-left: 20px; font-size: 1.3em; padding-right: 2%; padding-left: 2%;
                                                padding-top: -4%">
                                                SavvyPack<sub>®</sub> SA allows you to do both analytical and empirical structure
                                                design. It includes an inventory of commercial structures for you to choose from,
                                                and it also allows you to design a structure from scratch.
                                            </p>
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%--<div style="margin-top: 100px;">
                            <h3 class="tittle-w3ls">
                                &nbsp;Software configuration</h3>
                            <div class="inner_sec_info_wthree_agile">
                                <div class="works">
                                    <div class="col-md-6 banner_bottom_left">
                                        <h4 style="margin-left: 15px; margin-top: 30%; margin-left: 65%;">
                                            Analytical</h4>
                                    </div>
                                    <div class="col-md-6 banner_bottom_grid help">
                                        <br />
                                        <img src="Images/analytical.png" alt=" " style="margin-left: auto; margin-top: 2%;
                                            margin-right: auto; margin-bottom: 0px; display: block; width: 65%;" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div class="col-md-6 banner_bottom_left">
                                        <h4 style="margin-left: 15px; margin-top: 30%; margin-left: 65%;">
                                            Empirical</h4>
                                    </div>
                                    <div class="col-md-6 banner_bottom_grid help">
                                        <br />
                                        <img src="Images/Empirical Observation.jpg" alt=" " style="margin-left: auto; margin-top: 2%;
                                            margin-right: auto; margin-bottom: 6%; display: block; width: 65%;" />
                                    </div>
                                    <div class="clearfix">
                                    </div>
                                    <div style="text-align: left;">
                                        <p style="margin-left: 20px; font-size: 1.3em; padding-right: 2%; padding-left: 2%;
                                            padding-top: -2%">
                                            The software is configured to provide performance properties modeling. Our sister
                                            publication, SavvyPack<sub>®</sub> Analytical Service,is configured to match the
                                            real world value chain, from raw materials to retail.The SavvyPack software is the
                                            only software in the world to integrate the value chain and life cycle analysis.
                                        </p>
                                    </div>
                                </div>
                            </div>
                        </div>--%>
                            <div style="margin-top: 100px;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;Benefits</h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-6 banner_bottom_left">
                                            <h4 style="margin-left: 15px;">
                                                Save Time</h4>
                                            <p style="margin-left: 15px;">
                                                SavvyPack<sub>®</sub> SA saves the time of internal resources, and time to market
                                                is faster.
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
                                                SavvyPack<sub>®</sub> SA provideS much lower cost than doing models internally.
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
                                                SavvyPack<sub>®</sub> analysts only do modeling and are the very best at what they
                                                do.
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
                            <div style="margin-top: 100px; text-align: center;">
                                <h3 class="tittle-w3ls">
                                    &nbsp;Screen shots
                                </h3>
                                <div class="inner_sec_info_wthree_agile">
                                    <div class="works">
                                        <div class="col-md-9 banner_bottom_left">
                                            <img src="Images/search_cookie2 w frame.png" alt=" " class="img-responsive2" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                        <div class="col-md-9 banner_bottom_grid help">
                                            <br />
                                            <img src="Images/Structure2757screenshot_w border.png" alt=" " class="img-responsive2" />
                                        </div>
                                        <div class="clearfix">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="col-lg-3">
                    <div class="row InfoVide">
                        <div id="Div1" runat="Server" class="works_Info" style="text-align: justify; font-size: 13px;
                            font-weight: normal; font-family: Verdana; color: Black;">
                            <table>
                                <col width="40%" />
                                <col width="60%" />
                                <tr>
                                    <td colspan="2" style="padding-bottom: 3%;">
                                        If you would like more information on SavvyPack<sub>®</sub> SA&#8482;, please provide
                                        your name and email address.
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 11px;" align="right">
                                        <b>First Name:</b>
                                    </td>
                                    <td style="width: 0px;" align="left">
                                        <asp:TextBox ID="txtFName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 11px;" align="right">
                                        <b>Last Name:</b>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="font-size: 11px;" align="right">
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
                        <div id="Div2" runat="Server" class="testimonial" style="text-align: justify; font-size: 11px;
                            font-weight: normal; font-family: Verdana; color: Black;">
                            <td colspan="2">
                                <span style="font-weight: bold; color: #825f05; font-family: Verdana; font-size: 14px;">
                                    Questions?</span><br />
                                <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                                    Call us at [1] 952-405-7500 or</span>
                                <br />
                                <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                                    email us at <a style="text-decoration: none; font-style: italic; font-weight: bold"
                                        class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a></span>
                            </td>
                        </div>
                         <div id="Div3" runat="Server" class="testimonial" style="text-align: justify; font-size: 11px;
                                font-weight: normal; font-family: Verdana; color: Black; margin-top: 370px;">
                                <table>
                                    <tr>
                                        <td>
                                            <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                                                Want to know more about Structure Assistant Or Order Structure Assistant?</span>
                                            <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                                                Then please </span>
                                            <asp:LinkButton ID="LinkButton6" runat="Server" CssClass="InteractiveLink" Text="Click Here."
                                                ToolTip="Click here to order" Style="font-size: 14px;" PostBackUrl="~/InteractiveServices/SAModule.aspx"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <map id="m1" name="SPanalytical">
            <area id="tooltip1" class="tooltip" shape="rect" href="#" coords="3,100,195,183"
                title="SavvyPack® SA analysts are all packaging experts with years of experience in the packaging industry.  As a result, they speak your language and fully understand your packaging challenges."
                alt="Down/Left" />
            <area id="tooltip2" class="tooltip" shape="rect" href="#" coords="273,0,512,98" title="SavvyPack® SA content was specifically developed for the packaging industry.  It includes all relevant data for doing performance properties modeling.  This includes raw material and structure thickness, weight, barrier properties, and more based on a range of temperature and humidity inputs."
                alt="Down/Right" />
            <area id="tooltip3" class="tooltip" shape="rect" href="#" coords="657,90,850,200"
                title="SavvyPack® SA software is an internally developed, proprietary software tool designed specifically for doing performance properties modeling of packaging.  It is completely unique in the packaging industry in terms of its design, accuracy, and scope."
                alt="Up/Left" />
        </map>
        </div>
    </div>
    </form>
    <p style="background-color: #D3DAD0; height: 30px; text-align: center; margin-top: 0px;
        margin-bottom: 0px;">
        <asp:Label ID="lblTag" runat="Server"></asp:Label>
    </p>
	<script type="text/javascript">

        enquire.register("screen and (max-width: 599px)", {
            match: function () {
                // Load a mobile JS file
                loadJS('1');
            }
        });


        enquire.register("screen and (min-width: 600px) and (max-width: 899px)", {
            match: function () {
                // Load a tablet JS file
                loadJS('2');
                //console.log('tablet loaded');
            }
        });


        enquire.register("screen and (min-width: 900px) and (max-width: 999px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('3');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1000px) and (max-width: 1099px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('4');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1100px) and (max-width: 1199px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('5');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1200px) and (max-width: 1299px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('6');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1300px) and (max-width: 1399px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('7');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1400px) and (max-width: 1499px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('8');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1500px) and (max-width: 1599px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('9');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1600px) and (max-width: 1699px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('10');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1700px) and (max-width: 1799px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('11');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1800px) and (max-width: 1899px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('12');
                //console.log('desktop loaded');
            }
        });

        enquire.register("screen and (min-width: 1900px) and (max-width: 1999px)", {
            match: function () {
                // Load a desktop JS file
                loadJS('13');
                //console.log('desktop loaded');
            }
        });

        //        enquire.register("screen and (min-width: 1500px)", {
        //            match: function () {
        //                // Load a desktop JS file
        //                loadJS('14');
        //                //console.log('desktop loaded');
        //            }
        //        });
    </script>
</body>
</html>
