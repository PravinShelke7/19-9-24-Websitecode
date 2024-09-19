<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ForgotPasswordU.aspx.vb"
    Inherits="Users_Login_ForgotPasswordU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Forget Password</title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text" || txtarray[i].type == "password") {
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
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });
    </script>
    <script type="text/javascript" language="javascript">

        function checkValidation() {

            var Email = document.getElementById('<%= txtEmail.ClientID%>').value;

            var errorMsg1 = "";
            var errorMsg = "";
            var space = " ";
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;

            if (Email == "") {
                errorMsg1 += "\Please enter your email address.\n";
            }
            else {
                var address = Email;
            }

            if (errorMsg1 != "") {
                alert(errorMsg1);
                return false;
            }
            else if (reg.test(address) == false) {
                msg = "\Please enter your email address.";
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

        function CloseWindow() {
            alert("Please check your mailbox for your credentials");
            window.close()
        }

        function ShowAccountUpdation(Page, Type) {
            var msg = "Your account information must be completed in order to proceed. Please click ok to proceed to the account information edit screen."
            if (confirm(msg)) {
                var width = screen.width - 20; //  1000;
                var height = screen.height - 20; //  500;
                var left = 0; //  (screen.width - width) / 2;
                var top = 0; //  (screen.height - height) / 2;                
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=yes';
                params += ', menubar=yes';
                params += ', resizable=yes';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';

                newwin = window.open(Page, 'ChatIU', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                }
                else {
                    window.close();
                    if (Type.toString() == "Login") {
                        window.opener.document.getElementById('btnRefresh').click();
                    }
                    return true;
                }

            }
            else {
                return false;
            }

        }

        //function CheckSP(text) {

        //    var a = /\<|\>|\&#|\\/;
        //    var object = document.getElementById(text.id)//get your object
        //    if ((document.getElementById(text.id).value.match(a) != null)) {

        //        alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        return false;
        //    }
        //}

    </script>
</head>
<body style="background-color: #F1F1F2;">
    <form id="form1" runat="server" defaultbutton="btnSubmit">
        <div style="text-align: center">
            <table style="height: 60px; width: 100%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px; text-align: center" colspan="2">
                        <asp:Label ID="lbltitle" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td style="width: 15%; text-align: right;">
                        <asp:Label ID="lblEmail" runat="Server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtEmail" runat="Server" Width="350px" MaxLength="60" Style="font-size: 11px; height: 16px;"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td></td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" Width="90px"
                            OnClientClick="return checkValidation();" />
                        <asp:Button ID="btnCancel" runat="server" Style="margin-left: 6px;" CssClass="Button"
                            Text="Cancel" Width="90px" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidtype" runat="server" />
        </div>
    </form>
</body>
</html>
