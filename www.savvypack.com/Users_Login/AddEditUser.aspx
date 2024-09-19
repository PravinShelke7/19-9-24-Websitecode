<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="AddEditUser.aspx.vb" Inherits="Users_Login_AddEditUser" Title="Account Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <style type="text/css">
        .Available {
            font-family: Verdana;
            font-size: 11px;
            color: Green;
            font-style: italic;
        }

        .NotAvailable {
            font-family: Verdana;
            font-size: 11px;
            color: Red;
            font-style: italic;
        }
    </style>
    <script language="Javascript" type="text/javascript">
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
        function ShowAccountUpdation() {

            var msg = "To prevent delays in the account approval process, we recommend you to use only company specific email addresses (not gmail, for example).  Click on OK if you agree to change your domain name, or click on CANCEL to continue with same domain."
            if (confirm(msg)) {
                document.getElementById('<%=txtEmail.ClientID %>').focus();
            }
            else {
                document.getElementById('ctl00_ContentPlaceHolder1_btnUp').click();

            }

        }

        function ShowConfirmMessage() {
            var status = document.getElementById('<%= hidStatusId.ClientID%>').value;
            if (status == "2") {
                alert('Thank you for completing your account. Your account is now active.');
            }
            else {
                alert('Thank you for creating your account. Your email address has been verified, and you can now use your account.');
            }

            window.close();
        }

        function ShowConfirmMessageU() {
            alert('Thank you for completing your account. You can now use your account.');
            window.close();
        }

        function ShowStatusAccep() {
            alert('Thank you for creating your account. You can now use your account.');
            window.close();
        }

        function checkPassword() {

            var email = document.getElementById('<%= txtEmail.ClientID%>').value;
            var cnfemail = document.getElementById('<%= txtCEmail.ClientID%>').value;
            var FirstName = document.getElementById('<%= txtFirstName.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLastName.ClientID%>').value;
            var phone = document.getElementById('<%= txtphone.ClientID%>').value;
            //Chnages Mobile number
            var mobile = document.getElementById('<%= txtMob.ClientID%>').value;
            //var CompanyName  = document.getElementById('<%= txtCompanyName.ClientID%>').value      ; 
            var SAdress1 = document.getElementById('<%= txtSAdress1.ClientID%>').value;
            var City = document.getElementById('<%= txtCity.ClientID%>').value;

            var ZipCode = document.getElementById('<%= txtZipCode.ClientID%>').value;

            var Password = document.getElementById('<%= txtPassword.ClientID%>').value;
            var ConfirmPassword = document.getElementById('<%= txtConfirmPassword.ClientID%>').value;
            var country = document.getElementById('<%= ddlCountry.ClientID%>');
            var cName = country.options[country.selectedIndex].text;
            var State = "";
            var StateVal = "";

            //11Jan_2018
            var StateVal = document.getElementById('<%= ddlState.ClientID%>');
            if (StateVal != null) {
                StateVal = StateVal.options[StateVal.selectedIndex].value;
            }
            else {

                State = document.getElementById('<%= txtState.ClientID%>').value;
                if (State == "") {
                    StateVal = 0;
                }
            }
            //End11Jan

            //            if (cName == "United States") {

            //                var StateVal = document.getElementById('<%= ddlState.ClientID%>');
            //                StateVal = StateVal.options[StateVal.selectedIndex].value;
            //            }
            //            else {

            //                State = document.getElementById('<%= txtState.ClientID%>').value;
            //                if (State == "") {
            //                    StateVal = 0;
            //                }
            //            }

            //Start Company Changes 

            var Company = document.getElementById('<%= ddlCompany.ClientID%>');
            if (Company != null) {
                var CompName = Company.options[Company.selectedIndex].text;

                if (CompName == "Other") {
                    var CompanyName = document.getElementById('<%= txtCompanyName.ClientID%>').value;
                }
                else {
                    CompanyName = CompName
                }

            }
            else {
                var CompanyName = document.getElementById('<%= txtCompanyName.ClientID%>').value;
            }

            //End Company Changes

            fieldvalue = document.getElementById('<%= txtPassword.ClientID%>').value;
            var errorMsg1 = "";
            var errorMsg = "";
            var space = " ";

            fieldvalue = document.getElementById('<%= txtPassword.ClientID%>').value;
            //alert(fieldvalue.length);
            fieldlength = fieldvalue.length;

            //It must not contain a space
            if (email == "") {
                errorMsg1 += "\Email Address cannot be blank.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }
            else if (email != cnfemail) {
                errorMsg1 += "\Email Address and Confirm Email Address should match.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }
            else {

                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                var address = email;
                if (reg.test(address) == false) {
                    errorMsg1 += "\Email Address is not in proper format.\n";
                    errorMsg += alert(errorMsg1);
                    return false;
                }

            }

            if (Password == "") {
                errorMsg1 += "\Password cannot be blank.\n";
                errorMsg += alert(errorMsg1);
                return false;
            }
            else {
                if (Password == ConfirmPassword) {
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
                    //If there is aproblem with the form then display an error
                    if (errorMsg != "") {
                        errorMsg += alert(errorMsg1 + errorMsg);
                        return false;
                    }
                    else {

                        if (FirstName == "") {
                            errorMsg1 += "\First Name cannot be blank.\n";
                        }
                        if (LastName == "") {
                            errorMsg1 += "\Last Name cannot be blank.\n";
                        }
                        if (phone == "") {
                            errorMsg1 += "\Phone number cannot be blank.\n";
                        }
                        if (mobile == "") {
                            errorMsg1 += "\Mobile number cannot be blank.\n";
                        }
                        if (CompanyName == "") {
                            errorMsg1 += "\CompanyName cannot be blank.\n";
                        }
                        if (SAdress1 == "") {
                            errorMsg1 += "\Street Address cannot be blank.\n";
                        }


                        if (eval(country.value) == eval(0)) {
                            errorMsg1 += "\Country must be specified.\n";
                        }

                        if (eval(StateVal) == eval(0)) {
                            errorMsg1 += "\State must be specified.\n";
                        }

                        if (City == "") {
                            errorMsg1 += "\City cannot be blank.\n";
                        }
                        //                                      if(State =="")
                        //                                      {
                        //                                        errorMsg1 += "\State can not be blank.\n";                                         
                        //                                      } 
                        if (ZipCode == "") {
                            errorMsg1 += "\ZipCode cannot be blank.\n";
                        }
                        //                                       if(Country =="")
                        //                                      {
                        //                                        errorMsg1 += "\Country can not be blank.\n";                                         
                        //                                      }                                    

                        if (errorMsg1 != "") {
                            errorMsg += alert(errorMsg1);
                            return false;
                        }
                        else {

                            return true;
                        }
                    }
                }
                else {
                    errorMsg1 = "Password and Confirm Password should match.";
                    errorMsg += alert(errorMsg1);
                    return false;
                }



            }

        }

        function MessageWindow() {
            var msg = "";
            msg = "-----------------------------------------------------------------------------------------------------------------------------\n";
            msg += "A User account already exists for this email address..\n Please enter different email address to create a new account.\n Or, if you have forgotten your password to your original account, let us know and we will email it to you.\n";
            msg += "-----------------------------------------------------------------------------------------------------------------------------\n";
            alert(msg);
        }
        function checkValidationInEdit() {

            var FirstName = document.getElementById('<%= txtFNameE.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLNameE.ClientID%>').value;
            var phone = document.getElementById('<%= txtphoneE.ClientID%>').value;
            //Changes for Mobile
            var mobile = document.getElementById('<%= txtMobE.ClientID%>').value;
            //End

            //Start Company Changes
            var Company = document.getElementById('<%= ddlCompanyE.ClientID%>');
            if (Company != null) {
                var CompName = Company.options[Company.selectedIndex].text;

                if (CompName == "Other") {
                    var CompanyName = document.getElementById('<%= txtCompNameE.ClientID%>').value;
                }
                else {
                    CompanyName = CompName
                }

            }
            else {
                var CompanyName = document.getElementById('<%= txtCompNameE.ClientID%>').value;
            }
            //End Company Changes                       

            var SAdress1 = document.getElementById('<%= txtStAddress1E.ClientID%>').value;
            var City = document.getElementById('<%= txtCityE.ClientID%>').value;

            var ZipCode = document.getElementById('<%= txtZipCodeE.ClientID%>').value;

            var country = document.getElementById('<%= ddlCountryE.ClientID%>');
            var cName = country.options[country.selectedIndex].text;
            var State = "";
            var StateVal = "";
            // alert(cName);

            //10Jan_2018
            var StateVal = document.getElementById('<%= ddlStateE.ClientID%>');
            if (StateVal != null) {
                StateVal = StateVal.options[StateVal.selectedIndex].value;
            }
            else {
                State = document.getElementById('<%= txtStateE.ClientID%>').value;
                if (State == "") {
                    StateVal = 0;
                }

            }
            //End10Jan


            //            if (cName == "United States") {

            //                var StateVal = document.getElementById('<%= ddlStateE.ClientID%>');
            //                //   alert(StateVal);
            //                StateVal = StateVal.options[StateVal.selectedIndex].value;
            //            }
            //            else {
            //                // alert('sud');

            //                State = document.getElementById('<%= txtStateE.ClientID%>').value;
            //                if (State == "") {
            //                    StateVal = 0;
            //                }

            //            }

            var mode = document.getElementById('<%= hidCompMode.ClientID%>').value;
            if (mode == "Comp") {
                var Password = document.getElementById('<%= txtPasswordE.ClientID%>').value;
                var ConfirmPassword = document.getElementById('<%= txtConfirmPasswordE.ClientID%>').value;

                fieldvalue = document.getElementById('<%= txtPasswordE.ClientID%>').value;
                var errorMsg1 = "";
                var errorMsg = "";
                var space = " ";
                fieldlength = fieldvalue.length;

                if (Password == "") {
                    errorMsg1 += "\Password cannot be blank.\n";
                    alert(errorMsg1);
                    return false;
                }
                else {
                    if (Password == ConfirmPassword) {
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
                        //If there is aproblem with the form then display an error
                        if (errorMsg != "") {
                            errorMsg += alert(errorMsg1 + errorMsg);
                            return false;
                        }
                        else {
                            if (FirstName == "") {
                                errorMsg1 += "\First Name cannot be blank.\n";
                            }
                            if (LastName == "") {
                                errorMsg1 += "\Last Name cannot be blank.\n";
                            }
                            if (phone == "") {
                                errorMsg1 += "\Phone number cannot be blank.\n";
                            }
                            if (mobile == "") {
                                errorMsg1 += "\Mobile number cannot be blank.\n";
                            }
                            if (CompanyName == "") {
                                errorMsg1 += "\CompanyName cannot be blank.\n";
                            }
                            if (SAdress1 == "") {
                                errorMsg1 += "\Street Address cannot be blank.\n";
                            }

                            if (eval(country.value) == eval(0)) {
                                errorMsg1 += "\Country must be specified.\n";
                            }

                            if (eval(StateVal) == eval(0)) {
                                errorMsg1 += "\State must be specified.\n";
                            }

                            if (City == "") {
                                errorMsg1 += "\City cannot be blank.\n";
                            }
                            //                  if(State =="")
                            //                  {
                            //                    errorMsg1 += "\State can not be blank.\n";                                         
                            //                  } 
                            if (ZipCode == "") {
                                errorMsg1 += "\ZipCode cannot be blank.\n";
                            }
                            //                   if(Country =="")
                            //                  {
                            //                    errorMsg1 += "\Country can not be blank.\n";                                         
                            //                  } 

                            if (errorMsg1 != "") {
                                errorMsg += alert(errorMsg1);
                                return false;
                            }
                            else {

                                return true;

                            }
                        }
                    }
                    else {
                        errorMsg1 = "Password and Confirm Password should match.";
                        errorMsg += alert(errorMsg1);
                        return false;
                    }
                }
            }
            else {


                var errorMsg1 = "";
                var errorMsg = "";
                var space = " ";

                if (FirstName == "") {
                    errorMsg1 += "\First Name cannot be blank.\n";
                }
                if (LastName == "") {
                    errorMsg1 += "\Last Name cannot be blank.\n";
                }
                if (phone == "") {
                    errorMsg1 += "\Phone number cannot be blank.\n";
                }
                if (mobile == "") {
                    errorMsg1 += "\Mobile number cannot be blank.\n";
                }
                if (CompanyName == "") {
                    errorMsg1 += "\CompanyName cannot be blank.\n";
                }
                if (SAdress1 == "") {
                    errorMsg1 += "\Street Address cannot be blank.\n";
                }

                if (eval(country.value) == eval(0)) {
                    errorMsg1 += "\Country must be specified.\n";
                }

                if (eval(StateVal) == eval(0)) {
                    errorMsg1 += "\State must be specified.\n";
                }

                if (City == "") {
                    errorMsg1 += "\City cannot be blank.\n";
                }
                //                  if(State =="")
                //                  {
                //                    errorMsg1 += "\State can not be blank.\n";                                         
                //                  } 
                if (ZipCode == "") {
                    errorMsg1 += "\ZipCode cannot be blank.\n";
                }
                //                   if(Country =="")
                //                  {
                //                    errorMsg1 += "\Country can not be blank.\n";                                         
                //                  } 

                if (errorMsg1 != "") {
                    errorMsg += alert(errorMsg1);
                    return false;
                }
                else {

                    return true;
                }

            }
        }

        function ShowPopWindow(Page) {
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
            newwin = window.open(Page, 'ChatV', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return true;
        }

        function CloseWindow() {
            alert("Thank you for creating your account. Your email address has been verified. Because you used a generic email domain, your account is being reviewed for approval. You will be notified when your account is activiated.");
            window.close();
        }
        function CloseWindowAcp() {
            window.close();
        }
        function CloseWindowU() {
            alert("Thank you for updating your account. Your email address has been verified. Your account is being reviewed for approval, and you will be notified when your account is activiated.");
            window.close();
        }
        function CloseWindowAcpU() {
            alert("Thank you for updating your account. Your email address has been verified and you can now use your account.");
            window.close();
        }
        function CloseWindowUA() {
            alert("Thank you for creating your account. Your email address has been verified. Your account is being reviewed for approval, and you will be notified when your account is activiated.");
            window.close();
        }

        function CheckSC(text) {

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
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
    <table id="ContentPage" runat="server" width="950px">
        <tr>
            <td align="left">
                <div style="width: 100%; margin-left: 120px;">
                    <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="Link" Text="Forget your password?"
                        Style="font-size: 15px; font-weight: bold; display: none"></asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="text-align: center; display: none;" id="dvAddUser" runat="Server">
                    <table width="100%" style="text-align: center;">
                        <tr>
                            <td align="center">
                                <table width="70%" cellpadding="1px" cellspacing="1px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px; text-align: center;" colspan="2">Account Information
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 100%">
                                        <td style="width: 30%; text-align: right; height: 20px;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left; height: 20px;">
                                            <asp:TextBox ID="txtEmail" Style="width: 370px; font-size: 11px;" runat="server"
                                                AutoPostBack="true" MaxLength="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trUsername" runat="server" visible="false" class="AlterNateColor1" style="width: 100%">
                                        <td style="width: 30%; text-align: right; height: 20px;"></td>
                                        <td style="width: 65%; text-align: left; height: 20px;">
                                            <asp:Label ID="lblUserAv" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 100%">
                                        <td style="width: 30%; text-align: right; height: 20px;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lnlCEmail" runat="server" Text="Confirm Email:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left; height: 20px;">
                                            <asp:TextBox ID="txtCEmail" Style="width: 370px; font-size: 11px;" runat="server"
                                                MaxLength="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtPassword" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" runat="Server"  ErrorMessage="*">
                                        </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvConfirmPassword" ControlToValidate="txtConfirmPassword"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblPreFix" runat="server" Text="Prefix:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtPrefix" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFirstName" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvFirstName" ControlToValidate="txtFirstName"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtLastName" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtLastName"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblPhone" runat="server" Text="Phone:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtphone" Style="width: 300px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtphone"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblMob" runat="server" Text="Mobile:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtMob" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtphone"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblFax" runat="server" Text="Fax:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFax" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblCompanyName" runat="server" Text="Company Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCompany" runat="server" Visible="false" CssClass="DropDown"
                                                        Font-Size="11px" Width="300px" Height="20px" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtCompanyName" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                        runat="server" onchange="javascript:CheckSC(this);"></asp:TextBox>
                                                    <br />
                                                    <asp:Label ID="lblOtherC" runat="server" Visible="false"> <span style="color: Red; font-size: 11px;">*Please Provide your Company Name</span> </asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCompany" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblPos" runat="server" Text="Position:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtPosition" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblAdd" runat="server" Text="Street Address:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtSAdress1" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtSAdress1"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;"></td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtSAdress2" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblCountry" runat="server" Text="Country:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="DropDown" Font-Size="11px"
                                                AutoPostBack="true" Width="300px" Height="20px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblState" runat="server" Text="State:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <%--<asp:TextBox ID="txtState" style="width:160px;font-size:11px;" MaxLength="25" runat="server" ></asp:TextBox>--%>
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                <ContentTemplate>
                                                    <asp:TextBox ID="txtState" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                        runat="server" Visible="false" onchange="javascript:CheckSC(this);"></asp:TextBox>
                                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="DropDown" Font-Size="11px"
                                                        Width="300px" Height="20px" Visible="false">
                                                    </asp:DropDownList>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtCity" Style="width: 160px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblZipCode" runat="server" Text="Zip Code:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtZipCode" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                runat="server"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtZipCode"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="width: 30%; text-align: center;"></td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:Button ID="btnReg" runat="server" CssClass="ButtonWMarigin" Text="Submit" OnClientClick="return checkPassword()" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonWMarigin" Text="Cancel"
                                                CausesValidation="false" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td style="width: 100%; text-align: left;" colspan="2">
                                            <span style="color: Red; font-size: 15px; margin-left: 10px;">* = required </span>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px;"></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="text-align: center; display: none;" id="dvEditUser" runat="Server">
                    <table width="100%" style="text-align: center;">
                        <tr>
                            <td colspan="2" style="height: 20px; color: Red; font-size: 14px; vertical-align: top; font-weight: bold; font-family: Arial">
                                <asp:Label ID="lblMessage" runat="Server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="left">
                                <asp:LinkButton ID="LinkButton2" runat="Server" Style="text-decoration: none; font-weight: bold; font-size: 12px; margin-left: 140px"
                                    CssClass="Link" Text="Change your password"
                                    OnClick="ChangePassword"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top" align="center">
                                <table width="70%" cellpadding="1px" cellspacing="1px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px; text-align: center;" colspan="2">
                                            <asp:Label ID="lblTitleE" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 100%" id="trEM1" runat="server">
                                        <td style="width: 30%; text-align: right; height: 20px;">
                                            <asp:Label ID="Label6" runat="server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left; height: 20px;">
                                            <asp:TextBox ID="txtEmailE" Style="width: 300px; font-size: 11px;" MaxLength="60"
                                                runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trPassE" runat="server" class="AlterNateColor2" style="width: 100%;">
                                        <td style="width: 30%; text-align: right">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblPasswordE" runat="server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtPasswordE" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr id="trCnfPassE" runat="server" class="AlterNateColor1" style="width: 50%;">
                                        <td style="width: 30%; text-align: right">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblConfirmPasswordE" runat="server" Text="Confirm Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtConfirmPasswordE" TextMode="Password" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblPrefixE" runat="server" Text="Prefix:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtpreFixE" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblFirstNameE" runat="server" Text="First Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFNameE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator8" ControlToValidate="txtFNameE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label3" runat="server" Text="Last Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtLNameE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator9" ControlToValidate="txtLNameE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label4" runat="server" Text="Phone:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtphoneE" Style="width: 300px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%--   <asp:RequiredFieldValidator ID="RequiredFieldValidator10" ControlToValidate="txtphoneE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblMobE" runat="server" Text="Mobile:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtMobE" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtphone"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="Label5" runat="server" Text="Fax:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFaxE" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label7" runat="server" Text="Company Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlCompanyE" runat="server" Visible="true" CssClass="DropDown"
                                                        Font-Size="11px" Width="300px" Height="20px" AutoPostBack="true">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtCompNameE" Visible="False" Style="width: 300px; font-size: 11px;"
                                                        MaxLength="50" runat="server" onchange="javascript:CheckSC(this);"></asp:TextBox>
                                                    <br />
                                                    <asp:Label ID="lblOtherCE" runat="server" Visible="false"> <span style="color: Red; font-size: 11px;">*Please Provide your Company Name</span> </asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCompanyE" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%--<asp:TextBox ID="txtCompNameE" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                    runat="server" Enabled="false"></asp:TextBox>--%>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator12" ControlToValidate="txtCompNameE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="Label8" runat="server" Text="Position:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtposE" Style="width: 300px; font-size: 11px;" MaxLength="50" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label9" runat="server" Text="Street Address:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtStAddress1E" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtStAddress1E" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td></td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtStAddress2E" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label13" runat="server" Text="Country:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:DropDownList ID="ddlCountryE" runat="server" CssClass="DropDown" AutoPostBack="true"
                                                Font-Size="11px" Width="300px" Height="20px" Visible="true">
                                            </asp:DropDownList>
                                            <%--<asp:TextBox ID="txtCounE" style="width:160px;font-size:11px;" MaxLength="25" runat="server" ></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label11" runat="server" Text="State:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                                                    <asp:DropDownList ID="ddlStateE" runat="server" CssClass="DropDown" Font-Size="11px"
                                                        Width="300px" Height="20px" Visible="false">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtStateE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                        runat="server" onchange="javascript:CheckSC(this);"></asp:TextBox>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCountryE" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator15" ControlToValidate="txtStateE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label10" runat="server" Text="City:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtCityE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator14" ControlToValidate="txtCityE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label12" runat="server" Text="Zip Code:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtZipCodeE" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                runat="server"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator16" ControlToValidate="txtZipCodeE" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr id="trPromoMail" runat="server" class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblPromoMails" runat="server" Text="Allow Promotional Emails:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:RadioButton ID="rYes" Text="Yes" runat="Server" GroupName="promoMail" Checked="true" />
                                            <asp:RadioButton ID="rNo" Text="No" runat="Server" GroupName="promoMail" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="width: 30%; text-align: center;" colspan="2">
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" Style="margin-left: -90px;"
                                                OnClientClick="return checkValidationInEdit();" />
                                            <asp:Button ID="btncancelE" runat="server" CssClass="ButtonWMarigin" Text="Cancel"
                                                CausesValidation="false" />
                                            <asp:Button ID="btncancelU" runat="server" CssClass="ButtonWMarigin" Text="Cancel"
                                                CausesValidation="false" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: left;" colspan="2">
                                            <span style="color: Red; font-size: 15px; margin-left: 10px;">* = required</span>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="width: 30%; text-align: left;" colspan="2">
                                            <span style="font-weight: bold">Note:</span> You cannot change your email address
                                                    on this account. If your email address has changed, please create a new account.                                                    
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" style="height: 10px;"></td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <%--<tr>
            <td style="font-size: 16px; font-weight: bold; display: none; height: 30px">
                <div id="dvThanksPage" runat="Server">
                    Thanks for registering, Please check your mailbox for verifiation Mail. After verifying
                    your mail, your account will be active.
                    <br />
                </div>
            </td>
        </tr>--%>
    </table>
    <asp:HiddenField ID="hidStatusId" runat="server" />
    <asp:HiddenField ID="hidPass" runat="server" />
    <asp:HiddenField ID="hidCompMode" runat="server" />
    <asp:HiddenField ID="hidConf" runat="server" />
    <asp:HiddenField ID="hidUpdateLbl" runat="server" />
    <asp:HiddenField ID="hidDomID3" runat="server" />
    <asp:Button ID="btnUp" runat="server" Style="display: none;" />
    <asp:Button ID="btnClose" runat="server" Style="display: none;" />
    <asp:Button ID="btnCloseAcp" runat="server" Style="display: none;" />
    <asp:Button ID="btnCloseU" runat="server" Style="display: none;" />
    <asp:Button ID="btnCloseAcpU" runat="server" Style="display: none;" />
    <asp:Button ID="btnCloseUA" runat="server" Style="display: none;" />
</asp:Content>
