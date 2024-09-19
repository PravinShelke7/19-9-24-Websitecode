<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SavvypackProject.aspx.vb"
    Inherits="Savvypack" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <style type="text/css">
    /*Background image*/
body, html {
    height: 100%;
    margin: 0;
    font-family: Verdana, sans-serif; 
    background-attachment:fixed;
}

.bg {
    /* The image used */
    background-image: url("Images/business.jpg");

    /* Full height */
    height: 50%; 

    /* Center and scale the image nicely */
    background-position: center;
    background-repeat: no-repeat;
    background-size: cover;
    background-attachment:fixed;
}

/*logo image*/
.logo-container{
	float:center;
	width:100%;
     height:100%
}

.logo-img{
	display:block;
	margin-left:auto;
	margin-right:auto;
	width:50%;
}



/*layout*/
.flex-container {
  margin-top:2%;
  margin-left:5%;
  margin-right:5%;	
  display: flex;
  flex-flow: row wrap;
  justify-content: center;
}

.flexbox{
  background-color: #f1f1f1;
  width: 500px;
  height: 500px;
  margin:2%;
box-shadow: 0 8px 16px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
}


/*inblock image*/
.inblock-img{
	display:block;
	width:300px;
	margin-left:auto;
	margin-right:auto;
}



/*SlideShow*/
* {box-sizing: border-box}
.mySlides {display: none}
img {vertical-align: middle;}

/* Slideshow container */
.slideshow-container {
  width: 600px;
  height:450px;
  position: relative;
  margin: auto auto auto auto;
  
}

/* Next & previous buttons */
.prev, .next {
  cursor: pointer;
  position: absolute;
  top: 50%;
  width: auto;
  padding: 16px;
  margin-top: -22px;
  color: white;
  font-weight: bold;
  font-size: 18px;
  transition: 0.6s ease;
  border-radius: 0 3px 3px 0;
  background-color: rgba(0,0,0,0.2);
}

/* Position the "next button" to the right */
.next {
  right: 0%;
  border-radius: 3px 0 0 3px;
}

/* On hover, add a black background color with a little bit see-through */
.prev:hover, .next:hover {
  background-color: rgba(0,0,0,0.8);
}

/* Caption text */
.text {
  color: #ffffff;
  font-size: 15px;
  padding: 8px 12px;
  position: absolute;
  bottom: 2px;
  width: 100%;
  text-align: center;
}

/* Number text (1/4 etc) */
.numbertext {
  color: #ffffff;
  font-size: 12px;
  padding: 8px 12px;
  position: absolute;
  top: 0;
}

/* The dots/bullets/indicators */
.dot {
  cursor: pointer;
  height: 15px;
  width: 15px;
  margin: 0 2px;
  background-color: #bbb;
  border-radius: 50%;
  display: inline-block;
  transition: background-color 0.6s ease;
}

.active, .dot:hover {
  background-color: #717171;
}

/* Fading animation */
.fade {
  -webkit-animation-name: fade;
  -webkit-animation-duration: 1.5s;
  animation-name: fade;
  animation-duration: 1.5s;
}

-webkit-keyframes fade {
  from {opacity: .4} 
  to {opacity: 1}
}

keyframes fade {
  from {opacity: .4} 
  to {opacity: 1}
}

/* On smaller screens, decrease text size */
@media only screen and (max-width: 300px) {
  .prev, .next,.text {font-size: 11px}
}






/*Navigate Bar*/
#navbar {
z-index:11;
  overflow: hidden;
  background-color: #f3f3f3;
}

#navbar a {
  float: left;
  display: block;
  color: #222;
  text-align: center;
valign:center;
  padding: 14px 16px;
  text-decoration: none;
  font-size: 17px;
}

#navbar a:hover {
  background-color: #ddd;
  color: black;
}

#navbar a.active {
  background-color: #444444;
  padding:0;
  color: white;
}

.content {
  padding: 16px;
}

.sticky {
  position: fixed;
  top: 0;
  width: 100%;
}

.sticky + .content {
  padding-top: 60px;
}







/*Hover-Overlay*/
.hover-container {
  position: relative;
  width: 100%;
}

.hover-image {
  display: block;
  z-indxe:2;
  width:500px;
	margin-left:auto;
	margin-right:auto;
 height:500px;  

  
}

.hover-overlay {
  position: absolute;
  top: 0;
  bottom: 0;
  left: 0;
  right: 0;
  height: 100%;
  width: 100%;
  opacity: 0;
  transition: .5s ease;
  background-color: rgba(0,0,0,.7);
  z-index:9;
}

