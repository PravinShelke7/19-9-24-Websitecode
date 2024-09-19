<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="ForgotPassword.aspx.vb" Inherits="Users_Login_ForgotPassword" Title="Forget Password?" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function checkValidation() {

            var Email = document.getElementById('<%= txtEmail.ClientID%>').value;



      var errorMsg1 = "";
      var errorMsg = "";
      var space = " ";

      if (Email == "") {
          errorMsg1 += "\Email cannot be blank.\n";
      }

      if (errorMsg1 != "") {
          errorMsg += alert(errorMsg1);
          return false;
      }
      else {

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

  function ShowAccountUpdation(Page) {
      var msg = "Your account information must be completed in order to proceed. Please click ok to proceed to the account input screen."
      if (confirm(msg)) {

          newwin = window.open(Page, 'Chat', "");
          //window.close();
          return true;
      }
      else {
          return false;
      }

  }

  function ShowAccountAdd(Page) {
      var msg = "This username doesn't exist, Please create an account. Click OK to proceed."
      if (confirm(msg)) {

          newwin = window.open(Page, 'Chat', "");
          //window.close();
          return true;
      }
      else {
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



    <table class="ContentPage" id="tblPassword" runat="server" cellpadding="0" cellspacing="5" width="100%" visible="false">
        <tr>
            <td style="font-family: verdana; font-weight: bold; height: 30px; font-size: 16px; font-family: Arial" align="left" valign="middle">
                <asp:Label ID="lblPassMessage" runat="Server"></asp:Label>
            </td>
        </tr>



    </table>
    <table class="ContentPage" id="ContentPage" runat="server" cellpadding="0" cellspacing="5" width="100%">
        <tr>
            <td>
                <div style="font-weight: bold; font-family: Arial; font-size: 18px; margin-left: 15px;">
                    Forgot your Password? 
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="font-weight: bold; font-family: optima; font-size: 15px; margin-left: 15px; width: 100%;">
                    Just enter your email address to get a Password.
                </div>

            </td>
        </tr>
        <tr>
            <td align="center">
                <table width="75%">

                    <tr style="background-color: #000000; height: 12px;">
                        <td colspan="2"></td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td style="width: 15%; text-align: right;">
                            <asp:Label ID="lblEmail" runat="Server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                        </td>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtEmail" runat="Server" Width="350px" MaxLength="60" Style="font-size: 11px; height: 16px;"></asp:TextBox>

                        </td>
                    </tr>

                    <tr class="AlterNateColor1">
                        <td colspan="2" align="center">
                            <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Style="margin-left: -20px" Text="Submit" Width="90px" OnClientClick="return checkValidation();" />

                        </td>
                    </tr>
                </table>
            </td>
        </tr>

        <tr>
            <td style="height: 20px"></td>
        </tr>


    </table>
</asp:Content>
