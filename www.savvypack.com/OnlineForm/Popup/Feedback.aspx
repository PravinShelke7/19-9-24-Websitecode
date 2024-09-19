<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Feedback.aspx.vb" Inherits="Pages_Feedback" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Project Results</title>
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
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

                            if (ControlID == "txtQuan") {
                                key = 'Quantitative Results Summary';
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
                            CheckSPMul("1000");
                            var ControlID = $(this).attr('id');
                            var ControlValue = $(this).val();

                            if (ControlID == "txtQual") {
                                key = 'Qualitative Results Summary';
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

        //function CheckSP(text) {

        //    var a = /\<|\>|\&#|\\/;
        //    var key;

        //    var object = document.getElementById(text.id)//get your object
        //    if ((document.getElementById(text.id).value.match(a) != null)) {

        //        alert("You cannot use the following COMBINATIONS of characters:< > \\  &# . Please choose alternative characters.");

        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        PageMethods.UpdateCase("Quantitative Results Summary", text.value, onSucceed, onError);
        //        return false;
        //    }
        //    PageMethods.UpdateCase("Quantitative Results Summary", text.value, onSucceed, onError);
        //}

        //function Count(text) {

        //    var a = /\<|\>|\&#|\\/;
        //    var key;
        //    if ((document.getElementById(text.id).value.match(a) != null)) {

        //        var object = document.getElementById(text.id)  //get your object
        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        PageMethods.UpdateCase("Qualitative Results Summary", text.value, onSucceed, onError);
        //        return false;
        //    }
        //    //asp.net textarea maxlength doesnt work; do it by hand
        //    var maxlength = 1000; //set your value here (or add a parm and pass it in)
        //    var object = document.getElementById(text.id)  //get your object
        //    if (object.value.length > maxlength) {
        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.substring(0, maxlength); //truncate the value
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        return false;
        //    }
        //    PageMethods.UpdateCase("Qualitative Results Summary", text.value, onSucceed, onError);
        //}

        //function onSucceed(result) {

        //}

        //function onError(result) {

        //}
    </script>
    <script type="text/JavaScript">
        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .style1 {
            height: 20px;
        }

        .style2 {
            height: 20px;
        }

        .style3 {
            height: 20px;
        }
    </style>
    <script type="text/javascript">
        //        function Count(text) {
        //            var a = /\<|\>|\&#|\\/;
        //            if ((document.getElementById("txtQuan").value.match(a) != null) || (document.getElementById("txtQual").value.match(a) != null)) {
        //                if (text.id == "txtQuan") {
        //                    alert("You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' in Quantitative Benefits. Please choose alternative characters.");
        //                }
        //                else if (text.id == "txtQual") {
        //                    alert("You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' in Qualitative Benefits. Please choose alternative characters.");
        //                }
        //                var object = document.getElementById(text.id)  //get your object
        //                object.focus(); //set focus to prevent jumping
        //                object.value = text.value.replace(new RegExp("<", 'g'), "");
        //                object.value = text.value.replace(new RegExp(">", 'g'), "");
        //                object.value = text.value.replace(/\\/g, '');
        //                object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //                return false;
        //            }
        //            //asp.net textarea maxlength doesnt work; do it by hand
        //            var maxlength = 1000; //set your value here (or add a parm and pass it in)
        //            var object = document.getElementById(text.id)  //get your object
        //            if (object.value.length > maxlength) {
        //                object.focus(); //set focus to prevent jumping
        //                object.value = text.value.substring(0, maxlength); //truncate the value
        //                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //                return false;
        //            }
        //            return true;
        //        }
        //function PressEnter() {

        //    var a = /\<|\>|\&#|\\/;
        //    var Keyword = document.getElementById("txtQuan"); //get your object
        //    var Descri = document.getElementById("txtQual"); //get your object
        //    if ((document.getElementById(Keyword.id).value.match(a) != null) || (document.getElementById(Descri.id).value.match(a) != null)) {

        //        alert("You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' . Please choose alternative characters.");

        //        //object1.focus(); //set focus to prevent jumping
        //        Keyword.value = Keyword.value.replace(new RegExp("<", 'g'), "");
        //        Keyword.value = Keyword.value.replace(new RegExp(">", 'g'), "");
        //        Keyword.value = Keyword.value.replace(/\\/g, '');
        //        Keyword.value = Keyword.value.replace(new RegExp("&#", 'g'), "");
        //        Keyword.scrollTop = Keyword.scrollHeight; //scroll to the end to prevent jumping

        //        //object2.focus(); //set focus to prevent jumping
        //        Descri.value = Descri.value.replace(new RegExp("<", 'g'), "");
        //        Descri.value = Descri.value.replace(new RegExp(">", 'g'), "");
        //        Descri.value = Descri.value.replace(/\\/g, '');
        //        Descri.value = Descri.value.replace(new RegExp("&#", 'g'), "");
        //        Descri.scrollTop = Descri.scrollHeight; //scroll to the end to prevent jumping

        //        return false;
        //    }
        //}
    </script>
</head>
<body style="margin: 0 0 0 0">
    <form id="form1" runat="server" style="padding-left: 5px; padding-top: 5px">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <table class="ContentPage" id="ContentPage" runat="server" style="margin-top: 5px; margin-left: 5px">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 600px; text-align: center">
                        <asp:Label ID="Label1" Text="Project Results" runat="server"></asp:Label>
                    </div>
                    <div id="error">
                        <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
                        <div id="PageSection1" style="text-align: left;">
                            <div>
                                <table width="90%">
                                    <tr class="AlterNateColor1" id="trNum" runat="server">
                                        <td style="width: 35%">
                                            <asp:Label ID="lblNumber" runat="server" Text="Project Id:" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblId" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="width: 35%">
                                            <asp:Label ID="lblUserN" runat="server" Text="Project Title:" Font-Bold="true"></asp:Label>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="width: 35%">
                                            <asp:Label ID="lblQuan" runat="server" Text="Quantitative Results Summary:" Font-Bold="true"
                                                ToolTip="How much revenue generated by the project."></asp:Label>
                                        </td>
                                        <td style="width: 35%">
                                            <asp:TextBox ID="txtQuan" CssClass="SavvyMediumTextBox" MaxLength="100" Width="70%"
                                                runat="server" ToolTip="Estimate savings generated by the project."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr style="height: 100px;" class="AlterNateColor1">
                                        <td style="width: 35%;">
                                            <asp:Label ID="lblQual" runat="server" Text="Qualitative Results Summary:" Font-Bold="true"
                                                ToolTip="Describe benefits generated by the project."></asp:Label>
                                        </td>
                                        <td style="width: 88%">
                                            <asp:TextBox ID="txtQual" runat="server" CssClass="SavvyMediumTextBox" TextMode="MultiLine"
                                                MaxLength="1000" Width="80%" Height="80px"
                                                ToolTip="Other benefits gained by the project."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td style="width: 15%"></td>
                                        <td style="width: 88%">
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClientClick="return PressEnter();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
            <%--<tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>--%>
        </table>
    </form>
</body>
</html>
