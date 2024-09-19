// build out the divs, set attributes and call the fade function //
var MSGTIMER = 20;
var MSGSPEED = 5;
var MSGOFFSET = 3;
var MSGHIDE = 5;
var msg;
function inlineMsg(target, string, autohide) {
    // START OF MESSAGE SCRIPT //


    var msgcontent;
    if (!document.getElementById('msg')) {
        msg = document.createElement('div');
        msg.id = 'msg';
        msgcontent = document.createElement('div');
        msgcontent.id = 'msgcontent';
        document.body.appendChild(msg);
        msg.appendChild(msgcontent);
        msg.style.filter = 'alpha(opacity=0)';
        msg.style.opacity = 0;
        msg.alpha = 0;
    } else {
        msg = document.getElementById('msg');
        msgcontent = document.getElementById('msgcontent');
    }
    msgcontent.innerHTML = string;
    msg.style.display = 'block';
    var msgheight = msg.offsetHeight;
    var msgwidth = msg.offsetWidth;
    var targetdiv = document.getElementById(target);
    targetdiv.focus();
    var targetheight = targetdiv.offsetHeight;
    var targetwidth = targetdiv.offsetWidth;

    var topposition;
    if (msgwidth < 200) {
        topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2) - msgheight;
    }
    else {
        topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2) - msgheight + 9;
    }
    var leftposition = leftPosition(targetdiv) - (msgwidth / 2) + (targetwidth / 2) + MSGOFFSET;
    msg.style.top = topposition + 'px';
    msg.style.left = leftposition + 'px';
    clearInterval(msg.timer);
    msg.timer = setInterval("fadeMsg(1)", MSGTIMER);
    if (!autohide) {
        autohide = MSGHIDE;
    }
    window.setTimeout("hideMsg()", (autohide * 2000));
}


// hide the form alert //
function hideMsg(msg) {
    var msg = document.getElementById('msg');
    if (!msg.timer) {
        msg.timer = setInterval("fadeMsg(0)", MSGTIMER);
    }
}

// face the message box //
function fadeMsg(flag) {
    if (flag == null) {
        flag = 1;
    }
    var msg = document.getElementById('msg');
    var value;
    if (flag == 1) {
        value = msg.alpha + MSGSPEED;
    } else {
        value = msg.alpha - MSGSPEED;
    }
    msg.alpha = value;
    msg.style.opacity = (value / 100);
    msg.style.filter = 'alpha(opacity=' + value + ')';
    if (value >= 99) {
        clearInterval(msg.timer);
        msg.timer = null;
    } else if (value <= 1) {
        msg.style.display = "none";
        clearInterval(msg.timer);
    }
}

// calculate the position of the element in relation to the left of the browser //
function leftPosition(target) {
    var left = 0;
    if (target.offsetParent) {
        while (1) {
            left += target.offsetLeft;
            if (!target.offsetParent) {
                break;
            }
            target = target.offsetParent;
        }
    } else if (target.x) {
        left += target.x;
    }
    return left;
}

// calculate the position of the element in relation to the top of the browser window //
function topPosition(target) {
    var top = 0;
    if (target.offsetParent) {
        while (1) {
            top += target.offsetTop;
            if (!target.offsetParent) {
                break;
            }
            target = target.offsetParent;
        }
    } else if (target.y) {
        top += target.y;
    }
    return top;
}

// preload the arrow //
if (document.images) {
    arrow = new Image(7, 80);
    arrow.src = "../images/msg_arrow.gif";
}


// Checking individual text box for numric
function checkNumeric(value, id) {

    var anum = /(^\d+$)|(^\d+\.\d+$)/

    if (anum.test(value.replace(/,/g, ""))) {
        return true;
    }
    else {

        return false;
    }
}



// Checking All text box for numric
function checkNumericAll() {

    var txtarray = document.getElementsByTagName("input");
    var flag;
    var anum = /(^\d+$)|(^\d+\.\d+$)/
    for (var i = 0; i < txtarray.length; i++) {
        if (txtarray[i].type == "text") {
            var id = txtarray[i].id;
            //alert(txtarray[i].value);
            // if (anum.test(txtarray[i].value.replace(/,/g, "")))
            if (IsNumeric(txtarray[i].value.replace(/,/g, ""))) {

                flag = true;
            }
            else {
                inlineMsg(id, "Invalid Number");
                flag = false;
                break;
            }

        }

    }

    return flag;
}


