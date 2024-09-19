<%@ Page Title="Model Grid" Language="VB" AutoEventWireup="false" CodeFile="ModelInput.aspx.vb"
    Inherits="Pages_ModelInput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Model Grid</title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
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

                            if (ControlID == "txtType") {
                                key = "Package Type";
                            }
                            else if (ControlID == "txtChain") {
                                key = "Value Chain";
                            }
                            else if (ControlID == "txtSize") {
                                key = "Package Size";
                            }
                            else if (ControlID == "txtProd") {
                                key = "Product to Pack";
                            }
                            else if (ControlID == "txtGeog") {
                                key = "Geography";
                            }
                            else if (ControlID == "txtFeat1") {
                                key = "Special Features 1";
                            }
                            else if (ControlID == "txtFeat2") {
                                key = "Special Features 2";
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
        //    var key;
        //    var a = /\<|\>|\&#|\\/;
        //    var object = document.getElementById(text.id)//get your object
        //    if ((document.getElementById("txtType").value.match(a) != null) || (document.getElementById("txtChain").value.match(a) != null) || (document.getElementById("txtSize").value.match(a) != null) ||
        //        (document.getElementById("txtProd").value.match(a) != null) || (document.getElementById("txtGeog").value.match(a) != null) || (document.getElementById("txtFeat1").value.match(a) != null) || (document.getElementById("txtFeat2").value.match(a) != null)) {
        //        if (text.id == "txtType") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Package Type. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtChain") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Value Chain. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtSize") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Package Size. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtProd") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Product to Pack. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtGeog") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Geography. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtFeat1") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Special Features1. Please choose alternative characters.");
        //        }
        //        else if (text.id == "txtFeat2") {
        //            alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Special Features2. Please choose alternative characters.");
        //        }
        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        if (text.id == "txtType") {
        //            key = "Package Type";
        //        }
        //        else if (text.id == "txtChain") {
        //            key = "Value Chain";
        //        }
        //        else if (text.id == "txtSize") {
        //            key = "Package Size";
        //        }
        //        else if (text.id == "txtProd") {
        //            key = "Product to Pack";
        //        }
        //        else if (text.id == "txtGeog") {
        //            key = "Geography";
        //        }
        //        else if (text.id == "txtFeat1") {
        //            key = "Special Features 1";
        //        }
        //        else if (text.id == "txtFeat2") {
        //            key = "Special Features 2";
        //        }
        //        PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //        return false;
        //    }
        //    if (text.id == "txtType") {
        //        key = "Package Type";
        //    }
        //    else if (text.id == "txtChain") {
        //        key = "Value Chain";
        //    }
        //    else if (text.id == "txtSize") {
        //        key = "Package Size";
        //    }
        //    else if (text.id == "txtProd") {
        //        key = "Product to Pack";
        //    }
        //    else if (text.id == "txtGeog") {
        //        key = "Geography";
        //    }
        //    else if (text.id == "txtFeat1") {
        //        key = "Special Features 1";
        //    }
        //    else if (text.id == "txtFeat2") {
        //        key = "Special Features 2";
        //    }
        //    PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        //}

        //function onSucceed(result) {

        //}

        //function onError(result) {

        //}
    </script>
    <style type="text/css">
        a:link {
            color: Black;
            font-family: Optima;
            font-size: 12px;
            text-decoration: underline;
        }

        a:visited {
            color: Black;
            font-family: Optima;
            font-size: 12px;
            text-decoration: underline;
        }

        a:hover {
            color: Red;
            font-size: 12px;
        }
    </style>
    <style type="text/css">
        .divModel {
            width: 100%;
            word-break: break-all;
        }

        .MSavvyModule {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url(../Images/SavvyPackProject1350.gif);
            height: 45px;
            width: 1335px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }

        .grdProject {
            width: 600px;
            font-family: Verdana;
            font-size: 12px;
            border-right: #32659A 1px solid;
            border-top: #32659A 1px solid;
            border-left: #32659A 1px solid;
            border-bottom: #32659A 1px solid;
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
            window.close();

        }

        function SetTarget() {
            document.forms[0].target = "_blank";
        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 640;
            var height = 280;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function Help() {
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
            URL = "help/SavvyPackProjectInstructions.pdf"
            newwin = window.open(URL, 'NewWindow2', params);
            return false

        }


        function StructFormPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 950;
            var height = 650;
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
            Page = Page + "&ProjectId=" + hidProjectId + "";
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function ShowNewPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1050;
            var height = 650;
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
            Page = Page + "&ProjectId=" + hidProjectId + "";
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function ShowEditPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 500;
            var height = 350;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            var hidProjectId = document.getElementById('<%= hidProjectId.ClientID%>').value;
            Page = Page + "&ProjectId=" + hidProjectId + "";
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function ShowPopWindowProd(Id, ProjId, ProjCatId, Type, isDisabled) {

            //var listings = document.getElementById("hidProd1");
            //alert(ProdId + " " + ProjId);

            var width = 600;
            var height = 300;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            if (Type == "1") {
                var Page = "Popup/ProducerDetails.aspx?ProjectId=" + ProjId + "&ProducerId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "2") {
                var Page = "Popup/PackerDetails.aspx?ProjectId=" + ProjId + "&PackerId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "3") {
                var Page = "Popup/GraphicsDetails.aspx?ProjectId=" + ProjId + "&GraphicsId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "8") {
                var Page = "Popup/SKUDetails.aspx?ProjectId=" + ProjId + "&SKUId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }

            newwin = window.open(Page, 'Chat', params);
            //alert(listings.value);
            return false;
        }

        function ShowNewPopWindowProd(Id, ProjId, ProjCatId, Type, isDisabled) {

            //var listings = document.getElementById("hidProd1");
            //alert(ProdId + " " + ProjId);
            var width = 950;
            var height = 660;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            if (Type == "1") {
                var Page = "Popup/ProducerDetails.aspx?ProjectId=" + ProjId + "&ProducerId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "2") {
                var Page = "Popup/PackerDetails.aspx?ProjectId=" + ProjId + "&PackerId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "3") {
                var Page = "Popup/GraphicsDetails.aspx?ProjectId=" + ProjId + "&GraphicsId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "4") {
                var Page = "Popup/StructureDetails.aspx?ProjectId=" + ProjId + "&StructureId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "5") {
                var Page = "Popup/FormulaDetails.aspx?ProjectId=" + ProjId + "&FormulaId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "6") {
                var Page = "Popup/ManufactoryDetails.aspx?ProjectId=" + ProjId + "&ManufactoryId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "7") {
                var Page = "Popup/PackagingDetails.aspx?ProjectId=" + ProjId + "&PackagingId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            newwin = window.open(Page, 'Chat', params);
            //alert(listings.value);
            return false;
        }

        function ShowManuPopWindowProd(Id, ProjId, ProjCatId, Type, isDisabled) {

            //var listings = document.getElementById("hidProd1");
            //alert(ProdId + " " + ProjId);
            var width = 1050;
            var height = 660;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            if (Type == "6") {
                var Page = "Popup/ManufactoryDetails.aspx?ProjectId=" + ProjId + "&ManufactoryId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            else if (Type == "7") {
                var Page = "Popup/PackagingDetails.aspx?ProjectId=" + ProjId + "&PackagingId=" + Id + "&ProjCatId=" + ProjCatId + "&Type=E&isDisabled=" + isDisabled + "";
            }
            newwin = window.open(Page, 'Chat', params);
            //alert(listings.value);
            return false;
        }

        function ShowUploadPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 640;
            var height = 200;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
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

        function CompareCount(chkbox) {

            if (chkbox.checked == true) {
                document.getElementById('<%= btnCall.ClientID%>').click();
                return true;
            }
            else {
                return false;
            }

        }

        function RefreshPage() {

            window.opener.document.getElementById('btnRefresh').click();
        }
    </script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
                '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div id="MasterContent">
            <div>
                <table class="MSavvyModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                            runat="server" ToolTip="Instructions" Visible="true" OnClientClick="return Help();" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
        <table class="SavvyContentPage" id="ContentPage" runat="server" style="width: 1330px;">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                        Model Grid
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="600px">
                        <tr style="width: 100%">
                            <td style="width: 16%; vertical-align: middle; font-weight: bold;">
                                <asp:Label ID="Label1" runat="server" Text="Project Number:"></asp:Label>
                            </td>
                            <td style="width: 19%; text-align: left;">
                                <asp:Label ID="lblNum" runat="server"></asp:Label>
                            </td>
                            <td style="width: 12%; vertical-align: middle; font-weight: bold;">
                                <asp:Label ID="Label2" runat="server" Text="Project Title:"></asp:Label>
                            </td>
                            <td style="width: 53%; text-align: left;">
                                <asp:Label ID="lblTitle" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="SavvyContentPagemargin" runat="server" style="text-align: left; width: 1320px;">
                        <div id="SavvyPageSection1" style="text-align: left; width: 1320px;">
                            <table width="1320px">
                                <tr runat="server" id="trTable">
                                    <td valign="top">
                                        <table style="width: 720px">
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblType" runat="server" CssClass="SavvyPackLabel" Text="Package Type:"
                                                        Font-Bold="true" ToolTip="Eg: Bottles,Labels,Component,Rollstock, etc"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtType" CssClass="SavvyMediumTextBox" Width="40%" MaxLength="100"
                                                        runat="server" ToolTip="Eg: Bottles,Labels,Component,Rollstock, etc"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnType" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trType" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblType" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnEType" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=TYPE')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblChain" runat="server" CssClass="SavvyPackLabel" Text="Value Chain:"
                                                        Font-Bold="true" ToolTip="Eg: Package manufactory,Packaging, etc"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtChain" CssClass="SavvyMediumTextBox" MaxLength="100" Width="40%"
                                                        runat="server" ToolTip="Eg: Package manufactory,Packaging, etc"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnChain" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trChain" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblChain" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnEChain" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=VCHAIN')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblSize" CssClass="SavvyPackLabel" runat="server" Text="Package Size:"
                                                        Font-Bold="true" ToolTip="Eg: 1''x2.5''x5'', 500ml, 12g(Package weight), 20lb(Product weight), etc"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtSize" CssClass="SavvyMediumTextBox" MaxLength="100" Width="40%"
                                                        runat="server" ToolTip="Eg: 1''x2.5''x5'', 500ml, 12g(Package weight), 20lb(Product weight), etc"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnSize" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trSize" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblSize" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnESize" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=SIZE')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblProd" CssClass="SavvyPackLabel" runat="server" Text="Product to Pack:"
                                                        Font-Bold="true" ToolTip="Eg: Food, TV, Medical devices, etc"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtProd" CssClass="SavvyMediumTextBox" MaxLength="100" Width="40%"
                                                        runat="server" ToolTip="Eg: Food, TV, Medical devices, etc"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnProd" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trProd" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblProd" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnEProd" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=PROD')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblGeog" runat="server" CssClass="SavvyPackLabel" Text="Geography:"
                                                        Font-Bold="true" ToolTip="Eg: Burnsville, Germany, Asia, etc"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtGeog" CssClass="SavvyMediumTextBox" MaxLength="100" Width="40%"
                                                        runat="server" ToolTip="Eg: Burnsville, Germany, Asia, etc"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnGeog" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trGeog" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblGeog" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnEGeog" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=GEOG')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblFeat1" runat="server" CssClass="SavvyPackLabel" Text="Special Features 1:"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtFeat1" CssClass="SavvyMediumTextBox" Width="40%" MaxLength="100"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnFeat1" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trFeat1" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblFeat1" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnEFeat1" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=SPFET1')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1">
                                                <td style="width: 18%" colspan="2">
                                                    <asp:Label ID="lblFeat2" runat="server" CssClass="SavvyPackLabel" Text="Special Features 2:"
                                                        Font-Bold="true"></asp:Label>
                                                </td>
                                                <td style="width: 75%" colspan="2">
                                                    <asp:TextBox ID="txtFeat2" CssClass="SavvyMediumTextBox" Width="40%" MaxLength="100"
                                                        runat="server"></asp:TextBox>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnFeat2" runat="server" CssClass="ButtonWMarigin" Width="90%" Text="Add" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" id="trFeat2" runat="server" visible="false">
                                                <td style="width: 18%" colspan="2"></td>
                                                <td class="AlterNateColor2" style="width: 75%" colspan="2">
                                                    <asp:Table ID="tblFeat2" runat="server" Width="25%">
                                                    </asp:Table>
                                                </td>
                                                <td style="width: 7%; text-align: center;" colspan="2">
                                                    <asp:Button ID="btnEFeat2" runat="server" OnClientClick="return ShowEditPopWindow('Popup/Edit.aspx?Type=SPFET2')"
                                                        CssClass="ButtonWMarigin" Width="90%" Text="Edit" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table style="width: 580px">
                                            <tr style="text-align: center;">
                                                <td>
                                                    <asp:Label runat="server" Text="Package Producing" Font-Bold="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:Label runat="server" Text="Package Filling" Font-Bold="true"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="width: 50%;">
                                                    <table width="100%">
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkProducer" runat="server" Width="150px" CssClass="SavvyLink"
                                                                    OnClientClick="return ShowPopWindow('Popup/ProducerDetails.aspx?Type=N')">Add Package Producer</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trProducer" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblProducer" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkStruct" runat="server" Width="150px" CssClass="SavvyLink"
                                                                    OnClientClick="return StructFormPopWindow('Popup/StructureDetails.aspx?Type=N')">Add Package Structure</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trStruct" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblStruct" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkManuf" runat="server" Width="150px" CssClass="SavvyLink" OnClientClick="return ShowNewPopWindow('Popup/ManufactoryDetails.aspx?Type=N')">Add Package Producing</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trManuf" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblManuf" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkGraphics" runat="server" CssClass="SavvyLink" OnClientClick="return ShowPopWindow('Popup/GraphicsDetails.aspx?Type=N')">Add Graphic Settings</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trGraphics" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblGraphics" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td style="width: 50%;">
                                                    <table width="100%" style="margin-top: 0px;">
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkPacker" runat="server" Width="150px" CssClass="SavvyLinkN"
                                                                    OnClientClick="return ShowPopWindow('Popup/PackerDetails.aspx?Type=N')">Add Product Producer</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trPacker" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblPacker" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkFormula" runat="server" Width="150px" CssClass="SavvyLinkN"
                                                                    OnClientClick="return StructFormPopWindow('Popup/FormulaDetails.aspx?Type=N')">Add Product & Packages</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trFormula" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblFormula" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkPackg" runat="server" Width="150px" CssClass="SavvyLinkN"
                                                                    OnClientClick="return ShowNewPopWindow('Popup/PackagingDetails.aspx?Type=N')">Add Product Producing</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trPack" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblPack" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1">
                                                            <td style="width: 100%; text-align: center;">
                                                                <asp:LinkButton ID="lnkSKU" runat="server" Width="150px" CssClass="SavvyLinkN" OnClientClick="return ShowPopWindow('Popup/SKUDetails.aspx?Type=N')">Add SKU Settings</asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                        <tr class="AlterNateColor1" id="trSKU" runat="server" visible="false">
                                                            <td class="AlterNateColor2" style="width: 100%">
                                                                <asp:Table ID="tblSKU" runat="server">
                                                                </asp:Table>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="margin-top: 20px; text-align: center;">
                                            <asp:Button ID="btnSheet" runat="server" Text="Upload a Spec Sheet" Width="150px"
                                                OnClientClick="return ShowUploadPopWindow('Popup/FileUpload.aspx?Type=Universal')" />
                                            <asp:LinkButton ID="lnkFiles" runat="server" OnClientClick="return ShowPopWindow('Popup/ViewFiles.aspx')">View Files</asp:LinkButton>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                            <div style="text-align: center; background-color: #D3DAD0;">
                                <asp:Button ID="btnGrid" runat="server" Text="Generate Grid" />
                                <asp:Button ID="btnSave" runat="server" Text="Save the Grid" Visible="false" />
                            </div>
                            <br />
                            <asp:Label ID="lblCount" runat="server" Font-Bold="true" Font-Size="Small"></asp:Label>
                            <br />
                            <div id="divModel" runat="server" style="width: 1335px; overflow: auto; background-color: White;">
                                <asp:GridView CssClass="grdProject" runat="server" ID="grdModel" DataKeyNames="MODELID"
                                    AllowPaging="true" Width="1500px" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                    Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="Outset" BorderWidth="1px">
                                    <FooterStyle BackColor="#CCCC99" />
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="PROJECTID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                            SortExpression="PROJECTID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjectID" runat="server" Text='<%# bind("PROJECTID")%>'></asp:Label>
                                                <asp:Label ID="lblActive" runat="server" Text='<%# bind("ISACTIVE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="20px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            HeaderText="Exclude">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkHide" runat="server" OnCheckedChanged="CheckBox1_CheckedChanged"
                                                    AutoPostBack="true" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="50px" Wrap="true" HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Model ID" HeaderStyle-HorizontalAlign="Left" SortExpression="MODELID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblModelId" runat="server" Text='<%# bind("MODELID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="70px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Package Type" HeaderStyle-HorizontalAlign="Left" SortExpression="PACKTYPE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPckT" runat="server" Text='<%# bind("PACKTYPE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Value Chain" HeaderStyle-HorizontalAlign="Left" SortExpression="VCHAIN">
                                            <ItemTemplate>
                                                <asp:Label ID="lblChain" runat="server" Text='<%# bind("VCHAIN")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Package Size" HeaderStyle-HorizontalAlign="Left" SortExpression="PACKSIZE">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPckS" runat="server" Text='<%# bind("PACKSIZE")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product to Pack" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="PRODPACK">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProd" runat="server" Text='<%# bind("PRODPACK")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Wrap="true" HorizontalAlign="Left" Width="180px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Geography" HeaderStyle-HorizontalAlign="Left" SortExpression="GEOGRAPHY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGeog" runat="server" Text='<%# bind("GEOGRAPHY")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Special Features1" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="SPFET1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFeat1" runat="server" Text='<%# bind("SPFET1")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Special Features2" HeaderStyle-HorizontalAlign="Left"
                                            SortExpression="SPFET2">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFeat2" runat="server" Text='<%# bind("SPFET2")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Producer" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="PRODUCER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProducerId" runat="server" Visible="false" Text='<%# bind("PRODUCERID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkProducer" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("PRODUCER")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packer" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="PACKER">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackerId" runat="server" Visible="false" Text='<%# bind("PACKERID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkPacker" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("PACKER")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Graphics" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="GRAPHICS">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGraphicsId" runat="server" Visible="false" Text='<%# bind("GRAPHICSID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkGraphics" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("GRAPHICS")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Structure" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="STRUCTURES">
                                            <ItemTemplate>
                                                <asp:Label ID="lblStructId" runat="server" Visible="false" Text='<%# bind("STRUCTUREID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkStruct" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("STRUCTURES")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Formula" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="FORMULA">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFormulaId" runat="server" Visible="false" Text='<%# bind("FORMULAID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkFormula" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("FORMULA")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manufactory" HeaderStyle-HorizontalAlign="center"
                                            ItemStyle-HorizontalAlign="Left" SortExpression="MANUFACTORY">
                                            <ItemTemplate>
                                                <asp:Label ID="lblManufId" runat="server" Visible="false" Text='<%# bind("MANUFACTORYID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkManuf" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("MANUFACTORY")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packaging" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="PACKAGING">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPackgId" runat="server" Visible="false" Text='<%# bind("PACKAGINGID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkPackg" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("PACKAGING")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SKU" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left"
                                            SortExpression="SKU">
                                            <ItemTemplate>
                                                <asp:Label ID="lblSkuId" runat="server" Visible="false" Text='<%# bind("SKUID")%>'></asp:Label>
                                                <asp:LinkButton ID="lnkSku" runat="server" Style="font-family: Verdana; font-size: 12px;"
                                                    Text='<%# bind("SKU")%>'></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
                                        HorizontalAlign="Left" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                            <br />
                            <div class="AlterNateColor3" style="text-align: center; width: 1320px;">
                                <asp:Label ID="lblTag" runat="Server" CssClass="PageSHeading"></asp:Label>
                            </div>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <div id="AlliedLogo">
            <table>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 1320px; background-color: #edf0f4;">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidSortId" runat="server" />
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:Button ID="btnLoad" runat="server" Style="display: none;" />
        <asp:Button ID="btnCall" runat="server" Style="display: none;" />
    </form>
</body>
</html>
