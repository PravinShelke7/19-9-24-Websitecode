<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddEditUser.aspx.vb" Inherits="Universal_loginN_Pages_AddEditUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Add/Edit User</title>
     <script type="text/javascript" >
      function ValidateInfo()
      {
          var UserNameText= document.getElementById("<%=txtUserName.ClientID %>").value;
          var CUserNameText= document.getElementById("<%=txtConfirmUserName.ClientID %>").value;  
          
          var PasswordText= document.getElementById("<%=txtPassword.ClientID %>").value;
          var CPasswordText= document.getElementById("<%=txtConfirmPassword.ClientID %>").value;  
          
          if(UserNameText!="" && CUserNameText!="" && PasswordText!="" && CPasswordText!="")
          {
             if(UserNameText==CUserNameText)
              {
                  if(PasswordText==CPasswordText)
                  {                
                    return true;
                  }  
                  else
                  {
                    alert("The Password  and Confirm Password does not match,Please reenter the information!");
                    return false;
                  }   
              }   
              else
              {
                alert("The User Name  and Confirm User Name does not match,Please reenter the information!");
                return false;
              } 
          }
          else
          {
             return true;
          }
         
                 
      }
    </script>
      <script language = "Javascript" type="text/javascript">
         function MessageWindow()
        {
          var msg="";
            msg = "-----------------------------------------------------------------------------------------------------------------------------\n";
            msg += "A User account already exists for this email address..\n Please enter different email address to create a new account.\n Or, if you have forgotten your password to your original account, let us know and we will email it to you.\n";
            msg += "-----------------------------------------------------------------------------------------------------------------------------\n";
            alert(msg);
        }
        
     function checkRequiredField() 
     {
               
           var email  = document.getElementById('<%= txtUserName.ClientID%>').value      ; 
           var ConfEmail  = document.getElementById('<%= txtConfirmUserName.ClientID%>').value      ; 
           var FirstName  = document.getElementById('<%= txtFirstName.ClientID%>').value      ; 
           var LastName  = document.getElementById('<%= txtLastName.ClientID%>').value      ; 
         
            var Password  = document.getElementById('<%= txtPassword.ClientID%>').value      ; 
            var ConfirmPassword  = document.getElementById('<%= txtConfirmPassword.ClientID%>').value      ; 
           
           fieldvalue  = document.getElementById('<%= txtPassword.ClientID%>').value      ; 
            var errorMsg1 = "";
            var errorMsg = "";
            var space  = " ";          
           
            fieldvalue  = document.getElementById('<%= txtPassword.ClientID%>').value      ; 
            //alert(fieldvalue.length);
            fieldlength = fieldvalue.length; 

            //It must not contain a space
            if (email=="") {
                 errorMsg1 += "\     Email Address can not be blank.\n";                
                  msg = "-----------------------------------------------------\n";
                  msg += "          Please correct the problem(s).\n";
                  msg += "-----------------------------------------------------\n";
                 errorMsg += alert(msg+errorMsg1+"\n\n");
                 return false;
            } 
            else
            {
               
               var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
               var address = email;
               if(reg.test(address) == false) 
               {
                   errorMsg1 += "\    Email Address is not in proper format.\n";                
                   msg = "-----------------------------------------------------\n";
                   msg += "            Please correct the problem(s).\n";
                   msg += "-----------------------------------------------------\n";
                   errorMsg += alert(msg+errorMsg1+"\n\n");
                   return false;
               }
               else if(email!=ConfEmail)
               {
                   errorMsg1="User Name and Confirm User Name should match.";     
                   msg = "-----------------------------------------------------\n";
                   msg += "        Please correct the problem(s).\n";
                   msg += "-----------------------------------------------------\n";
                   errorMsg += alert(msg+errorMsg1+"\n\n");
                   return false;
               }

            } 
            
            if(Password=="")
            {
                errorMsg1 += "\  Password can not be blank.\n";
                  msg = "-----------------------------------------------------\n";
                  msg += "     Please correct the problem(s).\n";
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
                            //It must contain at least one lower case character
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
                                        errorMsg1 += "\     First Name can not be blank.\n";                                         
                                      } 
                                      if(LastName=="")
                                      {
                                        errorMsg1 += "\     Last Name can not be blank.\n";                                         
                                      } 
                                     
                                     if (errorMsg1 != "")
                                     {
                                          msg = "-----------------------------------------------------\n";
                                          msg += "         Please correct the problem(s).\n";
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
                                msg = "----------------------------------------------------------\n";
                                msg += "Please correct the problem(s) with your password.\n";
                                msg += "---------------------------------------------------------\n";
                                errorMsg += alert(msg+errorMsg1+"\n\n");
                                return false;
                    }
                    

                            
           }
                 
     }



    function checkPassword() 
     {           
          
         
            var Password  = document.getElementById('<%= txtEPassword.ClientID%>').value      ;             
           
          
            var errorMsg1 = "";
            var errorMsg = "";
            var space  = " ";          
           
            fieldvalue  = document.getElementById('<%= txtEPassword.ClientID%>').value      ; 
            //alert(fieldvalue.length);
            fieldlength = fieldvalue.length; 
            
            if(Password=="")
            {
                errorMsg1 += "\  Password can not be blank.\n";
                  msg = "-----------------------------------------------------\n";
                  msg += "     Please correct the problem(s).\n";
                  msg += "-----------------------------------------------------\n";
                errorMsg += alert(msg+errorMsg1+"\n\n");
                return false;
            } 
            else
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
                            //It must contain at least one lower case character
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
                                        errorMsg1 += "\     First Name can not be blank.\n";                                         
                                      } 
                                      if(LastName=="")
                                      {
                                        errorMsg1 += "\     Last Name can not be blank.\n";                                         
                                      } 
                                     
                                     if (errorMsg1 != "")
                                     {
                                          msg = "-----------------------------------------------------\n";
                                          msg += "         Please correct the problem(s).\n";
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
                            
                 
     }
    </script>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top:5px">
    <form id="form1" runat="server">
    <div id="MasterContent">
    
    <div id="AlliedLogo">
    <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
    </div>
    
    <div>
    <table class="ULoginModule1" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
        <tr>
             <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
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
            <br />
             <div class="PageHeading" id="divMainHeading" style="width:840px;">
                        <center></center> 
                     </div>
        
            </td>
        </tr>
              
    
        <tr>
            <td>
                <div style="text-align:center;display:inline;" id="dvAddUser" runat="Server"  >
                    <table width="100%" style="text-align:center;">
                    
                        <tr>
                            <td>
                                <table width="60%" cellpadding="1px" cellspacing ="1px">
                                
                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;" colspan="2">
                                                      Add New User
                                                    </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width:100%">
                                        <td style="width:30%;text-align:right; height: 20px;">
                                            <asp:Label ID="lblUserName" runat="server"  text="User Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width:65%;text-align:left; height: 20px;">
                                            <asp:TextBox ID="txtUserName" style="width:350px;font-size:11px;"  MaxLength="60" runat="server"></asp:TextBox>
                                           <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="rfvUserName" ControlToValidate="txtUserName" runat="Server" ErrorMessage="*" ValidationGroup="Add">
                                        </asp:RequiredFieldValidator>  
                                        <asp:RegularExpressionValidator
                                                   id="emailValidator" ValidationGroup="Add"
                                                   runat="server" Display="Dynamic"
                                                   ErrorMessage="Enter valid email adress."
                                                   ValidationExpression="^[\w\.\-]+@[a-zA-Z0-9\-]+(\.[a-zA-Z0-9\-]{1,})*(\.[a-zA-Z]{2,3}){1,2}$"
                                                   ControlToValidate="txtUserName">
                                               </asp:RegularExpressionValidator>--%>
                                                                            
                   
                                        </td>
                                       
                                    </tr>
                                      <tr class="AlterNateColor2" style="width:100%">
                                        <td style="width:30%;text-align:right;">
                                            <asp:Label ID="lblConfirmUserName" runat="server"  text="Confirm User Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width:65%;text-align:left">
                                            <asp:TextBox ID="txtConfirmUserName" style="width:350px;font-size:11px;"  MaxLength="60" runat="server" Font-Size="11px"></asp:TextBox>
                                            <%--   <asp:RequiredFieldValidator Display="Dynamic" ID="rfvConfirmUserName" ControlToValidate="txtConfirmUserName" runat="Server" ValidationGroup="Add" ErrorMessage="*">
                                        </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    
                                    <tr class="AlterNateColor1" style="width:50%">
                                        <td style="width:30%;text-align:right">
                                         <asp:Label ID="lblPassword" runat="server"  text="Password:" CssClass="NormalLabel"></asp:Label>   
                                        </td>
                                        <td style="width:65%;text-align:left">
                                        <asp:TextBox ID="txtPassword" TextMode="Password" style="width:150px;font-size:11px;" MaxLength="25" runat="server" Font-Size="11px"></asp:TextBox>
                                        <%--   <asp:RequiredFieldValidator Display="Dynamic" ID="rfvPassword" ControlToValidate="txtPassword" runat="Server" ValidationGroup="Add" ErrorMessage="*">
                                        </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                       <tr class="AlterNateColor2" style="width:50%">
                                        <td style="width:30%;text-align:right">
                                         <asp:Label ID="lblConfirmPassword" runat="server"  text="Confirm Password:" CssClass="NormalLabel"></asp:Label>   
                                        </td>
                                        <td style="width:65%;text-align:left">
                                            <asp:TextBox ID="txtConfirmPassword" TextMode="Password" style="width:150px;font-size:11px;" MaxLength="25" runat="server" Font-Size="11px"></asp:TextBox>
                                          <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="rfvConfirmPassword" ControlToValidate="txtConfirmPassword" ValidationGroup="Add" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width:50%">
                                        <td style="width:30%;text-align:right;">
                                            <asp:Label ID="lblFirstName" runat="server"  text="First Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width:65%;text-align:left">
                                            <asp:TextBox ID="txtFirstName" style="width:150px;font-size:11px;" MaxLength="25" runat="server" Font-Size="11px"></asp:TextBox>
                                           <%--   <asp:RequiredFieldValidator Display="Dynamic" ID="rfvFirstName" ControlToValidate="txtFirstName" ValidationGroup="Add" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                     <tr class="AlterNateColor2" style="width:50%">
                                        <td style="width:30%;text-align:right;">
                                            <asp:Label ID="lblLastName" runat="server"  text="Last Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width:65%;text-align:left">
                                            <asp:TextBox ID="txtLastName" style="width:150px;font-size:11px;" MaxLength="25" runat="server" Font-Size="11px"></asp:TextBox>
                                             <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="rfvLastName" ControlToValidate="txtLastName" ValidationGroup="Add" runat="Server" ErrorMessage="*">
                                           </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" >
                                        <td style="width:30%;text-align:center ;">
                                                                                  
                                        </td> 
                                        <td style="width:65%;text-align:left">
                                          <asp:Button ID="btnAddUser" runat="server" CssClass="ButtonWMarigin" Text="Add User" OnClientClick="return checkRequiredField();"  /> 
                                          <asp:Button ID="btnCancel" runat="server" CssClass="ButtonWMarigin" Text="Cancel" CausesValidation="false"/>
                                        </td>
                                       
                                    </tr>
                                     <tr class="AlterNateColor2" >
                                        <td style="width:100%;text-align:left;" colspan="2">
                                           <%--<asp:CompareValidator ID="cmpvUserName" ControlToValidate="txtUserName" ControlToCompare="txtConfirmUserName" runat="Server" Width="50%" ValidationGroup="Add" ErrorMessage="* The User Name  and Confirm User Name does not match." style="font-size:10px;">
                                           </asp:CompareValidator>  
                                            <asp:CompareValidator ID="cmpvPassword" ControlToValidate="txtPassword" ControlToCompare="txtConfirmPassword" Width="46%" runat="Server" ValidationGroup="Add" ErrorMessage="* The Password and Confirm Password does not match."  style="font-size:10px;">
                                           </asp:CompareValidator>                                  
                      --%>                  </td> 
                                        
                                       
                                    </tr>
                                   
                                 
                                </table>
                                
                                </td>
                        </tr>
                        <tr>
                                      <td colspan="2" style="height:70px;">
                                      
                                        </td>  
                                    </tr>
                    </table>
                </div>
            </td>
        
        </tr>
          <tr>
            <td>
                <div style="text-align:center;display:none;" id="dvEditUser" runat="Server"  >
                    <table width="100%" style="text-align:center;">
                    
                        <tr>
                            <td>
                                <table width="80%" cellpadding="1px" cellspacing ="1px">
                                
                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;" colspan="2">
                                                      Corporate User Edit Details
                                                    </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width:100%">
                                        <td style="width:30%;text-align:right; height: 20px;">
                                            <asp:Label ID="lblEUserName" runat="server"  text="User Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width:65%;text-align:left; height: 20px;">
                                            <asp:TextBox ID="txtEUserName" style="width:380px;font-size:11px;" MaxLength="60" runat="server" Enabled="false" Font-Size="11px"></asp:TextBox>                                           
                   
                                        </td>
                                       
                                    </tr>
                                                                         
                                    <tr class="AlterNateColor1" style="width:50%">
                                        <td style="width:30%;text-align:right">
                                         <asp:Label ID="lblEPassword" runat="server"  text="Password:" CssClass="NormalLabel"></asp:Label>   
                                        </td>
                                        <td style="width:65%;text-align:left">
                                        <asp:TextBox ID="txtEPassword"  style="width:160px;font-size:11px;" MaxLength="25" runat="server" Font-Size="11px"></asp:TextBox>
                                          <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtEPassword" runat="Server" ValidationGroup="Edit" ErrorMessage="Enter Password">
                                        </asp:RequiredFieldValidator>--%>
                                        </td>
                                    </tr>
                                     
                                  
                                     <tr class="AlterNateColor2" style="width:50%">
                                        <td style="width:30%;text-align:right;">
                                            <asp:Label ID="lblCompany" runat="server"  text="Company:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width:65%;text-align:left">
                                            <asp:TextBox ID="txtCompany" style="width:300px;font-size:11px;" MaxLength="50" runat="server" Enabled="false" Font-Size="11px"></asp:TextBox>
                                          
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" >
                                        <td style="width:30%;text-align:center ;">
                                                                                  
                                        </td> 
                                        <td style="width:65%;text-align:left">
                                          <asp:Button ID="btnUpdate" runat="server" CssClass="Button" Text="Update" OnClientClick="return checkPassword();"/> 
                                          <asp:Button ID="btnECancel" runat="server" CssClass="Button" Text="Cancel" CausesValidation="false"/>
                                        </td>
                                       
                                    </tr>
                                   
                                 
                                </table>
                                
                                </td>
                        </tr>
                        <tr>
                                      <td colspan="2" style="height:70px;">
                                      
                                        </td>  
                                    </tr>
                    </table>
                </div>
            </td>
        
        </tr>
       
        <tr  class="AlterNateColor3">
            <td class="PageSHeading" align="center">
             <asp:Label ID="lblTag" runat="Server" ></asp:Label>
            </td>
       </tr>
    </table>
    </form>
</body>

</html>
