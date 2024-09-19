<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserUsageReport.aspx.vb" Inherits="Charts_UsageReports_UserUsageReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>User Usage</title>   
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
     <div id="MasterContent" >
       
      <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>        
      
      <div>
        <table class="E1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:380px">
                    
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
                     <div class="PageHeading" id="divMainHeading" runat="server" style="width:840px;">
                         User Usage
                           <br />
                            <br />
                      </div>          
                            <div id="ContentPagemargin" runat="server">
                                <div id="PageSection1" style="text-align:center" >
                                    <br />
                                                                               
                                        <asp:Table ID="tblUsage" runat="server"></asp:Table>
                            
                                    <br />
                                  </div>
                            </div> 
                          
                </td>
            </tr>
             <tr style="height:20px">
                <td>
               
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
