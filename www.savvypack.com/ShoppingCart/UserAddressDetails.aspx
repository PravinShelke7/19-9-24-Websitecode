<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserAddressDetails.aspx.vb"
    Inherits="ShoppingCart_UserAddressDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Shopping Cart Addresses</title>
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
    <script type="text/JavaScript">

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
            //                StateVal = StateVal.options[StateVal.selectedIndex].value;
            //            }
            //            else {

            //                State = document.getElementById('<%= txtStateE.ClientID%>').value;
            //                if (State == "") {
            //                    StateVal = 0;
            //                }

            //            }
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
                msg = "------------------------------------------------------\n";
                msg += "       Please correct the following problem(s).\n";
                msg += "------------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            else {

                return true;
            }


        }
        function checkPassword() {

            var email = document.getElementById('<%= txtEmail.ClientID%>').value;
            var cnfemail = document.getElementById('<%= txtCEmail.ClientID%>').value;
            var FirstName = document.getElementById('<%= txtFirstName.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLastName.ClientID%>').value;
            var phone = document.getElementById('<%= txtphone.ClientID%>').value;
            var CompanyName = document.getElementById('<%= txtCompanyName.ClientID%>').value;
            var SAdress1 = document.getElementById('<%= txtSAdress1.ClientID%>').value;
            var City = document.getElementById('<%= txtCity.ClientID%>').value;
            var country = document.getElementById('<%= ddlCountry.ClientID%>');
            var cName = country.options[country.selectedIndex].text;
            var State = "";
            var StateVal = "";
            // alert(cName);
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

            var ZipCode = document.getElementById('<%= txtZipCode.ClientID%>').value;
            var AddHeader = document.getElementById('<%= txtAHeader.ClientID%>').value;

            var errorMsg1 = "";
            var errorMsg = "";
            var space = " ";



            //It must not contain a space
            if (email == "") {
                errorMsg1 += "\Email Address can not be blank.\n";
                msg = "-----------------------------------------------------\n";
                msg += "Please correct the following problem(s).\n";
                msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            else if (email != cnfemail) {
                errorMsg1 += "\Email Address and Confirm Email Address should match.\n";
                msg = "-----------------------------------------------------\n";
                msg += "Please correct the following problem(s).\n";
                msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            else {

                var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
                var address = email;
                if (reg.test(address) == false) {
                    errorMsg1 += "\Email Address is not in proper format.\n";
                    msg = "-----------------------------------------------------\n";
                    msg += "Please correct the following problem(s).\n";
                    msg += "-----------------------------------------------------\n";
                    errorMsg += alert(msg + errorMsg1 + "\n\n");
                    return false;
                }

            }

            if (AddHeader == "") {
                errorMsg1 += "\Address Description can not be blank.\n";
                msg = "-----------------------------------------------------\n";
                msg += "Please correct the following problem(s).\n";
                msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }
            else {
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
                    msg = "-----------------------------------------------------\n";
                    msg += "Please correct the following problem(s).\n";
                    msg += "-----------------------------------------------------\n";
                    errorMsg += alert(msg + errorMsg1 + "\n\n");
                    return false;
                }
                else {

                    return true;
                }
            }

        }
        function AddressDet(AddDes, AddId) {
            //            alert(AddDes);
            //            alert(AddId);
            var hidAdddes = document.getElementById('<%= hidAddDes.ClientID%>').value;
            var hidAddId = document.getElementById('<%= hidAddID.ClientID%>').value;
            //            alert(hidAdddes);
            //            alert(hidAddId);
            window.opener.document.getElementById(hidAdddes).innerText = AddDes;
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdnbilladdDes').value = AddDes;

            window.opener.document.getElementById(hidAddId).value = AddId;
            window.close();
        }
    </script>
</head>
<body style="background-color: #dae5f5;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="divGrid" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <span style="font-size: 18px; font-weight: bold; color: Black;">Shopping Cart Addresses
                        </span>
                    </td>
                </tr>

                <%-- <tr>
                <td align="center" style="height: 14px;">
                </td>
            </tr>--%>
                <tr>
                    <td align="center">
                        <div id="divList" runat="server" style="width: 700px; overflow: scroll;">
                            <asp:GridView runat="server" ID="grdUserAddress" DataKeyNames="USERADDRESSID" Width="1500px"
                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" CellPadding="4"
                                BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" PageSize="5"
                                EnableModelValidation="True">
                                <RowStyle BackColor="#C0C9E7" />
                                <AlternatingRowStyle BackColor="#D0D1D3" />
                                <%--  <FooterStyle BackColor="#32659A" ForeColor="#003399" />--%>

                                <Columns>
                                    <asp:TemplateField HeaderText="Edit User">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkEdit" runat="server" Text="edit" CssClass="Link" OnClick="lnkEditUser_Click"></asp:LinkButton>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="70px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="USERADDRESSID" HeaderText="USERADDRESSID" SortExpression="USERADDRESSID"
                                        Visible="False"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Select User" SortExpression="USERNAME">
                                        <ItemTemplate>
                                            <a href="#" onclick="AddressDet('<%#Container.DataItem("USERNAME")%>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<%#Container.DataItem("ADDHEADER")%>','<%#Container.DataItem("USERADDRESSID")%>')"
                                                class="Link">
                                                <%#Container.DataItem("USERNAME")%></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="180px" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="USERNAME" HeaderText="Select User" SortExpression="USERNAME"
                                        Visible="false">
                                        <ItemStyle Width="180px" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ADDHEADER" HeaderText="Address Description" SortExpression="ADDHEADER"></asp:BoundField>
                                    <asp:BoundField DataField="FULLNAME" HeaderText="User Name" SortExpression="FULLNAME">
                                        <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="PHONENUMBER" HeaderText="Phone" SortExpression="PHONENUMBER">
                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FAXNUMBER" HeaderText="Fax" SortExpression="FAXNUMBER">
                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Right" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="COMPANYNAME" HeaderText="Company Name" SortExpression="COMPANYNAME">
                                        <ItemStyle Width="150px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="JOBTITLE" HeaderText="Position" SortExpression="JOBTITLE">
                                        <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ADDRESS" HeaderText="Address" SortExpression="ADDRESS">
                                        <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY">
                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="STATE" HeaderText="State" SortExpression="STATE">
                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="ZIPCODE" HeaderText="Zip Code" SortExpression="ZIPCODE">
                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="COUNTRYDES" HeaderText="Country" SortExpression="COUNTRYDES">
                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                <HeaderStyle Height="25px" BackColor="#58595B" Font-Bold="True" ForeColor="White" />
                                <FooterStyle BackColor="#32659A" ForeColor="#003399" />
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px; color: Red; font-size: 14px; vertical-align: top; font-weight: bold; font-family: Arial; text-align: center">
                        <asp:Label ID="lblMessage" runat="Server"></asp:Label>
                    </td>
                </tr>
                <tr id="rowCngPwd" runat="server" visible="true" align="center">
                    <td>
                        <table width="80%" cellpadding="1px" cellspacing="1px">
                            <tr class="TdHeading">
                                <td class="PageSHeading" style="font-size: 14px;" colspan="2">Add Shopping Cart Address
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 100%">
                                <td style="width: 30%; text-align: right; height: 20px;">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lblEmail" runat="server" Text="Email:" Style="color: #421995;" CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left; height: 20px;">
                                    <asp:TextBox ID="txtEmail" Style="width: 370px; font-size: 11px;" runat="server"
                                        MaxLength="60" AutoPostBack="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 100%" id="rowcheckuser" runat="server"
                                visible="false">
                                <td colspan="2" style="color: Black; font-family: Optima; font-size: 12px; font-weight: normal;"
                                    align="justify">
                                    <asp:Label ID="lblmsg" runat="server" Style="text-align: left;" Width="510px">
                            This User already exist. 
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2" style="width: 100%">
                                <td style="width: 30%; text-align: right; height: 20px;">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lnlCEmail" runat="server" Style="color: #421995;" Text="Confirm Email:"
                                        CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left; height: 20px;">
                                    <asp:TextBox ID="txtCEmail" Style="width: 370px; font-size: 11px;" runat="server"
                                        MaxLength="60"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 50%">
                                <td style="width: 30%; text-align: right">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lblAddHeader" runat="server" Style="color: #421995;" Text="Address Description:"
                                        CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left">
                                    <asp:TextBox ID="txtAHeader" MaxLength="25" Style="width: 160px; font-size: 11px;"
                                        AutoPostBack="true" runat="server" ToolTip="Create a Nickname for this account"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 100%" id="rowcheckUAdd" runat="server"
                                visible="false">
                                <td colspan="2" style="color: Black; font-family: Optima; font-size: 12px; font-weight: normal;"
                                    align="justify">
                                    <asp:Label ID="lblUAdd" runat="server" Style="text-align: left;" Width="510px">
                            This combination of User Email and Address Description already exist.
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2" style="width: 50%">
                                <td style="width: 30%; text-align: right;">
                                    <asp:Label ID="lblPreFix" runat="server" Text="Prefix:" CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left">
                                    <asp:TextBox ID="txtPrefix" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                        runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 50%">
                                <td style="width: 30%; text-align: right;">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left">
                                    <asp:TextBox ID="txtFirstName" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                        runat="server"></asp:TextBox>

                                </td>
                            </tr>
                            <tr class="AlterNateColor2" style="width: 50%">
                                <td style="width: 30%; text-align: right;">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left">
                                    <asp:TextBox ID="txtLastName" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                        runat="server"></asp:TextBox>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 50%">
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
                                    <asp:TextBox ID="txtCompanyName" Style="width: 300px; font-size: 11px;" MaxLength="50"
                                        runat="server"></asp:TextBox>
                                    <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtCompanyName"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
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
                                    <asp:DropDownList ID="ddlCountry" runat="server" CssClass="DropDown" AutoPostBack="true"
                                        Font-Size="11px" Width="300px" Height="20px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2" style="width: 50%">
                                <td style="width: 30%; text-align: right;">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lblState" runat="server" Text="State:" CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left">
                                    <asp:TextBox ID="txtState" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                        runat="server" Visible="false"></asp:TextBox>
                                    <asp:DropDownList ID="ddlState" runat="server" CssClass="DropDown" Width="300px"
                                        Font-Size="11px" Height="20px" Visible="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="width: 50%">
                                <td style="width: 30%; text-align: right;">
                                    <span style="color: Red; font-size: 14px;">*</span>
                                    <asp:Label ID="lblCity" runat="server" Text="City:" CssClass="NormalLabel"></asp:Label>
                                </td>
                                <td style="width: 65%; text-align: left">
                                    <asp:TextBox ID="txtCity" Style="width: 160px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="txtCity"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
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
                                    <asp:Button ID="btnReg" runat="server" CssClass="ButtonWMarigin" Text="Add User"
                                        OnClientClick="return checkPassword()" />
                                    <asp:Button ID="btnCancel" runat="server" CssClass="ButtonWMarigin" Text="Cancel" Visible="false"
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
                <tr id="rowEditUser" runat="server" visible="false" align="center">
                    <td>
                        <table width="80%" cellpadding="1px" cellspacing="1px">
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
                                        runat="server" Enabled="false"></asp:TextBox>
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
                                    <asp:DropDownList ID="ddlStateE" runat="server" CssClass="DropDown" Font-Size="11px"
                                        Width="300px" Height="20px" Visible="true">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtStateE" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                        runat="server"></asp:TextBox>
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

                                    <asp:Button ID="btnCancelE" runat="server" CssClass="Button" Text="Cancel"
                                        Height="26px" Width="60px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField ID="hidAddDes" runat="server" />
            <asp:HiddenField ID="hidAddID" runat="server" />
            <asp:HiddenField ID="hidBillToShipToid" runat="server" />
            <asp:HiddenField ID="hvUserGrd" runat="server" />
            <asp:HiddenField ID="hvUserGrd1" runat="server" />
        </div>
    </form>
</body>
</html>
