<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserInfoEdit.aspx.vb" Inherits="ShoppingCart_UserInfoEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Edit Shopping Cart Address</title>
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
            var FirstName = document.getElementById('<%= txtFNameE.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLNameE.ClientID%>').value;
            var phone = document.getElementById('<%= txtphoneE.ClientID%>').value;
            var CompanyName = document.getElementById('<%= txtCompNameE.ClientID%>').value;
            var SAdress1 = document.getElementById('<%= txtStAddress1E.ClientID%>').value;
            var City = document.getElementById('<%= txtCityE.ClientID%>').value;

            var ZipCode = document.getElementById('<%= txtZipCodeE.ClientID%>').value;


            var country = document.getElementById('<%= ddlCountryE.ClientID%>');

            var cName = country.options[country.selectedIndex].text;
            var State = "";
            var StateVal = "";
            //alert(country.value);
            if (cName == "United States") {

                var StateVal = document.getElementById('<%= ddlStateE.ClientID%>');
                StateVal = StateVal.options[StateVal.selectedIndex].value;
            }
            else {

                State = document.getElementById('<%= txtStateE.ClientID%>').value;
                if (State == "") {
                    StateVal = 0;
                }

            }
            //alert(StateVal);
            var errorMsg1 = "";
            var errorMsg = "";
            var space = " ";

            if (FirstName == "") {
                errorMsg1 += "\First Name can not be blank.\n";
            }
            if (LastName == "") {
                errorMsg1 += "\Last Name can not be blank.\n";
            }
            if (phone == "") {
                errorMsg1 += "\Phone number can not be blank.\n";
            }
            if (CompanyName == "") {
                errorMsg1 += "\CompanyName can not be blank.\n";
            }
            if (SAdress1 == "") {
                errorMsg1 += "\Street Address can not be blank.\n";
            }
            if (City == "") {
                errorMsg1 += "\City can not be blank.\n";
            }
            //                  if(State =="")
            //                  {
            //                    errorMsg1 += "\State can not be blank.\n";                                         
            //                  } 

            if (eval(country.value) == eval(0)) {
                errorMsg1 += "\Country must be specified.\n";
            }

            if (eval(StateVal) == eval(0)) {
                errorMsg1 += "\State must be specified.\n";
            }
            if (ZipCode == "") {
                errorMsg1 += "\ZipCode can not be blank.\n";
            }

            if (errorMsg1 != "") {
                errorMsg += alert(errorMsg1);
                return false;
            }
            else {

                return true;
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

        function CloseWindow() {
            window.close();
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnRefresh').click();
        }
    </script>
</head>
<body style="background-color: #F1F1F2;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div>
            <table class="ContentPage" id="ContentPage" runat="server">
                <tr>
                    <td>
                        <div style="text-align: center; display: inline;" id="dvEditUser" runat="Server">
                            <table width="100%" style="text-align: center;">
                                <tr>
                                    <td>
                                        <table width="100%" cellpadding="1px" cellspacing="1px">
                                            <tr class="TdHeading" align="center">
                                                <td class="PageSHeading" style="font-size: 14px;" colspan="2">Edit Shopping Cart Address
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" style="width: 100%">
                                                <td style="width: 30%; text-align: right; height: 20px;">
                                                    <asp:Label ID="Label6" runat="server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left; height: 20px;">
                                                    <asp:TextBox ID="txtEmailE" Style="width: 360px; font-size: 11px;" MaxLength="60"
                                                        runat="server" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor2" style="width: 50%">
                                                <td style="width: 30%; text-align: right;">
                                                    <asp:Label ID="Label1" runat="server" Text="Address Header:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:TextBox ID="txtAddHeader" Style="width: 360px; font-size: 11px;" MaxLength="60"
                                                        runat="server" Enabled="false" ToolTip="Create a Nickname for this account"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" style="width: 50%">
                                                <td style="width: 30%; text-align: right;">
                                                    <asp:Label ID="lblPrefixE" runat="server" Text="Prefix:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:TextBox ID="txtpreFixE" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor2" style="width: 50%">
                                                <td style="width: 30%; text-align: right;">
                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                    <asp:Label ID="lblFirstNameE" runat="server" Text="First Name:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:TextBox ID="txtFNameE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" style="width: 50%">
                                                <td style="width: 30%; text-align: right;">
                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                    <asp:Label ID="Label3" runat="server" Text="Last Name:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:TextBox ID="txtLNameE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor2" style="width: 50%">
                                                <td style="width: 30%; text-align: right;">
                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                    <asp:Label ID="Label4" runat="server" Text="Phone:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:TextBox ID="txtphoneE" Style="width: 300px; font-size: 11px;" MaxLength="25"
                                                        runat="server"></asp:TextBox>
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
                                                    <asp:TextBox ID="txtCompNameE" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                                        runat="server"></asp:TextBox>
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
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" style="width: 50%">
                                                <td style="width: 30%; text-align: right;">
                                                    <span style="color: Red; font-size: 14px;">*</span>
                                                    <asp:Label ID="Label11" runat="server" Text="State:" CssClass="NormalLabel"></asp:Label>
                                                </td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                        <ContentTemplate>
                                                            <asp:DropDownList ID="ddlStateE" runat="server" CssClass="DropDown" Font-Size="11px"
                                                                Width="300px" Height="20px" Visible="true">
                                                            </asp:DropDownList>
                                                            <asp:TextBox ID="txtStateE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                                runat="server" onchange="javascript:CheckSC(this);"></asp:TextBox>
                                                        </ContentTemplate>
                                                        <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="ddlCountryE" EventName="SelectedIndexChanged" />
                                                        </Triggers>
                                                    </asp:UpdatePanel>
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
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor2">
                                                <td style="width: 30%; text-align: center;"></td>
                                                <td style="width: 65%; text-align: left">
                                                    <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" OnClientClick="return checkValidationInEdit();" />
                                                    <asp:Button ID="btnCancelE" runat="server" CssClass="Button" Text="Cancel" Height="26px"
                                                        Width="60px" />
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
                    <td style="font-size: 16px; font-weight: bold; display: none; height: 30px"></td>
                </tr>
            </table>
            <asp:HiddenField ID="hidUserID" runat="server" />
        </div>
    </form>
</body>
</html>
