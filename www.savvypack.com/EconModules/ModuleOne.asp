<%@ Language=VBScript%>
<% response.redirect "http://www.savvypack.com/InteractiveServices/InteractiveServices.aspx"%>
<%'Session("refnumber")= session.sessionID%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title>Economic Analysis Module One</title>
    <style type="text/css">
        A:link
        {
            FONT-WEIGHT: bold;
            FONT-SIZE: 12px;
            COLOR: black;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none
        }
        A:visited
        {
            FONT-WEIGHT: bold;
            FONT-SIZE: 12px;
            COLOR: black;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none
        }
        A:active
        {
            FONT-WEIGHT: bold;
            FONT-SIZE: 12px;
            COLOR: black;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none
        }
        A:hover
        {
            FONT-WEIGHT: bold;
            FONT-SIZE: 12px;
            COLOR: red;
            FONT-FAMILY: Verdana;
            TEXT-DECORATION: none
        }
    </style>
		<csscriptdict import>
			<script type="text/javascript" src="../GeneratedItems/CSScriptLib.js"></script>
		</csscriptdict>
		<csactiondict>
			<script type="text/javascript"><!--
var preloadFlag = false;
function preloadImages() {
	if (document.images) {
		over_order_study_1 = newImage(/*URL*/'../Images/Order2.gif');
		preloadFlag = true;
	}
}

// --></script>
		</csactiondict>
		<script type="text/javascript" src="../GeneratedItems/CSScriptLib.js"></script>
    <script type="text/javascript">
    <!--
var preloadFlag = false;
function preloadImages() {
	if (document.images) {
		over_order_study_1 = newImage(/*URL*/'../Interactive/Images/order_study-2.gif');
		preloadFlag = true;
	}
}

// --></script>
</head>
<body onload="preloadImages();" bgcolor="#336699">
<%'Datbase Conection
Set Conn = server.createobject ("ADODB.connection")
Conn.open Application("ATShopping")


Set RSinv= Server.CreateObject("ADODB.Recordset")
RSinv.ActiveConnection = Conn
RSinv.CursorType = 3
RSinv.Source = "select * from Inventory WHERE partid ='ECONMO1' "
RSinv.Open

 %>
 <div style="font-family:Verdana;font-size:12px; background-color:#FFFFFF; width:810px">
    <table cellpadding="0" cellspacing="0" border="0" style="width:800px;">
        <tr>
            <td>
                <!--Header-->
                <a name="anchor"></a>
                <table>
                    <tr>
				        <td valign="top">
    					    <p><!-- #INCLUDE FILE="../Header/Header.inc" --></p>
				        </td>

					</tr>

                </table>

            </td>

        </tr>
        <tr>
                <td valign="top">

                     <table style="width:95%;height:100%">
                        <tr>
                            <td style="width:30%;" valign="top">
                                 <!--Left Menu-->
                                 <!-- #INCLUDE FILE="../Header/LeftMenu.inc" -->
                             </td>
                            <td style="width:70%;" valign="top">
                                <!--Right Content-->
                                <div style="margin-top:5px; text-align:left">

                                    <div>
											<font size="+2" color="#825f05" face="Optima"><b><i>SavvyPack&reg; </i>Economic Analysis System</b></font></div>
										<div style="margin-top:5px;">
											(patent pending)</div>
										<div style="margin-top:15px;font-size:12px;text-align:justify">
											<font size="+1"><b>Module One - the package manufacturing module</b></font></div>
										<div style="margin-top:15px;font-size:12px;text-align:justify">
											<p>The packaging manufacturing module is a combination of software and content delivered to you through the Internet. It provides a broad array of economic analysis capabilities, which can be used to analyze the economics of the packaging production industry.</p>
											<p></p>
											<p>To learn about the system, we recommend you request a demonstration. Please call us at 952-405-7500 or email us at sales@savvypack.com. We welcome the opportunity to conduct a 30 minute web conference for you, which will provide a full understanding of what the system does and how it will benefit you and your company.</p>
											<p></p>
											<p>Two annual subscription levels are offered for the SavvyPack<sup>&reg;</sup> Economic Analysis System. Our Read Only subscription provides access to many features of the SavvyPack<sup>&reg;</sup> System at a low cost entry point. Our Premium subscription provides access to all features . A comparison is provided below:</p>
											<p><a href="http://www.savvypack.com/Email/SubscriptionTypes.gif"><img src="../Email/SubscriptionTypes.gif" alt="" height="225" width="509" border="0" livesrc="../Email/SubscriptionTypes.gif"></a></p>
											<p>You can also use the following links to obtain documents that explain the features and benefits of the system.</p>
											<p>Benefits for specific functions:</p>
											<p><a href="../EconMarketing/SavvyPack_Designers.pdf" target="_blank">Design and development professionals</a></p>
											<p><a href="../EconMarketing/SavvyPack_Operations.pdf" target="_blank">Operations professionals</a></p>
											<p><a href="../EconMarketing/SavvyPack_Procurement.pdf" target="_blank">Purchasing and procurement professionals</a></p>
											<p><a href="../EconMarketing/SavvyPack_SalesMarketing.pdf" target="_blank">Sales and Marketing professionals</a></p>
										</div>
										<div style="margin-top:15px;font-size:12px;text-align:right">
											<img src="../Images/ArrowRight.jpg" alt="" height="9" width="18" border="0"><font size="-2" color="#3a0bff" face="Arial,Helvetica,Geneva,Swiss,SunSans-Regular"><b><a href="../Interactive/InteractiveServices.asp" style="text-decoration: none">Back to Interactive List</a></b></font></div>
                                    <div style="margin-top:15px;"><b style="font-size:12px;color:#ff7b3c">Features</b></div>
                                    <div style="margin-top:15px;">
                                    <table width="100%" border="0">
                                        <tr>
                                            <td style="width: 500px">
                                                 <table cellpadding="5" width="100%">
                                                     <tr><td ><b><font size="2" color="#4e5f28"><img src="../Images/box.jpg" alt="" height="8" width="14" border="0">Publication date: <%'---
