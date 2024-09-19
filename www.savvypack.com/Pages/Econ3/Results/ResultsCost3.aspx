<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="ResultsCost3.aspx.vb" Inherits="Pages_Econ3_Results_ResultsCost3" title="E3-Manufacturing Cost (Unit)" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
	 <script type="text/javascript">
          function removeSession() {
              //localStorage.removeItem("R9");
              document.cookie = "W9=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
          }

          window.setInterval(function () {

//              if (localStorage.getItem("R9") != null) {
//                  localStorage.removeItem("R9");
//                  location.reload();
              //              }
              if (document.cookie.length != 0) {

                  var ca = document.cookie.split(";");
                  for (var i = 0; i < ca.length; i++) {
                      var c = ca[i].trim();

                      if (c.indexOf("W9") == 0) {

                          document.cookie = "W9=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                          location.reload();
                      }
                  }
              }

          }, 1500);
</script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Manufacturing Cost (Unit)')" onmouseout="UnTip()" >
                  Econ3 - Manufacturing Cost (Unit) 
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
                                    Row Display    
                                </td>
                                <td class="TdRight">
                                    <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt=""/>
                                </td>
                            </tr>
                        </table>                
                </div>
                 <div id="divContent2" class="divContent3"> 
                        <div style="margin-left:7px;margin-top:10px;margin-bottom:5px;overflow:auto;height:250px;">
                                 
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SVOL1" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_SVOL1'); " />Sales Volume In Weight 
                             <br />
                             <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SVOL2" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_SVOL2'); " />Sales Volume In Unit 
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES1" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES1'); " />Material Cost
                             <br />
                             <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES2" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES2'); " />Labor Cost
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES3" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES3'); " />Energy Cost
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES4" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES4'); " />Distribution Packaging Cost
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES5" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES5'); " />Shipping Cost
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES6" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES6'); " />Total Variable Cost
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES7" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES7'); " />Office Supplies Cost
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES8" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES8'); " />Labor Cost
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES9" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES9'); " />Energy Cost 
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES10" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES10'); " />Lease Cost                             
                              <br />                             
                               <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES11" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES11'); " />Insurance Cost
                                 <br />
                                 <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES12" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES12'); " />Utilities Cost
                                 <br />
                                <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES13" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES13'); " />Communications Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES14" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES14'); " />Travel Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES15" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES15'); " />Maintenance Supplies Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES16" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES16'); " />Minor Equipment Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES17" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES17'); " />Outside Services Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES18" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES18'); " />Professional Services Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES19" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES19'); " />Laboratory Supplies Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES20" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES20'); " />Ink Supplies Cost
                                 <br /> 
                                   <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES21" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES21'); " />Plate Supplies Cost
                                 <br />
                                 <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES22" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES22'); " />Metal Supplies Cost
                                 <br />
                                <%--<input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES23" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES23'); " />Depreciation
                                 <br />--%>
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES24" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES24'); " />Total Fixed Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PDES25" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_PDES25'); " />Total Cost
                                                             
                              
                             
                        </div>   
                                     
                    </div>
            </td>
            <td>
               <div class="divHeader3" style="display:none;">
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