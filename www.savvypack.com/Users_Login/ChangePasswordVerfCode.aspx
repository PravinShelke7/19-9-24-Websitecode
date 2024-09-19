<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangePasswordVerfCode.aspx.vb"
    Inherits="Users_Login_ChangePasswordVerfCode" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
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
    <style type="text/css">
        .VerificCode {
            font-family: Verdana;
            background-color: #dfe8ed;
            height: 20px;
            font-family: Optima;
            font-size: 12px;
            color: Black;
        }

        .ButtonFP {
            font-family: Verdana;
            font-size: 13px;
            color: blue;
            margin-left: 20px;
        }
    </style>
    <script type="text/javascript" language="javascript">
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

        function checkPassword() {
            var CurrPwd = document.getElementById('<%= hdnCurrentPwd.ClientID %>').value;
            var NewPwd = document.getElementById('<%= txtNewPwd.ClientID%>').value;
            var CnfPwd = document.getElementById('<%= txtConfirmPwd.ClientID%>').value;
            var errorMsg1 = "";
            var errorMsg = "";
            var msg = "";
            var space = " ";

            if (CurrPwd == "") {
                errorMsg1 += "\Server is not able to fetch Current Password.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }

            if (NewPwd == "") {
                errorMsg1 += "\Password cannot be blank.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }

            if (NewPwd == CurrPwd) {
                errorMsg1 += "\Current Password & New Password should not be same.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }

            if (NewPwd == CnfPwd) {
                fieldvalue = document.getElementById('<%= txtNewPwd.ClientID%>').value;
                fieldlength = fieldvalue.length;
                //It must not contain a space
                if (fieldvalue.indexOf(space) > -1) {
                    errorMsg += "Passwords cannot include a space.\n";
                }

                //It must contain at least one number character
                if (!(fieldvalue.match(/\d/))) {
                    errorMsg += "\nStrong passwords must include at least one number.\n";
                }

                //It must contain at least one upper case character     
                if (!(fieldvalue.match(/[A-Z]/))) {
                    errorMsg += "\nStrong passwords must include at least one uppercase letter.\n";
                }
                //It must contain at least one lower case character
                if (!(fieldvalue.match(/[a-z]/))) {
                    errorMsg += "\nStrong passwords must include one or more lowercase letters.\n";
                }

                //It must be at least 7 characters long.
                if (!(fieldlength >= 8)) {
                    errorMsg += "\nStrong passwords must be at least 8 characters long.\n";
                }
                if (fieldvalue.indexOf("'") != -1) {
                    errorMsg += "\Password should not contain apostrophe.\n";
                }
                if (errorMsg != "") {
                    errorMsg += alert(errorMsg1 + errorMsg);
                    return false;
                }
            }
            else {
                errorMsg1 = "Password and Confirm Password should match.";
                errorMsg += alert(errorMsg1);
                return false;
            }
            return true;
        }

        function VerfTxtCheck() {
            var VCode = document.getElementById('<%= txtVerfCode.ClientID%>').value;

            if (VCode == "") {
                alert("Please enter verification code.");
                return false;
            }
            else {
                return true;
            }

        }

    </script>
</head>
<body style="background-color: #F1F1F2;">
    <form id="form1" runat="server" style="margin-right: 20px; margin-left: 20px;">
        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <br />
        <div>
            <table style="font-family: Verdana; vertical-align: middle;">
                <tr>
                    <td>
                        <asp:Label ID="lblVCInfo" runat="server" Width="410px" Text="For your security, we need to authenticate your request. We've sent an OTP to the email. Please enter it below to complete verification."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold; padding-top: 20px;">
                        <asp:Label ID="lblVCode" runat="Server" Font-Size="14px" Text="Verification Code:"
                            CssClass="NormalLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtVerfCode" runat="server" Width="160px" Style="font-size: 11px;">
                        </asp:TextBox>
                        <asp:Button ID="btnSubmit" runat="server" CssClass="ButtonFP" Text="Submit" Height="22px"
                            OnClientClick="return VerfTxtCheck()" />
                    </td>
                </tr>
                <tr class="VerificCode">
                    <td style="color: Red;">*Note: Verification code will only remain valid for 10 minutes.
                    </td>
                </tr>
            </table>
        </div>
        <div id="dvChngePWd" runat="server" style="display: none;">
            <table style="font-family: Verdana; vertical-align: middle; margin-top: 15px;">
                <hr />
                <tr>
                    <td>
                        <asp:Label ID="lblPwdInfo" runat="server" Width="410px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold; padding-top: 20px; height: 20px;">
                        <asp:Label ID="lblNewPwd" runat="server" Font-Size="14px" Text="New Password :" CssClass="NormalLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px;">
                        <asp:TextBox ID="txtNewPwd" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                            runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="font-weight: bold; height: 20px;">
                        <asp:Label ID="lblConfirmPwd" runat="server" Font-Size="14px" Text="Confirm Password :"
                            CssClass="NormalLabel"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px;">
                        <asp:TextBox ID="txtConfirmPwd" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                            runat="server"></asp:TextBox>
                        <asp:Button ID="btnUpdate" runat="server" CssClass="ButtonFP" Text="Update" Height="22px"
                            OnClientClick="return checkPassword();" />
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hdnCurrentPwd" runat="server" />
        </div>
    </form>
</body>
</html>