response.write RSinv("PUBdate")

%></td>

                                                     </tr>
                                                     <tr>
                                                        <td style="width: 315px"><img src="../Images/box.jpg" alt="" height="8" width="14" border="0"><b><font size="2" color="#4e5f28">Subscription number: <%'---
response.write RSinv("PartID")

%></td>


                                                     </tr>
                                                     <tr><td style="width: 315px"><img src="../Images/box.jpg" alt="" height="7" width="13" border="0"><font size="2" color="#4e5f28"><b>Copyright - <%'---
response.write RSinv("copyright")

%></td>
                                                     </tr>
                                                     <tr><td nowrap><font size="2"><img src="../Images/box.jpg" alt="" height="8" width="14" border="0"></font><%'---
WHILE NOT RSinv.EOF
IF RSinv("delFORMAT")="First Annual Premium Subscription" THEN
response.write "<font size='2' color='#4e5f28'><b>"&RSinv("delFORMAT")&"</b></font>"
END IF
RSinv.movenext
WEND
RSinv.movefirst

%><font size="2"><b><font color="#4e5f28"> - </b>US$<%'---
WHILE NOT RSinv.EOF
IF RSinv("delFORMAT")="First Annual Premium Subscription" THEN
response.write Formatnumber(RSinv("PRICE"),0)
END IF
RSinv.movenext
WEND
RSinv.movefirst

%></td>
                                                     </tr>
                                                     <tr><td nowrap><font size="2"><img src="../Images/box.jpg" alt="" height="8" width="14" border="0"></font><%'---
WHILE NOT RSinv.EOF
IF RSinv("delFORMAT")="Each Additional Premium Subscription" THEN
response.write "<font size='2' color='#4e5f28'><b>"&RSinv("delFORMAT")&"</b></font>"
END IF
RSinv.movenext
WEND
RSinv.movefirst

%><font size="2"><b><font color="#4e5f28"> - </b> US$<%'---
WHILE NOT RSinv.EOF
IF RSinv("delFORMAT")="Each Additional Premium Subscription" THEN
response.write FormatNumber(RSinv("PRICE"),0)
END IF
RSinv.movenext
WEND
RSinv.movefirst

%></td>
                                                     </tr>
															<tr><td nowrap><img src="../Images/box.jpg" alt="" height="8" width="14" border="0"><%'---
WHILE NOT RSinv.EOF
IF RSinv("delFORMAT")="Annual Read Only Subscription" THEN
response.write "<font size='2' color='#4e5f28'><b>"&RSinv("delFORMAT")&"</b></font>"
END IF
RSinv.movenext
WEND
RSinv.movefirst

%><font size="2"><b><font color="#4e5f28"> - </b> US$<%'---
WHILE NOT RSinv.EOF
IF RSinv("delFORMAT")="Annual Read Only Subscription" THEN
response.write FormatNumber(RSinv("PRICE"),0)
END IF
RSinv.movenext
WEND
RSinv.movefirst

%></td>
                                                     </tr>
														</table>
                                            </td>
                                            <td valign="top">

                                                <div style="margin-left:0px;margin-top:20px;">
															<a title="Order Module One" onmouseover="changeImages( /*CMP*/'order_study_1',/*URL*/'../Images/Order2.gif');return true" onmouseout="changeImages( /*CMP*/'order_study_1',/*URL*/'../Images/Order.gif');return true" href="../NewShoppingCart/Order.asp?Id=ECONMO1" target="_blank"><img src="../Images/Order.gif" alt="" name="order_study_1" height="35" width="97" border="0"></a>
</div>

                                            </td>
                                        </tr>
                                    </table>


                                    </div>
                                    <div style="margin-top:15px;"><b style="font-size:12px;color:#ff7b3c">Questions?</b></div>
                                    <div style="margin-top:15px;font-size:12px;text-align:justify">
                                        Call us at 952-405-7500 or
                                        <br />
                                        email us at <a href="mailto:sales@savvypack.com" style="text-decoration: none">sales@savvypack.com</a>
                                    </div>
                                    <div style="margin-top:15px;font-size:12px;text-align:center">
											<img src="../Images/ArrowRight.jpg" alt="" height="9" width="18" border="0"><font size="-2" color="#3a0bff" face="Arial,Helvetica,Geneva,Swiss,SunSans-Regular"><b><a href="../Interactive/InteractiveServices.asp" style="text-decoration: none">Back to Interactive List</a></b></font></div>
                                    <div style="margin-top:15px;font-size:12px;text-align:center">
                                        <a href="#anchor"style="text-decoration: none;font-size:9px;"><b>Top of Page</b></a>
                                    </div>
                                </div>

                            </td>
                        </tr>

                    </table>

                </td>

            </tr>
        <tr>
            <td>
              <!--Bottom-->
              <!-- #INCLUDE FILE="../Header/Footer.inc" -->

            </td>

        </tr>
 </table>
</div>

</body>
</html>