function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}



function CheckForPersonnelPageE3(MasterId, Pos, CostT, CostTName, Pernum, index) {

    var IsNumflag;
    var IsDep;
    var isCostFlag;

    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;
    }

    //Checking Cost type
    if (IsNumflag) {
       
        for (var j = 1; j <= 30; j++) {
            var PosId = MasterId + "_" + Pos + j + "_" + index;
            var costId = MasterId + "_" + CostT + j + "_" + index;
            var costNameId = MasterId + "_" + CostTName + j + "_" + index;
            var PernumId = MasterId + "_" + Pernum + j + "_" + index;

            var PosVal = document.getElementById(PosId).value;
            var costVal = document.getElementById(costId).value;
            var costNameVal = document.getElementById(costNameId).value;
            var PerNumVal = document.getElementById(PernumId).value;
            //alert(DepId);
            if (PosVal > 0) {
                if (PerNumVal == 0) {
                    IsNumber = false;
                    //                                         alert(costNameId);
                    //                                         alert(document.getElementById(costNameId));
                    inlineMsg(PernumId, "Update did not complete, please enter a positive, non-zero number.", 3);
                    break;
                }
                else {
                    IsNumber = true;
                }
                if (costVal == 0) {
                    isCostFlag = false;
                    //                                         alert(costNameId);
                    //                                         alert(document.getElementById(costNameId));
                    inlineMsg(costNameId, "Update did not complete, please select a cost type.", 3);
                    break;
                }
                else {
                    isCostFlag = true;
                }

            }
            else {
                isCostFlag = true;
            }

        }

    }

    if (IsNumflag && isCostFlag && IsNumber) {
        return true;
    }
    else {
        return false;
    }


}


function CheckForPersonnelPageE3All(MasterId, Pos, CostT, CostTName, Pernum, index,CaseArray) {

    var IsNumflag;
    var IsDep;
    var IsCostFlag;

    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;
    }

    //Checking Cost type
    if (IsNumflag) {

        var m;
        for (m = 1; m <= index; m++) {
			if(CaseArray[m-1]>=1000)
			{
				//alert('sud'+CaseArray[m-1]);
            for (var j = 1; j <= 30; j++) {
                var PosId = MasterId + "_" + Pos + j + "_" + m;
                var costId = MasterId + "_" + CostT + j + "_" + m;
                var costNameId = MasterId + "_" + CostTName + j + "_" + m;
                var PernumId = MasterId + "_" + Pernum + j + "_" + m;

                var PosVal = document.getElementById(PosId).value;
                var costVal = document.getElementById(costId).value;
                var costNameVal = document.getElementById(costNameId).value;
                var PerNumVal = document.getElementById(PernumId).value;
                //alert(DepId);
                if (PosVal > 0) {
                    if (PerNumVal == 0) {
                        IsNumber = false;
                        //                                         alert(costNameId);
                        //                                         alert(document.getElementById(costNameId));
                        inlineMsg(PernumId, "Update did not complete, please enter a positive, non-zero number.", 3);
                        return false;
                    }
                    else {
                        IsNumber = true;
                    }
                    if (costVal == 0) {
                        IsCostFlag = false;
                        //                                         alert(costNameId);
                        //                                         alert(document.getElementById(costNameId));
                        inlineMsg(costNameId, "Update did not complete, please select a cost type.", 3);
                        return false;
                    }
                    else {
                        IsCostFlag = true;
                    }

                }
                else {
                    IsCostFlag = true;
                }

            }

        }
		}
    }


    if (IsNumflag && IsCostFlag && IsNumber) {
        return true;
    }
    else {
        return false;
    }


}


