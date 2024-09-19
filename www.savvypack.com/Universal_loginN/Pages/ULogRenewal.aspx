<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ULogRenewal.aspx.vb" Inherits="Universal_loginN_Pages_ULogRenewal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Log Renewal</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScripts/jquery-1.4.1.min.js" type="text/javascript"></script>
    <script src="../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text" || txtarray[i].type == "password") {
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
    <script type="text/javascript">
    function ValidateUser()
        {
           var UserNameText= document.getElementById("<%=txtUserName.ClientID %>").value;
           var PasswordText= document.getElementById("<%=txtPassword.ClientID %>").value;  
           var UserName= "<%=Session("UserName")%>";   
           var Password= "<%=Session("Password")%>";  
            if(UserNameText=="")
            {
              alert('Please enter your email address.');
               return false;
            }
            else if (PasswordText=="")
            {
                alert('Please enter your password.');
            }
            else
            {
              return true;
//               if(UserNameText==UserName && PasswordText==Password)
//                {
//                  return true;             
//                }
//                else
//                {
//                   alert('Renewal Login ID,User Name does not match with Logged In User!');
//                   return false;
//                }
            }
                                      
            
           
        }
        javascript: window.history.forward(1); 
    
        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;


        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
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
    <style type="text/css">
        .style1
        {
            width: 10%;
            height: 29px;
        }
        .style2
        {
            width: 4%;
        }
    </style>
</head>
<body style="margin-top: 5px">
    <form id="form1" runat="server">
    <div id="MasterContent">
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/SavvyPackLogoB.gif"
                Height="45px" runat="server" />
        </div>
        <div>
            <table class="ULoginModule1" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                    <center>
                    </center>
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 10px;">
            </td>
        </tr>
        <tr>
            <td>
                <div style="text-align: center;">
                    <center>
                        <table style="width: 74%">
                            <tr>
                                <td>
                                    <table cellpadding="1px" cellspacing="1px" style="width: 92%;">
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                Logon Renewal
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="text-align: right;" class="style2">
                                                <asp:Label ID="lblUserName" runat="server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtUserName" Style="width: 380px; margin-left: 0px;" MaxLength="60"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" style="width: 50%;">
                                            <td style="text-align: right;" class="style2">
                                                <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                            </td>
                                            <td style="width: 10%; text-align: left;">
                                                <asp:TextBox ID="txtPassword" TextMode="Password" Style="width: 380px" MaxLength="25"
                                                    runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" style="width: 50%">
                                            <td colspan="2" style="text-align: center;" class="style1">
                                                <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" OnClientClick="return ValidateUser()" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="height: 10px;">
                                </td>
                            </tr>
                        </table>
                    </center>
                </div>
            </td>
        </tr>
        <tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
