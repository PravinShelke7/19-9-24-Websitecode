<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IssueRFP.aspx.vb" Inherits="Pages_SavvyPackPro_IssueRFP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Issue</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
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
            left: 610px;
            top: 400px;
            position: absolute;
        }
    </style>
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
                <table style="width: 1110px;">
                    <tr class="AlterNateColor2">
                        <td style="padding-left: 10px; padding-top: 15px;">
                            <ajaxToolkit:TabContainer ID="tabIssueRFP" Height="550px" Width="1090px" runat="server"
                                ActiveTabIndex="0" AutoPostBack="true" Enabled="false">
                                <ajaxToolkit:TabPanel runat="server" HeaderText="Issue" ToolTip="Issue" ID="tabValidate">
                                    <ContentTemplate>
                                        <asp:Panel ID="pnlValidate" runat="server">
                                            <table id="tblValidate" runat="server" style="margin-top: 15px;">
                                                <tr id="Tr1" runat="server">
                                                    <td id="Td1" style="padding-top: 10px;" runat="server">
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
                                                <tr id="Tr2" runat="server">
                                                    <td id="Td2" style="padding-top: 10px;" runat="server">
                                                        <div id="ContentPagemargin" style="overflow: auto; height: 410px;" runat="server">
                                                            <asp:Label ID="lblNOVendor" runat="server" CssClass="NormalLabel" Visible="False"
                                                                Text="No record found."></asp:Label>
                                                            <asp:Label ID="lblDesc" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                                Font-Bold="True" Text="Submit to following vendors." Height="30px"></asp:Label>
                                                            <asp:GridView Width="1035px" CssClass="grdProject" runat="server" ID="grdUsers" DataKeyNames="VENDORID"
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
                                                                    <asp:BoundField DataField="ZIPCODE" HeaderText="ZIPCODE" SortExpression="ZIPCODE">
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
                                                <tr id="Tr3" runat="server">
                                                    <td id="Td3" style="padding-left: 10px;" runat="server">
                                                        <asp:Button ID="btnValidate" runat="server" Text="Validate" CssClass="ButtonWMarigin" />
                                                    </td>
                                                </tr>
                                                <tr id="Tr4" runat="server">
                                                    <td id="Td4" style="padding-left: 10px;" runat="server">
                                                        <asp:Button ID="btnSend" runat="server" Text="Issue RFP" CssClass="ButtonWMarigin" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </ajaxToolkit:TabPanel>
                            </ajaxToolkit:TabContainer>
                        </td>
                    </tr>
                    <tr id="Tr5" class="AlterNateColor3" runat="server">
                        <td id="Td5" class="PageSHeading" align="center" runat="server">
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
    </form>
</body>
</html>
