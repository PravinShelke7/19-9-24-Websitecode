<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateGroup.aspx.vb" Inherits="Pages_Market1Sub_PopUp_CreateGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Create Group</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });               
    </script>
    <script language="JavaScript" type="text/javascript">
        var isClose = false;
        document.onkeydown = checkKeycode
        function checkKeycode(e) {
            var keycode;
            if (window.event)
                keycode = window.event.keyCode;
            else if (e)
                keycode = e.which;
            if (keycode == 116) {
                isClose = true;
            }
        }
        function mousefunction() {
            isClose = true;
        }
        function doUnload() {
            if (!isClose) {

                if (document.getElementById("hdnUpdate").value == "1") {
                    window.opener.document.getElementById('btnRefresh').click();
                }

            }
        }
    </script>
    <script language="JavaScript" type="text/javascript">
        //        function CheckSP(text) {

        //            var a = /\<|\>|\&#|\\/;
        //            if ((document.getElementById("txtGDES1").value.match(a) != null) || (document.getElementById("txtGDES2").value.match(a) != null)) {
        //                var object = document.getElementById(text.id)  //get your object
        //                if (text.id == "txtGDES1") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Group Descriptor1. Please choose alternative characters.");
        //                }
        //                else if (text.id == "txtGDES2") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Group Descriptor2. Please choose alternative characters.");
        //                }
        //                object.focus(); //set focus to prevent jumping
        //                object.value = text.value.replace(new RegExp("<", 'g'), "");
        //                object.value = text.value.replace(new RegExp(">", 'g'), "");
        //                object.value = text.value.replace(/\\/g, '');
        //                object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //                return false;
        //            }
        //        }

        function Message() {

            if (document.getElementById("txtGDES1").value.split(' ').join('').length == 0) {
                alert("Group Descriptor1 cannot be blank");
                return false;
            }
            else if (document.getElementById("txtGDES2").value.split(' ').join('').length == 0) {
                alert("Group Descriptor2 cannot be blank");
                return false;
            }
            else {
                msg = "You are going to create a new Group. Do you want to proceed?"

            }

            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }


        }

        function Close(grdid) {
            if (grdid != "0") {
                alert("Group #" + grdid + " created successfully")
            }
            if (document.getElementById("hdnUpdate").value == "1") {
                window.opener.document.getElementById('btnRefresh').click();
            }
            window.close();
        }

    </script>
    <style type="text/css">
        .style1
        {
            font-size: 12px;
            font-weight: bold;
            width: 419px;
        }
    </style>
</head>
<body onmousedown="mousefunction()" onbeforeunload="doUnload()">
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0; height: 205px;">
        <div id="PageSection1" style="text-align: left; width: 470px; height: 233px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table style="text-align: center;">
                <tr>
                    <td class="style1" style="font-size: 16px; text-align: center; margin-left: 100px;">
                        Create Group
                    </td>
                </tr>
            </table>
            <div style="margin-top: 10px; margin-left: 20px;" id="divGModify" runat="server"
                visible="true">
                <table style="width: 429px; height: 125px">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                            <asp:Label ID="lblTitle" runat="server" Text="Create Proprietary Group"></asp:Label>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td align="right">
                            Group Descriptor1:
                        </td>
                        <td>
                            <asp:TextBox ID="txtGDES1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                width: 230px" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td align="right">
                            Group Descriptor2:
                        </td>
                        <td>
                            <asp:TextBox ID="txtGDES2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                width: 230px" MaxLength="25"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td>
                        </td>
                        <td>
                            <asp:Button ID="btnGCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                OnClientClick="return Message();" onmouseover="Tip('Create new Group')" onmouseout="UnTip()" />
                            <asp:Button ID="btnGCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidReportId" runat="server" />
        <asp:HiddenField ID="hidReportDes" runat="server" />
        <asp:HiddenField ID="hidReportDes1" runat="server" />
        <asp:HiddenField ID="hvReportGrd" runat="server" />
        <asp:HiddenField ID="hdnUpdate" runat="server" />
        <asp:HiddenField ID="hdnGrpID" runat="server" />
    </div>
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
