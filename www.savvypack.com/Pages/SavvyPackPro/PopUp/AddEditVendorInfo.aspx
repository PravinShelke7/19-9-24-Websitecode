<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddEditVendorInfo.aspx.vb" Inherits="Pages_SavvyPackPro_PopUp_AddEditVendorInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add/Edit Vendor</title>
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <script type="text/javascript">

        //window.onbeforeunload = WindowCloseHanlder;
        //function WindowCloseHanlder() {
        //    if (document.getElementById("hidUpdate").value == "1") {
        //        window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnRefresh').click();
        //    }
        //}

        function ClosePage() {
            window.close();
            window.opener.document.getElementById('btnRefreshVList').click();
        }
        function checkDetails() {

            var email = document.getElementById('txtEmail').value;
            var cnfemail = document.getElementById('txtCEmail').value;
            var FirstName = document.getElementById('txtFname').value;
            var LastName = document.getElementById('txtLname').value;
            var CompanyName = document.getElementById('txtCompany').value;
            var errorMsg1 = "";
            var errorMsg = "";
            var msg = "";

            
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

            if (FirstName == "") {
                errorMsg1 += "\First Name can not be blank.\n";
            }
            if (LastName == "") {
                errorMsg1 += "\Last Name can not be blank.\n";
            }
            if (CompanyName == "") {
                errorMsg1 += "\CompanyName can not be blank.\n";
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

        function MessageWindow() {
            var msg = "";
            msg = "-----------------------------------------------------------------------------------------------------------------------------\n";
            msg += " A Vendor account already exists for this email address..\n Please enter different email address to add a new vendor.\n ";
            msg += "-----------------------------------------------------------------------------------------------------------------------------\n";
            alert(msg);
        }

                      
        function CheckSP(text) {

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
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
    <style type="text/css">
        a.SavvyLink:link {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:visited {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:hover {
            color: Red;
            font-size: 11px;
        }

        #SavvyMasterContent {
            width: 610px;
        }

        .ContentPage {
            margin-top: 2px;
            margin-left: 1px;
            width: 855px;
            background-color: #D3E7CB;
        }

        .PageHeading {
            font-size: 18px;
            font-weight: bold;
            text-align: center;
        }

        #ContentPagemargin {
            margin-left: 20px;
            text-align: left;
        }

        #PageSection1 {
            background-color: #D3E7CB;
        }

        .AlterNateColor1 {
            background-color: #C0C9E7;
        }

        .AlterNateColor2 {
            background-color: #D0D1D3;
        }

        .AlterNateColor3 {
            background-color: #D3DAD0;
            height: 20px;
        }

        .PageSHeading {
            font-size: 12px;
            font-weight: bold;
        }

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

        .LongTextBox {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 320px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
        }

        .NormalTextBox {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 120px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
        }

        #dvAddUser {
            background-color: #D3E7CB;
        }

        #dvEditUSer {
            background-color: #D3E7CB;
        }

        .divUpdateprogress {
            position: absolute;
            top: 290px;
            left: 380px;
        }

        .SingleLineTextBox_G {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 15px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divUpdateprogress">
                            <table>
                                <tr>
                                    <td>
                                        <img alt="" src="../../../Images/Loading4.gif" height="50px" />
                                    </td>
                                    <td>
                                        <b style="color: Red;">Updating the Record</b>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="SavvyMasterContent">
                    <div id="error">
                        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                    </div>
                </div>
                <table class="ContentPage" id="ContentPage" runat="server">
                    <tr id="Tr1" runat="server">
                        <td id="Td1" runat="server">
                            <div class="PageHeading" id="divMainHeading">
                                <asp:Label ID="lblHeading" runat="server">Add Vendor</asp:Label>
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr2" style="height: 20px" runat="server">
                        <td id="Td2" runat="server">
                            <div id="ContentPagemargin" runat="server">
                                <div id="dvAddUser" style="text-align: left; display: inline;" runat="server">
                                    <table style="width: 100%; font-size: 13px;">
                                        <tr>
                                            <td>
                                                <table style="width: 100%; font-size: 13px;">
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">
                                                            <span style="color: Red; font-size: 12px;">*</span> Email:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtEmail" runat="server" CssClass="LongTextBox" MaxLength="60" AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr id="trUsername" runat="server" visible="False">
                                                        <td id="Td3" runat="server"></td>
                                                        <td id="Td4" align="left" runat="server">
                                                            <asp:Label ID="lblUserAv" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">
                                                            <span style="color: Red; font-size: 12px;">*</span> Confirm Email:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCEmail" runat="server" CssClass="LongTextBox" MaxLength="60"></asp:TextBox>
                                                        </td>
                                                    </tr>                                                  
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Country:
                                                        </td>
                                                        <td align="left">
                                                            <asp:DropDownList ID="ddlCountry" __designer:wfdid="w7" runat="server" Width="222px"
                                                                Style="font-size: 10px; font-family: Verdana;" AutoPostBack="True">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">State:
                                                        </td>
                                                        <td align="left">
                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:DropDownList ID="ddlState" __designer:wfdid="w7" runat="server" Width="222px"
                                                                        Style="font-size: 10px; font-family: Verdana;" Visible="false">
                                                                    </asp:DropDownList>
                                                                    <asp:TextBox ID="txtState" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                                        runat="server" Visible="false"></asp:TextBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Prefix:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPrefix" runat="server" CssClass="NormalTextBox" MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">
                                                            <span style="color: Red; font-size: 12px;">*</span> First Name:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFname" runat="server" CssClass="NormalTextBox" MaxLength="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">
                                                            <span style="color: Red; font-size: 12px;">*</span> Last Name:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtLname" runat="server" CssClass="NormalTextBox" MaxLength="25"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">[Country Code] Phone:
                                                        </td>
                                                        <td align="left">
                                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                                <ContentTemplate>
                                                                    <asp:Label ID="lblCountryCode" runat="server" Font-Bold="true" Font-Size="11px"></asp:Label>
                                                                    <asp:TextBox ID="txrtPhne" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="ddlCountry" EventName="SelectedIndexChanged" />
                                                                </Triggers>
                                                            </asp:UpdatePanel>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Mobile No:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtMob" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Fax:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtFax" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">
                                                            <span style="color: Red; font-size: 12px;">*</span> Company Name:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCompany" runat="server" MaxLength="50" CssClass="NormalTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Position:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtPosition" runat="server" MaxLength="50" CssClass="LongTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Street Address:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtStrAdd1" runat="server" MaxLength="50" CssClass="LongTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold"></td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtStrAdd2" runat="server" MaxLength="50" CssClass="LongTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">City:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtCity" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" style="font-weight: bold">Zip Code:
                                                        </td>
                                                        <td align="left">
                                                            <asp:TextBox ID="txtZip" runat="server" MaxLength="10" CssClass="NormalTextBox"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td align="left">
                                                            <asp:Button ID="btnAdd" runat="server" Text="Add Vendor" OnClientClick="return checkDetails()"></asp:Button>
                                                            <asp:Button ID="btnClose" runat="server" Text="Close" OnClientClick="javascript:window.close();"
                                                                Style="width: 61px; padding-left: 6px;" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right">
                                                            <span style="color: Red; font-size: 12px; margin-left: 10px;">* = required</span>
                                                        </td>
                                                        <td></td>
                                                    </tr>
                                                </table>
                                            </td>                                          
                                        </tr>
                                    </table>
                                </div>                               
                            </div>
                        </td>
                    </tr>
                    <tr id="Tr3" class="AlterNateColor3" runat="server">
                        <td id="Td17" class="PageSHeading" align="center" runat="server">
                            <asp:Label ID="lblTag" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hidUserID" runat="server" />
                <asp:HiddenField ID="hidUserNmEdit" runat="server" />
                <asp:HiddenField ID="hidCompanyNmEdit" runat="server" />
                <asp:HiddenField ID="hidLicenseId" runat="server" />
                <asp:HiddenField ID="hidLicenseDes" runat="server" />
                <asp:HiddenField ID="hidLicenseIdE" runat="server" />
                <asp:HiddenField ID="hidLicenseDesE" runat="server" />
                <asp:HiddenField ID="hidIDStatusId" runat="server" />
                <asp:HiddenField ID="hidIDStatusDes" runat="server" />
                <asp:HiddenField ID="hidIDStatusIdE" runat="server" />
                <asp:HiddenField ID="hidIDStatusDesE" runat="server" />
                <asp:HiddenField ID="hidUserGrpSortID" runat="server" />
                <asp:HiddenField ID="hidUpdate" runat="server" />
                <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
