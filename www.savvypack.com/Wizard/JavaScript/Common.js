// build out the divs, set attributes and call the fade function //
var MSGTIMER = 5;
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
    arrow.src = "../Images/msg_arrow.gif";    
}


// Checking individual text box for numric
function checkNumeric(id) {

    var txtarray = document.getElementsByTagName("input");
    var flag;
    var anum = /(^\d+$)|(^\d+\.\d+$)/

    var id = id;
    //alert(txtarray[i].value);
    // if (anum.test(txtarray[i].value.replace(/,/g, "")))
    if (IsNumeric(document.getElementById(id).value.replace(/,/g, ""))) {

        flag = true;
    }
    else {
        inlineMsg(id, "Invalid Number");
        flag = false;
    }


    return flag;
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



