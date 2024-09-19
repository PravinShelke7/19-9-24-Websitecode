<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnalyzeProposals.aspx.vb"
    Inherits="Pages_SavvyPackPro_AnalyzeProposals" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Analyze Proposals</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV1.js"></script>
    <script type="text/JavaScript">
        function ShowMasterGroupPopUp(Page) {

            var width = 860;
            var height = 520;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'SelMasterGrp', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ShowPopWindow(Page) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 650;
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
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;

        }
    </script>
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

        function CheckSP(text) {

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            else {
                document.getElementById('btnrefreshSeq').click();
                return true;

            }
        }

        function ShowPopUpRFP(Page) {

            var width = 860;
            var height = 420;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'SelRFP', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ShowPopUpVendorInfo(Page) {
            var width = 870;
            var height = 580;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'AddEditVendorInfo', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ShowPopUpGraph(Page) {

            var width = 470;
            var height = 420;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'SelRFP');
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function ShowPriceOpPopUp(Page) {
            var width = 300;
            var height = 230;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'PriceOptionSel', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;

        }

        function ShowGrpBPopUp(Page) {
            var PriceOptn = document.getElementById('hidPriceOpIDBP').value;
            if (PriceOptn != "" && PriceOptn != "0") {
                var width = 490;
                var height = 422;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=yes';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                Page = Page + '&RfpPriceID=' + PriceOptn + '&Type=Analysis';
                newwin = window.open(Page, 'GrpSelB', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
                }
            }
            else {
                alert("Please Select Price Option First");
            }

            return false;
        }

        function showhideColumn(colIndex) {
            var table = document.getElementById('tabAnalyzePPSl_tabPriceOptnAnalyz_tblPO');
            for (var r = 0; r < table.rows.length; r++) {
                if (document.getElementById('chkBox_' + colIndex).checked == true)
                    table.rows[r].cells[colIndex].style.display = '';
                else
                    table.rows[r].cells[colIndex].style.display = 'none';
            }

        }

        function showhideALL2(PIndex) {
            var gcnt = document.getElementById('hidGrpCnt').value;
            var pcnt = document.getElementById('hidPricetypeCnt').value;
            for (i = 0; i <= gcnt; i++) {
                for (j = 0; j <= pcnt; j++) {
                    if (j == PIndex) {
                        obj = document.getElementById("tabAnalyzePPSl_tabPriceOptnAnalyz_PriceOptn" + "_" + i + "_" + j);
                        if (document.getElementById('chkBoxPO_' + PIndex).checked == true)
                            obj.style.display = "";
                        else
                            obj.style.display = "none";
                    }
                }
            }

        }

        function ShowPopUpChart(Page) {
            var PriceOptn = document.getElementById('hidPriceOpIDBP').value;
            if (PriceOptn != "" && PriceOptn != "0") {
                Page = Page + '?RfpPriceID=' + PriceOptn;
                newwin = window.open(Page, 'PriceOptionSel');
            } else {
                alert("Please Select Price Option First");
            }
            return false;

        }

        function ClosePage() {
            window.opener.document.getElementById('btnRefresh').click();
        }


        function RefreshPage() {
            window.location.reload();
        }





    </script>
    <style type="text/css">
        tr.row {
            background-color: #fff;
        }

            tr.row td {
            }

            tr.row:hover td, tr.row.over td {
                background-color: #eee;
            }

        .breakword {
            word-wrap: break-word;
            word-break: break-all;
        }

        a.SavvyLink:link {
            font-family: Verdana;
            color: black;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:visited {
            font-family: Verdana;
            color: Black;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:hover {
            font-family: Verdana;
            color: Red;
            font-size: 11px;
        }

        .SingleLineTextBox {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 14px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }

        .AlternateColorAct1 {
            font-family: Verdana;
            background-color: #dfe8ed;
        }

        .MultiLineTextBoxG {
            font-family: Verdana;
            font-size: 10px;
            width: 320px;
            height: 50px;
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

        .SingleLineTextBox_G {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 15px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }

        .divUpdateprogress_SavvyPro {
            left: 510px;
            top: 300px;
            position: absolute;
        }
    </style>
    <style type="text/css">
        .SPModule {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../Images/SavvyPackPRO.gif');
            height: 50px;
            width: 1200px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }

        .iframe-auto-height {
            display: block;
            border: none;
            height: 100vh;
            width: 100%;
            padding-top: 30px;
        }

        .iframe-container {
            overflow: hidden;
            /* 16:9 aspect ratio */
            /*padding-top: 56.25%;*/
            position: relative;
            height: 100%;
        }

            .iframe-container iframe {
                border: 0;
                height: 100%;
                left: 0;
                position: absolute;
                top: 0;
                width: 100%;
                padding-top: 30px;
            }

        .divUpdateprogress {
            left: 510px;
            top: 300px;
            position: absolute;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <%--<asp:Image ImageAlign="AbsMiddle" Width="1350px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />--%>
        <div id="MasterContent">
            <div>
                <table class="SPModule" id="SPtable" runat="server" cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 1200px">
                    <tr>
                        <td style="padding-left: 410px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/ChartN.gif" Enabled="true"
                                            runat="server" ToolTip="chart" Visible="true" />
                                    </td>

                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>

        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divUpdateprogress_SavvyPro">
                            <table>
                                <tr>
                                    <td>
                                        <img alt="" src="../../Images/Loading4.gif" height="50px" />
                                    </td>
                                    <td>
                                        <b style="color: Red;">Updating the Record</b>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                    <table>
                        <tr>
                            <td>
                                <img alt="" src="../../Images/Loading4.gif" height="50px" />
                            </td>
                            <td>
                                <b style="color: Red;">Loading Charts...</b>
                            </td>
                        </tr>
                    </table>
                </div>
                <table style="width: 100%; font-family: Verdana; font-size: 14px;">
                    <tr valign="top" style="background-color: #dfe8ed;">
                        <td>
                            <asp:Panel ID="pnlRFPMng" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="width: 26%;">
                                            <div id="divContact" style="margin-left: 10px;">
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <b>Selector:</b>
                                                        </td>
                                                        <td>
                                                            <asp:LinkButton ID="lnkSelRFP" runat="server" Text="Nothing Selected" Font-Bold="true"
                                                                CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectRFP.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                        <%--<td>
                                        <div id="divAddRFP" style="margin-left: 10px;">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnAddRFP" runat="server" Width="155px" Text="Create RFP" OnClientClick="return MakeVisible('trCreate');" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>--%>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Panel ID="RfpDetail" runat="server" Visible="false">
                                                <table style="margin-left: 10px;">
                                                    <tr>
                                                        <td>
                                                            <b>Type: </b>
                                                            <asp:Label ID="lblType" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 120px;">
                                                            <b>Number: </b>
                                                            <asp:Label ID="lblSelRfpID" runat="server"></asp:Label>
                                                        </td>
                                                        <td style="padding-left: 120px;">
                                                            <b>Description:</b>
                                                            <asp:Label ID="lblSelRfpDes" runat="server"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table style="width: 100%; overflow: auto; height: 850px;">
                    <tr class="AlterNateColor2">
                        <td>
                            <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                                Analyze Proposals
                            </div>
                        </td>
                    </tr>
                    <tr class="AlterNateColor2">
                        <td style="padding-left: 10px; padding-top: 15px;">
                            <ajaxToolkit:TabContainer ID="tabAnalyzePPSl" Height="750px" runat="server" ActiveTabIndex="0" AutoPostBack="true">

                                <ajaxToolkit:TabPanel runat="server" HeaderText="Terms Analyzer" ToolTip="Terms Analyzer"
                                    ID="tabTermsManager">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnltabTermsManager" runat="server" Style="overflow-x: auto; overflow-y: auto;"
                                            Height="560px">
                                            <table class="ContentPage" id="Table1" runat="server" style="margin-top: 15px; width: 1200px;">
                                                <tr id="Tr3" style="height: 20px" runat="server">
                                                    <td id="Td3" runat="server">
                                                        <div id="Div1" runat="server">
                                                            <div id="Div4" style="text-align: left; overflow: auto; height: 500px; width: 1190px;">
                                                                <div id="Div2">
                                                                    <asp:Label ID="lblNoData" runat="server" Text="No Record Found" Visible="false" CssClass="Error"></asp:Label>
                                                                </div>
                                                                <div id="PageSection1" style="text-align: left;">
                                                                  <asp:Label ID="lblstandard" runat="server" text="Standard Terms:" Font-Bold="True" Style="text-align: right;
                                                                    padding-left: 20px;"></asp:Label>
                                                                    <asp:Table Width="100%" ID="tblEditQ" runat="server">
                                                                    </asp:Table>
                                                                     <asp:Label ID="lblcustomize" runat="server" text="Customize Terms:" Font-Bold="True" Style="text-align: right;
                                                                    padding-left: 20px;"></asp:Label>
                                                                     <asp:Table Width="100%" ID="tblcustomize" runat="server">
                                                                    </asp:Table>
                                                                </div>
                                                                 <div id="Div20" style="text-align: left; margin-top: 30px;">
                                                                    <b>Additional Terms:</b>
                                                                    <br />
                                                                    <asp:Table Width="100%" ID="tblAddTerm" runat="server">
                                                                    </asp:Table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel runat="server" HeaderText="Price" ToolTip="Price" ID="tabPriceAnlyzer">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPriceAnlyzer" runat="server" Style="overflow: auto;" Height="560px">
                                            <table class="ContentPage" id="Table2" runat="server" style="margin-top: 15px; width: 1012px;">
                                                <tr id="Tr1" style="height: 20px" runat="server">
                                                    <td id="Td1" runat="server">
                                                        <div id="Div3" runat="server">
                                                            <div id="Div5" style="text-align: left; overflow: auto; height: 500px; width: 980px;">
                                                                <div id="Div6">
                                                                    <asp:Label ID="lblNoRecordPA" runat="server" Text="No Record Found" Visible="false"
                                                                        CssClass="Error"></asp:Label>
                                                                </div>
                                                                <div id="Div7" style="text-align: left;">
                                                                    <asp:Table Width="100%" ID="tblPA" runat="server">
                                                                    </asp:Table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel runat="server" HeaderText="Price & Cost" ToolTip="Price & Cost"
                                    ID="tabPC">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPC" runat="server" Style="overflow-x: auto; overflow-y: auto;"
                                            Height="560px">
                                            <table class="ContentPage" id="Table3" runat="server" style="margin-top: 15px;">
                                                <tr id="Tr2" style="height: 20px" runat="server">
                                                    <td id="Td2" runat="server">
                                                        <div id="Div8" runat="server">
                                                            <div id="Div9" style="text-align: left; overflow: auto; height: 500px;">
                                                                <div id="Div10">
                                                                    <asp:Label ID="lblNoRecordPC" runat="server" Text="No Record Found" Visible="false"
                                                                        CssClass="Error"></asp:Label>
                                                                </div>
                                                                <div id="Div11" style="text-align: left;">
                                                                    <asp:Table Width="100%" ID="tblPC" runat="server">
                                                                    </asp:Table>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel runat="server" HeaderText="Price Analysis" ID="tabPriceOptnAnalyz">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPOA" runat="server" Style="overflow-x: auto; overflow-y: auto;"
                                            Height="560px">
                                            <table id="tblPOA" class="ContentPage" runat="server" style="margin-top: 15px; width: 100%;">
                                                <tr id="Tr4" runat="server">
                                                    <td id="Td4" runat="server">
                                                        <div id="div12">
                                                            <table>
                                                                <tr>
                                                                    <td style="vertical-align: top;">
                                                                        <b>Price Selector:</b>
                                                                        <asp:LinkButton ID="lnkSelRFPPO" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                                            CssClass="SavvyLink" OnClientClick="return ShowPriceOpPopUp('PopUp/PriceSelection.aspx?Id=hidPriceOpID&Des1=hidPriceOpNm&Des=tabAnalyzePPSl_tabPriceOptnAnalyz_lnkSelRFPPO&Type=PA')"></asp:LinkButton>                                                                        
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnPriceoptnSLV" runat="server" Text="Check SKU level value" Visible="false" CssClass="ButtonWMarigin" />
                                                                    </td>
                                                                    <td style="padding-left: 60px; vertical-align: top;">
                                                                        <div id="divHeader" class="divHeader" onclick="toggleDiv('divContent', 'img1')">
                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                <tr class="HTR">
                                                                                    <td class="TdLeft">Vendor Dispaly  
                                                                                    </td>
                                                                                    <td class="TdRight">
                                                                                        <img src="../../Images/down.png" class="HeaderImg" id="img1" alt="" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div id="divContent" class="divContent">
                                                                            <div>
                                                                                <%    
                                                                                    Try
                                                                                        If tabAnalyzePPSl.ActiveTabIndex = "3" Then
                                                                                            Dim VendorHide As New Integer
                                                                                            For VendorHide = 1 To VendorCnt
                                                                                                Response.Write("<Input type='checkbox' id='chkBox_" & (VendorHide + 2) & "' value='1' checked='true' onclick='showhideColumn (" & CStr(VendorHide + 2) & ")'>")
                                                                                                Response.Write("" & VendorDes(VendorHide - 1) & "<br/>")
                                                                                            Next
                                                                                        End If
                                                                                    Catch ex As Exception
                                                                                      
                                                                                    End Try
                                                                                %>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                    <td style="padding-left: 260px; vertical-align: top;">
                                                                        <div id="divHeader1" class="divHeader" onclick="toggleDiv('divContent1', 'img2')">
                                                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                                                <tr class="HTR">
                                                                                    <td class="TdLeft">Price Display  
                                                                                    </td>
                                                                                    <td class="TdRight">
                                                                                        <img src="../../Images/down.png" class="HeaderImg" id="img2" alt="" />
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </div>
                                                                        <div id="divContent1" class="divContent">
                                                                            <div>
                                                                                <%    
                                                                                    Try
                                                                                        If tabAnalyzePPSl.ActiveTabIndex = "3" Then
                                                                                            Dim PriceOptnHide As New Integer
                                                                                            For PriceOptnHide = 1 To PriceTypeCnt
                                                                                                Response.Write("<Input type='checkbox' id='chkBoxPO_" & (PriceOptnHide - 1) & "' value='1' checked='true' onclick='showhideALL2 (" & CStr(PriceOptnHide - 1) & ")'>")
                                                                                                Response.Write("" & PricetypeDesp(PriceOptnHide - 1) & "<br/>")
                                                                                            Next
                                                                                        End If
                                                                                    Catch ex As Exception
                                                                                        
                                                                                    End Try
                                                                                %>
                                                                            </div>
                                                                        </div>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr5" runat="server">
                                                    <td id="Td5" runat="server">
                                                        <div id="Div14" style="text-align: left; overflow: auto; height: 500px;">
                                                            <div id="Div15">
                                                                <asp:Label ID="lblNoPriceOp" runat="server" Text="Please Select Price first." Visible="False"
                                                                    CssClass="Error"></asp:Label>
                                                                <asp:Label ID="lblNoRecordPOA" runat="server" Text="No Record Found" Visible="False"
                                                                    CssClass="Error"></asp:Label>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <div id="Div17" style="text-align: left;">
                                                                <asp:Table Width="100%" ID="tblPO" runat="server"></asp:Table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel runat="server" HeaderText="Structure Analysis" ID="tabStructANLZ">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlStruct" runat="server" Style="overflow-x: auto; overflow-y: auto;"
                                            Height="560px">
                                            <table id="Table4" class="ContentPage" runat="server" style="margin-top: 15px; width: 100%;">
                                                <tr id="Tr6" runat="server">
                                                    <td id="Td6" runat="server">
                                                        <div id="div13">
                                                            <table>
                                                                <tr>
                                                                    <td style="vertical-align: top;">

                                                                        <b>Select Master Group:</b>
                                                                        <asp:LinkButton ID="lnkSelMasterGrp" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                                            CssClass="SavvyLink" OnClientClick="return ShowMasterGroupPopUp('PopUp/MGroupSelection.aspx?MID=hidMasterGrpID&MDes=hidMasterGrpDes&MInner=tabAnalyzePPSl_tabStructANLZ_lnkSelMasterGrp&Type=S')"></asp:LinkButton>
                                                                    </td>
                                                                     <td>
                                                                        <asp:Button ID="btnCheckSkuDataStruct" runat="server" Text="Check SKU level value" CssClass="ButtonWMarigin" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr7" runat="server">
                                                    <td id="Td7" runat="server">
                                                        <div id="Div21" style="text-align: left; overflow: auto; height: 500px;">
                                                            <div id="Div22">
                                                                <asp:Label ID="lblStructE" runat="server" Text="Please Select Master Group." Visible="False"
                                                                    CssClass="Error"></asp:Label>
                                                                <asp:Label ID="lblNoStruct" runat="server" Text="No Record Found" Visible="False"
                                                                    CssClass="Error"></asp:Label>
                                                            </div>
                                                            <br />
                                                            <br />
                                                            <div id="Div23" style="text-align: left;">
                                                                <asp:Table Width="100%" ID="tblStructA" runat="server"></asp:Table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel ID="tabBatch" HeaderText="Batch Price Setup2" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlBatch" runat="server" Style="overflow-x: auto; overflow-y: auto;"
                                            Height="560px">
                                            <table id="TblBatch" class="ContentPage" runat="server" style="margin-top: 15px; width: 100%;">
                                                <tr id="Tr8" runat="server">
                                                    <td id="Td8" runat="server">
                                                        <div id="div16">
                                                            <table>
                                                                <tr>
                                                                    <td style="vertical-align: top;">
                                                                        <b>Price Selector:</b>
                                                                        <asp:LinkButton ID="lnkPriceOpBatch" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                                            CssClass="SavvyLink" Style="margin-right: 20px;" OnClientClick="return ShowPriceOpPopUp('PopUp/PriceSelection.aspx?Id=hidPriceOpIDBP&Des1=hidPriceOpNmBP&Des=tabAnalyzePPSl_tabBatch_lnkPriceOpBatch&Type=BPS2')"></asp:LinkButton>
                                                                        <%--<b>Group Selection:</b>--%>
                                                                        <asp:LinkButton ID="lnkGrpSelectnB" runat="server" Text="Nothing Selected" Font-Bold="True" Visible="false"
                                                                            CssClass="SavvyLink" OnClientClick="return ShowGrpBPopUp('../../Charts/SavvyPackProCharts/PopUp/Groupbatchselection.aspx?Id=hidGroupId&Des1=hidGroupDes&Des=tabAnalyzePPSl_tabBatch_lnkGrpSelectnB')"></asp:LinkButton>
                                                                    </td>
                                                                    <%--<td>
                                                                                <asp:LinkButton ID="lnkChart" runat="server" Text="Generate multiple chart" Font-Bold="True"
                                                                                    CssClass="SavvyLink" OnClientClick="return ShowPopUpChart('PopUp/AllBatchChart.aspx')"></asp:LinkButton>

                                                                            </td>--%>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr9" runat="server">
                                                    <td id="Td9" runat="server">
                                                        <div id="Div25" style="text-align: left; overflow: auto; height: 500px;">
                                                            <div id="Div26">
                                                                <asp:Label ID="lblNoPOBatch" runat="server" Text="Please Select Price Option" Visible="False"
                                                                    CssClass="Error"></asp:Label>
                                                                <asp:Label ID="lblNoRcrdBatch" runat="server" Text="No Record Found" Visible="False"
                                                                    CssClass="Error"></asp:Label>
                                                            </div>
                                                            <br />
                                                            <div id="Div27" style="text-align: left;">
                                                                <table style="width: 100%; height: 100%;">
                                                                    <tr>
                                                                        <td style="width: 50%; height: 100%; vertical-align: top;">
                                                                            <asp:Table Width="100%" ID="tblBatchPrice" runat="server"></asp:Table>
                                                                        </td>
                                                                        <td style="width: 50%; height: 100%; vertical-align: top;">
                                                                           <%-- <div class="iframe-container">
                                                                                <iframe id="ifrDownloadPage" runat="server"></iframe>
                                                                            </div>--%>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                                <ajaxToolkit:TabPanel ID="tabSumUp" runat="server" HeaderText="SumUp">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlSumUp" runat="server" Style="overflow-x: auto; overflow-y: auto;"
                                            Height="560px">
                                            <table id="TblSUB" class="ContentPage" runat="server" style="margin-top: 15px; width: 100%;">
                                                <tr>
                                                    <td>
                                                        <div id="Div18" style="text-align: left; overflow: auto; height: 500px;">
                                                            <asp:Label ID="lblNoSumUpFP" runat="server" Text="Please Select Price For Sumup." Visible="False"
                                                                CssClass="Error"></asp:Label>
                                                            <asp:Label ID="lblNoSumupFnd" runat="server" Text="No Record Found" Visible="False"
                                                                CssClass="Error"></asp:Label>
                                                            <br />
                                                            <br />
                                                            <div id="Div19" style="text-align: left; vertical-align: top;">
                                                                <asp:Table Width="100%" ID="tblSumup" runat="server"></asp:Table>
                                                            </div>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>

                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
                <asp:HiddenField ID="hidRfpID" runat="server" />
                <asp:HiddenField ID="hidRfpNm" runat="server" />
                <asp:HiddenField ID="hidSurveyId" runat="server" />
                <asp:HiddenField ID="hidPricetypeCnt" runat="server" />
                <asp:HiddenField ID="hidGrpCnt" runat="server" />
                <asp:HiddenField ID="hidPriceOpID" runat="server" />
                <asp:HiddenField ID="hidPriceOpNm" runat="server" />
                <asp:HiddenField ID="hidRfpUnit" runat="server" />
                <asp:HiddenField ID="hidMasterGrpID" runat="server" />
                <asp:HiddenField ID="hidMasterGrpDes" runat="server" />
                <asp:HiddenField ID="hidStructCount" runat="server" />
                <asp:HiddenField ID="hidGroupDes" runat="server" />
                <asp:HiddenField ID="hidGroupId" runat="server" />
                <asp:HiddenField ID="hidPriceOpIDBP" runat="server" />
                <asp:HiddenField ID="hidPriceOpNmBP" runat="server" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnBatchPageRef" EventName="Click" />
            </Triggers>
           <%-- <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnrefresh" EventName="Click" />
            </Triggers>--%>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnMasterSel" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
        <asp:Button ID="btnRefreshVList" runat="server" Style="display: none;" />
        <asp:Button ID="btnrefreshSeq" runat="server" Style="display: none;" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
        <asp:Button ID="btnMasterSel" runat="server" Style="display: none;" />
        <asp:Button ID="btnBatchPageRef" runat="server" Style="display: none;" />
    </form>
</body>
</html>
