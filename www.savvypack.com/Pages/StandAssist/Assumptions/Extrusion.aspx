<%@ Page Title="Structure Assistant- Material and Structure" Language="VB" MasterPageFile="~/Masters/StandAssist.master"
    AutoEventWireup="false" CodeFile="Extrusion.aspx.vb" Inherits="Pages_StandAssist_Assumptions_Extrusion1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="StandAssistContentPlaceHolder" runat="Server">
    <script type="text/javascript" src="../../../javascripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/StructAssist1.js"></script>
    <style type="text/css">
        .StructTextBox
        {
            font-family: Optima;
            font-size: 11.5Px;
            height: 15px;
            width: 70px;
            background-color: #FEFCA1;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: right;
        }
        .TdHeadingNew
        {
            background-color: #58595B;
            color: White;
            font-family: Optima;
            font-size: 12px;
        }
        
        .MyTabStyle .ajax__tab_active .ajax__tab_tab
        {
            color: Black !important;
            background-color: #D3E7CB !important;
            background-image: none !important;
            font-family: "Helvetica Neue" , Arial, Sans-Serif;
            font-size: 14px;
            font-weight: bold;
            padding: 4px 4px 4px 4px;
        }
        .MyTabStyle .ajax__tab_tab
        {
            color: #8c8c8c !important;
            background-color: #d9d9d9 !important;
            background-image: none !important;
            font-family: "Helvetica Neue" , Arial, Sans-Serif;
            font-size: 14px;
            font-weight: bold;
            padding: 4px 4px 4px 4px;
        }
    </style>
    <style type="text/css">
        .ajax__myTab .ajax__tab_header
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 11px;
            border-bottom: solid 0px black;
            background-color: #F1F1F2;
            width: 1300px;
        }
        
        .ajax__myTab .ajax__tab_outer
        {
            padding-right: 4px;
            height: 21px;
            background-color: #e6e6e6;
            margin-right: 2px;
        }
        
        .ajax__myTab .ajax__tab_inner
        {
            padding-left: 3px;
            color: #999999;
            background-color: #e6e6e6;
            font-weight: bold;
        }
        
        .ajax__myTab .ajax__tab_tab
        {
            height: 13px;
            padding: 4px;
            margin: 0;
        }
        
        .ajax__myTab .ajax__tab_hover .ajax__tab_outer
        {
            background-color: #cccccc;
        }
        
        .ajax__myTab .ajax__tab_hover .ajax__tab_inner
        {
            background-color: #cccccc;
        }
        
        .ajax__myTab .ajax__tab_hover .ajax__tab_tab
        {
        }
        
        .ajax__myTab .ajax__tab_active .ajax__tab_outer
        {
            background-color: #D3E7CB;
            border-left: solid 0px black;
            border-top: solid 0px black;
            border-right: solid 0px black;
            border-bottom: solid 0px black;
        }
        
        .ajax__myTab .ajax__tab_active .ajax__tab_inner
        {
            background-color: #D3E7CB;
            color: Black;
        }
        
        .ajax__myTab .ajax__tab_active .ajax__tab_tab
        {
        }
        
        .ajax__myTab .ajax__tab_body
        {
            font-family: verdana,tahoma,helvetica;
            font-size: 10pt;
            border: 0px solid black;
            border-top: 0;
            padding: 8px;
            background-color: #D3E7CB;
        }
    </style>
    <script type="text/JavaScript">
        function OpenTDataSheet(SupId, matId, page, gradeid) {

            //alert(gradeid);
            var gId = document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_" + gradeid).value;
            var matId = document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_" + matId).value;
            var SupId = document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_" + SupId).value;
            //alert("MAT " + matId + ' SUP ' + SupId + ' Grade ' + gId);

            if (gId != "0") {
                var width = 1050;
                var height = 600;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=yes';
                params += ', scrollbars=no';
                params += ', status=yes';
                params += ', toolbar=no';

                page = page + "?Sponsor=" + SupId + "&MatId=" + matId + "&GradeId=" + gId
                newwin = window.open(page, 'Chat11', params);
                if (newwin == null || typeof (newwin) == "undefined") {
                    alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                }
            }
            else {

                if (matId == "0") {
                    alert('Please select Material.');
                }
                else {
                    alert('Please select Grade.');
                }
                return false;
            }

            //return false;
        }

        function clientActiveTabChanged(sender, args) {


            document.getElementById("<%=hdnTabNum.ClientID %>").value = sender.get_activeTabIndex();


            //alert(sender.get_activeTabIndex());
        }
        function errorMessage(caseid) {
            alert("Structure " + caseid + " is no longer available.");
            window.close();

        }
        function ShowToolTip(ControlId, Message) {

            document.getElementById(ControlId).onmouseover = function () { Tip(Message); };
            document.getElementById(ControlId).onmouseout = function () { UnTip(''); };

        }
        function ShowSGradePopWindow(Page, GradeId) {

            var width = 760;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var gId = document.getElementById(GradeId).value;
            alert(gId);
            Page = Page + "&GradeId=" + GradeId;

            newwin = window.open(Page, 'Chat', params);

        }
        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1250;
            var height = 700;
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

        }
        function ShowModPopWindow(Page, modTypeId) {

            Page = Page + "&ModId=" + document.getElementById(modTypeId).value;
            var width = 900;
            var height = 400;
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

        }
        function CallBackBarrier() {
            //alert('sud');
            //alert(document.getElementById('ctl00_imgBarProp').src);
            var msgBarr = ""
            var msgBarr1 = ""
            if (document.getElementById("ctl00_StandAssistContentPlaceHolder_hidBarrier").value == "0") {
                document.getElementById("ctl00_StandAssistContentPlaceHolder_hidBarrier").value = "1";
                document.getElementById('ctl00_imgBarProp').title = "Hide Barrier Properties";
                showDisplay();

                msgBarr = document.getElementById("ctl00_StandAssistContentPlaceHolder_hidMessageBarr").value;



                // msgBarr1 = msgBarr.replace("for", "\n");
                msgBarr = msgBarr.split("#").join("\n")
                //alert(msgBarr1);
                if (msgBarr != "") {
                    alert(msgBarr);
                    // alert('Data is not present for following materials for Date 5/1/2007\nResin EVOH (WVTR)\n');
                }
            }
            else {
                document.getElementById("ctl00_StandAssistContentPlaceHolder_hidBarrier").value = "0";
                document.getElementById('ctl00_imgBarProp').title = "Show Barrier Properties";
                showhide();
            }

            return false;
        }



        function ShowGradePopWindow(Page, MatId) {

            var width = 700;
            var height = 380;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var matId = document.getElementById(MatId).value;
            Page = Page + "&MatId=" + matId;
            if (matId == "0") {
                alert('Please select Material.');
            }
            else {
                newwin = window.open(Page, 'Chat', params);
            }


        } 

	function ShowChartWindow(Page) {
          
            var width = 720;
            var height = 470;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
                
            newwin = window.open(Page, '_blank', params);

        }
           
    </script>
    <script type="text/javascript">
        function openWindow(Page) {

            var msg = "The Structure Designer will be closed automatically in order to change preferences. Ok to proceed?"
            if (confirm(msg)) {

                newwin = window.open(Page, 'Chat', "");
                window.close();
                return true;
            }
            else {
                return false;
            }

        }
        function DisplayLayers() {
            showhideALL('tabpnl1', 'OTR');
            showhideALL('tabpnl1', 'WVTR');
            showhideALL('tabpnl1', 'TS1');
            showhideALL('tabpnl1', 'TS2');
        }
        function getValue(Type) {
            if (Type == "OTR") {
                if (document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value == "Y") {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "N";
                    showhideALL('tabpnl1', 'OTR')
                }
                else {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "Y";
                    showhideALL('tabpnl1', 'OTR')
                }
            }
            else if (Type == "WVTR") {
                if (document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value == "Y") {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "N";
                    showhideALL('tabpnl1', 'WVTR')
                }
                else {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "Y";
                    showhideALL('tabpnl1', 'WVTR')
                }
            }
            else if (Type == "TS1") {
                if (document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value == "Y") {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "N";
                    showhideALL('tabpnl1', 'TS1')
                }
                else {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "Y";
                    showhideALL('tabpnl1', 'TS1')
                }
            }
            else if (Type == "TS2") {
                if (document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value == "Y") {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "N";
                    showhideALL('tabpnl1', 'TS2')
                }
                else {
                    document.getElementById("ctl00_StandAssistContentPlaceHolder_hid" + Type).value = "Y";
                    showhideALL('tabpnl1', 'TS2')
                }
            }
        }
    </script>
    <div id="ContentPagemargin" runat="server" style="width: 1320px;">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div id="PageSection" style="text-align: left; width: 100%;">
            <br />
            <table cellspacing="10" style="margin-top: -20px; width: 100%;">
                <tr>
                    <td>
                        <div style="margin-left: 0px; background-color: #D3E7CB;">
                            <ajaxToolkit:TabContainer ID="tabSDesigner" runat="server" ActiveTabIndex="0" CssClass="ajax__myTab"
                                Style="text-align: left;" OnClientActiveTabChanged="clientActiveTabChanged" Width="99%">
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Laboratory Environment" ToolTip="Laboratory Environment"
                                    Style="background-color: #D3E7CB;" ID="tabpnl1">
                                    <ContentTemplate>
                                        <center>
                                            <table width="100%" style="background-color: #D3E7CB;">
                                                <tbody>
                                                    <tr>
                                                        <td style="width: 30%;">
                                                            <table id="tblBarrier" runat="server" style="display: block;">
                                                                <tr id="Tr1" runat="server">
                                                                    <td id="Td1" style="width: 200px; text-align: right;" title="Controlled Environment Temperature"
                                                                        runat="server">
                                                                        <asp:Label ID="lblOTRTemp" runat="server" Style="font-family: Optima; font-size: 12px;
                                                                            height: 12px; width: 100px; margin-right: 9px; margin-top: 2px; margin-bottom: 2px;
                                                                            margin-left: 5px; text-align: left; font-weight: bold">OTR Temprature (°C):</asp:Label>
                                                                        &nbsp;<asp:TextBox ID="txtOTRTemp" runat="server" CssClass="StructTextBox" Width="45px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td2" style="width: 27px" runat="server">
                                                                    </td>
                                                                    <td id="Td3" style="width: 200px; text-align: left;" title="Relative Humidity" runat="server">
                                                                        <asp:Label ID="lblOTRHumidity" runat="server" Style="font-family: Optima; font-size: 12px;
                                                                            height: 12px; width: 100px; margin-right: 0px; margin-top: 2px; margin-bottom: 2px;
                                                                            margin-left: 5px; text-align: left; font-weight: bold">Relative Humidity (%):</asp:Label>
                                                                        <asp:TextBox ID="txtOTRHumidity" runat="server" CssClass="StructTextBox" Width="45px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr id="Tr2" runat="server">
                                                                    <td id="Td4" style="width: 200px; text-align: right;" title="Controlled Environment Temperature"
                                                                        runat="server">
                                                                        <asp:Label ID="lblWVTRTemp" runat="server" Style="font-family: Optima; font-size: 12px;
                                                                            height: 12px; width: 100px; margin-right: 1px; margin-top: 2px; margin-bottom: 2px;
                                                                            margin-left: 5px; text-align: left; font-weight: bold">WVTR Temprature (°C):</asp:Label>
                                                                        <asp:TextBox ID="txtWVTRTemp" runat="server" CssClass="StructTextBox" Width="45px"></asp:TextBox>
                                                                    </td>
                                                                    <td id="Td5" style="width: 27px" runat="server">
                                                                    </td>
                                                                    <td id="Td6" style="width: 200px; text-align: left;" title="Relative Humidity" runat="server">
                                                                        <asp:Label ID="lbWVTRHumidity" runat="server" Style="font-family: Optima; font-size: 12px;
                                                                            height: 12px; width: 100px; margin-right: 0px; margin-top: 2px; margin-bottom: 2px;
                                                                            margin-left: 5px; text-align: left; font-weight: bold">Relative Humidity (%):</asp:Label>
                                                                        <asp:TextBox ID="txtWVTRHumidity" runat="server" CssClass="StructTextBox" Width="45px"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td style="width: 40%;">
                                                            <table>
                                                                <tr>
                                                                    <%--<td>
                                                                        <b>Toggle Column:</b>
                                                                    </td>--%>
                                                                    <td>
                                                                        <a id="lnkOTR" runat="server" style="text-decoration: none; font-size: 10px; margin-left: 10px;"
                                                                            href="javascript:getValue('OTR');">OTR</a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkWVTR" runat="server" style="text-decoration: none; font-size: 10px; margin-left: 20px;"
                                                                            href="javascript:getValue('WVTR');">WVTR</a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkTS1" runat="server" style="text-decoration: none; font-size: 10px; margin-left: 20px;"
                                                                            href="javascript:getValue('TS1');">Tensile at Break MD</a>
                                                                    </td>
                                                                    <td>
                                                                        <a id="lnkTS2" runat="server" style="text-decoration: none; font-size: 10px; margin-left: 20px;"
                                                                            href="javascript:getValue('TS2');">Tensile at Break TD</a>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                                                            </asp:Table>
                                                        </td>
                                                    </tr>
                                                </tbody>
                                            </table>
                                        </center>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Supply Chain Environment" ToolTip="Supply Chain Environment"
                                    Style="background-color: #D3E7CB;" ID="tabpnl2">
                                    <ContentTemplate>
                                        <table width="100%" style="background-color: #D3E7CB;">
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <table id="tblHumidity" runat="server" visible="true" width="100%">
                                                            <tr>
                                                                <td style="height: 4px;">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td title="Distribution Temperature for MVTR and OTR" align="left">
                                                                    <asp:Label ID="lblRHTemp" runat="server" Style="font-family: Optima; font-size: 12px;
                                                                        height: 12px; width: 100px; margin-right: 1px; margin-top: 2px; margin-bottom: 2px;
                                                                        margin-left: 60px; text-align: left; font-weight: bold">Temprature (°C):</asp:Label>
                                                                    <asp:TextBox ID="txtRHTemp" runat="server" CssClass="StructTextBox" Width="45px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="padding-left: 5px" valign="top" colspan="2" align="right">
                                                        <asp:LinkButton ID="lnkPrefRH" Font-Bold="true" Style="font-size: 14px; margin-right: 80px;display:none;"
                                                            runat="server" CssClass="Link" Width="300px" OnClientClick="return openWindow('Preferences.aspx');">Preferences</asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2" Width="950px">
                                                        </asp:Table>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidUpdate" runat="server" />
        <asp:HiddenField ID="hidBarrier" runat="server" />
        <asp:HiddenField ID="OTRTEMP1" runat="server" />
        <asp:HiddenField ID="OTRTEMP2" runat="server" />
        <asp:HiddenField ID="RH1" runat="server" />
        <asp:HiddenField ID="RH2" runat="server" />
        <asp:HiddenField ID="hidMessageBarr" runat="server" />
        <asp:HiddenField ID="hidBarrVal" runat="server" />
        <asp:HiddenField ID="hidPageLoad" runat="server" />
        <asp:HiddenField ID="hidMat" runat="server" />
        <asp:HiddenField ID="hdnTabNum" runat="server" />
        <asp:HiddenField ID="hidOTR" runat="server" />
        <asp:HiddenField ID="hidWVTR" runat="server" />
        <asp:HiddenField ID="hidTS1" runat="server" />
        <asp:HiddenField ID="hidTS2" runat="server" />
    </div>
</asp:Content>
