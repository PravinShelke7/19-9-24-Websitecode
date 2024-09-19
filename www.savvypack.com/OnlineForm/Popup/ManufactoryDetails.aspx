<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManufactoryDetails.aspx.vb"
    Inherits="Pages_PopUp_ManufactoryDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manufactory Details</title>
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

                            var str = String(ControlID);

                            if (ControlID == "txtName") {
                                key = 'Manufactory Process Name';
                            }
                            else if (ControlID == "txtOSize") {
                                key = 'Order Size';
                            }
                            else if (ControlID == "txtRSize") {
                                key = 'Run Size';
                            }
                            else if (str.search("txtPP") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Production Process " + matches[0];
                            }
                            else if (str.search("txtMac") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Machine " + matches[0];
                            }
                            else if (str.search("txtRate") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Instaneous Rate " + matches[0];
                            }
                            else if (str.search("txtTPut") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Throughput " + matches[0];
                            }
                            else if (str.search("txtDTime") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Downtime " + matches[0];
                            }
                            else if (str.search("txtWaste") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Waste " + matches[0];
                            }
                            else if (str.search("txtCC") == 0) {
                                var matches = str.match(/(\d+)/);
                                key = "Capital Cost " + matches[0];
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

                            var str = String(ControlID);
                            var matches = str.match(/(\d+)/);
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
        //        var matches = str.match(/(\d+)/);
        //        key = "Additional Information " + matches[0];
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

        //    var a = /\<|\>|\&#|\\/;
        //    var key;
        //    var object = document.getElementById(text.id)//get your object
        //    var str = String(text.id);
        //    if ((document.getElementById(text.id).value.match(a) != null)) {
        //        if (text.id == "txtName") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Name. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtOSize") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Order Size. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtRSize") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Run Size. Please choose alternative characters.");
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
        //            key = 'Manufactory Process Name';
        //        }
        //        else if (text.id == "txtOSize") {
        //            key = 'Order Size';
        //        }
        //        else if (text.id == "txtRSize") {
        //            key = 'Run Size';
        //        }
        //        else if (str.search("txtPP") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Production Process " + matches[0];
        //        }
        //        else if (str.search("txtMac") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Machine " + matches[0];
        //        }
        //        else if (str.search("txtRate") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Instaneous Rate " + matches[0];
        //        }
        //        else if (str.search("txtTPut") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Throughput " + matches[0];
        //        }
        //        else if (str.search("txtDTime") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Downtime " + matches[0];
        //        }
        //        else if (str.search("txtWaste") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Waste " + matches[0];
        //        }
        //        else if (str.search("txtCC") == 0) {
        //            var matches = str.match(/(\d+)/);
        //            key = "Capital Cost " + matches[0];
        //        }

        //        PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //        return false;
        //    }

        //    if (text.id == "txtName") {
        //        key = 'Manufactory Process Name';
        //    }
        //    else if (text.id == "txtOSize") {
        //        key = 'Order Size';
        //    }
        //    else if (text.id == "txtRSize") {
        //        key = 'Run Size';
        //    }
        //    else if (str.search("txtPP") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Production Process " + matches[0];
        //    }
        //    else if (str.search("txtMac") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Machine " + matches[0];
        //    }
        //    else if (str.search("txtRate") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Instaneous Rate " + matches[0];
        //    }
        //    else if (str.search("txtTPut") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Throughput " + matches[0];
        //    }
        //    else if (str.search("txtDTime") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Downtime " + matches[0];
        //    }
        //    else if (str.search("txtWaste") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Waste " + matches[0];
        //    }
        //    else if (str.search("txtCC") == 0) {
        //        var matches = str.match(/(\d+)/);
        //        key = "Capital Cost " + matches[0];
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
                    Manufactory Process Details
                </div>
                </br>
            <table cellpadding="0" cellspacing="2" width="400px" style="margin-left: 20px;">
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Manufactory Process Name:" ToolTip="Name Displayed in the Grid"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" ToolTip="Name Displayed in the Grid"
                            CssClass="SavvyMediumTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Order Size:" ToolTip="Order size (or order frequency)"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtOSize" runat="server" MaxLength="100" ToolTip="Order size (or order frequency)"
                            CssClass="SavvyMediumTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Run Size:" ToolTip="Volume for each run" CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtRSize" runat="server" MaxLength="100" ToolTip="Volume for each run"
                            CssClass="SavvyMediumTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
                <asp:Table ID="tblManuf" runat="server">
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
    </form>
</body>
</html>
