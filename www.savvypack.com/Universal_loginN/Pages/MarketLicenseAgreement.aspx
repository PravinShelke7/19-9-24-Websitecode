<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MarketLicenseAgreement.aspx.vb" Inherits="Universal_loginN_Pages_MarketLicenseAgreement" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>License</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            font-size: large;
            font-weight: bold;
        }
    </style>    
</head>
<body style="margin-top: 5px">
    <form id="form1" runat="server">
    <div id="MasterContent">
       <%-- <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>--%>
        <div id="AlliedLogo">
                      <table>
            <tr>
                <td class="PageSHeading" align="center">
                    <table style="width: 845px; background-color: #edf0f4;">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
            </table>
        </div>

        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <table class="ContentPage" id="ContentPage" runat="server" style="margin-top: 1px;">
        <tr>
            <td>
                <table width="838" border="0" cellspacing="2" cellpadding="0" style="border-collapse: collapse;
                    vertical-align: top;">
                    <tr>
                        <td>
                            <div style="text-align: center">
                                <h1 style="color: red">
                                    SavvyPack Corporation</h1>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="text-align: left">
                                <h2>
                                    ON-LINE PACKAGING ANALYSIS SYSTEM AGREEMENT
                                    <br />
                                    <br />
                                </h2>
                                <h3>
                                    READ THE TERMS OF THIS USER AGREEMENT CAREFULLY BEFORE BEGINNING TO USE THE PACKAGING
                                    ANALYSIS SYSTEM OWNED BY SavvyPack Corporation BY USING THE PACKAGING ANALYSIS
                                    SYSTEM, YOU AGREE TO THE TERMS OF THIS AGREEMENT. INDICATE YOUR ACCEPTANCE OF THESE
                                    TERMS BY SELECTING THE &quot;ACCEPT&quot; BUTTON AT THE END OF THIS AGREEMENT. IF
                                    YOU DO NOT AGREE TO ALL THESE TERMS, SELECT THE &quot;DECLINE&quot; BUTTON AT THE
                                    END OF THIS AGREEMENT.<br />
                                    <br />
                                </h3>
                                <p>
                                    <font size="4">1. <b>You, the User</b>, agree not to share your ID and password, which
                                        provides you access to SavvyPack Corporation&#x2019;s Packaging Analysis
                                        system, with any other person, including other employees at your place of employment
                                        (User Company).</font><font size="+2"><br />
                                        </font>
                                    <br />
                                </p>
                                <font size="4">2. <b>LICENSE TO USE.</b> SavvyPack Corporation grants User a 
                                non-exclusive and non-transferable license or licenses for internal use only at 
                                User Company of the software, content, documentation, and modifications provided 
                                by SavvyPack Corporation (collectively &quot;Packaging Analysis System&quot;), 
                                commensurate with the number of licenses for which the corresponding fee has 
                                been paid. Internal use only means that the User agrees that the Packaging Analysis
                                    System and its results may not be sold, nor passed on, communicated or disseminated
                                    in any form, nor access granted to any third party, including but not limited to
                                    suppliers, agents, partners, in other ventures, accountants, solicitors, bankers,
                                    brokers, licensees, or to any subsidiary, associated company, or holding company
                                    of the User Company, whether trading or non-trading, or to any entity trading under
                                    the same umbrella trading name where the equity interest is different in any way
                                    to that of the User Company without written approval from SavvyPack Corporation</font><br />
                                <br />
                                <font size="4">3. <b>RESTRICTIONS. </b>The Packaging Analysis System is a proprietary,
                                    copyrighted, and patent pending system owned by SavvyPack Corporation SavvyPack Corporation
                                    retains ownership of the Packaging Analysis System and all associated
                                    intellectual property rights. Except as specifically authorized by SavvyPack Corporation,
                                    User may not make copies of software, content, or any part of the Packaging
                                    Analysis System. No right, title or interest in or to any trademark, service mark,
                                    logo or trade name of SavvyPack Corporation or its licensors is granted under
                                    this Agreement.<br />
                                </font>
                                <br />
                                <font size="4">4. <b>DISCLAIMER OF WARRANTY.</b> UNLESS SPECIFIED IN THIS AGREEMENT,
                                    ALL EXPRESS OR IMPLIED CONDITIONS, REPRESENTATIONS AND WARRANTIES, INCLUDING ANY
                                    IMPLIED WARRANTY OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR NON-INFRINGEMENT
                                    ARE DISCLAIMED, EXCEPT TO THE EXTENT THAT THESE DISCLAIMERS ARE HELD TO BE LEGALLY
                                    INVALID.<br />
                                    <br />
                                    5. <b>LIMITATION OF LIABILITY.</b> TO THE EXTENT NOT PROHIBITED BY LAW, IN NO EVENT
                                    WILL SavvyPack Corporation OR ITS LICENSORS BE LIABLE FOR ANY LOST REVENUE, PROFIT
                                    OR DATA, OR FOR SPECIAL, INDIRECT, CONSEQUENTIAL, INCIDENTAL OR PUNITIVE DAMAGES,
                                    HOWEVER CAUSED REGARDLESS OF THE THEORY OF LIABILITY, ARISING OUT OF OR RELATED
                                    TO THE USE OF OR INABILITY TO USE THE PACKAGING ANALYSIS SYSTEM, EVEN IF SavvyPack Corporation
                                    HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. In no event
                                    will SavvyPack Corporation's liability to User and User Company, whether in contract,
                                    tort (including negligence), or otherwise, exceed the amount paid by User Company
                                    for use of the Packaging Analysis System under this Agreement.<br />
                                    <br />
                                    6. <b>TERMINATION.</b> This Agreement is effective until terminated. This Agreement
                                    will terminate immediately without notice from SavvyPack Corporation if User
                                    or User Company fails to comply with any provision of this Agreement.<br />
                                    <br />
                                    7. 
                                </font><span class="style2">GOVERNING LAW</span><font size="4">. Any action related to this Agreement will be governed by Minnesota,
                                    U.S.A. law and controlling U.S. federal law. No choice of law rules of any jurisdiction
                                    will apply.<br />
                                    <br />
                                    8. 
                                </font><span class="style2">SEVERABILITY</span><font size="4">. If any provision of this Agreement is held to be unenforceable,
                                    this Agreement will remain in effect with the provision omitted, unless omission
                                    would frustrate the intent of the parties, in which case this Agreement will immediately
                                    terminate.<br />
                                    <br />
                                    9.<b> INTEGRATION.</b> This Agreement is the entire agreement between User, User
                                    Company, and SavvyPack Corporation relating to its subject matter. It supersedes
                                    all prior or contemporaneous oral or written communications, proposals, representations
                                    and warranties and prevails over any conflicting or additional terms of any quote,
                                    order, acknowledgment, or other communication between the parties relating to its
                                    subject matter during the term of this Agreement. No modification of this Agreement
                                    will be binding, unless in writing and signed by an authorized representative of
                                    each party.<br />
                                    <br />
                                    10. <b>NOTICE OF AUTOMATIC UPDATES FROM SavvyPack Corporation</b> User acknowledges
                                    that SavvyPack Corporation will automatically implement software improvements
                                    and content updates, which may require User to accept updated terms and conditions
                                    for installation. If additional terms and conditions are not presented on implementation,
                                    the software improvements and content updates will be considered part of the Packaging
                                    Analysis System and subject to the terms and conditions of the Agreement.<br />
                                    <br />
                                    For inquiries please contact: SavvyPack Corporation. Burnsville, MN 55337,[1] 952-405-7500</font><p>
                                </p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 25px">
                            <table width="100%" border="0" cellspacing="2" cellpadding="0">
                                <tr>
                                    <td>
                                        <div style="text-align: right">
                                            <asp:Button ID="btnAccept" Text="Accept" runat="server" />
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDecline" Text="Decline" runat="server" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 15px">
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                    </tr>
                    <%--<tr>
					                <td></td>
				                </tr>--%>
                </table>
            </td>
        </tr>
        <tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidTId" runat="server" />   
    </form>
</body>
</html>
