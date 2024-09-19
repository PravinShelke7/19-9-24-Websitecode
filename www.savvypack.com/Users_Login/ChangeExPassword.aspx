<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChangeExPassword.aspx.vb"
    Inherits="Users_Login_ChangeExPassword" %>

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
        .VerificCode
        {
            font-family: Verdana;
            background-color: #dfe8ed;
            height: 20px;
            font-family: Optima;
            font-size: 12px;
            color: Black;
        }
        .style1
        {
            width: 86px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function SetParentFocus() {
            // alert('Hi');
            //window.opener.document.getElementById("txtUserName").value = "Sud";
            window.opener.document.getElementById("txtUserName").focus();
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
            msg = "-----------------------------------------------------\n";
            msg += "Please correct the following problem(s).\n";
            msg += "-----------------------------------------------------\n";

            if (currentPwd == "") {
                errorMsg1 += "\Current Password can not be blank.\n";
                //                  msg = "-----------------------------------------------------\n";
                //                  msg += "Please correct the following problem(s).\n";
                //                  msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            if (NewPwd == "") {
                errorMsg1 += "\Password can not be blank.\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            if (NewPwd == currentPwd) {
                errorMsg1 += "\Current Password & New Password should not be same.\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            else if (NewPwd == CnfPwd) {
                if (document.getElementById('<%= hidSecurityLvl.ClientID%>').value == "50") {
                    fieldvalue = document.getElementById('<%= txtNewPwd.ClientID%>').value;
                    fieldlength = fieldvalue.length;
                    //It must not contain a space
                    if (fieldvalue.indexOf(space) > -1) {
                        errorMsg += "\nPasswords cannot include a space.\n";
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
                        errorMsg += alert(errorMsg1 + msg + errorMsg + "\n\n");
                        return false;
                    }
                }
                else {
                    fieldvalue = document.getElementById('<%= txtNewPwd.ClientID%>').value;
                    fieldlength = fieldvalue.length;
                    //It must not contain a space
                    if (fieldvalue.indexOf(space) > -1) {
                        errorMsg += "\nPasswords cannot include a space.\n";
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
                        errorMsg += alert(errorMsg1 + msg + errorMsg + "\n\n");
                        return false;
                    }
                }
            }
            else {
                errorMsg1 = "Password and Confirm Password should match.";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
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
        <div id="MasterContent">
            <div id="AlliedLogo">
                <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                    runat="server" />
            </div>
            <div>
                <table class="ULoginModule1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
                    width: 842px;">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
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
        <table class="ContentPage" id="ContentPage" runat="server" style="width: 840px;">
            <tr>
                <td>
                    <br />
                    <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div style="text-align: center;">
                        <center>
                            <table width="500px">
                                <tr>
                                    <td>
                                        <table class="ContentPage" id="Table1" runat="server" width="710px">
                                            <tr>
                                                <td>
                                                    <asp:HyperLink ID="lnkMyAccount" runat="server" CssClass="Link" Style="text-decoration: underline;
                                                        font-weight: bold; font-size: 12px; margin-left: 10px;" Text="Log In" Visible="false"></asp:HyperLink>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 20px; color: Red; font-size: 14px; vertical-align: top; font-weight: bold;
                                                    font-family: Arial; text-align: center">
                                                    <asp:Label ID="lblMessage" runat="Server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr id="rowCngPwd" runat="server" visible="true">
                                                <td align="center">
                                                    <table width="70%" cellpadding="1px" cellspacing="1px">
                                                        <tr>
                                                            <td align="left" colspan="2">
                                                                <asp:Label ID="Label1" Text="To reset your password, provide your current password."
                                                                    runat="server" CssClass="NormalLabel" Style="text-align: justify"></asp:Label>
                                                                <span style="font-weight: bold;" class="NormalLabel">Note:</span>
                                                                <asp:Label ID="Label2" Text="You can't use your old password once you change it!"
                                                                    Style="text-align: justify" runat="server" CssClass="NormalLabel"></asp:Label>
                                                                <br />
                                                                <br />
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor4">
                                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                                Change Password
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
                                                                <asp:TextBox ID="txtCurrentPwd" TextMode="Password" MaxLength="25" Style="width: 160px;
                                                                    font-size: 11px;" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" style="width: 100%">
                                                            <td style="width: 100%; text-align: right; height: 20px;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor2" style="width: 100%">
                                                            <td style="width: 30%; text-align: right; height: 20px;">
                                                                <span style="color: Red; font-size: 14px;">*</span>
                                                                <asp:Label ID="lblNewPwd" runat="server" Text="New Password :" CssClass="NormalLabel"></asp:Label>
                                                            </td>
                                                            <td style="width: 65%; text-align: left; height: 20px;">
                                                                <asp:TextBox ID="txtNewPwd" TextMode="Password" MaxLength="25" Style="width: 160px;
                                                                    font-size: 11px;" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" style="width: 100%">
                                                            <td style="width: 30%; text-align: right; height: 20px;">
                                                                <span style="color: Red; font-size: 14px;">*</span>
                                                                <asp:Label ID="lblConfirmPwd" runat="server" Text="Confirm Password :" CssClass="NormalLabel"></asp:Label>
                                                            </td>
                                                            <td style="width: 65%; text-align: left; height: 20px;">
                                                                <asp:TextBox ID="txtConfirmPwd" TextMode="Password" MaxLength="25" Style="width: 160px;
                                                                    font-size: 11px;" runat="server"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr class="AlterNateColor1">
                        <td style="width: 100%; " colspan="2" >
                            <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" style="margin-left:-10px;"
                                OnClientClick="return checkPassword();" />
                               <asp:Button ID="btnCanel" runat="server" CssClass="Button" Text="Cancel"  CausesValidation="false"
                                />
                        </td>
                    </tr>--%>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 35%; text-align: right; height: 20px;">
                                                            </td>
                                                            <td style="width: 65%; text-align: left; height: 20px;">
                                                                <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" Style="margin-left: 10px;"
                                                                    OnClientClick="return checkPassword();" />
                                                                <asp:Button ID="btnCanel" runat="server" CssClass="Button" Text="Cancel" CausesValidation="false" />
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor2" style="width: 50%">
                                                            <td style="width: 30%; text-align: left;" colspan="2">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2" style="height: 30px;">
                                                            </td>
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
                <%--<td style="width:100%;"  >
                   <table>
                      <tr>
                         <td style="width:500px;padding-left:310px;" colspan="2" >
                            <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="LinkF" Text="Forget your password?"
                        OnClientClick="return ShowPopWindow('pass');"></asp:LinkButton>
                         </td>
                        
                      </tr>
                      <tr>
                        <td colspan="2" style="height:10px"></td>
                      </tr>
                      <tr>
                         <td style="font-family:Optima;color:Black;font-weight:bold;font-size:14px;width:500px;padding-left:240px;" align="left"   colspan="2"  > New user?
                            <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="LinkF" Text="Create an account"
                        OnClientClick="return ShowPopWindow('Acc');"></asp:LinkButton>
                         </td>
                        
                      </tr>
                      <tr>
                         <td style="height:10px"></td>
                      </tr>
                   </table>
                   
                </td>--%>
            </tr>
            <tr>
                <td class="PageSHeading">
                    <div class="PageHeading" id="div1" style="width: 840px; color: Red; text-align: left">
                        SavvyPack<sup>&reg;</sup>Interactive System
                        <br />
                    </div>
                    <div id="div2" style="width: 840px; color: Black; text-align: left; font-size: 14px;
                        font-family: Optima; margin-top: 5px;">
                        <ul>
                            <asp:Label ID="lbSavvyPack" runat="Server" Text="The SavvyPack® system includes a variety of subscription based services. These services include:"></asp:Label>
                            <li style="margin-top: 5px;">Economic Analysis System </li>
                            <li style="margin-top: 5px;">Environmental Analysis System </li>
                            <li style="margin-top: 5px;">Knowledgebases </li>
                            <li style="margin-top: 5px;">On-line Studies</li>
                        </ul>
                    </div>
                    <%--<div>
<h4>If you are not a member, 
    <a href="../../InteractiveServices/InteractiveServices.aspx">click here</a> to learn more about the system or to purchase a subscription.</h4>
                  </div>--%>
                </td>
            </tr>
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hidSecurityLvl" runat="server"></asp:HiddenField>
    </form>
</body>
</html>
