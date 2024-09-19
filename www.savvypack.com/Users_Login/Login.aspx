<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false"
    CodeFile="Login.aspx.vb" Inherits="Users_Login_Login" Title="User Login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
    </style>
    <script type="text/javascript" language="javascript">
        function CloseWindow() {
            var uType = document.getElementById('<%= hdnUTYPE.ClientID%>').value;
            if (uType == "1") {
                alert("Thank you for creating your account. Your email address has been verified.  Because you used a generic email domain, your account is being reviewed for approval. You will be notified when your account is activiated.");
            }
            else {
                alert("Thank you for updating your account. Your email address has been verified. Your account is being reviewed for approval, and you will be notified when your account is activiated.");

            }
        }

        function CloseWindowUA() {
            var uType = document.getElementById('<%= hdnUTYPE.ClientID%>').value;
            if (uType == "1") {
                alert("Thank you for creating your account. Your email address has been verified.  Your account is being reviewed for approval, and you will be notified when your account is activiated.");
            }
            else {
                alert("Thank you for completing your account. Your email address has been verified.  Your account is being reviewed for approval, and you will be notified when your account is activiated.");

            }
        }

        function CloseWindowAcp() {
            alert("Thank you for completing your account. You can now use your account.");
        }

        function checkValidation() {

            var Email = document.getElementById('<%= txtEmail.ClientID%>').value;
            var Pass = document.getElementById('<%= txtPass.ClientID%>').value;


            var errorMsg1 = "";
            var errorMsg = "";
            var space = " ";

            if (Email == "") {
                errorMsg1 += "\Email can not be blank.\n";
            }
            if (Pass == "") {
                errorMsg1 += "\Password can not be blank.\n";
            }

            if (errorMsg1 != "") {
                msg = "---------------------------------------------------------------------\n";
                msg += "                 Please correct the problem(s).\n";
                msg += "---------------------------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            else {

                return true;
            }


        }
        function ShowPopWindowVerf(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
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
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return true;
        }
        function ShowPopWindow() {
            document.getElementById('<%=btnSubmit.ClientID %>').focus();
            document.getElementById("<%=txtEmail.ClientID %>").focus();
            newwin = window.open('../Users_Login/ForgotPassword.aspx');
            return false;
        }
        function OpenNewPopUp() {

            var page;
            //            page = "AddEditUser.aspx?Mode=" + document.getElementById('<%= hdnMode.ClientID%>').value; ;
            page = "AddEditAccount.aspx";
            document.getElementById('<%=btnSubmit.ClientID %>').focus();
            document.getElementById("<%=txtEmail.ClientID %>").focus();
            newwin = window.open(page, 'NewWindow');
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
    <table class="ContentPage" id="ContentPage" runat="server" cellpadding="0" cellspacing="5"
        width="100%" border="0">
        <tr>
            <td>
                <div style="font-weight: bold; font-family: Arial; font-size: 18px; margin-left: 18px;">
                    My Account
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="font-weight: bold; font-family: optima; font-size: 15px; margin-left: 18px;
                    margin-right: 18px; width: 100%;">
                    Enter your email address and password to log into your account and continue. If
                    you don't have an<br />
                    SavvyPack Corporation account, please create one below.
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="left" style="width: 100%">
                <div style="text-align: center;">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <table width="75%">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            Member Login
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 100%">
                                        <td style="text-align: right; width: 25%" class="style1">
                                            <asp:Label ID="lblEmail" runat="Server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="text-align: left;" class="style2">
                                            <asp:TextBox ID="txtEmail" runat="Server" Width="360px" Style="font-size: 11px; height: 16px;"
                                                MaxLength="60"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvUserName" ControlToValidate="txtEmail" runat="Server" ErrorMessage="*" ValidationGroup="varify">
                                                        </asp:RequiredFieldValidator>  --%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 100%">
                                        <td style="text-align: right;">
                                            <asp:Label ID="lblPasswod" runat="Server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="text-align: left;">
                                            <asp:TextBox ID="txtPass" runat="Server" TextMode="Password" Width="360px" Style="font-size: 11px;
                                                height: 16px;" MaxLength="25"></asp:TextBox>
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
                                        <td colspan="2" align="center">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Style="margin-left: -20px"
                                                Text="Submit" Width="90px" OnClientClick="return checkValidation();" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <div style="width: 100%; margin-left: 15px;">
                    <asp:LinkButton ID="lnkCreateAcc" runat="Server" CssClass="Link" Text="Create an account"
                        Style="font-size: 16px; font-weight: bold;" OnClientClick="return OpenNewPopUp();"></asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="font-weight: bold; font-family: optima; font-size: 15px; margin-left: 15px;
                    width: 100%;">
                    By creating an account, you eliminate the need to re-enter your account information
                    when registering<br />
                    for a conference, placing an order, etc.
                </div>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td align="left" valign="middle">
                <div style="width: 100%; margin-left: 15px;">
                    <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="Link" Text="Forget your password?"
                        Style="font-size: 16px; font-weight: bold;" OnClientClick="return ShowPopWindow();"></asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td style="height: 20px">
            </td>
        </tr>
    </table>
    <asp:Button ID="btnClose" runat="server" Style="display: none;" />
    <asp:HiddenField ID="hdnMode" runat="Server" />
    <asp:Button ID="btnCloseAcp" runat="server" Style="display: none;" />
    <asp:Button ID="btnCloseUA" runat="server" Style="display: none;" />
    <asp:HiddenField ID="hdnUTYPE" runat="server" />
</asp:Content>
