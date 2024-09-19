<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="ProductFormat.aspx.vb" Inherits="Pages_Econ3_Assumptions_ProductFormat" title="E3-Product Format Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
    
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script for="document" event="onkeypress"> if (event.keyCode==13) return false; </script> 
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
               // localStorage.removeItem("A2");
               document.cookie = "U2=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
           }

           window.setInterval(function () {

//               if (localStorage.getItem("A2") != null) {
//                   localStorage.removeItem("A2");
//                   location.reload();

               //               }

               if (document.cookie.length != 0) {

                   var ca = document.cookie.split(";");
                   for (var i = 0; i < ca.length; i++) {
                       var c = ca[i].trim();

                       if (c.indexOf("U2") == 0) {
                           document.cookie = "U2=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                           location.reload();

                       }
                   }
               }

           }, 1500);
</script>
   
    <script type="text/javascript">

           function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, countVal) {
         
             if(checkNumericAll())
           {
            var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>";    
              //alert(SessionID+'-----'+CompID);   
             
              countVal = eval(countVal);
                //alert(countVal);
              var m = 0;
              for (m = 0; m < countVal; m++) {
                  var flag;
                  
                var arrFORMAT_M2;
                var arrFORMAT_M3;
                var arrFORMAT_M4;
                var arrFORMAT_M5;
                var arrFORMAT_M6;
                var arrPRODWTPREF;
                var ProdId;

                  document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
                  index = m + 1;
             
             
             arrFORMAT_M2= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_2").value;
             arrFORMAT_M3= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_3").value;
             arrFORMAT_M4= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_4").value;
             arrFORMAT_M5= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_5").value;
             arrFORMAT_M6= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_6").value;
              arrPRODWTPREF = document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value;
//                ProdId = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidProductid" + eval(index)).value;
                PF = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidPFdesid1_" + eval(index)).value;
                ProdId=PF;
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
                       PageMethods.UpdateAllCases(caseId1, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 1) {
                    if(caseId2>1000)
                      {
                       PageMethods.UpdateAllCases(caseId2, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 2) {
                     if(caseId3>1000)
                      {
                       PageMethods.UpdateAllCases(caseId3, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 3) {
                  if(caseId4>1000)
                      {
                       PageMethods.UpdateAllCases(caseId4, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                  }
                  }

                  else if (m == 4) {
                   if(caseId5>1000)
                      {
                       PageMethods.UpdateAllCases(caseId5, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                  }
                  }
                  else if (m == 5) {
                   if(caseId6>1000)
                      {
                       PageMethods.UpdateAllCases(caseId6, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                     }
                  }
                  else if (m == 6) {
                         if(caseId7>1000)
                      {
                       PageMethods.UpdateAllCases(caseId7, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                     }
                  }
                  else if (m == 7) {
                     if(caseId8>1000)
                      {
                       PageMethods.UpdateAllCases(caseId8, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                 }
                  }
                  else if (m == 8) {
                    if(caseId9>1000)
                      {
                       PageMethods.UpdateAllCases(caseId9, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 9) {
                          if(caseId10>1000)
                      {
                       PageMethods.UpdateAllCases(caseId10, index,ProdId, flag, SessionID, CompID, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceedAll, onErrorAll);
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
  
           function onSucceedAll(result) {0
              var index;
              var flag;
               var cpArea = [];
                      cpArea = result.split("#");
                      //alert(cpArea[0]+'#'+cpArea[1]);
                      flag = cpArea[1];
                      index = cpArea[0];
                        
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_2").innerText=cpArea[4]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_3").innerText=cpArea[5]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_4").innerText=cpArea[6]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_5").innerText=cpArea[7]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_6").innerText=cpArea[8]+"\n";

                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_2").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_3").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_4").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_5").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_6").style.fontWeight = "900";


                     var FORMAT_M2N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_2").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_2").value=addCommas(parseFloat(FORMAT_M2N.replace(',','')).toFixed(3));
                      //alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M"));
                      //alert(FORMAT_M2N);

                      var FORMAT_M3N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_3").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_3").value=addCommas(parseFloat(FORMAT_M3N.replace(',','')).toFixed(3));

                      var FORMAT_M4N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_4").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_4").value=addCommas(parseFloat(FORMAT_M4N.replace(',','')).toFixed(3));

                      var FORMAT_M5N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_5").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_5").value=addCommas(parseFloat(FORMAT_M5N.replace(',','')).toFixed(3));

                      var FORMAT_M6N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_6").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_6").value=addCommas(parseFloat(FORMAT_M6N.replace(',','')).toFixed(3));

                     // alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTSUG" + eval(index)).innerHTML);
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTSUG" + eval(index)).innerHTML=addCommas(parseFloat(cpArea[2].replace(',','')).toFixed(4));

                      var PRODWTPREFN=document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value;
                       var PRODWTPREFN1 = parseFloat(PRODWTPREFN.replace(/,/g, ''));  
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value=Number(parseFloat(PRODWTPREFN1).toFixed(4)).toLocaleString('en', {
                        minimumFractionDigits: 4
                    })
                
                      //alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML);
                      if(document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML!="")
                      {           
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML=addCommas(parseFloat(cpArea[3].replace(',','')).toFixed(3));
                      }
                      else
                      {
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML="";
                      }
            
            
              if (flag == "Y") {
          
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
           // document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
                  //Getting Page List End
                  }

           }

        function onErrorAll(result) {
        }


    </script>

    <script type="text/JavaScript">
         function clickButton(e) {
             var evt = e ? e : window.event;


             if (evt.keyCode == 13) {

                 return false;
             }

         }

          function Update(caseId, index) 
          {
           if(checkNumericAll())
           {
             var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>"; 
             
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
            

                 var arrFORMAT_M2;
                 var arrFORMAT_M3;
                 var arrFORMAT_M4;
                 var arrFORMAT_M5; 
                 var arrFORMAT_M6;
                 var arrPRODWTPREF;    
                 var ProdId; 
                  
              arrFORMAT_M2= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_2").value;
              
              arrFORMAT_M3= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_3").value;
              
              arrFORMAT_M4= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_4").value;
          
              arrFORMAT_M5= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_5").value;
             
              arrFORMAT_M6= document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_6").value;
              
              arrPRODWTPREF = document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value;
              
            
         //       ProdId = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidProductid" + eval(index)).value;
         //     alert(ProdId);
                PF = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidPFdesid1_" + eval(index)).value;
         ProdId=PF;
//             alert(caseId+'----'+index+'----'+arrFORMAT_M2+'-----'+arrFORMAT_M3+'----'+arrFORMAT_M4+'----'+arrFORMAT_M5+'-----'+arrFORMAT_M6+'-----'+arrPRODWTPREF); 

             PageMethods.UpdateCase(caseId, index,ProdId, arrFORMAT_M2, arrFORMAT_M3, arrFORMAT_M4, arrFORMAT_M5, arrFORMAT_M6, arrPRODWTPREF,PF, onSucceed, onError);
            return false;
            }
            else
            {
              return false;
            }
          
        }

        function onSucceed(result) {
         var valArray = [];
          valArray = result.split("#");     
             var index=valArray[0];
        
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_2").innerText=valArray[3]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_3").innerText=valArray[4]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_4").innerText=valArray[5]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_5").innerText=valArray[6]+"\n";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_6").innerText=valArray[7]+"\n";

                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_2").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_3").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_4").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_5").style.fontWeight = "900";
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_lblFORMAT_M" + eval(index)+"_6").style.fontWeight = "900";

                          
                      var FORMAT_M2N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_2").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_2").value=addCommas(parseFloat(FORMAT_M2N.replace(',','')).toFixed(3));
                      //alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M"));
                      //alert(FORMAT_M2N);

                      var FORMAT_M3N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_3").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_3").value=addCommas(parseFloat(FORMAT_M3N.replace(',','')).toFixed(3));

                      var FORMAT_M4N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_4").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_4").value=addCommas(parseFloat(FORMAT_M4N.replace(',','')).toFixed(3));

                      var FORMAT_M5N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_5").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_5").value=addCommas(parseFloat(FORMAT_M5N.replace(',','')).toFixed(3));

                      var FORMAT_M6N=document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_6").value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_FORMAT_M" + eval(index)+"_6").value=addCommas(parseFloat(FORMAT_M6N.replace(',','')).toFixed(3));

                     // alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTSUG" + eval(index)).innerHTML);
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTSUG" + eval(index)).innerHTML=addCommas(parseFloat(valArray[1].replace(',','')).toFixed(4));

