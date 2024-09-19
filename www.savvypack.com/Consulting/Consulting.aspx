<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="Consulting.aspx.vb" Inherits="Consulting_Consulting" Title="Consulting" %>

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
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 2px; margin-left: 5px; color: #825f05;">
        Consulting
    </div>
    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table cellspacing="7">
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify;" valign="top">
                    <br />
                    <span style="font-weight: normal; font-family: Verdana; font-size: 13px">Best known
                        for being the most reliable source for packaging industry intelligence, SavvyPack Corporation
                        offers comprehensive consulting services. </span>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="ContentPagemargin1" runat="server" style="vertical-align: top; margin-left: 7px;
        margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <div style="background-color: #58595B; color: White; height: 22px; width: 100%; font-weight: bold;
                        font-size: 16px; text-align: left; margin-top: 2px;">
                        <span style="margin-left: 5px">Analytical Research</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; text-align: justify;" valign="top">
                    <br />
                    <span style="font-family: Verdana; font-size: 13px">SavvyPack Corporation is highly respected
                        for its analytical research capabilities, which include detailed and powerful analytical
                        tools for analyzing packaging economics and environmental impact. </span>
                    <asp:LinkButton ID="lnkAnalytical" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                        ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 0px;"
                        PostBackUrl="~/Consulting/Analytical.aspx">
                    </asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvEconomicAnalysis" runat="server" style="vertical-align: top; margin-left: 7px;
        margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <div style="background-color: #58595B; color: White; height: 22px; width: 100%; font-weight: bold;
                        font-size: 16px; text-align: left; margin-top: 2px;">
                        <span style="margin-left: 5px">Business Research</span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-size: 13px; text-align: justify;"
                    valign="top">
                    <br />
                    <span style="font-family: Verdana; font-size: 13px">SavvyPack Corporation provides highly
                        accurate packaging data and forecasts for market assessments to help companies make
                        informed decisions on their marketing and business strategy. </span>
                    <asp:LinkButton ID="lnkBusiness" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                        ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 2px;"
                        PostBackUrl="~/Consulting/Business.aspx"></asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="dvInteractiveService" runat="server" style="vertical-align: top; margin-left: 7px;
        margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <div style="background-color: #58595B; color: White; height: 22px; width: 100%; font-weight: bold;
                        font-size: 16px; text-align: left; margin-top: 2px;">
                        <span style="margin-left: 5px">Program Development & Implementation </span>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; text-align: justify;" valign="top">
                    <br />
                    <span style="font-family: Verdana; font-size: 13px">SavvyPack Corporation&#8217;s expertise
                        doesn&#8217;t end with research. We can convert research into action by developing
                        and implementing sales, marketing, and technology programs, tailored for client&#8217;s
                        unique needs. </span>
                    <asp:LinkButton ID="lnkProgramDev" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                        ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 2px;"
                        PostBackUrl="~/Consulting/ProgramDev.aspx"></asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    </div>
    <div id="Div1" runat="server" style="vertical-align: top; margin-left: 7px; margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <div style="background-color: #58595B; color: White; height: 22px; width: 100%; font-weight: bold;
                        font-size: 16px; text-align: left; margin-top: 2px;">
                        <span style="margin-left: 5px">SavvyPack Corporation Advantages</span></div>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; text-align: justify;" valign="top">
                    <br />
                    <span style="font-family: Verdana; font-size: 13px">SavvyPack Corporation works solely
                        and extensively in the packaging industry. The company&#8217;s continuous high level
                        of activity, experience, knowledge, network, and publishing provide unmatched packaging
                        consulting capabilities. </span>
                    <asp:LinkButton ID="lnkAdvantages" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                        ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 2px;"
                        PostBackUrl="~/Consulting/Advantages.aspx"></asp:LinkButton>
                    <br />
                    <br />
                </td>
            </tr>
        </table>
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
