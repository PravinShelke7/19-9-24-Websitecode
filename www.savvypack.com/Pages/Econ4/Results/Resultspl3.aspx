<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="Resultspl3.aspx.vb" 
Inherits="Pages_Econ4_Results_Resultspl3" title="E4-Profit And Loss (Per Unit)" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">
 
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Profit And Loss (Per Unit)')" onmouseout="UnTip()" >
                  Econ4 - Profit And Loss (Per Unit)  
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
                 <div id="divHeader2"  class="divHeader4" onclick="toggleDiv('divContent2', 'img2')">   
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
                 <div id="divContent2" class="divContent3"> 
                        <div style="margin-left:7px;margin-top:10px;margin-bottom:5px;overflow:auto;height:250px;">
                                 
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_SVOL1" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_SVOL1'); " />Sales Volume In Weight 
                             <br />
                             <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_SVOL2" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_SVOL2'); " />Sales Volume In Unit 
                             <br />
                          <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES1" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES1'); " />Revenue
                             <br />
                             <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES2" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES2'); " />Materials
                             <br />
                           <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES3" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES3'); " />Labor
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES4" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES4'); " />Energy
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES5" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES5'); " />Distribution Packaging
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES6" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES6'); " />Shipping to Customer
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES7" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES7'); " />Variable Margin
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES8" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES8'); " />Office Supplies
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES9" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES9'); " />LaborP 
                             <br />
                              <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES10" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES10'); " />EnergyP                             
                              <br />                             
                               <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES11" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES11'); " />Lease Cost
                                 <br />
                                 <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES12" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES12'); " />Insurance
                                 <br />
                                <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES13" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES13'); " />Utilities
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES14" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES14'); " />Communications
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES15" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES15'); " />Travel
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES16" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES16'); " />Maintenance Supplies
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES17" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES17'); " />Minor Equipment
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES18" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES18'); " />Outside Services
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES19" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES19'); " />Professional Services
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES20" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES20'); " />Laboratory Supplies
                                 <br /> 
                                   <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES21" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES21'); " />Ink Supplies
                                 <br />
                                 <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES22" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES22'); " />Plate Supplies
                                 <br />
                                <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES23" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES23'); " />Metal Supplies
                                 <br />
                                <%--  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES24" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES24'); " />Depreciation
                                 <br />--%>
                                  <input type="checkbox" id="ctl00_Econ4ContentPlaceHolder_PDES24" checked="checked"   onclick="showhide('ctl00_Econ4ContentPlaceHolder_PDES24'); " />Plant Margin
                                                             
                              
                             
                        </div>   
                                     
                    </div>
            </td>
            <td>
               <div class="divHeader3">
                <table style="margin-left:10px;">
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

