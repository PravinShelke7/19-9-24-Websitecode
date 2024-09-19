<%@ Page Title="S3-Support equipment" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="Equipment2In.aspx.vb" Inherits="Pages_Sustain3_Assumptions_Equipment2In" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
   
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Support equipment')" onmouseout="UnTip()" >
                  Support Equipment
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
                             
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_AD" checked="checked"   onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_AD'); " />Asset Description
                          <br />
                           <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ES" checked="checked"   onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_ES'); " />Maximum Annual Run Hours
                          
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECS" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_ECS'); " />Electricity Consumption Sugg.
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_ECP" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_ECP'); " />Electricity Consumption Pref.
                              
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_NCS" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_NCS'); " />Natural Gas Consumption Sugg.
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_NCP" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_NCP'); " />Natural Gas Consumption Pref.
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WaterS" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_WaterS'); " />Water Consumption Sugg.
                             <br />
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WaterP" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_WaterP'); " />Water Consumption Pref.
                             <br />   
                          <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_MD" checked="checked"  onclick="showhideALL2('ctl00_Sustain3ContentPlaceHolder_MD'); " />Mfg. Dept
                           
                          
                         
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
