<%@ Page Title="S2-Preferences" Language="VB" MasterPageFile="~/Masters/Sustain2.master" AutoEventWireup="false" CodeFile="Preferences.aspx.vb" Inherits="Pages_Sustain2_Assumptions_Preferences" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain2ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Preferred Units
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdEnglish" GroupName="Unit" runat="server" Text="English units" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdMetric" GroupName="Unit" runat="server" Text="Metric units" />
                        </td>                        
                     </tr>                      
                    
                 </table>
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            LCI Data</td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:DropDownList ID="ddlLCI" CssClass="DropDown" Width="125px" runat="server" 
                                AutoPostBack="True"></asp:DropDownList>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Date
                        </td>
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
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                        Energy Type
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdbTot" GroupName="Type" runat="server" Text="Total Energy" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdbNon" GroupName="Type" runat="server" Text="Non Renewable" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdbRen" GroupName="Type" runat="server" Text="Renewable" />
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
                        <td colspan="2">
                            <asp:RadioButton ID="rdvolume" GroupName="pvol" runat="server" Text="Product Volume With Package Volume " />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdsvolume" GroupName="pvol" runat="server" Text="Product Volume " />
                        </td>                        
                     </tr>                      
                    
                 </table>
		<br />
		<table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Energy Calculations
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdErgyCalc" GroupName="Unit1" runat="server" Text="Adjust Automatically" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdErgyCalcA" GroupName="Unit1" runat="server" Text="Use Capacity" />
                        </td>                        
                     </tr>                      
                    
                 </table>
		<br />
         </div>   
     </div>
</asp:Content>

