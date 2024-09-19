<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="Users_Login_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Input Error</title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
        javascript: window.history.forward(1); 
      </script>
</head>
<body>
    <form id="form1" runat="server">
      <div id="MasterContent">
       
      <div id="AlliedLogo">            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
     </div>  
     
     <div>
        <table class="ULoginModule" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                  
               </td>
            </tr>
        </table>
       </div>   
    
     
       
    <table class="ContentPage" id="ContentPage" runat="server">
     
        <tr>
            <td>
                <div class="ErrorDiv" style="width:100%">
       <div id="divUpdate" runat="server" style="margin-bottom:20px;">
        Please <b><asp:HyperLink ID="hypPage" runat="server" Text=" Click Here" CssClass="Link" Font-Size="16px"></asp:HyperLink></b>&nbsp;<asp:Label ID="lblText" runat="Server"></asp:Label> 
       </div>
        <table width="90%">
            <tr>
                <td style="width:12%">
                    <b>Error Code:</b>
                </td>
                <td style="width:80%">
                    <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Error Message:</b>
                </td>
                <td>
                    <asp:Label ID="lblErrorMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
   </div>
   <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
   </div>
            </td>
        </tr>
       </table>
       
   </div>
   
    </form>
</body>
</html>