.hover-container:hover .hover-overlay {
  opacity: 1;
}

.hover-text {
  color: white;
  font-size: 18px;
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  -ms-transform: translate(-50%, -50%);
  text-align: left;
  float:left;
}




/*Title text over image in blocks*/
.title-text{
z-index:5;
position:absolute;
background-color:rgba(0,0,0,0.5);
margin-top:-200px;
height:200px;
width:500px;
color:white;
font-size:26pt;
text-align:center;
valign:bottom;
overflow:auto;
padding-top:0;
}





/*Modal Box*/
/* The Modal (background) */
.modal {
    display: none; /* Hidden by default */
    position: fixed; /* Stay in place */
    z-index: 10; /* Sit on top */
    padding-top: 100px; /* Location of the box */
    left: 0;
    top: 0;
    width: 100%; /* Full width */
    height: 100%; /* Full height */
    overflow: auto; /* Enable scroll if needed */
    background-color: rgb(0,0,0); /* Fallback color */
    background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
}
/* Modal Content */
.modal-content {
    background-color: #666666;
    color: #fefefe;
    margin: auto;
    padding: 20px;
    border: 1px solid #888;
    width: 60%;
}

/* The Close Button */
.close0, .close1 {
    color: #aaaaaa;
    float: right;
    font-size: 28px;
    font-weight: bold;
}
.close0:hover, .close1:hover,
.close0:focus, .close1:focus {
    color: #000;
    text-decoration: none;
    cursor: pointer;
}




