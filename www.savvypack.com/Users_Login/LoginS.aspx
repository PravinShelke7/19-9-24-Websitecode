<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LoginS.aspx.vb" Inherits="Users_Login_LoginS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script src="../JavaScripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <%--<script type="text/javascript">
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
    </script>--%>
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
        
        a.LinkFL:link
        {
            color: Black;
            font-family: Optima;
            font-size: 14px;
            text-decoration: none;
        }
        
        a.LinkFL:visited
        {
            color: Black;
            font-family: Optima;
            font-size: 14px;
            text-decoration: none;
        }
        a.LinkFL:hover
        {
            color: Red;
            font-size: 14px;
        }
    </style>
    <script type="text/javascript">
        function checkValidationWithCookies() {
            var x = "Cookies enabled: " + navigator.cookieEnabled;

            document.cookie = "UCOOKIE=UCOOKIE;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 3020 20:47:11 UTC;";
            var x = false;
            if (document.cookie.length != 0) {

                var ca = document.cookie.split(";");
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i].trim();

                    if (c.indexOf("UCOOKIE") == 0) {
                        document.cookie = "UCOOKIE=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 3000 20:47:11 UTC;";
                        x = true;

                    }
                }
            }
            // alert(x);
         var x = true;
            if (x == false) {
                var width = 500;
                var height = 140;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=yes';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                newwin = window.open("../Popup/Popupbox.aspx", 'UManager111', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Your web browser is currently blocking cookies, so please enable cookies in your web browser. If you do not enable cookies, this website will not work properly.");
                }
                return false;


            }
            else {
                var Email = document.getElementById('<%= txtEmail.ClientID%>').value;
                var Pass = document.getElementById('<%= txtPass.ClientID%>').value;


                var errorMsg1 = "";
                var errorMsg = "";
                var space = " ";

                if (Email == "") {
                    errorMsg1 += "\Please enter your email address.\n";
                }
                if (Pass == "") {
                    errorMsg1 += "\Please enter your Password.\n";
                }

                if (errorMsg1 != "") {
                    errorMsg += alert(errorMsg1);
                    return false;
                }
                else {

                    var a = /\<|\>|\&#|\\/;
                    var objEmail = document.getElementById('<%= txtEmail.ClientID%>');
                    var objPass = document.getElementById('<%= txtPass.ClientID%>');
                    if ((Email.match(a) != null)) {

                        alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                        objEmail.focus(); //set focus to prevent jumping
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(new RegExp("<", 'g'), "");
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(new RegExp(">", 'g'), "");
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(/\\/g, '');
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(new RegExp("&#", 'g'), "");
                        objEmail.scrollTop = objEmail.scrollHeight; //scroll to the end to prevent jumping
                        return false;
                    }

                    else if ((Pass.match(a) != null)) {

                        alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                        objPass.focus(); //set focus to prevent jumping
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(new RegExp("<", 'g'), "");
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(new RegExp(">", 'g'), "");
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(/\\/g, '');
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(new RegExp("&#", 'g'), "");
                        objPass.scrollTop = objPass.scrollHeight; //scroll to the end to prevent jumping
                        return false;
                    }
                    else {
                        return true;
                    }

                }
            }

            alert(x);
        }
