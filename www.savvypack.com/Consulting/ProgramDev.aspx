<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="ProgramDev.aspx.vb" Inherits="Consulting_ProgramDev" Title="Program Development and Implementation" %>

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
        margin-top: 10px; margin-left: 10px; color: #825f05;">
        Program Development and Implementation
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 10px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation&#8217;s expertise
            doesn&#8217;t stop with research. It includes the ability to develop programs to
            commercialize the research, and to implement them. Some of the programs in highest
            demand include: </span>
    </div>
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 15px; margin-left: 10px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Marketing
            Strategy</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation can develop and
            implement a marketing strategy for your company and products. Successfully brand
            and market new and existing products by partnering with SavvyPack Corporation.</span></div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Sales
            Strategy</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation can develop and
            implement a sales strategy for your company and products. Increase sales and improve
            your bottom line with assistance from SavvyPack Corporation.</span></div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Best Path
            to Market </span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 10px; color: #666666;">
        <span style="font-family: Verdana; color: Black">If you want to commercialize your new
            products in the packaging industry in the fastest possible time, your success is
            maximized by SavvyPack Corporation&#8217;s knowledge and experience.</span>
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
