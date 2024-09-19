<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="Reports.aspx.vb" Inherits="Pages_Market1_Reports" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
<script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
          <br />
          
             <table width="50%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Continent wise (From <asp:Label ID="lblFrom" runat="server"></asp:Label> To  <asp:Label ID="lblTo" runat="server"></asp:Label>)
                        </td>
                    </tr>
                      <tr>
                        <td>
                             <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="1" style="width:95%"></asp:Table>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
              
            <div id="divCountry" runat="server" visible="false">
             <table width="50%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Country wise (<asp:Label ID="lblFrom1" runat="server"></asp:Label> To  <asp:Label ID="lblTo1" runat="server"></asp:Label>)
                        </td>
                    </tr>
                      <tr>
                        <td>
                             <asp:Table ID="tblComparision2" runat="server" CellPadding="0" CellSpacing="1" style="width:95%"></asp:Table>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
              </div>
              
                  
            <div id="divCountryYear" runat="server" visible="false">
             <table width="90%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                          Country year wise (<asp:Label ID="lblFrom2" runat="server"></asp:Label> To  <asp:Label ID="lblTo2" runat="server"></asp:Label>)
                        </td>
                    </tr>
                      <tr>
                        <td>
                             <asp:Table ID="tblComparision3" runat="server" CellPadding="0" CellSpacing="1" style="width:95%"></asp:Table>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
              </div>
             
             
              
             
             <br />
       </div>
    </div>
</asp:Content>

