﻿<%@ Page Title="S3-Pallet Packaging" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="PalletIn.aspx.vb" Inherits="Pages_Sustain3_Assumptions_PalletIn" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
   
    
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Pallet Packaging')" onmouseout="UnTip()" >
                  Pallet Packaging
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
                             
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_IT" checked="checked"   onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_IT'); " />Item
                          <br />
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_N" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_N'); " />Number
                           <br /> 
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_NU" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_NU'); " />Number Of Uses
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WTS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_WTS'); " />Weight Each Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WTP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_WTP'); " />Weight Each Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ES" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_ES'); " />Energy Sugg 
                              <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_EP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_EP'); " />Energy Pref.
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GHGS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_GHGS'); " />CO2 Sugg 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_GHGP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_GHGP'); " />CO2 Pref. 
                             <br />
                         <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WATERS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_WATERS'); " />Water Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WATERP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_WATERP'); " />Water Pref. 
                             <br />    
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
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SS" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SS'); " />Ship Distance Sugg. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_SP" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_SP'); " />Ship Distance Pref. 
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_D" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_D'); " />Mfg. Department 
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


