<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NotesPopUp.aspx.vb" Inherits="Pages_StandAssist_PopUp_NotesPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Notes and Sponsor Message Details</title>
	 <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function closePopup() {            
            window.close();
        }
        function OpenPDF(page) {
            window.open(page);
            window.close();
            return false;
        }
     </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <div id="PageSection1" style="height:260px;"  >
    <table style="margin-left:5px;margin-top:10px;height:190px;"   >     
    <tr style="width:100%;height:30px;"> 
    <td style="width:12%;"></td>   
    <td style="text-align:Center;width:85%;margin-left:5px;" valign="top">
   <asp:Label ID="lblTitle" runat="server" CssClass="CalculatedFeilds" Visible="true" style="font-size:18px;color:Black; text-align:Center;" 
              ></asp:Label></td>    
    <td style="width:3%;margin-left:5px;"></td>  
    </tr>
    <tr style="width:100%;height:160px;margin-top:55px; "> 
    <td style="width:12%;margin-left:5px;"></td>   
    <td style="text-align:left;width:85%;margin-left:5px;" valign="top">
    <asp:Label ID="lblNotes" runat="server" Width="450px" Visible="true" style="font-size:14px;color:Black; text-align:left;word-wrap:break-word;" 
              ></asp:Label>
    </td>
    <td style="width:3%;margin-left:5px;"></td>  
    </tr>
      
    </table>
    <table style="width:100%;">       
    <tr style="width:100%;"> 
    <td style="width:9%;"></td>   
    <td style="width:85%;" align="center" valign="top" >
    <asp:Button ID="btnsubmit" runat="server" Text="OK" CssClass="ButtonWMarigin" Width="70px" OnClientClick="javascript:closePopup();" /></td>   
    <td style="width:7%;"></td>  
    </tr>
      </table>  
</div> 
        <asp:HiddenField ID="hidNote" runat="server" />
        <asp:HiddenField ID="hidGNote" runat="server" />
               <asp:HiddenField ID="hidType" runat="server" />
               
    </div>
    </form>
</body>
</html>
