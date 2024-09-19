<%@ Page Title="S1-GHG Results For All Specification" Language="VB" MasterPageFile="~/Masters/Spec.master" AutoEventWireup="false" CodeFile="S1AllSpecGhg.aspx.vb" Inherits="Pages_Spec_Results_S1AllSpecGhg" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Sustain1 - GHG Results For All Specification')" onmouseout="UnTip()" >
                  Sustain1 - GHG Results For All Specification
                </td>
                
             
            </tr>
        </table>
   <br />
   <br />
 <div id="ContentPagemargin">
   <div id="PageSection1" style="text-align:left">
        <br />
            <table>
                 <tr style="height:20px">
                    <td>
                        <asp:RadioButton ID="rdSaleUnit" GroupName="Sales" Checked="true" 
                            runat="server" Text="By Sales Volume Unit" CssClass="PageSSHeading" 
                            AutoPostBack="True" />
                        
                    </td>
                    <td>
                        <asp:RadioButton ID="rdSaleMsi" GroupName="Sales" Checked="false" 
                            runat="server" Text="By Sales Volume Msi" CssClass="PageSSHeading" 
                            AutoPostBack="True"/>
                    </td>
                     <td>
                        <asp:RadioButton ID="rdSaleAll" GroupName="Sales" Checked="false" 
                            runat="server" Text="All" CssClass="PageSSHeading" 
                            AutoPostBack="True"/>
                    </td>
                </tr>
            </table>
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