function CheckForPEquipmentPageE3(MasterId, Equip, Dep, DepName, EqNum, index) {
    var IsNumflag;
    var IsDep;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }

    //Cheking Dept.
    if (IsNumflag) {
        for (var j = 1; j <= 30; j++) {
            var EqId = MasterId + "_" + Equip + j + "_" + index;
            var DepId = MasterId + "_" + Dep + j + "_" + index;
            var DepNameId = MasterId + "_" + DepName + j + "_" + index;
            var EqNumId = MasterId + "_" + EqNum + j + "_" + index;

            var EqVal = document.getElementById(EqId).value;
            var DepVal = document.getElementById(DepId).value;
            var DepText = document.getElementById(DepNameId);
            var EqpNumVal = document.getElementById(EqNumId).value;




            if (EqVal > 0) {
                //                  
                if (EqpNumVal == 0) {
                    IsNumber = false;
                    inlineMsg(EqNumId, "Update did not complete, please enter a positive, non-zero number.", 3);
                    break;
                }
                else {
                    IsNumber = true;
                }
                if (DepVal == 0) {
                    IsDep = false;
                    inlineMsg(DepNameId, "Update did not complete, please select a department.", 3);
                    break;
                }
                else {

                    if
                    (DepText.innerHTML == 'Dept. Conflict') {
                        IsDep = false;
                        inlineMsg(DepNameId, "Update did not complete, please select a department.", 3);
                        break;
                    }
                    else {
                        IsDep = true;
                    }
                }

            }
            else {
                IsDep = true;
            }

        }
    }

    if (IsNumflag && IsDep && IsNumber) {
        return true;
    }
    else {
        return false;
    }


}


function CheckForPEquipmentPageE3All(MasterId, Equip, Dep, DepName, EqNum, index) {

    var IsNumflag;
    var IsDep;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }
    //alert(MasterId);
    // alert(index);

    //Cheking Dept.
    if (IsNumflag) {

        // for (var m = 1; m <= 2; j++) {
        var m;
        for (m = 1; m <= index; m++) {
            for (var j = 1; j <= 30; j++) {
                var EqId = MasterId + "_" + Equip + j + "_" + m;
                var DepId = MasterId + "_" + Dep + j + "_" + m;
                var DepNameId = MasterId + "_" + DepName + j + "_" + m;
                var EqNumId = MasterId + "_" + EqNum + j + "_" + m;

                var EqVal = document.getElementById(EqId).value;
                var DepVal = document.getElementById(DepId).value;
                var DepText = document.getElementById(DepNameId);
                var EqpNumVal = document.getElementById(EqNumId).value;

                //  alert('Sud E3.1----' + m + '----' + j);


                if (EqVal > 0) {

                    //                  
                    if (EqpNumVal == 0) {
                        // alert(EqId + '^^^' + EqNumId);
                        //  alert(DepId + '^^^' + DepNameId);
                        IsNumber = false;
                        inlineMsg(EqNumId, "Update did not complete, please enter a positive, non-zero number.", 3);
                        return false;
                    }
                    else {
                        IsNumber = true;
                    }
                    if (DepVal == 0) {
                        IsDep = false;
                        inlineMsg(DepNameId, "Update did not complete, please select a department.", 3);
                        return false;
                    }
                    else {

                        if
                                        (DepText.innerHTML == 'Dept. Conflict') {
                            IsDep = false;
                            inlineMsg(DepNameId, "Update did not complete, please select a department.", 3);
                            return false;
                        }
                        else {
                            IsDep = true;
                        }
                    }

                }
                else {
                    IsDep = true;
                }

            }
        }
    }

    if (IsNumflag && IsDep && IsNumber) {
        return true;
    }
    else {
        return false;
    }


}


function CheckForSEquipmentPageE3(MasterId, Equip, CstType, CstTypeName, EqNum, index) {

    var IsCstType;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }

    //Cheking Dept.
    if (IsNumflag) {
        for (var j = 1; j <= 30; j++) {
            var EqId = MasterId + "_" + Equip + j + "_" + index;
            var CstTypeId = MasterId + "_" + CstType + j + "_" + index;
            var CstTypeNameId = MasterId + "_" + CstTypeName + j + "_" + index;
            var EqNumId = MasterId + "_" + EqNum + j + "_" + index;

            var EqVal = document.getElementById(EqId).value;
            var CstTypeVal = document.getElementById(CstTypeId).value;
            var CstTypeText = document.getElementById(CstTypeNameId);
            var EqpNumVal = document.getElementById(EqNumId).value;




            if (EqVal > 0) {
                //                  
                if (EqpNumVal == 0) {
                    IsNumber = false;
                    inlineMsg(EqNumId, "Update did not complete, please enter a positive, non-zero number.", 3);
                    break;
                }
                else {
                    IsNumber = true;
                }
                if (CstTypeVal == 0) {
                    IsCstType = false;
                    inlineMsg(CstTypeNameId, "Update did not complete, please select a Cost Type.", 3);
                    break;
                }
                else {

                    if
                    (CstTypeText.innerHTML == 'Dept. Conflict') {
                        IsCstType = false;
                        inlineMsg(CstTypeNameId, "Update did not complete, please select a Cost Type.", 3);
                        break;
                    }
                    else {
                        IsCstType = true;
                    }
                }

            }
            else {
                IsCstType = true;
            }

        }
    }

    if (IsNumflag && IsCstType && IsNumber) {
        return true;
    }
    else {
        return false;
    }


}

