<%@ Page Title="Edist-Preferences" Language="VB" MasterPageFile="~/Masters/Distribution.master" AutoEventWireup="false" CodeFile="Preferences.aspx.vb" Inherits="Pages_Distribution_Assumptions_Preferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="DistributionContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
               <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Country Selections
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            Country of Manufacture:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlMfg" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>
                     </tr> 
                     
                     <tr class="AlterNateColor1">
                        <td style="width:125px">
                            Country of Destination:
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDesti" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>
                     </tr>
                 </table>
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Preferred Units
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:RadioButton ID="rdEnglish" GroupName="Unit" runat="server" Text="American units" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:RadioButton ID="rdMetric" GroupName="Unit" runat="server" Text="Metric units" />
                        </td>                        
                     </tr>                      
                    
                 </table>
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Date</td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:DropDownList ID="ddlEffdate" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Preferred Currency
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:DropDownList ID="ddlCurrancy" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
                  <br />
                    <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Preferred Volume
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td>
                            <asp:RadioButton ID="rdvolume" GroupName="pvol" runat="server" Text="Product Volume With Package Volume " />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:RadioButton ID="rdsvolume" GroupName="pvol" runat="server" Text="Product Volume " />
                        </td>                        
                     </tr>                      
                    
                 </table><br />
                 <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Energy Calculations
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:RadioButton ID="rdErgyCalc" GroupName="ErgyCalc" runat="server" Text="Adjust Automatically" />
                        </td>                        
                     </tr> 
                     
                      <tr class="AlterNateColor1">
                        <td style="width:125px">
                            <asp:RadioButton ID="rdErgyCalcA" GroupName="ErgyCalc" runat="server" Text="Use Capacity" />
                        </td>                        
                     </tr>                      
                    
                 </table>
             <br />
         </div>   
     </div>
</asp:Content>

