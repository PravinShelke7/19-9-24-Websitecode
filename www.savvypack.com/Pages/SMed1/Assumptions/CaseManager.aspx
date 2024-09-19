<%@ Page Title="SMed1-Case Manager" Language="VB" MasterPageFile="~/Masters/SMed1.master" AutoEventWireup="false" CodeFile="CaseManager.aspx.vb" Inherits="Pages_MedSustain1_Assumptions_CaseManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SMed1ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
<div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
        <table width="98%" style="text-align:left;">
                <tr class="PageSSHeading" style="height:20px;background-color:Transparent;">
                    <td style="width: 48px">
                    
                    </td>
                    <td style="width: 251px">
                        
                    </td>
                    <td>
                         <a href="Preferences.aspx" class="Link" target="_blank" style="color:Red;">Preferences</a>  
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr class="PageSSHeading" style="height:20px;">
                    <td class="TdHeading" style="width: 48px">
                    
                    </td>
                    <td class="TdHeading" style="width: 251px">
                        Specify Assumptions
                    </td>
                    <td class="TdHeading">
                        Review Intermediate Result
                    </td>
                    <td class="TdHeading">
                        Review Final Results	
                    </td>
                </tr>
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step1
                    </td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="Extrusion.aspx" class="Link" 
                            target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/ExtrusionOut.aspx" class="Link" target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/ErgyRes.aspx" class="Link" target="_blank">Energy</a>
                    </td>
                </tr>
                
                 <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step2
                    </td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="RwMatPack.aspx" class="Link" target="_blank">Raw Material Packaging</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/PalletInNewOut.aspx" class="Link" target="_blank">Raw Material Packaging</a>
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/GhgRes.aspx" class="Link" target="_blank">GHG</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step3
                    </td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="ProductFormatIN.aspx" class="Link" target="_blank">Product Format</a>
                    </td>
                    <td class="StaticTdLeft">
                         <a href="../IntResults/ResultsEOLIN.aspx" class="Link" target="_blank">End Of Life</a>
                    </td>
                      <td class="StaticTdLeft"> 
                          <a href="../Results/ResultsEOL.aspx" class="Link" target="_blank">End Of Life</a></td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step4</td>
                    <td class="StaticTdLeft" style="width: 251px">
                         <a href="TruckPalletIN.aspx" class="Link" target="_blank">Pallet and Truck Configuration</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                          <a href="../Results/WaterRes.aspx" class="Link" target="_blank">Water</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step5</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="PalletIn.aspx" class="Link" target="_blank">Pallet Packaging</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/PalletInout.aspx" class="Link" target="_blank">Pallet Packaging</a>
                    </td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step6</td>
                    <td class="StaticTdLeft" style="width: 251px">
                       <a href="PlantConfig.aspx" class="Link" target="_blank">Department Configuration</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/PrintCaseSummary.aspx" class="Link" target="_blank">Print Case Summary</a>
                        </td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step7</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="Efficiency.aspx" class="Link" target="_blank">Material Efficiency</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                        <%--<a href="../IntResults/SummerySheetIntm.aspx" class="Link" target="_blank">Scorecard</a>--%>
                                            
                    </td>
                    <td class="StaticTdLeft"> 
                       
                        <%-- <a href="../Results/ScoreCardRes.aspx" class="Link" target="_blank">Scorecard</a>--%>
                                           
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step8</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="EquipmentIn.aspx" class="Link" target="_blank">Process Equipment</a>
                    </td>
                    <td class="StaticTdLeft">
                       
                    </td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step9</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="Equipment2In.aspx" class="Link" target="_blank"> Support Equipment</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/ErgyRes2.aspx" class="Link" target="_blank">Customer Energy</a>
                     </td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step10</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="OperationsIn.aspx" class="Link" target="_blank">Operating Parameters</a>
                    </td>
                    <td class="StaticTdLeft">
                      <a href="../IntResults/OperationsOut.aspx" class="Link" target="_blank">Operating Results</a>
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/GHGRes2.aspx" class="Link" target="_blank">Customer GHG</a>
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step11</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="PersonnelIn.aspx" class="Link" target="_blank">Personnel</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                           <a href="../Results/ResultsEOL2.aspx" class="Link" target="_blank">Customer End Of Life</a>
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step12</td>
                    <td class="StaticTdLeft" style="width: 251px">
                       <a href="PlantConfig2.aspx" class="Link" target="_blank">Plant Space Requirements</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                          <a href="../Results/WaterRes2.aspx" class="Link" target="_blank">Customer Water</a>
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step13</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="EnergyIn.aspx" class="Link" target="_blank">Energy Assumptions</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step14</td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="CustomerIn.aspx" class="Link" target="_blank">Transportation Assumptions</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft" style="width: 48px">
                        Step15
                    </td>
                    <td class="StaticTdLeft" style="width: 251px">
                        <a href="ExtraProcess.aspx" class="Link" target="_blank">Extra-Process Assumptions</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
            </table>
         <br />
     </div>
   
   </div>
</asp:Content>