function CheckForSEquipmentPageE3All(MasterId, Equip, CstType, CstTypeName, EqNum, index) {

    var IsCstType;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }

    //Cheking Dept.
    if (IsNumflag) {

        for (m = 1; m <= index; m++) {
            for (var j = 1; j <= 30; j++) {
                var EqId = MasterId + "_" + Equip + j + "_" + m;
                var CstTypeId = MasterId + "_" + CstType + j + "_" + m;
                var CstTypeNameId = MasterId + "_" + CstTypeName + j + "_" + m;
                var EqNumId = MasterId + "_" + EqNum + j + "_" + m;

                var EqVal = document.getElementById(EqId).value;
                var CstTypeVal = document.getElementById(CstTypeId).value;
                var CstTypeText = document.getElementById(CstTypeNameId);
                var EqpNumVal = document.getElementById(EqNumId).value;




                if (EqVal > 0) {
                    //                  
                    if (EqpNumVal == 0) {
                        IsNumber = false;
                        inlineMsg(EqNumId, "Update did not complete, please enter a positive, non-zero number.", 3);
                        return false;
                    }
                    else {
                        IsNumber = true;
                    }
                    if (CstTypeVal == 0) {
                        IsCstType = false;
                        inlineMsg(CstTypeNameId, "Update did not complete, please select a CostType.", 3);
                        return false;
                    }
                    else {

                        if
                    (CstTypeText.innerHTML == 'Dept. Conflict') {
                            IsCstType = false;
                            inlineMsg(CstTypeNameId, "Update did not complete, please select a CostType.", 3);
                            return false;
                        }
                        else {
                            IsCstType = true;
                        }
                    }

                }
                else {
                    IsCstType = true;
                }

            }
        }

        if (IsNumflag && IsCstType && IsNumber) {
            return true;
        }
        else {
            return false;
        }

    }
}



function CheckForMaterialPageE3(MasterId, Mat, Thick, Dep, DepName, index) {
    var IsNumflag;
    var IsThick;
    var IsDep;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }

    //Non zero thickness check for selected material
    if (IsNumflag) {

        for (var i = 1; i <= 15; i++) {
            var MatId = MasterId + "_" + Mat + i + "_" + index;
            var ThickId = MasterId + "_" + Thick + i + "_" + index;

            var MatVal = document.getElementById(MatId).value;
            var ThickVal = document.getElementById(ThickId).value;

            if (MatVal > 0) {
                if (ThickVal == 0) {
                    IsThick = false;
                    inlineMsg(ThickId, "Update did not complete, please enter a positive, non-zero number.");
                    break;

                }
                else {
                    IsThick = true;
                }

            }
            else {
                if (ThickVal > 0) {
                    IsThick = false;
                    inlineMsg(ThickId, "Update did not complete because no material is selected. Please enter zero.");
                    break;
                }
                else {
                    IsThick = true;
                }
            }
        }

    }

    //Cheking Dept.
    if (IsThick) {
        for (var j = 1; j <= 15; j++) {

            var MatId = MasterId + "_" + Mat + j + "_" + index;
            var DepId = MasterId + "_" + Dep + j + "_" + index;
            var DepNameId = MasterId + "_" + DepName + j + "_" + index;

            var MatVal = document.getElementById(MatId).value;
            var DepVal = document.getElementById(DepId).value;
            var DepText = document.getElementById(DepNameId);

            //alert(DepId);
            if (MatVal > 0) {
                if (DepVal == 0) {
                    IsDep = false;
                    inlineMsg(DepNameId, "Update did not complete, please select a department.");
                    break;
                }
                else {
                    if (DepText.innerHTML == 'Dept. Conflict') {
                        IsDep = false;
                        inlineMsg(DepNameId, "Update did not complete, please select a department.");
                        break;
                    }
                    else {
                        IsDep = true;
                    }
                }

            }
            else {
                IsDep = true;
            }

        }
    }

    if (IsNumflag && IsThick && IsDep) {
        return true;
    }
    else {
        return false;
    }


}


