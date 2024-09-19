<%@ Page Language="VB" MasterPageFile="~/Masters/E4Chart.master" AutoEventWireup="false" CodeFile="CPrftCost.aspx.vb" 
Inherits="Charts_E4Charts_CPrftCost" title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="E4ChartContentPlaceHolder" Runat="Server">
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
                        <br />
                          <asp:Label ID="lblHeadingS" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="14px" >                
                        </asp:Label>
                    </td>
                </tr> 
                  
                
            </table>
            
            <table style="width:710px">
                <tr>
                    <td>
                         <div id="ChartComp" runat="server">
                         </div>
                    </td>
                </tr>
            </table>
            
            <table style="width:710px">
        <tr>
           <td align="center" style="width:100%; ">
                <div style="margin-left:10px;margin-top:10px; text-align:left;">
                    <table width="65%" >
                     <tr id="Tr2" runat="server">
                            <td>
                                <b>Page Name:</b>
                            </td>
                              <td align="left">
                                <asp:DropDownList ID="ddlPName" runat="server" CssClass="dropdown"  Width="200" AutoPostBack="true" >
                                   <asp:ListItem  Text="Profit and Loss" Value="PFT"></asp:ListItem>
                                   <asp:ListItem  Text="Profit and Loss with Depreciation" Value="PFTD"></asp:ListItem>
                                     <asp:ListItem  Text="Cost" Value="COST"></asp:ListItem>
                                   <asp:ListItem  Text="Cost with Depreciation" Value="COSTD"></asp:ListItem>
                                 </asp:DropDownList> 
                            </td>
                         </tr>
                         <tr id="Tr1" runat="server">
                            <td>
                                <b>Chart Type:</b>
                            </td>
                              <td align="left">
                                <asp:DropDownList ID="ddlChartType" runat="server" CssClass="dropdown" AutoPostBack="true"   Width="200">
                                             <asp:ListItem  Text="Regular Bar Chart" Value="RBC"></asp:ListItem>
                                    <asp:ListItem  Text="Pie Chart" Value="PIE"></asp:ListItem>
                                    <asp:ListItem Text="Stacked Bar Chart" Value="SBAR"></asp:ListItem>
                                 </asp:DropDownList> 
                            </td>
                         </tr>
                             <tr id="ROWCategoryset" runat="server" visible="false">
                                    <td>
                                        <b>Category Set:</b>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlCategorySet" runat="server" CssClass="Dropdown" Width="200">
                                        </asp:DropDownList>
                                    </td>
                                    <td id="lnkManageCategory" runat="server" visible="false">
                                        <b><a href="../../Pages/Econ4/Category/ManageCategory.aspx" class="Link" style="font-size: 13px;">
                                            Manage Category Set</a> </b>
                                    </td>
                                </tr>
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

