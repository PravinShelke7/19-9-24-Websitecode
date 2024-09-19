<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjActivityLog.aspx.vb" Inherits="OnlineForm_ProjActivityLog" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Subscription Activity Log</title>
    <link href="../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
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
        });
    </script>
    <script type="text/JavaScript">

        function ShowSubscriptionPopUp(Page) {
            var LID = document.getElementById("hidLicenseID").value;
            var width = 445;
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
            Page = Page + "&Lid=" + LID;
            newwin = window.open(Page, 'SubScription', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
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

        .ContentPage_In {
            margin-top: 2px;
            margin-left: 1px;
            width: 1160px;
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

        .LongTextBox_IN {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 320px;
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="Div1">
                <asp:Image ImageAlign="AbsMiddle" Width="1160px" ID="Image1" ImageUrl="~/Images/SavvyPackProject1268x45.gif"
                    runat="server" />
            </div>
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table class="ContentPage_In" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                        Subscription Activity Log
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="SavvyContentPagemargin" runat="server">
                        <div id="SavvyPageSection1" style="text-align: left;">
                            <table style="width: 100%">
                                <tr style="height: 20px">
                                    <td>
                                        <div style="text-align: left; background-color: #D3E7CB;">
                                            <table style="width: 100%">
                                                <tr style="font-size: 11px; font-family:Verdana;">
                                                    <td style ="vertical-align:top;">
                                                        <asp:LinkButton ID="lnkSelSubscrp" runat="server" CssClass="SavvyLink" Text="Select Subscription Year"
                                                            OnClientClick="return ShowSubscriptionPopUp('PopUp/SubscriptionList.aspx?Id=hidSubscrpID&Innertxt=lnkSelSubscrp&Nm=hidSubScrpName');"
                                                            Style="margin-left: 6px;"></asp:LinkButton>
                                                    </td>
                                                    <td style ="vertical-align:top; padding-left:40px;">
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lblsdate" runat="server" Text="Subscription Start Date:"
                                                                        Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSubStartDate" runat="server" Text="NA"
                                                                        Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="lbledate" runat="server" Text="Subscription Renewal Date:"
                                                                        Font-Bold="true"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblSubEndDate" runat="server" Text="NA"
                                                                        Font-Bold="true"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style ="vertical-align:top;">
                                                        <asp:Label ID="lblKeywordSB" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                                            Style="margin-left: 162px;" Font-Bold="true"></asp:Label>
                                                        <asp:TextBox ID="txtSearchSB" runat="server" CssClass="LongTextBox_IN"></asp:TextBox>
                                                        <asp:Button ID="btnSearchSB" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                    </td>
                                                </tr>
                                                <tr id="trpagecountSB" runat="server" visible="false">
                                                    <td colspan="3" style="font-size: 11px; font-family:Verdana; padding-top:10px;">
                                                        <asp:Label ID="lblPageSizeSB" runat="server" Text="Page Size:" Font-Bold="true" Style="text-align: right; margin-left: 10px;"></asp:Label>
                                                        <asp:DropDownList ID="ddlPageCountSB" runat="server" Width="55px" CssClass="DropDown"
                                                            AutoPostBack="true">
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                            <asp:ListItem Value="100" Selected="True">100</asp:ListItem>
                                                            <asp:ListItem Value="200">200</asp:ListItem>
                                                            <asp:ListItem Value="300">300</asp:ListItem>
                                                            <asp:ListItem Value="400">400</asp:ListItem>
                                                            <asp:ListItem Value="500">500</asp:ListItem>
                                                            <asp:ListItem Value="1000">1000</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:Label ID="lblProj" runat="server" Text="Project(s):" Font-Bold="true" Style="text-align: right; padding-left: 50px;"></asp:Label>
                                                        <asp:Label ID="lblNOP" runat="server" CssClass="NormalLabel" ForeColor="Red"
                                                            Font-Bold="true"></asp:Label>
                                                        <asp:Label ID="lblModels" runat="server" Text="Model(s):" Font-Bold="true" Style="text-align: right; padding-left: 50px;"></asp:Label>
                                                        <asp:Label ID="lblNOM" runat="server" CssClass="NormalLabel" ForeColor="Red"
                                                            Font-Bold="true"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" style="padding-left: 10px; padding-top:5px; text-align:center;">
                                                        <asp:Label ID="lblMsg" runat="server" Visible="false"
                                                            Text="No SavvyPack Subscription Actitvity Log to display." Font-Size="11"
                                                            Style="text-align: center; margin-top: 30px; color: red; font-weight: bold;"></asp:Label>
                                                        <asp:GridView Width="1120px" CssClass="grdProject" runat="server" ID="grdProjDetail"
                                                            DataKeyNames="PROJECTID" AllowPaging="True" PageSize="100" AllowSorting="True"
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
                                                                <asp:BoundField DataField="PROJTITLE" HeaderText="Project Name" SortExpression="PROJTITLE">
                                                                    <ItemStyle Width="220px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="POWNER" HeaderText="Project Sponsor" SortExpression="POWNER">
                                                                    <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="SUBMITDATE" HeaderText="Project Submitted" SortExpression="SDATE">
                                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="COMPLETEDATE" HeaderText="Project Complete" SortExpression="CDATE">
                                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="PROJDAYS" HeaderText="Project Days" SortExpression="PROJDAYS">
                                                                    <ItemStyle Width="30px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="ModelCompleted" HeaderText="Individual Models Completed" SortExpression="ModelCompleted">
                                                                    <ItemStyle Width="30px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                                <asp:BoundField DataField="GRPMODELCOMPLETED" HeaderText="Group Models Completed" SortExpression="GRPMODELCOMPLETED">
                                                                    <ItemStyle Width="30px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                    <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                </asp:BoundField>
                                                            </Columns>
                                                            <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
                                                                HorizontalAlign="Left" />
                                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                            <AlternatingRowStyle BackColor="White" />
                                                        </asp:GridView>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
        <div id="AlliedLogo">
            <table>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 1160px; background-color: #edf0f4;">
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
        <asp:HiddenField ID="hidSubscrpID" runat="server" />
        <asp:HiddenField ID="hidSubScrpName" runat="server" />
        <asp:HiddenField ID="hidSortId" runat="server" />
        <asp:HiddenField ID="hidSubSdate" runat="server" />
        <asp:HiddenField ID="hidSubEdate" runat="server" />
        <asp:HiddenField ID="hidLicenseID" runat ="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </form>
</body>
</html>

