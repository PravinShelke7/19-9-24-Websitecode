<%@ Page Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master" AutoEventWireup="false" CodeFile="ThankYou.aspx.vb" Inherits="Users_Login_ThankYou" title="User Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
			    <td style="font-size:16px;font-weight:bold;height:90px">
			       <div id="dvThanksPage" runat="Server" style="margin-left:35px;margin-right:25px;width:655px;font-family:Arial;color:Black " visible="false"  >
			            Thank you for creating your account. A verification code has been emailed to you. Please retrieve this verification code and log in. 
                       You will be prompted to enter this code, which will activate your account.
			        <br />
			       </div>
			         <div id="dvVarified" runat="Server" style="margin-left:15px;margin-right:5px;width:700px;font-family:Arial;color:Black;font-size:16px; " visible="false"  >
			           Thank you for verifying your email address. Your account is now activated and you can login to our web site. Please close this page to continue.
			          <%-- <a id="lnkCreate" style="font-style:normal;font-size:16px;" target="_blank" class="LinkU" href="http://www.savvypack.com">www.savvypack.com</a>
			             and login to your account.--%>
			        <br />
			       </div>
			      
			    
			    </td>
			
			</tr>
       
       
    </table>
    </asp:Content>