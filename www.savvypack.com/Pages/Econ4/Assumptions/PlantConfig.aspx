<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="PlantConfig.aspx.vb" Inherits="Pages_Econ4_Assumptions_PlantConfig" title="Plant Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">
  
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Plant Configuration')" onmouseout="UnTip()" >
                  Econ4 - Plant Configuration
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
                                          Response.Write("Case#" & CaseDesp(CaseHide - 1) & "<br/>")
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
                             
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_MS" checked="checked"   onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_MS'); " />Required Departments
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN1" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN1'); " />Next Process 1
                             <br />                           
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN2" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN2'); " />Next Process 2
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN3" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN3'); " />Next Process 3 
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN4" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN4'); " />Next Process 4
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN5" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN5'); " />Next Process 5
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN6" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN6'); " />Next Process 6
                             <br />
                           <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN7" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN7'); " />Next Process 7
                             <br />
                           <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN8" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN8'); " />Next Process 8
                             <br />
                           <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_DN9" checked="checked"  onclick="showhideALL('ctl00_Econ4ContentPlaceHolder_DN9'); " />Next Process 9
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
                        <asp:TextBox ID="txtDWidth" runat="server" Text="300" CssClass="SmallTextBox"></asp:TextBox>
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