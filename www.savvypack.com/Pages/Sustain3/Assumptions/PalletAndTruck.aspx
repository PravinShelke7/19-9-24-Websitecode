<%@ Page Title="S3-Pallet and Truck Configuration " Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="PalletAndTruck.aspx.vb" Inherits="Pages_Sustain3_Assumptions_PalletAndTruck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
   
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:52%" class="PageHeading" 
                    onmouseover="Tip('Pallet and Truck Configuration')" onmouseout="UnTip()" >
                  Pallet and Truck Configuration
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
                              <%  Try
                                      
                               
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
                              <b>Pallet Dimensions</b>
                              <br />
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PW" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PW'); " />Width
                              <br />
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PL" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PL'); " />Length
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PH" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PH'); " />Height
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CPP" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CPP'); " />Cartons Per Pallet
                              <br />
                             
                                <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PPP" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PPP'); " />Product Per Pallet
                              <br />
                               <b>Truck  Dimensions</b>
                                <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_TW" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_TW'); " />Width
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_TL" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_TL'); " />Length
                              <br />
                              <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_TH" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_TH'); " />Height
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_WL" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_WL'); " />Weight Limit 
                              <br />
                               <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_PPT" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_PPT'); " />Pallets Per Truck
                              <br />
                                <input type="checkbox" id="ctl00_Sustain3ContentPlaceHolder_CW" checked="checked"   onclick="showhideALL1('ctl00_Sustain3ContentPlaceHolder_CW'); " />Calculated Weight 
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