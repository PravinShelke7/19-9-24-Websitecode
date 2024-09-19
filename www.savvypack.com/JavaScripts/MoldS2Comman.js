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
    if(msgwidth<200)
    {
      var topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight;
    }
    else
    {
      topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2)-msgheight+9;
    }
    var leftposition = leftPosition(targetdiv) - (msgwidth / 2) + (targetwidth/2) + MSGOFFSET;
//    var topposition = topPosition(targetdiv) - ((msgheight - targetheight) / 2);
//    var leftposition = leftPosition(targetdiv) + targetwidth + MSGOFFSET;
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


function IsNumeric(input) {
    return (input - 0) == input && input.length > 0;
}


// Checking individual text box for numric

function checkNumeric(value, id) {

    var anum = /(^\d+$)|(^\d+\.\d+$)/

//    if (anum.test(value.replace(/,/g, ""))) {
    if (IsNumeric(value.replace(/,/g, ""))) {
        return true;
    }
    else {
             
        return false;
    }
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


function CheckForMaterialPage(MasterId, Mat,Weight,IC,S1Case,S1CaseName ,Dep, DepName) {

    var IsNumflag;   
    var IsDep;
    var IsWeight;
    var IsS1Case;
    //Numeric Check
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }   

    //Checking S1 Cases
     if (IsNumflag) {
        for (var j = 1; j <= 10; j++) {
            var MatId = MasterId + "_" + Mat + j;
            var PCompId = MasterId + "_" + IC + j;
            
            var S1CaseId = MasterId + "_" + S1Case + j;
            var S1CaseNameId = MasterId + "_" + S1CaseName + j;
            var MatVal = document.getElementById(MatId).value;
            var S1CaseVal = document.getElementById(S1CaseId).value;
            //alert(S1CaseVal);
            if (MatVal > 0) 
            {
               //alert(PCompId);
              if(document.getElementById(PCompId).checked)
              {
              // if(PCompId.checked=true)
                if (S1CaseVal == 0) 
                {
                    IsS1Case = false;
                    inlineMsg(S1CaseNameId, "Update did not complete, please select a Sustain1 Case.");
                    break;
                }
                else 
                {
                    IsS1Case = true;
                }
               }

            }
            else {
                IsS1Case = true;
            }

        }
    }
   //Non zero Weight check for selected material
    if (IsS1Case) {

        for (var i = 1; i <= 10; i++) {
            var MatId = MasterId + "_" + Mat + i;
            var WeightId = MasterId + "_" + Weight + i;
            var MatVal = document.getElementById(MatId).value;
            var WeightVal = document.getElementById(WeightId).value;
            if (MatVal > 0)
             {

                 IsWeight = true;
//                if (WeightVal == 0) {
//                    IsWeight = false;
//                    inlineMsg(WeightId, "Update did not complete, please enter a positive, non-zero number.");
//                    break;

//                }
//                else {
//                    IsWeight = true;
//                }

            } 
             else {
                if (WeightVal > 0) {
                    IsWeight = false;
                    inlineMsg(WeightId, "Update did not complete because no material is selected. Please enter zero.");
                    break;
                }
                else {
                    IsWeight = true;
                }
            } 
                      
        }

    }
    
    //Cheking Dept.
    if (IsWeight) {
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

    if (IsNumflag && IsDep && IsWeight) {
        return true;
    }
    else {
        return false;
    }


}


function CheckForPalletIn(MasterId, Pat, Num,NOFUSE) {
    var IsNumflag;
    var IsNumber;
    var IsNumUsesflag;
    
   
    //Getting Id of Div   
    var div = document.getElementById("divVariable");
    var radio = document.getElementById("rdScrollOff");
    var ids = "PCRECP,SDP,hypPalletDep";

    //Numeric Check
    if (checkNumericAll(div, radio, ids)) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }

        if (IsNumflag) 
        {
           
            for (var j = 1; j <= 10; j++) 
            {
                var PatId = MasterId + "_" + Pat + j;
                var NumId = MasterId + "_" + Num + j;

                var PatVal = document.getElementById(PatId).value;
                var NumVal = document.getElementById(NumId).value;
                //alert(PatVal + ' '+ NumVal);
                if (PatVal > 0) 
                {
                    if (NumVal == 0) {
                        IsNumber = false;
                        inlineMsg(NumId, "Update did not complete, please enter a positive, non-zero number.");
                        break;
                    }
                    else {
                        IsNumber = true;
                    }
                }
                //
                else 
                {
                    if (NumVal > 0) 
                    {
                        IsNumber = false;
                        inlineMsg(NumId, "Update did not complete because no pallet is selected. Please enter zero.");
                        break;
                    }
                    else 
                    {
                        IsNumber = true;
                    }
                }               
                
            }

        }
      
        if(IsNumber)
        {
                for (var i = 1; i <= 10; i++) 
                {
                    var NUsesId = MasterId + "_" + NOFUSE + i;
                    var  NUsesVal = document.getElementById(NUsesId).value;
                   
                     if (NUsesVal == 0) 
                     {
                            IsNumUsesflag = false;
                            inlineMsg(NUsesId, "Update did not complete, please enter a positive, non-zero number.");
                            break;
                     }
                     else
                     {
                       IsNumUsesflag = true;
                     }
                }
        }  
  
        if (IsNumflag && IsNumber && IsNumUsesflag) {
            return true;
        }
        else {
            return false;
        }


}


