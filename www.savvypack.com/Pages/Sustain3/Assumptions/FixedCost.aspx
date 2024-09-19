<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="FixedCost.aspx.vb" Inherits="Pages_Sustain3_Assumptions_FixedCost" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
   
    
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:46%" class="PageHeading" onmouseover="Tip('Plant space requirements')" onmouseout="UnTip()" >
                  Plant Space requirements
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
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_P" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_P'); " />Production
                              <br />
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_W" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_W'); " />Warehouse
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_O" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_O'); " />Office
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_S" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_S'); " />Support
                              <br />
                                <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_T" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_T'); " />Total
                          
                          
                          
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
                             <asp:Button ID="Button1" runat="server" Text="Set" />
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