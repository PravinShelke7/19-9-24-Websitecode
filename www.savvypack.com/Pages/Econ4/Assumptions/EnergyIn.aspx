<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="EnergyIn.aspx.vb" 
Inherits="Pages_Econ4_Assumptions_EnergyIn" title="E4-Energy Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Energy Assumptions')" onmouseout="UnTip()" >
                  Econ4 - Energy Assumptions
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
                          <b>Energy Price</b>
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_Elec" checked="checked"   onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_Elec'); " />Electricity
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_NATG" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_NATG'); " />Natural Gas
                          <br />
                          <b>Energy Requirements</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PRODA" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_PRODA'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_WARA" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_WARA'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_OFFA" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_OFFA'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_SUPPA" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_SUPPA'); " />Support
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_TOTA" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_TOTA'); " />Total
                          <br />
                          <b>Energy Cost</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PRODB" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_PRODB'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_WARB" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_WARB'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_OFFB" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_OFFB'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_SUPPB" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_SUPPB'); " />Support
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_TOTB" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_TOTB'); " />Total
                            <br />
                          <b>Energy Cost</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PRODTC" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_PRODTC'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_WARTC" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_WARTC'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_OFFTC" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_OFFTC'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_SUPTC" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_SUPTC'); " />Support
                          <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_TOTTC" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_TOTTC'); " />Total
                          
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
                        <asp:TextBox ID="txtDWidth" runat="server" Text="270" CssClass="SmallTextBox"></asp:TextBox>
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
                 <a href="AdditionalEnergy.aspx" class="Link" target="_blank" style="font-weight:bold;">Addtional Energy Assumptions</a>  
              
        <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
         <br />
     </div>
   
   </div>
</asp:Content>

