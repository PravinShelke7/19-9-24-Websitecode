<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CheckRFP_Status.aspx.vb"
    Inherits="Pages_SavvyPackPro_CheckRFP_Status" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Check Status</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function ShowPopUpStatus(Page) {
            var width = 300;
            var height = 200;
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

        function ClosePage() {
            window.opener.document.getElementById('btnRefresh').click();
        }


        function RefreshPage() {
            window.location.reload();
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
    <asp:Image ImageAlign="AbsMiddle" Width="1050px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
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
            <div id="DivRFPSelector" runat="server" style="width: 1050px; font-family: Verdana;
                font-size: 14px; display: none;">
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
                                                    <td style="padding-left: 120px;">
                                                        <b>Due Date:</b>
                                                        <asp:Label ID="lblDueD" runat="server"></asp:Label>
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
            </div>
            <table style="width: 1050px;">
                <tr class="AlterNateColor2">
                    <td style="padding-left: 10px; padding-top: 15px;">
                        <ajaxToolkit:TabContainer ID="tabRfpManager" Height="600px" Width="1000" runat="server"
                            ActiveTabIndex="1" AutoPostBack="true">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Summary" ToolTip="Summary" ID="tabBMConfigVendor">
                                <ContentTemplate>
                                    <asp:Panel ID="pnlBMConfigVendor" runat="server">
                                        <table id="PageSection1" runat="server" style="margin-top: 15px; background-color: #D3E7CB;
                                            width: 971px;">
                                            <tr id="Tr1" runat="server">
                                                <td id="Td1" runat="server">
                                                    <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                                                        Summary
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="Tr2" runat="server">
                                                <td id="Td2" style="padding-top: 10px;" runat="server">
                                                    <table style="width: 99%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblPageSizeC" runat="server" Font-Bold="True" Style="text-align: right;
                                                                    font-size: 12px;" Text="Page Size:"></asp:Label>
                                                                <asp:DropDownList ID="ddlPageCountC" runat="server" AutoPostBack="True" CssClass="DropDown"
                                                                    Width="55px">
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
                                                                <asp:Label ID="lblRF" runat="server" Font-Bold="True" Style="text-align: right; padding-left: 20px;"
                                                                    Text="Record(s):"></asp:Label>
                                                                <asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel" Font-Bold="True"
                                                                    ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblKeyWord" runat="server" Font-Bold="True" Font-Size="13px" Style="margin-left: 80px;"
                                                                    Text="Keyword:"></asp:Label>
                                                                <asp:TextBox ID="txtKey" runat="server" CssClass="LongTextBox" MaxLength="50" onchange="javascript:CheckSP(this);"
                                                                    ToolTip="Enter EmailID/CompanyName/FirstName/LastName to search vendor."></asp:TextBox>
                                                                <asp:Button ID="btnSearch" runat="server" CssClass="ButtonWMarigin" Text="Search" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="Tr3" runat="server">
                                                <td id="Td3" style="padding-top: 10px;" runat="server">
                                                    <div id="ContentPagemargin" runat="server" class="ContentPage" style="overflow: auto;
                                                        height: 449px; width: 940px;">
                                                        <asp:Label ID="lblNOVendor" runat="server" CssClass="CalculatedFeilds" Text="No record found."
                                                            Visible="False"></asp:Label>
                                                        <asp:GridView ID="grdUsers" runat="server" AllowPaging="True" AllowSorting="True"
                                                            AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                                            BorderWidth="1px" CellPadding="4" CssClass="grdProject" DataKeyNames="ID" Font-Size="11px"
                                                            ForeColor="Black" GridLines="Vertical" Width="920px">
                                                            <PagerSettings Position="Top" />
                                                            <PagerTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandArgument="First" CommandName="Page"
                                                                    Style="color: Black;">First</asp:LinkButton>
                                                                <asp:Label ID="pmore" runat="server" Text="..."></asp:Label>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" CommandArgument="Prev" CommandName="Page"
                                                                    Style="color: #284E98;">Previous</asp:LinkButton>
                                                                <asp:LinkButton ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:Label ID="CurrentPage" runat="server" Style="color: Red;" Text="Label"></asp:Label><asp:LinkButton
                                                                    ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandArgument="Next" CommandName="Page"
                                                                    Style="color: #284E98;">Next</asp:LinkButton>
                                                                <asp:Label ID="nmore" runat="server" Text="..."></asp:Label>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" CommandArgument="Last" CommandName="Page"
                                                                    Style="color: Black;">Last</asp:LinkButton></PagerTemplate>
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
                                                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
                                                                    <ItemStyle CssClass="breakword" HorizontalAlign="Left" Width="20px" Wrap="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
                                                                    <ItemStyle CssClass="breakword" HorizontalAlign="Left" Width="80px" Wrap="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="NAME" HeaderText="VENDOR" SortExpression="NAME">
                                                                    <ItemStyle CssClass="breakword" HorizontalAlign="Left" Width="100px" Wrap="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS">
                                                                    <ItemStyle CssClass="breakword" HorizontalAlign="Left" Width="60px" Wrap="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="DATE" SortExpression="UPDATEDATE">
                                                                    <ItemStyle CssClass="breakword" HorizontalAlign="Left" Width="100px" Wrap="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="FDUEDATE" HeaderText="DUE DATE" SortExpression="FDUEDATE  ">
                                                                    <ItemStyle CssClass="breakword" HorizontalAlign="Left" Width="100px" Wrap="True" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle BackColor="#F7F7DE" Font-Bold="True" Font-Underline="False" ForeColor="DarkBlue"
                                                                HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" Height="25px" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Log" ToolTip="Log" ID="TabPanel1">
                                <ContentTemplate>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table id="Table1" runat="server" style="margin-top: 15px; background-color: #D3E7CB;
                                            width: 971px;">
                                            <tr id="Tr4" runat="server">
                                                <td id="Td4" runat="server">
                                                    <div class="PageHeading" id="div1" runat="server" style="text-align: center;">
                                                       Status
                                                    </div>
                                                </td>
                                            </tr>

                                             <tr id="Tr7" runat="server">
                                                <td id="Td7" style="padding-top: 10px;" runat="server">
                                                    <table style="width: 99%;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label1" runat="server" Font-Bold="True" Style="text-align: right;
                                                                    font-size: 12px;" Text="Page Size:"></asp:Label>
                                                                <asp:DropDownList ID="ddllogcnt" runat="server" AutoPostBack="True" CssClass="DropDown"
                                                                    Width="55px">
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
                                                                <asp:Label ID="lblrfp1" runat="server" Font-Bold="True" Style="text-align: right; padding-left: 20px;"
                                                                    Text="Record(s):"></asp:Label>
                                                                <asp:Label ID="lbllogcnt" runat="server" CssClass="NormalLabel" Font-Bold="True"
                                                                    ForeColor="Red"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblkey" runat="server" Font-Bold="True" Font-Size="13px" Style="margin-left: 80px;"
                                                                    Text="Keyword:"></asp:Label>
                                                                <asp:TextBox ID="txtlogs" runat="server" CssClass="LongTextBox" MaxLength="50" onchange="javascript:CheckSP(this);"
                                                                    ToolTip="Enter EmailID/CompanyName/FirstName/LastName to search vendor."></asp:TextBox>
                                                                <asp:Button ID="btnLogSearch" runat="server" CssClass="ButtonWMarigin" Text="Search" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>

                                            <tr id="Tr5" runat="server">
                                                <td id="Td5" style="padding-top: 10px;" runat="server">
                                                    <div id="Div2" class="ContentPage" style="overflow: auto; height: 449px; width: 940px;"
                                                        runat="server">
                                                        <asp:Label ID="lblnorcord" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                            Text="No record found."></asp:Label>
                                                        <asp:GridView Width="920px" CssClass="grdProject" runat="server" ID="grdLog" DataKeyNames="ID"
                                                            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" Font-Size="11px"
                                                            CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                                                            BorderStyle="None" BorderWidth="1px">
                                                            <PagerSettings Position="Top" />
                                                            <PagerTemplate>
                                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                                    Style="color: Black;">First</asp:LinkButton>
                                                                <asp:Label ID="pmore" runat="server" Text="..."></asp:Label>
                                                                <asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;" CommandName="Page"
                                                                    CommandArgument="Prev">Previous</asp:LinkButton>
                                                                <asp:LinkButton ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:Label ID="CurrentPage" runat="server" Text="Label" Style="color: Red;"></asp:Label><asp:LinkButton
                                                                    ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                    ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next"
                                                                    Style="color: #284E98;">Next</asp:LinkButton>
                                                                <asp:Label ID="nmore" runat="server" Text="..."></asp:Label>
                                                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page" CommandArgument="Last"
                                                                    Style="color: Black;">Last</asp:LinkButton></PagerTemplate>
                                                            <FooterStyle BackColor="#CCCC99" />
                                                            <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="USERID" Visible="False"
                                                                    SortExpression="USERID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUserID1" Width="50px" runat="server" Text='<%# bind("USERID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID">
                                                                    <ItemStyle Width="20px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="DESCRIPTION" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
                                                                    <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="TO_SUPPLIER" HeaderText="VENDOR" SortExpression="TO_SUPPLIER">
                                                                    <ItemStyle Width="100px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="STATUS" HeaderText="STATUS" SortExpression="STATUS" Visible ="false" >
                                                                    <ItemStyle Width="60px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                </asp:BoundField>
                                                                  <asp:TemplateField HeaderText="STATUS" SortExpression="STATUS">
                                                                            <ItemTemplate>
                                                                                <asp:LinkButton ID="lnkstatus" runat="server" Width="100px" Style="color: Black;
                                                                                    font-family: Verdana; font-size: 11px; text-decoration: underline;" Text='<%# bind("STATUS")%>'></asp:LinkButton>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="140px" Wrap="True" HorizontalAlign="Left" />
                                                                        </asp:TemplateField>
                                                                <asp:BoundField DataField="UPDATEDATE" HeaderText="DATE" SortExpression="UPDATEDATE">
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
                                        </table>
                                    </asp:Panel>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
                <tr id="Tr6" class="AlterNateColor3" runat="server">
                    <td id="Td6" class="PageSHeading" align="center" runat="server">
                        <asp:Label ID="lblFooter" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
            <asp:HiddenField ID="hidRfpID" runat="server" />
            <asp:HiddenField ID="hidRfpNm" runat="server" />
            <asp:Button ID="btnrefreshT" runat="server" Style="display: none;" />
            <asp:Button ID="btnrefresh" runat="server" Style="display: none;" />
            <asp:HiddenField ID="hidSurveyId" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
    <asp:Button ID="btnRefreshVList" runat="server" Style="display: none;" />
    <asp:Button ID="btnrefreshSeq" runat="server" Style="display: none;" />
    </form>
</body>
</html>
