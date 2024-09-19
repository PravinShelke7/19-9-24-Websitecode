﻿// build out the divs, set attributes and call the fade function //
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
    if(msgwidth<200)
    {
       topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight;
    }
    else
    {
      topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight+9;
    }
    var leftposition = leftPosition(targetdiv) - (msgwidth / 2) + (targetwidth/2) + MSGOFFSET;
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


//Barrier
function CheckForMaterialPageBarrierNew(MasterId, Mat, Thick, Dep, DepName, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {
    var IsNumflag;
    var IsThick;
    var IsDep;
    //Numeric Check
     var radbarr = document.getElementById("ctl00_Econ1ContentPlaceHolder_hidBarrier").value;
     //alert(radbarr);
    if (radbarr == "0") {
        if (checkNumeric2AllWOBarr(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL)) {
            IsNumflag = true;
        }
        else {
            IsNumflag = false;
        }
    }
    else {
        if (checkNumeric2AllWithBarr(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL)) {
            IsNumflag = true;
        }
        else {
            IsNumflag = false;
        }
    }

    //Non zero thickness check for selected material
    if (IsNumflag) {

        for (var i = 1; i <= 10; i++) {
            var MatId = MasterId + "_" + Mat + i;
            var ThickId = MasterId + "_" + Thick + i;
            var MatVal = document.getElementById(MatId).value;
            var ThickVal = document.getElementById(ThickId).value;
           // alert(MatVal);
            if (MatVal > 0) {
                if (ThickVal == "") {
                    IsThick = true;
                    
//                    inlineMsg(ThickId, "Update did not complete, please enter a positive, non-zero number.");
//                    break;

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
        for (var j = 1; j <= 10; j++) {
            var MatId = MasterId + "_" + Mat + j;
            var DepId = MasterId + "_" + Dep + j;
            var DepNameId = MasterId + "_" + DepName + j;
            var MatVal = document.getElementById(MatId).value;
            var DepVal = document.getElementById(DepId).value;
            var DepText=document.getElementById(DepNameId);
            
            //alert(DepId);
            if (MatVal > 0) {
                if (DepVal == 0) {
                    IsDep = false;
                    inlineMsg(DepNameId, "Update did not complete, please select a department.");
                    break;
                }
                else 
                {
                    if(DepText.innerHTML=='Dept. Conflict')
                    {
                       IsDep = false;
                       inlineMsg(DepNameId, "Update did not complete, please select a department.");
                       break;
                    }
                    else
                    {
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

function checkNumeric2AllWOBarr(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {

    var txtarray = document.getElementsByTagName("input");
    var flag;
    var anum = /(^\d+$)|(^\d+\.\d+$)/
    var k = 1;
    var k1 = 1;
    var k2 = 1;
    var k3 = 1;
     //alert('Sud2.0');
    var radbarr = document.getElementById("ctl00_Econ1ContentPlaceHolder_hidBarrier").value;
  
    for (var i = 0; i < txtarray.length; i++) 
    {

        if (txtarray[i].type == "text") {
            //alert(txtarray[i].value + "," + i);
            var id = txtarray[i].id;
            var OTRID = MasterId + "_OTR" + k;
            var WVTRID = MasterId + "_WVTR" + k1;
            var PRICEID = MasterId + "_P" + k2;
            var THICKNESSID = MasterId + "_T" + k3;

            var THICKNESS = document.getElementById(THICKNESSID).value;
            var PRICE = document.getElementById(PRICEID).value;
            // alert(id);0
            //alert(THICKNESS);
            if (txtarray[i].value.match(/\S/)) 
            {

                if (anum.test(txtarray[i].value.replace(/,/g, ""))) 
                    {
                        flag = true;
                        if (OTRID == id)//check for Preferred Price Textboxes
                        {
                            k = k + 1;
                            flag = true;
                        }
                        else if (WVTRID == id)//check for Preferred Price Textboxes
                        {
                            k1 = k1 + 1;
                            flag = true;
                        }
//                        else if (PRICEID == id)//check for Preferred Price Textboxes
//                        {
//                            k2 = k2 + 1;
//                            flag = true;
//                        }
                    }
                    else {

                        inlineMsg(id, "Invalid Number");
                        flag = false;
                        break;
                    }
                
            }
            else 
            {
               // alert(id + '---' + txtarray[i].value.match(/\S/) + '---' + flag);
              
                if (OTRID == id)//check for Preferred Price Textboxes
                {
                    //alert(OTRID);
                    k = k + 1;
                    flag = true;
                }
                else if (WVTRID == id)//check for Preferred Price Textboxes
                {
                    //alert(WVTRID);
                    k1 = k1 + 1;
                    flag = true;
                }
//                else if (PRICEID == id)//check for Preferred Price Textboxes
//                {
//                   // alert(PRICEID);
//                    k2 = k2 + 1;
//                    flag = true;
//                }

                else 
                {
                    if (anum.test(txtarray[i].value.replace(/,/g, ""))) {
                        flag = true;
                    }
                    else {
                    
                        if (PRICE == "") {
                            flag = true;
                        }
                        else if (THICKNESS == "") {
                           flag = true;
                        }
                        else {
                            inlineMsg(id, "Invalid Number");
                            flag = false;
                            break;
                        }

                    }
                }

            }


        }

    }
       
        return flag;

    }
    
    function checkNumeric2AllWithBarr(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {

    var txtarray = document.getElementsByTagName("input");
    var flag;
    var anum = /(^\d+$)|(^\d+\.\d+$)/
    var k = 1;
    var k1 = 1;
    var k2 = 1;
    var k3 = 1;
    var radbarr = document.getElementById("ctl00_Econ1ContentPlaceHolder_hidBarrier").value;
  // alert('Sud3.0');

    for (var i = 0; i < txtarray.length; i++) {
        if (txtarray[i].type == "text") {
            var id = txtarray[i].id;
            var OTRID = MasterId + "_OTR" + k;
            var WVTRID = MasterId + "_WVTR" + k1;
            var PRICEID = MasterId + "_P" + k2;
            var THICKNESSID = MasterId + "_T" + k3;

            var PRICE = document.getElementById(PRICEID).value;
            var THICKNESS = document.getElementById(THICKNESSID).value;
             //alert(id);
            if (txtarray[i].value.match(/\S/)) 
            {

                if (anum.test(txtarray[i].value.replace(/,/g, ""))) {
                    flag = true;
                    if (OTRID == id)//check for Preferred Price Textboxes
                    {
                        k = k + 1;
                        flag = true;
                    }
                    else if (WVTRID == id)//check for Preferred Price Textboxes
                    {
                        k1 = k1 + 1;
                        flag = true;
                    }
//                    else if (PRICEID == id)//check for Preferred Price Textboxes
//                    {
//                        k2 = k2 + 1;
//                        flag = true;
//                    }
                }
                else {
                    inlineMsg(id, "Invalid Number");
                    flag = false;
                    break;
                }

            }
            else 
            {
               // alert(id + '---' + txtarray[i].value.match(/\S/) + '---' + flag);

                if (OTRID == id)//check for Preferred Price Textboxes
                {
                    k = k + 1;
                    flag = true;
                }
                else if (WVTRID == id)//check for Preferred Price Textboxes
                {
                    k1 = k1 + 1;
                    flag = true;
                }
//                else if (PRICEID == id)//check for Preferred Price Textboxes
//                {
//                    k2 = k2 + 1;
//                    flag = true;
//                }

                else {
                    if (anum.test(txtarray[i].value.replace(/,/g, ""))) {
                        flag = true;
                    }
                    else {

                        if (PRICE == "") {
                            flag = true;
                        }
                        else if (THICKNESS == "") {
                            flag = true;
                        }
                        else {
                            inlineMsg(id, "Invalid Number");
                            flag = false;
                            break;
                        }

                    }
                }

            }


        }

    }
  //  alert(flag);
 if (flag) 
    {
    if (CheckForBarrierPageNew(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) == true) {
        return flag;

    }
    else {
        return false;
    }
 }
    else 
    {
        return false;
    }

}   

function CheckForBarrierPageNew(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {
    var IsNumflag = true;
    var IsTemp;
    var IsTemp1;
    var IsRH;
    var IsRH2;

    if (IsNumflag) 
    {
        IsTemp = true;
        //alert(IsTemp+'sud1');

        var UID = MasterId + "_UnitId";
        // alert(UID);
       // alert(document.getElementById(UID).value);
        var UIDVAL =  document.getElementById(UID).value;
        // alert(UIDVAL);

        var OTRTEMPL = MasterId + "_" + OTRTEMP + "1";
        //alert(OTRTEMPL);
        var OTRTEMPValL = document.getElementById(OTRTEMPL).value;
        var OTRTEMPH = MasterId + "_" + OTRTEMP + "2";
        var OTRTEMPValH = document.getElementById(OTRTEMPH).value;


        var OTRT = MasterId + "_" + TEMPVAL1;
        var OTRTVal = document.getElementById(OTRT).value;



        if (eval(OTRTVal) < eval(OTRTEMPValL)) {
            IsTemp = false;
        }
        else if (eval(OTRTVal) > eval(OTRTEMPValH)) {
            IsTemp = false;
        }
//alert(IsTemp);
        if (!IsTemp) 
        {
            istemp = false;
            if (UIDVAL == "0") 
            {
                //alert('wwwwww');
                inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtOTRTemp", "Please enter a value between 32 and 122 Fahrenheit.");
            }
            else 
            {
                inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtOTRTemp", "Please enter a value between 0 and 50 celsius");
            }
        }
      

    }

    if (IsTemp) 
    {
        IsTemp1 = true;

        var OTRTEMPL = MasterId + "_" + OTRTEMP + "1";
        var OTRTEMPValL = document.getElementById(OTRTEMPL).value;
        var OTRTEMPH = MasterId + "_" + OTRTEMP + "2";
        var OTRTEMPValH = document.getElementById(OTRTEMPH).value;


        var WVTRT = MasterId + "_" + TEMPVAL2;
        var WVTRTVal = document.getElementById(WVTRT).value;

        if (eval(WVTRTVal) < eval(OTRTEMPValL)) {
            IsTemp1 = false;
        }
        else if (eval(WVTRTVal) > eval(OTRTEMPValH)) {
            IsTemp1 = false;
        }
        if (!IsTemp1) 
        {
            istemp1 = false;

            if (UIDVAL == "0") {
                inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtWVTRTemp", "Please enter a value between 32 and 122 Fahrenheit.");
            }
            else {
                inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtWVTRTemp", "Please enter a value between 0 and 50 celsius");
            }
            //inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtWVTRTemp", "Update did not complete.Please enter proper Temprature.");

        }
       
    }
    //    alert('ok');
    if (IsTemp1) {
        IsRH = true;
        //alert(IsTemp+'sud2');

        var RHL = MasterId + "_" + RH + "1";
        var RHValL = document.getElementById(RHL).value;


        var RHH = MasterId + "_" + RH + "2";
        var RHValH = document.getElementById(RHH).value;
        //alert(RHValH);

        var OTRRH = MasterId + "_" + RHVAL;
        var RHVal = document.getElementById(OTRRH).value;
        //alert(RHVal);

        if (eval(RHVal) < eval(RHValL)) {
            IsRH = false;
        }
        else if (eval(RHVal) > eval(RHValH)) {
            IsRH = false;
        }
        //alert(IsRH);
        if (!IsRH) 
        {
            IsRH = false;
            // var OTRV = OTRT.replace(/^\s+|\s+$/g, '');
            //  alert(OTRT);
            //inlineMsg(OTRT, "Update did not complete.Please enter proper Temprature.");
            inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtOTRHumidity", "Please enter a value between 0% and 100%.");

        }

    }
  
    if (IsRH) {
        IsRH2 = true;
        //alert(IsTemp+'sud2');

        var RHL = MasterId + "_" + RH + "1";
        var RHValL = document.getElementById(RHL).value;


        var RHH = MasterId + "_" + RH + "2";
        var RHValH = document.getElementById(RHH).value;
        //alert(RHValH);

        var WVTRRH = MasterId + "_txtWVTRHumidity";
        var RHVal = document.getElementById(WVTRRH).value;
        //alert(RHVal);

        //        if (eval(RHVal) != 100) {
        //            IsRH2 = false;
        //           
        //        }

        if (eval(RHVal) < 90) {
            IsRH2 = false;

        }
        else if (eval(RHVal) > 100) {
            IsRH2 = false;

        }

        if (!IsRH2) {
            IsRH2 = false;

            inlineMsgBarrier("ctl00_Econ1ContentPlaceHolder_txtWVTRHumidity", "Please enter a value between 90% and 100%.");

        }

    }

    //alert(IsNumflag + '-' + IsTemp + '-' + IsRH2 + '-' + IsRH);
    if (IsNumflag && IsTemp && IsRH & IsRH2) {
        
        return true;
    }
    else {
       
        return false;
    }
}
function inlineMsgBarrier(target, string, autohide) {
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
     //alert(targetdiv+'sud');
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
    window.setTimeout("hideMsg()", (autohide * 500));
}