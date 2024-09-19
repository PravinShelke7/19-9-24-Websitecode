<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="AdditionalEnergy.aspx.vb" Inherits="Pages_Econ3_Assumptions_AdditionalEnergy" title="E3-Additional Energy Info" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
	  <script type="text/javascript">
          function removeSession() {
              //localStorage.removeItem("A16");
              document.cookie = "U_16=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
          }

          window.setInterval(function () {

//              if (localStorage.getItem("A16") != null) {
//                  localStorage.removeItem("A16");
//                  location.reload();
              //              }
              if (document.cookie.length != 0) {

                  var ca = document.cookie.split(";");
                  for (var i = 0; i < ca.length; i++) {
                      var c = ca[i].trim();

                      if (c.indexOf("U_16") == 0) {
                          document.cookie = "U_16=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                          location.reload();

                      }
                  }
              }

          }, 1500);
</script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Additional Energy Info')" onmouseout="UnTip()" >
                  Additional Energy Info
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
                        
                          <b>Plant Space</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PROD" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PROD'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WAR" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_WAR'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_OFF" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_OFF'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SUPP" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SUPP'); " />Support
                          <br />
                          
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
        <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
         <br />
     </div>
   
   </div>
   </asp:Content>

