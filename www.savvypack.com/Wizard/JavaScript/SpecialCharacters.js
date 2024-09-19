
function CheckSP() {
    var txtarray = document.getElementsByTagName("input");
    var flag;

    for (var i = 0; i < txtarray.length; i++) {
        if (txtarray[i].type == "text" || txtarray[i].type == "password") {
            var id = txtarray[i].id;

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(id)//get your object

            if ((document.getElementById(id).value.match(a) != null)) {

                object.focus(); //set focus to prevent jumping
                object.value = txtarray[i].value.replace(new RegExp("<", 'g'), "");
                object.value = txtarray[i].value.replace(new RegExp(">", 'g'), "");
                object.value = txtarray[i].value.replace(/\\/g, '');
                object.value = txtarray[i].value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping


                //inlineMsg1(id, "Invalid Number.");
                //alert( "You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' in Textbox. Please choose alternative characters.");
                alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                flag = false;
                break;
            }
            else {
                flag = true;
            }

        }

    }

}

function CheckSPMul(MaxLen) {
    var txtMularray = document.getElementsByTagName("textarea");
    var flag;
    
    for (var i = 0; i < txtMularray.length; i++) {
        if (txtMularray[i].type == "textarea") {
            var id = txtMularray[i].id;

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(id)//get your object
            var maxlength = MaxLen; //set your value here (or add a parm and pass it in)      

            if ((document.getElementById(id).value.match(a) != null) || (object.value.length > maxlength)) {

                if ((document.getElementById(id).value.match(a) != null)) {
                    object.focus(); //set focus to prevent jumping
                    object.value = txtMularray[i].value.replace(new RegExp("<", 'g'), "");
                    object.value = txtMularray[i].value.replace(new RegExp(">", 'g'), "");
                    object.value = txtMularray[i].value.replace(/\\/g, '');
                    object.value = txtMularray[i].value.replace(new RegExp("&#", 'g'), "");
                    object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                    //alert("You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' in Textbox. Please choose alternative characters.");
                    alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                }

                if (object.value.length > maxlength) {
                    object.focus(); //set focus to prevent jumping
                    object.value = object.value.substring(0, maxlength); //truncate the value
                    object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping                    
                }
                flag = false;
                break;
            }
            else {
                flag = true;
            }

        }

    }

}

function CheckDesc(id, type) {
    var flag;

    var a = /\<|\>|\&#|\\/;
    var object = document.getElementById(id)//get your object

    if ((document.getElementById(id).value.match(a) != null)) {

        object.focus(); //set focus to prevent jumping
        object.value = object.value.replace(new RegExp("<", 'g'), "");
        object.value = object.value.replace(new RegExp(">", 'g'), "");
        object.value = object.value.replace(/\\/g, '');
        object.value = object.value.replace(new RegExp("&#", 'g'), "");
        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping


        //inlineMsg1(id, "Invalid Number.");
       alert("You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' in Textbox. Please choose alternative characters.");
        flag = false;

    }
    else {
        flag = true;
    }



}

var MSGTIMER = 20;
var MSGSPEED = 5;
var MSGOFFSET = 3;
var MSGHIDE = 5;
var msg;
function inlineMsg1(target, Type, string, autohide) {
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
    if (Type == 'Text') {
        var targetheight = targetdiv.offsetHeight + 20;
    }
    else if (Type == 'Text1') {
        var targetheight = targetdiv.offsetHeight - 5;
    }
    else {
        var targetheight = targetdiv.offsetHeight - 65;
    }

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


















