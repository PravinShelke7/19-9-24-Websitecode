<%@ Page Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="Preferences.aspx.vb" Inherits="Pages_Econ3_Assumptions_Preferences" title="E3-Preferences" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
   
   
      <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E3Comman.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
      <style type ="text/css">
    .dropdownunit
{
	font-family:Optima;
	font-size:11.5Px;
	height:17px;
	
	background-color:#FEFCA1;
	margin-top:2px;
	margin-bottom:2px;
	margin-left:2px;
	margin-right:2px;
	border-right: #7F9DB9 1px solid;
	border-top: #7F9DB9 1px solid;
	border-left: #7F9DB9 1px solid;
	border-bottom: #7F9DB9 1px solid;

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
               //localStorage.removeItem("A4");
               document.cookie = "U_17=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
           }

           window.setInterval(function () {

               //              if (localStorage.getItem("A4") != null) {
               //                  localStorage.removeItem("A4");
               //                  location.reload();

               //              }
               if (document.cookie.length != 0) {

                   var ca = document.cookie.split(";");
                   for (var i = 0; i < ca.length; i++) {
                       var c = ca[i].trim();

                       if (c.indexOf("U_17") == 0) {
                           document.cookie = "U_17=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
                           location.reload();

                       }
                   }
               }

           }, 1500);
