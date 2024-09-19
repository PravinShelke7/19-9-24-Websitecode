<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="EnergyIn.aspx.vb" Inherits="Pages_Econ3_Assumptions_EnergyIn" title="E3-Energy Assumptions" %>
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
              //localStorage.removeItem("A12");
              document.cookie = "U_12=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
          }

          window.setInterval(function () {

//              if (localStorage.getItem("A12") != null) {
//                  localStorage.removeItem("A12");
//                  location.reload();

              //              }
              if (document.cookie.length != 0) {

                  var ca = document.cookie.split(";");
                  for (var i = 0; i < ca.length; i++) {
                      var c = ca[i].trim();

                      if (c.indexOf("U_12") == 0) {
                          document.cookie = "U_12=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
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
         
              countVal = eval(countVal);
              var m = 0;
              for (m = 0; m < countVal; m++) {
                  var flag;
                  document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';

                  index = m + 1;
              var arrELEC ;
             var arrNATGAS;

                  var i;
                  arrELEC = document.getElementById("ELEC"+ index).value;
                arrNATGAS = document.getElementById("NGAS"+ index).value;
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
                        PageMethods.UpdateAllCases(caseId1, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                      }
                    
                  }
                  else if (m == 1) {
                    if(caseId2>1000)
                      {
                      PageMethods.UpdateAllCases(caseId2, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 2) {
                    if(caseId3>1000)
                      {
                       PageMethods.UpdateAllCases(caseId3, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 3) {
                   if(caseId4>1000)
                      {
                        PageMethods.UpdateAllCases(caseId4, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                        }
                  }

                  else if (m == 4) {
                   if(caseId5>1000)
                      {
                       PageMethods.UpdateAllCases(caseId5, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 5) {
                    if(caseId6>1000)
                      {
                       PageMethods.UpdateAllCases(caseId6, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 6) {
                     if(caseId7>1000)
                      {
                      PageMethods.UpdateAllCases(caseId7, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 7) {
                    if(caseId8>1000)
                      {
                       PageMethods.UpdateAllCases(caseId8, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 8) {
                    if(caseId9>1000)
                      {
                       PageMethods.UpdateAllCases(caseId9, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 9) {
                   if(caseId9>1000)
                      {
                       PageMethods.UpdateAllCases(caseId10, index, flag,arrELEC,arrNATGAS, onSucceedAll, onErrorAll);
                      }
                  }
                 
              }
            
              return false;
              }
              else
              {
                  return false;
              }
          }
          
          function onSucceedAll(result) {

               var index;
              var valArray = [];
              var flag;
             
             
              var i;
           
               for (i = 0; i < 4; i++) 
               {
                     valArray = result[i].split("#");      
                     if(i==0)
                     {
                          index = valArray[10]; 
                           document.getElementById("TELEC" + index).innerHTML = valArray[3]; 
                           document.getElementById("TNATG" + index).innerHTML = valArray[4]; 
                           document.getElementById("TTOT" + index).innerHTML = valArray[5]; 
                         //  alert(valArray[8]+' '+valArray[9])
                             document.getElementById("TOTVAR" + index).innerHTML = valArray[8]; 
                           document.getElementById("TOTFIXED" + index).innerHTML = valArray[9]; 
                           flag=valArray[11];

                            var ELECVal=document.getElementById("ELEC"+ index).value;
                            document.getElementById("ELEC"+ index).value=addCommas(parseFloat(ELECVal.replace(',','')).toFixed(3));

                      var NGASVal=document.getElementById("NGAS"+ index).value;
                      document.getElementById("NGAS"+ index).value=addCommas(parseFloat(NGASVal.replace(',','')).toFixed(3));
                          //alert(flag+'----')
                     }
                       //alert(valArray[0]);                                               
                     


                        if(i==0)
                          {
                           document.getElementById("PRODELEC" + index).innerHTML = valArray[0]; 
                           document.getElementById("PRODNATG" + index).innerHTML = valArray[1]; 
                           document.getElementById("PRODTOT" + index).innerHTML = valArray[2]; 

                          // alert(valArray[6]+' '+valArray[7])
                           document.getElementById("PRODVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("PRODFIXED" + index).innerHTML = valArray[7]; 
                           }
                       
                       
                        else if(i==1)
                        {
                          document.getElementById("WAREELEC" + index).innerHTML = valArray[0]; 
                          document.getElementById("WARENATG" + index).innerHTML = valArray[1]; 
                          document.getElementById("WARETOT" + index).innerHTML = valArray[2];      
                          
                            document.getElementById("WAREVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("WAREFIXED" + index).innerHTML = valArray[7];  
                       }   
                       
                         else if(i==2)
                        {
                          document.getElementById("OFFCELEC" + index).innerHTML = valArray[0]; 
                          document.getElementById("OFFCNATG" + index).innerHTML = valArray[1]; 
                          document.getElementById("OFFCTOT" + index).innerHTML = valArray[2];   
                          
                             document.getElementById("OFFCVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("OFFCFIXED" + index).innerHTML = valArray[7];      
                       }   
                         else if(i==3)
                        {
                          document.getElementById("SUPPELEC" + index).innerHTML = valArray[0]; 
                          document.getElementById("SUPPNATG" + index).innerHTML = valArray[1]; 
                          document.getElementById("SUPPTOT" + index).innerHTML = valArray[2];       

                              document.getElementById("SUPVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("SUPFIXED" + index).innerHTML = valArray[7];      
                       }                                             
              

               }
              // alert(flag);
              if (flag == "Y") 
              {  
//                 localStorage.setItem("A1", "A1");
//                        localStorage.setItem("A2", "A2");
//                         localStorage.setItem("A3", "A3");
//                          localStorage.setItem("A4", "A4");
//                          localStorage.setItem("A5", "A5");
//                          localStorage.setItem("A6", "A6");
//                          localStorage.setItem("A7", "A7");
//                          localStorage.setItem("A8", "A8");
//                          localStorage.setItem("A9", "A9");
//                          localStorage.setItem("A10", "A10");
//                          localStorage.setItem("A11", "A11");
//                          //localStorage.setItem("A12", "A12");
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
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
           // document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
                   
      
             var arrELEC ;
             var arrNATGAS;
                       
             var i;
          
                arrELEC = document.getElementById("ELEC"+ index).value;
                arrNATGAS = document.getElementById("NGAS"+ index).value;
                
                PageMethods.UpdateCase(caseId, index,arrELEC,arrNATGAS, onSucceed, onError);
            
            return false;
            }
            else
            {
                return false;
            }
        }
        function onSucceed(result) {

            var index;
              var valArray = [];
            //   alert('Hi');
           
            var i;
           
            for (i = 0; i < 4; i++) 
            {        
                     valArray = result[i].split("#");      
                     if(i==0)
                     {
                          index = valArray[10]; 
                           document.getElementById("TELEC" + index).innerHTML = valArray[3]; 
                           document.getElementById("TNATG" + index).innerHTML = valArray[4]; 
                           document.getElementById("TTOT" + index).innerHTML = valArray[5]; 
                         //  alert(valArray[8]+' '+valArray[9])
                             document.getElementById("TOTVAR" + index).innerHTML = valArray[8];
                             document.getElementById("TOTFIXED" + index).innerHTML = valArray[9]; 

                               var ELECVal=document.getElementById("ELEC"+ index).value;
                            document.getElementById("ELEC"+ index).value=addCommas(parseFloat(ELECVal.replace(',','')).toFixed(3));

                      var NGASVal=document.getElementById("NGAS"+ index).value;
                      document.getElementById("NGAS"+ index).value=addCommas(parseFloat(NGASVal.replace(',','')).toFixed(3));
                     }
                       //alert(valArray[0]);                                               
                   

                        if(i==0)
                          {
                           document.getElementById("PRODELEC" + index).innerHTML = valArray[0]; 
                           document.getElementById("PRODNATG" + index).innerHTML = valArray[1]; 
                           document.getElementById("PRODTOT" + index).innerHTML = valArray[2]; 

                          // alert(valArray[6]+' '+valArray[7])
                           document.getElementById("PRODVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("PRODFIXED" + index).innerHTML = valArray[7]; 
                           }
                       
                       
                        else if(i==1)
                        {
                          document.getElementById("WAREELEC" + index).innerHTML = valArray[0]; 
                          document.getElementById("WARENATG" + index).innerHTML = valArray[1]; 
                          document.getElementById("WARETOT" + index).innerHTML = valArray[2];      
                          
                            document.getElementById("WAREVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("WAREFIXED" + index).innerHTML = valArray[7];  
                       }   
                       
                         else if(i==2)
                        {
                          document.getElementById("OFFCELEC" + index).innerHTML = valArray[0]; 
                          document.getElementById("OFFCNATG" + index).innerHTML = valArray[1]; 
                          document.getElementById("OFFCTOT" + index).innerHTML = valArray[2];   
                          
                             document.getElementById("OFFCVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("OFFCFIXED" + index).innerHTML = valArray[7];      
                       }   
                         else if(i==3)
                        {
                          document.getElementById("SUPPELEC" + index).innerHTML = valArray[0]; 
                          document.getElementById("SUPPNATG" + index).innerHTML = valArray[1]; 
                          document.getElementById("SUPPTOT" + index).innerHTML = valArray[2];       

                              document.getElementById("SUPVAR" + index).innerHTML = valArray[6]; 
                           document.getElementById("SUPFIXED" + index).innerHTML = valArray[7];      
                       }                                             
              

            }
//             localStorage.setItem("A1", "A1");
//                        localStorage.setItem("A2", "A2");
//                         localStorage.setItem("A3", "A3");
//                          localStorage.setItem("A4", "A4");
//                          localStorage.setItem("A5", "A5");
//                          localStorage.setItem("A6", "A6");
//                          localStorage.setItem("A7", "A7");
//                          localStorage.setItem("A8", "A8");
//                          localStorage.setItem("A9", "A9");
//                          localStorage.setItem("A10", "A10");
//                          localStorage.setItem("A11", "A11");
//                        //  localStorage.setItem("A12", "A12");
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
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            //document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
        function onError(result) {
        }
      
    </script>
      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Energy Assumptions')" onmouseout="UnTip()" >
                  Energy Assumptions
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
                          <b>Energy Price</b>
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_Elec" checked="checked"   onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_Elec'); " />Electricity
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_NATG" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_NATG'); " />Natural Gas
                          <br />
                          <b>Energy Requirements</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PRODA" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PRODA'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WARA" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_WARA'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_OFFA" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_OFFA'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SUPPA" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SUPPA'); " />Support
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_TOTA" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_TOTA'); " />Total
                          <br />
                          <b>Energy Cost</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PRODB" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PRODB'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WARB" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_WARB'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_OFFB" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_OFFB'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SUPPB" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SUPPB'); " />Support
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_TOTB" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_TOTB'); " />Total
                            <br />
                          <b>Energy Cost</b>   
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PRODTC" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PRODTC'); " />Production
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_WARTC" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_WARTC'); " />Warehouse
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_OFFTC" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_OFFTC'); " />Office
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SUPTC" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SUPTC'); " />Support
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_TOTTC" checked="checked"  onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_TOTTC'); " />Total
           
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
                 <a href="AdditionalEnergy.aspx" class="Link" target="_blank" style="font-weight:bold;">Addtional Energy Assumptions</a>
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

