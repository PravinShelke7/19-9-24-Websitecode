<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProducerDetails.aspx.vb"
    Inherits="Pages_PopUp_ProducerDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Producer Details</title>
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
                                key = 'Producers Name';
                            }
                            else if (ControlID == "txtLocat") {
                                key = 'Location of package production plant';
                            }
                            else if (ControlID == "txtCapacity") {
                                key = 'Annual Capacity of the plant';
                            }
                            else if (ControlID == "txtLines") {
                                key = 'Number of Production lines in plant';
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

                            if (ControlID == "txtInfo") {
                                key = 'Additional Information';
                            }

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
        //        PageMethods.UpdateCase("Additional Information", text.value, onSucceed, onError);
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
        //    PageMethods.UpdateCase("Additional Information", text.value, onSucceed, onError);
        //    return true;
        //}

        //function CheckSP(text) {

        //    var a = /\<|\>|\&#|\\/;
        //    var object = document.getElementById(text.id)//get your object
        //    if ((document.getElementById(text.id).value.match(a) != null)) {
        //        if (text.id == "txtName") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Name. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtLocat") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Location. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtCapacity") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Annual Capacity. Please choose alternative characters.");
        //        }
        //        else {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Lines. Please choose alternative characters.");
        //        }

        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        if (text.id == "txtName") {
        //            key = 'Producers Name';
        //        }
        //        else if (text.id == "txtLocat") {
        //            key = 'Location of package production plant';
        //        }
        //        else if (text.id == "txtCapacity") {
        //            key = 'Annual Capacity of the plant';
        //        }
        //        else if (text.id == "txtLines") {
        //            key = 'Number of Production lines in plant';
        //        }
        //        PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //        return false;
        //    }
        //    if (text.id == "txtName") {
        //        key = 'Producers Name';
        //    }
        //    else if (text.id == "txtLocat") {
        //        key = 'Location of package production plant';
        //    }
        //    else if (text.id == "txtCapacity") {
        //        key = 'Annual Capacity of the plant';
        //    }
        //    else if (text.id == "txtLines") {
        //        key = 'Number of Production lines in plant';
        //    }
        //    PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //}
        //function onSucceed(result) {

        //}

        //function onError(result) {

        //}

    </script>
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
                    Producer Details
                </div>
                </br>
            <table cellpadding="0" cellspacing="2" width="500px" style="margin-left: 50px;">
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Producer's Name:" ToolTip="Name Displayed in the Grid"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="100" ToolTip="Name Displayed in the Grid"
                            CssClass="SavvyMediumTextBox" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Location:" ToolTip="Location of package production plant"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLocat" runat="server" ToolTip="Location of package production plant"
                            CssClass="SavvyMediumTextBox" MaxLength="100" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Capacity:" ToolTip="Annual Capacity of the plant"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCapacity" runat="server" MaxLength="100" ToolTip="Annual Capacity of the plant"
                            CssClass="SavvyMediumTextBox" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Lines:" ToolTip="Number of Production lines in plant"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtLines" runat="server" MaxLength="100" ToolTip="Number of Production lines in plant"
                            CssClass="SavvyMediumTextBox" Width="270px"></asp:TextBox>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td align="left">
                        <b>
                            <asp:Label runat="server" Text="Additional Information:" ToolTip="Any Other Information on the plant"
                                CssClass="SavvyPackLabel"></asp:Label></b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtInfo" runat="server" TextMode="MultiLine" MaxLength="500" ToolTip="Any Other Information on the plant"
                            CssClass="SavvyMediumTextBox" Width="320px" Height="70px"></asp:TextBox>
                    </td>
                </tr>
            </table>
                <br />
                <div style="text-align: center;">
                    <asp:Button ID="btnUpdate" Text="Update" runat="server" CssClass="Button" />
                    <asp:Button ID="btnDelete" Text="Delete" runat="server" CssClass="Button" />
                </div>
                <br />
            </div>
        </div>
        <asp:HiddenField ID="hidName" runat="server" />
    </form>
</body>
</html>
