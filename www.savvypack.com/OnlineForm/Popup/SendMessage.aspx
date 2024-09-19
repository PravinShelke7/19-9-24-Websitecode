<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SendMessage.aspx.vb" Inherits="OnlineForm_Popup_SendMail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Send Message</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
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
                            CheckSPMul("4000");
                        });
                    }
                }
            }
        });               
    </script>
    <script language="javascript" type="text/javascript">
        function ClosePopup() {
            //add the required functionality
            alert('Message sent successfully...');
            window.close();
        }

        //Select User
        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 600;
            var height = 460;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ClosePage() {

            window.opener.document.getElementById('btnRefresh1').click();
            window.close();
        }
    </script>
    <style type="text/css">
        #PageSectionSM
        {
            background-color: #bcd1f2;
            margin-left: 5px;
            height: 100%;
        }
        #SavvyContentPagemargin
        {
            margin-left: 1px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="SavvyContentPagemargin" style="width: 690px; margin: 0 0 0 0;">
        <div id="PageSectionSM">
            <table width="100%" style="height: 200px; margin-left: 10px;">
                <tr>
                    <td>
                    </td>
                </tr>
                <tr style="height: 20px; margin-top: 20px;">
                    <td>
                        <asp:Label ID="Label2" Width="102px" runat="server" Font-Bold="True" Text="To:" Font-Size="10pt"></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkTo" runat="server" OnClientClick="return ShowPopWindow('SelectUser.aspx?Id=hidToUser&Des=lnkTo&Des1=hidUserDes'); return false;"
                            Width="150px" Style="color: Black; font-family: Verdana; font-size: 11px; text-decoration: underline;"
                            Text="Select User"></asp:LinkButton>
                        <asp:Label ID="lblTo" runat="server" Font-Size="10" Visible="false"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td>
                        <asp:Label ID="Label6" runat="server" Font-Bold="true" Text="Regarding project:"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkPrjR" runat="server" OnClientClick="return ShowPopWindow('SelectProjTitle.aspx?Id=hidProjectId&Des=lnkPrjR&Des1=hidProjDes'); return false;"
                            Width="150px" Style="color: Black; font-family: Verdana; font-size: 11px; text-decoration: underline;"
                            Text="Select Project"></asp:LinkButton>
                        <asp:Label ID="lblProjR" runat="server" Font-Bold="true" Visible="false" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td>
                        <asp:Label ID="Label1" Width="113px" runat="server" Font-Bold="True" Text="Subject:"
                            Font-Size="10pt"></asp:Label>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSub" runat="server" MaxLength="100" Width="480px" Style="margin-left: 3px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td colspan="2">
                        <asp:Label ID="lblC" runat="server" Style="vertical-align: text-top;" Text="Content:"
                            Font-Bold="true" Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div style="vertical-align: top; margin-left: 30px;">
                            <asp:TextBox runat="server" TextMode="MultiLine" ID="txtContent" BackColor="white"
                                Height="207px" Width="592px"></asp:TextBox>
                            <br />
                            <br />
                            <div style="vertical-align: middle; height: 31px; width: 251px; margin-left: 260px;">
                                <asp:Button ID="btnSend" runat="server" Text="Send" Width="57px" />
                                <asp:Button ID="btnSave" runat="server" Text="Save" Width="57px" Visible="false" />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:HiddenField ID="hidProjDes" runat="server" />
        <asp:HiddenField ID="hidToUser" runat="server" />
        <asp:HiddenField ID="hiFromUser" runat="server" />
        <asp:HiddenField ID="hidUserDes" runat="server" />
        <asp:HiddenField ID="hidMsgType" runat="server" />
        <asp:HiddenField ID="hidMsgId" runat="server" />
        <asp:HiddenField ID="hidReplyId" runat="server" />
        <asp:HiddenField ID="hidForwId" runat="server" />
    </div>
    </form>
</body>
</html>
