<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MaterialViewer.aspx.vb" Inherits="Pages_Echem1_PopUp_MaterialViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Material Viewer</title>
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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
    <div id="ContentPagemargin" style="margin:0 0 0 0;width:100%;">
       <div id="PageSection1" style="text-align:left;" >
       <div style="margin-left:10px;width:600px;">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
         <table border="0" id="tblCaseDes" runat="server" cellpadding="0" cellspacing="0">
                       <tr style="height:20px">
                            <td colspan="2" >
                                
                            </td>
                        </tr>
                        <tr style="height:20px">
                            <td style="width:350px;text-align:Left;">
                                <b>Active Material Id:</b><asp:Label ID="lblMatId" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                              <td style="width:80px;text-align:Left;" >
                                 
                            </td>                           
                        </tr>
                        
                    </table>
          
       
          
         <table width="90%">
             <tr class="AlterNateColor4">
                <td class="PageSHeading" style="font-size:14px;">
                    <asp:Label ID="lblMatDesDes" runat="server" Text="Material Brief"></asp:Label>
                </td>
            </tr>
            
             <tr class="AlterNateColor1">
                <td>
                      <asp:DropDownList ID="ddlMat" CssClass="DropDown" Width="80%" runat="Server"></asp:DropDownList>
               
                </td>
             </tr> 
             <tr class="AlterNateColor1">
                <td>  
                     <asp:Button ID="btnPrivious" runat="server"  CssClass="Button" Text="Privious" style="margin-left:0px;" />                                        
                     <asp:Button ID="btnNext" runat="server" Text="Next"  CssClass="Button"></asp:Button>                               
                </td>
             </tr>
                
        </table>
         <br />
         <table width="90%">
                <tr class="AlterNateColor4">                    
                    <td align="center" class="PageSHeading" style="height:10px" colspan="2">
                        Material Characteristics                        
                    </td>
                </tr>
                 <tr class="AlterNateColor4">  
                            <td class="PageSHeading" style="width:50%; height: 17px;" align="Center">
                                    <asp:Label id="lblPrice" runat="Server"></asp:Label>
                                    
                                </td>
                                <td class="PageSHeading" align="center" style="height: 17px">
                                    <asp:Label id="lblSG" runat="Server" Text="Specific Gravity"></asp:Label>
                                   
                            </td>                     
     
                 </tr>
                  <tr class="AlterNateColor1">  
                            <td class="PageSHeading" style="width:50%; height: 17px;" align="Center" >
                                    <asp:Label id="lblPriceVal" runat="Server"></asp:Label>
                                    
                                </td>
                                <td class="PageSHeading" align="center"  style="height: 17px">
                                    <asp:Label id="lblSGVal" runat="Server"></asp:Label>
                                   
                            </td>                     
     
                 </tr>
         </table>
          <br />          
         
        </div>    
        </div>
    </div>
    </form>
</body>
</html>
