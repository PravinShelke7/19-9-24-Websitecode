<%@ Page Title="S3-Raw material packaging" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="PalletInNew.aspx.vb" Inherits="Pages_Sustain3_Assumptions_PalletInNew" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
   
    
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Raw material packaging')" onmouseout="UnTip()" >
                  Raw Material Packaging 
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
                
                
                <td style="width:30%" class="PageSHeading">
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
   <table>
        <tr>
            <td>
                <div id="divHeader"  class="divHeader" onclick="toggleDiv('divContent', 'img1')">   
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr class="HTR">
                                <td class="TdLeft">
                                    Case Display  
                                </td>
                                <td class="TdRight">
                                    <img src="../../../Images/down.png" class="HeaderImg" id="img1" alt=""/>
                                </td>
                            </tr>
                        </table>
                
                
                </div>
                <div id="divContent" class="divContent"> 
                    <div style="margin-left:7px;margin-top:10px;margin-bottom:5px">
                              <%    
                                  Try
                                
                                      Dim CaseHide As New Integer
                                      For CaseHide = 1 To DataCnt + 1
                                          Response.Write("<Input type='checkbox' id='chkBox_" & (CaseHide) & "' value='1' checked='true' onclick='showhideColumn (" & CStr(CaseHide) & ")'>")
                                          Response.Write("Case" & CaseDesp(CaseHide - 1) & "<br/>")
                                      Next
                                      
                                  Catch ex As Exception
                                      
                                  End Try
                             %>
                             
                                    
                         
                    </div>
                                        
              </div>
            </td>
            <td>
                 <div id="divHeader2"  class="divHeader2" onclick="toggleDiv('divContent2', 'img2')">   
                        <table width="100%" cellpadding="0" cellspacing="0">
                            <tr class="HTR">
                                <td class="TdLeft">
                                    Layer Display    
                                </td>
                                <td class="TdRight">
                                    <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt=""/>
                                </td>
                            </tr>
                        </table>                
                </div>
                 <div id="divContent2" class="divContent2"> 
                        <div style="margin-left:7px;margin-top:10px;margin-bottom:5px">
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_TUC" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_TUC',1); " />Transport Unit Cube 
                            <br /> 
                          <b>Materials</b>    
                            <br /> 
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_A" checked="checked"   onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_A'); " />Item
                            <br />
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_B" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_B'); " />Number
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_C" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_C'); " />Weight
                            <br /> 
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_D" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_D'); " />Energy Suggested
                            <br /> 
  <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_E" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_E'); " />Energy Preferred
                            <br /> 
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_F" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_F'); " />CO2 Equivalent Suggested
                            <br /> 
  <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_G" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_G'); " />CO2 Equivalent Preferred
                            <br /> 
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WS" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_WS'); " />Water Sugg.
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WP" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_WP'); " />Water Pref.
                            <br />    
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_H" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_H'); " />Recycle
                            <br /> 
                                <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_I" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_I'); " />Recovery Suggested
                            <br /> 
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_J" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_J'); " />Recovery Preferred
                            <br /> 
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_K" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_K'); " />Sustainable Materials Suggested
                            <br />
                                <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_L" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_L'); " />Sustainable Materials Preferred
                            <br /> 
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_M'); " />PC Recycle Suggested
                            <br /> 
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_N" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_N'); " />PC Recycle Preferred
                            <br /> 
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_O" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_O'); " />Ship Suggested
                            <br /> 
                             <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_P" checked="checked"  onclick="showhideALLRW('ctl00_Sustain3ContentPlaceHolder_P'); " />Ship Preferred
                            <br /> 
                            
                            
                         
                        </div>   
                                     
                    </div>
            </td>
            <td>
               <div class="divHeader3">
                <table>
                  <tr style="height:20px;">
                     <td>
                        Column Width: 
                     </td>
                     <td>
                        <asp:TextBox ID="txtDWidth" runat="server" Text="200" CssClass="SmallTextBox"></asp:TextBox>
                     </td>
                     <td>
                         <asp:Button ID="btnWidthSet" runat="server" Text="Set" />
                     </td>
                     
                   </tr>
                </table>
                </div>
            
            </td>
        </tr>
        
   </table>
   <br />
   <br />
   <div id="ContentPagemargin">
   <div id="PageSection1" style="text-align:left">
        <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
         <br />
     </div>
   
   </div>
   </asp:Content>
