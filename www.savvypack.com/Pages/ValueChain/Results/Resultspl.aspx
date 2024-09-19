<%@ Page Language="VB" MasterPageFile="~/Masters/VChain.master" AutoEventWireup="false" CodeFile="Resultspl.aspx.vb" Inherits="Pages_ValueChain_Results_Resultspl" title="Profit and Loss Statement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VChainContentPlaceHolder" Runat="Server">
 <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
<table width="840px"  style="text-align:left;">
            <tr align="left">
                <td style="width:33%" class="PageHeading" onmouseover="Tip('Profit and Loss Statement')" onmouseout="UnTip()" colspan="2">
                  Profit and Loss Statement
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
        </table>
   <br />

   
   <div id="ContentPagemargin">
   <div id="PageSection1"  style="text-align:left;" >  
    <table style="width:400px;">
                <tr>
                   <td>
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td>
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
             </table>       
            <asp:Table ID="tblPft"  runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
         <br />
     </div>
   
   </div>
</asp:Content>

