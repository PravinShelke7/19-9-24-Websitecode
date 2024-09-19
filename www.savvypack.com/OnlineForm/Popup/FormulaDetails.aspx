﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FormulaDetails.aspx.vb"
    Inherits="Pages_PopUp_FormulaDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Formula Details</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">    

        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            var key;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                            var ControlID = $(this).attr('id');
                            var ControlValue = $(this).val();

                            if (ControlID == "txtName") {
                                key = 'Formula Name';
                            }
                            else if (ControlID == "txtSize") {
                                key = 'Formula Size';
                            }
                            else if (ControlID.search("txtMat") == 0) {
                                var matches = ControlID.match(/(\d+)/);
                                key = "Product & Packages " + matches[0];
                            }
                            else if (ControlID.search("txtVol") == 0) {
                                var matches = ControlID.match(/(\d+)/);
                                key = "Volume or Size " + matches[0];
                            }
                            else if (ControlID.search("txtPrice") == 0) {
                                var matches = ControlID.match(/(\d+)/);
                                key = "Price " + matches[0];
                            }
                            
                            PageMethods.UpdateCase(key, ControlValue, onSucceed, onError);

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
                            var ControlID = $(this).attr('id');
                            var ControlValue = $(this).val();

                            var matches = ControlID.match(/(\d+)/);
                            key = "Additional Information " + matches[0];
                            
                            PageMethods.UpdateCase(key, ControlValue, onSucceed, onError);
                        });
                    }
                }
            }

            function onSucceed(result) {

            }

            function onError(result) {

            }
        });

        //function Count(text) {
        //    var key;
        //    var str = String(text.id);
        //    var a = /\<|\>|\&#|\\/;
        //    if ((document.getElementById(text.id).value.match(a) != null)) {
        //        alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Additional Information. Please choose alternative characters.");

        //        var object = document.getElementById(text.id)  //get your object
        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        alert(str);
        //        var matches = str.match(/(\d+)/);
        //        key = "Additional Information " + matches[0];
        //        alert(key);
        //        PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //        return false;
        //    }
        //    //asp.net textarea maxlength doesnt work; do it by hand
        //    var maxlength = 500; //set your value here (or add a parm and pass it in)
        //    var object = document.getElementById(text.id)  //get your object
        //    if (object.value.length > maxlength) {
        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.substring(0, maxlength); //truncate the value
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        return false;
        //    }
        //    var matches = str.match(/(\d+)/);
        //    key = "Additional Information " + matches[0];
        //    PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //    return true;
        //}

        //function CheckSP(text) {

        //    var key;
        //    var a = /\<|\>|\&#|\\/;
        //    var object = document.getElementById(text.id)//get your object
        //    var str = String(text.id);
        //    if ((document.getElementById(text.id).value.match(a) != null)) {
        //        if (text.id == "txtName") {
        //            key = "Formula Name";
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Formula Name. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtSize") {
        //            key = "Package Size";
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Package Size. Please choose alternative characters.");
        //        }
        //        else {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# . Please choose alternative characters.");
        //        }

        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        if (text.id == "txtName") {
        //            key = "Formula Name";
        //        }
        //        else if (text.id == "txtSize") {
        //            key = "Package Size";
        //        }
        //        else if (str.search("txtMat") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Product & Packages " + matches[0];
        //        }
        //        else if (str.search("txtVol") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Volume or Size " + matches[0];
        //        }
        //        else if (str.search("txtPrice") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Price " + matches[0];
        //        }
        //        PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //        return false;
        //    }
        //    if (text.id == "txtName") {
        //        key = "Formula Name";
        //    }
        //    else if (text.id == "txtSize") {
        //        key = "Package Size";
        //    }
        //    else if (str.search("txtMat") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Product & Packages " + matches[0];
        //    }
        //    else if (str.search("txtVol") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Volume or Size " + matches[0];
        //    }
        //    else if (str.search("txtPrice") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Price " + matches[0];
        //    }
        //    PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //}

        //function onSucceed(result) {

        //}

        //function onError(result) {

        //}

    </script>
    <style type="text/css">
        .TdHeadingNew {
            background-color: #58595B;
            color: White;
            font-family: Optima;
            font-size: 12px;
        }
    </style>
    <script type="text/JavaScript">
        function ClosePage(Type) {

            if (Type == "N") {
                window.opener.document.getElementById('btnLoad').click();
            }
            else if (Type == "E") {
                window.opener.document.getElementById('btnGrid').click();
            }
            window.close();
        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 640;
            var height = 200;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var hidProjectId = document.getElementById('<%= hidProjectId.ClientID%>').value;
            Page = Page + "&ProjectId=" + hidProjectId;
            newwin = window.open(Page, 'Chat1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
            <div id="PageSection1" style="text-align: left">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <br />
                <div style="vertical-align: middle; font-weight: bold; text-align: center; font-size: large;">
                    Formula Details
                </div>
                </br>
            <table cellpadding="0" cellspacing="2" width="400px" style="margin-left: 20px;">
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Formula Name:" CssClass="SavvyPackLabel" ToolTip="Name Displayed in the Grid"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" ToolTip="Name Displayed in the Grid"
                            onchange="javascript:CheckSP(this);" CssClass="SavvyMediumTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td align="left"></td>
                    <td>
                        <asp:Button ID="btnSheet" runat="server" Text="Upload a Spec Sheet" Width="150px"
                            OnClientClick="return ShowPopWindow('FileUpload.aspx?Type=Formula')" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Package Size:" CssClass="SavvyPackLabel" ToolTip="Specifiy the detail info of the package size"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSize" runat="server" ToolTip="Specifiy the detail info of the package size" onchange="javascript:CheckSP(this);"
                            MaxLength="100" CssClass="SavvyMediumTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
                <asp:Table ID="tblFormula" runat="server">
                </asp:Table>
                <br />
                <table style="margin-left: 200px;">
                    <tr>
                        <td>
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="150px" />
                        </td>
                        <td>
                            <asp:Button ID="btnDelete" runat="server" Text="Delete" Width="150px" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <asp:HiddenField ID="hidName" runat="server" />
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:HiddenField ID="hidFormulaId" runat="server" />
    </form>
</body>
</html>
