<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateProject.aspx.vb" Inherits="OnlineForm_Popup_ProjectSummary1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Create Project</title>
    <%--<link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />--%>
    <link href="../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
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

                            if (ControlID == "txtTitle") {
                                key = 'Project Title';
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

                            if (ControlID == "txtDesc") {
                                key = 'Project Description';
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
    </script>
    <style type="text/css">
        .SavvySmallTextBox {
            font-family: Optima;
            font-size: 11.5Px;
            height: 17px;
            width: 80px;
            background-color: #FEFCA1;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
        }

        .TdHeadingNew {
            background-color: #58595B;
            color: White;
            font-family: Optima;
            font-size: 12px;
        }

        .TdHeading {
            background-color: #58595B;
            color: White;
        }


        .MediumTextBox1 {
            font-family: Optima;
            font-size: 11.5Px;
            height: 17px;
            width: 80px;
            background-color: #FEFCA1;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
    </style>
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

        function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, 'NewWindow2', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }


        }

        function RefreshPage() {

            window.opener.document.getElementById('btnRefresh').click();
        }

        function OpenWindow(Page) {

            var width = 640;
            var height = 420;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }

        function CheckControl() {
            var Pname = document.getElementById("txtTitle").value;
            if (Pname == "") {
                alert("Please enter project title.")
                return false;
            }
            else {
                return true;
            }
        }

        //        function Count(text) {

        //            var a = /\<|\>|\&#|\\/;
        //            if ((document.getElementById("txtDesc").value.match(a) != null)) {
        //                if (text.id == "txtWord") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Unique Features. Please choose alternative characters.");
        //                }
        //                else if (text.id == "txtDesc") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Project Description. Please choose alternative characters.");
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

        //        function CheckSP(text) {

        //            var a = /\<|\>|\&#|\\/;
        //            if ((document.getElementById("txtTitle").value.match(a) != null)) {
        //                var object = document.getElementById(text.id)  //get your object
        //                if (text.id == "txtTitle") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Project Title. Please choose alternative characters.");
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


    </script>

    <style type="text/css">
        .style1 {
            width: 25%;
        }

        #PageSection1 {
            background-color: #D3E7CB;
            margin-left: 2px;
            height: 510px;
        }

        .style3 {
            width: 35%;
        }

        .divUpdateprogress {
            left: 440px;
            top: 150px;
            position: absolute;
        }
    </style>
