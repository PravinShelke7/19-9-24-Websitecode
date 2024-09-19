<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ContactUs.aspx.vb" Inherits="Popup_ContactUs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Contact Us</title>
    <style type="text/css">
        .NormalTextBox
        {
            font-family: Verdana;
            font-size: 11px;
            height: 15px;
            width: 170px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
        }
        
        .works_Info
        {
            margin: 2.5% auto 0 auto;
            margin-left: 45px;
            margin-top: 20px;
            padding: 5em 0;
            background-color: #ff9966;
            border-radius: 25px;
            border: outset #CCC;
            padding: 13px;
            width: 310px;
            position: fixed;
        }
        
        .testimonial
        {
            margin-top: 250px;
            margin-left: 45px;
            border-radius: 25px;
            border: outset #CCC;
            padding: 7px;
            padding-left: 13px;
            width: 310px;
            height: 80px;
            position: fixed;
            background-color: #FEFCA1;
        }
    </style>
    <script type="text/javascript">
        function checkPassword() {

            var email = document.getElementById('<%= txtEmail.ClientID%>').value;
            var FirstName = document.getElementById('<%= txtFName.ClientID%>').value;
            var LastName = document.getElementById('<%= txtLName.ClientID%>').value;
            var errorMsg1 = "";
            var msg = "";
            var errorMsg = "";

            if (FirstName == "") {
                errorMsg1 += "\First Name can not be blank.\n";

            }
            if (LastName == "") {
                errorMsg1 += "\Last Name can not be blank.\n";

            }
            if (email == "") {
                errorMsg1 += "\Email Address can not be blank.\n";

            }
            if (errorMsg1 != "") {
                msg = "-----------------------------------------------------\n";
                msg += "Please correct the following problem(s).\n";
                msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg + errorMsg1 + "\n\n");
                return false;
            }


            if (email == "") {

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

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 350;
            var height = 140;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
        function ClosePopup() {

            //add the required functionality
            window.parent.HideModalPopup();
            window.close();
            return false;
        }

        function myFunction() {
            var txt;
            if (confirm("Press a button!")) {
                window.parent.HideModalPopup();

            } else {
                window.close();
            }

        }
    </script>
</head>
<body style="background: lightgray;">
    <form id="form1" runat="server">
   <%-- <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="divUpdateprogress">
                        <table>
                            <tr>
                                <td>
                                    <img alt="" src="../Images/Loading4.gif" height="50px" />
                                </td>
                                <td>
                                    <b style="color: Red;">Sending email...</b>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>--%>
            <div id="Div1" runat="Server" class="works_Info" style="text-align: justify; font-size: 13px;
                font-weight: normal; font-family: Verdana; color: Black;">
                <table>
                    <tr>
                        <td colspan="2" style="padding-bottom: 3%;">
                            If you would like more information on SavvyPack<sub>®</sub>, please provide your name and email address.
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 11px;" align="right">
                            <b>First Name:</b>
                        </td>
                        <td style="width: 0px;" align="left">
                            <asp:TextBox ID="txtFName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 11px;" align="right">
                            <b>Last Name:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtLName" runat="server" MaxLength="25" CssClass="NormalTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-size: 11px;" align="right">
                            <b>Email Address:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtEmail" runat="server" MaxLength="60" CssClass="NormalTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="Button" OnClientClick="return checkPassword()" />
                            <asp:Button ID="btnClos" runat="server" Text="Submit" Style="display: none;" CssClass="Button"
                                OnClientClick="return ClosePopup()" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="Div2" runat="Server" class="testimonial" style="text-align: justify; padding-bottom: 5px;
                font-size: 11px; font-weight: normal; font-family: Verdana; color: Black;">
                <td colspan="2">
                    <span style="font-weight: bold; color: #825f05; font-family: Verdana; font-size: 14px;">
                        Questions?</span><br />
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        Call us at [1] 952-405-7500 or</span>
                    <br />
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        email us at <a style="text-decoration: none; font-style: italic; font-weight: bold"
                            class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a></span>
                </td>
            </div>
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    </form>
</body>
</html>
