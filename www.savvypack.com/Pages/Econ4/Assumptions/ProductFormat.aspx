<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="ProductFormat.aspx.vb" Inherits="Pages_Econ4_Assumptions_ProductFormat" title="Product Format" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">
  
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Product Format Assumptions')" onmouseout="UnTip()" >
                  Econ4 - Product Format Assumptions
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
                        <table cellpadding="0" cellspacing="0">
                            <tr class="HTR">
                                <td class="TdLeft" >
                                    Layer Display    
                                </td>
                                <td class="TdRight">
                                    <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt=""/>
                                </td>
                            </tr>
                        </table>                
                </div>
                 <div id="divContent2" class="divContent2"> 
                        <div style="margin-left:7px;margin-top:10px;margin-bottom:5px;">
                             
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PF" checked="checked"   onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_PF'); " />Product Format
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_I2" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_I2'); " />Input 1
                             <br />                           
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_I3" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_I3'); " />Input 2
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_I4" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_I4'); " />Input 3
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_I5" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_I5'); " />Input 4
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_I6" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_I6'); " />Input 5
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PW" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_PW'); " />Packaging And Product Weight
                                                                     
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_RD" checked="checked"  onclick="showhideALL3('ctl00_Econ4ContentPlaceHolder_RD'); " />Roll Diameter
                      
                          </div>   
                                     
                    </div>
            </td>
            <td >
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