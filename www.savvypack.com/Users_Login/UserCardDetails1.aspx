<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserCardDetails1.aspx.vb"
    Inherits="Users_Login_UserCardDetails1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Card Details</title>
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
    <script type="text/javascript" language="javascript">
        function cardValidation() {
            var IsNumber = true;
            var IsAuthNumber = true;
            var ValidChars = "0123456789";
            var length;

            var AuthCode = document.getElementById('<%=txtAuthCode.ClientID%>').value;
            var cardNumber = document.getElementById('<%=txtCC.ClientID%>').value;
            var cardType = document.getElementById('<%=ddlCCType.ClientID%>').value;
            var cardMonth = document.getElementById('<%=ddlMonth.ClientID%>').value;
            var cardYear = document.getElementById('<%=ddlYears.ClientID%>').value;

            if (cardNumber == "") {
                alert("Please enter card number");
                return false;
            }
            else {

                for (i = 0; i < cardNumber.length; i++) {
                    Char = cardNumber.charAt(i);
                    if (ValidChars.indexOf(Char) == -1) {
                        IsNumber = false;
                        break;
                    }

                }
                for (i = 0; i < AuthCode.length; i++) {
                    Char = AuthCode.charAt(i);
                    if (ValidChars.indexOf(Char) == -1) {
                        IsAuthNumber = false;
                        break;
                    }

                }

                if (IsNumber == true) {

                    if (AuthCode == "") {
                        alert("Please enter Security Code");
                        return false;
                    }
                    else {
                        if (IsAuthNumber == true) {
                            var d = new Date();

                            if (cardYear <= d.getFullYear()) {

                                if (cardMonth < d.getMonth() + 1) {
                                    alert('This Card has already expired.');
                                    return false;
                                }
                                else {
                                    return true;
                                }
                            }
                            else {
                                return true;
                            }
                        }
                        else {
                            alert("Please enter valid Security Code.");
                            return false;
                        }
                    }
                }
                else {
                    alert("Please enter valid card number.");
                    return false;
                }
            }
            return true;
        }

        function CloseWindow(Type) {
            window.close();
            window.opener.document.getElementById('ctl00_ContentPlaceHolder1_btnRefresh').click();
        }
         
    </script>
</head>
<body style="background-color: #F1F1F2;">
    <form id="form1" runat="server">
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
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                Card Details
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" id="rowCardType" runat="server" visible="True">
                                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                                * Credit Card Type:
                                            </td>
                                            <td align="left" class="WebInnerTd">
                                                <asp:DropDownList ID="ddlCCType" runat="server" Font-Size="11px">
                                                    <asp:ListItem Text="VISA" Value="VISA"></asp:ListItem>
                                                    <asp:ListItem Text="MASTERCARD" Value="MASTERCARD"></asp:ListItem>
                                                    <asp:ListItem Text="AMERICAN EXPRESS" Value="AMERICAN EXPRESS" Selected="True"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" id="Tr1" runat="server" visible="false">
                                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                                * Person's Name on Card:
                                            </td>
                                            <td align="left" class="WebInnerTd">
                                                <asp:TextBox ID="txtNameOnC" runat="server" CssClass="LongTextBox" MaxLength="50"
                                                    Style="text-align: left"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" id="rowCardNo" runat="server" visible="True">
                                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                                * Credit Card Number:
                                            </td>
                                            <td align="left" class="WebInnerTd">
                                                <asp:TextBox ID="txtCC" runat="server" CssClass="LongTextBox" MaxLength="25" Style="text-align: left"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" id="trSecurityCode" runat="server" visible="True">
                                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                                * Security Code (CVV):
                                            </td>
                                            <td align="left" class="WebInnerTd">
                                                <asp:TextBox ID="txtAuthCode" runat="server" CssClass="LongTextBox" Width="70px"
                                                    MaxLength="10" Style="text-align: left"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" id="rowExpDate" runat="server" visible="True">
                                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                                * Expiration Date:
                                            </td>
                                            <td align="left" class="WebInnerTd">
                                                <asp:DropDownList ID="ddlMonth" Width="40px" runat="server" Font-Size="11px">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddlYears" Width="50px" runat="server" Font-Size="11px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="font-weight: bold; width: 37%" class="WebInnerTd">
                                            </td>
                                            <td align="left" class="WebInnerTd">
                                                <asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Update"
                                                    OnClientClick="return cardValidation();" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                    Style="margin-left: 5px;" />
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
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
