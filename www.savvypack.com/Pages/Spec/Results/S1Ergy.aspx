<%@ Page Title="SPEC-Energy Results" Language="VB" MasterPageFile="~/Masters/Spec.master" AutoEventWireup="false" CodeFile="S1Ergy.aspx.vb" Inherits="Pages_Spec_Results_S1Ergy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Sustain1 - Energy Results')" onmouseout="UnTip()" >
                  Sustain1 - Energy Results
                </td>
                
                <td style="width:23%" class="PageSHeading">
                    <table>
                        <tr>
                            <td>
                                Specification ID:
                            </td>
                            
                            <td>
                                  <asp:Label ID="lblAID"  CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                
                
                <td style="width:30%" class="PageSHeading">
                     <table>
                        <tr>
                            <td>
                                Description:
                            </td>
                            
                            <td>
                                 <asp:Label ID="lblAdes" CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
   <br />
   <br />
 <div id="ContentPagemargin">
   <div id="PageSection1" style="text-align:left">
        <br />
            <table>
                <tr>
                    <td>
                        <b>Sales Volume:</b>
                    </td>
                    <td>
                        <asp:Label ID="lblVolume" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <b>Sales Volume Unit:</b>
                    </td>
                     <td>
                        <asp:Label ID="lblUnit" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
         <br />
     </div>
   
   </div>
</asp:Content>
