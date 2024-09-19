 <%@ Page Language="VB" AutoEventWireup="false" CodeFile="CWaterCharts2.aspx.vb" Inherits="Charts_SDistCharts_CWaterCharts2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SDist-Combine Water Bar Chart</title>
    <link rel="stylesheet" href="../../../../../App_Themes/SkinFile/Econ3Style.css" />
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
 
<body>
    <form id="form1" runat="server">
    <div style="font-family:Verdana;font-size:11px;">
     <table>
            <tr>
                <td align="center">
                    <div id="AlliedLogo">
            <table>
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
            </table>
        </div> 
                   
                </td>
            </tr>
            <tr>    
                    <td>
                     <asp:Image ID="line" ImageUrl="~/Images/Line.jpg"  width="750" height="9" runat="server" ToolTip="Allied Logo" /> 
                    </td>
            </tr>
            <tr> 
                <td>
                      <asp:Label ID="lblsession" Text="Your Username and/or Password are not currently valid. Your session may have timed out. Please close your current windows accessing SavvyPack Corporation's Economic Service and sign in again"  Visible="false" runat="server"></asp:Label>

                </td>
    </table>
    
    <div id="Charts" runat="server">
    <a class="Logoff" href="../../../../../Charts/ChartPreferences/ChartPreferencesS.aspx" target="_blank">Chart Preferences</a>
<br />

     <table style="width:710px">
        <tr>
            <td align="center" style="background-color:#5B5B5B;height:50px; ">
                <asp:Label ID="lblHeading" runat="server" ForeColor="White" Font-Bold="true" Font-Size="18px" >                
                </asp:Label>
            </td>
        </tr>
    
    </table>
    
     <table style="width:710px">
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
                    <table style="width: 54%" >
                        <tr>
                            <td>
                                <b>Type:</b>
                            </td>
                              <td align="left">
                                <asp:DropDownList ID="ddltype" runat="server" CssClass="dropdown"  Width="250">
                                     <asp:ListItem  Text="Raw Materials" Value="RMWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Raw Materials Packaging" Value="RMPACKWATER"></asp:ListItem>
                                     <asp:ListItem  Text="RM & Pack Transport" Value="RMANDPACKTRNSPTWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Process" Value="PROCWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Distribution Packaging" Value="DPPACKWATER"></asp:ListItem>
                                     <asp:ListItem  Text="DP Transport" Value="DPTRNSPTWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Transport to Customer" Value="TRSPTTOCUSWATER"></asp:ListItem>                                      
                                     <asp:ListItem  Text="Process (SDist)" Value="PROCWATERS2"></asp:ListItem>                                   
                                     <asp:ListItem  Text="Packaged Product Packaging(SDist)" Value="PACKGEDPRODPACKWATER"></asp:ListItem> 
                                     <asp:ListItem  Text="PPP Transport(SDist)" Value="PPPTRNSPTWATER"></asp:ListItem> 
                                     <asp:ListItem  Text="Packaged Product Transport(SDist)" Value="PACKGEDPRODTRNSPTWATER"></asp:ListItem> 
                                     <asp:ListItem  Text="Purchased Materials" Value="PURMATERIALWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Total Process" Value="PROCESSWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Transportation" Value="TRNSPTWATER"></asp:ListItem>
                                     <asp:ListItem  Text="Total Water Gas" Value="TOTALENWATER"></asp:ListItem>
                                 </asp:DropDownList> 
                            </td>
                         </tr>
                          <tr>
                            <td>
                                <b>Conversion Type:</b>
                            </td>
                             <td align="left">
                                <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown"  Width="200">
                                    <%--<asp:ListItem  Text="Total" Value="Total"></asp:ListItem>--%>
                                    <asp:ListItem  Text="Customer Water Total" Value="CTotal"></asp:ListItem>
                                     <asp:ListItem Text="Customer Water Per Unit" Value="CPUnit"></asp:ListItem>
                                     <asp:ListItem Text="Customer Water Per Weight" Value="CPWeight"></asp:ListItem>
                                </asp:DropDownList> 
                            </td>
                         </tr>                         
                       
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
    </form>
</body>
</html>