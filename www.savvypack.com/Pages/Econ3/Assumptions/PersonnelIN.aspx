<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false"
    CodeFile="PersonnelIN.aspx.vb" Inherits="Pages_Econ3_Assumptions_PersonnelIN"
    Title="E3-Personnel Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E3Comman.js"></script>
    <script>
        function removeSession() {
            // localStorage.removeItem("A10");
            document.cookie = "U_10=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
        }

        window.setInterval(function () {

//            if (localStorage.getItem("A10") != null) {
//                localStorage.removeItem("A10");
//                location.reload();

//            }
            if (document.cookie.length != 0) {

                var ca = document.cookie.split(";");
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i].trim();

                    if (c.indexOf("U_10") == 0) {
                        document.cookie = "U_10=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                        location.reload();

                    }
                }
            }
        }, 1500);
    </script>
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

       function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, countVal) {
           var CaseArray = [];
           CaseArray[0] = caseId1;
           CaseArray[1] = caseId2;
           CaseArray[2] = caseId3;
           CaseArray[3] = caseId4;
           CaseArray[4] = caseId5;
           CaseArray[5] = caseId6;
           CaseArray[6] = caseId7;
           CaseArray[7] = caseId8;
           CaseArray[8] = caseId9;
           CaseArray[9] = caseId10;
           if (CheckForPersonnelPageE3All('ctl00_Econ3ContentPlaceHolder', 'hidposdesid', 'hidCTdesid', 'hypCTdes', 'PERNUM', countVal, CaseArray)) {

               countVal = eval(countVal);
               var m = 0;
               for (m = 0; m < countVal; m++) {
                   var flag;
                   document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
                   index = m + 1;

                   var arrPosDes = [];
                   var arrNoWorker = [];
                   var arrPrefSal = [];
                   var arrCostType = [];
                   var arrMDep = [];
                   var i;
                   for (i = 0; i < 30; i++) {

                       arrPosDes[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidposdesid" + (i + 1) + "_" + eval(index)).value;
                       arrNoWorker[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_PERNUM" + (i + 1) + "_" + eval(index)).value;
                       arrPrefSal[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value;
                       arrCostType[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidCTdesid" + (i + 1) + "_" + eval(index)).value;
                       arrMDep[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidMDepdesid" + (i + 1) + "_" + eval(index)).value;

                   }

                   if ((m + 1) == countVal) {
                       flag = "Y"
                   }
                   else {

                       flag = "N"

                   }



                   //TEST END
                   if (m == 0) {
                       if (caseId1 > 1000) {
                           PageMethods.UpdateAllCases(caseId1, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 1) {
                       if (caseId2 > 1000) {
                           PageMethods.UpdateAllCases(caseId2, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 2) {
                       if (caseId3 > 1000) {
                           PageMethods.UpdateAllCases(caseId3, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 3) {
                       if (caseId4 > 1000) {
                           PageMethods.UpdateAllCases(caseId4, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }

                   else if (m == 4) {
                       if (caseId5 > 1000) {
                           PageMethods.UpdateAllCases(caseId5, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 5) {
                       if (caseId6 > 1000) {
                           PageMethods.UpdateAllCases(caseId6, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 6) {
                       if (caseId7 > 1000) {
                           PageMethods.UpdateAllCases(caseId7, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 7) {
                       if (caseId8 > 1000) {
                           PageMethods.UpdateAllCases(caseId8, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 8) {
                       if (caseId9 > 1000) {
                           PageMethods.UpdateAllCases(caseId9, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
                   else if (m == 9) {
                       if (caseId10 > 1000) {
                           PageMethods.UpdateAllCases(caseId10, index, flag, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceedAll, onErrorAll);
                       }
                   }
               }

               return false;
           }
           else {
               return false;
           }
       }

       function onSucceedAll(result) {

           var index;
           var flag;
           var valArray = [];
           var i;
           nfObject = new Intl.NumberFormat('en-US'); 
           for (i = 0; i < 30; i++) {

               valArray = result[i].split("#");
               if (i == 0) {
                   index = valArray[1];
                   flag = valArray[2];
               }

               document.getElementById("ctl00_Econ3ContentPlaceHolder_SALSUG" + (i + 1) + "_" + index + "").innerHTML = valArray[0];

               var NoWorkerN = document.getElementById("ctl00_Econ3ContentPlaceHolder_PERNUM" + (i + 1) + "_" + eval(index)).value;
               document.getElementById("ctl00_Econ3ContentPlaceHolder_PERNUM" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(NoWorkerN.replace(',', '')).toFixed(2));

               var PrefSalBN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value;
               var PrefSalBN1 = output = parseFloat(PrefSalBN.replace(/,/g, ''));
               document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value = nfObject.format(PrefSalBN1); //addCommas(parseFloat(PrefSalBN.replace(',', '')).toFixed(0));

           }

           if (flag == "Y") {
               //                localStorage.setItem("A1", "A1");
               //                localStorage.setItem("A2", "A2");
               //                localStorage.setItem("A3", "A3");
               //                localStorage.setItem("A4", "A4");
               //                localStorage.setItem("A5", "A5");
               //                localStorage.setItem("A6", "A6");
               //                localStorage.setItem("A7", "A7");
               //                localStorage.setItem("A8", "A8");
               //                localStorage.setItem("A9", "A9");
               //                //localStorage.setItem("A10", "A10");
               //                localStorage.setItem("A11", "A11");
               //                localStorage.setItem("A12", "A12");
               //                localStorage.setItem("A13", "A13");
               //                localStorage.setItem("A14", "A14");
               //                localStorage.setItem("A15", "A15");
               //                localStorage.setItem("A16", "A16");

               //                localStorage.setItem("I1", "I1");
               //                localStorage.setItem("I2", "I2");
               //                localStorage.setItem("I3", "I3");

               //                localStorage.setItem("R1", "R1");
               //                localStorage.setItem("R2", "R2");
               //                localStorage.setItem("R3", "R3");
               //                localStorage.setItem("R4", "R4");
               //                localStorage.setItem("R5", "R5");
               //                localStorage.setItem("R6", "R6");
               //                localStorage.setItem("R7", "R7");
               //                localStorage.setItem("R8", "R8");
               //                localStorage.setItem("R9", "R9");
               //                localStorage.setItem("R10", "R10");
               //                localStorage.setItem("R11", "R11");
               //                localStorage.setItem("R12", "R12");
               //                localStorage.setItem("R13", "R13");

               document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
               //document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);

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

            if (CheckForPersonnelPageE3('ctl00_Econ3ContentPlaceHolder', 'hidposdesid', 'hidCTdesid', 'hypCTdes', 'PERNUM', index)) {

                document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';

                var arrPosDes = [];
                var arrNoWorker = [];
                var arrPrefSal = [];
                var arrCostType = [];
                var arrMDep = [];
                var i;

                for (i = 0; i < 30; i++) {

                    arrPosDes[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidposdesid" + (i + 1) + "_" + eval(index)).value;
                    arrNoWorker[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_PERNUM" + (i + 1) + "_" + eval(index)).value;
                    arrPrefSal[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value;
                    arrCostType[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidCTdesid" + (i + 1) + "_" + eval(index)).value;
                    arrMDep[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidMDepdesid" + (i + 1) + "_" + eval(index)).value;

                }

                PageMethods.UpdateCase(caseId, index, arrPosDes, arrNoWorker, arrPrefSal, arrCostType, arrMDep, onSucceed, onError);

                return false;
            }
            else {
                return false;
            }
        }


        function onSucceed(result) {

            var index;
            var pageL;

            nfObject = new Intl.NumberFormat('en-US'); 
            var i;
            var valArray = [];
            for (i = 0; i < 30; i++) {

                valArray = result[i].split("#");
                if (i == 0) {
                    index = valArray[1];
                }

                document.getElementById("ctl00_Econ3ContentPlaceHolder_SALSUG" + (i + 1) + "_" + index + "").innerHTML = valArray[0];

                var NoWorkerN = document.getElementById("ctl00_Econ3ContentPlaceHolder_PERNUM" + (i + 1) + "_" + eval(index)).value;
                document.getElementById("ctl00_Econ3ContentPlaceHolder_PERNUM" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(NoWorkerN.replace(',', '')).toFixed(2));

//                var PrefSalBN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value;
//                document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(PrefSalBN.replace(',', '')).toFixed(0));
                var PrefSalBN = document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value;
                var PrefSalBN1 = output = parseFloat(PrefSalBN.replace(/,/g, ''));
                document.getElementById("ctl00_Econ3ContentPlaceHolder_SALPRE" + (i + 1) + "_" + eval(index)).value = nfObject.format(PrefSalBN1);

            }

            //            localStorage.setItem("A1", "A1");
            //            localStorage.setItem("A2", "A2");
            //            localStorage.setItem("A3", "A3");
            //            localStorage.setItem("A4", "A4");
            //            localStorage.setItem("A5", "A5");
            //            localStorage.setItem("A6", "A6");
            //            localStorage.setItem("A7", "A7");
            //            localStorage.setItem("A8", "A8");
            //            localStorage.setItem("A9", "A9");
            //            //localStorage.setItem("A10", "A10");
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

            document.cookie = "U1=U1;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U2=U2;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U3=U3;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U4=U4;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U5=U5;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U6=U6;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U7=U7;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U8=U8;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U9=U9;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            //document.cookie = "U_10=U_10;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
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
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Personnel Assumptions')"
                onmouseout="UnTip()">
                Econ3 - Personnel Assumptions
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
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PD" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_PD'); " />Position
                        Description
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_NOW" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_NOW'); " />Number
                        of Workers
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SBS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_SBS'); " />Salary
                        & Benefits Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_SBP" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_SBP'); " />Salary
                        & Benefits Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_CT" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_CT'); " />Cost
                        Type
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_MD" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_MD'); " />Mfg.
                        Department
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
