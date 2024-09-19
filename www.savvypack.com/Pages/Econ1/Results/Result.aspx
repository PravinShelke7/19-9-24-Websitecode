<%@ Page Title="E1-Direct Materials with Depreciation" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="Result.aspx.vb" Inherits="Pages_Econ1_Results_Result" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
  

 <script type="text/JavaScript">
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
     function CheckResultpl(MasterId, Txt1, Txt2) {

         var flag
         var anum = /(^\d+$)|(^\d+\.\d+$)/
         var id = MasterId + "_" + Txt1;
         var val = document.getElementById(id).value
         var idpref = MasterId + "_" + Txt2;
         var valpref = document.getElementById(idpref).value
         var valpref1 = parseInt(valpref);

         // alert(valpref.length);
         if (val != "") {
             if (valpref != "") {
                 if (anum.test(val.replace(/,/g, ""))) {
                     if (anum.test(valpref.replace(/,/g, ""))) {
                         IsNumflag = false;
                         inlineMsg(id, "Only one of price or plant margin percent can be input.");
                     }
                     else {
                         IsNumflag = false;
                         inlineMsg(idpref, "Invalid Number.");
                     }
                 }
                 else {
                     IsNumflag = false;
                     inlineMsg(id, "Invalid Number.");
                 }
             }
             else {

                 if (anum.test(val.replace(/,/g, ""))) {
                     IsNumflag = true;
                 }
                 else {
                     IsNumflag = false;
                     inlineMsg(id, "Invalid Number.");
                 }
             }
         }
         else {
             IsNumflag = true;
             if (anum.test(valpref.replace(/,/g, ""))) {
                 IsNumflag = true;
                 //if (valpref1 < 0 && valpref1 > 99) {
                 if (parseInt(valpref) >= 100) {
                     IsNumflag = false;
                     inlineMsg(idpref, "plant margin percent should be in between 0 to 99.");
                 }
             }
             else {
                 if (valpref != "") {
                     IsNumflag = false;
                     inlineMsg(idpref, "Invalid Number.");
                 }
             }
         }
         if (IsNumflag) {
             return true;
         }
         else {
             return false;
         }

     }
</script>

    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
              <table style="width:782px;">
                <tr>
                    <td>
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td>
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
 <td>
                         <asp:Label id="lblSalesVolume" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
             </table>
             <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />  
             <asp:TextBox ID="txthiddien" Style="visibility:hidden;" runat="server" Text="0"></asp:TextBox>       
         </div>   
     </div>
       <asp:HiddenField ID="hdnVolume" runat="server" />
         <asp:HiddenField ID="hdnUnit" runat="server" />  
</asp:Content>


