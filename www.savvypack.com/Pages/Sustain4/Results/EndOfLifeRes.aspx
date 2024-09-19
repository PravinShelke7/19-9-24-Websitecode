<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain4.master" AutoEventWireup="false" CodeFile="EndOfLifeRes.aspx.vb" 
Inherits="Pages_Sustain4_Results_EndOfLifeRes" title="S4-Material Balance Statement (Results)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain4ContentPlaceHolder" Runat="Server">
 
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:58%" class="PageHeading" 
                    onmouseover="Tip('Material Balance Statement (Results)')" onmouseout="UnTip()" >
                  Material Balance Statement (Results)
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
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_SVOL1" checked="checked"   onclick="showhideALL1('ctl00_Sustain4ContentPlaceHolder_SVOL1',1); " />Sales Volume in Weight
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_SVOL2" checked="checked"   onclick="showhideALL1('ctl00_Sustain4ContentPlaceHolder_SVOL2',1); " />Sales Volume in Unit
                             <br /> 
                            <b>Product</b><br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M2" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M2',5); " />Raw Materials
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M3" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M3',5); " />Production Waste
                             <br /> 
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M4" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M4',5); " />Finished Product
                             <br />
                           <b>Raw Material Packaging</b><br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M6" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M6',5); " />Total Flow
                             <br />                        
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M7" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M7',5); " />Waste
                             <br />
                           <b>Product Packaging</b><br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M9" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M9',5); " />Total Flow
                             <br />                            
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M10" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M10',5); " />Waste
                             <br />
                           <b>Other Waste</b><br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M12" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M12',5); " />Plant
                             <br />
                           <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M13" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M13',5); " />Office
                            <br />
                            <b>End Of life Energy (MJ)</b><br />             
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M15" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M15',5); " />Finished Product (MJ)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M16" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M16',5); " />Production Waste (MJ)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M17" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M17',5); " />Raw Material Packaging Waste (MJ)   
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M18" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M18',5); " />Product Packaging Waste (MJ)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M19" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M19',5); " />Plant Waste (MJ)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M20" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M20',5); " />Office Waste (MJ)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M21" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M21',5); " />Total End Of life Energy (MJ)
                            <br />
                            <b>End Of Life GHG (kg)</b><br />             
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M23" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M23',5); " />Finished Product (kg)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M24" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M24',5); " />Production Waste (kg)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M25" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M25',5); " />Raw Material Packaging Waste (kg)   
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M26" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M26',5); " />Product Packaging Waste (kg)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M27" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M27',5); " />Plant Waste (kg)
                            <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M28" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M28',5); " />Office Waste (kg)
                         <br />
                             <input type="checkbox" id="ctl00_Sustain4ContentPlaceHolder_M29" checked="checked"   onclick="showhideALLCnt('ctl00_Sustain4ContentPlaceHolder_M29',5); " />Total End Of life GHG (kg)
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

