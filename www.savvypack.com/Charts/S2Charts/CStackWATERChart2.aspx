<%@ Page Language="VB"  AutoEventWireup="false" CodeFile="CStackWATERChart2.aspx.vb" MasterPageFile="~/Masters/S2Chart.master" Inherits="Charts_S2Charts_CStackWATERChart2" 
 Title="S2-Combine Customer Water Stack Chart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain2ContentPlaceHolder" runat="Server">

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
                    <table width="50%" >
                      
                          <tr>
                            <td>
                                <b>Conversion Type:</b>
                            </td>
                             <td align="left">
                                <asp:DropDownList ID="ddlCnType" runat="server" CssClass="dropdown"  Width="200">
                                   <%--<asp:ListItem  Text="Total" Value="Total"></asp:ListItem>--%>
                                    <%--<asp:ListItem  Text="Customer Total" Value="CTotal"></asp:ListItem>--%>
                                   <asp:ListItem  Text="Customer Water Total" Value="CTotal"></asp:ListItem>
                                     <asp:ListItem Text="Customer Water Per Unit" Value="CPUnit"></asp:ListItem>
                                     <asp:ListItem Text="Customer Water Per Weight" Value="CPWeight"></asp:ListItem>
                                   
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
            <br />
        </div>
    </div>
</asp:Content>