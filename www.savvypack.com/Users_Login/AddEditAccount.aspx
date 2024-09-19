<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddEditAccount.aspx.vb"
    Inherits="Users_Login_AddEditAccount" MasterPageFile="~/Masters/SavvyPackMenu.master"
    Title="Create Account" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function ShowPopupFP(Page) {

            var width = 470;
            var height = 345;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'ForgotPWDPopUp', params);
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


        function ShowAccountAdd(Page) {
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

            newwin = window.open(Page, 'ChatANU', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            else {
                window.close();
                window.opener.document.getElementById('btnClosePopUp').click();
            }
        }

        function ShowAccountUpdation(Page) {
            var msg = "An account already exists for this email address but the account information is incomplete. Please click ok to complete your account information."
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

                newwin = window.open(Page, 'ChatE1', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Your web browser is currently blocking cookies, so please enable cookies in your web browser. If you do not enable cookies, this website will not work properly.");
                }
                else {
                    window.close();
                    window.opener.document.getElementById('btnClosePopUp').click();
                    return true;
                }
            }
            else {
                return false;
            }

        }

        function ShowPopWindow() {
            document.getElementById('<%=btnSubmit.ClientID %>').focus();
            document.getElementById("<%=txtEmail.ClientID %>").focus();
            newwin = window.open('../Users_Login/ForgotPassword.aspx');
            return false;
        }


        function CloseWindow(Acc) {
            if (Acc.toString() == "Y") {
                window.close();
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnRefresh').click();
            }
            else if (Acc.toString() == "P") {
                window.close();
                window.opener.document.getElementById('btnRefresh').click();
            }
            else {
                window.close();
                window.opener.document.getElementById('ctl00_btnRefresh').click();
            }
        }

        function ShowForgetPassPopWindow() {

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
            var Page = "../Users_Login/ForgotPasswordU.aspx?Type=Acnt";
            newwin = window.open(Page, 'ChatFPUA', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function checkValidation() {

            var email = document.getElementById('<%= txtEmail.ClientID%>').value;

            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            var address = email;
            if (reg.test(address) == false) {
                msg = "\Please enter your email address.";
                alert(msg);

                return false;
            }
            else {
                if (document.getElementById('<%= txtPass.ClientID%>').value == "") {
                    return true;
                }
                else {
                    var x = "Cookies enabled: " + navigator.cookieEnabled;

                    document.cookie = "UCOOKIE=UCOOKIE;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2020 20:47:11 UTC;";
                    var x = false;
                    if (document.cookie.length != 0) {

                        var ca = document.cookie.split(";");
                        for (var i = 0; i < ca.length; i++) {
                            var c = ca[i].trim();

                            if (c.indexOf("UCOOKIE") == 0) {
                                document.cookie = "UCOOKIE=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                                x = true;

                            }
                        }
                    }
                    //  alert(x);
                    //var x = true;
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
                            alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                        }
                        return false;


                    }
                    else {
                        return true;
                    }
                }
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

        function ShowAccCheckAlert(Page) {
            var msg = "We periodically check, and your profile is incomplete. Please select Ok to update your profile, or select Cancel to continue without updating your profile."
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

                newwin = window.open(Page, 'AccCheckComplete_a', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                }
                else {
                    window.close();
                    window.opener.document.getElementById('btnRefresh').click();
                    return true;
                }

            }
            else {
                window.close();
                window.opener.document.getElementById('btnRefresh').click();
            }
        }

    </script>
    <table class="ContentPage" id="ContentPage" runat="server" cellpadding="0" cellspacing="5"
        width="100%" border="0">
        <tr style="height: 20px">
            <td align="left" style="width: 100%">
                <div style="text-align: center;">
                    <asp:Panel ID="pnlAEA" runat="server" DefaultButton="btnSubmit">
                        <table width="100%">
                            <tr>
                                <td align="center">
                                    <table width="75%">
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px; text-align: center;" colspan="2">
                                                <asp:Label ID="lbltitle" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" style="width: 100%">
                                            <td style="text-align: right; width: 25%" class="style1">
                                                <asp:Label ID="lblEmail" runat="Server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                            </td>
                                            <td style="text-align: left;" class="style2">
                                                <asp:TextBox ID="txtEmail" runat="Server" Width="360px" Style="font-size: 11px; height: 16px;"
                                                    MaxLength="60"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" style="width: 100%" id="trPassword" runat="server" visible="false">
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblPasswod" runat="Server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtPass" runat="Server" TextMode="Password" Width="360px" Style="font-size: 11px; height: 16px;"
                                                    MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td colspan="2" align="center">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Style="margin-left: -20px"
                                                    Text="Submit" Width="90px" OnClientClick="return checkValidation();" />
                                                <asp:Button ID="btnCancel" runat="server" CssClass="Button" Style="margin-left: 15px"
                                                    Text="Cancel" Width="90px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 20px"></td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 60%;">
                            <asp:LinkButton ID="lnkForgotPass" runat="Server" Visible="false" CssClass="Link"
                                Text="Forgot password" Style="text-decoration: none; padding-left: 122px; font-size: 14px; font-weight: bold;"
                                OnClientClick="return FPEmailCheck()"></asp:LinkButton>
                        </td>
                        <td style="color: black; width: 40%;">
                            <div style="font-size: 14px; font-weight: bold; margin-top: 4px;">
                                <span style="margin-top: 4px; color: #ff7b3c;">Questions?</span><br />
                                <span style="margin-top: 4px">Call us at [1] 952-405-7500</span><br />
                                <span style="margin-top: 4px">Or email us at <a style="text-decoration: none; font-weight: bold; font-size: 14px;"
                                    class="LinkSupp" href="mailto:support@savvypack.com">support@savvypack.com</a></span><br />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--<tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 274px; padding-left: 32px;">
                             <asp:LinkButton ID="lnkForgotPass" runat="Server" Visible="false" CssClass="Link"
                                Text="Forgot password" Style="text-decoration: none; font-size: 14px;
                                font-weight: bold;" OnClientClick="return FPEmailCheck()"></asp:LinkButton>
                        </td>
                        <td style="padding-left: 170px; color: black;">
                            <div style="text-align: left; font-size: 14px; font-weight: bold; margin-top: 4px;">
                                <span style="margin-top: 4px; color: #ff7b3c;">Questions?</span><br />
                                <span style="margin-top: 4px">Call us at [1] 952-405-7500</span><br />
                                <span style="margin-top: 4px">Or email us at <a style="text-decoration: none; font-weight: bold;
                                    font-size: 14px;" class="LinkSupp" href="mailto:support@savvypack.com">support@savvypack.com</a></span><br />
                            </div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>--%>
        <%--   <tr>
            <td align="left" valign="middle">
                <div style="width: 100%; margin-left: 15px;">
                    <asp:LinkButton ID="lnkForgotPass" runat="Server" Visible="false" CssClass="Link"
                        Text="Retrieve your password" Style="font-size: 16px; font-weight: bold;" OnClientClick="return ShowForgetPassPopWindow();"></asp:LinkButton>
                </div>
            </td>
        </tr>--%>
        <%-- <tr>
            <td style="height: 20px">
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="font-size: 17px; color: #ff7b3c; margin-left: 15px; font-weight: bold">
                    Questions?</div>
                <div style="margin-top: 5px; font-size: 12pt; color: #000000; margin-left: 18px;
                    font-family: Optima;">
                    Call us at 952-405-7500
                </div>
            </td>
        </tr>--%>
        <tr>
            <td style="height: 20px"></td>
        </tr>
    </table>
    <asp:HiddenField ID="hidAcc" runat="server" />
</asp:Content>
