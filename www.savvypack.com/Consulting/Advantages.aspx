<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="Advantages.aspx.vb" Inherits="Consulting_Advantages" Title="SavvyPack Corporation Advantages" %>

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
        SavvyPack Corporation Advantages</div>
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 15px; margin-left: 5px; color: #666666;">
        <p style="text-decoration: underline; font-family: Verdana; color: Black;">
            Activity</p>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">Packaging is SavvyPack Corporation's total
            focus. Because of this, SavvyPack Corporation works continuously and at a high level
            of activity, communicating, interviewing, and surveying packaging industry participants
            every day of the year. </span>
    </div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Experience</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">Through this activity, along with industry
            experience, SavvyPack Corporation personnel comprise highly experienced researchers
            and consultants. They include a synergistic mix of experience and skills.</span>
    </div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Knowledge</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation provides personnel
            with extensive knowledge specific to the packaging industry. In addition, information
            systems such as SavvyPack Corporation&#8217;s SavvyPack® System, serves as a unique
            knowledge generator and repository. </span>
    </div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Network</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">Due to SavvyPack Corporation&#8217;s long
            history of professional activity in the packaging industry, it has a tremendous
            network within the industry. SavvyPack Corporation utilizes this network to your advantage.
        </span>
    </div>
    <br />
    <div style="height: 18px; width: 97.5%; font-weight: bold; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="text-decoration: underline; font-family: Verdana; color: Black;">Publishing</span>
    </div>
    <div style="margin-left: 50px; width: 97.5%; font-size: 13px; text-align: justify;
        margin-top: 2px; margin-left: 5px; color: #666666;">
        <span style="font-family: Verdana; color: Black">SavvyPack Corporation’s publishing experience
            makes it possible to provide the highest quality consulting project deliverables,
            including clearly written commentary and excellent graphics, integrated into a variety
            of highly professional outputs. </span>
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
