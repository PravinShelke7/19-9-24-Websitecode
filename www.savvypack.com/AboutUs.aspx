<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="AboutUs.aspx.vb" Inherits="AboutUs" Title="SavvyPack Corporation History" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        
    </script>
	<style type="text/css">
        .HeaderStyleA
        {
            font-size:14px;
            font-family:Verdana;
            font-weight:bold;
        }
    </style>
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 2px; margin-left: 5px; color: #825f05;">
        About Us
    </div>
    <div id="ContentPagemargin1" runat="server" style="vertical-align: top; margin-left: 7px;
        margin-right: 7px;">
        <%--<table cellspacing="0" cellpadding="5">
            <tr>
                <td style="height: 12px">
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: verdana; font-size: 12px;
                    text-align: justify">
                    <span style="font-weight: normal; font-size: small">SavvyPack Corporation began operations
                        in 1995 to fill a void in the packaging consulting industry.&nbsp;&nbsp; The company
                        has expanded rapidly ever since, in lock step with the growing demand for packaging
                        intellgience. </span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: verdana; font-size: 12px;
                    text-align: justify">
                    <span style="font-weight: normal; font-size: small">Through the years, SavvyPack Corporation's
                        products and services have grown from consulting alone to include its own portfolio
                        of publications, package design and analysis, and Internet subscription services.
                    </span>
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    1995 - Began Consulting Operations
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    1998 - Internal development of SavvyPack<sup>®</sup> Packaging Analysis System begins
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    1999 - Consulting and Package Design and Analysis businesses are separated
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    2001 - Publishing business begins
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    2003 - On-line access to research introduced
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    2005 - SavvyPack<sup>®</sup> System opened to client subscription
                </td>
            </tr>
            <tr>
                <td style="height: 15px; font-size: small; color: #000000; font-family: Verdana;">
                    2009 - First geographic expansion for SavvyPack Corporation
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="5">
            <tr>
                <td style="height: 12px">
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: verdana; font-size: 12px;
                    text-align: justify">
                    <span style="font-weight: normal; font-size: small">SavvyPack Corporation now serves over
                        500 clients around the globe.&nbsp; These clients have rewarded SavvyPack Corporation
                        for its commitment to the packaging industry, customer orientation, strategic thinking,
                        and hands on practicality.&nbsp; SavvyPack Corporation personnal are a key part of
                        this success, with decades of packaging experience, analytical, research, and publishing
                        skills.
                        <br />
                        <br />
                        The need for packaging intelligence has grown greatly since 1995, making SavvyPack 
                        Corporation's services even more important today.&nbsp; The future appears very
                        bright for SavvyPack Corporation, and the clients it serves. </span>
                </td>
            </tr>
        </table>--%>
		
		<table cellspacing="0" cellpadding="5" style="font-weight: 500; color: Black; font-family: verdana;
            font-size: 12px; text-align: justify">
            <tr>
                <td style="height: 12px">
                </td>
            </tr>
            <tr>
                <td class="HeaderStyleA">
                    History
                </td>
            </tr>
            <tr>
                <td>
                    Originally established in 1995 as the packaging intelligence division of Allied Development,
                     this division was spun off to form SavvyPack Corporation
                    (SavvyPack) in 2019.
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="5" style="font-weight: 500; color: Black; font-family: verdana;
            font-size: 12px; text-align: justify">
            <tr>
                <td style="height: 12px">
                </td>
            </tr>
            <tr>
                <td class="HeaderStyleA">
                    Who we are
                </td>
            </tr>
            <tr>
                <td>
                    SavvyPack is the packaging industry’s most reliable source for packaging industry
                    intelligence and analytics. We now serve over 750 companies that operate in the
                    packaging industry including the largest and best raw material suppliers, converters,
                    and brand owners.
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="5" style="font-weight: 500; color: Black; font-family: verdana;
            font-size: 12px; text-align: justify">
            <tr>
                <td style="height: 12px">
                </td>
            </tr>
            <tr>
                <td class="HeaderStyleA">
                    What we do
                </td>
            </tr>
            <tr>
                <td>
                    Global Provider of Strategic Packaging Intelligence:
                </td>
            </tr>
            <tr>
                <td>
                    <ul style="list-style-type: disc;">
                        <li>Packaging Market Data & Trends</li>
                        <li>Clean Sheeting & Should Cost Analysis</li>
                        <li>Structural Development Assistance</li>
                        <li>Package Optimization</li>
                        <li>Life Cycle Analysis</li>
                    </ul>
                </td>
            </tr>
        </table>
		
    </div>
    <asp:HiddenField ID="hdnUserLoginPath" runat="server" />
</asp:Content>
