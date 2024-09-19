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
//alert(msgwidth);
     var topposition;
    if(msgwidth<200)
    {
      var topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight;
    }
    else
    {
      topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight+9;
    }
    var leftposition = leftPosition(targetdiv) - (msgwidth / 2) + (targetwidth/2) + MSGOFFSET;
    
//    var topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight;
//    var leftposition = leftPosition(targetdiv) - targetwidth/2 + MSGOFFSET;
    msg.style.top = topposition + 'px';
    msg.style.left = leftposition + 'px';
    clearInterval(msg.timer);
    msg.timer = setInterval("fadeMsg(1)", MSGTIMER);
    if (!autohide) {
        autohide = MSGHIDE;
    }
    window.setTimeout("hideMsg()", (autohide * 1000));
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

    //if (anum.test(value.replace(/,/g, ""))) {
    if (IsNumeric(value)) {
        return true;
    }
    else {
             
        return false;
    }
}

function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}

// Checking All text box for numric
function checkNumericAll(div,radio,ids) {

    var txtarray = document.getElementsByTagName("input");
    var flag;
    var anum = /(^\d+$)|(^\d+\.\d+$)/
    var IsScrollItem;
    IsScrollItem = false;
    for (var i = 0; i < txtarray.length; i++) {
        if (txtarray[i].type == "text") {
            var id = txtarray[i].id;
//            if (anum.test(txtarray[i].value.replace(/,/g, ""))) {
            if (IsNumeric(txtarray[i].value.replace(/,/g, ""))) {
                flag = true;
            }
            else {
                
                flag = false;
                if (radio != null) {
                    var idArray = ids.split(",");
                    for (var i = 0; i < idArray.length; i++) {
                     
                        var str = "_" + idArray[i];
                        if (id.search(str) > 0) {
                            IsScrollItem = true
                            radio.checked = true;
                            div.className = "DivWScrolles";
                            inlineMsg(id, "Invalid Number");
                        }
                        

                    }
                   

                }
                if (!IsScrollItem) {
                    inlineMsg(id, "Invalid Number");
                }            
                break;
            }

        }

    }

    return flag;
}




// Checking Material text box for numric
function checkMatNumericAll(RECOP,PCRECP,ExtraP,SGP,div,radio) {

    var txtarray = document.getElementsByTagName("input");
    var flag;
    var anum = /(^\d+$)|(^\d+\.\d+$)/
    for (var i = 0; i < txtarray.length; i++) {
        if (txtarray[i].type == "text") {
            var id = txtarray[i].id;
            //alert(txtarray[i].value);
            //if (anum.test(txtarray[i].value.replace(/,/g, ""))) {
            if (IsNumeric(txtarray[i].value.replace(/,/g, ""))) {
                flag = true;
            }
            else {
                //Checking for Recovery Preferred
                if(id.search('_RECOP')>0)
                {
                  radio.checked=true;
                  div.className = "DivWScrolles";
                }
                //Checking for PC Recycle
                 if(id.search('_PCRECP')>0)
                {
                  radio.checked=true;
                  div.className = "DivWScrolles";
                }
                //Checking Extra Process
                if(id.search('_EP')>0)
                {
                  radio.checked=true;
                  div.className = "DivWScrolles";
                }
                //Checking for Specific Gravity                 
                if(id.search('_SGP')>0)
                {
                  radio.checked=true;
                  div.className = "DivWScrolles";
                }
                //Checking for Shipping Unit
                if(id.search('_SHIPU')>0)
                {
                  radio.checked=true;
                  div.className = "DivWScrolles";
                }
                inlineMsg(id, "Invalid Number");
                flag = false;
                break;
            }

        }

    }

    return flag;
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
function inlineMsgB(target, string, autohide) {
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
    var msgheight = 47;//  msg.offsetHeight;
    var msgwidth = 264; // msg.offsetWidth;
   
    //alert(msg.offsetWidth);
    var targetdiv = document.getElementById(target);
    targetdiv.focus();
    var targetheight = targetdiv.offsetHeight;
    var targetwidth = targetdiv.offsetWidth;
    //alert(topPosition(targetdiv) + ' ' + msgheight + ' ');
    var topposition;
    if (msgwidth < 200) 
    {
        var topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2) - msgheight;
    }
    else 
    {
        topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2) - msgheight + 9;
    }
    // alert(msgwidth+'-'+targetwidth+' '+' '+MSGOFFSET);
   // topposition = topposition - 20;
    var leftposition = leftPosition(targetdiv) - (msgwidth / 2) + (targetwidth / 2) + MSGOFFSET+30;
    //alert(msgwidth + ' ' + topposition);
    //    var topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight;
    //    var leftposition = leftPosition(targetdiv) - targetwidth/2 + MSGOFFSET;
    msg.style.top = topposition + 'px';
    msg.style.left = leftposition + 'px';
    clearInterval(msg.timer);
    msg.timer = setInterval("fadeMsg(1)", MSGTIMER);
    if (!autohide) {
        autohide = MSGHIDE;
    }
    window.setTimeout("hideMsg()", (autohide * 1000));
}

