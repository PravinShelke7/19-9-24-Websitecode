<%@ Page Title="S3-Raw material packaging" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="PalletInNewOut.aspx.vb" Inherits="Pages_Sustain3_IntResults_PalletInNewOut" %>

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
                 <div id="divHeader2"  class="divHeader2" onclick="toggleDiv('divContent2', 'img2')" >   
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
                 <div id="divContent2" class="divContent2" style="width:280px;"> 
                        <div style="margin-left:7px;margin-top:10px;margin-bottom:5px">
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_A" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_A'); " />Raw Material Weight Per Shipping Unit
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_B" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_B'); " />Raw Material Packaging Weight Per Shipping Unit
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_C" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_C'); " />Total Weight Per Shipping Unit
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_D" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_D'); " />Number Of Shipping Units Per Transport
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_E" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_E'); " />Raw Material Weight Per Transport
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_F" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_F'); " />Raw Material Packaging Weight Per Transport
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_G" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_G'); " />Total Weight Per Transport
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_H" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_H'); " />Total Number Of Transports
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_I" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_I'); " />Total Raw Material Packaging Weight
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_J" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_J'); " />Raw Material Packaging Energy
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_K" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_K'); " />Raw Material and Packaging Transportation Energy
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_L" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_L'); " />Raw Material Packaging CO2
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_M" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_M'); " />Raw Material and Packaging Transportation Co2
                            <br /> 
                             <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_N" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_N'); " />Raw Material Packaging Water
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_O" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_O'); " />Raw Material and Packaging Transportation Water
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_P" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_P'); " />Raw Material and Packaging Incineration Energy
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_Q" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_Q'); " />Raw Material and Packaging Incineration GHG
                            <br /> 
                            <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_R" checked="checked"  onclick="showhideALL('ctl00_Sustain3ContentPlaceHolder_R'); " />Raw Material and Packaging Compost GHG
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
