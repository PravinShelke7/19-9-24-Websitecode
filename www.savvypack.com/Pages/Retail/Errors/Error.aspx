<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Error.aspx.vb" Inherits="Pages_Retail_Errors_Error" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Input Error</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
      <script type="text/javascript">
        javascript: window.history.forward(1); 
      </script>
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
<body>
    <form id="form1" runat="server">
   <div id="MasterContent">
       
     <tr>
                <td class="PageSHeading" align="center">
                    <table style="width: 845px; background-color: #edf0f4;">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>      
      
       <div>
        <table class="RetModuleModule" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                   
               </td>
            </tr>
        </table>
       </div>  
    <table class="ContentPage" id="ContentPage" runat="server" style="width:845px">         
            <tr style="height:20px">
                <td>
                  <div class="PageHeading" id="divMainHeading" runat="server" >
                     Retail - Error
                     
                  </div>
                </td>
            </tr>   
             <tr style="height:20px">
                <td>
                  <div id="ContentPagemargin" runat="server">
                     <div id="PageSection1" style="text-align:left" >
                         <div class="ErrorDiv" style="width:100%">
       
                            <div id="divUpdate" runat="server" style="margin-bottom:20px;">
                                Please <b><asp:HyperLink ID="hypPage" runat="server" Text="Click Here" CssClass="Link" Font-Size="16px"></asp:HyperLink></b>&nbsp;to go input page. 
                            </div>
                            <table>
            <tr>
                <td>
                    <b>Error Code:-</b>
                </td>
                <td>
                    <asp:Label ID="lblErrorCode" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Error Message:-</b>
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
                        </div>
                     </div>
                  <br /><br /><br />
                     </td>
                   </tr>
                   </table>   
                   
                   </div>
    </form>
</body>
</html>
