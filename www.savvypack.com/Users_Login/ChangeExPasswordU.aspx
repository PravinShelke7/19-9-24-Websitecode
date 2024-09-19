<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangeExPasswordU.aspx.vb"
    Inherits="Users_Login_ChangeExPasswordU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Change Password</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href='../App_Themes/SkinFile/AlliedNew.css' rel="stylesheet" type="text/css" />
    <link href="../JavaScripts/_assets/css/jquery.alerts.css" rel="stylesheet" type="text/css"
        media="screen" />
    <meta http-equiv="content-type" content="text/html;charset=iso-8859-1" />
    <meta http-equiv="X-UA-Compatible" content="IE=7" />
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

        .style1 {
            width: 86px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SetParentFocus() {
            // alert('Hi');
            //window.opener.document.getElementById("txtUserName").value = "Sud";
            //window.opener.document.getElementById("txtEmail").focus();
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
        function checkPassword() {
            var currentPwd = document.getElementById('<%= txtCurrentPwd.ClientID%>').value;
            var NewPwd = document.getElementById('<%= txtNewPwd.ClientID%>').value;
            var CnfPwd = document.getElementById('<%= txtConfirmPwd.ClientID%>').value;
            var errorMsg1 = "";
            var errorMsg = "";
            var msg = "";
            var space = " ";

            if (currentPwd == "") {
                errorMsg1 += "\Current Password cannot be blank.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }
            if (NewPwd == "") {
                errorMsg1 += "\Password cannot be blank.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }
            if (NewPwd == currentPwd) {
                errorMsg1 += "\Current Password & New Password should not be same.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }
            else if (NewPwd == CnfPwd) {
                if (document.getElementById('<%= hidSecurityLvl.ClientID%>').value == "50") {
                    fieldvalue = document.getElementById('<%= txtNewPwd.ClientID%>').value;
                    fieldlength = fieldvalue.length;
                    //It must not contain a space
                    if (fieldvalue.indexOf(space) > -1) {
                        errorMsg += "Passwords cannot include a space.\n";
                    }
                    var arrPwd = new Array();
                    arrPwd = NewPwd.split('');
                    var CntDigit = 0;
                    for (var i = 0; i < arrPwd.length; i = i + 1) {
                        if (!isNaN(arrPwd.slice(i, i + 1))) {
                            CntDigit = CntDigit + 1;
                        }
                    }

                    //It must contain at least one number character
                    if (CntDigit < 2) {
                        errorMsg += "\nStrong passwords must include at least two number.\n";
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
            }
            else {
                errorMsg1 = "Password and Confirm Password should match.";
                errorMsg += alert(errorMsg1);
                return false;
            }
        return true;
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
<body>
    <form id="form1" runat="server">
        <div>
            <div id="error" style="width: 600px">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
            <table class="ContentPage" id="ContentPage" runat="server" style="width: 640px;">
                <tr style="height: 20px">
                    <td>
                        <div style="text-align: center;">
                            <center>
                                <table width="500px">
                                    <tr>
                                        <td>
                                            <table class="ContentPage" id="Table1" runat="server" width="620px">

                                                <tr id="rowCngPwd" runat="server" visible="true">
                                                    <td align="center">
                                                        <table width="70%" cellpadding="1px" cellspacing="1px">
                                                            <tr class="AlterNateColor4">
                                                                <td class="PageSHeading" style="font-size: 14px; text-align: center;" colspan="2">
                                                                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                            </tr>
                                                            <tr class="AlterNateColor2" style="width: 100%">
                                                                <td style="width: 30%; text-align: right; height: 20px;">
                                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                                    <asp:Label ID="lblCurrentPwd" runat="server" Text="Current Password :" CssClass="NormalLabel"></asp:Label>
                                                                </td>
                                                                <td style="width: 65%; text-align: left; height: 20px;">
                                                                    <asp:TextBox ID="txtCurrentPwd" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="AlterNateColor1" style="width: 100%">
                                                                <td style="width: 100%; text-align: right; height: 20px;" colspan="2"></td>
                                                            </tr>
                                                            <tr class="AlterNateColor2" style="width: 100%">
                                                                <td style="width: 30%; text-align: right; height: 20px;">
                                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                                    <asp:Label ID="lblNewPwd" runat="server" Text="New Password :" CssClass="NormalLabel"></asp:Label>
                                                                </td>
                                                                <td style="width: 65%; text-align: left; height: 20px;">
                                                                    <asp:TextBox ID="txtNewPwd" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="AlterNateColor1" style="width: 100%">
                                                                <td style="width: 30%; text-align: right; height: 20px;">
                                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                                    <asp:Label ID="lblConfirmPwd" runat="server" Text="Confirm Password :" CssClass="NormalLabel"></asp:Label>
                                                                </td>
                                                                <td style="width: 65%; text-align: left; height: 20px;">
                                                                    <asp:TextBox ID="txtConfirmPwd" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                                        runat="server"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr class="AlterNateColor1">
                                                                <td style="width: 35%; text-align: right; height: 20px;"></td>
                                                                <td style="width: 65%; text-align: left; height: 20px;">
                                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" Style="margin-left: 10px;"
                                                                        OnClientClick="return checkPassword();" />
                                                                    <asp:Button ID="btnCanel" runat="server" CssClass="Button" Text="Cancel" CausesValidation="false" />
                                                                </td>
                                                            </tr>
                                                            <tr class="AlterNateColor2" style="width: 50%">
                                                                <td style="width: 30%; text-align: left;" colspan="2"></td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </td>
                </tr>
                <tr>
                </tr>

            </table>
        </div>
        <asp:HiddenField ID="hidSecurityLvl" runat="server"></asp:HiddenField>
    </form>
</body>
</html>
