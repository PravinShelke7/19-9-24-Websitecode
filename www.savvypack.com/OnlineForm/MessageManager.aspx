<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MessageManager.aspx.vb"
    Inherits="OnlineForm_Popup_EmailAnalyst" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Message Manager</title>
    <%--<link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />--%>
    <script src="../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("3990");
                        });
                    }
                }
            }
        });               
    </script>
    <script language="javascript" type="text/javascript">
        function test() {
            //add the required functionality
            //alert('Hi');
        }

        function OpenLoginPopup() {

            var Page = "http://www.savvypack.com/OnlineForm/LoginS.aspx?Savvy=N";
            var width = 700;
            var height = 300;

            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            return false;

        }
        function OpenLoginPopupSavvy() {

            var Page = "http://www.savvypack.com/OnlineForm/LoginS.aspx?Savvy=Y";
            var width = 700;
            var height = 300;

            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            return false;

        }


        //Send Mail
        function ShowPopWindow6(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 710;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function OpenSavvyPack() {

            window.open("ProjectManager.aspx");
            return false;

        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 420;
            var height = 200;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }
    </script>
    <style type="text/css">
        #PageSectionSM
        {
            background-color: #F1F1F2;
            margin-left: 0px;
            height: 525px;
            width: 100%;
        }
        .PSavvyModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background: url('../Images/SavvyPackProject1268x45.gif');
            height: 45px;
            width: 1268px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        #PAlliedLogo
        {
            margin-top: 5px;
        }
        .AlterNateColor3
        {
            background-color: #D3DAD0;
            height: 20px;
        }
        .PageSHeading
        {
            font-size: 12px;
            font-weight: bold;
        }
        .style1
        {
            width: 131px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <div id="MasterContent">
            <div>
                <table class="PSavvyModule" id="Savvytable" runat="server" cellpadding="0" cellspacing="0"
                    style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px" class="style1">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <%--  <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                                        runat="server" ToolTip="Instructions" Visible="true" />--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="margin-left: 0px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnLogin" runat="server" Width="65px" Text="Login" ForeColor="black"
                                            Font-Bold="true" Style="border-radius: 15px;" Height="29px" Visible="false">
                                        </asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnLogout" runat="server" Width="65px" Text="Logout" ForeColor="black"
                                            Font-Bold="true" Style="border-radius: 15px;" Height="29px" Visible="false">
                                        </asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnSavvy" runat="server" Width="160px" Text="Go to Project Manager"
                                            ForeColor="black" Font-Bold="true" Style="border-radius: 15px; margin-left: 20px;"
                                            Height="29px" Visible="false"></asp:Button>
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
        <div id="ContentPagemargin" style="width: 1270px; margin: 0 0 0 0;">
            <div id="PageSectionSM">
                <table width="1270px" style="height: 100%;">
                    <tr>
                        <td width="500px" style="background-color: #98b4e2;">
                            <div style="margin-top: -275px; height: 100%; position: absolute;">
                                <table>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:TextBox ID="txtSearch" runat="server" Style="text-align: left; vertical-align: middle;"
                                                MaxLength="100" Height="15px" Width="250px" placeholder="Enter text to search"></asp:TextBox>
                                            <asp:ImageButton ID="btnSearch" runat="server" Style="vertical-align: middle;" margin-top="5px"
                                                src="../Images/search.png" Width="20px" Height="18px" ToolTip="Search" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">
                                            <asp:Button ID="btnCreate" runat="server" Text="Create a message" Style="border-radius: 10px;
                                                margin-top: 5px; margin-bottom: 3px; text-align: center;" Height="30px" Width="150px"
                                                Font-Bold="true" ToolTip="Create a message"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <ajaxToolkit:TabContainer ID="tabMessage" Height="445px" runat="server" ActiveTabIndex="0"
                                                AutoPostBack="true">
                                                <ajaxToolkit:TabPanel runat="server" HeaderText="Inbox" ToolTip="Contact details"
                                                    ID="tabInox">
                                                    <HeaderTemplate>
                                                        <div style="float: right; width: 100px; height: 25px; font-size: medium;">
                                                            Inbox</div>
                                                    </HeaderTemplate>
                                                    <%-- Inbox
                                                    </HeaderTemplate>--%>
                                                    <ContentTemplate>
                                                        <div style="height: 445px; overflow: auto; width: 285px; margin-bottom: 15px;">
                                                            <asp:Table runat="server" ID="tbleInbox" Height="30px" Width="250px">
                                                            </asp:Table>
                                                            <asp:Label ID="lblMsg" runat="server" Visible="false" Text="You don't have any message."
                                                                Style="font-weight: bold; text-align: left;"></asp:Label>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                                <ajaxToolkit:TabPanel runat="server" HeaderText="Outbox" ToolTip="Contact details"
                                                    ID="TabOutbox">
                                                    <HeaderTemplate>
                                                        <div style="float: right; width: 100px; height: 25px; font-size: medium;">
                                                            Outbox</div>
                                                    </HeaderTemplate>
                                                    <ContentTemplate>
                                                        <div style="height: 445px; overflow: auto; width: 285px; margin-bottom: 15px;">
                                                            <asp:Table runat="server" ID="tbleOutbox" Height="30px" Width="250px">
                                                            </asp:Table>
                                                            <asp:Label ID="lblOMsg" runat="server" Visible="false" Text="You don't have any message."
                                                                Style="font-weight: bold; text-align: left;"></asp:Label>
                                                        </div>
                                                    </ContentTemplate>
                                                </ajaxToolkit:TabPanel>
                                            </ajaxToolkit:TabContainer>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                    <td style="text-align: left; margin-top: 5px;">
                                        <div style="height: 480px; overflow: auto; width: 288px; margin-bottom: 15px;">
                                            <asp:Table runat="server" ID="tbleInbox" Style="margin-left: 5px; margin-top: 5px;"
                                                Height="30px" Width="200px">
                                            </asp:Table>
                                            <asp:Label ID="lblMsg" runat="server" Visible="false" Text="You don't have any message."
                                                Style="font-weight: bold; text-align: left;"></asp:Label>
                                        </div>
                                    </td>
                                </tr>--%>
                                </table>
                            </div>
                        </td>
                        <td width="970px" style="background-color: #bcd1f2;">
                            <table style="margin-left: 20px; margin-bottom: 10px; margin-top: 0px;">
                                <tr>
                                    <td>
                                        <div style="margin-top: 0px; margin-bottom: 5px;">
                                            <asp:ImageButton ID="imgReply" ImageUrl="../Images/Reply.png" runat="server" ToolTip=""
                                                Height="18px" Width="22px" OnClientClick="" Style="margin-left: 650px" />
                                            <asp:Button ID="btnReply" runat="server" Width="61px" Text="Reply" ForeColor="black"
                                                Font-Bold="true" Style="border-radius: 15px;" Height="29px"></asp:Button>
                                            <asp:Label ID="Label1" runat="server" Width="5px"></asp:Label>
                                            <asp:ImageButton ID="imgForw" ImageUrl="../Images/Forward.png" runat="server" ToolTip=""
                                                Height="16px" Width="20px" OnClientClick="" Style="margin-left: 15px" />
                                            <asp:Button ID="btnForw" Height="29px" runat="server" Style="border-radius: 15px;"
                                                Text="Forward" ForeColor="black" Font-Bold="true"></asp:Button>
                                        </div>
                                        <hr noshade="noshade" style="margin-top: 5px;" />
                                    </td>
                                </tr>
                                <tr style="margin-bottom: 15px;">
                                    <td style="margin-left: 20px; height: 15px;">
                                        <asp:Label ID="lblName" runat="server" Style="vertical-align: text-top;" Font-Bold="true"
                                            Font-Size="14"></asp:Label>
                                        <asp:Label ID="lblDate" runat="server" Style="float: right; margin-bottom: 15px;
                                            height: 15px;" Font-Size="10" Font-Bold="true" ToolTip="Received time"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 20px;">
                                        <asp:Label ID="lblFrom" runat="server" Style="vertical-align: text-top;" Text="From:"
                                            Font-Bold="true" Font-Size="12"></asp:Label>
                                        <asp:Label ID="lblAnalyst" runat="server" Style="vertical-align: text-top;" Font-Size="11"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 20px;">
                                        <asp:Label ID="lblTo" runat="server" Style="vertical-align: text-top;" Text="To:"
                                            Font-Bold="true" Font-Size="12"></asp:Label>
                                        <asp:Label ID="lblTName" runat="server" Style="vertical-align: text-top;" Font-Size="11"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 20px;">
                                        <asp:Label ID="lblSub" runat="server" Style="vertical-align: text-top;" Text="Subject:"
                                            Font-Bold="true" Font-Size="11"></asp:Label>
                                        <asp:Label ID="lblS" runat="server" Text="" Font-Size="10"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 20px;">
                                        <asp:Label ID="lblProject" runat="server" Style="vertical-align: text-top;" Text="Regarding project:"
                                            Font-Bold="true" Font-Size="11"></asp:Label>
                                        <asp:Label ID="lblPrj" runat="server" Text="" Font-Size="10"></asp:Label>
                                        <hr noshade="noshade" style="margin-top: 20px;" />
                                        <asp:Label ID="lblC" runat="server" Style="vertical-align: text-top;" Text="Content:"
                                            Font-Bold="true" Font-Size="12"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="margin-left: 20px;">
                                        <asp:TextBox ID="txtContent" runat="server" Enabled="false" Height="300px" Width="850px"
                                            TextMode="MultiLine" Style="margin-left: 10px; margin-top: 10px; margin-right: 10px;
                                            margin-bottom: 10px; text-align: left;"></asp:TextBox>
                                        <%--<asp:Label runat="server" Font-Size="13" ID="lblContent1" BackColor="#dee1e5" Height="300px"
                                        Width="900px" Style="border-radius: 10px;"></asp:Label>--%>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <%--<tr class="AlterNateColor3" style="vertical-align: middle;">
                <td class="PageSHeading">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>--%>
            </div>
        </div>
        <div style="width: 1268px; margin-top: 40px;">
            <table width="1268px">
                <tr class="AlterNateColor3">
                    <td colspan="2" class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <%-- <div id="PAlliedLogo">
        <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLog_PackagingIntell1268x56.gif"
            runat="server" Style="margin-top: 0px" />
    </div>--%>
        <div id="AlliedLogo">
            <table>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 1268px; background-color: #edf0f4;">
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
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
        <asp:Button ID="btnRefresh1" runat="server" Style="display: none;" />
        <asp:HiddenField ID="hiPrevUserId" runat="server" />
    </center>
    </form>
</body>
</html>
