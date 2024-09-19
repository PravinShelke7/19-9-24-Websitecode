<%@ Page Title="Web Conference Shopping Cart" Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master"
    AutoEventWireup="false" CodeFile="ShoppingCart.aspx.vb" Inherits="WebConferenceN_ShoppingCart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="JavaScript" src="https://seals.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function cardValidation() {
            var IsNumber = true;
            var IsAuthNumber = true;
            var ValidChars = "0123456789";
            var length;
            var AttAdd1 = document.getElementById('<%=hdnlnkAtAdd1.ClientID%>').value;
            var AttAdd2 = document.getElementById('<%=hdnlnkAtAdd2.ClientID%>').value;
            var AttAdd3 = document.getElementById('<%=hdnlnkAtAdd3.ClientID%>').value;
            var AttAdd4 = document.getElementById('<%=hdnlnkAtAdd4.ClientID%>').value;
            var AttAdd5 = document.getElementById('<%=hdnlnkAtAdd5.ClientID%>').value;
            var AttAdd6 = document.getElementById('<%=hdnlnkAtAdd6.ClientID%>').value;
            var AttAdd7 = document.getElementById('<%=hdnlnkAtAdd7.ClientID%>').value;
            var AttAdd8 = document.getElementById('<%=hdnlnkAtAdd8.ClientID%>').value;
            var AttAdd9 = document.getElementById('<%=hdnlnkAtAdd9.ClientID%>').value;
            var AttAdd10 = document.getElementById('<%=hdnlnkAtAdd10.ClientID%>').value;

            var BillToadd = document.getElementById('<%=hdnBillAddId.ClientID%>').value;
            //var CardHadd = document.getElementById('<%=hdnCardHAddId.ClientID%>').value;
            var CardMethod = document.getElementById('<%=rdbCreditcard.ClientID%>').value;
           
            if (AttAdd1 == 0) {
                alert("Please select at least one attendee.");
                return false;
            }
             var confType="<%=Session("FConType")%>";   
            // alert(confType);  
            if (confType == "Free") 
            {
                return true;
            }
            if (BillToadd == 0) {
                alert("Please Select Bill To Address");
                return false;
            }

            var AuthCode = document.getElementById('<%=txtAuthCode.ClientID%>').value;
            
            if (document.getElementById('<%=rdbInvoice.ClientID%>').checked == true) {
                return true;
            }
//            if (CardHadd == 0) {

//                alert("Please Select Card Holder Address.");
//                return false;