</script>
    <script type="text/javascript" language="javascript">

        function ShowWindow(flag) {

            if (flag == "pass") {
                document.getElementById('<%=btnSubmit.ClientID %>').focus();
                document.getElementById("<%=txtEmail.ClientID %>").focus();
                newwin = window.open('../Users_Login/ForgotPassword.aspx');
                return false;
            }
            else {
                var width = screen.width - 20; //  1000;
                var height = screen.height - 20; //  500;
                var left = 0; //  (screen.width - width) / 2;
                var top = 0; //  (screen.height - height) / 2;                
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=yes';
                params += ', menubar=yes';
                params += ', resizable=yes';
                params += ', scrollbars=yes';
                params += ', status=no';
                params += ', toolbar=no';

                document.getElementById('<%=btnSubmit.ClientID %>').focus();
                document.getElementById("<%=txtEmail.ClientID %>").focus();
                //newwin = window.open('../Users_Login/AddEditAccount.aspx?Acc=P');
                //var Page = "../Users_Login/AddEditAccount.aspx?Acc=P";
                //window.opener.location.href = Page;
                //window.close();
                window.open("../Users_Login/AddEditAccount.aspx?Acc=P", "myWindow", params);
                return false;
            }

        }
        function checkValidation() {
        

                var Email = document.getElementById('<%= txtEmail.ClientID%>').value;
                var Pass = document.getElementById('<%= txtPass.ClientID%>').value;


                var errorMsg1 = "";
                var errorMsg = "";
                var space = " ";

                if (Email == "") {
                    errorMsg1 += "\Please enter your email address.\n";
                }
                if (Pass == "") {
                    errorMsg1 += "\Please enter your Password.\n";
                }

                if (errorMsg1 != "") {
                    errorMsg += alert(errorMsg1);
                    return false;
                }
                else {

                    var a = /\<|\>|\&#|\\/;
                    var objEmail = document.getElementById('<%= txtEmail.ClientID%>');
                    var objPass = document.getElementById('<%= txtPass.ClientID%>');
                    if ((Email.match(a) != null)) {

                        alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                        objEmail.focus(); //set focus to prevent jumping
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(new RegExp("<", 'g'), "");
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(new RegExp(">", 'g'), "");
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(/\\/g, '');
                        objEmail.value = document.getElementById('<%= txtEmail.ClientID%>').value.replace(new RegExp("&#", 'g'), "");
                        objEmail.scrollTop = objEmail.scrollHeight; //scroll to the end to prevent jumping
                        return false;
                    }

                    else if ((Pass.match(a) != null)) {

                        alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                        objPass.focus(); //set focus to prevent jumping
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(new RegExp("<", 'g'), "");
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(new RegExp(">", 'g'), "");
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(/\\/g, '');
                        objPass.value = document.getElementById('<%= txtPass.ClientID%>').value.replace(new RegExp("&#", 'g'), "");
                        objPass.scrollTop = objPass.scrollHeight; //scroll to the end to prevent jumping
                        return false;
                    }
                    else {
                        return true;
                    }

                }
           
        }
        function closewindow(flag) {

            //window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId').value = document.getElementById('hidUserID').value;
            if (window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId') == null) {

            }
            else {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId').value = document.getElementById('hidUserID').value;
            }
//            if (flag == "Y") {

//                window.opener.document.getElementById('ctl00_hdnAlert').value = "1";
//            }
            //            window.opener.document.getElementById('ctl00_btnRefresh').click();
            if (flag == "Y") {
                window.opener.document.getElementById('ctl00_hdnAlert').value = "1";
            }
            var Serv = document.getElementById('hidServ').value;
            if (Serv == 'Y') {
                window.opener.document.getElementById('ctl00_btnSavvy').click();
            }
            else if (Serv == 'YO') {
                window.opener.document.getElementById('ctl00_btnOData').click();
            }
            else if (Serv == 'YS') {
                window.opener.document.getElementById('ctl00_btnProj').click();
            }
			else if (Serv == 'SC') {
                window.opener.document.getElementById('ctl00_btnShopCart').click();
            }
			else if (Serv == 'AI') {
                window.opener.document.getElementById('ctl00_btnAccInfo').click();
            }
            else if (Serv == 'OP') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnOrder').click();
            }
            else if (Serv == 'SP') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnSmple').click();
            }
			else if (Serv == 'YC') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnSubmit').click();
            }
			else if (Serv == 'WB') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnUser').click();
            }
            else {
                window.opener.document.getElementById('ctl00_btnRefresh').click();
            }
            window.close();            
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

        function ShowPopWindowVerf(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 580;
            var height = 200;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'ChatDV', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function CloseWindowP() {
            var uType = document.getElementById('<%= hdnUTYPE.ClientID%>').value;
            if (uType == "1") {
                alert("Thank you for creating your account. Your email address has been verified. Because you used a generic email domain, your account is being reviewed for approval. You will be notified when your account is activiated.");
            }
            else {
                alert("Thank you for updating your account. Your email address has been verified. Because you used a generic email domain, your account is being reviewed for approval, and you will be notified when your account is activiated.");

            }
            //window.close();
        }

        function CloseWindowUA() {
            var uType = document.getElementById('<%= hdnUTYPE.ClientID%>').value;
            if (uType == "1") {
                alert("Thank you for creating your account. Your email address has been verified. Your account is being reviewed for approval, and you will be notified when your account is activiated.");
            }
            else {
                alert("Thank you for completing your account. Your email address has been verified. Your account is being reviewed for approval, and you will be notified when your account is activiated.");

            }
            //window.close();
        }

        function CloseWindowAcp() {
            alert("Thank you for completing your account. You can now use your account.");
            //window.close();
        }

        function CloseWindowCA() {
            alert("Thank you for creating your account. You can now use your account.");
        }

        function ShowAccountUpdation(Page) {
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
				
				newwin = window.open(Page, 'ChatCP', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                }
                else {
                    window.close();
                    return true;
                }               
            }
            else {
                return false;
            }

        }

        function ShowPopWindow() {

            var width = 550;
            var height = 160;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var Page = "../Users_Login/ForgotPasswordU.aspx?Type=Login";
            newwin = window.open(Page, 'ChatFPU', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ShowChangePassword(Page) {

            var width = 680;
            var height = 225;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'ChatCPP', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
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

        function ShowAccCheckAlert(Page) {
            var msg = "We periodically check, and your profile is incomplete. Please select Ok to update your profile, or select Cancel to continue without updating your profile."
            if (confirm(msg)) {                           
                window.opener.location.href = Page;
                window.close();
                return true;
            }
            else {
                if (window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId') == null) {

                }
                else {
                    window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnUserId').value = document.getElementById('hidUserID').value;
                }
                window.opener.document.getElementById('ctl00_btnRefresh').click();
                window.close();
            }

        }

        function ShowPopupFP(Page) {

            var width = 485;
            var height = 355;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'ForgotPwd', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function FPEmailCheck() {
            var Email = document.getElementById('<%= txtEmail.ClientID%>').value;

            if (Email == "") {
                alert("Please enter your email address.");
                return false;
            }
            else {
                return true;
            }
        }

    </script>
</head>
<body style="background-color: #F1F1F2;">
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table style="width: 100%">
            <tr>
                <td class="PageSHeading" style="height: 15px;" colspan="2">
                </td>
            </tr>
            <tr class="AlterNateColor4">
                <td class="PageSHeading" style="font-size: 16px;" colspan="2">
                    <asp:Label ID="lbltitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="AlterNateColor1" style="width: 100%">
                <td style="text-align: right; width: 25%" class="style1">
                    <asp:Label ID="lblEmail" runat="Server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                </td>
                <td style="text-align: left; padding-right: 10px;">
                    <asp:TextBox ID="txtEmail" runat="Server" Width="360px" Style="font-size: 11px; height: 16px;"
                        MaxLength="60" ></asp:TextBox>
                    <%-- <asp:RequiredFieldValidator ID="rfvUserName" ControlToValidate="txtEmail" runat="Server" ErrorMessage="*" ValidationGroup="varify">
                    </asp:RequiredFieldValidator>  --%>
                </td>
            </tr>
            <tr class="AlterNateColor2" style="width: 100%">
                <td style="text-align: right;">
                    <asp:Label ID="lblPasswod" runat="Server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                </td>
                <td style="text-align: left; padding-right: 10px;">
                    <asp:TextBox ID="txtPass" runat="Server" TextMode="Password" Width="360px" Style="font-size: 11px;
                        height: 16px;" MaxLength="25" ></asp:TextBox>
                    <%-- <asp:RequiredFieldValidator ID="rfvConfirmUserName" ControlToValidate="txtPass" runat="Server" ValidationGroup="varify" ErrorMessage="*">
                    </asp:RequiredFieldValidator>--%>
                </td>
            </tr>
            <tr class="VerificCode" id="trVCode" runat="server" visible="false">
                <td style="text-align: right;">
                    <asp:Label ID="lblVCode" runat="Server" Text="Verification Code:" CssClass="NormalLabel"></asp:Label>
                </td>
                <td style="text-align: left;">
                    <asp:TextBox ID="txtVerfCode" runat="Server" Width="360px" Style="font-size: 11px;
                        height: 18px;"></asp:TextBox>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Style="margin-left: -20px"
                        Text="Submit" Width="90px" OnClientClick="return checkValidationWithCookies();" />
                     <asp:Button ID="btnCls" runat="server" CssClass="Button" Style="margin-left: 6px"
                        Text="Cancel" Width="90px" />
                </td>
            </tr>
            <tr>
                <td class="PageSHeading" style="height: 15px;" colspan="2">
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td style="width: 55%;" align="left">
                    <table style="margin-left: 35px;">
                        <%-- <tr>
                            <td style="width: 580px; font-weight: bold;" align="left">
                                <asp:LinkButton ID="LinkButton2" runat="Server" CssClass="LinkFL" Text="Change your password"
                                    OnClick="ChangePassword"></asp:LinkButton>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 580px; font-weight: bold;" align="left">
                                <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="LinkFL" Text="Create your account"
                                    OnClientClick="return ShowWindow('Acc');"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 580px; font-weight: bold; padding-top: 10px;" align="left">
                                <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="LinkFL" Text="Forgot password"
                                    OnClientClick="return FPEmailCheck()"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 580px; font-weight: bold; padding-top: 10px;" align="left">
                                <asp:LinkButton ID="LinkButton3" runat="Server" CssClass="LinkFL" Text="Verify your email"
                                    OnClick="VerifyEmail" OnClientClick="return checkValidation();"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 45%;" align="left" valign="bottom">
                    <table style="padding-left: 30px;">
                        <%-- <tr>
                            <td style="width: 360px; font-size: 14px; font-family: Optima; font-weight: bold;"
                                align="left">
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="width: 360px; font-size: 14px; font-family: Optima; font-weight: bold;
                                color: #ff7b3c;" align="left">
                                Questions?
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 360px; font-size: 14px; font-family: Optima; font-weight: bold;
                                padding-top: 5px;" align="left">
                                Call us at [1] 952-405-7500
                            </td>
                        </tr>
                        <tr>
                             <td style="width: 360px; font-size: 14px; font-family: Optima; font-weight: bold;
                                padding-top: 5px;" align="left">
                                Or email us at <a style="text-decoration: none; font-weight: bold; font-size: 14px;"
                                        class="LinkSupp" href="mailto:Support@savvypack.com">Support@savvypack.com</a>
                            </td>                           
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidUserID" runat="server" />
        <asp:HiddenField ID="hdnUTYPE" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
        <asp:Button ID="btnClose" runat="server" Style="display: none;" />
        <asp:Button ID="btnCloseAcp" runat="server" Style="display: none;" />
        <asp:Button ID="btnCloseUA" runat="server" Style="display: none;" />
        <asp:Button ID="btnClosePopUp" runat="server" Style="display: none;" />
        <asp:Button ID="btnCloseCA" runat="server" Style="display: none;" />
        <asp:HiddenField ID="hidServ" runat="server" />
    </div>
    </form>
</body>
</html>