</head>
<body style="height: 250px;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server" AsyncPostBackTimeout="4500" EnablePageMethods="true">
        </asp:ScriptManager>
        <script type="text/javascript">
            window.onsubmit = function () {

                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, 100);

            }
        </script>
        <div id="ContentPagemargin" runat="server" visible="true">
            <div id="PageSection1">
                <div id="Popup1" runat="server">
                    <div id="Div4" style="text-align: left;">
                        <div style="text-align: center; height: 30px;">
                            <asp:Label ID="Label19" runat="server" Font-Bold="true" Font-Italic="true" Font-Size="18px"
                                Text="Complete 4 steps to create a project:" Style="margin-top: 5px; margin-left: 170px;">
                            </asp:Label>
                            <asp:Label ID="Label11" runat="server" Font-Bold="true" Font-Italic="true" Style="margin-left: 140px;"
                                Font-Size="15px" Text="(1/4)"></asp:Label>
                        </div>
                        <div style="text-align: center; height: 25px;">
                            <asp:Label ID="Label23" runat="server" Font-Bold="true" Font-Italic="true" Font-Size="16px"
                                Text=""></asp:Label>
                        </div>
                        <div style="text-align: left; height: 40px;">
                            <asp:Label ID="Label10" runat="server" Font-Bold="true" Font-Italic="true" Font-Size="15px"
                                Text="1. Please name your project and select what type of analysis it is." Style="margin-left: 30px;">
                            </asp:Label>
                        </div>
                        <table width="90%" style="margin-left: 30px;">
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="Label6" runat="server" Text="Project Title:" Font-Bold="true" ToolTip="Create an easy-to-remember name for your project">
                                    </asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:TextBox ID="txtTitle" ToolTip="Create an easy-to-remember name for your project"
                                        CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" id="trAnalysis" runat="server">
                                <td style="width: 25%">
                                    <asp:Label ID="Label3" runat="server" Text="Type of Analysis" Font-Bold="true" ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both.">
                                    </asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:LinkButton ID="lnkAnalysis" runat="server" Width="200px" Style="color: Black; font-family: Verdana; font-size: 12px;"
                                        Text='Select Analysis' OnClientClick="return OpenWindow('../OnlineForm/Popup/AnalysisDetails.aspx?Des=lnkAnalysis&Id=hidAnalysisId&Des1=hidAmalysisDes');"
                                        ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both.">
                                    </asp:LinkButton>
                                </td>
                            </tr>
                            <tr style="height: 100px;">
                                <td style="width: 15%"></td>
                                <td style="width: 88%"></td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 15%"></td>
                                <td style="width: 88%;">
                                    <asp:Button ID="btnnxtT" Style="margin-left: 50px;" runat="server" Text="Next" OnClientClick="return CheckControl()" />
                                </td>
                            </tr>
                        </table>
                        <div style="text-align: center; height: 60px">
                            <asp:Label ID="Label14" runat="server" Font-Bold="true" Font-Size="16px" Height="15px"
                                Font-Italic="true" Text=""></asp:Label>
                        </div>
                    </div>
                </div>
                <div id="Popup2" runat="server" visible="false " style="text-align: left;">
                    <div style="text-align: center;">
                        <asp:Label ID="Label12" runat="server" Font-Bold="true" Font-Size="16px" Height="15px"
                            Font-Italic="true" Text=""></asp:Label>
                    </div>
                    <div style="text-align: left;">
                        <asp:Label ID="Label4" runat="server" Font-Bold="true" Font-Size="16px" Height="30px"
                            Font-Italic="true" Text="2. Please enter a description for your project." Style="margin-left: 10px;">
                        </asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="true" Font-Italic="true" Style="margin-left: 260px;"
                            Height="30px" Font-Size="15px" Text="(2/4)"></asp:Label>
                    </div>
                    <table width="90%" style="margin-left: 30px;">
                        <tr class="AlterNateColor1">
                            <td class="style1">
                                <asp:Label ID="lblDesc" runat="server" Text="Project Description:" Font-Bold="true"
                                    ToolTip="Provide a full description for the project by providing all necessary details.">
                                </asp:Label>
                            </td>
                            <td style="width: 45%">
                                <asp:TextBox ID="txtDesc" runat="server" CssClass="SavvyMediumTextBox" TextMode="MultiLine"
                                    MaxLength="1000" Width="78%" Height="150px" ToolTip="Provide a detail description for the project if you like.">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr style="height: 70px;">
                            <td class="style1"></td>
                            <td style="width: 88%">
                                <asp:Button ID="btnBack1" runat="server" Text="Back" />
                                <asp:Button ID="btnnxtD" runat="server" Style="margin-left: 50px;" Text="Next" />
                            </td>
                        </tr>
                    </table>
                    <div style="text-align: center; height: 30px">
                        <asp:Label ID="Label15" runat="server" Font-Bold="true" Font-Size="16px" Height="15px"
                            Font-Italic="true" Text=""></asp:Label>
                    </div>
                </div>
                <div id="Popup3" runat="server" visible="false ">
                    <div id="Div5" style="text-align: left;">
                        <div style="text-align: center; height: 25px;">
                            <asp:Label ID="LabelQ2" runat="server" Font-Bold="true" Font-Italic="true" Font-Size="16px"
                                Text=""></asp:Label>
                        </div>
                        <div style="text-align: left; height: 40px;">
                            <asp:Label ID="LabelQ3" runat="server" Font-Bold="true" Font-Italic="true" Font-Size="15px"
                                Text="3. Please enter the details for following fields." Style="margin-left: 30px;">
                            </asp:Label>
                            <asp:Label ID="lblQuickPrice" runat="server" Font-Bold="true" Font-Italic="true"
                                Style="margin-left: 140px;" Font-Size="15px"></asp:Label>
                        </div>
                        <table width="90%" style="margin-left: 30px;">
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelAQ" runat="server" Text="Annual order quantity:" Font-Bold="true"
                                        ToolTip="Enter no of Annual order quantity." Font-Size="14px"></asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:Label ID="LblAQ" runat="server" Font-Bold="true" Font-Size="14px" ToolTip="Enter no of Annual order quantity.">
                                    </asp:Label>
                                </td>
                                <td style="width: 25%">
                                    <asp:TextBox ID="txtOQ" ToolTip="Enter no of Annual order quantity." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelOS" runat="server" Text="Order size:" Font-Bold="true" ToolTip="Enter Order size" Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td class="style4">
                                    <asp:Label ID="LblOS" runat="server" Font-Size="14px" Font-Bold="true" ToolTip="(number).">
                                    </asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:TextBox ID="txtOS" ToolTip="Enter Order size." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelQ4" runat="server" Text="Flat blank dimensions:" Font-Bold="true"
                                        ToolTip="Enter Flat blank dimensions." Font-Size="14px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlFlat_BD" CssClass="DropDown" Width="55px" runat="server"></asp:DropDownList>

                                </td>
                                <td style="width: 45%">
                                    <asp:Label ID="Labelwidth" runat="server" Text="Width:" Font-Bold="true" ToolTip="Enter Flat blank dimensions.">
                                    </asp:Label>
                                    <asp:TextBox ID="txtWidth" ToolTip="Enter width." CssClass="SavvySmallTextBox" MaxLength="100"
                                        runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="lbl" runat="server" Text="" Font-Bold="true" ToolTip="" Style="visibility: hidden;">
                                    </asp:Label>
                                    <asp:Label ID="Labelheight" runat="server" Text="Length:" Font-Bold="true" ToolTip="Enter Flat blank dimensions.">
                                    </asp:Label>
                                    <asp:TextBox ID="txtHeight" ToolTip="Enter height." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="lbl3" runat="server" Text="" Font-Bold="true" ToolTip="" Style="visibility: hidden;">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelCOD" runat="server" Text="Carton outside dimensions:" Font-Bold="true"
                                        ToolTip="Enter Carton outside dimensions" Font-Size="14px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddl_COD" CssClass="DropDown" Width="55px" runat="server"></asp:DropDownList>

                                </td>
                                <td style="width: 45%">
                                    <asp:Label ID="Labelwidth_COD" runat="server" Text="Width:" Font-Bold="true" ToolTip="Enter Width for Flat blank dimensions.">
                                    </asp:Label>
                                    <asp:TextBox ID="txtWidth_COD" ToolTip="Enter Width for Flat blank dimensions" CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="lbl1" runat="server" Text="" Font-Bold="true" ToolTip="" Style="visibility: hidden;">
                                    </asp:Label>
                                    <asp:Label ID="LabelLength_COD" runat="server" Text="Length:" Font-Bold="true" ToolTip="Enter Length for Flat blank dimensions.">
                                    </asp:Label>
                                    <asp:TextBox ID="txtHeight_COD" ToolTip="Enter height." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="lbl2" runat="server" Text="" Font-Bold="true" ToolTip="" Style="visibility: hidden;">
                                    </asp:Label>
                                    <asp:Label ID="Labelheight_COD" runat="server" Text="Height:" Font-Bold="true" ToolTip="Enter Height for Flat blank dimensions.">
                                    </asp:Label>
                                    <asp:TextBox ID="txtLength_COD" ToolTip="Enter Length for Flat blank dimensions."
                                        CssClass="SavvySmallTextBox" MaxLength="100" runat="server">
                                    </asp:TextBox>
                                    <asp:Label ID="lbl5" runat="server" Text="" Font-Bold="true" ToolTip="" Style="visibility: hidden;">
                                    </asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelWEC" runat="server" Text="Weight of empty case:" Font-Bold="true" Font-Size="14px"
                                        ToolTip="Enter Weight of empty case."></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlwc_BD" CssClass="DropDown" Width="55px" runat="server"></asp:DropDownList>

                                </td>
                                <td style="width: 45%">
                                    <asp:TextBox ID="txtWEC" ToolTip="Enter Weight of empty case." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelWPP" runat="server" Text="Weight of product packaged:" Font-Bold="true" Font-Size="14px"
                                        ToolTip="Enter Weight of product packaged."></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlwpp_BD" CssClass="DropDown" Width="55px" runat="server"></asp:DropDownList>

                                </td>
                                <td style="width: 45%">
                                    <asp:TextBox ID="txtWPP" ToolTip="Enter Weight of product packaged." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelPrinted" runat="server" Text="Printed :" Font-Bold="true" ToolTip="Enter Printed Y/N." Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="Printed_DDL" CssClass="DropDown" Width="55px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelECT" runat="server" Text="ECT:" Font-Bold="true" ToolTip="Enter ECT." Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblECT" runat="server" Font-Bold="true" ToolTip="" Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="ECT_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelMul" runat="server" Text="Mullens Rating:" Font-Bold="true" ToolTip="Enter Mullens Rating." Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblmULLENS" runat="server" Font-Size="14px" Font-Bold="true" ToolTip="">
                                    </asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="MULLEN_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelPQ" runat="server" Text="Print quality :" Font-Bold="true" Font-Size="14px" ToolTip="Enter Print quality (pre or post printed)">
                                    </asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="PQ_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelPC" runat="server" Text="Print :" Font-Size="14px" Font-Bold="true" ToolTip="Enter Print (number of colors).">
                                    </asp:Label>
                                </td>

                                <td class="style4">
                                    <asp:Label ID="LblPC1" runat="server" Font-Bold="true" ToolTip="(number)." Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:TextBox ID="txtPC" ToolTip="Enter Print (number of colors)." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelBCom" runat="server" Text="Board combination:" Font-Bold="true"
                                        ToolTip="Enter Board combination." Font-Size="14px"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="LblBcom" runat="server" Font-Bold="true" ToolTip="" Font-Size="14px"></asp:Label>
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="Bcom_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelBW" runat="server" Text="Overall board weight:" Font-Bold="true"
                                        ToolTip="Enter Overall board weight." Font-Size="14px"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="Bw_BD" CssClass="DropDown" Width="90px" runat="server"></asp:DropDownList>

                                </td>
                                <td style="width: 45%">
                                    <asp:TextBox ID="txtBW" ToolTip="Enter Overall board weight." CssClass="SavvySmallTextBox"
                                        MaxLength="100" runat="server">
                                    </asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelFS" runat="server" Text="Flute size :" Font-Bold="true" ToolTip="Enter Flute size." Font-Size="14px">
                                    </asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="FS_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelBStyle" runat="server" Text="Container style :" Font-Size="14px" Font-Bold="true" ToolTip="Enter Box style.">
                                    </asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="BStyle_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>


                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                    <asp:Label ID="LabelSFormat" runat="server" Text="Ship format :" Font-Bold="true"
                                        ToolTip="Enter Ship format." Font-Size="14px"></asp:Label>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td style="width: 45%">
                                    <asp:DropDownList ID="SFormat_DDL" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>

                                </td>
                            </tr>

                            <tr style="height: 40px;">
                                <td style="width: 25%"></td>
                                <td style="width: 18%;"></td>
                                <td style="width: 95%;">
                                    <asp:Button ID="btnback2_QP" runat="server" Text="Back" />
                                    <asp:Button ID="btnnxtQP" runat="server" Style="margin-left: 50px;" Text="Next" />
                                </td>
                            </tr>
                        </table>
                        <%-- <div style="text-align: center; height: 60px">
                        <asp:Label ID="LabelQ5" runat="server" Font-Bold="true" Font-Size="16px" Height="15px"
                            Font-Italic="true" Text=""></asp:Label>
                    </div>--%>
                    </div>
                </div>
                <div id="Popup4" runat="server" style="text-align: left; background-color: #D3E7CB;">
                    <div style="text-align: center;">
                        <asp:Label ID="Label24" runat="server" Height="15px" Text=""></asp:Label>
                    </div>
                    <div style="text-align: left;">
                        <asp:Label ID="Label9" runat="server" Text="3. Please upload files related to your project."
                            Font-Size="16px" Height="40px" Font-Italic="true" Font-Bold="true" Style="margin-left: 10px;">
                        </asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="true" Font-Italic="true" Style="margin-left: 260px;"
                            Height="40px" Font-Size="15px" Text="(3/4)"></asp:Label>
                    </div>
                    <div style="text-align: center;">
                        <table class="ContentPage" id="Table1" runat="server" style="margin-left: 5px; text-align: center; width: 100%;">
                            <tr>
                                <td>
                                    <div id="UpdatePanelwithUpload" runat="server">
                                        <asp:UpdatePanel ID="upd1" runat="server" ChildrenAsTriggers="true">
                                            <ContentTemplate>
                                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd1" DynamicLayout="true">
                                                    <ProgressTemplate>
                                                        <div class="divUpdateprogress">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <img alt="" src="../Images/Loading4.gif" height="50px" />
                                                                    </td>
                                                                    <td>
                                                                        <b style="color: Red;">Uploading...</b>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <div id="Div2" style="text-align: center; width: 100%; height: auto;">
                                                    <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                                                    <br />
                                                    <div style="width: 100%; text-align: center;">
                                                        <table width="100%">
                                                            <tr>
                                                                <td>
                                                                    <asp:FileUpload ID="fuSheet" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding-top: 10px;">
                                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return CheckFileExists()" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <br />
                                                    </div>
                                                    <div id="Div3" style="text-align: center; margin-left: 180px; width: 600px; height: 180px; overflow: auto;">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                                                        <br />
                                                        <asp:Table ID="tblDwnldList" Width="100%" runat="server">
                                                        </asp:Table>
                                                        <asp:Label ID="lblError" runat="server"></asp:Label>
                                                        <br />
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="btnSubmit" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnBack2" runat="server" Text="Back" />
                                    <asp:Button ID="btnnxtUpld" runat="server" Text="Next" Style="margin-left: 50px;" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div id="Popup5" runat="server" visible="false " style="text-align: left;">
                    <div style="text-align: center;">
                        <%-- <asp:Label ID="Label2" runat="server" Height="12px" Font-Bold="True" Font-Size="16px"
                        Font-Italic="True" Text=""></asp:Label>--%>
                    </div>
                    <div style="text-align: left;">
                        <asp:Label ID="Label7" runat="server" Height="27px" Font-Bold="True" Font-Size="16px"
                            Font-Italic="True" Text="4. Please select the date you desire the project to be completed."
                            Style="margin-left: 10px;"></asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="true" Font-Italic="true" Style="margin-left: 140px;"
                            Height="27px" Font-Size="15px" Text="(4/4)"></asp:Label>
                    </div>
                    <table width="90%" style="margin-left: 30px;">
                        <tr class="AlterNateColor1">
                            <td class="style3">
                                <asp:Label ID="Label5" runat="server" Text="Desired Complete Date:" Font-Bold="true"
                                    ToolTip="Set this date when you have a target date for the project to be completed.">
                                </asp:Label>
                            </td>
                            <td style="width: 45%">
                                <asp:TextBox ID="txtDate" CssClass="SavvyMediumTextBox" MaxLength="1000" Width="50%"
                                    runat="server" ToolTip="Set this date when you have a target date for the project to be completed..">
                                </asp:TextBox>
                                <ajaxToolkit:CalendarExtender ID="calcExt" runat="server" Enabled="True" TargetControlID="txtDate">
                                </ajaxToolkit:CalendarExtender>
                            </td>
                        </tr>
                        <tr style="height: 160px;">
                            <td class="style3"></td>
                            <td style="width: 88%"></td>
                        </tr>
                        <tr style="height: 50px;">
                            <td class="style3"></td>
                            <td style="width: 88%">
                                <asp:Button ID="btnBack3" runat="server" Text="Back" />
                                <asp:Button ID="btnnxtS" runat="server" Style="margin-left: 50px;" Text="Next" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="Popup6" runat="server" visible="false " style="text-align: left;">
                    <div style="text-align: center;">
                        <asp:Label ID="Label8" runat="server" Height="12px" Font-Bold="True" Font-Size="16px"
                            Font-Italic="True" Text=""></asp:Label>
                    </div>
                    <table width="90%" style="margin-left: 30px;">
                        <tr style="height: 50px; text-align: center;">
                            <td class="style3">
                                <asp:Label ID="Label17" runat="server" Font-Size="15" Font-Italic="true" ForeColor="Red"
                                    Font-Bold="true" Text="Congratulations!"></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 150px; font-family: Optima;">
                            <td class="style3">
                                <asp:Label ID="Label1" runat="server" Style="margin-left: 3px;" Font-Bold="false"
                                    Font-Size="13" Text="  You are about to submit your project. Once you submit your project,one of">
                                </asp:Label>
                                <asp:Label ID="Label13" runat="server" Style="margin-left: 5px;" Font-Bold="false"
                                    Font-Size="13" Text="  our package analysts will review your project inputs and contact you.You can ">
                                </asp:Label>
                                <asp:Label ID="Label16" runat="server" Style="margin-left: 5px;" Font-Bold="false"
                                    Font-Size="13" Text="  view and edit your inputs from Project Manager. You can also track the status ">
                                </asp:Label>
                                <asp:Label ID="Label18" runat="server" Style="margin-left: 5px;" Font-Bold="false"
                                    Font-Size="13" Text="  of your project from there."></asp:Label>
                            </td>
                        </tr>
                        <tr style="height: 50px; vertical-align: middle; text-align: center;">
                            <td style="width: 88%">
                                <asp:Button ID="btnBack4" runat="server" Text="Back" />
                                <asp:Button ID="submit" runat="server" Text="Submit" Style="margin-left: 50px;" CommandName="Submit" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:HiddenField ID="hidAnalysisId" runat="server" />
        <asp:HiddenField ID="hidFileAction" runat="server" />
        <asp:HiddenField ID="hidAmalysisDes" runat="server" />
        <script type="text/javascript">
            function CheckFileExists() {
                var existfile = "<%=Session("CPExistingFile")%>"
                var newFile = document.getElementById('fuSheet').value;
                if (newFile != "") {
                    newFile = newFile.substring(newFile.lastIndexOf("\\") + 1, newFile.length);
                    var overwrite = false;

                    var ExFile = existfile.split(",");

                    for (i = 0; i < ExFile.length; i++) {
                        if (newFile == ExFile[i]) {
                            overwrite = true;
                        }
                    }

                    if (overwrite) {
                        if (confirm("This file already exists. Do you want to overwrite it?")) {
                            document.getElementById("hidFileAction").value = "Update";
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        document.getElementById("hidFileAction").value = "Insert";
                        return true;
                    }
                }
                else {
                    alert("Please select file to upload.");
                    return false;
                }
            }

            //function Count(text) {

            //    var a = /\<|\>|\&#|\\/;
            //    if ((document.getElementById("txtDesc").value.match(a) != null)) {
            //        if (text.id == "txtWord") {
            //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Unique Features. Please choose alternative characters.");
            //        }
            //        else if (text.id == "txtDesc") {
            //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Project Description. Please choose alternative characters.");
            //        }
            //        var object = document.getElementById(text.id)  //get your object
            //        object.focus(); //set focus to prevent jumping
            //        object.value = text.value.replace(new RegExp("<", 'g'), "");
            //        object.value = text.value.replace(new RegExp(">", 'g'), "");
            //        object.value = text.value.replace(/\\/g, '');
            //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
            //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
            //        if (text.id == "txtWord") {
            //            key = 'Brief Description';
            //        }
            //        else if (text.id == "txtDesc") {
            //            key = 'Project Description';
            //        }
            //        //var oValue = text.oldvalue;
            //        PageMethods.UpdateCase(key, text.value, onSucceed, onError);
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
            //    if (text.id == "txtWord") {
            //        key = 'Brief Description';
            //    }
            //    else if (text.id == "txtDesc") {
            //        key = 'Project Description';
            //    }
            //    //var oValue = text.oldvalue;
            //    PageMethods.UpdateCase(key, text.value, onSucceed, onError);
            //}            

            //function onSucceed(result) {

            //}

            //function onError(result) {

            //}
        </script>
    </form>
</body>
</html>