function CheckForMaterialPageE3All(MasterId, Mat, Thick, Dep, DepName, index) {
  

    var IsNumflag;
    var IsThick;
    var IsDep;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }

    //Non zero thickness check for selected material
    if (IsNumflag) {
        for (var m = 1; m <= index; m++) {
            for (var i = 1; i <= 15; i++) {
                var MatId = MasterId + "_" + Mat + i + "_" + m;
                var ThickId = MasterId + "_" + Thick + i + "_" + m;

                var MatVal = document.getElementById(MatId).value;
                var ThickVal = document.getElementById(ThickId).value;

                if (MatVal > 0) {
                    if (ThickVal == 0) {
                        IsThick = false;
                        inlineMsg(ThickId, "Update did not complete, please enter a positive, non-zero number.");
                        return false;

                    }
                    else {
                        IsThick = true;
                    }

                }
                else {
                    if (ThickVal > 0) {
                        IsThick = false;
                        inlineMsg(ThickId, "Update did not complete because no material is selected. Please enter zero.");
                        return false;
                    }
                    else {
                        IsThick = true;
                    }
                }
            }

        }

        //Cheking Dept.
        if (IsThick) { 
            for (b = 1; b <= index; b++) {
                for (var j = 1; j <= 15; j++) {
                    var MatId = MasterId + "_" + Mat + j + "_" + b;
                    var DepId = MasterId + "_" + Dep + j + "_" + b;
                    var DepNameId = MasterId + "_" + DepName + j + "_" + b;
                    var MatVal = document.getElementById(MatId).value;
                    var DepVal = document.getElementById(DepId).value;
                    var DepText = document.getElementById(DepNameId);

                    //alert(DepId);
                    if (MatVal > 0) {

                        if (DepVal == 0) {
                            IsDep = false;
                            inlineMsg(DepNameId, "Update did not complete, please select a department.");
                            return false;
                        }
                        else {
                            if ((DepText.innerHTML == 'Dept. Conflict') && (IsDep = false)) {
                                IsDep = false;
                                inlineMsg(DepNameId, "Update did not complete, please select a department.");
                                return false;
                            }
                            else {
                                IsDep = true;
                            }
                        }


                    }
                    else {
                        IsDep = true;
                    }

                }
            }
        }
    }

    if (IsNumflag && IsThick && IsDep) {
        return true;
    }
    else {
        return false;
    }


}




//function CheckForFixedCostEcon3(MasterId, Txt, Pref, countVal) {
//    //alert(Flag);
//    var IsNumflag;
//    var isCostFlag;
//    //Checking Preferred Value
//    var anum = /(^\d+$)|(^\d+\.\d+$)/

//    for (var i = 1; i <= 30; i++) {
//        var TxtId = MasterId + "_" + Txt + i + "_" + countVal;
//        var PrefId = MasterId + "_" + Pref + i + "_" + countVal;
//        var val = document.getElementById(TxtId).value;
//        var Prefval = document.getElementById(PrefId).value;
//        alert(val);
//        if (val != "") {
//            if (anum.test(val.replace(/,/g, ""))) {
//                IsNumflag = true;
//            }
//            else {
//                IsNumflag = false;
//                inlineMsg(TxtId, "Invalid Number");
//                break;
//            }
//        }
//        else {
//            IsNumflag = true;
//        }

//        if (anum.test(Prefval.replace(/,/g, ""))) {
//            IsNumflag = true;
//        }
//        else {
//            IsNumflag = false;
//            inlineMsg(PrefId, "Invalid Number");
//            break;
//        }
//    }

//    if (IsNumflag) {
//        return true;
//    }
//    else {
//        return false;
//    }
//}
