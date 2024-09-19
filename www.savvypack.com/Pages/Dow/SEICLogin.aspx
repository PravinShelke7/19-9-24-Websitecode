<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SEICLogin.aspx.vb" Inherits="Pages_Dow_SEICLogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Member Entrance</title>
     <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
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
        function ValidateUser() {
            var UserNameText = document.getElementById("<%=txtUserName.ClientID %>").value;
            var PasswordText = document.getElementById("<%=txtPass.ClientID %>").value;
            if (UserNameText == "" || PasswordText == "") {
                alert('Please Enter UserName and Password before Login!');
                return false;
            }
            else {
                return true;
            }

        }
       
        function clickButton(e, buttonid) {
            //        alert(navigator.appName);
            var bt = document.getElementById(buttonid);
            if (bt) {

                if (navigator.appName.indexOf("Microsoft Internet Explorer") > (-1)) {
                    if (event.keyCode == 13) {
                        document.getElementById(buttonid).focus();
                        return true;
                    }
                }

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

      (function () {
          var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
          ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
          var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
      })();

</script>
  </head>
<body  style="margin-top:5px;">
    <form id="form1" runat="server">
      <div id="MasterContent">
       
      <div id="AlliedLogo">
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>        
      
       <div>
        <table class="S1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>                
                                <td>
                                      <asp:ImageButton ID="btnHomePage" runat="server" ImageUrl="~/Images/Home2.gif" PostBackUrl="~/Index.aspx" 
                                       ToolTip="Return To SavvyPack Corporation Home Page"  />
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
               
                     <div class="PageHeading" id="divMainHeading" style="width:840px;">
                         Member Entrance
                     </div>
                    
    
                   
                                    
                </td>
            </tr>
             <tr style="height:20px">
                <td>              
                   <div style="width:100%" >
                          
                           <table width="55%">
                                <tr>
                                    <td>
                                           <table width="90%">
                                                <tr class="AlterNateColor4" >
                                                    <td class="PageSHeading" style="font-size:14px;" colspan="2">
                                                       User Login
                                                    </td>
                                                </tr>
                                                <tr class="AlterNateColor1" style="width:100%">
                                                    <td  style="width:20%;text-align:right;" >
                                                        <asp:Label ID="lblUserName" runat="Server" Text="User Name:" CssClass="NormalLabel" ></asp:Label>                                                       
                                                    </td>
                                                     <td  style="width:30%;text-align:left;">
                                                       <asp:TextBox ID="txtUserName"  MaxLength="60" runat="Server" Width="250px" Font-Size="11px"></asp:TextBox>
                                                    </td>
                                                 </tr> 
                                                   <tr class="AlterNateColor2" style="width:100%">
                                                    <td  style="width:20%;text-align:right;" >
                                                        <asp:Label ID="lblPasswod" runat="Server" Text="Password:" CssClass="NormalLabel" ></asp:Label>                                                       
                                                    </td>
                                                     <td  style="width:40%;text-align:left;">
                                                       <asp:TextBox ID="txtPass" runat="Server" TextMode="Password" MaxLength="25" Width="250px" Font-Size="11px"></asp:TextBox>
                                                    </td>
                                                 </tr> 
                                                 <tr class="AlterNateColor1">
                                                    <td colspan="2" style="text-align:center">  
                                                        <asp:Button ID="btnSubmit" runat="server"  CssClass="Button" Text="Submit" OnClientClick="return ValidateUser()" />                                    

                                                    </td>
                                                 </tr>  
                                               
                                        </table>    
                                    </td>
                                     
                                </tr>
                           </table>
                           
                            
                                                        
                          
                          
                    
                    </div>  
            
               </td>
            </tr>
             <tr class="AlterNateColor3">
                 <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server" ></asp:Label>
                </td>
           </tr>
        </table>
    </form>
</body>
</html>
