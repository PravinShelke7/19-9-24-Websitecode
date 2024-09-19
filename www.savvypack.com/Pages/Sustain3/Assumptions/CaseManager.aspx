<%@ Page Title="S3-Case Manager" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="CaseManager.aspx.vb" Inherits="Pages_Sustain3_Assumptions_CaseManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px"  style="text-align:left;">
            <tr align="left">
                <td style="width:33%" class="PageHeading" onmouseover="Tip('Case Manager')" onmouseout="UnTip()" >
                  Case Manager
                </td>
                
                <td style="width:23%" class="PageSHeading">
                    <table>
                        <tr>
                            <td>
                                Comparison ID:
                            </td>
                            
                            <td>
                                  <asp:Label ID="lblAID"  CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                
                
                <td style="width:40%" class="PageSHeading">
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
   <div id="PageSection1" style="text-align:center" >
        <br />
            <table width="98%" style="text-align:left;">
                <tr class="PageSSHeading" style="height:20px;background-color:Transparent;">
                    <td>
                    
                    </td>
                    <td>
                        
                    </td>
                    <td>
                      <a href="Preference.aspx" class="Link" target="_blank" style="color:Red;">Preferences</a>  
                    <td>
                        
                    </td>
                </tr>
                <tr class="PageSSHeading" style="height:20px;">
                    <td class="TdHeading">
                    
                    </td>
                    <td class="TdHeading">
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
                    <td class="StaticTdLeft">
                        Step1
                    </td>
                    <td class="StaticTdLeft">
                        <a href="Extrusion.aspx" class="Link" target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/ExtrusionOut.aspx" class="Link" target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/ErgyReslt1.aspx" class="Link" target="_blank">Energy (Total)</a>
                    </td>
                </tr>
                
                 <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step2
                    </td>
                    <td class="StaticTdLeft">
                        <a href="PalletInNew.aspx" class="Link" target="_blank">Raw Material Packaging</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/PalletInNewOut.aspx" class="Link" target="_blank">Raw Material Packaging</a>
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/ErgyReslt2.aspx" class="Link" target="_blank">Energy (Per Unit Weight)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step3
                    </td>
                    <td class="StaticTdLeft">
                        <a href="ProductFormat.aspx" class="Link" target="_blank">Product Format</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/EndOfLifeIn.aspx" class="Link" target="_blank">End Of Life</a>
                    </td>
                      <td class="StaticTdLeft"> 
                         <a href="../Results/ErgyReslt3.aspx" class="Link" target="_blank">Energy (Per Unit)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step4</td>
                    <td class="StaticTdLeft">
                         <a href="PalletAndTruck.aspx" class="Link" target="_blank">Pallet and Truck Configuration</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                       <a href="../Results/GhgResults1.aspx" class="Link" target="_blank"> Greenhouse Gas (Total)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step5</td>
                    <td class="StaticTdLeft">
                        <a href="PalletIn.aspx" class="Link" target="_blank">Pallet Packaging</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../IntResults/PalletOut.aspx" class="Link" target="_blank">Pallet Packaging</a>
                    </td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/GhgResults2.aspx" class="Link" target="_blank"> Greenhouse Gas (Per Unit Weight)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step6</td>
                    <td class="StaticTdLeft">
                       <a href="DeptConfig.aspx" class="Link" target="_blank">Department Configuration</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/GhgResults3.aspx" class="Link" target="_blank"> Greenhouse Gas (Per Unit)</a>
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step7</td>
                    <td class="StaticTdLeft">
                        <a href="Effciency.aspx" class="Link" target="_blank">Material Efficiency</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                        <a href="../IntResults/ScoreCardIntm.aspx" class="Link" target="_blank">Scorecard</a>
                                            
                    </td>
                     <td class="StaticTdLeft"> 
                       
                       <a href="../Results/WaterResults1.aspx"  class="Link" target="_blank">Water(Total)</a>
                                           
                    </td>
                   
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step8</td>
                    <td class="StaticTdLeft">
                        <a href="EquipmentIn.aspx" class="Link" target="_blank">Process Equipment</a>
                    </td>
                  
                    <td class="StaticTdLeft"> 
                        <a href="../Results/EndOfLifeRes.aspx" class="Link" target="_blank">End Of Life</a>
                    </td>
                     <td class="StaticTdLeft"> 
                        <a href="../Results/WaterResults2.aspx" class="Link" target="_blank">Water (Per Unit Weight)</a>
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step9</td>
                    <td class="StaticTdLeft">
                        <a href="Equipment2In.aspx" class="Link" target="_blank"> Support Equipment</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                     <td class="StaticTdLeft"> 
                     <%--   <a href="../Graphs/ErgyComp.aspx" class="Link" target="_blank"> Energy Charts</a>--%>
                     <a href="../Results/WaterResults3.aspx" class="Link" target="_blank">Water (Per Unit)</a>
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step10</td>
                    <td class="StaticTdLeft">
                        <a href="OperationsIn.aspx" class="Link" target="_blank">Operating Parameters</a>
                    </td>
                    <td class="StaticTdLeft">
                      <a href="../IntResults/OperationsOut.aspx" class="Link" target="_blank">Operating Results</a>
                    </td>
                    <td class="StaticTdLeft"> 
                       
                         <a href="../Results/ScoreCardRes.aspx" class="Link" target="_blank">Scorecard</a>
                                           
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step11</td>
                    <td class="StaticTdLeft">
                        <a href="PersonnelIn.aspx" class="Link" target="_blank">Personnel</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                  <td class="StaticTdLeft"> 
                       <a href="../Results/EndOfLifeRes.aspx" class="Link" target="_blank">End Of Life</a>    
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step12</td>
                    <td class="StaticTdLeft">
                       <a href="PlantConfig2.aspx" class="Link" target="_blank">Plant Space Requirements</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                     <td class="StaticTdLeft" style="height: 22px"> 
                        <a href="../../../Charts/S3Charts/CErgyGhg.aspx" class="Link" target="_blank">Graphic Tool</a> 
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step13</td>
                    <td class="StaticTdLeft">
                        <a href="EnergyIn.aspx" class="Link" target="_blank">Energy Assumptions</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step14</td>
                    <td class="StaticTdLeft">
                        <a href="CustomerIn.aspx" class="Link" target="_blank">Transportation Assumptions</a>
                    </td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step15
                    </td>
                    <td class="StaticTdLeft">
                        <a href="ExtraProcess.aspx" class="Link" target="_blank">Extra-process Assumptions</a>
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

