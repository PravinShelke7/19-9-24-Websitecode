<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContLicenseAgr.aspx.vb"
    Inherits="Universal_loginN_Pages_ContLicenseAgr" Title="Contract License" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>License</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">        

        function CloseTab() {
            window.opener.document.getElementById('ctl00_btnOData').click();
            window.close();
            return false;
        }
    
    </script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body style="margin-top: 5px">
    <form id="form1" runat="server">
    <div id="MasterContent">
        <%--<div id="AlliedLogo">
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
                        <td rowspan="28">
                            <div style="text-align: left">
                                <h2>
                                    ON-LINE KNOWLEDGE SYSTEM SERVICE AGREEMENT
                                    <br />
                                    <br />
                                </h2>
                                <h3>
                                    READ THE TERMS OF THIS AGREEMENT AND ANY PROVIDED SUPPLEMENTAL SERVICE TERMS (COLLECTIVELY
                                    &quot;AGREEMENT&quot;) CAREFULLY BEFORE BEGINNING TO USE THE ON-LINE KNOWLEDGE SYSTEM.
                                    BY USING THE ON-LINE KNOWLEDGE SYSTEM, YOU AGREE TO THE TERMS OF THIS AGREEMENT.
                                    INDICATE YOUR ACCEPTANCE OF THESE TERMS BY SELECTING THE &quot;ACCEPT&quot; BUTTON
                                    AT THE END OF THIS AGREEMENT. IF YOU DO NOT AGREE TO ALL THESE TERMS, SELECT THE
                                    &quot;DECLINE&quot; BUTTON AT THE END OF THIS AGREEMENT.<br />
                                    <br />
                                </h3>
                                <p>
                                    <font size="4"><b>1. LICENSE TO USE.</b> SavvyPack Corporation grants Client a non-exclusive
                                        and non-transferable license or licenses for the <u>internal use only</u> of the
                                        software, content, documentation, and modifications provided by SavvyPack Corporation
                                        (collectively &quot;On-line Knowledge System&quot;), commensurate with the
                                        number of users and licenses for which the corresponding fee has been paid. Licenses
                                        for the On-line Knowledge System authorize an unlimited number of users, of which
                                        one at a time can access the system.</font></p>
                                <br />
                                <p>
                                    <font size="4"><u>Internal use only</u> means that the Client agrees that the On-line
                                        Knowledge System and its results may not be sold, nor passed on, communicated or
                                        disseminated in any form, nor access granted to any third pary, including but not
                                        limited to clients, potential clients, suppliers, agents, partners, in other ventures,
                                        accountants, solicitors, bankers, brokers, licensees, or to any subsidiary, associated
                                        company, or holding company of the Client, whether trading or non-trading, or to
                                        any entity trading under the same umbrella trading name where the equity interest
                                        is different in any way to that of the Client.</font></p>
                                <br />
                                <font size="4">
                                    <p>
                                        <b>2. RESTRICTIONS.</b> The On-line Knowledge System is confidential, copyrighted,
                                        and patent pending by SavvyPack Corporation &nbsp;SavvyPack Corporation retains
                                        ownership of the On-line Knowledge System and all associated intellectual property
                                        rights. Except as specifically authorized by SavvyPack Corporation, Client may
                                        not make copies of software, content, or any part of the On-line Knowledge System.
                                        No right, title or interest in or to any trademark, service mark, logo or trade
                                        name of SavvyPack Corporation or its licensors is granted under this Agreement.</p>
                                </font>
                                <br />
                                <br />
                                <font size="4">
                                    <p>
                                        <b>3. DISCLAIMER OF WARRANTY.</b> UNLESS SPECIFIED IN THIS AGREEMENT, ALL EXPRESS
                                        OR IMPLIED CONDITIONS, REPRESENTATIONS AND WARRANTIES, INCLUDING ANY IMPLIED WARRANTY
                                        OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE OR NON-INFRINGEMENT ARE DISCLAIMED,
                                        EXCEPT TO THE EXTENT THAT THESE DISCLAIMERS ARE HELD TO BE LEGALLY INVALID.</p>
                                </font>
                                <br />
                                <br />
                                <font size="4">
                                    <p>
                                        <b>4. LIMITATION OF LIABILITY</b>. TO THE EXTENT NOT PROHIBITED BY LAW, IN NO EVENT
                                        WILL SavvyPack Corporation OR ITS LICENSORS BE LIABLE FOR ANY LOST REVENUE, PROFIT
                                        OR DATA, OR FOR SPECIAL, INDIRECT, CONSEQUENTIAL, INCIDENTAL OR PUNITIVE DAMAGES,
                                        HOWEVER CAUSED REGARDLESS OF THE THEORY OF LIABILITY, ARISING OUT OF OR RELATED
                                        TO THE USE OF OR INABILITY TO USE THE ECONOMIC ANALYSIS SYSTEM, EVEN IF SavvyPack Corporation
                                        HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. In no event will SavvyPack Corporation's
                                        liability to Client, whether in contract, tort (including negligence),
                                        or otherwise, exceed the amount paid by Client for use of the Economic Analysis
                                        System under this Agreement.</p>
                                </font>
                                <br />
                                <font size="4">
                                    <p>
                                        <b>5. Termination.</b> This Agreement is effective until terminated. This Agreement
                                        will terminate immediately without notice from SavvyPack Corporation if Client
                                        fails to comply with any provision of this Agreement.
                                    </p>
                                </font>
                                <br />
                                <font size="4">
                                    <p>
                                        <b>6. Governing Law.</b> Any action related to this Agreement will be governed by
                                        Minnesota, U.S.A. law and controlling U.S. federal law. No choice of law rules of
                                        any jurisdiction will apply.</p>
                                </font>
                                <br />
                                <font size="4">
                                    <p>
                                        <b>7. Severability.</b> If any provision of this Agreement is held to be unenforceable,
                                        this Agreement will remain in effect with the provision omitted, unless omission
                                        would frustrate the intent of the parties, in which case this Agreement will immediately
                                        terminate.</p>
                                </font>
                                <br />
                                <font size="4">
                                    <p>
                                        <b>8. Integration.</b> This Agreement is the entire agreement between Client and
                                        SavvyPack Corporation relating to its subject matter. It supersedes all prior
                                        or contemporaneous oral or written communications, proposals, representations and
                                        warranties and prevails over any conflicting or additional terms of any quote, order,
                                        acknowledgment, or other communication between the parties relating to its subject
                                        matter during the term of this Agreement. No modification of this Agreement will
                                        be binding, unless in writing and signed by an authorized representative of each
                                        party.</p>
                                </font>
                                <br />
                                <font size="4">
                                    <p>
                                        <b>9. Notice of Automatic Updates from SavvyPack Corporation </b>Client acknowledges
                                        that SavvyPack Corporation will automatically implement software improvements
                                        and content updates, which may require Client to accept updated terms and conditions
                                        for installation. If additional terms and conditions are not presented on implementation,
                                        the software improvements and content updates will be considered part of the Economic
                                        Analysis System and subject to the terms and conditions of the Agreement.</p>
                                </font>
                                <br />
                                <br />
                                <font size="3">
                                    <p style="font-style: italic">
                                        For inquiries please contact: SavvyPack Corporation. Burnsville, MN 55337,[1] 952-405-7500</p>
                                </font>
                                <p>
                                </p>
                            </div>
                        </td>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
                    </tr>
                    <tr>
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
