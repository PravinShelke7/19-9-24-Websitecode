<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="Efficiency.aspx.vb" Inherits="Pages_Econ3_Efficiency" title="E3-Efficiency Table Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
  
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
	<style type="text/css">
        .divUpdateprogress
        {
            left: 410px;
            top: 200px;
            position: absolute;
        }
    </style>
    <script>
          function removeSession() {
              //localStorage.removeItem("A6");
              document.cookie = "U6=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
          }

          window.setInterval(function () {

//              if (localStorage.getItem("A6") != null) {
//                  localStorage.removeItem("A6");
//                  location.reload();

              //              }
              if (document.cookie.length != 0) {

                  var ca = document.cookie.split(";");
                  for (var i = 0; i < ca.length; i++) {
                      var c = ca[i].trim();

                      if (c.indexOf("U6") == 0) {
                          document.cookie = "U6=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                          location.reload();

                      }
                  }
              }

          }, 1500);
</script>
 <script type="text/javascript">
     function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, countVal) {
         for (m = 0; m < countVal; m++) {
             var flag;
             document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
             index = m + 1;
             var arrCHK = [];
             var arrlbl = [];
             var arrmat = [];
             var arru = [];
             var arrcode;
             var i;
             var j;
             for (i = 0; i < 15; i++) {
                 arrmat[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_Material" + (i + 1) + "").value;
                 for (j = 0; j < 10; j++) {
                     arrCHK[j] = document.getElementById("ctl00_Econ3ContentPlaceHolder_chkMAT" + String.fromCharCode(65 + i) + "" + (j + 1) + "" + m + "").value;
                     arrlbl[j] = document.getElementById("ctl00_Econ3ContentPlaceHolder_MAT" + String.fromCharCode(65 + i) + "" + (j + 1) + "" + m + "").value;
                     arrcode = String.fromCharCode(65 + i);
                     if (document.getElementById("ctl00_Econ3ContentPlaceHolder_chkMAT" + String.fromCharCode(65 + i) + "" + (j + 1) + "" + m + "").checked == true) {
                         arru[j] = 1;
                     }
                     else {
                         arru[j] = 0;
                     }
                 }
                 if (m == 0) {
                     if (caseId1 > 1000) {
                         PageMethods.UpdateAllCases(caseId1, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 1) {
                     if (caseId2 > 1000) {
                         PageMethods.UpdateAllCases(caseId2, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 2) {
                     if (caseId3 > 1000) {
                         PageMethods.UpdateAllCases(caseId3, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 3) {
                     if (caseId4 > 1000) {
                         PageMethods.UpdateAllCases(caseId4, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }

                 else if (m == 4) {
                     if (caseId5 > 1000) {
                         PageMethods.UpdateAllCases(caseId5, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 5) {
                     if (caseId6 > 1000) {
                         PageMethods.UpdateAllCases(caseId6, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 6) {
                     if (caseId7 > 1000) {
                         PageMethods.UpdateAllCases(caseId7, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 7) {
                     if (caseId8 > 1000) {
                         PageMethods.UpdateAllCases(caseId8, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 8) {
                     if (caseId9 > 1000) {
                         PageMethods.UpdateAllCases(caseId9, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }
                 else if (m == 9) {
                     if (caseId10 > 1000) {
                         PageMethods.UpdateAllCases(caseId10, arru, arrcode, onSucceedAll, onErrorAll);
                     }
                 }

             }
         }
         return false;
     }




     function onSucceedAll(result) {
         //                     localStorage.setItem("A1", "A1");
         //                        //localStorage.setItem("A2", "A2");
         //                         localStorage.setItem("A3", "A3");
         //                          localStorage.setItem("A4", "A4");
         //                          localStorage.setItem("A5", "A5");
         //                          localStorage.setItem("A6", "A6");
         //                          localStorage.setItem("A7", "A7");
         //                          localStorage.setItem("A8", "A8");
         //                          localStorage.setItem("A9", "A9");
         //                          localStorage.setItem("A10", "A10");
         //                          localStorage.setItem("A11", "A11");
         //                          localStorage.setItem("A12", "A12");
         //                          localStorage.setItem("A13", "A13");
         //                          localStorage.setItem("A14", "A14");
         //                          localStorage.setItem("A15", "A15");
         //                             localStorage.setItem("A16", "A16");

         //                            localStorage.setItem("I1", "I1");
         //                        localStorage.setItem("I2", "I2");
         //                         localStorage.setItem("I3", "I3");

         //                          localStorage.setItem("R1", "R1");
         //                        localStorage.setItem("R2", "R2");
         //                         localStorage.setItem("R3", "R3");
         //                          localStorage.setItem("R4", "R4");
         //                          localStorage.setItem("R5", "R5");
         //                          localStorage.setItem("R6", "R6");
         //                          localStorage.setItem("R7", "R7");
         //                          localStorage.setItem("R8", "R8");
         //                          localStorage.setItem("R9", "R9");
         //                          localStorage.setItem("R10", "R10");
         //                          localStorage.setItem("R11", "R11");
         //                          localStorage.setItem("R12", "R12");
         //                          localStorage.setItem("R13", "R13");

         document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         // document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_13=U_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_14=U_14;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_15=U_15;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_16=U_16;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "U_17=U_17;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

         document.cookie = "V1=V1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "V2=V2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "V3=V3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

         document.cookie = "W1=W1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W2=W2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W3=W3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W4=W4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W5=W5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W6=W6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W7=W7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W8=W8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W9=W9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W_10=W_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W_11=W_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W_12=W_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
         document.cookie = "W_13=W_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";



         document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';

     }


     function onErrorAll(result) {
     }
    </script>
    <script type="text/javascript">
        function clickButton(e) {
            var evt = e ? e : window.event;


            if (evt.keyCode == 13) {

                return false;
            }

        }

        function Update(caseId, index) {
            var flag;
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
            //index = m + 1;
            var arrCHK = [];
            var arrlbl = [];
            var arrmat = [];
            var arru = [];
            var arrcode;
            var i;
            var j;
            for (i = 0; i < 15; i++) {
                arrmat[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_Material" + (i + 1) + "").value;
                for (j = 0; j < 10; j++) {
                    arrCHK[j] = document.getElementById("ctl00_Econ3ContentPlaceHolder_chkMAT" + String.fromCharCode(65 + i) + "" + (j + 1) + "" + index + "").value;
                    arrlbl[j] = document.getElementById("ctl00_Econ3ContentPlaceHolder_MAT" + String.fromCharCode(65 + i) + "" + (j + 1) + "" + index + "").value;
                    arrcode = String.fromCharCode(65 + i);
                    if (document.getElementById("ctl00_Econ3ContentPlaceHolder_chkMAT" + String.fromCharCode(65 + i) + "" + (j + 1) + "" + index + "").checked == true) {
                        arru[j] = 1;
                    }
                    else {
                        arru[j] = 0;
                    }
                }
                PageMethods.UpdateAllCases(caseId, arru, arrcode, onSucceed, onError);
            }
            return false;

        }

        function onSucceed(result) {


            //                       localStorage.setItem("A1", "A1");
            //                       // localStorage.setItem("A2", "A2");
            //                         localStorage.setItem("A3", "A3");
            //                          localStorage.setItem("A4", "A4");
            //                          localStorage.setItem("A5", "A5");
            //                          localStorage.setItem("A6", "A6");
            //                         localStorage.setItem("A7", "A7");
            //                          localStorage.setItem("A8", "A8");
            //                          localStorage.setItem("A9", "A9");
            //                          localStorage.setItem("A10", "A10");
            //                          localStorage.setItem("A11", "A11");
            //                          localStorage.setItem("A12", "A12");
            //                          localStorage.setItem("A13", "A13");
            //                          localStorage.setItem("A14", "A14");
            //                          localStorage.setItem("A15", "A15");
            //                             localStorage.setItem("A16", "A16");

            //                            localStorage.setItem("I1", "I1");
            //                        localStorage.setItem("I2", "I2");
            //                         localStorage.setItem("I3", "I3");

            //                          localStorage.setItem("R1", "R1");
            //                        localStorage.setItem("R2", "R2");
            //                         localStorage.setItem("R3", "R3");
            //                          localStorage.setItem("R4", "R4");
            //                          localStorage.setItem("R5", "R5");
            //                          localStorage.setItem("R6", "R6");
            //                          localStorage.setItem("R7", "R7");
            //                          localStorage.setItem("R8", "R8");
            //                          localStorage.setItem("R9", "R9");
            //                          localStorage.setItem("R10", "R10");
            //                          localStorage.setItem("R11", "R11");
            //                          localStorage.setItem("R12", "R12");
            //                          localStorage.setItem("R13", "R13");

            document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            // document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_13=U_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_14=U_14;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_15=U_15;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_16=U_16;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_17=U_17;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

            document.cookie = "V1=V1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "V2=V2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "V3=V3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

            document.cookie = "W1=W1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W2=W2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W3=W3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W4=W4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W5=W5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W6=W6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W7=W7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W8=W8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W9=W9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W_10=W_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W_11=W_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W_12=W_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "W_13=W_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";



            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';
        }

        function addCommas(nStr) {
            nStr += '';
            x = nStr.split('.');
            x1 = x[0];
            x2 = x.length > 1 ? '.' + x[1] : '';
            var rgx = /(\d+)(\d{3})/;
            while (rgx.test(x1)) {
                x1 = x1.replace(rgx, '$1' + ',' + '$2');
            }
            return x1 + x2;
        }

        function onError(result) {
        }
        function onSucceed1(result) {
            document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_1_2").value = "211";
        }
       
      
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Efficiency Table Assumptions')" onmouseout="UnTip()" >
                  Econ3 - Efficiency Table Assumptions
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
                             
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MS" checked="checked"   onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_MS'); " />Department Selection
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN1" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN1'); " />Material 1
                             <br />                           
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN2" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN2'); " />Material 2
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN3" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN3'); " />Material 3 
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN4" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN4'); " />Material 4
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN5" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN5'); " />Material 5
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN6" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN6'); " />Material 6
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN7" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN7'); " />Material 7
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN8" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN8'); " />Material 8
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN9" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN9'); " />Material 9
                             <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DN10" checked="checked"  onclick="showhideALLOpOut('ctl00_Econ3ContentPlaceHolder_DN10'); " />Material 10
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
<div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
        <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
         <br />
     </div>
   </div>
   </asp:Content>
