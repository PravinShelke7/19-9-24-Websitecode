<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SupplierDefault.aspx.vb" Inherits="Pages_SavvyPackPro_SupplierDefault" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Supplier Manager</title>
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

        function ValidateList() {


            var name = document.getElementById("tabSupplierManager_tabSMSpec_txtGroupDe1").value;

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
    </style>
    <style type="text/css">
        a.SavvyLink:link {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:visited {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }

        a.SavvyLink:hover {
            color: Red;
            font-size: 11px;
        }

        .PSavvyModule {
            margin-top: 2px;
            margin-left: 1px;
            background: url('../../Images/AdminHeader.gif') no-repeat;
            height: 45px;
            width: 1220px;
            text-align: center;
            vertical-align: middle;
        }

        .PageHeading {
            font-size: 18px;
            font-weight: bold;
        }

        .ContentPage {
            margin-top: 2px;
            margin-left: 1px;
            width: 1220px;
            background-color: #F1F1F2;
        }

        #SavvyPageSection1 {
            background-color: #D3E7CB;
        }

        .AlterNateColor3 {
            background-color: #D3DAD0;
            height: 20px;
        }

        .PageSHeading {
            font-size: 12px;
            font-weight: bold;
        }

        .InsUpdMsg {
            font-family: Verdana;
            font-size: 12px;
            font-style: italic;
            color: Red;
            font-weight: bold;
        }

        .LongTextBox {
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

        .breakword {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>

    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 9px;
        }

        .Comments {
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

        .Amount {
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

        .NormalLable_Term {
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
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" Width="1320px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
                runat="server" />
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <ajaxToolkit:TabContainer ID="tabSupplierManager" Height="750px" runat="server" ActiveTabIndex="3"
            AutoPostBack="true">
            <ajaxToolkit:TabPanel runat="server" HeaderText="Proposal Manager" ToolTip="Proposal Manager"
                ID="tabSMProposal">
                <ContentTemplate>
                    <asp:Panel ID="pnlSMProposal" runat="server" DefaultButton="btnSearchSMProposal">
                        <table class="ContentPage" id="tblSB" runat="server" style="margin-top: 15px;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                                        Proposal Manager
                                    </div>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td>
                                    <div style="text-align: left; background-color: #D3E7CB;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="font-size: 12px; width: 40%;">
                                                                <asp:Label ID="lblKeywordSMProposal" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                                                    Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                                                                <asp:TextBox ID="txtSearchSMProposal" runat="server" CssClass="LongTextBox" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                                <asp:Button ID="btnSearchSMProposal" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblSelGrpSMProposal" runat="server" Text="Group Selector:" Style="margin-left: 10px; font-size: 12px;"
                                                                    Font-Bold="true"></asp:Label>
                                                                <asp:LinkButton ID="lnkSelGrpSMProposal" runat="server" CssClass="SavvyLink" Text="Select Group"
                                                                    OnClientClick="return ShowGrpSelPopUp('PopUp/GrpSelector.aspx?');"
                                                                    Style="margin-left: 6px;"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="trpagecountSMProposal" runat="server" visible="false">
                                                <td style="font-size: 12px;">
                                                    <asp:Label ID="lblPageSizeSMProposal" runat="server" Text="Page Size:" Font-Bold="true" Style="text-align: right; margin-left: 10px;"></asp:Label>
                                                    <asp:DropDownList ID="ddlPageCountSMProposal" runat="server" Width="55px" CssClass="DropDown"
                                                        AutoPostBack="true">
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
                                                    <div style="overflow: auto; height: 460px;">
                                                        <asp:Label ID="lblMsgSMProposal" runat="server" Visible="false" Text="No records found."></asp:Label>
                                                        <asp:GridView Width="1120px" CssClass="grdProject" runat="server" ID="grdPpslMngr"
                                                            DataKeyNames="GROUPID" AllowPaging="True" PageSize="100" AllowSorting="True"
                                                            AutoGenerateColumns="False" Font-Size="11px" Font-Names="Verdana" CellPadding="4"
                                                            ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
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
                                                                <asp:TemplateField HeaderText="Group ID" HeaderStyle-HorizontalAlign="Left" SortExpression="GROUPID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrpID" runat="server" Visible="true" Text='<%# Bind("GROUPID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Group Des" HeaderStyle-HorizontalAlign="Left" SortExpression="GROUPDES">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGrpDes" runat="server" Visible="true" Text='<%# bind("GROUPDES")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SKU ID" HeaderStyle-HorizontalAlign="Left" SortExpression="SKUID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSKUID" runat="server" Visible="true" Text='<%# Bind("SKUID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="SKU Des" HeaderStyle-HorizontalAlign="Left" SortExpression="SKUDES">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSkuDes" runat="server" Text='<%# bind("SKUDES")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Spec" HeaderStyle-HorizontalAlign="Left" SortExpression="SPECID">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSpecID" runat="server" Text='<%# Bind("SPECID")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="40px" Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Product Type" HeaderStyle-HorizontalAlign="left" SortExpression="PRODUCTTYPE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblProdTYPE" runat="server" Visible="true" Text='<%# Bind("PRODUCTTYPE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="90px" Wrap="true" HorizontalAlign="Left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Package Type" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="PACAKAGETYPE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPACKTYPE" runat="server" Visible="true" Text='<%# Bind("PACAKAGETYPE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="90px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Width (in)" HeaderStyle-HorizontalAlign="left" SortExpression="WIDTH">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWIDTH" runat="server" Visible="true" Text='<%# Bind("WIDTH")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="60px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Gusset (in)" HeaderStyle-HorizontalAlign="left" SortExpression="LENGTH">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblLNGTH" runat="server" Visible="true" Text='<%# bind("LENGTH")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Height (in)" HeaderStyle-HorizontalAlign="left" SortExpression="HEIGHT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHEIGHT" runat="server" Visible="true" Text='<%# Bind("HEIGHT")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Weight (lb)" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="WEIGHT">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWEIGHT" runat="server" Visible="true" Text='<%# bind("WEIGHT")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Structure" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="STRUCTURE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSTRUCTURE" runat="server" Visible="true" Text='<%# Bind("STRUCTURE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="THICKNESS (mil)" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="THICKNESS">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWEIGHT" runat="server" Visible="true" Text='<%# Bind("THICKNESS")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Volume (number)" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="VOLUME">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVOLUME" runat="server" Visible="true" Text='<%# bind("VOLUME")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Price ($/M)" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="PRICE">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPRICE" runat="server" Visible="true" Text='<%# Bind("PRICE")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Setup ($/M)" HeaderStyle-HorizontalAlign="left"
                                                                    SortExpression="SETUP">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblPRICE" runat="server" Visible="true" Text='<%# Bind("SETUP")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                            </Columns>
                                                            <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
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
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="lblTagSB" runat="Server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" HeaderText="Specification Manager" ToolTip="Specification Manager"
                ID="tabSMSpec">
                <ContentTemplate>
                    <asp:Panel ID="pnlSMSpec" runat="server" DefaultButton="btnSearchSMSpec">
                        <table class="ContentPage" id="tblSpecs" runat="server" style="margin-top: 15px;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="div1" runat="server" style="text-align: center;">
                                        Specification Manager
                                    </div>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td>
                                    <div style="text-align: left; background-color: #D3E7CB;">
                                        <table style="width: 100%">
                                            <tr>
                                                <td>
                                                    <table width="100%">
                                                        <tr>
                                                            <td style="font-size: 12px; width: 40%;">
                                                                <asp:Label ID="lblSpecKeyword" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                                                    Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                                                                <asp:TextBox ID="txtSpecSrch" runat="server" CssClass="LongTextBox" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                                <asp:Button ID="btnSearchSMSpec" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--<table>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Group Selector:" Style="margin-left: 10px; font-size: 12px;"
                                                                    Font-Bold="true"></asp:Label>
                                                                <asp:LinkButton ID="LinkButton5" runat="server" CssClass="SavvyLink" Text="Select Group"
                                                                    OnClientClick="return ShowGrpSelPopUp('PopUp/GrpSelector.aspx?');"
                                                                    Style="margin-left: 6px;"></asp:LinkButton>
                                                            </td>
                                                        </tr>
                                                    </table>--%>
                                                </td>
                                            </tr>
                                            <tr id="trSpecPageCount" runat="server" visible="false">
                                                <td style="font-size: 12px;">
                                                    <asp:Label ID="lblSpecPageSize" runat="server" Text="Page Size:" Font-Bold="true" Style="text-align: right; margin-left: 10px;"></asp:Label>
                                                    <asp:DropDownList ID="drpSpecPageCount" runat="server" Width="55px" CssClass="DropDown"
                                                        AutoPostBack="true">
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
                                                    <div style="overflow: auto; height: 460px;">
                                                         <asp:Label ID="lblSpecNoFound" runat="server" Visible="false" Text="No records found."></asp:Label>
                                                    <asp:GridView Width="1120px" CssClass="grdProject" runat="server" ID="grdSpec"
                                                        DataKeyNames="SpecID" AllowPaging="True" PageSize="100" AllowSorting="True"
                                                        AutoGenerateColumns="False" Font-Size="11px" Font-Names="Verdana" CellPadding="4"
                                                        ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
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
                                                            <asp:TemplateField HeaderText="Group ID" HeaderStyle-HorizontalAlign="Left" SortExpression="GROUPID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblGrpID" runat="server" Visible="true" Text='<%# Bind("GROUPID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Group Des" HeaderStyle-HorizontalAlign="Left" SortExpression="GROUPDES">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkGrpDes" runat="server" Visible="true" OnClientClick="return false;" Text='<%# Bind("GROUPDES")%>'></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SKU ID" HeaderStyle-HorizontalAlign="Left" SortExpression="SKUID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSKUID" runat="server" Visible="true" Text='<%# Bind("SKUID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SKU Des" HeaderStyle-HorizontalAlign="Left" SortExpression="SKUDES">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSkuDes" runat="server" Text='<%# bind("SKUDES")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Spec #" HeaderStyle-HorizontalAlign="Left" SortExpression="SPECID">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSpecID" runat="server" Text='<%# Bind("SPECID")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="40px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Spec Files" HeaderStyle-HorizontalAlign="left">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lnkSpecFIle" runat="server" CssClass="SavvyLink" Text="Files"></asp:LinkButton>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Product Type" HeaderStyle-HorizontalAlign="left" SortExpression="PRODUCTTYPE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblProdTYPE" runat="server" Visible="true" Text='<%# Bind("PRODUCTTYPE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" Wrap="true" HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Package Type" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="PACAKAGETYPE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPACKTYPE" runat="server" Visible="true" Text='<%# Bind("PACAKAGETYPE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="90px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Width (in)" HeaderStyle-HorizontalAlign="left" SortExpression="WIDTH">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWIDTH" runat="server" Visible="true" Text='<%# Bind("WIDTH")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="60px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Gusset (in)" HeaderStyle-HorizontalAlign="left" SortExpression="LENGTH">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblLNGTH" runat="server" Visible="true" Text='<%# bind("LENGTH")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Height (in)" HeaderStyle-HorizontalAlign="left" SortExpression="HEIGHT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblHEIGHT" runat="server" Visible="true" Text='<%# Bind("HEIGHT")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Weight (lb)" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="WEIGHT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWEIGHT" runat="server" Visible="true" Text='<%# bind("WEIGHT")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>


                                                            <asp:TemplateField HeaderText="Structure" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="STRUCTURE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSTRUCTURE" runat="server" Visible="true" Text='<%# Bind("STRUCTURE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="THICKNESS (mil)" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="THICKNESS">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblWEIGHT" runat="server" Visible="true" Text='<%# Bind("THICKNESS")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Volume (number)" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="VOLUME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblVOLUME" runat="server" Visible="true" Text='<%# bind("VOLUME")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="70px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Price ($/M)" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="PRICE">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRICE" runat="server" Visible="true" Text='<%# Bind("PRICE")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Setup ($/M)" HeaderStyle-HorizontalAlign="left"
                                                                SortExpression="SETUP">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPRICE" runat="server" Visible="true" Text='<%# Bind("SETUP")%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="left" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
                                                            HorizontalAlign="Left" />
                                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                        <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                    </div>                                                   
                                                </td>
                                            </tr>
                                        </table>
                                        <table style="margin-left: 10px;">
                                            <tr class="AlterNateColor1">
                                                <td colspan="3">
                                                    <asp:Button ID="btnCGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                                        ToolTip="Create Group and Description" OnClientClick="return MakeVisible('tabSupplierManager_tabSMSpec_trCreate');" />
                                                    <asp:Button ID="btnEditGroup" runat="server" Text="Edit Group" CssClass="ButtonWMarigin"
                                                        ToolTip="Edit Group and Description" Enabled="false" OnClientClick="return OpenEGroupPopup('../Popup/EditGroup.aspx')" />
                                                </td>
                                            </tr>
                                            <tr class="AlterNateColor1" style="display: none; height:80px" id="trCreate" runat="Server">
                                                <td colspan="3">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <b>Group Name :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtGroupDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left; width: 250px"
                                                                    MaxLength="25"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <b>Group Description :</b>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox TextMode="MultiLine" ID="txtGroupDe2" runat="server" CssClass="MediumTextBox"
                                                                    Style="text-align: left; height: 40px; width: 400px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="2">
                                                                <asp:Button ID="btnCreateGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                                                    OnClientClick="return ValidateList();" />
                                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                                    OnClientClick="return MakeInVisible('tabSupplierManager_tabSMSpec_trCreate');" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="Label5" runat="Server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" HeaderText="Structure Manager" ToolTip="Structure Manager"
                ID="tabSMStruct">
                <ContentTemplate>
                    <asp:Panel ID="pnlSMStruct" runat="server">
                        <table class="ContentPage" id="tblStruct" runat="server" style="margin-top: 15px;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                                        Structure Manager
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label6" runat="server" Text="Group Selector:" Style="margin-left: 10px; font-size: 12px;"
                                        Font-Bold="true"></asp:Label>
                                    <asp:LinkButton ID="lnkGrpSelStruct" runat="server" CssClass="SavvyLink" Text="Select Group"
                                        OnClientClick="return ShowGrpSelPopUp('PopUp/GrpSelector.aspx?');"
                                        Style="margin-left: 6px;"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Table ID="tblmain" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>

                                            </td>
                                            <td>
                                                <asp:Table ID="tblJSeq" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%--<asp:Button ID="btnUpdateStruct" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />--%>
                            </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="Label1" runat="Server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" HeaderText="Price & Cost Manager" ToolTip="Price & Cost Manager"
                ID="tabSMPC">
                <ContentTemplate>
                    <asp:Panel ID="pnlSMPC" runat="server">
                        <table class="ContentPage" id="tblPC" runat="server" style="margin-top: 15px; overflow: auto;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="div4" runat="server" style="text-align: center;">
                                        Price and Cost Manager
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tr>
                                            <td>
                                                <b>SKU:</b><asp:Label ID="lblSKU" runat="server" CssClass="NormalLable"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span id="caseDe3" runat="server"><b>SKU Description:</b></span>
                                                <asp:Label ID="lblSKUDesc" runat="server" CssClass="NormalLable"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                                    </asp:Table>
                                    <br />
                                    <asp:TextBox ID="txthiddien" Style="visibility: hidden;" runat="server" Text="0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="Label2" runat="Server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" HeaderText="Investment Manager" ToolTip="Investment Manager"
                ID="tabSMInvest">
                <ContentTemplate>
                    <asp:Panel ID="pnlSMInvest" runat="server">
                        <table class="ContentPage" id="tblSMInvest" runat="server" style="margin-top: 15px;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="div2" runat="server" style="text-align: center;">
                                        Investment Manager
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Table ID="tblInvest" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                                    <br />
                                    <%--<asp:Button ID="btnupdate" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />--%>
                                </td>
                            </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="Label3" runat="Server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" HeaderText="Terms" ToolTip="Terms"
                ID="tabSMTerms">
                <ContentTemplate>
                    <asp:Panel ID="pnlSMTerms" runat="server">
                        <table class="ContentPage" id="tblSMTerms" runat="server" style="margin-top: 15px;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="div5" runat="server" style="text-align: center;">
                                        Terms Manager
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <br />
                                    <asp:Table ID="tblterm" runat="server" CellPadding="4" CellSpacing="3"></asp:Table>
                                    <br />
                                </td>
                            </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="Label4" runat="Server"></asp:Label>
                                </td>
                            </tr>
                        </table>
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
        <asp:HiddenField ID="hidPMGrpId" runat="server" />
        <asp:HiddenField ID="hidPMGrpNm" runat="server" />
        <asp:HiddenField ID="hidRowNum" runat="server" Value="2" />
        <asp:HiddenField ID="hidEqId" runat="server" />
    </form>
</body>
</html>
