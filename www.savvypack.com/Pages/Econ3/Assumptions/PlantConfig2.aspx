<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false"
    CodeFile="PlantConfig2.aspx.vb" Inherits="Pages_Econ3_Assumptions_PlantConfig2"
    Title="E3-Plant Space Assumptions" %>

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
         
              countVal = eval(countVal);
              var m = 0;
            for (m = 0; m < countVal; m++) {
                    var flag;
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';

                     index = m + 1;
                        var arrLPH ;
                        var arrLPPH;
                         var arrLPS;
                        var arrLWH ;
                        var arrLOF;
                        var arrLOS;
                         var arrSWH ;
                        var arrSOF;
                         var arrSOS;

                            var i;

                   // SPACE TYPE
                  arrSWH = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value;
                  arrSOF = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value;
                  arrSOS = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value;

                  // LEASE TYPE
                  arrLPH = document.getElementById("LPH"+ index).value;
                  arrLPPH = document.getElementById("LPPH"+ index).value;
                  arrLPS = document.getElementById("LPS"+ index).value;
                  arrLWH = document.getElementById("LWH"+ index).value;
                  arrLOF =document.getElementById("LOF"+ index).value;
                  arrLOS =document.getElementById("LOS"+ index).value;

                 

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
                        PageMethods.UpdateAllCases(caseId1, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                      }
                    
                  }
                  else if (m == 1) {
                    if(caseId2>1000)
                      {
                      PageMethods.UpdateAllCases(caseId2, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 2) {
                    if(caseId3>1000)
                      {
                       PageMethods.UpdateAllCases(caseId3, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 3) {
                   if(caseId4>1000)
                      {
                        PageMethods.UpdateAllCases(caseId4, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                        }
                  }

                  else if (m == 4) {
                   if(caseId5>1000)
                      {
                       PageMethods.UpdateAllCases(caseId5, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 5) {
                    if(caseId6>1000)
                      {
                       PageMethods.UpdateAllCases(caseId6, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 6) {
                     if(caseId7>1000)
                      {
                      PageMethods.UpdateAllCases(caseId7, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 7) {
                    if(caseId8>1000)
                      {
                       PageMethods.UpdateAllCases(caseId8, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                       }
                  }
                  else if (m == 8) {
                    if(caseId9>1000)
                      {
                       PageMethods.UpdateAllCases(caseId9, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 9) {
                   if(caseId9>1000)
                      {
                       PageMethods.UpdateAllCases(caseId10, index, flag,arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceedAll, onErrorAll);
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
          
         function onSucceedAll(result) 
         {
      
            var index;
            var pageL;
            var flag;
            nfObject = new Intl.NumberFormat('en-US'); 
            var i;
            var cpArea = [];
            cpArea = result.split("#");
                       
                 index = cpArea[9];                       
               var txtSWH=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value;
               var txtSWHN  = parseFloat(txtSWH.replace(/,/g, ''));
               var txtSWHN1 = Number(parseFloat(txtSWHN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value=Number(parseFloat(txtSWHN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(txtSWH.replace(',','')).toFixed(3));

               var txtSOF=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value;
               var txtSOFN  = parseFloat(txtSOF.replace(/,/g, ''));
               var txtSOFN1 = Number(parseFloat(txtSOFN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value=Number(parseFloat(txtSOFN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(txtSOF.replace(',','')).toFixed(3));

               var txtSOS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value;
              var txtSOSN  = parseFloat(txtSOS.replace(/,/g, ''));
               var txtSOSN1 = Number(parseFloat(txtSOSN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value=Number(parseFloat(txtSOSN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(txtSOS.replace(',','')).toFixed(3));

             //  document.getElementById("ctl00_Econ3ContentPlaceHolder_WEIGHT" + index + "").innerHTML = cpArea[0];
                   
                   var LPHVal=document.getElementById("LPH"+ index).value;
                   var LPHValN  = parseFloat(LPHVal.replace(/,/g, ''));
               var LPHValN1 = Number(parseFloat(LPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                    document.getElementById("LPH"+ index).value=Number(parseFloat(LPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LPHVal.replace(',','')).toFixed(3));

                     var LPPHVal=document.getElementById("LPPH"+ index).value;
                     var LPPHValN  = parseFloat(LPPHVal.replace(/,/g, ''));
               var LPPHValN1 = Number(parseFloat(LPPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                    document.getElementById("LPPH"+ index).value=Number(parseFloat(LPPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LPPHVal.replace(',','')).toFixed(3));

                     var LPSVal=document.getElementById("LPS"+ index).value;
                     var LPSValN  = parseFloat(LPSVal.replace(/,/g, ''));
               var LPSValN1 = Number(parseFloat(LPSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                    document.getElementById("LPS"+ index).value=Number(parseFloat(LPSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LPSVal.replace(',','')).toFixed(3));
                    
               
               var LWHVal=document.getElementById("LWH"+ index).value;
               var LWHValN  = parseFloat(LWHVal.replace(/,/g, ''));
               var LWHValN1 = Number(parseFloat(LWHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("LWH"+ index).value=Number(parseFloat(LWHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LWHVal.replace(',','')).toFixed(3));

                var LOFVal=document.getElementById("LOF"+ index).value;
                var LOFValN  = parseFloat(LOFVal.replace(/,/g, ''));
               var LOFValN1 = Number(parseFloat(LOFValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                document.getElementById("LOF"+ index).value=Number(parseFloat(LOFValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LOFVal.replace(',','')).toFixed(3));
           
               var LOSVal=document.getElementById("LOS"+ index).value;
                var LOSValN  = parseFloat(LOSVal.replace(/,/g, ''));
               var LOSValN1 = Number(parseFloat(LOSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("LOS"+ index).value=Number(parseFloat(LOSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LOSVal.replace(',','')).toFixed(3));

               document.getElementById("PRODTOTAL" + index).innerHTML = cpArea[0]; 
             document.getElementById("TOTALLT" + index).innerHTML = cpArea[1]; 
             document.getElementById("ctl00_Econ3ContentPlaceHolder_TotalST_" + index + "").innerHTML = cpArea[2];
             document.getElementById("PRODSTAND" + index).innerHTML = cpArea[3]; 
             document.getElementById("WAREHOUSE" + index).innerHTML = cpArea[4]; 
             document.getElementById("OFFICE" + index).innerHTML = cpArea[5]; 
             document.getElementById("SUPPORT" + index).innerHTML = cpArea[6]; 
              document.getElementById("PRODHIGHBAY" + index).innerHTML = cpArea[7]; 
             document.getElementById("PRODPARHIGHBAY" + index).innerHTML = cpArea[8]; 
            
             flag=  cpArea[10];
                          
                              
            
              if (flag == "Y") 
              {  

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
           // document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
         }

          function onErrorAll(result) {
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
    </script>
    <script>
    function Update(caseId, index) {
            
               if(checkNumericAll())
           {
            
              var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>"; 
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
                       var arrLPH ;
                        var arrLPPH;
                         var arrLPS;
                        var arrLWH ;
                        var arrLOF;
                        var arrLOS;
                         var arrSWH ;
                        var arrSOF;
                         var arrSOS;
                        var i;
          

                 // SPACE TYPE
                  arrSWH = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value;
                  arrSOF = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value;
                  arrSOS = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value;

                  // LEASE TYPE
                  arrLPH = document.getElementById("LPH"+ index).value;
                  arrLPPH = document.getElementById("LPPH"+ index).value;
                  arrLPS = document.getElementById("LPS"+ index).value;
                  arrLWH = document.getElementById("LWH"+ index).value;
                  arrLOF =document.getElementById("LOF"+ index).value;
                  arrLOS =document.getElementById("LOS"+ index).value;

           
          

            //PageMethods.GetStatus(arr, caseId, index,SessionID,CompID,arrMat,arrThick, onSucceed, onError);
             PageMethods.UpdateCase(caseId, index, arrLPH,arrLPPH,arrLPS,arrLWH,arrLOF,arrLOS,arrSWH,arrSOF,arrSOS, onSucceed, onError);
            return false;
            }
            else
            {
            return false;
            }
        }
        function onSucceed(result) {
        
            var index;
            var pageL;
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';
            var i;
             nfObject = new Intl.NumberFormat('en-US'); 
           var cpArea = [];
            cpArea = result.split("#");
          
            index = cpArea[9];
            document.getElementById("PRODTOTAL" + index).innerHTML = cpArea[0]; 
             document.getElementById("TOTALLT" + index).innerHTML = cpArea[1]; 
             document.getElementById("ctl00_Econ3ContentPlaceHolder_TotalST_" + index + "").innerHTML = cpArea[2];
             document.getElementById("PRODSTAND" + index).innerHTML = cpArea[3]; 

             document.getElementById("WAREHOUSE" + index).innerHTML = cpArea[4]; 
             document.getElementById("OFFICE" + index).innerHTML = cpArea[5]; 
             document.getElementById("SUPPORT" + index).innerHTML = cpArea[6]; 
             document.getElementById("PRODHIGHBAY" + index).innerHTML = cpArea[7]; 
             document.getElementById("PRODPARHIGHBAY" + index).innerHTML = cpArea[8]; 


//            var txtSWH=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value;
//               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value=addCommas(parseFloat(txtSWH.replace(',','')).toFixed(3));

//               var txtSOF=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value;
//               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value=addCommas(parseFloat(txtSOF.replace(',','')).toFixed(3));

//               var txtSOS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value;
//               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value=addCommas(parseFloat(txtSOS.replace(',','')).toFixed(3));

//                              
//                   var LPHVal=document.getElementById("LPH"+ index).value;
//                    document.getElementById("LPH"+ index).value=addCommas(parseFloat(LPHVal.replace(',','')).toFixed(3));

//                     var LPPHVal=document.getElementById("LPPH"+ index).value;
//                    document.getElementById("LPPH"+ index).value=addCommas(parseFloat(LPPHVal.replace(',','')).toFixed(3));

//                     var LPSVal=document.getElementById("LPS"+ index).value;
//                    document.getElementById("LPS"+ index).value=addCommas(parseFloat(LPSVal.replace(',','')).toFixed(3));
//                    
//               
//               var LWHVal=document.getElementById("LWH"+ index).value;
//               document.getElementById("LWH"+ index).value=addCommas(parseFloat(LWHVal.replace(',','')).toFixed(3));

//                var LOFVal=document.getElementById("LOF"+ index).value;
//                document.getElementById("LOF"+ index).value=addCommas(parseFloat(LOFVal.replace(',','')).toFixed(3));
//           
//               var LOSVal=document.getElementById("LOS"+ index).value;
//               document.getElementById("LOS"+ index).value=addCommas(parseFloat(LOSVal.replace(',','')).toFixed(3));
                var txtSWH=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value;
               var txtSWHN  = parseFloat(txtSWH.replace(/,/g, ''));
               var txtSWHN1 = Number(parseFloat(txtSWHN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSWH_" + eval(index)).value=Number(parseFloat(txtSWHN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(txtSWH.replace(',','')).toFixed(3));

               var txtSOF=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value;
               var txtSOFN  = parseFloat(txtSOF.replace(/,/g, ''));
               var txtSOFN1 = Number(parseFloat(txtSOFN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOF_" + eval(index)).value=Number(parseFloat(txtSOFN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(txtSOF.replace(',','')).toFixed(3));

               var txtSOS=document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value;
              var txtSOSN  = parseFloat(txtSOS.replace(/,/g, ''));
               var txtSOSN1 = Number(parseFloat(txtSOSN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("ctl00_Econ3ContentPlaceHolder_txtSOS_" + eval(index)).value=Number(parseFloat(txtSOSN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(txtSOS.replace(',','')).toFixed(3));

             //  document.getElementById("ctl00_Econ3ContentPlaceHolder_WEIGHT" + index + "").innerHTML = cpArea[0];
                   
                   var LPHVal=document.getElementById("LPH"+ index).value;
                   var LPHValN  = parseFloat(LPHVal.replace(/,/g, ''));
               var LPHValN1 = Number(parseFloat(LPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                    document.getElementById("LPH"+ index).value=Number(parseFloat(LPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LPHVal.replace(',','')).toFixed(3));

                     var LPPHVal=document.getElementById("LPPH"+ index).value;
                     var LPPHValN  = parseFloat(LPPHVal.replace(/,/g, ''));
               var LPPHValN1 = Number(parseFloat(LPPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                    document.getElementById("LPPH"+ index).value=Number(parseFloat(LPPHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LPPHVal.replace(',','')).toFixed(3));

                     var LPSVal=document.getElementById("LPS"+ index).value;
                     var LPSValN  = parseFloat(LPSVal.replace(/,/g, ''));
               var LPSValN1 = Number(parseFloat(LPSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                    document.getElementById("LPS"+ index).value=Number(parseFloat(LPSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LPSVal.replace(',','')).toFixed(3));
                    
               
               var LWHVal=document.getElementById("LWH"+ index).value;
               var LWHValN  = parseFloat(LWHVal.replace(/,/g, ''));
               var LWHValN1 = Number(parseFloat(LWHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("LWH"+ index).value=Number(parseFloat(LWHValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LWHVal.replace(',','')).toFixed(3));

                var LOFVal=document.getElementById("LOF"+ index).value;
                var LOFValN  = parseFloat(LOFVal.replace(/,/g, ''));
               var LOFValN1 = Number(parseFloat(LOFValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                document.getElementById("LOF"+ index).value=Number(parseFloat(LOFValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LOFVal.replace(',','')).toFixed(3));
           
               var LOSVal=document.getElementById("LOS"+ index).value;
                var LOSValN  = parseFloat(LOSVal.replace(/,/g, ''));
               var LOSValN1 = Number(parseFloat(LOSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
               document.getElementById("LOS"+ index).value=Number(parseFloat(LOSValN).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })//addCommas(parseFloat(LOSVal.replace(',','')).toFixed(3));
              
//                  localStorage.setItem("A1", "A1");
//                        localStorage.setItem("A2", "A2");
//                       //  localStorage.setItem("A3", "A3");
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
//                          localStorage.setItem("A16", "A16");

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
           // document.cookie = "U_11=U_11;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
    <script>
        function removeSession() {
            //localStorage.removeItem("A11");
            document.cookie = "U_11=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
        }

        window.setInterval(function () {

            //                if (localStorage.getItem("A11") != null) {
            //                    localStorage.removeItem("A11");
            //                    location.reload();

            //                }
            if (document.cookie.length != 0) {

                var ca = document.cookie.split(";");
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i].trim();

                    if (c.indexOf("U_11") == 0) {
                        document.cookie = "U_11=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                        location.reload();

                    }
                }
            }

        }, 1500);
    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table width="840px">
        <tr align="left">
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Plant Space Assumptions')"
                onmouseout="UnTip()">
                Econ3 - Plant Space Requirements
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
                        <b>Space Type</b>
                        <div style="margin-left: 2px;">
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SPD" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SPD'); " />Production
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SWH" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SWH'); " />Warehouse
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SOF" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SOF'); " />Office
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SOS" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_SOS'); " />Support
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ST0T" checked="checked"
                                onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_ST0T'); " />Total
                        </div>
                        <b>Lease Type</b>
                        <div style="margin-left: 2px;">
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LPH" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LPH'); " />Prod.
                            High Bay
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LPPH" checked="checked"
                                onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LPPH'); " />Prod. Partial
                            High Bay
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LPS" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LPS'); " />Prod.
                            Standard Bay
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LPT" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LPT'); " />Prod.
                            Total
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LWH" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LWH'); " />Warehouse
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LOF" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LOF'); " />Office
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LOS" checked="checked" onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LOS'); " />Support
                            <br />
                            <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_LT0T" checked="checked"
                                onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_LT0T'); " />Total
                        </div>
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
