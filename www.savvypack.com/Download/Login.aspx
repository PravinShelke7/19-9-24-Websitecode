<%@ Page Title="Login" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master"
    AutoEventWireup="false" CodeFile="Login.aspx.vb" Inherits="DownLoad_Login" %>

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
      function ShowPopWindow(flag) {

            if (flag == "pass") {
             document.getElementById('<%=btnSubmit.ClientID %>').focus();
       document.getElementById("<%=txtEmail.ClientID %>").focus();
                newwin = window.open('../Users_Login/ForgotPassword.aspx');
                return false;
            }

        }
  function checkValidation() 
     {          
           
           var Email  = document.getElementById('<%= txtEmail.ClientID%>').value      ; 
           var Pass  = document.getElementById('<%= txtPass.ClientID%>').value      ; 
           
     
            var errorMsg1 = "";
            var errorMsg = "";
            var space  = " ";     
             
                 if(Email=="")
                  {
                    errorMsg1 += "\Email can not be blank.\n";                                         
                  } 
                  if(Pass=="")
                  {
                    errorMsg1 += "\Password can not be blank.\n";                                         
                  } 
                  
                 if (errorMsg1 != "")
                 {
                      msg = "---------------------------------------------------------------------\n";
                      msg += "                 Please correct the problem(s).\n";
                      msg += "---------------------------------------------------------------------\n";
                      errorMsg += alert(msg + errorMsg1 + "\n\n");
                      return false;
                 }
                 else
                 {
                 
                   return true;
                 }
           
                 
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

    <div>
        <table>
            <tr>
                <td>
                    <span style="font-weight: bold; font-family: Verdana; font-size: 14px; margin-left: 18px;">
                        You must logon to download files.</span>
                    <br />
                    <br />
                    <span style="font-weight: noemal; font-family: Verdana; font-size: 12px; margin-left: 18px;
                        color: Black;">To ensure that our limited resources are not abused we must ask that
                        all members wishing to download files log in first.</span>
                    <br />
                    <br />
                </td>
            </tr>
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
                            </td>
                        </tr>
                        <tr class="AlterNateColor2" style="width: 100%">
                            <td style="text-align: right;">
                                <asp:Label ID="lblPasswod" runat="Server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                            </td>
                            <td style="text-align: left;">
                                <asp:TextBox ID="txtPass" runat="Server" TextMode="Password" Width="360px" Style="font-size: 11px;
                                    height: 16px;" MaxLength="25"></asp:TextBox>
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
                    <table>
                        <tr>
                            <td style="width: 100%;" align="left">
                                <table>
                                    <tr>
                                        <td colspan="2">
                                            <table>
                                                <tr>
                                                    <td style="width: 180px">
                                                    </td>
                                                    <td style="width: 380px; font-weight: bold" align="left">
                                                        <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="LinkF" Text="Forget your password?"
                                                            OnClientClick="return ShowPopWindow('pass');"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="height: 10px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="height: 10px">
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
    </div>
</asp:Content>