</script>

    <script type="text/javascript">
        function Update(caseId, index, dc) {
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
            var arr = [];
            var arrMat;
            var arrerd = [];
            var arrmrd = []
            var t;
            var i = 1;
            var unit;

            for (t = 1; t <= 5; t++) {
                
                if (t == 3) {
                    arrerd[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_" + index + "_" + t + "_" + i + "").value;
                    arrmrd[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_rdmid_" + index + "_" + t + "_" + i + "").value;
                    if (document.getElementById("ctl00_Econ3ContentPlaceHolder_rdmid_" + index + "_" + t + "_" + i + "").checked == true) {
                        unit = 1;
                    }
                    else if (document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_" + index + "_" + t + "_" + i + "").checked == true) {
                        unit = 0;
                    }
                }
                if (t == 1 || t == 2) {
                    arr[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlOid_" + index + "_" + t + "_" + i + "").value;

                }
                if (t == 4) {
                    arr[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlEffDateid_" + index + "_" + t + "_" + i + "").value;
                }
                if (t == 5) {
                    arr[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_" + index + "_" + t + "_" + i + "").value;

                }
            }
           // alert(arr+"--"+caseId+"--"+unit);
            PageMethods.UpdateCase(arr,index, caseId, unit, onSucceed, onError);
          //  document.getElementById("ctl00_Econ3ContentPlaceHolder_btnrefresh").click();
           //location.reload();
            return false;
        }

        function onSucceed(result) {
            var flag;
            var index=result[0];
            flag = result[1];
            var i=1;
            for (t = 1; t <= 6; t++) {
               
                if ( t == 1) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlOid_" + index + "_" + t + "_" + i + "").value=result[2];

                }
                if (t == 2) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlOid_" + index + "_" + t + "_" + i + "").value = result[3];

                }
                if (t == 3) {
                    if (result[4] == 1) {
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_rdmid_" + index + "_" + t + "_" + i + "").value = 1;
                        }
                        else {
                            document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_" + index + "_" + t + "_" + i + "").value=0;
                        }
                    
                }
                if (t == 4) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlEffDateid_" + index + "_" + t + "_" + i + "").value=result[6];
                }
                if (t == 5) {
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_" + index + "_" + t + "_" + i + "").value=result[5];

                 }

             }
             var unit;

             //alert(document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_0_3_1").value);
             if (index > 0) {

                 if (document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_0_5_1").value != result[5]) {                 
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_lblc"+index).innerText = "\n Currency Mismatch";//.fontcolor("red");
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_lblc" + index).style.color = 'red';
                 }
                 else {
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_lblc"+index).innerText = ""
                 }
                 if (result[4] == 1) {
                     unit = false;
                 }
                 else {
                     unit = true;
                 }
                 if (document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_0_3_1").checked != unit) {

                     document.getElementById("ctl00_Econ3ContentPlaceHolder_lblu"+index).innerText = "\n Unit Mismatch";//.fontcolor("red");
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_lblu" + index).style.color = 'red';
                 
                 }
                 else {
                     document.getElementById("ctl00_Econ3ContentPlaceHolder_lblu"+index).innerText = ""
                 }

             }
            //              localStorage.setItem("A1", "A1");
            //              localStorage.setItem("A2", "A2");
            //              localStorage.setItem("A3", "A3");
            //              localStorage.setItem("A4", "A4");
            //              localStorage.setItem("A5", "A5");
            //              localStorage.setItem("A6", "A6");
            //              localStorage.setItem("A7", "A7");
            //              // localStorage.setItem("A8", "A8");
            //              localStorage.setItem("A9", "A9");
            //              localStorage.setItem("A10", "A10");
            //              localStorage.setItem("A11", "A11");
            //              localStorage.setItem("A12", "A12");
            //              localStorage.setItem("A13", "A13");
            //              localStorage.setItem("A14", "A14");
            //              localStorage.setItem("A15", "A15");
            //              localStorage.setItem("A16", "A16");

            //              localStorage.setItem("I1", "I1");
            //              localStorage.setItem("I2", "I2");
            //              localStorage.setItem("I3", "I3");

            //              localStorage.setItem("R1", "R1");
            //              localStorage.setItem("R2", "R2");
            //              localStorage.setItem("R3", "R3");
            //              localStorage.setItem("R4", "R4");
            //              localStorage.setItem("R5", "R5");
            //              localStorage.setItem("R6", "R6");
            //              localStorage.setItem("R7", "R7");
            //              localStorage.setItem("R8", "R8");
            //              localStorage.setItem("R9", "R9");
            //              localStorage.setItem("R10", "R10");
            //              localStorage.setItem("R11", "R11");
            //              localStorage.setItem("R12", "R12");
            //              localStorage.setItem("R13", "R13");

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
            document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_13=U_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_14=U_14;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_15=U_15;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            document.cookie = "U_16=U_16;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
            //document.cookie = "U_17=U_17;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

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
        function onError(result) {
        }
      
        </script>

    <script type="text/javascript">
        function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, datacnt) {
            for (i = 1; i <= datacnt; i++) {
                document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
                var arr = [];
                var arrMat;
                var arrerd = [];
                var arrmrd = []
                var t;
                var i1 = 1;
                var unit;
                var flag;
                if (i == datacnt) {
                    flag = "Y";
                }
                else {
                    flag = "N";

                }
                for (t = 1; t <= 5; t++) {
                    if (t == 3) {
                        arrerd[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_" + (i - 1) + "_" + t + "_" + i1 + "").value;
                        arrmrd[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_rdmid_" + (i - 1) + "_" + t + "_" + i1 + "").value;
                        if (document.getElementById("ctl00_Econ3ContentPlaceHolder_rdmid_" + (i - 1) + "_" + t + "_" + i1 + "").checked == true) {
                            unit = 1;
                        }
                        else if (document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_" + (i - 1) + "_" + t + "_" + i1 + "").checked == true) {
                            unit = 0;
                        }

                    }
                    if (t == 1 || t == 2) {
                        arr[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlOid_" + (i - 1) + "_" + t + "_" + i1 + "").value;
                    }
                    if (t == 4) {
                        arr[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlEffDateid_" + (i - 1) + "_" + t + "_" + i1 + "").value;
                    }
                    if (t == 5) {
                        arr[t] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_" + (i - 1) + "_" + t + "_" + i1 + "").value;

                    }
                }

                if (i == 1) {
                   // alert(i);
                    PageMethods.UpdateAllCases(arr,i, flag, caseId1, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 2) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId2, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 3) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId3, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 4) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId4, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 5) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId5, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 6) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId6, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 7) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId7, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 8) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId8, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 9) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId9, unit, onSucceedAll, onErrorAll);
                }
                else if (i == 10) {
                    PageMethods.UpdateAllCases(arr,i, flag, caseId10, unit, onSucceedAll, onErrorAll);
                }
            }
           // location.reload();
           // document.getElementById("ctl00_Econ3ContentPlaceHolder_btnrefresh").click();
            
            return false;

        }

        function onSucceedAll(result) {
            var flag;
            var index = result[0]-1;
            flag = result[1];
            //alert(result);
            var i = 1;
            //alert(index);
            for (t = 1; t <= 6; t++) {
              //  alert(t);
                if (t == 1) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlOid_" + index + "_" + t + "_" + i + "").value = result[2];

                }
                if (t == 2) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlOid_" + index + "_" + t + "_" + i + "").value = result[3];

                }
                if (t == 3) {
                    if (result[4] == 1) {
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_rdmid_" + index + "_" + t + "_" + i + "").value = 1;
                    }
                    else {
                        document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_" + index + "_" + t + "_" + i + "").value = 0;
                    }

                }
                if (t == 4) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlEffDateid_" + index + "_" + t + "_" + i + "").value = result[6];
                }
                if (t == 5) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_" + index + "_" + t + "_" + i + "").value = result[5];
