<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerificationU.aspx.vb" Inherits="Users_Login_VerificationU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Account verification</title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScripts/jquery-1.4.1.min.js" type="text/javascript"></script>
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
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });
    </script>
    <style type="text/css">
        .VerificCode {
            font-family: Verdana;
            background-color: #dfe8ed;
            height: 20px;
            font-family: Optima;
            font-size: 12px;
            color: Black;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function closewindow(flag) {

            if (window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId') == null) {

            }
            else {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId').value = document.getElementById('hidUserID').value;
            }

            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnCloseAcpU').click();
            //window.open("../Index.aspx", 'NewWindow', "");
            window.close();
        }

        function CloseWindowWithoutLogin() {
            //window.open("../Index.aspx", 'NewWindow', "");           
            //window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnClose').click();
            // alert("Thank you for creating your account. Your account has been verified.");    
            window.close();
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnCloseU').click();

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
                <tr class="VerificCode" id="trVCode" runat="server">
                    <td style="text-align: right;">
                        <asp:Label ID="lblVCode" runat="Server" Text="Verification Code:" CssClass="NormalLabel"></asp:Label>
                    </td>
                    <td style="text-align: left;">
                        <asp:TextBox ID="txtVerfCode" runat="Server" Width="260px" Style="font-size: 11px; height: 16px;"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td></td>
                    <td align="left">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" Width="90px" />
                        <asp:Button ID="btncls" runat="server" CssClass="Button" Text="Cancel" Width="90px" Style="margin-left: 6px;" />
                    </td>
                </tr>
                <tr class="VerificCode">
                    <td class="PageSHeading" style="font-size: 14px; color: Red;" colspan="2">*Note: We emailed you a verification code that will only remain valid for 10 minutes.
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidUserID" runat="server" />
        </div>
    </form>
</body>
</html>
