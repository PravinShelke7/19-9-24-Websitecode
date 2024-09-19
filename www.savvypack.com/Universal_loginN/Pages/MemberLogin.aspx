<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MemberLogin.aspx.vb" Inherits="Universal_loginN_Pages_MemberLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Member Entrance</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
.VerificCode
{
      font-family:Verdana;
	  background-color:#dfe8ed;
	   height:20px;
	     font-family:Optima;
    font-size:12px;	   
   color:Black;
}
</style> 
    <script type="text/javascript">
        javascript: window.history.forward(1); 
      function ShowPassWindow(user,secLevel)
      {      
       document.getElementById('<%=btnSubmit.ClientID %>').focus();
       document.getElementById("<%=txtUserName.ClientID %>").focus();
       window.open('../../Users_Login/ChangeExPassword.aspx?un='+user+'&SecLvl='+secLevel);
       return false;
      }
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
        function ValidateUser()
        {
            var UserNameText= document.getElementById("<%=txtUserName.ClientID %>").value;
            var PasswordText= document.getElementById("<%=txtPass.ClientID %>").value;                          
            if(UserNameText=="" || PasswordText=="")
            {
              alert('Please Enter UserName and Password before Login!');
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
     function ShowPopWindow(flag) 
  {
    
    if(flag=="pass")
    {
     document.getElementById('<%=btnSubmit.ClientID %>').focus();
     document.getElementById("<%=txtUserName.ClientID %>").focus();
     newwin = window.open('../../Users_Login/ForgotPassword.aspx');
     return false;
     }
     else
     {
       document.getElementById('<%=btnSubmit.ClientID %>').focus();
       document.getElementById("<%=txtUserName.ClientID %>").focus();
       newwin = window.open('../../Users_Login/AddEditAccount.aspx');
       return false;
     }
  }
//  function ShowPopWindow(user,secLevel) 
//  {
//  //alert(user+"-"+secLevel);
//   document.getElementById('<%=btnSubmit.ClientID %>').focus();
//       document.getElementById("<%=txtUserName.ClientID %>").focus();
//  window.open('../../Users_Login/ChangeExPassword.aspx?un='+user+'&SecLvl='+secLevel);
//  return false;
//  }    
    
    
      function ShowAccountUpdation(Page)
     {
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
     
       function ShowAccountAdd(Page)
     {
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
    </script>

    <style type="text/css">
        .style1
        {
            width: 413px;
        }
    </style>

    <script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-16991293-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + 
'.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

    </script>

</head>
<body style="margin-top: 5px;">
    <form id="form1" runat="server">
    <div id="MasterContent">
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>
        <div>
            <table class="ULoginModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnHomePage" runat="server" ImageUrl="~/Images/Home2.gif" PostBackUrl="~/Index.aspx"
                                        ToolTip="Return To SavvyPack Corporation Home Page" />
                                    <%--  onmouseover="Tip('Return to Home Page')" onmouseout="UnTip()"--%>
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
                <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                    Member Entrance
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td align="center">
                <div style="text-align: center;">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                <table width="60%">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            User Login
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="width: 100%">
                                        <td style="width: 10%; text-align: right;">
                                            <asp:Label ID="lblUserName" runat="Server" Text="User Name:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 25%; text-align: left;">
                                            <asp:TextBox ID="txtUserName" MaxLength="60" runat="Server" Width="300px" Font-Size="11px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" style="width: 100%">
                                        <td style="width: 10%; text-align: right;">
                                            <asp:Label ID="lblPasswod" runat="Server" Text="Password:" CssClass="NormalLabel"></asp:Label>
                                        </td>
                                        <td style="width: 25%; text-align: left;">
                                            <asp:TextBox ID="txtPass" runat="Server" TextMode="Password" MaxLength="25" Width="300px"
                                                Font-Size="11px"></asp:TextBox>
                                        </td>
                                    </tr>
                                         <tr class="VerificCode" id="trVCode" runat="server" visible="false">
                                            <td style="text-align: right;">
                                                <asp:Label ID="lblVCode" runat="Server" Text="Verification Code:" CssClass="NormalLabel"></asp:Label>
                                            </td>
                                            <td style="text-align: left;">
                                                <asp:TextBox ID="txtVerfCode" runat="Server" Width="300px" Style="font-size: 11px;
                                                    height: 18px;"></asp:TextBox>
                                            </td>
                                        </tr>
                                    <tr class="AlterNateColor1">
                                        <td colspan="2" style="text-align: center">
                                            <asp:Button ID="btnSubmit" runat="server" CssClass="Button" Text="Submit" OnClientClick="return ValidateUser()" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <%--  <td>
                                           <table width="90%">
                                                <tr class="AlterNateColor4">
                                                    <td class="PageSHeading" style="font-size:14px;" colspan="2">
                                                       Corporate Administrator Login
                                                    </td>
                                                </tr>
                                                <tr class="AlterNateColor1" style="width:100%">
                                                    <td  style="width:20%;text-align:right;" >
                                                        <asp:Label ID="lblCUserName" runat="Server" Text="User Name:" CssClass="NormalLabel" ></asp:Label>                                                       
                                                    </td>
                                                     <td  style="width:30%;text-align:left;">
                                                       <asp:TextBox ID="txtCUserName" runat="Server" MaxLength="60" Width="300px" Font-Size="11px"></asp:TextBox>
                                                    </td>
                                                 </tr> 
                                                   <tr class="AlterNateColor2" style="width:100%">
                                                    <td  style="width:20%;text-align:right;" >
                                                        <asp:Label ID="lblCPass" runat="Server" Text="Password:" CssClass="NormalLabel" ></asp:Label>                                                       
                                                    </td>
                                                     <td  style="width:30%;text-align:left;">
                                                       <asp:TextBox ID="txtCPass" runat="Server"  TextMode="Password" MaxLength="25"  Width="300px" Font-Size="11px"></asp:TextBox>
                                                    </td>
                                                 </tr> 
                                                 <tr class="AlterNateColor1">
                                                    <td colspan="2" style="text-align:center">  
                                                        <asp:Button ID="btnCSubmit" runat="server"  CssClass="Button" Text="Submit"  OnClientClick="return ValidateCUser()" />                                    

                                                    </td>
                                                 </tr>  
                                               
                                        </table>    
                                    </td>--%>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 100%;">
                <table>
                    <tr>
                        <td style="padding-left:180px;">
                            <asp:LinkButton ID="lnkForgotPass" runat="Server" CssClass="LinkF" Text="Forget your password?"
                                OnClientClick="return ShowPopWindow('pass');"></asp:LinkButton>
                        </td>
                        <td style="padding-right:110px;padding-left:50px">
                        <asp:LinkButton ID="LinkChngPass" runat="Server" CssClass="LinkF" Text="Change password?"
                        OnClick="ChangePassword" Width="140px"></asp:LinkButton>
                        </td> 
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 10px">
                        </td>
                    </tr>
                    <tr>
                        <td style="font-family: Optima; color: Black; font-weight: bold; font-size: 14px;
                            width: 500px; padding-left: 240px;" align="left" colspan="2">
                            New user?
                            <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="LinkF" Text="Create an account"
                                OnClientClick="return ShowPopWindow('Acc');"></asp:LinkButton>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 10px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="PageSHeading">
                <div class="PageHeading" id="div1" style="width: 840px; color: Red; text-align: left">
                    SavvyPack<sup>&reg;</sup>Interactive System
                    <br />
                </div>
                <div id="div2" style="width: 840px; color: Black; text-align: left; font-size: 14px;
                    font-family: Optima; margin-top: 5px;">
                    <ul>
                        <asp:Label ID="lbSavvyPack" runat="Server" Text="The SavvyPack® system includes a variety of subscription based services. These services include:"></asp:Label>
                        <li style="margin-top: 5px;">Economic Analysis System </li>
                        <li style="margin-top: 5px;">Environmental Analysis System </li>
                        <li style="margin-top: 5px;">Knowledgebases </li>
                        <li style="margin-top: 5px;">On-line Studies</li>
                    </ul>
                </div>
                <div>
                    <h4>
                        If you are not a member, <a href="../../InteractiveServices/InteractiveServices.aspx">
                            click here</a> to learn more about the system or to purchase a subscription.</h4>
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
