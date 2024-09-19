<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain4.master" AutoEventWireup="false" 
CodeFile="ScoreCardRes.aspx.vb" Inherits="Pages_Sustain4_Results_ScoreCardRes" title="S4-Scorecard Results" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain4ContentPlaceHolder" Runat="Server">

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:58%" class="PageHeading" 
                    onmouseover="Tip('Scorecard Results')" onmouseout="UnTip()" >
                  Scorecard Results
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
                         <%-- <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_A" checked="checked"   onclick="showhideALLS('ctl00_Sustain4ContentPlaceHolder_A',1); " />Type
                             <br />--%>
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_B" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_B',9); " />Raw Score
                             <br /> 
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_C" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_C',9); " /> Low Range Sugg.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_D" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_D',9); " />Low Range Pref.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_E" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_E',9); " />High Range Sugg.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_F" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_F',9); " />High Range Pref.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_G" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_G',9); " />Weighting Sugg.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_H" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_H',9); " />Weighting Pref.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_I" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_I',9); " />Max Score
                           <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_J" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_J',9); " />Total Score
                          
                          
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

