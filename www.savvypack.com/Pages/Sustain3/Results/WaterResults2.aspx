﻿<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="WaterResults2.aspx.vb"
 Inherits="Pages_Sustain3_Results_WaterResults2" title="S3-Water Gas (Per Unit Weight)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
 
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:58%" class="PageHeading" 
                    onmouseover="Tip('S3-Water (Per Unit Weight)')" onmouseout="UnTip()" >
                     S3-Water (Per Unit Weight)
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
                             
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M1" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M1',1); " />Sales Volume in Weight
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M2" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M2',1); " />Sales Volume in Unit
                             <br /> 
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M3" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M3',1); " />Raw Materials
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M4" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M4',1); " />Raw Materials Pack.
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M5" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M5',1); " />RM & Pack Transport
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M6" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M6',1); " />Process 	
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M7" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M7',1); " />Distribution Pack. 
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M8" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M8',1); " />DP Transport 
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M9" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M9',1); " />Transport to Customer
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M10" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M10',1); " />Total Water
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M11" checked="checked"  onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M11',1); " />Purchased Materials
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M12" checked="checked"  onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M12',1); " />Process
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M13" checked="checked"  onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M13',1); " />Transportation
                             <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M14" checked="checked"  onclick="showhideALLCnt('ctl00_Sustain3ContentPlaceHolder_M14',1); " />Total Water
                          
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

