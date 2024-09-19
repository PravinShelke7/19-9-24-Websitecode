<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="Analytical.aspx.vb" Inherits="Consulting_Analytical" Title="Analytical Research" %>

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
            return false;
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
        margin-top: 10px; margin-left: 10px; color: #825f05;">
        Analytical Research
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 10px; margin-left: 10px; color: #666666; color: Black;">
        <span style="font-family: Verdana;">SavvyPack Corporation is highly respected for its analytical
            research capabilities. Some of its most sought after analytical services includes:</span></div>
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 15px; margin-left: 10px; color: #666666;">
        <p style="text-decoration: underline; font-family: Verdana; color: Black;">
            Economic Analysis</p>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation personnel are highly
            skilled economic analysts with extensive experience developing value chain analysis,
            manufacturing cost comparisons, profit margin estimates, and many more. </span>
        <br />
        <br />
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation&#8217;s SavvyPack®
            software and content system is used extensively by company analysts, and it provides
            a unique capability that is of critical importance to this service. </span>
        <br />
        <br />
    </div>
    <div style="height: 15px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Environmental
            Analysis </span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation personnel are highly
            skilled environmental analysts with extensive experience developing cradle-to-gate,
            cradle-to-cradle, and cradle-to-grave life cycle analyses, materials assessments,
            and many more. </span>
        <br />
        <br />
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation&#8217;s SavvyPack®
            software and content system is used extensively by company analysts, and it provides
            a unique capability that is of critical importance to this service. </span>
        <br />
        <br />
    </div>
    <div style="height: 15px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Technology
            Assessment</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">In addition to economic and environmental
            assessments of technology, SavvyPack Corporation can assess feasibility development
            requirements, best path to market, and others. </span>
    </div>
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
