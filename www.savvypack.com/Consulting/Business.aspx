<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="Business.aspx.vb" Inherits="Consulting_Business" Title="Business Research & Forecasting" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function OpenNewWindow(Page) {

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
            newwin = window.open(Page, 'winName', params);

        }
        function BackPage() {
            window.open('Consulting.aspx', '_self');
            return false;
        }
		
		function OpenAccWindow(Page) {

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
            newwin = window.open(Page, '_self');

        }
    </script>
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: right;
        margin-top: 10px; margin-left: 10px; color: #825f05;">
        <asp:Button ID="btnBack" runat="server" Text="Go Back" OnClientClick="return BackPage();" />
    </div>
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 10px; margin-left: 5px; color: #825f05;">
        Business Research & Forecasting
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 10px; margin-left: 5px; color: #666666; color: Black;">
        <span style="font-family: Verdana; color: Black">Known for being the most reliable source
            for packaging industry intelligence, SavvyPack Corporation specializes in primary research
            for business intelligence. Some of its research services that are most in demand
            include:</span></div>
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 15px; margin-left: 5px; color: #666666;">
        <p style="text-decoration: underline; font-family: Verdana; color: Black;">
            Market Data</p>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">With researchers in the United States
            and Asia, SavvyPack Corporation can work around the clock and around the world researching
            the market data you need. This research capability and SavvyPack Corporation&#8217;s
            existing knowledgebase of market information is a powerful combination that benefits
            our clients.</span></div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Competitive
            Assessment </span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation analysts can assess
            companies, products, countries, and many more from a competitive standpoint. Again,
            clients can draw from our experience and extensive existing knowledge.</span>
    </div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">New Product
            Potential</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">Take it to the next level, fully integrating
            market and competitive data with product characteristics to understand the market
            potential of a new or newly enhanced product.</span>
    </div>
    <br />
    <div id="dvCompanyInfo" runat="server" style="vertical-align: top; margin-left: 7px;
        margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td style="height: 25px">
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-weight: bold; color: #825f05; font-family: Verdana; font-size: 14px;">
                        Questions?</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        Call us at 952-405-7500 or</span>
                    <br />
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        email us at <a style="text-decoration: none; font-style: italic; font-weight: bold"
                            class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a></span>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnUserLoginPath" runat="server" />
</asp:Content>
