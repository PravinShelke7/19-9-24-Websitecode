<%@ Page Language="VB" AutoEventWireup="false" CodeFile="WebUserInformation.aspx.vb"
    Inherits="WebConferenceN_WebUserInformation" MasterPageFile="~/Masters/SavvyPackMenu.master"
    Title="Accopunt Information" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">   
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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

        function checkValidationInEdit() {

            var email = document.getElementById('<%= txtEmailE.ClientID%>').value;
            var FirstName = document.getElementById('<%= txtFNameE.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLNameE.ClientID%>').value;
            var phone = document.getElementById('<%= txtphoneE.ClientID%>').value;
            //Changes for Mobile
            var mobile = document.getElementById('<%= txtMobE.ClientID%>').value;
            //End

            var errorMsg1 = "";
            var errorMsg = "";
            var space = " ";

            if (email == "") {
                errorMsg1 += "\Email Address cannot be blank.\n";
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
            //11Jan_2018
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
            //End11Jan
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

        function Verify(type) {
            if (type.toString() == 'Y') {
                alert("Please verify your account information.");
                return true;
            }
            else {
                return false;
            }
        }


        function CheckSP(text) {

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

    </script>
    <table id="ContentPage" runat="server" width="950px">
        <tr>
            <td>
                <div style="text-align: center; display: inline;" id="dvEditUser" runat="Server">
                    <table width="100%" style="text-align: center;">
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
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblPrefixE" runat="server" Text="Prefix:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtpreFixE" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblFirstNameE" runat="server" Text="First Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFNameE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                            <asp:TextBox ID="txtMobE" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"
                                                onchange="javascript:CheckSP(this);"></asp:TextBox>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="txtphone"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="Label5" runat="server" Text="Fax:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFaxE" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"
                                                onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                                        MaxLength="50" runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                    <br />
                                                    <asp:Label ID="lblOtherCE" runat="server" Visible="false"> <span style="color: Red; font-size: 11px;">*Please Provide your Company Name</span> </asp:Label>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="ddlCompanyE" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                            <%-- <asp:TextBox ID="txtCompNameE" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server" Enabled="false"></asp:TextBox>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="Label8" runat="server" Text="Position:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtposE" Style="width: 300px; font-size: 11px;" MaxLength="50" runat="server"
                                                onchange="javascript:CheckSP(this);"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="Label9" runat="server" Text="Street Address:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtStAddress1E" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                            <%--    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" ControlToValidate="txtStAddress1E" ValidationGroup="Edit" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtStAddress2E" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                                        runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                                runat="server" onchange="javascript:CheckSP(this);"></asp:TextBox>
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
                                            <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Proceed" Style="margin-left: -90px;"
                                                OnClientClick="return checkValidationInEdit();" />
                                            <asp:Button ID="btncancelE" runat="server" CssClass="Button" Style="margin-left: 4px;"
                                                Text="Cancel" CausesValidation="false" />
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: left;" colspan="2">
                                            <span style="color: Red; font-size: 15px; margin-left: 10px;">* = required</span>
                                        </td>
                                    </tr>
                                </table>
                            </td>                            
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="font-size: 16px; font-weight: bold; display: none; height: 30px">
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidStatusId" runat="server" />
    <asp:HiddenField ID="hidConf" runat="server" />
    <asp:HiddenField ID="hidUpdateLbl" runat="server" />
</asp:Content>
