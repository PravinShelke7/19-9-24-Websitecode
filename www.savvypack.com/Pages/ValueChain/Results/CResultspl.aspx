<%@ Page Language="VB" MasterPageFile="~/Masters/VChain.master" AutoEventWireup="false" CodeFile="CResultspl.aspx.vb" Inherits="Pages_ValueChain_Results_CResultspl" title=" Profit and Loss Comparison" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VChainContentPlaceHolder" Runat="Server">
  <table class="ContentPage" id="ContentPage" runat="server" width="840px" >
            <tr>
                <td style="width:33%" colspan="2">
                     <div class="PageHeading" id="divMainHeading">
                         Profit and Loss Comparison
                     </div>                                                          
                </td>
                 <td style="width:20%" >
                    <table>
                        <tr>
                            <td class="PageSHeading" style="width: 88px;">
                                Value Chain Id:
                            </td>
                            
                            <td style="text-align:left">
                                  <asp:Label ID="lblAID"  CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                   <td style="width:50%" >
                     <table>
                        <tr>
                            <td style="text-align:Right;width: 79px" class="PageSHeading">
                                Description:
                            </td>
                            
                            <td>
                                 <asp:Label ID="lblAdes" CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            
            
            <tr >
                <td style="width:24%; text-align:Right;"  class="PageSHeading" >
                  Module Name:
                </td>
                <td>
                    <asp:Label ID="lblModName"  CssClass="LableFonts" runat="server"></asp:Label>
                </td>
                <td style="width:20%;" >
                    <table>
                        <tr>
                            <td class="PageSHeading" style="text-align:Right; width: 83px;">
                                Case Id:
                            </td>
                            
                            <td>
                                  <asp:Label ID="lblCaseId"  CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                
                
                <td style="width:50%" >
                     <table>
                        <tr >
                            <td style="text-align:Right;width: 79px" class="PageSHeading">
                                Description:
                            </td>
                            
                            <td>
                                 <asp:Label ID="lblCaseDesc" CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                                
            </tr>
            
            
             <tr style="height:20px">
                <td colspan="4">
                <div id="ContentPagemargin" runat="server">
                   <div id="PageSection1" style="text-align:left;width:80%" >
                    <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                   </div>
                 </div>
                 </td>
             </tr>
          </table>
</asp:Content>