//            }

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

        
        function registermsg()
        {
         if (cardValidation()==true) 
          { 
            var msg;
            
            if (document.getElementById('<%=hdnIsLogINuser.ClientID%>').value=='N')
             {
                var name=document.getElementById('<%=hdnUserName.ClientID%>').value;
                msg=confirm(""+name+" has not registered for the conference. To register "+name+", click cancel, and select "+name+" as an attendee. Otherwise click ok."); 
                if (msg==true)
                 {
                    return true;
                    }
                 else
                {
                    return false;
                 }
                  return false;
                 }
          }
          else
          {
           return false;
          }
        }

        function disableCrditRows(obj) {
            if (obj.checked == true) {
                document.getElementById('<%=rowCardNo.ClientID%>').style.display = 'none';
                document.getElementById('<%=rowExpDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=rowCardType.ClientID%>').style.display = 'none';
                //document.getElementById('<%=rowCardHolderAdd.ClientID%>').style.display = 'none';
                document.getElementById('<%=trSecurityCode.ClientID%>').style.display = 'none';


            }

        }
        function enableCrditRows(obj) {
            if (obj.checked == true) {
                document.getElementById('<%=rowCardNo.ClientID%>').style.display = 'inline';
                document.getElementById('<%=rowExpDate.ClientID%>').style.display = 'inline';
                document.getElementById('<%=rowCardType.ClientID%>').style.display = 'inline';
                //document.getElementById('<%=rowCardHolderAdd.ClientID%>').style.display = 'inline';
                document.getElementById('<%=trSecurityCode.ClientID%>').style.display = 'inline';
            }
            else {
                document.getElementById('<%=rowCardNo.ClientID%>').style.display = 'none';
                document.getElementById('<%=rowExpDate.ClientID%>').style.display = 'none';
                document.getElementById('<%=rowCardType.ClientID%>').style.display = 'none';
                //document.getElementById('<%=rowCardHolderAdd.ClientID%>').style.display = 'none';
                document.getElementById('<%=trSecurityCode.ClientID%>').style.display = 'none';
            }

        }
        function OpenNewWindow(Page, PageName) {

            var width = 750;
            var height = 580;
            var left = (screen.width - width) / 2;
            var top = 20;//(screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, PageName, params);

            return false;
        }
    </script>
    <div class="MnContentPage">
        <%-- <div style="font-family: Optima; font-size: 12px">
            Please enter your credit card information.<br />
            <br />
        </div>--%>
        <table style="width: 580px; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td class="WebInnerTd" style="width: 366px">
                    <b>Order Number:</b>
                    <asp:Label ID="lblRefNumber" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
                <td class="WebInnerTd">
                    <%--<asp:HyperLink ID="hypUser" runat="server" Text="Edit Profile" CssClass="Link" Target="_blank"></asp:HyperLink>--%>
                </td>
            </tr>
            <tr>
                <td class="WebInnerTd" colspan="2">
                    <b>Order Amount:</b> US$<asp:Label ID="lblAmt" runat="server" CssClass="NormalLabel"></asp:Label>
                </td>
            </tr>
        </table>
        <%-- <br />--%>
        <table style="width: 590px; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td class="WebInnerTd" style="font-family: Optima; font-size: 12px; width: 328px;">
                    <%--    <b>Note:</b> Please refresh the page to see updated profile.--%>
                </td>
                <td class="WebInnerTd">
                    <%--<asp:LinkButton style="TEXT-DECORATION: none;font-size:12px;margin-right:105px;text-decoration:underline " id="lnkUserAddress" CssClass="Link" href="../Users_Login/UserAddress.aspx?PrevPage=ShoppingCart.aspx" target="_self" runat="server"  >Add/Edit Address</asp:LinkButton>                     --%>
                    <asp:LinkButton Style="text-decoration: none; font-size: 12px; width: 300px; text-decoration: underline"
                        ID="lnkUserAddress" CssClass="Link" href="AddNewUser.aspx" Visible="false" target="_blank"
                        runat="server">Add BillTo/ShipTo/CardHolder User</asp:LinkButton>
                    <%-- <asp:HyperLink ID="HyperLink1" runat="server" Text="Edit Profile" CssClass="Link" Target="_blank" ></asp:HyperLink>--%>
                </td>
            </tr>
        </table>
        <table style="width: 610px; text-align: left" cellpadding="2" cellspacing="2">
            <tr>
                <td style="width: 80%">
                    <table cellpadding="0" cellspacing="2" style="width: 100%">
                        <tr class="AlterNateColor4">
                            <td align="center" class="TdHeading" colspan="2">
                                <asp:Label ID="lblOrderInformation" runat="server" Width="100%" ToolTip="You can register up to 10 people for the web conference on this screen."><b>Order Information</b></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%" class="WebInnerTd">
                                User Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblName" runat="server" CssClass="NormalLabel" ToolTip="You must register as an attendee in order to attend."></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%" class="WebInnerTd">
                                User Email:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%" class="WebInnerTd">
                                Company Name:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:Label ID="lblCompName" runat="server" CssClass="NormalLabel"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 1:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:DropDownList ID="DropDownList1" runat="server" Style="font-family: Optima; font-size: 11px;"
                                    Width="300px" Visible="false">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkAtAdd1" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 2:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd2" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 3:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd3" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 4:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd4" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 5:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd5" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 6:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd6" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 7:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd7" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 8:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd8" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 9:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd9" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Attendee 10:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkAtAdd10" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="rowBilltoAdd" runat="server" visible="True">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Bill To Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:DropDownList ID="ddlBillTo" runat="server" Style="font-family: Optima; font-size: 11px;"
                                    Width="300px" Visible="false">
                                </asp:DropDownList>
                                <asp:LinkButton ID="lnkBillAdd" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="rowCardHolderAdd" runat="server" visible="false">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Card Holder Address:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:LinkButton ID="lnkCardHAdd" runat="server" CssClass="Link">No One Selected</asp:LinkButton>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="rowPayType" runat="server" visible="True">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Select Payment Type:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <input type="radio" runat="server" id="rdbCreditcard" checked="True" />Pay
                                with Credit Card
                                 <%--<input type="radio" runat="server" id="rdbInvoice" onclick="disableCrditRows(this)" />Invoice
                                Me--%>
                                <input type="radio" runat="server" id="rdbInvoice" onclick="disableCrditRows(this)" visible="false"  />
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
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNameOnC"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="rowCardNo" runat="server" visible="True">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Credit Card Number:
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:TextBox ID="txtCC" runat="server" CssClass="LongTextBox" MaxLength="25" Style="text-align: left"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCC"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" id="trSecurityCode" runat="server" visible="True">
                            <td style="font-weight: bold; width: 37%; color: Red" class="WebInnerTd">
                                * Security Code (CVV):
                            </td>
                            <td align="left" class="WebInnerTd">
                                <asp:TextBox ID="txtAuthCode" runat="server" CssClass="LongTextBox" Width="70px"
                                    MaxLength="10" Style="text-align: left"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAuthCode"
                                    ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1" id="rowExpDate" runat="server" visible="True">
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
                        <tr class="AlterNateColor2">
                            <td style="font-weight: bold; width: 37%" class="WebInnerTd">
                            </td>
                            <td align="left" class="WebInnerTd">
                                <%--<asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Continue Order Process" OnClientClick="return cardValidation();"  />                    --%>
                                <asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Continue Order Process" width="200px"
                                    OnClientClick="return registermsg();" />
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                    Style="margin-left: 5px;" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 20%">
                   <%-- <script type="text/javascript">                        SiteSeal("https://seal.networksolutions.com/images/colorsealbasic.gif", "NETSB", "none");</script>--%>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hdnBillAddId" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnCardHAddId" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkCardHAddDes" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkBillAddDes" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd1" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd2" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd3" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd4" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd5" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd6" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd7" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd8" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd9" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd10" runat="server" Value="0" />
                    <asp:HiddenField ID="hdnlnkAtAdd1Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd2Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd3Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd4Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd5Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd6Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd7Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd8Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd9Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnlnkAtAdd10Des" runat="server" Value="No One Selected" />
                    <asp:HiddenField ID="hdnUserName" runat="server" />
                    <asp:HiddenField ID="hdnUserId" runat="server" />
                    <asp:HiddenField ID="hdnIsLogINuser" runat="server" />
                </td>
                <td>
                    <asp:Button ID="btnRef" runat="server" Text="postback" Style="display: none;" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
