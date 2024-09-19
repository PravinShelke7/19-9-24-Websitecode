<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="PlantConfig.aspx.vb" Inherits="Pages_Econ3_Assumptions_PlantConfig" title="E3-Plant Configuration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
  
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
	 <script>
          function removeSession() {
              //localStorage.removeItem("A5");
              document.cookie = "U5=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
          }

          window.setInterval(function () {

//              if (localStorage.getItem("A5") != null) {
//                  localStorage.removeItem("A5");
//                  location.reload();

//              }
              if (document.cookie.length != 0) {

                  var ca = document.cookie.split(";");
                  for (var i = 0; i < ca.length; i++) {
                      var c = ca[i].trim();

                      if (c.indexOf("U5") == 0) {
                          document.cookie = "U5=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                          location.reload();

                      }
                  }
              }

          }, 1500);
</script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Plant Configuration')" onmouseout="UnTip()" >
                  Econ3 - Plant Configuration
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
                                    Row Display    
                                </td>
                                <td class="TdRight">
                                    <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt=""/>
                                </td>
                            </tr>
                        </table>                
                </div>
                 <div id="divContent2" class="divContent2"> 
                        <div style="margin-left:7px;margin-top:10px;margin-bottom:5px">
                             
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MS" checked="checked"   onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_MS'); " />Required Departments
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN1" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN1'); " />Next Process 1
                             <br />                           
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN2" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN2'); " />Next Process 2
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN3" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN3'); " />Next Process 3 
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN4" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN4'); " />Next Process 4
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN5" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN5'); " />Next Process 5
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN6" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN6'); " />Next Process 6
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN7" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN7'); " />Next Process 7
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN8" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN8'); " />Next Process 8
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN9" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN9'); " />Next Process 9
                                  <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN10" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN10'); " />Next Process 10
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN11" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN11'); " />Next Process 11
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN12" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN12'); " />Next Process 12
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN13" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN13'); " />Next Process 13
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN14" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN14'); " />Next Process 14
                           </div>   
                                     
                    </div>
            </td>
            <td>
               <div class="divHeader3" style="display:none;">
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