function CheckForMaterialPageBarr(MasterId, Mat, Thick, ShipUnit, ShipSel, ShipSelName, Dep, DepName, divVariable, rdScrollOff,OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {
//  var id =  MasterId + "_" + div;
 
    var IsNumflag;
    var IsThick;
    var IsShipU;
    var IsShipS;    
    var IsDep;
    
    //Getting Id of Div   
     var div= document.getElementById(MasterId + "_divVariable"); 
    //alert(div);
     var radio = document.getElementById(MasterId + "_rdScrollOff");
     //alert(radio);
   
     
      //Numeric Check
        var radbarr = document.getElementById("ctl00_MoldSustain1ContentPlaceHolder_hidBarrier").value;

        if (radbarr == "0") {
            //alert('sud');
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
   
//  alert('IsNumflag'+IsNumflag);
    //Non zero thickness check for selected material
    if (IsNumflag) {

        for (var i = 1; i <= 10; i++) {
            var MatId = MasterId + "_" + Mat + i;
            var ThickId = MasterId + "_" + Thick + i;
            var MatVal = document.getElementById(MatId).value;
            var ThickVal = document.getElementById(ThickId).value;
            if (MatVal > 0) {
                if (ThickVal == 0) {
                    IsThick = false;
                    inlineMsg(ThickId, "Update did not complete,please enter a positive, non-zero number.");
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
     
    //Non zero Shipping Unit check for selected material
    if (IsThick) {

        for (var i = 1; i <= 10; i++) {
            var MatId = MasterId + "_" + Mat + i;
            var ShipUId = MasterId + "_" + ShipUnit + i;
            var MatVal = document.getElementById(MatId).value;
            var ShipUVal = document.getElementById(ShipUId).value;
            if (MatVal > 0) {
            
                if (ShipUVal == 0) {
                    IsShipU = false;                  
                    
                    inlineMsgB(ShipUId, "Update did not complete, please enter a positive, non-zero number.");
                    break;

                }
                else {
                    IsShipU = true;
                }

            }            
        }

    }
   
    //Checking Shipping Selector   
    if (IsShipU) {
        for (var j = 1; j <= 10; j++) {
            var MatId = MasterId + "_" + Mat + j;
            var ShipSId = MasterId + "_" + ShipSel + j;
            var ShipSNameId = MasterId + "_" + ShipSelName + j;
            var MatVal = document.getElementById(MatId).value;
            var ShipSVal = document.getElementById(ShipSId).value;
            //alert(DepId);
            if (MatVal > 0) {
                if (ShipSVal == 0) {
                    IsShipS = false;                   
                    inlineMsgB(ShipSNameId, "Update did not complete, please select a shipping selector.");
                    break;
                }
                else {
                    IsShipS = true;
                }

            }
            else {
                IsShipS = true;
            }

        }
    }
    
    
    //Cheking Dept.
    if (IsShipS) {
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
                    inlineMsg(DepNameId, "Update did not complete, please select a department");
                    break;
                }
                else 
                {
//                    IsDep = true;
                          if(DepText.innerHTML=='Dept. Conflict')
                        {
                           IsDep = false;                          
                           inlineMsg(DepNameId, "Update did not complete, please select a department");
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

    if (IsNumflag && IsThick && IsDep && IsShipU && IsShipS) {
        return true;
    }
    else 
    {
        return false;
    }


}
    function CheckForMaterialPageBarr1(MasterId, Mat, Thick, ShipUnit, ShipSel, ShipSelName, Dep, DepName, divVariable, rdScrollOff,OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {
        //  var id =  MasterId + "_" + div;

        var IsNumflag;
        var IsThick;
        var IsShipU;
        var IsShipS;
        var IsDep;

        //Getting Id of Div   
        var div = document.getElementById(MasterId + "_divVariable");
        //alert(div);
        var radio = document.getElementById(MasterId + "_rdScrollOff");
        // alert(radio);
//alert('sud');

        //Numeric Check
        var radbarr = document.getElementById("ctl00_MoldSustain1ContentPlaceHolder_hidBarrier").value;

        if (radbarr == "0") {
            //alert('sud');
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

        //  alert('IsNumflag'+IsNumflag);
        //Non zero thickness check for selected material
        if (IsNumflag) {

            for (var i = 1; i <= 10; i++) {
                var MatId = MasterId + "_" + Mat + i;
                var ThickId = MasterId + "_" + Thick + i;
                var MatVal = document.getElementById(MatId).value;
                var ThickVal = document.getElementById(ThickId).value;
                if (MatVal > 0) {
                    if (ThickVal == 0) {
                        IsThick = false;
                        inlineMsg(ThickId, "Update did not complete,please enter a positive, non-zero number.");
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

        //Non zero Shipping Unit check for selected material
        if (IsThick) {

            for (var i = 1; i <= 10; i++) {
                var MatId = MasterId + "_" + Mat + i;
                var ShipUId = MasterId + "_" + ShipUnit + i;
                var MatVal = document.getElementById(MatId).value;
                var ShipUVal = document.getElementById(ShipUId).value;
                if (MatVal > 0) {

                    if (ShipUVal == 0) {
                        IsShipU = false;

                        inlineMsgB(ShipUId, "Update did not complete, please enter a positive, non-zero number.");
                        break;

                    }
                    else {
                        IsShipU = true;
                    }

                }
            }

        }

        //Checking Shipping Selector   
        if (IsShipU) {
            for (var j = 1; j <= 10; j++) {
                var MatId = MasterId + "_" + Mat + j;
                var ShipSId = MasterId + "_" + ShipSel + j;
                var ShipSNameId = MasterId + "_" + ShipSelName + j;
                var MatVal = document.getElementById(MatId).value;
                var ShipSVal = document.getElementById(ShipSId).value;
                //alert(DepId);
                if (MatVal > 0) {
                    if (ShipSVal == 0) {
                        IsShipS = false;
                        inlineMsgB(ShipSNameId, "Update did not complete, please select a shipping selector.");
                        break;
                    }
                    else {
                        IsShipS = true;
                    }

                }
                else {
                    IsShipS = true;
                }

            }
        }


        //Cheking Dept.
        if (IsShipS) {
            for (var j = 1; j <= 10; j++) {
                var MatId = MasterId + "_" + Mat + j;
                var DepId = MasterId + "_" + Dep + j;
                var DepNameId = MasterId + "_" + DepName + j;
                var MatVal = document.getElementById(MatId).value;
                var DepVal = document.getElementById(DepId).value;
                var DepText = document.getElementById(DepNameId);
                //alert(DepId);
                if (MatVal > 0) {
                    if (DepVal == 0) {
                        IsDep = false;
                        inlineMsg(DepNameId, "Update did not complete, please select a department");
                        break;
                    }
                    else {
                        //                    IsDep = true;
                        if (DepText.innerHTML == 'Dept. Conflict') {
                            IsDep = false;
                            inlineMsg(DepNameId, "Update did not complete, please select a department");
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

        if (IsNumflag && IsThick && IsDep && IsShipU && IsShipS) {
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
     
        var radbarr = document.getElementById("ctl00_MoldSustain1ContentPlaceHolder_hidBarrier").value;
        //alert(radbarr);

        for (var i = 0; i < txtarray.length; i++) {
            if (txtarray[i].type == "text") {
                var id = txtarray[i].id;
                var OTRID = MasterId + "_OTR" + k;
                var WVTRID = MasterId + "_WVTR" + k1;
              
                // alert(id);
                if (txtarray[i].value.match(/\S/)) {

                    if (IsNumeric(txtarray[i].value.replace(/,/g, ""))) {
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
                    
                    }
                    else {

                        inlineMsg(id, "Invalid Number");
                        flag = false;
                        break;
                    }

                }
                else {
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
                   
                    else {
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


            }

        }
        //alert(flag);
        return flag;


    }





    function checkNumeric2AllWithBarr(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {

        var txtarray = document.getElementsByTagName("input");
        var flag;
        var anum = /(^\d+$)|(^\d+\.\d+$)/
        var k = 1;
        var k1 = 1;
      
        var radbarr = document.getElementById("ctl00_MoldSustain1ContentPlaceHolder_hidBarrier").value;
          
        for (var i = 0; i < txtarray.length; i++) {
            if (txtarray[i].type == "text") {
                var id = txtarray[i].id;
                var OTRID = MasterId + "_OTR" + k;
                var WVTRID = MasterId + "_WVTR" + k1;
               
                // alert(id);
                if (txtarray[i].value.match(/\S/)) {

                    if (IsNumeric(txtarray[i].value.replace(/,/g, ""))) {
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
                      
                    }
                    else {
                        // alert('sud');
                        inlineMsg(id, "Invalid Number");
                        //   alert('sud2');
                        flag = false;
                        break;
                    }

                }
                else {
                    //alert(id + '---' + txtarray[i].value.match(/\S/) + '---' + flag);

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
                   

                    else {
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


            }

        }
        //alert(flag);
        if (flag) {
            if (CheckForBarrierPageNew(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) == true) {
                return flag;

            }
            else {
                return false;
            }
        }
        else {
            return false;
        }


    }

    function CheckForBarrierPageNew(MasterId, OTRTEMP, RH, TEMPVAL1, TEMPVAL2, RHVAL) {
        var IsNumflag = true;
        var IsTemp;
        var IsTemp1;
        var IsRH;
        var IsRH2;

        if (IsNumflag) {
            IsTemp = true;
          //  alert(IsTemp+'sud1');

            var UID = MasterId + "_UnitId";
            // alert(UID);
             //alert(document.getElementById(UID).value);
            var UIDVAL = document.getElementById(UID).value;
            // alert(UIDVAL);

            var OTRTEMPL = MasterId + "_" + OTRTEMP + "1";
            var OTRTEMPValL = document.getElementById(OTRTEMPL).value;
            var OTRTEMPH = MasterId + "_" + OTRTEMP + "2";
            var OTRTEMPValH = document.getElementById(OTRTEMPH).value;
        //    alert(OTRTEMPValL + '-' + OTRTEMPValH);

            var OTRT = MasterId + "_" + TEMPVAL1;
            var OTRTVal = document.getElementById(OTRT).value;

            if (eval(OTRTVal) < eval(OTRTEMPValL)) {
                IsTemp = false;
            }
            else if (eval(OTRTVal) > eval(OTRTEMPValH)) {
                IsTemp = false;
            }
            if (!IsTemp) {
                istemp = false;
                if (UIDVAL == "0") {
                    inlineMsgBarrier("ctl00_MoldSustain1ContentPlaceHolder_txtOTRTemp", "Please enter a value between 32 and 122 Fahrenheit.");
                }
                else {
                    inlineMsgBarrier("ctl00_MoldSustain1ContentPlaceHolder_txtOTRTemp", "Please enter a value between 0 and 50 celsius");
                }
            }


        }

        if (IsTemp) {
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
            if (!IsTemp1) {
                istemp1 = false;

                if (UIDVAL == "0") {
                    inlineMsgBarrier("ctl00_MoldSustain1ContentPlaceHolder_txtWVTRTemp", "Please enter a value between 32 and 122 Fahrenheit.");
                }
                else {
                    inlineMsgBarrier("ctl00_MoldSustain1ContentPlaceHolder_txtWVTRTemp", "Please enter a value between 0 and 50 celsius");
                }
                //inlineMsgBarrier("ctl00_Sustain1ContentPlaceHolder_txtWVTRTemp", "Update did not complete.Please enter proper Temprature.");

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
            if (!IsRH) {
                IsRH = false;
                // var OTRV = OTRT.replace(/^\s+|\s+$/g, '');
                //  alert(OTRT);
                //inlineMsg(OTRT, "Update did not complete.Please enter proper Temprature.");
                inlineMsgBarrier("ctl00_MoldSustain1ContentPlaceHolder_txtOTRHumidity", "Please enter a value between 0% and 100%.");

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

                inlineMsgBarrier("ctl00_MoldSustain1ContentPlaceHolder_txtWVTRHumidity", "Please enter a value between 90% and 100%.");

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