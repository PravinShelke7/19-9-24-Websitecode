<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false"
    CodeFile="OperationsIN.aspx.vb" Inherits="Pages_Econ3_Assumptions_OperationsIN"
    Title="E3-Operating Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
    <style type="text/css">
        .PrefTextBox
        {
            font-family: Optima;
            font-size: 11.5Px;
            height: 17px;
            width: 50px;
            background-color: #FEFCA1;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: right;
        }
        .divHeader5
        {
            position: absolute;
            margin-top: 0px;
            margin-left: 600px;
            text-align: left;
            vertical-align: top;
            width: 210px;
            z-index: 1;
        }
        .divContent3
        {
            overflow: hidden;
            display: none;
            margin-top: 20px;
            border-bottom: solid 2px black;
            border-right: solid 2px black;
            border-left: solid 1px #E0E1E4;
            width: 195px;
            position: absolute;
            font-family: Optima;
            font-size: 10pt;
            background-color: White;
            margin-left: 400px;
            cursor: pointer;
        }
    </style>
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
            // localStorage.removeItem("A9");
            document.cookie = "U9=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
        }

        window.setInterval(function () {

            //               if (localStorage.getItem("A9") != null) {
            //                   localStorage.removeItem("A9");
            //                   location.reload();

            //               }

            if (document.cookie.length != 0) {

                var ca = document.cookie.split(";");
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i].trim();

                    if (c.indexOf("U9") == 0) {
                        document.cookie = "U9=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                        location.reload();

                    }
                }
            }

        }, 1500);
    </script>
    <script type="text/javascript">
     function clickButton(e) {
            var evt = e ? e : window.event;
            
           
                if (evt.keyCode == 13) {
                   
                    return false;
                }

            }


   function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, countVal) {
        
       if(checkNumericAll())
          {
            var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>";    
           //alert(SessionID+'-----'+CompID);   
              
              
              countVal = eval(countVal);
                           
              var m = 0;
              for (m = 0; m < countVal; m++) {
                  var flag;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
                    index = m + 1;
                    
                    //index = result;
                   var arrWWP = [];
                   var arrMRH = [];
                   var arrIR1 = [];
                   var arrDT = [];
                   var arrWS = [];
                   var arrDWS = [];
                   var i;

                   for (i = 0; i < 30; i++) {
                   arrWWP[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWWP_" + (i + 1) + "_" + eval(index)).value;
                   arrMRH[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtMRH_" + (i + 1) + "_" + eval(index)).value;
                   arrIR1[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtIR1_" + (i + 1) + "_" + eval(index)).value;
                   arrDT[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDT_" + (i + 1) + "_" + eval(index)).value;
                   arrWS[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWS_" + (i + 1) + "_" + eval(index)).value;
                   arrDWS[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDWS_" + (i + 1) + "_" + eval(index)).value;
                   
                
                   }  

                                    
                  if ((m + 1) == countVal) {
                      flag = "Y"
                  }
                  else {

                      flag = "N"
                      
                  }

              
               
                  //TEST END
                  if (m == 0) {
                   if(caseId1>1000)
                      {
                      PageMethods.UpdateAllCases(caseId1, index, flag, arrWWP, arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 1) {
                  if(caseId2>1000)
                      {
                      PageMethods.UpdateAllCases(caseId2, index, flag, arrWWP, arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 2) {
                  if(caseId3>1000)
                      {
                       PageMethods.UpdateAllCases(caseId3, index, flag, arrWWP, arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 3) {
                    if(caseId4>1000)
                      {
                       PageMethods.UpdateAllCases(caseId4, index, flag, arrWWP, arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }

                  else if (m == 4) {
                   if(caseId5>1000)
                      {
                       PageMethods.UpdateAllCases(caseId5, index, flag, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 5) {
                   if(caseId6>1000)
                      {
                       PageMethods.UpdateAllCases(caseId6, index, flag, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 6) {
                    if(caseId7>1000)
                      {
                       PageMethods.UpdateAllCases(caseId7, index, flag, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 7) {
                   if(caseId8>1000)
                      {
                       PageMethods.UpdateAllCases(caseId8, index, flag, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 8) {
                   if(caseId9>1000)
                      {
                       PageMethods.UpdateAllCases(caseId9, index, flag, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 9) {
                   if(caseId10>1000)
                      {
                       PageMethods.UpdateAllCases(caseId10, index, flag, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceedAll, onErrorAll);
                       }
                  }

        // PageMethods.GetStatus(arrCOST[i], caseId, onSucceed, onError);

              }
              // document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';
              return false;
             }
              else
              {
               return false;
              }
          }

       function onSucceedAll(result) {
      
            var index;
            var pageL;
            var flag;
           
             for (i = 0; i < 30; i++) {
              var i;
              var cpArea = [];
              cpArea = result[i].split("#");  
             if(i == "0") {
                      flag = cpArea[2];       
                      index = cpArea[1];          
                }

                    // document.getElementById("ctl00_Econ3ContentPlaceHolder_WEBWIDTHS_" + index + "").innerHTML = cpArea[1]; 
                            
                         
                      var WWP=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWWP_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWWP_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(WWP.replace(',','')).toFixed(2));
                       
                       var MRH=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtMRH_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtMRH_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(MRH.replace(',','')).toFixed(0));

                       var IR1=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtIR1_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtIR1_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(IR1.replace(',','')).toFixed(2));

                       var DT=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDT_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDT_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(DT.replace(',','')).toFixed(1));

                       var WS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWS_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWS_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(WS.replace(',','')).toFixed(1));

                       var DWS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDWS_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDWS_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(DWS.replace(',','')).toFixed(1));
               
                    //  document.getElementById("ctl00_Econ3ContentPlaceHolder_WEBWIDTHS_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0];
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_INSTRATE_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0]; 
               
               }

                if (flag == "Y") {



document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
           // document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_13=U_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_14=U_14;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_15=U_15;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_16=U_16;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

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
                  //Getting Page List End
                  }

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

              if(checkNumericAll())
           {
          
                var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>"; 

            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
              
                   var arrWWP = [];
                   var arrMRH = [];
                   var arrIR1 = [];
                   var arrDT = [];
                   var arrWS = [];
                   var arrDWS = [];
                   var i;

                for (i = 0; i < 30; i++) {
           
                   arrWWP[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWWP_" + (i + 1) + "_" + eval(index)).value;
                   arrMRH[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtMRH_" + (i + 1) + "_" + eval(index)).value;
                   arrIR1[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtIR1_" + (i + 1) + "_" + eval(index)).value;
                   arrDT[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDT_" + (i + 1) + "_" + eval(index)).value;
                   arrWS[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWS_" + (i + 1) + "_" + eval(index)).value;
                   arrDWS[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDWS_" + (i + 1) + "_" + eval(index)).value;
                
            }  
          
             

//             alert('PageMethods.UpdateCase(arr, caseId, index,SessionID,CompID, arrPosDes, arrNoWorker, arrPrefSal, onSucceed, onError);');

             PageMethods.UpdateCase(caseId, index, arrWWP,arrMRH, arrIR1, arrDT, arrWS, arrDWS, onSucceed, onError);
          
               return false;
               }
               else
               {
                 return false;
               }
         }
                

            function onSucceed(result) 
            {
               
                var index;
                var i;
              
            for (i = 0; i < 30; i++) {
                     var cpArea = [];
                      cpArea = result[i].split("#");     
                      if(i == "0") 
                      {
                                  
                          index = cpArea[1];          
                      }
                                                         
                                                                 
                       var WWP=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWWP_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWWP_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(WWP.replace(',','')).toFixed(2));
                       
                       var MRH=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtMRH_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtMRH_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(MRH.replace(',','')).toFixed(0));

                       var IR1=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtIR1_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtIR1_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(IR1.replace(',','')).toFixed(2));

                       var DT=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDT_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDT_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(DT.replace(',','')).toFixed(1));

                       var WS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWS_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtWS_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(WS.replace(',','')).toFixed(1));

                       var DWS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDWS_" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtDWS_" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(DWS.replace(',','')).toFixed(1));       
                     
                      //document.getElementById("ctl00_Econ3ContentPlaceHolder_WEBWIDTHS_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0];
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_INSTRATE_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0]; 
              }

            document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            //document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_13=U_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_14=U_14;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_15=U_15;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_16=U_16;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

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

        function addCommas(nStr)
{
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
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Operating Assumptions')"
                onmouseout="UnTip()">
                Econ3 - Operating Assumptions
            </td>
            <td style="width: 23%" class="PageSHeading">
                <table>
                    <tr>
                        <td>
                            Comparison ID:
                        </td>
                        <td>
                            <asp:Label ID="lblAID" CssClass="LableFonts" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 30%" class="PageSHeading">
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
                <div id="divHeader" class="divHeader" onclick="toggleDiv('divContent', 'img1')">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="HTR">
                            <td class="TdLeft">
                                Case Display
                            </td>
                            <td class="TdRight">
                                <img src="../../../Images/down.png" class="HeaderImg" id="img1" alt="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divContent" class="divContent">
                    <div style="margin-left: 7px; margin-top: 10px; margin-bottom: 5px">
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
                <div id="divHeader2" class="divHeader2" onclick="toggleDiv('divContent2', 'img2')">
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr class="HTR">
                            <td class="TdLeft">
                                Row Display
                            </td>
                            <td class="TdRight">
                                <img src="../../../Images/down.png" class="HeaderImg" id="img2" alt="" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divContent2" class="divContent2">
                    <div style="margin-left: 7px; margin-top: 10px; margin-bottom: 5px">
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ED" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_ED'); " />Equipment
                        Description
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WWS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_WWS'); " />Web
                        Width/Cavities Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WWP" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_WWP'); " />Web
                        Width/Cavities Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MRH" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_MRH'); " />Maximum
                        Annual Run Hours
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_IR1" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_IR1'); " />Instantaneous
                        Rate
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_UT" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_UT'); " />Units
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_IR2" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_IR2'); " />Instantaneous
                        Rate
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DT" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_DT'); " />Downtime
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_WS'); " />Production
                        Waste
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DWS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_DWS'); " />Design
                        Waste
                    </div>
                </div>
            </td>
            <td>
                <div class="divHeader3" style="display: none;">
                    <table>
                        <tr style="height: 20px;">
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
        <div id="PageSection1" style="text-align: left">
            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
            <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
        </div>
    </div>
</asp:Content>
