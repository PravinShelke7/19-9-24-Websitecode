<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SupplierPrice.aspx.vb" Inherits="Pages_SavvyPackPro_SupplierPrice" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Supplier Price</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
      <style type="text/css">
        .SPModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../Images/SavvyPackPRO.gif');
            height: 50px;
            width: 1200px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        </style>
    <script type="text/JavaScript">
        function ShowPopWindow(Page) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 150;
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

        function ShowPopWindow_PriceCost(Page) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1100;
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
            newwin = window.open(Page, 'PriceCost', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;

        }
        function ShowPopWindow_Price(Page) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 560;
            var height = 250;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Price', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;

        }

        function ClosePage() {
            window.opener.document.getElementById('btnRefresh').click();
        }
    </script>
    <script type="text/JavaScript">
        function CheckSP(text, hidvalue) {
            var sequence = document.getElementById(text.id).value;
            if (sequence != "") {
                if (isNaN(sequence)) {
                    alert("Sequence must be in number");
                    document.getElementById(text.id).value = "";
                    document.getElementById(text.id).value = hidvalue;
                    return false;
                }
            }
            else {
                alert("Please enter sequence");
                document.getElementById(text.id).value = hidvalue;
                return false;
            }

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
                document.getElementById('btnSeq').click();
                return true;
            }
        }

    </script>
    <style type="text/css">
        tr.row
        {
            background-color: #fff;
        }
        
        tr.row td
        {
        }
        
        tr.row:hover td, tr.row.over td
        {
            background-color: #eee;
        }
        
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
        
        a.SavvyLink:link
        {
            font-family: Verdana;
            color: black;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            font-family: Verdana;
            color: Black;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            font-family: Verdana;
            color: Red;
            font-size: 11px;
        }
        
        .SingleLineTextBox
        {
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
        
        .AlternateColorAct1
        {
            font-family: Verdana;
            background-color: #dfe8ed;
        }
        
        .MultiLineTextBoxG
        {
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
        
        .SingleLineTextBox_G
        {
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
        
        .divUpdateprogress_SavvyPro
        {
            left: 610px;
            top: 400px;
            position: absolute;
        }
        .style1
        {
            width: 150px;
            height: 20px;
        }
        .style2
        {
            height: 20px;
        }
    </style>
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

        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;


        }

        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }

        function ValidateList() {


            var name = document.getElementById("txtRFPNm").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter RFP Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
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

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
   <%-- <asp:Image ImageAlign="AbsMiddle" Width="1200px" Height="45px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />--%>

        <div id="MasterContent">
        <div>
            <table class="SPModule" id="SPtable" runat="server" cellpadding="0" cellspacing="0" style="border-collapse: collapse;width:1200px">
                <tr>
                    <td style="padding-left: 410px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif" Enabled="true" 
                                        runat="server" ToolTip="Update" Visible="true" />
                                    <asp:Button ID="btnExtrapSkuLvl" runat="server" Text="Extrapolate To Sku" BorderStyle="Ridge" Style="margin-left: 10px;"
                                            CssClass="ButtonWMarigin" />
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
    <table style="width: 848px; font-family: Verdana; font-size: 14px;">
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
                                                <b>Type Selector:</b>
                                            </td>
                                            <td>
                                               <%-- <asp:LinkButton ID="lnkSelRFP" runat="server" Text="Nothing Selected" Font-Bold="true"
                                                    CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectRFP.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>--%>
                                                    <asp:Label ID="lbSelRFP" runat="server"></asp:Label>
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
                        <%--<tr style="display: none;" id="trCreate" runat="Server">
                                    <td colspan="2">
                                        <table style="width: 100%; background-color: #7F9DB9; margin-left: 10px;">
                                            <tr>
                                                <td>
                                                    <b>RFP Name:</b>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRFPNm" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                        width: 250px" MaxLength="25"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>RFP Description:</b>
                                                </td>
                                                <td>
                                                    <asp:TextBox TextMode="MultiLine" ID="txtRfpDes" runat="server" CssClass="MediumTextBox"
                                                        Style="text-align: left; height: 40px; width: 380px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnCreateRFP" runat="server" Text="Add RFP" CssClass="ButtonWMarigin"
                                                        OnClientClick="return ValidateList();" />
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                        OnClientClick="return MakeInVisible('trCreate');" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>--%>
                        <tr>
                            <td>
                                <asp:Panel ID="RfpDetail" runat="server" Visible="false">
                                    <table style="margin-left: 10px;">
                                        <tr>
                                            <td>
                                                <b>Type:</b>
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
     
    <div id="divCreate" style="margin-top: 0px; margin-left: 2px;" runat="server" visible="true">
        <table style="width:848px">
            <tr class="AlterNateColor4">
                <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                    Price Details
                </td>
            </tr>
              <tr class="AlterNateColor1" id="Tr1" runat="server" visible="true">
                <td align="right" class="style1" style="width:100px;font-weight:bold;">
                    Price name: 
                </td>
                <td class="style2">
                     <asp:Label ID="lblPriceName" runat="server" style="margin-left:5px;"></asp:Label>
                 
                </td>
            </tr>
                      <tr class="AlterNateColor5">
                <td colspan="2">
                    <asp:Table ID="tblPRICE" runat="server" CellPadding="0" CellSpacing="1" Style="margin-left: 50px;">
                    </asp:Table>
                </td>
            </tr>
        </table>
        <br />
    </div>
    <div id="divReportFrameWork" style="margin-top: 0px; margin-left: 4px;" runat="server"
        visible="true">
        <br />
        <asp:Button ID="btnAddNew" runat="server" CssClass="Button" Text="Save" Style="margin: 0 0 0 0;"
            Visible="false" />
    </div>
    <br />
    <asp:HiddenField ID="hidSortIdBSpec" runat="server" />
    <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
    <asp:HiddenField ID="hidRfpID" runat="server" />
    <asp:HiddenField ID="hidRfpNm" runat="server" />
    <asp:Button ID="btnrefreshT" runat="server" Style="display: none;" />
    <asp:Button ID="btnrefresh" runat="server" Style="display: none;" />
    <asp:HiddenField ID="hidSurveyId" runat="server" />
    <asp:HiddenField ID="hidRowC" runat="server" />
    <asp:HiddenField ID="hidColumnC" runat="server" />
    <asp:HiddenField ID="hidFilterC" runat="server" />
    <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
    <asp:Button ID="btnRefreshVList" runat="server" Style="display: none;" />
    <asp:Button ID="btnrefreshSeq" runat="server" Style="display: none;" />
     <asp:HiddenField ID="hidPriceID" runat="server" />
    </form>
</body>
</html>
