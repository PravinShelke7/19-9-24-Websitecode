<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false"
    CodeFile="UpdateUsers.aspx.vb" Inherits="Users_Login_UpdateUsers" Title="Account Update" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
  function ShowConfirmMessage()
     {
        alert('Thank you for completing your account. You can now use your account.');
        window.close();
     }
     
     function checkPassword () 
     {
               
          
          var FirstName  = document.getElementById('<%= txtFirstName.ClientID%>').value      ; 
           var LastName  = document.getElementById('<%= txtLastName.ClientID%>').value      ; 
           var phone  = document.getElementById('<%= txtphone.ClientID%>').value      ; 
            var mphone = document.getElementById('<%= txtMob.ClientID%>').value;
           var CompanyName  = document.getElementById('<%= txtCompanyName.ClientID%>').value      ; 
           var SAdress1  = document.getElementById('<%= txtSAdress1.ClientID%>').value      ; 
           var City  = document.getElementById('<%= txtCity.ClientID%>').value      ; 
         
           var ZipCode  = document.getElementById('<%= txtZipCode.ClientID%>').value      ; 
          
            var Password  = document.getElementById('<%= txtPassword.ClientID%>').value      ; 
            var ConfirmPassword  = document.getElementById('<%= txtConfirmPassword.ClientID%>').value      ; 
            var country = document.getElementById('<%= ddlCountry.ClientID%>'); 
            var cName = country.options[country.selectedIndex].text;
            var State="";
            var StateVal="";
             if(cName=="United States")
           { 
               
                  var StateVal= document.getElementById('<%= ddlState.ClientID%>'); 
                  StateVal = StateVal.options[StateVal.selectedIndex].value;
           }
           else
           {
                 
                  State = document.getElementById('<%= txtState.ClientID%>').value ; 
                  if(State=="")
                  {
                     StateVal=0;
                  }
           }
           
           fieldvalue  = document.getElementById('<%= txtPassword.ClientID%>').value      ; 
            var errorMsg1 = "";
            var errorMsg = "";
            var space  = " ";          
           
            fieldvalue  = document.getElementById('<%= txtPassword.ClientID%>').value      ; 
            //alert(fieldvalue.length);
            fieldlength = fieldvalue.length; 

            //It must not contain a space
                       
            if(Password=="")
            {
                errorMsg1 += "\Password can not be blank.\n";
                  msg = "-----------------------------------------------------\n";
                  msg += "Please correct the problem(s).\n";
                  msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg+errorMsg1+"\n\n");
                return false;
            } 
            else
            {  
                    if(Password==ConfirmPassword)
                    {
                          //It must not contain a space
                            if (fieldvalue.indexOf(space) > -1) {
                                 errorMsg += "\nPasswords cannot include a space.\n";
                            }     

                            //It must contain at least one number character
                            if (!(fieldvalue.match(/\d/))) {
                                 errorMsg += "\nStrong passwords must include at least one number.\n";
                            }
                          
                            //It must contain at least one upper case character     
                            if (!(fieldvalue.match(/[A-Z]/))) {
                                 errorMsg += "\nStrong passwords must include at least one uppercase letter.\n";
                            }
                            //It must contain at least one lower case ch aracter
                            if (!(fieldvalue.match(/[a-z]/))) {
                                 errorMsg += "\nStrong passwords must include one or more lowercase letters.\n";
                            }
                           
                            //It must be at least 7 characters long.
                            if (!(fieldlength >= 8)) {
                                 errorMsg += "\nStrong passwords must be at least 8 characters long.\n";
                            }
                            if (fieldvalue.indexOf("'")!=-1)
                            {
                                 errorMsg += "\Password should not contain apostrophe.\n";
                            }
                            //If there is aproblem with the form then display an error
                             if (errorMsg != "")
                             {
                                  msg = "-----------------------------------------------------\n";
                                  msg += "Please correct the problem(s) with your password.\n";
                                  msg += "-----------------------------------------------------\n";
                                  errorMsg += alert(errorMsg1+msg + errorMsg + "\n\n");
                                  return false;
                             }
                             else
                             {
                                    
                                     if(FirstName=="")
                                      {
                                        errorMsg1 += "\First Name can not be blank.\n";                                         
                                      } 
                                      if(LastName=="")
                                      {
                                        errorMsg1 += "\Last Name can not be blank.\n";                                         
                                      } 
                                      if(phone=="")
                                      {
                                        errorMsg1 += "\Phone number can not be blank.\n";                                         
                                      } 
                                       if(mphone=="")
                                      {
                                        errorMsg1 += "\Mobile number can not be blank.\n";                                         
                                      } 
                                      if(CompanyName=="")
                                      {
                                        errorMsg1 += "\CompanyName can not be blank.\n";                                         
                                      } 
                                       if(SAdress1 =="")
                                      {
                                        errorMsg1 += "\Street Address can not be blank.\n";                                         
                                      } 
                                      if(City =="")
                                      {
                                        errorMsg1 += "\City can not be blank.\n";                                         
                                      } 
//                                      if(State =="")
//                                      {
//                                        errorMsg1 += "\State can not be blank.\n";                                         
//                                      } 
                                      if(ZipCode =="")
                                      {
                                        errorMsg1 += "\ZipCode can not be blank.\n";                                         
                                      } 
//                                       if(Country =="")
//                                      {
//                                        errorMsg1 += "\Country can not be blank.\n";                                         
//                                      } 
                                     if(eval(country.value) == eval(0))
                                      {
                                        errorMsg1 += "\Country must be specified.\n";                                         
                                      } 

                                      if(eval(StateVal)== eval(0))
                                      {
                                        errorMsg1 += "\State must be specified.\n";                                         
                                      } 
                                      
                                     if (errorMsg1 != "")
                                     {
                                          msg = "-----------------------------------------------------\n";
                                          msg += "Please correct the problem(s).\n";
                                          msg += "-----------------------------------------------------\n";
                                          errorMsg += alert(msg + errorMsg1 + "\n\n");
                                          return false;
                                     }
                                     else
                                     {
                                     
                                       return true;
                                     }
                             }      
                    }
                    else
                    {
                                errorMsg1="Password and Confirm Password should match.";
                                msg = "-----------------------------------------------------\n";
                                msg += "Please correct the problem(s) with your password.\n";
                                msg += "-----------------------------------------------------\n";
                                errorMsg += alert(msg+errorMsg1+"\n\n");
                                return false;
                    }
                    

                            
           }
                 
     }
      
        function MessageWindow()
        {
          var msg="";
            msg = "-----------------------------------------------------------------------------------------------------------------------------\n";
            msg += "A User account already exists for this email address..\n Please enter different email address to create a new account.\n Or, if you have forgotten your password to your original account, let us know and we will email it to you.\n";
            msg += "-----------------------------------------------------------------------------------------------------------------------------\n";
            alert(msg);
        }
         

 function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 200;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function CloseWindow() {
         alert("Thank you for updating your account. Your email address has been verified.  Your account is being reviewed for approval, and you will be notified when your account is activiated.");    
            window.close();
        }
          function CloseWindowAcp() 
          {  
            alert("Thank you for updating your account. Your email address has been verified, and you can now use your account.");    
           
            window.close();            
        }
    </script>

    <table class="ContentPage" id="ContentPage" runat="server" width="710px">
        <tr>
            <td align="left">
                <div style="width: 100%; margin-left: 120px;">
                    <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="Link" Text="Forget your password?"
                        Style="font-size: 15px; font-weight: bold; display: none"></asp:LinkButton>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div style="text-align: center; display: inline;" id="dvAddUser" runat="Server">
                    <table width="100%" style="text-align: center;">
                        <tr>
                            <td align="center">
                                <table width="70%" cellpadding="1px" cellspacing="1px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            SavvyPack Corporation Account
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 100%">
                                        <td style="width: 30%; text-align: right; height: 20px;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblEmail" runat="server" Text="Email:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left; height: 20px;">
                                            <asp:TextBox ID="txtEmail" Style="width: 370px; font-size: 11px;" runat="server"
                                                MaxLength="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblPassword" runat="server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtPassword" TextMode="Password" MaxLength="25" Style="width: 160px;
                                                font-size: 11px;" runat="server"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ID="rfvPassword" ControlToValidate="txtPassword" runat="Server"  ErrorMessage="*">
                                        </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblConfirmPassword" runat="server" Text="Confirm Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" MaxLength="25" Style="width: 160px;
                                                font-size: 11px;" runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvConfirmPassword" ControlToValidate="txtConfirmPassword"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <asp:Label ID="lblPreFix" runat="server" Text="Prefix:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtPrefix" Style="width: 100px; font-size: 11px;" MaxLength="10"
                                                runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblFirstName" runat="server" Text="First Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtFirstName" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvFirstName" ControlToValidate="txtFirstName"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblLastName" runat="server" Text="Last Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtLastName" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server"></asp:TextBox>
                                            <%-- <asp:RequiredFieldValidator ID="rfvLastName" ControlToValidate="txtLastName"  runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
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
                                    <tr class="AlterNateColor1" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblMob" runat="server" Text="Mobile No:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:TextBox ID="txtMob" Style="width: 300px; font-size: 11px;" MaxLength="25" runat="server"></asp:TextBox>
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
                                        <td style="width: 30%; text-align: right;">
                                        </td>
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
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="DropDown" Font-Size="11px"
                                                AutoPostBack="true" Width="300px" Height="20px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 50%">
                                        <td style="width: 30%; text-align: right;">
                                            <span style="color: Red; font-size: 14px;">*</span>
                                            <asp:Label ID="lblState" runat="server" Text="State:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <%--<asp:TextBox ID="txtState" style="width:160px;font-size:11px;" MaxLength="25" runat="server" ></asp:TextBox>--%>
                                            <asp:TextBox ID="txtState" Style="width: 160px; font-size: 11px;" MaxLength="25"
                                                runat="server" Visible="false"></asp:TextBox>
                                            <asp:DropDownList ID="ddlState" runat="server" CssClass="DropDown" Font-Size="11px"
                                                Width="300px" Height="20px" Visible="false">
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
                                        <td style="width: 30%; text-align: center;">
                                        </td>
                                        <td style="width: 65%; text-align: left">
                                            <asp:Button ID="btnReg" runat="server" CssClass="ButtonWMarigin" Text="Update" OnClientClick="return checkPassword()" />
                                            <asp:Button ID="btnCancel" runat="server" CssClass="ButtonWMarigin" Text="Cancel"
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
                        <tr>
                            <td colspan="2" style="height: 70px;">
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnClose" runat="server" Style="display: none;" />
      <asp:Button ID="btnCloseAcp" runat="server" Style="display: none;" />
</asp:Content>
