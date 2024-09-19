<%@ Page Language="VB" MasterPageFile="~/Masters/SDistChart.master" AutoEventWireup="false" CodeFile="CErgyGHg.aspx.vb" Inherits="Charts_SDistCharts_CErgyGHg" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="SDistContentPlaceHolder" Runat="Server">

   <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV.js"></script>
   <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>
   <script type="text/JavaScript" src="../../JavaScripts/tip_balloon.js"></script>
<div id="ContentPagemargin" runat="server">
    <div id="PageSection1" style="text-align:left" >
        <br />
            <table style="width:710px">
                <tr>
                    <td align="center" style="height:50px; ">
                        <asp:Label ID="lblHeading" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="18px" >                
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
           <td align="center" style="width:100%; ">
                <div style="margin-left:10px;margin-top:10px; text-align:left;">
                    <table width="50%" >
                        <tr id="rwType" runat="server">
                            <td>
                                <b>Type:</b>
                            </td>
                              <td align="left">
                                <asp:DropDownList ID="ddltype" runat="server" CssClass="dropdown"  Width="200">                             
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
                                    <asp:ListItem  Text="Per Unit" Value="PUnit"></asp:ListItem>
                                    <asp:ListItem  Text="Per Weight" Value="PWeight"></asp:ListItem>
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

