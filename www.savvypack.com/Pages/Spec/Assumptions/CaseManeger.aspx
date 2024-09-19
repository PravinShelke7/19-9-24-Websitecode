<%@ Page Title="SPEC-Case Manager" Language="VB" MasterPageFile="~/Masters/Spec.master" AutoEventWireup="false" CodeFile="CaseManeger.aspx.vb" Inherits="Pages_Spec_Assumptions_CaseManeger" %>

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
                                Specification ID:
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
                         &nbsp;</td>
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
                        <a href="Extursion.aspx" class="Link" target="_blank">Material and structure</a>
                    </td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/S1Ergy.aspx" class="Link" target="_blank">Energy</a></td>
                </tr>
                
                 <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step2
                    </td>
                    <td class="StaticTdLeft">
                        Product Format
                    </td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                         <a href="../Results/S1Ghg.aspx" class="Link" target="_blank">Greenhouse Gases</a></td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step3
                    </td>
                    <td class="StaticTdLeft">
                        Process</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                      <td class="StaticTdLeft"><a href="../Results/S1AllSpecErgy.aspx" class="Link" target="_blank">Total Energy</a></td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step4</td>
                    <td class="StaticTdLeft">
                         &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                   <td class="StaticTdLeft"><a href="../Results/S1AllSpecGhg.aspx" class="Link" target="_blank">Total GHG</a></td>
                </tr>
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step5</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step6</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step7</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                       
                         &nbsp;</td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step8</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step9</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step10</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft"> 
                        &nbsp;</td>
                </tr>
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step11</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step12</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        Step13</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
                
                
                
                <tr class="AlterNateColor2"> 
                    <td class="StaticTdLeft">
                        Step14</td>
                    <td class="StaticTdLeft">
                        &nbsp;</td>
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
                        &nbsp;</td>
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

