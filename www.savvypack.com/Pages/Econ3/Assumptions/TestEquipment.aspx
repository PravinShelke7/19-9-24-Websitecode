<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/Econ3.master" AutoEventWireup="false" CodeFile="TestEquipment.aspx.vb" Inherits="Pages_Econ3_Assumptions_TestEquipment" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E3Comman.js"></script>
    <script>
        function removeSession() {
            localStorage.removeItem("A7");
        }

        window.setInterval(function () {

            if (localStorage.getItem("A7") != null) {
                localStorage.removeItem("A7");
                location.reload();

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
    <script>
        function removeSession() {
            localStorage.removeItem("A7");
        }

        window.setInterval(function () {

            if (localStorage.getItem("A7") != null) {
                localStorage.removeItem("A7");
                location.reload();

            }

        }, 1500);
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
    <script type="text/javascript">
        function UpdateAll(caseId1, caseId2, caseId3, caseId4, caseId5, caseId6, caseId7, caseId8, caseId9, caseId10, countVal) {

            if (CheckForPEquipmentPageE3All('ctl00_Econ3ContentPlaceHolder', 'hidAssetId', 'hidEqDepid', 'hypEqDep', 'NOASSET', countVal)) {

                countVal = eval(countVal);
                var m = 0;
                for (m = 0; m < countVal; m++) {
				
											
                    var flag;
                    document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';

                    index = m + 1;
                    var arrEQ = [];
                    var arrNUM = [];
                    var arrPCOST = [];
                    var arrDeptT = [];

                    var i;
                    for (i = 0; i < 30; i++) {

                        arrNUM[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_NOASSET" + (i + 1) + "_" + index + "").value;
                        arrEQ[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidAssetId" + (i + 1) + "_" + eval(index)).value;
                        arrPCOST[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTP" + (i + 1) + "_" + eval(index)).value;
                        arrDeptT[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidEqDepid" + (i + 1) + "_" + eval(index)).value;
                    }

                    if ((m + 1) == countVal) {
                        flag = "Y"
                    }
                    else {
                        flag = "N"
                    }


                    //TEST END
                    if (m == 0) 
					{
						if (caseId1 > 1000) 
						{
					        
					        PageMethods.UpdateAllCases(caseId1, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }
                    else if (m == 1) {
                        if (caseId2 > 1000) {
						//setTimeout("hidetest('"+caseId2+"')", 2000);
                            PageMethods.UpdateAllCases(caseId2, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
							
                        }
                    }
                    else if (m == 2) {
                        if (caseId3 > 1000) {
						//setTimeout("hidetest('"+caseId3+"')", 2000);
                            PageMethods.UpdateAllCases(caseId3, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
							
                        }
                    }
                    else if (m == 3) {
                        if (caseId4 > 1000) {
						//setTimeout("hidetest('"+caseId4+"')", 2000);
                            PageMethods.UpdateAllCases(caseId4, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }

                    else if (m == 4) {

                        if (caseId5 > 1000) {
						//setTimeout("hidetest('"+caseId5+"')", 2000);
                            PageMethods.UpdateAllCases(caseId5, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }
                    else if (m == 5) {
                        if (caseId6 > 1000) {
					//	setTimeout("hidetest('"+caseId6+"')", 2000);
                            PageMethods.UpdateAllCases(caseId6, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }
                    else if (m == 6) {
                        if (caseId7 > 1000) {
						//setTimeout("hidetest('"+caseId7+"')", 2000);
                            PageMethods.UpdateAllCases(caseId7, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }
                    else if (m == 7) {
                        if (caseId8 > 1000) {
						//setTimeout("hidetest('"+caseId8+"')", 2000);
                            PageMethods.UpdateAllCases(caseId8, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }
                    else if (m == 8) {
                        if (caseId9 > 1000) {
						//setTimeout("hidetest('"+caseId9+"')", 2000);
                            PageMethods.UpdateAllCases(caseId9, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }
                    else if (m == 9) {
                        if (caseId10 > 1000) {
					//	setTimeout("hidetest('"+caseId10+"')", 2000);
                            PageMethods.UpdateAllCases(caseId10, index, flag, arrEQ, arrNUM, arrPCOST, arrDeptT, onSucceedAll, onErrorAll);
                        }
                    }


                    // PageMethods.GetStatus(arrCOST[i], caseId, onSucceed, onError);

                }
                // document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';
                return false;
            }
            else {
                return false;
            }
        }

        function onSucceedAll(result) {

            var index;
            var valArray = [];
            var flag;


            var i;

            for (i = 0; i < 30; i++) {
                valArray = result[i].split("#");

                if (i == 0) {
                    index = valArray[5];
                    flag = valArray[6];
					alert(index);
                }


                var NUM = document.getElementById("ctl00_Econ3ContentPlaceHolder_NOASSET" + (i + 1) + "_" + eval(index)).value;
                document.getElementById("ctl00_Econ3ContentPlaceHolder_NOASSET" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(NUM.replace(',', '')).toFixed(0));

                var COST = document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTP" + (i + 1) + "_" + eval(index)).value;
                document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTP" + (i + 1) + "_" + eval(index)).value = addCommas(parseFloat(COST.replace(',', '')).toFixed(0));


                document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTS" + (i + 1) + "_" + index + "").innerHTML = valArray[0];
                document.getElementById("ctl00_Econ3ContentPlaceHolder_ATYPE" + (i + 1) + "_" + index + "").innerHTML = valArray[1];
                document.getElementById("ctl00_Econ3ContentPlaceHolder_PAS" + (i + 1) + "_" + index + "").innerHTML = valArray[2];
                document.getElementById("ctl00_Econ3ContentPlaceHolder_ECS" + (i + 1) + "_" + index + "").innerHTML = valArray[3];
                document.getElementById("ctl00_Econ3ContentPlaceHolder_NGCS" + (i + 1) + "_" + index + "").innerHTML = valArray[4];




            }

            if (flag == "Y") {
                localStorage.setItem("A1", "A1");
                localStorage.setItem("A2", "A2");
                localStorage.setItem("A3", "A3");
                localStorage.setItem("A4", "A4");
                localStorage.setItem("A5", "A5");
                localStorage.setItem("A6", "A6");
                // localStorage.setItem("A7", "A7");
                localStorage.setItem("A8", "A8");
                localStorage.setItem("A9", "A9");
                localStorage.setItem("A10", "A10");
                localStorage.setItem("A11", "A11");
                localStorage.setItem("A12", "A12");
                localStorage.setItem("A13", "A13");
                localStorage.setItem("A14", "A14");
                localStorage.setItem("A15", "A15");
                localStorage.setItem("A16", "A16");

                localStorage.setItem("I1", "I1");
                localStorage.setItem("I2", "I2");
                localStorage.setItem("I3", "I3");

                localStorage.setItem("R1", "R1");
                localStorage.setItem("R2", "R2");
                localStorage.setItem("R3", "R3");
                localStorage.setItem("R4", "R4");
                localStorage.setItem("R5", "R5");
                localStorage.setItem("R6", "R6");
                localStorage.setItem("R7", "R7");
                localStorage.setItem("R8", "R8");
                localStorage.setItem("R9", "R9");
                localStorage.setItem("R10", "R10");
                localStorage.setItem("R11", "R11");
                localStorage.setItem("R12", "R12");
                localStorage.setItem("R13", "R13");

				 document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';
                // setTimeout("hide()", 9000);
            }
        }
        function hide() {
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'none';
        }
		 function hidetest(id) {
           //alert(id);
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
            // alert(index);
            if(CheckForPEquipmentPageE3('ctl00_Econ3ContentPlaceHolder','hidAssetId','hidEqDepid','hypEqDep','NOASSET',index))
            {

             var SessionID="<%=Session("E3SessionID")%>";   
             var CompID="<%=Session("AssumptionID")%>"; 
            document.getElementById("ctl00_Econ3ContentPlaceHolder_loading").style.display = 'inline';
              var arrEQ = [];
              var arrNUM = [];
              var arrPCOST = [];
              var arrDeptT=[];
             // alert(index);
            var i;
            for (i = 0; i < 30; i++) {

                arrNUM[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_NOASSET" + (i + 1) + "_" + index + "").value;
                arrEQ[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidAssetId" + (i + 1) + "_" + eval(index)).value;
                arrPCOST[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTP" + (i + 1) + "_" + eval(index)).value;
                 arrDeptT[i] = document.getElementById("ctl00_Econ3ContentPlaceHolder_hidEqDepid" + (i + 1) + "_" + eval(index)).value;
               }
           
            PageMethods.UpdateCase(caseId, index,arrEQ,arrNUM,arrPCOST,arrDeptT, onSucceed, onError);
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
           
            for (i = 0; i < 30; i++) {
          
                      valArray = result[i].split("#");      
                     if(i==0)
                     {
                     index=valArray[5]; 
                     }
                   //  alert(index);
                                                     
                      var NUM=document.getElementById("ctl00_Econ3ContentPlaceHolder_NOASSET" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_NOASSET" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(NUM.replace(',','')).toFixed(0));

                      var COST=document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTP" + (i + 1) + "_" + eval(index)).value;
                      document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTP" + (i + 1) + "_" + eval(index)).value=addCommas(parseFloat(COST.replace(',','')).toFixed(0));

                       document.getElementById("ctl00_Econ3ContentPlaceHolder_ASCOSTS" + (i + 1) + "_" + index + "").innerHTML = valArray[0]; 
                       document.getElementById("ctl00_Econ3ContentPlaceHolder_ATYPE" + (i + 1) + "_" + index + "").innerHTML = valArray[1];     
                       document.getElementById("ctl00_Econ3ContentPlaceHolder_PAS" + (i + 1) + "_" + index + "").innerHTML = valArray[2];     
                       document.getElementById("ctl00_Econ3ContentPlaceHolder_ECS" + (i + 1) + "_" + index + "").innerHTML = valArray[3];     
                       document.getElementById("ctl00_Econ3ContentPlaceHolder_NGCS" + (i + 1) + "_" + index + "").innerHTML = valArray[4];                                        
              

            }

                           localStorage.setItem("A1", "A1");
                        localStorage.setItem("A2", "A2");
                         localStorage.setItem("A3", "A3");
                          localStorage.setItem("A4", "A4");
                          localStorage.setItem("A5", "A5");
                          localStorage.setItem("A6", "A6");
                         // localStorage.setItem("A7", "A7");
                          localStorage.setItem("A8", "A8");
                          localStorage.setItem("A9", "A9");
                          localStorage.setItem("A10", "A10");
                          localStorage.setItem("A11", "A11");
                          localStorage.setItem("A12", "A12");
                          localStorage.setItem("A13", "A13");
                          localStorage.setItem("A14", "A14");
                          localStorage.setItem("A15", "A15");
                             localStorage.setItem("A16", "A16");

                            localStorage.setItem("I1", "I1");
                        localStorage.setItem("I2", "I2");
                         localStorage.setItem("I3", "I3");

                          localStorage.setItem("R1", "R1");
                        localStorage.setItem("R2", "R2");
                         localStorage.setItem("R3", "R3");
                          localStorage.setItem("R4", "R4");
                          localStorage.setItem("R5", "R5");
                          localStorage.setItem("R6", "R6");
                          localStorage.setItem("R7", "R7");
                          localStorage.setItem("R8", "R8");
                          localStorage.setItem("R9", "R9");
                          localStorage.setItem("R10", "R10");
                          localStorage.setItem("R11", "R11");
                          localStorage.setItem("R12", "R12");
                          localStorage.setItem("R13", "R13");

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
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('Equipment Assumptions')"
                onmouseout="UnTip()">
                Econ3 - Equipment Assumptions
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
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_AD" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_AD'); " />Asset
                        Description
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ACS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_ACS'); " />Asset
                        Cost Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ACP" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_ACP'); " />Asset
                        Cost Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_AT" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_AT'); " />Area
                        Type
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PAS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_PAS'); " />Plant
                        Area Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_PAP" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_PAP'); " />Plant
                        Area Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ECS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_ECS'); " />Elec.
                        Consumption Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_ECP" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_ECP'); " />Elec.
                        Consumption Pref.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_GCS" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_GCS'); " />Nat.
                        Gas Consumption Sugg.
                        <br />
                        <input type="checkbox" id="ctl00_Econ3ContentPlaceHolder_GCP" checked="checked" onclick="showhideALL2('ctl00_Econ3ContentPlaceHolder_GCP'); " />Nat.
                        Gas Consumption Pref.
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