/*The "Learn More" button*/
.mybtn{
	width:200px;
	color:#f0f0f0;
	font-size:15pt;
	padding:5px;
	float: center;
	border:2px solid #f0f0f0;
	background-color:rgba(0,0,0,0.5);
	margin-left: 150px;
	margin-right:150px;
	margin-top:350px;
	
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
    <div class="bg">
        <div class="logo-container">
            <img class="logo-img" style="padding-top: 48px;" alt="" src="Images/SavvyPackProject.png" />
        </div>
        <div id="navbar">
             <a class="active" onclick="window.close();">
                <img src="Images/SavvyPack.png" alt="" style="width: 180px" /></a> <a href="http://www.savvypack.com/AnalyticalService.aspx">
                    SavvyPack&reg Analytical Service</a> <a href="http://www.savvypack.com/Structure%20Assistant.aspx">
                        SavvyPack&reg Structure Assistant</a> <a href="javascript:void(0)">SavvyPack&reg Project&#8482
                        </a>
        </div>
    </div>
    <div class="flex-container">
        <!--What is it-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/tablet1.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>
                        What is it?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <img src="Images/multi.jpg" alt="" class="inblock-img" style="margin-top: -30px;" />
                        <p>
                            SavvyPack&reg Project&#8482 is a multi-platform app for managing your SavvyPack&reg
                            projects. Use it to initiate projects with your analyst, check on the status of
                            your projects, download project results, and more.</p>
                    </div>
                    <asp:Button ID="myBtn2" class="mybtn" Style="margin-top: 435px;" runat="server" Text=" Take me there"
                        CssClass="mybtn" OnClientClick="return index();"></asp:Button>
                </div>
            </div>
        </div>
        <!--How does it work-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/cogwheel.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>
                        How does it work?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <img class="inblock-img" alt="" src="Images/project.jpg" />
                        <p>SavvyPack&reg Project&#8482 consists a set of web app and mobile app, allowing you to engage in your projects at any time and from wherever you are. 
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!--What does it provide-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/hands.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>
                        Why should I get it?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <p style="margin-top: -50px;">
                            SavvyPack&reg Project&#8482 provides a concise but comprehensive repository for
                            all your SavvyPack&reg subscription service or individual consulting projects.
                        </p>
                    </div>
                    <button id="myBtn0" class="mybtn">
                        More details</button>
                </div>
            </div>
        </div>
        <!-- The Modal -->
        <div id="myModal0" class="modal">
            <!-- Modal content -->
            <div class="modal-content">
                <span class="close0">&times;</span>
                <table style="width: 600px; margin-left: auto; margin-right: auto;">
                    <tr>
                        <td style="width: 600px;">
                            Capabilities include:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: left;">
                            <p>
                                Initiate projects with a SavvyPack&reg analyst
                            </p>
                        </td>
                        <td style="width: 300px;">
                            <img alt="" src="Images/cellphone.jpg" style="width: 300px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: left;">
                            <p>
                                Establish project timeframe requirements</p>
                        </td>
                        <td style="width: 300px;">
                            <img alt="" src="Images/planning-session.jpg" style="width: 300px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: left;">
                            <p>
                                Upload project templates and data files</p>
                        </td>
                        <td style="width: 300px;">
                            <img alt="" src="Images/shelf.jpg" style="width: 300px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: left;">
                            <p>
                                Download project deliverables</p>
                        </td>
                        <td style="width: 300px;">
                            <img alt="" src="Images/result.jpg" style="width: 300px;" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 300px; text-align: left;">
                            <p>
                                View other projects sponsored by your organization</p>
                        </td>
                        <td style="width: 300px;">
                            <img alt="" src="Images/laptop.jpg" style="width: 300px;" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <!--How much I need to pay-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/cash.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>
                        How much do I need to pay?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <img class="inblock-img" alt="" src="Images/creditcard.jpg" />
                        <p>
                            SavvyPack&reg Project&#8482 is free to use. All you need is a reliable email to
                            register an account with us. Again, it is all free!
                        </p>
                    </div>
                    <asp:Button ID="myBtn3" class="mybtn" Style="margin-top: 420px;" runat="server" Text=" Try it now"
                        CssClass="mybtn" OnClientClick="return index();"></asp:Button>
                </div>
            </div>
        </div>
        <!--What does it look like?-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/screenshot.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>
                        What does it look like?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <!-- Trigger/Open The Modal -->
                        <p style="margin-top: -50px;">
                            SavvyPack&reg Project&#8482 mainly consists of two modules: Project Manager for
                            managing projects and Model Grid for specifying inputs for projects.</p>
                    </div>
                    <button id="myBtn1" class="mybtn">
                        More Details</button>
                </div>
            </div>
        </div>
        <!-- The Modal -->
        <div id="myModal1" class="modal">
            <!-- Modal content -->
            <div class="modal-content">
                <span class="close1">&times;</span>
                <div class="slideshow-container">
                    <div class="mySlides fade">
                        <div class="numbertext">
                            1 / 4</div>
                        <img alt="" src="Images/ProjectManager.png" style="width: 100%" />
                        <div class="text">
                            Project Manager</div>
                    </div>
                    <div class="mySlides fade">
                        <div class="numbertext">
                            2 / 4</div>
                        <img alt="" src="Images/ProjectSummary.png" style="width: 100%" />
                        <div class="text">
                            Project Summary</div>
                    </div>
                    <div class="mySlides fade">
                        <div class="numbertext">
                            3 / 4</div>
                        <img alt="" src="Images/StructureDetails.png" style="width: 100%" />
                        <div class="text">
                            Structure Details</div>
                    </div>
                    <div class="mySlides fade">
                        <div class="numbertext">
                            4 / 4</div>
                        <img alt="" src="Images/ModelGrid.png" style="width: 100%" />
                        <div class="text">
                            Model Grid</div>
                    </div>
                    <a class="prev" onclick="plusSlides(-1)">&#10094;</a> <a class="next" onclick="plusSlides(1)">
                        &#10095;</a>
                </div>
                <br />
                <div style="text-align: center">
                    <span class="dot" onclick="currentSlide(1)"></span><span class="dot" onclick="currentSlide(2)">
                    </span><span class="dot" onclick="currentSlide(3)"></span><span class="dot" onclick="currentSlide(4)">
                    </span>
                </div>
            </div>
        </div>
        <!--not a current user-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/screen.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>
                        Not a current SavvyPack&reg subscriber?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <img alt="" class="inblock-img" src="Images/student.jpg" />
                        <p>
                            You can still benefit from SavvyPack&reg Project. Use it to upload your project
                            scope and request a quote. From there, if the project is iniated, you can use SavvyPack&reg
                            Project to engage with your project analyst, check on the status of your project,
                            download project results and deliverables and more.
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <!--Contact-->
        <div class="flexbox">
            <div class="hover-container">
                <img src="Images/tele.jpg" alt="Avatar" class="hover-image" />
                <div class="title-text">
                    <p>Schedule a demo?</p>
                </div>
                <div class="hover-overlay">
                    <div class="hover-text">
                        <table>
                            <tr>
                                <td colspan="2">
                                    If you would like more information on SavvyPack&reg Project&#8482, please provide
                                    your name and email address.
                                </td>
                            </tr>
                            <tr style="padding-top: 15px;">
                                <td style="font-size: 15px; padding-right: 5px;" align="right">
                                    <b>First Name:</b>
                                </td>
                                <td style="width: 0px;" align="left">
                                    <asp:TextBox ID="txtFName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 15px; padding-right: 5px;" align="right">
                                    <b>Last Name:</b>
                                </td>
                                <td align="left">
                                     <asp:TextBox ID="txtLName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="font-size: 15px; padding-right: 5px;" align="right">
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
                                </td>
                            </tr>
                        </table>
                        
                        <b>---OR---</b>
                        <p>
                            Call us at
                            <br />
                            [1] 952-405-7500 or
                            <br />
                            email us at
                            <br />
                            sales@savvypack.com</p>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!--Slideshow-clickplay-->
    <script type="text/javascript">
        var slideIndex = 1;
        showSlides(slideIndex);

        function plusSlides(n) {
            showSlides(slideIndex += n);
        }

        function currentSlide(n) {
            showSlides(slideIndex = n);
        }

        function showSlides(n) {
            var i;
            var slides = document.getElementsByClassName("mySlides");
            var dots = document.getElementsByClassName("dot");
            if (n > slides.length) { slideIndex = 1 }
            if (n < 1) { slideIndex = slides.length }
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            for (i = 0; i < dots.length; i++) {
                dots[i].className = dots[i].className.replace(" active", "");
            }
            slides[slideIndex - 1].style.display = "block";
            dots[slideIndex - 1].className += " active";
        }
    </script>
    <!--Navigate Bar-->
    <script type="text/javascript">
        window.onscroll = function () { myFunction() };

        var navbar = document.getElementById("navbar");
        var sticky = navbar.offsetTop;

        function myFunction() {
            if (window.pageYOffset >= sticky) {
                navbar.classList.add("sticky")
            } else {
                navbar.classList.remove("sticky");
            }
        }
    </script>
    <script type="text/javascript">
        function index() {
            window.open("http://www.savvypack.com/");
            return false;
        }
    </script>
    <!--Modal Box for Benefits-->
    <!--Slideshow-clickplay-->
    <script type="text/javascript">
        var slideIndex = 1;
        showSlides(slideIndex);

        function plusSlides(n) {
            showSlides(slideIndex += n);
        }

        function currentSlide(n) {
            showSlides(slideIndex = n);
        }

        function showSlides(n) {
            var i;
            var slides = document.getElementsByClassName("mySlides");
            var dots = document.getElementsByClassName("dot");
            if (n > slides.length) { slideIndex = 1 }
            if (n < 1) { slideIndex = slides.length }
            for (i = 0; i < slides.length; i++) {
                slides[i].style.display = "none";
            }
            for (i = 0; i < dots.length; i++) {
                dots[i].className = dots[i].className.replace(" active", "");
            }
            slides[slideIndex - 1].style.display = "block";
            dots[slideIndex - 1].className += " active";
        }
    </script>
    <!--Navigate Bar-->
    <script type="text/javascript">
        window.onscroll = function () { myFunction() };

        var navbar = document.getElementById("navbar");
        var sticky = navbar.offsetTop;

        function myFunction() {
            if (window.pageYOffset >= sticky) {
                navbar.classList.add("sticky")
            } else {
                navbar.classList.remove("sticky");
            }
        }
    </script>
    <!--Modal Box for Benefits-->
    <script type="text/javascript">
        // Get the modal
        var modal0 = document.getElementById('myModal0');

        // Get the button that opens the modal
        var btn0 = document.getElementById("myBtn0");

        // Get the <span> element that closes the modal
        var span0 = document.getElementsByClassName("close0")[0];

        // When the user clicks the button, open the modal 
        btn0.onclick = function () {
            modal0.style.display = "block";
            return false;
        }

        // When the user clicks on <span> (x), close the modal
        span0.onclick = function () {
            modal0.style.display = "none";
            return false;
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal) {
                modal0.style.display = "none";
                return false;
            }
        }
    </script>
    <!--Modal Box for Screenshots-->
    <script type="text/javascript">
        // Get the modal
        var modal1 = document.getElementById('myModal1');

        // Get the button that opens the modal
        var btn1 = document.getElementById("myBtn1");

        // Get the <span> element that closes the modal
        var span1 = document.getElementsByClassName("close1")[0];

        // When the user clicks the button, open the modal 
        btn1.onclick = function () {
            modal1.style.display = "block";
            return false;
        }

        // When the user clicks on <span> (x), close the modal
        span1.onclick = function () {
            modal1.style.display = "none";
            return false;
        }

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal1) {
                modal1.style.display = "none";
                return false;
            }
        }
    </script>
    <div style="width: 100%;">
        <p style="background-color: #D3DAD0; text-align: center; margin-top: 0px; margin-bottom: 0px;">
            <asp:Label ID="lblTag" runat="Server"></asp:Label>
        </p>
    </div>
    </form>
</body>
</html>
