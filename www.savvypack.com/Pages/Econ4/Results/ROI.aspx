<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="ROI.aspx.vb" 
Inherits="Pages_Econ4_Results_ROI" title="E4-ROI Comparison" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('ROI Comparison')" onmouseout="UnTip()" >
                  Econ4 - ROI Comparison
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
   
  <div id="ContentPagemargin">
   <div id="PageSection1" style="text-align:left">
        
        <table>
          <tr style="height:20px;">
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
                  <div class="divHeader2">
                    <table>
                        <tr>
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
         <table width="500px" >
                     <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;padding-left:5px;" colspan="3">
                            ROI Selection
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td style="width:30%;padding-left:5px;">
                            Cost of Capital: 
                        </td>
                        <td>
                            <asp:TextBox id="txtCostCapital" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="width:60%">
                            Annual Percent
                            <asp:RequiredFieldValidator ID="reqtxtCostCapital" runat="server" 
                                ControlToValidate="txtCostCapital" 
                                ErrorMessage="Cost of capital can not be blank" Display="Dynamic" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr class="AlterNateColor2">
                        <td style="width:30%;padding-left:5px;">
                            Time Period: 
                        </td>
                        <td>
                            <asp:TextBox id="txtTimePeriod" runat="server" CssClass="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="width:60%">
                            Years
                             <asp:RequiredFieldValidator ID="reqtxtTimePeriod" runat="server" 
                                ControlToValidate="txtTimePeriod" 
                                ErrorMessage="Time period can not be blank" Display="Dynamic" 
                                SetFocusOnError="True"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                     <tr class="AlterNateColor1">
                        <td style="width:30%;padding-left:5px;">
                            Starting Point Case:  
                        </td>
                        <td colspan="2">
                              <asp:DropDownList ID="ddlSPCases" CssClass="DropDown" Width="95%" runat="server"></asp:DropDownList>
                        </td>
                   </tr>    
                    <tr class="AlterNateColor2">
                        <td>
                        </td>
                       <td colspan="2">
                            <asp:Button ID="btnSubmitt" runat="server" Text="Submit" CssClass="Button" Style="margin-left:0" />
                         </td>
                   </tr>
                         
             </table>
                        
        <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
         <br />
     </div>
   
   </div>
</asp:Content>

