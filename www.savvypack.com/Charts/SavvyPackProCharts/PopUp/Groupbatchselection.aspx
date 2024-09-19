<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Groupbatchselection.aspx.vb" Inherits="Charts_SavvyPackProCharts_PopUp_Groupbatchselection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Group Selection</title>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/SDistComman.js"></script>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function CaseSearch(GrpDes, GrpId) {

            var hidGrpDes = document.getElementById('<%= hidGroupDes.ClientID%>').value;
            var hidGrpId = document.getElementById('<%= hidGroupId.ClientID%>').value;
            var hidLinkId = document.getElementById('<%= hidlnkdes.ClientID%>').value;
            var type = document.getElementById('<%= hidType.ClientID%>').value;

            GrpDes = GrpDes.replace(new RegExp("&#", 'g'), "'");
            window.opener.document.getElementById(hidLinkId).innerText = GrpDes;
            window.opener.document.getElementById("hidGroupId").value = GrpId;
            window.opener.document.getElementById("hidGroupDes").value = GrpDes;
            if (type == "Analysis") {
                window.opener.document.getElementById("btnBatchPageRef").click();
            }

            window.close();

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

        .divUpdateprogress_SavvyPro {
            left: 610px;
            top: 400px;
            position: absolute;
        }
    </style>
    <style type="text/css">
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            <table class="PageSection1" id="PageSection1" runat="server" style="margin-top: 15px; width: 470px;">
                <tr>
                    <td colspan="2" style="font-size: 17px; font-weight: bold; text-align: center; margin-bottom: 5px; width: 100%; height: 25px; left: 10px; text-align: center;">Select Group
                    </td>
                </tr>
                <tr>
                    <td style="padding-left :10px;">
                        <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox
                            ID="txtKeyword" runat="server" CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button
                                ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                    </td>                    
                </tr>
                <tr>
                    <td style="padding-left:10px;">
                        <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right; font-size: 12px;"></asp:Label><asp:DropDownList ID="ddlPageCountC" runat="server"
                            Width="55px" CssClass="DropDown" AutoPostBack="True">
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
                        <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right; padding-left: 20px;"></asp:Label><asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel"
                            ForeColor="Red" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>                       
                        <div style="width: 280px; height: 310px; overflow: auto; margin-left: 10px;">
                            <asp:Label ID="lblGroupNoFound" runat="server" Text="No Record Found." Visible="false"></asp:Label>
                            <asp:GridView Width="260px" CssClass="grdProject" runat="server" ID="grdGroup"
                                DataKeyNames="OTHERPREFRFPID" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                Font-Size="11px" Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" EnableModelValidation="True">
                                <FooterStyle BackColor="#CCCC99" />
                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                <Columns>

                                    <asp:BoundField DataField="OTHERPREFRFPID" HeaderText="Column Id" SortExpression="OTHERPREFRFPID" Visible="false"></asp:BoundField>
                                    <asp:TemplateField HeaderText=" Group Combination Name" SortExpression="OTHERPREFRFPID">
                                        <ItemTemplate>
                                            <a href="#" onclick="CaseSearch('<%#Container.DataItem("CODEPOS")%>','<%#Container.DataItem("OTHERPREFRFPID")%>')"
                                                class="Link">
                                                <%# Container.DataItem("CODE")%></a>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" />
                                        <HeaderStyle HorizontalAlign="Left" />
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
            <asp:HiddenField ID="hidGroupId" runat="server" />
            <asp:HiddenField ID="hidGroupDes" runat="server" />
            <asp:HiddenField ID="hidlnkdes" runat="server" />
            <asp:HiddenField ID="hidRfpPriceOpID" runat="server" />
            <asp:HiddenField ID="hidType" runat="server" />
            <asp:HiddenField ID="hidSortIdMGroup" runat="server" />
        </div>
    </form>
</body>
</html>

