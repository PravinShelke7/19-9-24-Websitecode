<%@ Page Title="S3-Material Assumption" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="Extrusion.aspx.vb" Inherits="Pages_Sustain3_Assumptions_Extrusion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
   
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Material Assumption')" onmouseout="UnTip()" >
                  Material Assumptions
                </td>
                
                <td style="width:23%" class="PageSHeading">
                    <table>
                        <tr>
                            <td>
                                Assumption ID:
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
                                                                            
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M" checked="checked"   onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_M'); " />Material
                          <br />
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_T" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_T'); " />Thickness
                           <br /> 
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_RE" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_RE'); " />Recycle
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ES" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_ES'); " />Energy Sugg.
                              <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_EP'); " />Energy Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GHGS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_GHGS'); " />CO2 Sugg.
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GHGP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_GHGP'); " />CO2 Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WaterS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_WaterS'); " />Water Sugg 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WaterP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_WaterP'); " />Water Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SS'); " />Ship Distance Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SP'); " />Ship Distance Pref. 
                             <br />
                                   
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_E" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_E'); " />Extra-process 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SGS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SGS'); " />Specific Gravity Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SGP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SGP'); " />Specific Gravity Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_W" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_W'); " />Weight/Area 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PSP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_PSP'); " />Shipping Unit 
                            <br />
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SSC" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SSC'); "/>Shipping Selector 
                            <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_D" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_D'); " />Mfg. Department 
                       <br/>
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_RECS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_RECS'); " />Recovery Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_RECP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_RECP'); " />Recovery Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SUS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SUS'); " />Sustainable Materials Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SUP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SUP'); " />Sustainable Materials Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_PCS'); " />PC Recycle Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_PCP'); " />PC Recycle Pref. 
                        <br/>
                        <b>Discrete Materials</b>
                          <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCMM" checked="checked"  onclick="showhideALLNEW('ctl00_Sustain3ContentPlaceHolder_PCMM'); " />Material
                          <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCML" checked="checked"  onclick="showhideALLNEW('ctl00_Sustain3ContentPlaceHolder_PCML'); " />Weight 
                          <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCMK" checked="checked"  onclick="showhideALLNEW('ctl00_Sustain3ContentPlaceHolder_PCMK'); " />Energy 
                          <br />
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCMN" checked="checked"  onclick="showhideALLNEW('ctl00_Sustain3ContentPlaceHolder_PCMN'); " />CO2 Equivalent
                          <br/>
                          
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCIN" checked="checked"  onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PCIN'); " />Include Weight of Discrete Materials in P&L statements. 
                          <br />  
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PCMQ" checked="checked"  onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PCMQ'); " />Printing Plates 
                         <br/>                  
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