//                      var PRODWTPREFN=document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value;
//                      document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value=addCommas(parseFloat(PRODWTPREFN.replace(',','')).toFixed(4));
                    var PRODWTPREFN=document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value;
                       var PRODWTPREFN1 = parseFloat(PRODWTPREFN.replace(/,/g, ''));  
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_PRODWTPREF" + eval(index)).value=Number(parseFloat(PRODWTPREFN1).toFixed(4)).toLocaleString('en', {
                        minimumFractionDigits: 4
                    })
              
                  if(document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML!="")
                      {           
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML=addCommas(parseFloat(valArray[2].replace(',','')).toFixed(3));
                      }
                      else
                      {
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_ROLLD" + eval(index)).innerHTML="";
                      }


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
           // document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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

    </script>
    
    <script type="text/javascript">
        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);

        }
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Product Format Assumptions')" onmouseout="UnTip()" >
                  Econ3 - Product Format Assumptions
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
                             
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PF" checked="checked"   onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PF'); " />Product Format
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_I2" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_I2'); " />Input 1
                             <br />                           
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_I3" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_I3'); " />Input 2
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_I4" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_I4'); " />Input 3
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_I5" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_I5'); " />Input 4
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_I6" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_I6'); " />Input 5
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PW" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PW'); " />Packaging Weight Suggested
                             
                              <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PWS" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PWS'); " />Packaging Weight Preferred
                             <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_RD" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_RD'); " />Roll Diameter
                      
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