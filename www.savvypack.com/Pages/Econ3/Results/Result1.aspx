<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="Result1.aspx.vb" Inherits="Pages_Econ3_Results_Result1" title="E3-Direct Materials with Depreciation (Currency)" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
   
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
	 <script type="text/javascript">
      function removeSession(val1) {

          // localStorage.removeItem("R1");
          document.cookie = "W14=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
      }
 
       window.setInterval(function () {

//           if (localStorage.getItem("R1") != null) {
//              localStorage.removeItem("R1");
//              location.reload();
           //                         }
           if (document.cookie.length != 0) {

               var ca = document.cookie.split(";");
               for (var i = 0; i < ca.length; i++) {
                   var c = ca[i].trim();
			while (c.charAt(0) == ' ') {
      			c = c.substring(1);
  			  }
                   if (eval(c.indexOf("W14")) == 0) {

                       document.cookie = "W14=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                       location.reload();
                   }
               }
           }

       }, 1500);
</script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Direct Materials with Depreciation (Currency)')" onmouseout="UnTip()" >
                  Econ3 - Direct Materials with Depreciation (Currency) 
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
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES1" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES1'); " />Material 1
                             <br />
                             <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES2" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES2'); " />Material 2
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES3" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES3'); " />Material 3
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES4" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES4'); " />Material 4
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES5" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES5'); " />Material 5
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES6" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES6'); " />Material 6
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES7" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES7'); " />Material 7
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES8" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES8'); " />Material 8
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES9" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES9'); " />Material 9 
                             <br />
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES10" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES10'); " />Material 10                             
                              <br />                             
                               <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES11" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES11'); " />Direct Materials
                                 <br />
                                 <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES12" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES12'); " />Scrap
                                 <br />
                                <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES13" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES13'); " />Discrete Materials
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES14" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES14'); " />Raw Material Cost
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES15" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES15'); " />Conversion
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES16" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES16'); " />Margin
                                 <br />
                                  <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MDES17" checked="checked"   onclick="showhide('ctl00_Econ3ContentPlaceHolder_MDES17'); " />Total Market Price                                
                             
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

