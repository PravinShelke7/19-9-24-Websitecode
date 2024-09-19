<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="CaseManager.aspx.vb" Inherits="Pages_Econ4_Assumptions_CaseManager" title="Case Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">
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
         <table width="98%" style="text-align:left;">
            <tr style="height:20px;">
                    <td style="width: 56px">
                    
                    </td>
                    <td style="width: 223px">
                        
                    </td>
                    <td style="width: 169px">
                      <b><a href="Preferences.aspx" class="Link" target="_blank">Preferences</a></b>                       
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr class="PageSSHeading" style="height:20px;">
                    <td class="TdHeading">
                    
                    </td>
                    <td class="TdHeading" style="width: 223px">
                        Specify Assumptions
                    </td>
                    <td class="TdHeading" style="width: 169px">
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
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="Extrusion.aspx" class="Link" target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        <a href="../IntResults/ExtrusionOut.aspx" class="Link" target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/Resultspl1.aspx" class="Link" target="_blank">Profit And 
                         Loss (Currency)</a>
                    </td>
                </tr>
                
                 <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step2
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="ProductFormat.aspx" class="Link" target="_blank">Product Format</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/Resultspl2.aspx" class="Link" target="_blank">Profit And 
                         Loss (Per Unit Weight)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step3
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                         <a href="PalletAndTruck.aspx" class="Link" target="_blank">Pallet and Truck Configuration</a>&nbsp;
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                      <td class="StaticTdLeft"> 
                         <a href="../Results/Resultspl3.aspx" class="Link" target="_blank">Profit And Loss (Per Unit)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step4</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PalletIn.aspx" class="Link" target="_blank">Pallet Packaging</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/ResultsplDep1.aspx" class="Link" target="_blank">Profit And 
                        Loss W/depreciation (Currency)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step5</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PlantConfig.aspx" class="Link" target="_blank">Department Configuration</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/ResultsplDep2.aspx" class="Link" target="_blank"> Profit And Loss W/depreciation (Per Unit Weight)</a>
                    </td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step6</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="Efficiency.aspx" class="Link" target="_blank">Material Efficiency</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/ResultsplDep3.aspx" class="Link" target="_blank">Profit And Loss W/depreciation (Per Unit)</a>
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step7</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="EquipmentIn.aspx" class="Link" target="_blank">Process Equipment</a> </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                       
                         <a href="../Results/ResultsCost1.aspx" class="Link" target="_blank">Cost (Currency)</a>
                                                               
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step8</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="Equipment2In.aspx" class="Link" target="_blank">Support Equipment</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/ResultsCost2.aspx" class="Link" target="_blank"> Cost (Per Unit Weight)</a>
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step9</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="OperationsIn.aspx" class="Link" target="_blank">Operating Parameters</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                         <a href="../IntResults/OperationsOut.aspx" class="Link" target="_blank">Operating Results</a>
                    </td>
                    <td class="StaticTdLeft"> 
                        
                        <a href="../Results/ResultsCost3.aspx" class="Link" target="_blank">Cost (Per Unit)</a></td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step10</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PersonnelIn.aspx" class="Link" target="_blank">Personnel</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                      <a href="../IntResults/PersonnelOut.aspx"  class="Link" target="_blank">Personnel Results</a>
                    </td>
                    <td class="StaticTdLeft"> 
                        
                         <a href="../Results/ResultsCostD1.aspx" class="Link" target="_blank">Cost W/depreciation (Currency)</a>
                                                               
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step11</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PlantConfig2.aspx" class="Link" target="_blank">Plant Space Requirements</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                         
                     
                                            
                    </td>
                    <td class="StaticTdLeft"> 
                        
                        <a href="../Results/ResultsCostD2.aspx" class="Link" target="_blank"> Cost W/depreciation (Per Unit Weight)</a>
                                            
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step12</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="EnergyIn.aspx" class="Link" target="_blank">Energy Assumptions</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                       <a href="../Results/ResultsCostD3.aspx" class="Link" target="_blank"> Cost W/depreciation (Per Unit)</a> 
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step13</td>
            
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="CustomerIn.aspx" class="Link" target="_blank">Customer Specifications</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        <a href="../Results/ROI.aspx" class="Link" target="_blank">ROI Comparison</a>
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step14 </td> <td class="StaticTdLeft" style="width: 223px">
                        <a href="FixedCost.aspx" class="Link" target="_blank">Fixed Cost Assumptions</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                           <a href="../../../Charts/E4Charts/CPrftCost.aspx" class="Link" target="_blank">Graphic Tool</a>
                    </td>
                </tr>
                 <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                    Step15
                       </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="DepreciationAssumption.aspx" class="Link" target="_blank">Depreciation Assumption</a></td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
               
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft" style="width: 223px">
                        &nbsp;</td>
                    <td class="StaticTdLeft" style="width: 169px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
            </table>
         <br />
     </div>
   
   </div>
</asp:Content>


