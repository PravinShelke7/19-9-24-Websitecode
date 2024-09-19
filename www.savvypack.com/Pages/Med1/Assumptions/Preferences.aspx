﻿<%@ Page Title="Med1-Preferences" Language="VB" MasterPageFile="~/Masters/Med1.master" AutoEventWireup="false" CodeFile="Preferences.aspx.vb" Inherits="Pages_MedEcon1_Assumptions_Preferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Med1ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <div id="ContentPagemargin" runat="server">
       <div style="margin: 0px 200px 5px 5px;;text-align:right ">
        <asp:HyperLink ID="lnkAPref" runat="server" Text="Additional Preferences" CssClass="Link" Target="_blank" 
            Font-Bold="true" NavigateUrl="~/Pages/Med1/Assumptions/APreferences.aspx"></asp:HyperLink>
                               
                            </div>
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
                    <tr class="AlterNateColor4" >
                        <td class="PageSHeading" style="font-size:14px;" colspan="2" >
                            Preferred Units
                        </td>
                    </tr>
                      <tr class="AlterNateColor1" >
                        <td colspan="2">
                            <asp:RadioButton ID="rdEnglish" GroupName="Unit" runat="server" Text="English units" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1" >
                        <td colspan="2">
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
                        <td colspan="2">
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
                        <td colspan="2">
                            <asp:DropDownList ID="ddlCurrancy" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
                  <br />
	   
           
              <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Discrete Calculations
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdNewDis" GroupName="DISCRETE" runat="server" Text="Shipped together" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdOldDis" GroupName="DISCRETE" runat="server" Text="Shipped separately" />
                        </td>                        
                     </tr>                      
                    
                 </table>
             <br />
         </div>   
     </div>
</asp:Content>

