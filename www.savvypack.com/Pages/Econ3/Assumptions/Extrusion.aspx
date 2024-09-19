<%@ Page Title="E3-Material Assumption" Language="VB" MasterPageFile="~/Masters/Econ3.master"
    AutoEventWireup="false" CodeFile="Extrusion.aspx.vb" Inherits="Pages_Econ3_Assumptions_Extrusion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E3Comman.js"></script>
    <script for="document" event="onkeypress"> if (event.keyCode==13) return false; </script>
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
            //localStorage.removeItem("A1");
document.cookie = "U1=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
        }

        window.setInterval(function () {

           //            if (localStorage.getItem("A1") != null) {
//                localStorage.removeItem("A1");
//                location.reload();

            //            }
           // alert(document.cookie.length);


            if (document.cookie.length != 0) {

                var ca = document.cookie.split(";");
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i].trim();

                    if (c.indexOf("U1") == 0) {
                        document.cookie = "U1=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
			location.reload();
                       
                    }
                }
            }

        }, 1500);
    </script>
    <script type="text/javascript">
        function ShowMatPopWindow(el, Page, Id) {

            // find all elements that have the linkActive class
            var elems = document.querySelectorAll(".linkActive");

            // loop through them and ...
            for (var i = 0; i < elems.length; i++) {
                // remove the linkActive class
                elems[i].classList.remove('linkActive');
                elems[i].style.color = 'Black';
            }

            // now add the class to the link that was just clicked
            el.classList.add('linkActive');
            el.style.color = 'red';

            var width = 550;
            var height = 550;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            var Mid = document.getElementById(Id).value;
            Page = Page + "&MatId=" + Mid
            newwin = window.open(Page, 'Chat', params);
			return false;
        }

        function clickButton(e) {
            var evt = e ? e : window.event;


            if (evt.keyCode == 13) {

                return false;
            }

        }

        function Update(caseId, index) {
            if (CheckForMaterialPageE3('ctl00_Econ3ContentPlaceHolder', 'hidmatid', 'Thick', 'hidDepId', 'hydep', index)) {
                document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
                var arr = [];
                var arrMat = [];
                var arrThick = [];
                var arrCOST = [10];
                var arrRECY = [];
                var arrEXPR = [];
                var arrSGP = [];
                var arrDept = [];
                var i;
                for (i = 0; i < 10; i++) {
                    arr[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_" + (i + 1) + "_" + index + "").value;
                    arrMat[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidmatid" + (i + 1) + "_" + eval(index)).value;
                    arrThick[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value;

                    arrRECY[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value;
                    arrEXPR[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value;
                    arrSGP[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                    arrDept[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidDepId" + (i + 1) + "_" + eval(index)).value;
                }
                PageMethods.UpdateCase(arr, caseId, index, arrMat, arrThick, arrRECY, arrEXPR, arrSGP, arrDept, onSucceed, onError);
                return false;
            }
            else {
                return false;
            }
        }
        function onSucceed(result) {

            var index;

            var i;
            nfObject = new Intl.NumberFormat('en-US'); 
            for (i = 0; i < 10; i++) {
                if (i == "0") {
                    var cpArea = [];
                    cpArea = result[i].split("#");

                    var PriceP = [];
                    PriceP = cpArea[1].split("-");

                    index = PriceP[0];

                    document.getElementById("ctl00_Econ3ContentPlaceHolder_CPAREA_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_" + (i + 1) + "_" + index + "").value = PriceP[1];

                    document.getElementById("ctl00_Econ3ContentPlaceHolder_WPAREA_" + (i + 1) + "_" + index + "").innerHTML = PriceP[2];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_PRS_" + (i + 1) + "_" + index + "").innerHTML = PriceP[3];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGS_" + (i + 1) + "_" + index + "").innerHTML = PriceP[4];


                    var thickN = document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(thickN.replace(',', '')).toFixed(3));

                    var RECYN = document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(RECYN.replace(',', '')).toFixed(2));

                    var EXPRN = document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(EXPRN.replace(',', '')).toFixed(2));

//                    var SGPN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
//                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(SGPN.replace(',', '')).toFixed(3));
                    
                    var SGPN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                    var SGPN1 = parseFloat(SGPN.replace(/,/g, ''));                    
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value = Number(parseFloat(SGPN1).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                }
                else {
                    var cpArea = [];
                    cpArea = result[i].split("-");
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_CPAREA_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_" + (i + 1) + "_" + index + "").value = cpArea[1];

                    document.getElementById("ctl00_Econ3ContentPlaceHolder_WPAREA_" + (i + 1) + "_" + index + "").innerHTML = cpArea[2];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_PRS_" + (i + 1) + "_" + index + "").innerHTML = cpArea[3];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGS_" + (i + 1) + "_" + index + "").innerHTML = cpArea[4];


                    var thickN = document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(thickN.replace(',', '')).toFixed(3));

                    var RECYN = document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(RECYN.replace(',', '')).toFixed(2));

                    var EXPRN = document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(EXPRN.replace(',', '')).toFixed(2));

//                    var SGPN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                    //                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(SGPN.replace(',', '')).toFixed(3));
                    var SGPN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                    var SGPN1 = parseFloat(SGPN.replace(/,/g, ''));
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value = Number(parseFloat(SGPN1).toFixed(3)).toLocaleString('en', {
                        minimumFractionDigits: 3
                    })
                }


            }
//            // localStorage.setItem("A1", "A1");
//            localStorage.setItem("A2", "A2");
//            localStorage.setItem("A3", "A3");
//            localStorage.setItem("A4", "A4");
//            localStorage.setItem("A5", "A5");
//            localStorage.setItem("A6", "A6");
//            localStorage.setItem("A7", "A7");
//            localStorage.setItem("A8", "A8");
//            localStorage.setItem("A9", "A9");
//            localStorage.setItem("A10", "A10");
//            localStorage.setItem("A11", "A11");
//            localStorage.setItem("A12", "A12");
//            localStorage.setItem("A13", "A13");
//            localStorage.setItem("A14", "A14");
//            localStorage.setItem("A15", "A15");
//            localStorage.setItem("A16", "A16");

//            localStorage.setItem("I1", "I1");
//            localStorage.setItem("I2", "I2");
//            localStorage.setItem("I3", "I3");

//            localStorage.setItem("R1", "R1");
//            localStorage.setItem("R2", "R2");
//            localStorage.setItem("R3", "R3");
//            localStorage.setItem("R4", "R4");
//            localStorage.setItem("R5", "R5");
//            localStorage.setItem("R6", "R6");
//            localStorage.setItem("R7", "R7");
//            localStorage.setItem("R8", "R8");
//            localStorage.setItem("R9", "R9");
//            localStorage.setItem("R10", "R10");
//            localStorage.setItem("R11", "R11");
//            localStorage.setItem("R12", "R12");
//            localStorage.setItem("R13", "R13");

            //document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
        function onError(result) {
        }
        function onSucceed1(result) {
            document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_1_2").value = "211";
        }
        function onError(result) {
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
    </script>
    <script type="text/javascript">
           function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, countVal) {
           if(CheckForMaterialPageE3All('ctl00_Econ3ContentPlaceHolder','hidmatid','Thick','hidDepId', 'hydep',countVal))
           {
            var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>";    
           // alert(SessionID+'-----'+CompID);   
              //alert(countVal);
              countVal = eval(countVal);
              var m = 0;
              for (m = 0; m < countVal; m++) {
                  var flag;
                  document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';

                  index = m + 1;
                  var arr = [];
                   var arrCOST = [10];

                      var arrMat = [];
                      var arrThick = [];
                      var arrRECY = [];
                      var arrEXPR = [];
                      var arrSGP = [];
                      var arrDept = [];
                
                  var i;
            for (i = 0; i < 10; i++) 
            {
                 arr[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_" + (i + 1) + "_" + index + "").value;
                 arrMat[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidmatid" + (i + 1) + "_" + eval(index)).value;
                arrThick[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value;

                arrRECY[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value;
                arrEXPR[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value;
                arrSGP[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                arrDept[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidDepId" + (i + 1) + "_" + eval(index)).value;
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
                      PageMethods.UpdateAllCases(arr, caseId1, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 1) {
                    if(caseId2>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId2, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 2) {
                    if(caseId3>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId3, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 3) {
                    if(caseId4>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId4, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }

                  else if (m == 4) {
                   if(caseId5>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId5, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 5) {
                   if(caseId6>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId6, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 6) {
                   if(caseId7>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId7, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 7) {
                        if(caseId8>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId8, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 8) {
                    if(caseId9>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId9, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
                      }
                  }
                  else if (m == 9) {
                   if(caseId10>1000)
                      {
                      PageMethods.UpdateAllCases(arr, caseId10, index, flag,arrMat,arrThick,arrRECY,arrEXPR,arrSGP,arrDept, onSucceedAll, onErrorAll);
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
              var flag;
             
           
              var i;
              nfObject = new Intl.NumberFormat('en-US'); 
              for (i = 0; i < 10; i++) {
                  if (i == "0") {
                      var cpArea = [];
                      cpArea = result[i].split("#");
                  
                       index = cpArea[1];
                    
                      var PriceP = [];
                      PriceP = cpArea[2].split("-");
                      flag = PriceP[0];

                     
                      //alert(PriceP[0] + "----" + PriceP[1]);
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_CPAREA_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0];
                    
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_" + (i + 1) + "_" + index + "").value = PriceP[1];
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_WPAREA_" + (i + 1) + "_" + index + "").innerHTML = PriceP[2];
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_PRS_" + (i + 1) + "_" + index + "").innerHTML = PriceP[3];
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_SGS_" + (i + 1) + "_" + index + "").innerHTML = PriceP[4];

                      var thickN=document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(thickN.replace(',','')).toFixed(3));

                       var RECYN=document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(RECYN.replace(',','')).toFixed(2));

                      var EXPRN=document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(EXPRN.replace(',','')).toFixed(2));

                      var SGPN=document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                      var SGPN1  = parseFloat(SGPN.replace(/,/g, ''));                    
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value =  Number(parseFloat(SGPN1).toFixed(3)).toLocaleString('en', {
                    minimumFractionDigits: 3
                })//addCommas(parseFloat(SGPN.replace(',','')).toFixed(3));
                      //index = cpArea[1]
                      // alert(index);
                  }
                  else {
                      var cpArea = [];
                      cpArea = result[i].split("-");
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_CPAREA_" + (i + 1) + "_" + index + "").innerHTML = cpArea[0];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_txtPref_" + (i + 1) + "_" + index + "").value = cpArea[1];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_WPAREA_" + (i + 1) + "_" + index + "").innerHTML = cpArea[2];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_PRS_" + (i + 1) + "_" + index + "").innerHTML = cpArea[3];
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_SGS_" + (i + 1) + "_" + index + "").innerHTML = cpArea[4];

                      var thickN=document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_Thick" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(thickN.replace(',','')).toFixed(3));

                       var RECYN=document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_RECY" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(RECYN.replace(',','')).toFixed(2));

                      var EXPRN=document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_EXPR" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(EXPRN.replace(',','')).toFixed(2));

                      var SGPN=document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_SGP" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(SGPN.replace(',','')).toFixed(3));
                   
                  }
                

              }
              if (flag == "Y")
               {
//                // localStorage.setItem("A1", "A1");
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


                        // document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
                 
               //   newwin = window.open('EnergyIn.aspx', 'EI');
              }
          }

          function onErrorAll(result) {
          }
    </script>
    <script type="text/JavaScript">

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
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
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Material Assumption')"
                onmouseout="UnTip()">
                Econ3 - Material Assumptions
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
        <tr align="left">
            <td style="width: 10px;">
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
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_EFFD" checked="checked"
                            onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_EFFD'); " />Effective Date
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_M" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_M'); " />Material
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_T" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_T'); " />Thickness
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PRS" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_PRS'); " />Price
                        Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PRP" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_PRP'); " />Price
                        Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_RE" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_RE'); " />Recycle
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_E" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_E'); " />Extra-process
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SGS" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_SGS'); " />Specific
                        Gravity Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SGP" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_SGP'); " />Specific
                        Gravity Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_W" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_W'); " />Weight/area
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_COST" checked="checked"
                            onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_COST'); " />Cost/area
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_D" checked="checked" onclick="showhideALL('ctl00_Econ3ContentPlaceHolder_D'); " />Mfg.
                        Department
                        <br />
                        <b>Discrete Materials</b>
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DISM" checked="checked"
                            onclick="showhideALLNEW('ctl00_Econ3ContentPlaceHolder_DISM'); " />Material
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DISW" checked="checked"
                            onclick="showhideALLNEW('ctl00_Econ3ContentPlaceHolder_DISW'); " />Weight
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DISP" checked="checked"
                            onclick="showhideALLNEW('ctl00_Econ3ContentPlaceHolder_DISP'); " />Price
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PCIN" checked="checked"
                            onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PCIN'); " />Include weight
                        of discrete materials in P&L statements.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PCMQ" checked="checked"
                            onclick="showhideALL3('ctl00_Econ3ContentPlaceHolder_PCMQ'); " />Printing Plates
                        <br />
                    </div>
                </div>
            </td>
            <td>
                <div class="divHeader5" style="display: none;">
                    <table>
                        <tr style="height: 20px;">
                            <td>
                                Column Width:
                            </td>
                            <td>
                                <asp:TextBox ID="txtDWidth" runat="server" CssClass="SmallTextBox"></asp:TextBox>
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
        <div id="PageSection1" style="text-align: left; margin-left: 0px;">
            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
            <br />
            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
            <br />
        </div>
        <asp:HiddenField ID="hdnCaseID" runat="server" />
    </div>
</asp:Content>