//                    if (document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_0_" + t + "_" + i + "").value != result[5])
//                    {
//                   // document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_" + index + "_" + t + "_" + i + "").value
//                    }
                }
                //alert(t);
            }

            if (index > 0) {

                if (document.getElementById("ctl00_Econ3ContentPlaceHolder_ddlCurrid_0_5_1").value != result[5]) {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_lblc" + index).innerText = "\n Currency Mismatch"; //.fontcolor("red");
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_lblc" + index).style.color = 'red';
                }
                else {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_lblc" + index).innerText = ""
                }
                if (result[4] == 1) {
                    unit = false;
                }
                else {
                    unit = true;
                }
                if (document.getElementById("ctl00_Econ3ContentPlaceHolder_rdeid_0_3_1").checked != unit) {

                    document.getElementById("ctl00_Econ3ContentPlaceHolder_lblu" + index).innerText = "\n Unit Mismatch"; //.fontcolor("red");
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_lblu" + index).style.color = 'red';

                }
                else {
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_lblu" + index).innerText = ""
                }

            }

            if (flag == "Y") {
                //            localStorage.setItem("A1", "A1");
                //            localStorage.setItem("A2", "A2");
                //            localStorage.setItem("A3", "A3");
                //            localStorage.setItem("A4", "A4");
                //            localStorage.setItem("A5", "A5");
                //            localStorage.setItem("A6", "A6");
                //            localStorage.setItem("A7", "A7");
                //            // localStorage.setItem("A8", "A8");
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
                document.cookie = "U_12=U_12;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
                document.cookie = "U_13=U_13;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
                document.cookie = "U_14=U_14;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
                document.cookie = "U_15=U_15;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
                document.cookie = "U_16=U_16;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";
                //document.cookie = "U_17=U_17;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2099 20:47:11 UTC;";

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
      <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Preferences')" onmouseout="UnTip()" >
                  Preferences
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
                        
                          
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_CM" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_CM'); " />Country of Manufacture
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_CD" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_CD'); " />Country of Destination
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PREFU" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_PREFU'); " />Prefferred Unit
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_EFFDATE" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_EFFDATE'); " />Effective Date
                          <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PREFCUR" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_PREFCUR'); " />Preferred Currency
                          <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ERGYCAL" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_ERGYCAL'); " />Energy Calculations
                          <br />
                           <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DISCAL" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_DISCAL'); " />Discrete Calculations
                          <br />
                          <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_DESCAL" checked="checked"  onclick="showhideALL1('ctl00_Econ3ContentPlaceHolder_DESCAL'); " />Design Waste Calculations
                          <br />
                        </div>   
                                     
                    </div>
            </td>
            <td>
               <div class="divHeader3">
                <table>
                  <tr style="height:20px;">
                     <td>
                        Column Width: 
                     </td>
                     <td>
                        <asp:TextBox ID="txtDWidth" runat="server" Width="35px" CssClass="SmallTextBox"></asp:TextBox>
                     </td>
                     <td>
                         <asp:Button ID="btnWidthSet" runat="server" Text="Update" CssClass="Button"  />
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
