<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SkuManager.aspx.vb" Inherits="Pages_SavvyPackPro_SkuManager" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SKU Manager</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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
            left: 410px;
            top: 300px;
            position: absolute;
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

        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?

                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
            }
        }

        function ShowPopWindowEditG(Page) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 850;
            var height = 600;
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

        function ValidateMasterGrp() {

            var name = document.getElementById("tabSkuManager_tabGrpManager_txtMGNm").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter Master Group Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }

        function ValidateGroup() {


            var name = document.getElementById("tabSkuManager_tabGrpManager_txtGrpName").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter Group Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }

        function ValidateDDLMaster() {

            var TypeSel = document.getElementById("tabSkuManager_tabGrpManager_ddlGrpByMGId").value;

            if (TypeSel == "0") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please Select Group.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
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

        function ShowGroupPopUp(Page) {
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
            newwin = window.open(Page, 'SelUpdateGrp', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }

        function CheckAllObject(objRef, ClassNm) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var spanList = GridView.getElementsByClassName(ClassNm);
            for (var i = 0; i < spanList.length; i++) {
                var input = spanList[i].childNodes[0];
                if (objRef.checked && !input.disabled) {
                    input.checked = true;
                }
                else {
                    input.checked = false;
                }
            }
        }






     
            function RefreshPage() {
                window.location.reload();

                window.close();
            }

     




    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <asp:Image ImageAlign="AbsMiddle" Width="1200px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
            runat="server" />
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
                <table style="width: 1200px; font-family: Verdana; font-size: 14px;">
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
                                                            <asp:LinkButton ID="lnkSelRFP" runat="server" Text="Nothing Selected" Font-Bold="true"
                                                                CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectRFP.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
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
                <table style="width: 1200px;">
                    <tr class="AlterNateColor2">
                        <td style="padding-left: 10px; padding-top: 15px;">
                            <ajaxToolkit:TabContainer ID="tabSkuManager" Height="750px" runat="server" ActiveTabIndex="0"
                                AutoPostBack="true" Enabled="false">
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Select SKU's" ID="tabBMSpec" TabIndex="0">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlBMSpec" runat="server">
                                            <table class="ContentPage" id="tblBMSpec" runat="server" style="margin-top: 15px;">
                                                <tr id="Tr7" runat="server">
                                                    <td id="Td7" runat="server">
                                                        <div class="PageHeading" id="div7" runat="server" style="text-align: center; height: 20px;">
                                                            Select SKU's
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr9" runat="server">
                                                    <td id="Td1" style="padding-top: 10px;" runat="server">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label3" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right;
                                                                        font-size: 12px;"></asp:Label><asp:DropDownList ID="ddlSpec" runat="server" Width="55px"
                                                                            CssClass="DropDown" AutoPostBack="True">
                                                                            <asp:ListItem Value="1">10</asp:ListItem>
                                                                            <asp:ListItem Value="2">25</asp:ListItem>
                                                                            <asp:ListItem Value="3">50</asp:ListItem>
                                                                            <asp:ListItem Value="4">100</asp:ListItem>
                                                                            <asp:ListItem Value="5">200</asp:ListItem>
                                                                            <asp:ListItem Value="6">300</asp:ListItem>
                                                                            <asp:ListItem Value="7">400</asp:ListItem>
                                                                            <asp:ListItem Value="8">500</asp:ListItem>
                                                                            <asp:ListItem Value="9">1000</asp:ListItem>
                                                                        </asp:DropDownList>
                                                                    <asp:Label ID="lblRecoed" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right;
                                                                        padding-left: 20px;"></asp:Label><asp:Label ID="lblNoOfSpec" runat="server" CssClass="NormalLabel"
                                                                            ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSpecKeyword" runat="server" Text="Keyword:" Style="margin-left: 80px;"
                                                                        Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox ID="txtSpecKeyword" runat="server"
                                                                            CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button ID="btnSpecSearch"
                                                                                runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 430px;">
                                                            <asp:Label ID="lblSpecNoFound" runat="server" Visible="False" Text="No records found."></asp:Label>
                                                            <asp:GridView Width="1150px" CssClass="grdProject" runat="server" ID="grdSpec" DataKeyNames="SKUID"
                                                                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Font-Size="11px"
                                                                CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                                                                BorderStyle="None" BorderWidth="1px" >
                                                                <PagerSettings Position="Top" />
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <%-- <HeaderTemplate>
                                                                                        <input id="HeaderLevelCheckBox" runat="server" onclick="javascript: CheckAllObject(this,'SelectAllSKU');" type="checkbox" />
                                                                                    </HeaderTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />--%>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSku" runat="server" OnCheckedChanged="chkchangedSku" AutoPostBack="true"
                                                                                CssClass="SelectAllSKU" />
                                                                        </ItemTemplate>
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SKU #" SortExpression="SKUID">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSKUID" runat="server" Text='<%# Bind("SKUID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="40px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="SKU Des" SortExpression="SKUDES">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSkuDes" runat="server" Text='<%# bind("SKUDES")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Spec Files">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkSpecFIle" runat="server" CssClass="SavvyLink" Text="Files"></asp:LinkButton>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Product Type" SortExpression="PRODUCTTYPE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblProdTYPE" runat="server" Visible="true" Text='<%# Bind("PRODUCTTYPE")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="160px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Package Type" SortExpression="PACKAGETYPE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblPACKTYPE" runat="server" Visible="true" Text='<%# Bind("PACKAGETYPE")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="160px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Width" SortExpression="WIDTH">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWIDTH" runat="server" Visible="true" Text='<%# Bind("WIDTH")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Gusset" SortExpression="LENGTH">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblLNGTH" runat="server" Visible="true" Text='<%# bind("LENGTH")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Height" SortExpression="HEIGHT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblHEIGHT" runat="server" Visible="true" Text='<%# Bind("HEIGHT")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Weight" SortExpression="WEIGHT">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblWEIGHT" runat="server" Visible="true" Text='<%# bind("WEIGHT")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Structure" SortExpression="STRUCTURE">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblSTRUCTURE" runat="server" Visible="true" Text='<%# Bind("STRUCTURE")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="THICKNESS" HeaderText="THICKNESS" SortExpression="THICKNESS">
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="VOLUME" HeaderText="Volume" SortExpression="VOLUME">
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="PRICE" HeaderText="Price" SortExpression="PRICE">
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SETUP" HeaderText="Setup" SortExpression="SETUP">
                                                                        <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                                                    HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel ID="tabGrpManager" runat="server" HeaderText="Group Manager"
                                    TabIndex="1">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlGrpMngr" runat="server">
                                            <table class="ContentPage" id="tblGrpMngr" runat="server" style="margin-top: 15px;">
                                                <tr id="Tr8" runat="server">
                                                    <td id="Td6" runat="server">
                                                        <div class="PageHeading" id="div1" runat="server" style="text-align: center; height: 20px;">
                                                            Group Manager
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr2" runat="server">
                                                    <td id="Td2" style="width: 26%; padding-top: 10px;" runat="server">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <b>Select Master Group:</b>
                                                                </td>
                                                                <td>
                                                                    <asp:LinkButton ID="lnkSelMasterGrp" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                                        CssClass="SavvyLink" OnClientClick="return ShowMasterGroupPopUp('PopUp/SelectMasterGroup.aspx?MID=hidMasterGrpID&MDes=hidMasterGrpDes&MInner=tabSkuManager_tabGrpManager_lnkSelMasterGrp&Type=B')"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" runat="server">
                                                    <td id="Td3" runat="server">
                                                        <asp:Panel ID="pnlHeade1r" runat="server" Height="30px" Style="cursor: pointer; margin-top: 15px;">
                                                            <div style="font-weight: bold;">
                                                                <asp:ImageButton CausesValidation="False" ID="Properties_ToggleImage" ImageAlign="AbsBottom"
                                                                    runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="expand" Width="16px" />
                                                                Advance Search
                                                            </div>
                                                        </asp:Panel>
                                                        <asp:Panel ID="pnlConntent1" runat="server" Height="0px" Style="cursor: pointer;">
                                                            <div>
                                                                <div style="vertical-align: middle; font-weight: bold; text-align: left; background-color: #C0C9E7;">
                                                                    <table>
                                                                        <tr>
                                                                            <td style="width: 20%;">
                                                                                <asp:Label ID="lblSKUDes" runat="server" Text="SKU Description: " Font-Bold="True"
                                                                                    Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtSKUDes" Style="width: 200px; font-size: 11px;" MaxLength="25"
                                                                                    runat="server" CssClass="LongTextBox"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5%;">
                                                                                <asp:Label ID="lblWidth" runat="server" Text="Width: " Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtWidth" Style="width: 200px; font-size: 11px;" MaxLength="25"
                                                                                    runat="server" CssClass="LongTextBox"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5%;">
                                                                                <asp:Label ID="lblGusst" runat="server" Text="Gusst: " Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtGusst" Style="width: 200px; font-size: 11px;" MaxLength="25"
                                                                                    runat="server" CssClass="LongTextBox"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td style="width: 5%;">
                                                                                <asp:Label ID="lblHeight" runat="server" Text="Height: " Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                            </td>
                                                                            <td>
                                                                                <asp:TextBox ID="txtHeight" Style="width: 200px; font-size: 11px;" MaxLength="25"
                                                                                    runat="server" CssClass="LongTextBox"></asp:TextBox>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </div>
                                                                <table width="100%" style="background-color: #C0C9E7;">
                                                                    <tr>
                                                                        <td style="width: 11%;">
                                                                        </td>
                                                                        <td style="width: 5%;">
                                                                            <asp:Button ID="btnAdvSrch" runat="server" Text="Search" />
                                                                        </td>
                                                                        <td>
                                                                            <asp:RadioButtonList ID="rdbtnLogic" RepeatDirection="Horizontal" Font-Size="12px"
                                                                                runat="server">
                                                                                <asp:ListItem Value="1">AND</asp:ListItem>
                                                                                <asp:ListItem Value="2" Selected="True">OR</asp:ListItem>
                                                                            </asp:RadioButtonList>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </asp:Panel>
                                                        <ajaxToolkit:CollapsiblePanelExtender ID="cpsAddLicense" runat="server" TargetControlID="pnlConntent1"
                                                            CollapseControlID="pnlHeade1r" ExpandControlID="pnlHeade1r" Collapsed="True"
                                                            ImageControlID="Properties_ToggleImage" CollapsedText="Expand" ExpandedText="Collaps"
                                                            ExpandedImage="~/images/collapse.jpg" CollapsedImage="~/images/expand.jpg" SuppressPostBack="True"
                                                            Enabled="True">
                                                        </ajaxToolkit:CollapsiblePanelExtender>
                                                    </td>
                                                </tr>
                                                <tr id="Tr5" runat="server">
                                                    <td id="Td4" style="padding-top: 10px;" runat="server">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPageSizeGM" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right;
                                                                        font-size: 12px;"></asp:Label>
                                                                    <asp:DropDownList ID="ddlPageCountGM" runat="server" Width="55px" CssClass="DropDown"
                                                                        AutoPostBack="True">
                                                                        <asp:ListItem Value="1">10</asp:ListItem>
                                                                        <asp:ListItem Value="2">25</asp:ListItem>
                                                                        <asp:ListItem Value="3">50</asp:ListItem>
                                                                        <asp:ListItem Value="4">100</asp:ListItem>
                                                                        <asp:ListItem Value="5">200</asp:ListItem>
                                                                        <asp:ListItem Value="6">300</asp:ListItem>
                                                                        <asp:ListItem Value="7">400</asp:ListItem>
                                                                        <asp:ListItem Value="8">500</asp:ListItem>
                                                                        <asp:ListItem Value="9">1000</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    <asp:Label ID="lblRcrdGM" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right;
                                                                        padding-left: 20px;"></asp:Label>
                                                                    <asp:Label ID="lblRcrdCountGM" runat="server" CssClass="NormalLabel" ForeColor="Red"
                                                                        Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblKeywordGM" runat="server" Text="Keyword:" Style="margin-left: 40px;"
                                                                        Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                    <asp:TextBox ID="txtKeyWordGM" runat="server" onchange="javascript:CheckSP(this);"
                                                                        CssClass="LongTextBox" MaxLength="50"></asp:TextBox>
                                                                    <asp:Button ID="btnSrchGM" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="Tr6" runat="server">
                                                    <td id="Td5" style="padding-top: 10px;" runat="server">
                                                        <div id="ContentPagemargin" style="overflow: auto; height: 410px;" runat="server">
                                                            <asp:Label ID="lblNORcrdGM" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                                Text="No record found."></asp:Label>
                                                            <asp:Label ID="lblNoMasterGrp" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                                Text="No Master Group Record Found. Please Create One Master Group."></asp:Label>
                                                            <asp:GridView Width="1020px" CssClass="grdProject" runat="server" ID="grdGrpMngr"
                                                                DataKeyNames="SKUID" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                                Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" >
                                                                <PagerSettings Position="Top" />
                                                                <PagerTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                                        Style="color: Black;">First</asp:LinkButton>
                                                                    <asp:Label ID="pmore" runat="server" Text="..."></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;" CommandName="Page"
                                                                        CommandArgument="Prev">Previous</asp:LinkButton>
                                                                    <asp:LinkButton ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                        ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                            ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:Label
                                                                                ID="CurrentPage" runat="server" Text="Label" Style="color: Red;"></asp:Label><asp:LinkButton
                                                                                    ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                        ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                            ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                    ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                        ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                            ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                                ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next" Style="color: #284E98;">Next</asp:LinkButton><asp:Label
                                                                                                                    ID="nmore" runat="server" Text="..."></asp:Label><asp:LinkButton ID="LinkButton4"
                                                                                                                        runat="server" CommandName="Page" CommandArgument="Last" Style="color: Black;">Last</asp:LinkButton>
                                                                </PagerTemplate>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <%-- <HeaderTemplate>
                                                                            <input id="HeaderLevelCheckBox" runat="server" onclick="javascript: SelectAllCheckboxes(this);"
                                                                                type="checkbox" />
                                                                        </HeaderTemplate>--%>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ChkSku" runat="server"></asp:CheckBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="MGRPNM" HeaderText="Master Group" SortExpression="MGRPNM">
                                                                        <ItemStyle Width="120px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:TemplateField HeaderText="Group" SortExpression="GRPNM">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkGrpNm" runat="server" Text='<%# Bind("GRPNM")%>' CssClass="SavvyLink"></asp:LinkButton>
                                                                            <asp:Label ID="lblSkuID" runat="server" Visible="False" Text='<%# Bind("SKUID")%>'></asp:Label>
                                                                            <asp:Label ID="lblGrpID" runat="server" Visible="False" Text='<%# Bind("GRPID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="160px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="CODE" HeaderText="SKUID" SortExpression="CODE">
                                                                        <ItemStyle Width="120px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="SKUDES" HeaderText="SKU Description" SortExpression="SKUDES">
                                                                        <ItemStyle Width="180px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="WIDTH" HeaderText="Width" SortExpression="WIDTH">
                                                                        <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="GUSSET" HeaderText="Gusset" SortExpression="GUSSET">
                                                                        <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="HEIGHT" HeaderText="Height" SortExpression="HEIGHT">
                                                                        <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="WEIGHT" HeaderText="Weight" SortExpression="WEIGHT">
                                                                        <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                                                    HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr4" runat="server">
                                                    <td id="Td8" style="padding-top: 10px;" runat="server">
                                                        <table style="width: 100%;">
                                                            <tr class="AlterNateColor1">
                                                                <td>
                                                                    <asp:Button ID="btnCGrp" runat="server" Text="Create Master Group" CssClass="ButtonWMarigin"
                                                                        ToolTip="Create Master group" OnClientClick="return ShowPopWindowEditG('PopUp/MasterGrpPopup.aspx')" />
                                                                    <asp:Button ID="btnSubgrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                                                        ToolTip="Create group" OnClientClick="return ShowPopWindowEditG('PopUp/EditGroupPopup.aspx')" />
                                                                    <asp:Button ID="btnSelgrp" runat="server" Text="Connect Selected SKU" CssClass="ButtonWMarigin"
                                                                        ToolTip="Select Group to update Checked SKU's" OnClientClick="return MakeVisible('tabSkuManager_tabGrpManager_trConnectSku');" />
                                                                </td>
                                                            </tr>
                                                            <tr id="trConnectSku" style="display: none;" runat="server">
                                                                <td id="Td11" runat="server">
                                                                    <table style="width: 100%; background-color: #7F9DB9;">
                                                                        <tr>
                                                                            <td>
                                                                                <b>Select Group:</b>
                                                                            </td>
                                                                            <td>
                                                                                <asp:DropDownList ID="ddlGrpByMGId" runat="server" Width="120px" CssClass="DropDown">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td>
                                                                            </td>
                                                                            <td>
                                                                                <asp:Button ID="btnConnectSku" Text="Connect" runat="server" CssClass="ButtonWMarigin"
                                                                                    OnClientClick="return ValidateDDLMaster();" />
                                                                                <asp:Button ID="btnCancleConn" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                                                    OnClientClick="return MakeInVisible('tabSkuManager_tabGrpManager_trConnectSku');" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr id="Tr14" class="AlterNateColor3" runat="server">
                        <td id="Td14" class="PageSHeading" align="center" runat="server">
                            <asp:Label ID="lblFooter" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HiddenField ID="hidSortIdBSpec" runat="server" />
                <asp:HiddenField ID="hidRfpID" runat="server" />
                <asp:HiddenField ID="hidRfpNm" runat="server" />
                <asp:HiddenField ID="hidSortIdGrpMngr" runat="server" />
                <asp:HiddenField ID="hidMasterGrpID" runat="server" />
                <asp:HiddenField ID="hidMasterGrpDes" runat="server" />
                <asp:HiddenField ID="hidMTypeID" runat="server" />
                <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
                <asp:Button ID="btnMasterSel" runat="server" Style="display: none;" />


                 <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
