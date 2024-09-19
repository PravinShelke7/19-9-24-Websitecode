<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="CWaterChartS2.aspx.vb" MasterPageFile="~/Masters/Schem1Chart.master"
 Inherits="Charts_Schem1Charts_CWaterChartSchem1" Title="Schem1-Combine Water Bar Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Schem1ContentPlaceHolder" runat="Server">

    <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../JavaScripts/tip_balloon.js"></script>

    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <br />
            <table style="width: 710px">
                <tr>
                    <td align="center" style="height: 50px;">
                        <asp:Label ID="lblHeading" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="18px">                
                        </asp:Label>
                    </td>
                </tr>
            </table>
            <table style="width: 710px">
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
                                     <asp:ListItem  Text="Process (Schem1)" Value="PROCWATERS2"></asp:ListItem>                                   
                                     <asp:ListItem  Text="Packaged Product Packaging(Schem1)" Value="PACKGEDPRODPACKWATER"></asp:ListItem> 
                                     <asp:ListItem  Text="PPP Transport(Schem1)" Value="PPPTRNSPTWATER"></asp:ListItem> 
                                     <asp:ListItem  Text="Packaged Product Transport(Schem1)" Value="PACKGEDPRODTRNSPTWATER"></asp:ListItem> 
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
                                    <asp:ListItem  Text="Total" Value="Total"></asp:ListItem>
                                    <asp:ListItem  Text="Customer Total" Value="CTotal"></asp:ListItem>
                                   
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
            <br />
        </div>
    </div>
</asp:Content>