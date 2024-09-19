<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SubscriptionList.aspx.vb" Inherits="OnlineForm_Popup_SubscriptionList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Subscription Selector</title>
    <link href="../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
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
    <script type="text/javascript">
        function SendCaseInfo(SubIDs) {            
            var SubScrpID = document.getElementById('<%= hidSubScrpID.ClientID%>').value;
            //var InnerTxt = document.getElementById('<%= hidInnerTxt.ClientID%>').value;
            //var SubScrpNm = document.getElementById('<%= hidSubScrpNm.ClientID%>').value;
            //Name = Name.replace(new RegExp("&#", 'g'), "'");
            //window.opener.document.getElementById(SubScrpID).value = Id;
            //window.opener.document.getElementById(InnerTxt).innerText = Name;
            //window.opener.document.getElementById(SubScrpNm).value = Name;

            window.opener.document.getElementById(SubScrpID).value = SubIDs;
            window.close();
            window.opener.document.getElementById('btnRefresh').click();
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

        .PageHeading {
            font-size: 18px;
            font-weight: bold;
        }

        .ContentPage_In {
            margin-top: 2px;
            margin-left: 1px;
            width: 400px;
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
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
            <table class="ContentPage_In" id="ContentPage" runat="server">
                <tr>
                    <td>
                        <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                            Subscription Selector
                        </div>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td>
                        <div id="SavvyContentPagemargin" runat="server">
                            <div id="SavvyPageSection1" style="text-align: left;">
                                <table style="width: 100%">
                                    <tr style="height: 20px">
                                        <td>
                                            <div style="text-align: left; background-color: #D3E7CB;">
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="padding-left: 10px;">
                                                            <div style="width: 400px; text-align:center; height: 410px; overflow: auto;">
                                                                <asp:Label ID="lblMsg" runat="server" Visible="false"
                                                                    Text="You currently have no Subscription to display." Font-Size="11"
                                                                    Style="margin-top: 30px; color: red; font-weight: bold;"></asp:Label>
                                                                <asp:GridView Width="390px" CssClass="grdProject" runat="server" ID="grdSubScrp"
                                                                    DataKeyNames="SUBSCDETAILID" AllowPaging="True" PageSize="20" AllowSorting="True"
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
                                                                        <asp:BoundField DataField="SUBSCDETAILID" HeaderText="SUBSCDETAILID" SortExpression="SUBSCDETAILID" Visible="false"></asp:BoundField>
                                                                        <asp:TemplateField>
                                                                            <HeaderTemplate>
                                                                                <input id="HeaderLevelCheckBox" onclick="javascript: SelectAllCheckboxes(this);"
                                                                                    runat="server" type="checkbox" />
                                                                            </HeaderTemplate>
                                                                            <ItemTemplate>
                                                                                <asp:Label ID="lblSUBSCDETAILID" runat="server" Text='<%# Bind("SUBSCDETAILID")%>' Style="display: none"></asp:Label>
                                                                                <asp:Label ID="lblSUBSCNAME" runat="server" Text='<%# Bind("SUBSCNAME")%>' Style="display: none"></asp:Label>
                                                                                <asp:CheckBox ID="SelCase" runat="server"></asp:CheckBox>
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="center" Width="30px" />
                                                                            <HeaderStyle HorizontalAlign="center" Width="30px" />
                                                                        </asp:TemplateField>
                                                                        <asp:BoundField DataField="STARTDATE" HeaderText="Start Date" SortExpression="SDATE">
                                                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        </asp:BoundField>
                                                                        <asp:BoundField DataField="ENDDDATE" HeaderText="End Date" SortExpression="EDATE">
                                                                            <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" CssClass="breakword" />
                                                                            <HeaderStyle HorizontalAlign="Left" Width="120px" />
                                                                        </asp:BoundField>
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
                                                    <tr>
                                                        <td style="text-align:center;">
                                                            <asp:Button runat="server" ID="btnSubmit" Text="Submit" />
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
        </div>
        <asp:HiddenField ID="hidSortId" runat="server" />
        <asp:HiddenField ID="hidSubScrpID" runat="server" />
        <asp:HiddenField ID="hidInnerTxt" runat="server" />
        <asp:HiddenField ID="hidSubScrpNm" runat="server" />
    </form>
</body>
</html>
