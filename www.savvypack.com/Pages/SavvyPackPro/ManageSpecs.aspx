<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManageSpecs.aspx.vb" Inherits="Pages_SavvyPackPro_ManageSpecs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Specs</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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

        function ValidateGroup() {


            var name = document.getElementById("tabSupplierManager_tabGrpManager_txtGrpName").value;

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

        function ValidateList1() {

            var Desc = document.getElementById("tabSupplierManager_tabMasterGroup_txtDes").value;
            var name = document.getElementById("tabSupplierManager_tabMasterGroup_txtMGNm").value;
            var TypeSel = document.getElementById("tabSupplierManager_tabMasterGroup_ddlMasterGroup").value; //tabSupplierManager_tabMasterGroup_ddlMasterGroup

            if (name == "") {
                var msg1 = "-------------------------------------------------\n";
                msg1 += "    Please enter Master Group Name.\n";
                msg1 += "-------------------------------------------------\n";
                alert(msg1);
                return false;
            }
            else if (Desc == "") {
                var msg2 = "-------------------------------------------------\n";
                msg2 += "    Please enter Description.\n";
                msg2 += "-------------------------------------------------\n";
                alert(msg2);
                return false;
            }
            else if (TypeSel == "0") {
                var msg3 = "-------------------------------------------------\n";
                msg3 += "    Please select Type.\n";
                msg3 += "-------------------------------------------------\n";
                alert(msg3);
                return false;

            }
            return true;
        }


        function ValidateMasterGrp() {

            var name = document.getElementById("tabSupplierManager_tabGrpManager_txtMGNm").value;

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

        function ValidateSpecGrp() {

            var name = document.getElementById("tabSupplierManager_tabSMSpec_txtGroupDe1").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }

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

//        function SelectAllCheckboxes(spanChk) {
//            var oItem = spanChk.children;
//            var theBox = (spanChk.type == "checkbox") ?

//                spanChk : spanChk.children.item[0];
//            xState = theBox.checked;
//            elm = theBox.form.elements;
//            for (i = 0; i < elm.length; i++) {
//                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
//                    if (elm[i].checked != xState)
//                        elm[i].click();
//                }
//            }
//        }

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
    </style>
    <style type="text/css">
        a.SavvyLink:link
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            color: Red;
            font-size: 11px;
        }
        
        .PSavvyModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background: url('../../Images/AdminHeader.gif') no-repeat;
            height: 45px;
            width: 1220px;
            text-align: center;
            vertical-align: middle;
        }
        
        .PageHeading
        {
            font-size: 18px;
            font-weight: bold;
        }
        
        .ContentPage
        {
            margin-top: 2px;
            margin-left: 1px;
            width: 1220px;
            background-color: #F1F1F2;
        }
        
        #SavvyPageSection1
        {
            background-color: #D3E7CB;
        }
        
        .AlterNateColor3
        {
            background-color: #D3DAD0;
            height: 20px;
        }
        
        .PageSHeading
        {
            font-size: 12px;
            font-weight: bold;
        }
        
        .InsUpdMsg
        {
            font-family: Verdana;
            font-size: 12px;
            font-style: italic;
            color: Red;
            font-weight: bold;
        }
        
        .LongTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 320px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            background-color: #FEFCA1;
        }
        
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        .style2
        {
            width: 9px;
        }
        
        .Comments
        {
            font-family: Optima;
            font-size: 12px;
            height: 16px;
            width: 265px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            background-color: #FEFCA1;
            text-align: left;
        }
        
        .Amount
        {
            font-family: Optima;
            font-size: 11.5Px;
            height: 17px;
            width: 50px;
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
        
        .NormalLable_Term
        {
            font-family: Verdana;
            font-size: 18px;
            height: 15px;
            width: 100px;
            margin-right: 3px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 5px;
            text-align: left;
        }
    </style>
    <script type="text/javascript">
        function ShowMatPopWindow(el, Page) {

            // find all elements that have the linkActive class
            var elems = document.querySelectorAll(".linkActive");

            // loop through them and ...
            for (var i = 0; i < elems.length; i++) {
                // remove the linkActive class
                elems[i].classList.remove('linkActive');
                elems[i].style.color = 'Black';
            }

            // now add the class to the link that was just clicked
            el.classList.add('linkActive');
            el.style.color = 'red';

            var width = 470;
            var height = 420;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            //        var Mid = document.getElementById(Id).value;
            //        Page = Page + "&MatId=" + Mid
            newwin = window.open(Page, 'Chat', params);
            return false;
        }
        function ShowPopWindow(Page) {
            //alert("j0" + matId.toString());              
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
            // var matId = document.getElementById(MatId).value;
            //Page = Page + "&Dese=" + matId;

            newwin = window.open(Page, 'Chat', params);

        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <asp:Image ImageAlign="AbsMiddle" Width="1350px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <ajaxToolkit:TabContainer ID="tabSupplierManager" Height="750px" runat="server" ActiveTabIndex="0"
        AutoPostBack="true">
        <ajaxToolkit:TabPanel ID="tabGrpManager" runat="server" HeaderText="" Visible="false">
            <ContentTemplate>
                <asp:Panel ID="pnlGrpMngr" runat="server">
                    <table class="ContentPage" id="tblGrpMngr" runat="server" style="margin-top: 15px;">
                        <tr id="Tr1" runat="server">
                            <td id="Td1" style="width: 26%; padding-top: 10px;" runat="server">
                                <table>
                                    <tr>
                                        <td>
                                            <b>Select Master Group:</b>
                                        </td>
                                        <td>
                                            <asp:LinkButton ID="lnkSelMasterGrp" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                CssClass="SavvyLink" OnClientClick="return ShowMasterGroupPopUp('PopUp/SelectMasterGroup.aspx?MID=hidMasterGrpID&MDes=hidMasterGrpDes&MInner=tabSupplierManager_tabGrpManager_lnkSelMasterGrp')"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr id="Tr2" runat="server">
                            <td id="Td2" runat="server">
                                <asp:Panel ID="pnlHeade1r" runat="server" Height="30px" Style="cursor: pointer; margin-top: 15px;">
                                    <div style="font-weight: bold;">
                                        <asp:ImageButton CausesValidation="False" ID="Properties_ToggleImage" ImageAlign="AbsBottom"
                                            runat="server" ImageUrl="~/Images/expand.jpg" AlternateText="expand" Width="16px" />
                                        Advance Search
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="pnlConntent1" runat="server" Height="0px" Style="cursor: pointer;">
                                    <div>
                                        <div style="vertical-align: middle; font-weight: bold; text-align: left;">
                                            <asp:Label ID="lblNoData" runat="server" Text="Please add some data in SEARCHCOLUMN table for advance search."></asp:Label>
                                        </div>
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
                            <td id="Td3" style="padding-top: 10px;" runat="server">
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
                            <td id="Td4" style="padding-top: 10px;" runat="server">
                                <div id="ContentPagemargin" style="overflow: auto; height: 410px;" runat="server">
                                    <asp:Label ID="lblNORcrdGM" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                        Text="No record found."></asp:Label>
                                    <asp:Label ID="lblNoMasterGrp" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                        Text="No Master Group Record Found. Please Create One Master Group."></asp:Label>
                                    <asp:GridView Width="1020px" CssClass="grdProject" runat="server" ID="grdGrpMngr"
                                        DataKeyNames="SKUID" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                        Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                        <PagerSettings Position="Top" />
                                        <PagerTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                Style="color: Black;">First</asp:LinkButton><asp:Label ID="pmore" runat="server"
                                                    Text="..."></asp:Label><asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;"
                                                        CommandName="Page" CommandArgument="Prev">Previous</asp:LinkButton><asp:LinkButton
                                                            ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
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
                                                <HeaderTemplate>
                                                    <input id="HeaderLevelCheckBox" runat="server" onclick="javascript: SelectAllCheckboxes(this);"
                                                        type="checkbox" />
                                                </HeaderTemplate>
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
                                            <%-- <asp:BoundField DataField="SKUID" HeaderText="SKU ID" SortExpression="SKUID">
                                                    <ItemStyle Width="30px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:BoundField>--%>
                                            <asp:BoundField DataField="CODE" HeaderText="SKUID" SortExpression="CODE">
                                                <ItemStyle Width="120px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SKUDES" HeaderText="SKU Description" SortExpression="SKUDES">
                                                <ItemStyle Width="180px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WIDTH" HeaderText="Width(in)" SortExpression="WIDTH">
                                                <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="GUSSET" HeaderText="Gusset(in)" SortExpression="GUSSET">
                                                <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="HEIGHT" HeaderText="Height(in)" SortExpression="HEIGHT">
                                                <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="WEIGHT" HeaderText="Weight(g)" SortExpression="WEIGHT">
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
                        <tr id="Tr7" runat="server">
                            <td id="Td8" style="padding-top: 10px;" runat="server">
                                <table style="width: 100%;">
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnCGrp" runat="server" Text="Create Master Group" CssClass="ButtonWMarigin"
                                                ToolTip="Create Master group" OnClientClick="return MakeVisible('tabSupplierManager_tabGrpManager_trMasterGrp');" />
                                            <asp:Button ID="btnSubgrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                                ToolTip="Create group" OnClientClick="return MakeVisible('tabSupplierManager_tabGrpManager_trCreateGrp');" />
                                            <asp:Button ID="btnSelgrp" runat="server" Text="Connect Selected SKU" CssClass="ButtonWMarigin"
                                                ToolTip="Select Group to update Checked SKU's" OnClientClick="return MakeVisible('tabSupplierManager_tabGrpManager_trConnectSku');" />
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="trMasterGrp" runat="server">
                                        <td id="Td9" runat="server">
                                            <table style="width: 100%; background-color: #7F9DB9;">
                                                <tr>
                                                    <td>
                                                        <b>Master Group Name:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMGNm" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                            width: 250px" MaxLength="25" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Description:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox TextMode="MultiLine" ID="txtMGrpDes" runat="server" CssClass="MediumTextBox"
                                                            Style="text-align: left; height: 40px; width: 380px" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Select Type:</b>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMType" CssClass="DropDown" Width="120px" runat="server">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnCreateMasterGrp" runat="server" Text="Create Master Group" CssClass="ButtonWMarigin"
                                                            OnClientClick="return ValidateMasterGrp();" />
                                                        <asp:Button ID="btnCancleMGC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                            OnClientClick="return MakeInVisible('tabSupplierManager_tabGrpManager_trMasterGrp');" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="display: none;" id="trCreateGrp" runat="server">
                                        <td id="Td10" runat="server">
                                            <table style="width: 100%; background-color: #7F9DB9;">
                                                <tr>
                                                    <td>
                                                        <b>Group Name:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGrpName" runat="server" Width="250px" CssClass="MediumTextBox"
                                                            Style="text-align: left;" BackColor="#FEFCA1"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Group Description:</b>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox TextMode="MultiLine" ID="txtGrpDescription" runat="server" MaxLength="499"
                                                            CssClass="MediumTextBox" Style="text-align: left; height: 40px; width: 400px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <b>Select Master Group:</b>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlMGrp" runat="server" Width="120px" CssClass="DropDown">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnCreateGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                                            OnClientClick="return ValidateGroup();" />
                                                        <asp:Button ID="btnCancleGrp" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                            OnClientClick="return MakeInVisible('tabSupplierManager_tabGrpManager_trCreateGrp');" />
                                                    </td>
                                                </tr>
                                            </table>
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
                                                        <asp:Button ID="btnConnectSku" Text="Connect" runat="server" CssClass="ButtonWMarigin" />
                                                        <asp:Button ID="btnCancleConn" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                            OnClientClick="return MakeInVisible('tabSupplierManager_tabGrpManager_trConnectSku');" />
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
        <ajaxToolkit:TabPanel runat="server" HeaderText="SKU Manager" ToolTip="SKU Manager"
            ID="tabSKU">
            <ContentTemplate>
                <asp:Panel ID="pnlSKU" runat="server" DefaultButton="btnSearchSMSku">
                    <table class="ContentPage" id="Table1" runat="server" style="margin-top: 15px;">
                        <tr id="Tr3" runat="server">
                            <td id="Td5" runat="server">
                                <div class="PageHeading" id="div2" runat="server" style="text-align: center;">
                                    SKU Manager
                                </div>
                            </td>
                        </tr>
                        <tr id="Tr4" style="height: 20px" runat="server">
                            <td id="Td6" runat="server">
                                <div style="text-align: left; background-color: #D3E7CB;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td style="font-size: 12px; width: 40%;">
                                                            <asp:Label ID="lblSkuNoFound" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                                                Style="margin-left: 10px;" Font-Bold="True"></asp:Label><asp:TextBox ID="txtSkuSrch"
                                                                    runat="server" CssClass="LongTextBox" onchange="javascript:CheckSP(this);"></asp:TextBox><asp:Button
                                                                        ID="btnSearchSMSku" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr id="trSkuPageCount" runat="server" visible="False">
                                            <td id="Td7" style="font-size: 12px;" runat="server">
                                                <asp:Label ID="Label2" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right;
                                                    margin-left: 10px;"></asp:Label><asp:DropDownList ID="drpSkuPageCount" runat="server"
                                                        Width="55px" CssClass="DropDown" AutoPostBack="True">
                                                        <asp:ListItem Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                        <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                        <asp:ListItem Value="200">200</asp:ListItem>
                                                        <asp:ListItem Value="300">300</asp:ListItem>
                                                        <asp:ListItem Value="400">400</asp:ListItem>
                                                        <asp:ListItem Value="500">500</asp:ListItem>
                                                        <asp:ListItem Value="1000">1000</asp:ListItem>
                                                    </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding-left: 10px;">
                                                <div style="overflow: auto; height: 430px;">
                                                    <asp:Label ID="Label3" runat="server" Visible="False" Text="No records found."></asp:Label><asp:GridView
                                                        Width="1120px" CssClass="grdProject" runat="server" ID="grdsku" DataKeyNames="SKUID"
                                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Font-Size="11px"
                                                        Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                                        <PagerSettings Position="Top" />
                                                        <PagerTemplate>
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                                Style="color: Black;">First</asp:LinkButton><asp:Label ID="pmore" runat="server"
                                                                    Text="..."></asp:Label><asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;"
                                                                        CommandName="Page" CommandArgument="Prev">Previous</asp:LinkButton><asp:LinkButton
                                                                            ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
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
                                                            <asp:TemplateField HeaderText="USERID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                                SortExpression="USERID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblUserID" Width="50px" runat="server" Text='<%# bind("USERID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="20px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SKU ID" SortExpression="SKUID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSKUID" runat="server" Visible="true" Text='<%# Bind("SKUID")%>'></asp:Label>
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
                                                            <asp:TemplateField HeaderText="Product Type" SortExpression="PRODUCTTYPE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProdTYPE" runat="server" Visible="true" Text='<%# Bind("PRODUCTTYPE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Package Type" SortExpression="PACAKAGETYPE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPACKTYPE" runat="server" Visible="true" Text='<%# Bind("PACAKAGETYPE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="90px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Width (in)" SortExpression="WIDTH">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWIDTH" runat="server" Visible="true" Text='<%# Bind("WIDTH")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="60px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gusset (in)" SortExpression="LENGTH">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLNGTH" runat="server" Visible="true" Text='<%# bind("LENGTH")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Height (in)" SortExpression="HEIGHT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHEIGHT" runat="server" Visible="true" Text='<%# Bind("HEIGHT")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight (lb)" SortExpression="WEIGHT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWEIGHT1" runat="server" Visible="true" Text='<%# bind("WEIGHT")%>'></asp:Label>
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
                                                            <asp:TemplateField HeaderText="THICKNESS (mil)" SortExpression="THICKNESS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWEIGHT2" runat="server" Visible="true" Text='<%# Bind("THICKNESS")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Volume (number)" SortExpression="VOLUME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVOLUME" runat="server" Visible="true" Text='<%# bind("VOLUME")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="70px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Price ($/M)" SortExpression="PRICE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRICE1" runat="server" Visible="true" Text='<%# Bind("PRICE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Setup ($/M)" SortExpression="SETUP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRICE2" runat="server" Visible="true" Text='<%# Bind("SETUP")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
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
                                </div>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
        <ajaxToolkit:TabPanel runat="server" HeaderText="Specification Manager" ToolTip="Specification Manager"
            ID="tabSMSpec">
            <ContentTemplate>
                <asp:Panel ID="pnlSMSpec" runat="server">
                </asp:Panel>
            </ContentTemplate>
        </ajaxToolkit:TabPanel>
    </ajaxToolkit:TabContainer>
    <div class="AlterNateColor3">
        <div class="PageSHeading" align="center">
            <asp:Label ID="lblFooter" runat="Server"></asp:Label>
        </div>
    </div>
    <asp:HiddenField ID="hidSortIdSMPM" runat="server" />
    <asp:HiddenField ID="hidSortIdSpec" runat="server" />
    <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
    <asp:HiddenField ID="hidPMGrpId" runat="server" />
    <asp:HiddenField ID="hidPMGrpNm" runat="server" />
    <asp:HiddenField ID="hidRowNum" runat="server" Value="2" />
    <asp:HiddenField ID="hidEqId" runat="server" />
    <asp:HiddenField ID="hidSortIdGrp" runat="server" />
    <asp:HiddenField ID="hidSortIdGrpMngr" runat="server" />
    <asp:Button ID="btnMasterSel" runat="server" Style="display: none;" />
    <asp:HiddenField ID="hidMasterGrpID" runat="server" />
    <asp:HiddenField ID="hidMasterGrpDes" runat="server" />
    <asp:HiddenField ID="hidMTypeID" runat="server" />
    <asp:HiddenField ID="hidSortIdMGroup" runat="server" />
    </form>
</body>
</html>
