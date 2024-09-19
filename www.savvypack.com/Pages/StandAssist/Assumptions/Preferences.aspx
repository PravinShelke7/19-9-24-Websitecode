<%@ Page Title="Preferences" Language="VB" MasterPageFile="~/Masters/SBAssist.master" AutoEventWireup="false" CodeFile="Preferences.aspx.vb" Inherits="Pages_StandAssist_Assumptions_Preferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SBAssistContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <div id="ContentPagemargin" runat="server">
      <div style="margin: 0px 200px 5px 5px;;text-align:right ">
        
                            </div>
       <div id="PageSection1" style="text-align:left" >
             <br />
               
             <br />
             <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Preferred Units
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdEnglish" GroupName="Unit" runat="server" Text="English units"  />
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
                            Date of Permeability Dataset</td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:DropDownList ID="ddlEffdate" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>
             <br />
             
             <br />
        
           
           
         </div>   
     </div>
</asp:Content>

