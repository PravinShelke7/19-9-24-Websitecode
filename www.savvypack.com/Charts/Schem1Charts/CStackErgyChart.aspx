<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CStackErgyChart.aspx.vb" Inherits="Charts_Schem1Charts_CStackErgyChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Schem1-Combine Energy Stack Chart</title>
 <%--   <link rel="stylesheet" href="../../App_Themes/SkinFile/Econ3Style.css" />--%>
   <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
 
</head>
<body>
    <form id="form1" runat="server">
         <div id="MasterContent" style="margin-top:-5px">
       
      <div id="AlliedLogo">
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>        
      
      <div>
        <table class="Schem1Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                      
                    </table>
               </td>
            </tr>
        </table>
       </div>
            
         <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
          </div>
    
   
   </div>
   
  
     <table class="ContentPage" id="ContentPage" runat="server" width="60%">
                        <tr style="height:20px">
                            <td>
                            <div id="ContentPagemargin" runat="server">
                               <div style="text-align:left;" >                        
                                 <div  class="PageHeading" style="width:825px" style="font-size:14px">
                                          <a  href="../ChartPreferences/ChartPreferencesS.aspx" target="_blank">Chart Preferences</a>
                                </div>
                                  <table style="width:710px">
                                        <tr>
                                            <td align="center" style="background-color:#5B5B5B;height:50px; ">
                                                <asp:Label ID="lblHeading" runat="server" ForeColor="White" Font-Bold="true" Font-Size="18px" >                
                                                </asp:Label>
                                            </td>
                                        </tr>
                                    
                                    </table>
    
                                         <table  style="width:800px">
                                            <tr>
                                                <td>
                                                     <div id="EnergyComp" runat="server">
                                                     </div>
                                                </td>
                                            </tr>
                                          </table>
                        
                                       <table style="width:710px">
                                        <tr>
                                           <td align="center" style="background-color:#5B5B5B;width:100%; ">
                                                <div style="margin-left:10px;margin-top:10px; text-align:left;color:White">
                                                    <table width="50%" >
                                                      
                                                          <tr>
                                                            <td>
                                                                <b>Conversion Type:</b>
                                                            </td>
                                                             <td align="left">
                                                                <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown"  Width="200">
                                                                    <asp:ListItem  Text="Total" Value="Total"></asp:ListItem>
                                                                    <asp:ListItem  Text="Per Unit" Value="PUnit"></asp:ListItem>
                                                                    <asp:ListItem  Text="Per Weight" Value="PWeight"></asp:ListItem>
                                                                </asp:DropDownList> 
                                                            </td>
                                                         </tr>
                                                         
                                                        <%-- <tr>
                                                            <td>
                                                                <b>Chart Type:</b>
                                                            </td>
                                                             <td align="left">
                                                                <asp:DropDownList ID="ddlChType" runat="server" CssClass="dropdown"  Width="200">
                                                                    <asp:ListItem  Text="Bar Chart" Value="Sargento_KraftPouchvsTray_Html3"></asp:ListItem>
                                                                </asp:DropDownList> 
                                                            </td>
                                                         </tr>--%>
                                                           <tr>
                                                            <td>
                                                                
                                                            </td>
                                                            <td align="left">
                                                               <asp:Button id="bynUpdate" runat="server" Text="Update" Width="93px" Font-Names="Verdana" />
                                                            </td>
                                                         </tr>
                                                    </table>               
                                                </div>
                                           </td>
                                        </tr>
                                     </table>
                           
                             
                   </div>
               </div> 
               </td>
            </tr>
            <tr class="AlterNateColor3">
             <td class="PageSHeading" align="center">
               Copyright 1997 - 2010 SavvyPack Corporation
            </td>
           </tr>
        </table>
    </form>
</body>
</html>