function CheckForEquipmentIn(MasterId, Equip, Dept,DeptText, EqNum) {
    var IsNumflag;
    var IsDep;
      var IsNumber;
//alert(Equip + "  "+Dept+" "+DeptText);
    if (checkNumericAll()) {
        IsNumflag = true;
    }
    else {
        IsNumflag = false;

    }
    

    if (IsNumflag) {
        for (var j = 1; j <= 30; j++) {
            var EquipId = MasterId + "_" + Equip + j;
            var DeptId = MasterId + "_" + Dept + j;
            var DeptTextId = MasterId + "_" + DeptText + j;
            var EqNumId = MasterId + "_" + EqNum + j;
             
            var EquipVal = document.getElementById(EquipId).value;
            var DeptVal = document.getElementById(DeptId).value;
            var DepText = document.getElementById(DeptTextId);
            var EqpNumVal = document.getElementById(EqNumId).value;
            
            if (EquipVal > 0) 
            {
               if (EqpNumVal == 0) 
               {
                    IsNumber = false;
                    inlineMsg(EqNumId, "Update did not complete, please enter a positive, non-zero number.");
                    break;
                }
                else {
                    IsNumber = true;
                }
                if (DeptVal == 0) 
                {
                    IsDep = false;
                    inlineMsg(DeptTextId, "Update did not complete, please select a department.");
                    break;
                }
                else 
                {
                    if (DepText.innerHTML == 'Dept. Conflict') 
                    {
                        IsDep = false;
                        inlineMsg(DeptTextId, "Update did not complete, please select a department.");
                        break;
                    }
                    else 
                    {
                        IsDep = true;
                    }
                }
            }
            else
            {
              IsDep = true;
            }
        }

    }
    if (IsNumflag && IsDep  && IsNumber) {
        return true;
        }
        else {
            return false;
        }

    }

function CheckForSEquipmentPage(MasterId, Equip, EqNum) 
    {
      
        var IsNumflag
        var IsNumber;
        if (checkNumericAll()) {
            IsNumflag = true;
        }
        else 
        {
            IsNumflag = false;
        }

        if (IsNumflag) 
        {
            for (var j = 1; j <= 30; j++) {

                var EqId = MasterId + "_" + Equip + j;
                var EqNumId = MasterId + "_" + EqNum + j;

                var EqVal = document.getElementById(EqId).value;
                var EqpNumVal = document.getElementById(EqNumId).value;

                if (EqVal > 0) 
                {

                    if (EqpNumVal == 0) 
                    {
                        IsNumber = false;
                        inlineMsg(EqNumId, "Update did not complete, please enter a positive, non-zero number.");
                        break;
                    }
                    else 
                    {
                        IsNumber = true;
                    }                    
                }
               
            }
        }

        if (IsNumflag && IsNumber) {
            return true;
        }
        else {
            return false;
        }
    }