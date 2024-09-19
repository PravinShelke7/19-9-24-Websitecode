<%@ Page Language="VB" AutoEventWireup="false" CodeFile="transferLicense.aspx.vb"
    Inherits="Popup_transferLicense" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Transfer License</title>
    <meta name="robots" content="noindex,nofollow" />
    <link href="../../App_Themes/SkinFile/AlliedAdmin.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Close() {

            window.opener.document.getElementById('ctl00_ContractContentPlaceHolder_btnRefresh').click();
        
            window.close();
        }
        function closeWindow() {
            window.opener.document.getElementById('ctl00_ContractContentPlaceHolder_btnRefresh').click();
            window.close();

        }
        function cmessageORG() {
            var msg = "You are going to transfer Contract Packager service to this User. Do you want to proceed?"
            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

        }

        function cmessage() {
           
              var Tosuer = document.getElementById('<%= lblToUser.ClientID%>').innerHTML;
            var e = document.getElementById("ddlFromUser");
            var FromUser = e.options[e.selectedIndex].text;
            var licAdm = document.getElementById("hidAdminLic").value;
            if (licAdm == "Y") {
                alert("You can not transfer Contract Packager Knowledgebase privileges from License Administrator user. Please select different User.");
                return false;
            }
            else {
                if (confirm('You are going to transfer Contract Packager Knowledgebase privileges from user ' + FromUser + ' to user ' + Tosuer + '. Only one transfer is allowed per license per year. Do you want to proceed?')) {
                    document.getElementById('<%= btnTransfer.ClientID%>');
                    return true;
                }
                else {
                    return false;
                }
            }
        }
    </script>
</head>
<body style="background-color: #F1F1F2;">
    <form id="form1" runat="server">
    <asp:HiddenField ID="hidMessage" runat="server" />
    <asp:HiddenField ID="hidOptype" runat="server" />
    <table style="width: 510px;">
     
        <tr>
            <td style="padding-left: 30px;">
                <div id="msg1" runat="server">
                    <table cellpadding="4" cellspacing="2">
                       <tr class="AlterNateColor4">
                        <td style="font-size: 14px; font-family:Verdana; padding-left:120px;font-weight:bold;" colspan="2">
                            Transfer License
                        </td>
                    </tr>
                        <tr>
                            <td style="height: 25px;" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; font-family: Verdana; font-weight: bold; width: 20%;
                                text-align: right;">
                                From User:
                            </td>
                            <td style="font-size: 12px; font-family: Verdana; font-weight: bold; width: 80%">
                                <asp:DropDownList ID="ddlFromUser" runat="server" Style="font-size: 12px; font-family: Verdana;
                                    font-weight: normal; width: 300px; height: 17px;" CssClass="DropDown" AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 12px; font-family: Verdana; font-weight: bold; width: 20%;
                                text-align: right;">
                                To User:
                            </td>
                            <td style="font-size: 12px; font-family: Verdana; font-weight: normal; width: 80%;">
                                <asp:Label ID="lblToUser" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 25px;" colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 20%; text-align: right;">
                            </td>
                            <td style="width: 80%; text-align: left;">
                                <asp:Button ID="btnTransfer" Text="Transfer" runat="server" OnClientClick="return cmessage();" />
                                <asp:Button ID="btnCancel" Text="Cancel" runat="server" OnClientClick="return closeWindow();" />
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidAdminLic" runat="server" />
    </form>
</body>
</html>
