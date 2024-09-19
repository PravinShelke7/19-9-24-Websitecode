<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnalyzeProposal_Supplier.aspx.vb" Inherits="Pages_SavvyPackPro_AnalyzeProposal_Supplier" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Analyze Proposal</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
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

      
        .style1 {
            width: 1130px;
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

        function CheckNum(text, hidvalue) {
            var sequence = document.getElementById(text.id).value;
            if (sequence != "") {
                if (isNaN(sequence)) {
                    alert("Please enter numeric value.");
                    document.getElementById(text.id).value = "";
                    document.getElementById(text.id).value = hidvalue;
                    return false;
                }
            }
            else {
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
                document.getElementById('btnrefresh').click();
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

            var width = 980;
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
            newwin = window.open(Page, 'SelTypeVendor', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
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

        function ShowPopWindow_Struct(Page) {
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
            newwin = window.open(Page, 'MatSel', params);

        }



        function ClosePage() {
            window.opener.document.getElementById('btnRefresh').click();
        }


        function RefreshPage() {
            window.location.reload();
        }


    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <asp:Image ImageAlign="AbsMiddle" Width="1100px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
            runat="server" />
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    
                <table style="width: 1100px; font-family: Verdana; font-size: 14px;">
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
                                                                CssClass="SavvyLink" OnClientClick="return ShowPopUpRFP('PopUp/SelectType_Vendor.aspx?RfpID=hidRfpID&RfpDes=hidRfpNm&RfpInnertxt=lnkSelRFP')"></asp:LinkButton>
                                                        </td>
                                                        <%--<td style="padding-left: 40px;">
                                                        <b>Units: </b>
                                                        <asp:Label ID="lblUnits" runat="server"></asp:Label>
                                                    </td>
                                                    <td style="padding-left: 75px;">
                                                        <b>Currency: </b>
                                                        <asp:Label ID="lblCurr" runat="server"></asp:Label>
                                                    </td>--%>
                                                    </tr>
                                                </table>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
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
                                                        <td style="padding-left: 120px;">
                                                            <b>Buyer:</b>
                                                            <asp:Label ID="lblBuyer" runat="server"></asp:Label>
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
                <table style="width: 1100px; overflow: auto;">
                    <tr class="AlterNateColor2">
                        <td style="padding-left: 10px; padding-top: 15px;">
                            <ajaxToolkit:TabContainer ID="tabRfpSManager" Height="550px" runat="server" ActiveTabIndex="1"
                                AutoPostBack="true" Enabled="false">
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Vendor Configure" ToolTip="Vendor Configure" TabIndex="0"
                                    Enabled="false" ID="tabBMConfigVendor">
                                    <HeaderTemplate>
                                        Vendor Configure
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlBMConfigVendor" runat="server">
                                            <table id="tblBMConfigVendor" runat="server" style="margin-top: 15px;">
                                                <tr id="Tr1" runat="server">
                                                    <td id="Td1" runat="server" class="style1">
                                                        <asp:LinkButton ID="lnkAddContact" runat="server" Text="Add Vendor" CssClass="SavvyLink"
                                                            Font-Bold="True" OnClientClick="return ShowPopUpVendorInfo('PopUp/AddEditVendorInfo.aspx');"></asp:LinkButton>
                                                    </td>
                                                </tr>
                                                <tr id="Tr2" runat="server">
                                                    <td id="Td2" style="padding-top: 10px;" runat="server" class="style1">
                                                        <table style="width: 100%;">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right; font-size: 12px;"></asp:Label>
                                                                    <asp:DropDownList ID="ddlPageCountC" runat="server" Width="55px" CssClass="DropDown"
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
                                                                    <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right; padding-left: 20px;"></asp:Label>
                                                                    <asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel" ForeColor="Red"
                                                                        Font-Bold="True"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Style="margin-left: 80px;"
                                                                        Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                    <asp:TextBox ID="txtKey" runat="server" onchange="javascript:CheckSP(this);" ToolTip="Enter EmailID/CompanyName/FirstName/LastName to search vendor."
                                                                        CssClass="LongTextBox" MaxLength="50"></asp:TextBox>
                                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="Tr3" runat="server">
                                                    <td id="Td3" style="padding-top: 10px;" runat="server" class="style1">
                                                        <div id="ContentPagemargin" style="overflow: auto; height: 410px;" runat="server">
                                                            <asp:Label ID="lblNOVendor" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                                Text="No record found."></asp:Label>
                                                            <asp:GridView Width="1100px" CssClass="grdProject" runat="server" ID="grdUsers" DataKeyNames="VENDORID"
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
                                                                    <asp:LinkButton ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:Label ID="CurrentPage" runat="server" Text="Label" Style="color: Red;"></asp:Label>
                                                                    <asp:LinkButton ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next"
                                                                        Style="color: #284E98;">Next</asp:LinkButton>
                                                                    <asp:Label ID="nmore" runat="server" Text="..."></asp:Label>
                                                                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page" CommandArgument="Last"
                                                                        Style="color: Black;">Last</asp:LinkButton>
                                                                </PagerTemplate>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <input id="HeaderLevelCheckBox" onclick="javascript: SelectAllCheckboxes(this);"
                                                                                runat="server" type="checkbox" />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="select" runat="server"></asp:CheckBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="VENDORID" SortExpression="VENDORID" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMUsrID" runat="server" Visible="False" Text='<%# Bind("VENDORID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="EMAILADDRESS" HeaderText="User Name" SortExpression="EMAILADDRESS">
                                                                        <ItemStyle Width="200px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FULLNAME" HeaderText="Contact Name" SortExpression="FULLNAME">
                                                                        <ItemStyle Width="140px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="COMPANYNAME" HeaderText="Company" SortExpression="COMPANYNAME">
                                                                        <ItemStyle Width="180px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="COUNTRY" HeaderText="Country" SortExpression="COUNTRY">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="STATE" HeaderText="State" SortExpression="STATE">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ZIPCODE" HeaderText="City" SortExpression="ZIPCODE">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
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
                                <ajaxToolkit:TabPanel runat="server" HeaderText=" Terms Manager" ToolTip=" Terms Manager" TabIndex="1"
                                    ID="tabTermsManager">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnltabTermsManager" runat="server" Style="overflow:auto; " Height="526px">
                                            <table class="ContentPage" id="Table3" runat="server" style="margin-top: 15px; width: 1012px;">
                                                <tr id="Tr4" runat="server">
                                                    <td id="Td4" runat="server">
                                                        <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                                                            RFP
                                                        <%--<asp:ImageButton ID="Update" ImageUrl="~/Images/Update.gif" runat="server" ToolTip="Update"
                                                            Visible="true" align="Right" Style="padding-right: 25px; padding-bottom: 5px;
                                                            padding-top: 5px;" />--%>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="Tr5" style="height: 20px" runat="server">
                                                    <td id="Td5" runat="server">
                                                        <div id="Div1" runat="server">
                                                            <div id="Div4" style="text-align: left; height: 500px; width: 980px;">
                                                                <div id="Div2">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="Error"></asp:Label>
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
                                                                <div id="Div8" style="text-align: left; margin-top: 30px;">
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
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Price" ToolTip="Price" ID="tabSMPrice" TabIndex="2">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPrice" runat="server">
                                            <table class="ContentPage" style="margin-top: 15px; overflow: auto; width: 1012px; height: 500px;">
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 430px;">
                                                            <asp:Label ID="lblNoRecordPrice" runat="server" Text="No Record Found" Visible="false"
                                                                CssClass="Error"></asp:Label>
                                                            <asp:Table ID="tblPrice" runat="server" CellPadding="0" CellSpacing="2">
                                                            </asp:Table>
                                                        </div>
                                                        <br />
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Price & Cost" ToolTip="Price & Cost" TabIndex="3"
                                    ID="tabSMPC">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlSMPC1" runat="server">
                                            <table id="Table1" class="ContentPage" runat="server" style="margin-top: 15px; overflow: auto; width: 1012px; height: 500px;">
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 430px;">
                                                            <asp:Label ID="lblNoDataPriceCost" runat="server" Text="No Record Found" Visible="false"
                                                                CssClass="Error"></asp:Label>
                                                            <asp:Table ID="tblPC" runat="server" CellPadding="0" CellSpacing="2">
                                                            </asp:Table>
                                                        </div>
                                                        <br />
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Price Options" ToolTip="Price Options" TabIndex="4"
                                    ID="tabCreatePriceOp">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlPriceOp" runat="server">
                                            <table class="ContentPage" id="Table2" runat="server" style="width: 404px;">
                                                <tr id="Tr6" runat="server">
                                                    <td id="Td6" runat="server">
                                                        <div class="PageHeading" id="div5" runat="server" style="text-align: center; height: 30px;">
                                                            Price Options
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr id="trPo" runat ="server" >
                                                <td>
                                                <asp:Label ID="lblPriceName" runat="server" Style="margin-left: 5px;"></asp:Label>
                                                </td>
                                                </tr>
                                                     <tr>
                                                    <td>
                                                      <b>  <asp:Label ID="Label2" runat="server" Text="Select Price Option:"></asp:Label></b>
                                                       
                                                        <asp:LinkButton ID="lbkpriceopt" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                            CssClass="SavvyLink" OnClientClick="return ShowMasterGroupPopUp('PopUp/PriceSelection.aspx?ID=hidPriceId&Des1=hidPriceDes&Des=tabRfpSManager_tabCreatePriceOp_lbkpriceopt&Type=PA')"></asp:LinkButton>
                                                    </td> 
                                                      <td>
                                                       <asp:Button ID="btnpoptsku" runat="server" Text="Show SKU level data" CssClass="ButtonWMarigin" />
                                                    </td>                                                                                                   
                                                </tr>
                                                 <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 430px;">
                                                            <asp:Label ID="lblnosku" runat="server" Text="" Visible="false"
                                                                CssClass="Error"></asp:Label>
                                                            <asp:Table ID="tblPriceOpt" runat="server" CellPadding="0" CellSpacing="2">
                                                            </asp:Table>
                                                        </div>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr id="Tr7" style="height: 20px" runat="server">
                                                    <td id="Td7" runat="server">
                                                        <div id="" style="text-align: left; width: 551px; margin-top: 5px; margin-left: 70px;">
                                                            <asp:Label ID="lblNoPO" runat="server" Text="No Price Options Available" Visible="false"></asp:Label>
                                                            <asp:GridView Width="350px" CssClass="grdProject" runat="server" ID="GrdPriceOption"
                                                                DataKeyNames="PRICEOPTIONID" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                                Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Height="100px">
                                                                <PagerSettings Position="Top" />
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                                <Columns>
                                                                    <asp:TemplateField HeaderText="PRICE OPTION">
                                                                        <ItemTemplate>
                                                                            <asp:LinkButton ID="lnkPriceOption" runat="server" Width="100px" Style="color: Black; font-family: Verdana; font-size: 11px; text-decoration: underline;"
                                                                                Text='<%# bind("PRICEOPTION")%>'></asp:LinkButton>
                                                                            <asp:Label ID="lblPriceID" runat="server" Visible="FALSE" Text='<%# Bind("PRICEOPTIONID")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemStyle Width="140px" Wrap="True" HorizontalAlign="Left" />
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
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Operating" ToolTip="RFQ" ID="tabRFQ" TabIndex="5">
                                    <ContentTemplate>
                                        <asp:Panel ID="PnlOp" runat="server">
                                            <table class="ContentPage" style="margin-top: 15px; overflow: auto; width: 1012px; height: 500px;">
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 430px;">
                                                            <asp:Label ID="lblNoDataRFQ" runat="server" Text="No Record Found" Visible="false"
                                                                CssClass="Error"></asp:Label>
                                                            <asp:Table ID="tbleRFQ" runat="server" CellPadding="0" CellSpacing="2">
                                                            </asp:Table>
                                                        </div>
                                                        <br />
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Structure Analysis" ToolTip="Structure" ID="tabStruct" TabIndex="7">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlStruct" runat="server">
                                            <table class="ContentPage" style="overflow: auto; width: 1012px; height: 500px;">
                                                <tr id="trSelM" runat="server">
                                                    <td>
                                                      <b>  <asp:Label ID="lblmaster" runat="server" Text="Select Master Group:"></asp:Label></b>
                                                       
                                                        <asp:LinkButton ID="lnkSelMasterGrp" runat="server" Text="Nothing Selected" Font-Bold="True"
                                                            CssClass="SavvyLink" OnClientClick="return ShowMasterGroupPopUp('PopUp/SelectMasterGroup.aspx?MID=hidMasterGrpID&MDes=hidMasterGrpDes&MInner=tabRfpSManager_tabStruct_lnkSelMasterGrp&Type=S')"></asp:LinkButton>
                                                    </td>
                                                
                                                    <td>
                                                       <asp:Button ID="btnSkulvlData" runat="server" Text="Show SKU level data" CssClass="ButtonWMarigin" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div id="Div7" style="text-align: left;">
                                                            <asp:Table Width="30%" ID="tblSpecDes" runat="server">
                                                            </asp:Table>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <div style="overflow: auto; height: 430px;">
                                                            <asp:Label ID="lblNoStructData" runat="server" Text="" Visible="false"
                                                                CssClass="Error"></asp:Label>
                                                            <asp:Table ID="tblStruct" runat="server" CellPadding="0" CellSpacing="2">
                                                            </asp:Table>
                                                        </div>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                       <%-- <asp:Button ID="btnUpdateStruct" runat="server" Text="Update" CssClass="ButtonWMarigin" />--%>
                                                    </td>
                                                    <td>
                                                        <%--<asp:Button ID="btnExtrapSkuLvl" runat="server" Text="Extrapolate To Sku" CssClass="ButtonWMarigin" />--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr id="Tr11" class="AlterNateColor3" runat="server">
                        <td id="Td11" class="PageSHeading" align="center" runat="server">
                            <asp:Label ID="lblFooter" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <br />
                <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
                <asp:HiddenField ID="hidRfpID" runat="server" />
                <asp:HiddenField ID="hidRfpNm" runat="server" />
                <asp:Button ID="btnrefreshT" runat="server" Style="display: none;" />
                <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
                <asp:HiddenField ID="hidSurveyId" runat="server" />
                <asp:HiddenField ID="hidRSOwnerEmailID" runat="server" />
                <asp:HiddenField ID="hidRowNumBM" runat="server" />
                <asp:HiddenField ID="hidSpecSID" runat="server" Value="1" />
                <asp:HiddenField ID="hidRowNum" runat="server" />
                <asp:HiddenField ID="hidRow" runat="server" />
                <asp:HiddenField ID="hidRowR" runat="server" />
                <asp:HiddenField ID="hidMasterGrpID" runat="server" />
                <asp:HiddenField ID="hidMasterGrpDes" runat="server" />
                <asp:HiddenField ID="hidGrpCount" runat="server" />
                <asp:HiddenField ID="hidPriceId" runat="server" />
                <asp:HiddenField ID="hidPriceDes" runat="server" />
                <asp:HiddenField ID="hidlnkdes" runat="server" />

            </ContentTemplate>

        <asp:Button ID="btnHidRFPCreate" runat="server" Style="display: none;" />
        <asp:Button ID="btnRefreshVList" runat="server" Style="display: none;" />
        <asp:Button ID="btnrefreshSeq" runat="server" Style="display: none;" />
        <asp:Button ID="btnMasterSel" runat="server" Style="display: none;" />
    </form>
</body>
</html>
