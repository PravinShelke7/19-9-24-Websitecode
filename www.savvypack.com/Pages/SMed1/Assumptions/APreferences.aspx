<%@ Page Title="SMed1-Additional Preferences" Language="VB" MasterPageFile="~/Masters/SMed1.master" AutoEventWireup="false" 
CodeFile="APreferences.aspx.vb" Inherits="Pages_MedSustain1_Assumptions_APreferences" %>


<asp:Content ID="Content1" ContentPlaceHolderID="SMed1ContentPlaceHolder" Runat="Server">
  <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <br />
           
              <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" >
                            Energy Calculations
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td >
                            <asp:RadioButton ID="rdErgyCalc" GroupName="Unit1" runat="server" Text="Adjust Automatically" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td >
                            <asp:RadioButton ID="rdErgyCalcA" GroupName="Unit1" runat="server" Text="Use Capacity" />
                        </td>                        
                     </tr>                      
                    
                 </table>
            
             <br />
                   <table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Design Waste Calculations
                        </td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:RadioButton ID="rdDesignOld" GroupName="DESIGNW" runat="server" Text="Old" />
                        </td>                        
                     </tr> 
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                          <asp:RadioButton ID="rdDesignNew" GroupName="DESIGNW" runat="server" Text="New" />
                        </td>                        
                     </tr>                      
                    
                 </table>
                 <br />
         </div>   
     </div>
</asp:Content>

