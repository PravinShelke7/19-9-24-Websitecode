<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectType_Vendor.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_SelectType_Vendor" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Select RFP/RFI</title>
    <script type="text/javascript">
        function RFPSearch(Id, Name,OwnerEID) {
            var hidRfpID = document.getElementById('<%= hidRfpID.ClientID%>').value;
            var hidRfpDes = document.getElementById('<%= hidRfpDes.ClientID%>').value;
            var hidRfpIT = document.getElementById('<%= hidRfpIT.ClientID%>').value;
            //var hidRSOwnerEmailID = document.getElementById('<%= hidOwnerEID.ClientID%>').value;

            Name = Name.replace(new RegExp("&#", 'g'), "'");
            OwnerEID = OwnerEID.replace(new RegExp("&#", 'g'), "'");

            window.opener.document.getElementById(hidRfpID).value = Id;
            window.opener.document.getElementById(hidRfpIT).innerText = Name;
            window.opener.document.getElementById(hidRfpDes).value = Name;
            window.opener.document.getElementById('hidRSOwnerEmailID').value = OwnerEID;
            window.opener.document.getElementById('btnHidRFPCreate').click();
            window.close();

        }
    </script>
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

        #ContentPagemargin {
            margin-left: 20px;
            text-align: left;
        }

        #PageSection1 {
            background-color: #D3E7CB;
        }

        .AlterNateColor1 {
            background-color: #C0C9E7;
        }

        .AlterNateColor2 {
            background-color: #D0D1D3;
        }

        .AlterNateColor4 {
            background-color: #000000;
            color: White;
            height: 20px;
        }

        .CalculatedFeilds {
            color: black;
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
    </style>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
        <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0;">
            <div id="PageSection1" style="text-align: left">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <br />
                <table cellpadding="0" cellspacing="2" style="margin-left:10px;">
                    <tr>
                        <td align="right" style="font-size: 13px;">
                            <b>Keyword:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtKeyword" runat="server" CssClass="LongTextBox" Width="200px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
                <br />
                <table cellspacing="0" style="margin-left:10px;">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="SavvyLink" CausesValidation="false" ID="lnkShowAll" runat="server" />
                        </td>
                    </tr>
                </table>
                <div style="width: 940px; height: 300px; overflow: auto; margin-left:10px;"">
                    <asp:Label ID="lblNRF" runat="server" Text="No Record Found." Visible="false"></asp:Label>
                    <asp:GridView Width="920px" CssClass="grdProject" runat="server" ID="grdRfpDetails"
                        DataKeyNames="ID" AllowPaging="True" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                        Font-Size="11px" Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" EnableModelValidation="True">
                        <FooterStyle BackColor="#CCCC99" />
                        <RowStyle BackColor="#F7F7DE" CssClass="row" />
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false"></asp:BoundField>
                            <asp:TemplateField HeaderText="RFP Name" SortExpression="DES1">
                                <ItemTemplate>
                                    <a href="#" onclick="RFPSearch('<%#Container.DataItem("ID")%>','<%#Container.DataItem("RFPDESPOS")%>','<%#Container.DataItem("VENDOREMAILIDPOS")%>')"
                                        class="SavvyLink">
                                        <%# Container.DataItem("DES1")%>
                                    </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="140px" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="DES2" HeaderText="Description" SortExpression="DES2">
                                <ItemStyle Width="220px" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OWNER" HeaderText="Owner" SortExpression="OWNER">
                                <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundField>
                             <asp:BoundField DataField="BUYER" HeaderText="Buyer" SortExpression="BUYER">
                                <ItemStyle Width="120px" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FCDATE" HeaderText="Creation Date" SortExpression="CREATIONDATE">
                                <ItemStyle Width="160px" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="FUDATE" HeaderText="Last Update Date" SortExpression="UPDATEDATE">
                                <ItemStyle Width="160px" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundField> 
                            <asp:BoundField DataField="TYPEDESC" HeaderText="Type" SortExpression="TYPEDESC">
                                <ItemStyle Width="50px" Wrap="True" HorizontalAlign="Left" />
                            </asp:BoundField>                              
                        </Columns>
                        <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                            HorizontalAlign="Left" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </div>
                <br />
            </div>
            <asp:HiddenField ID="hidSortId" runat="server" />
            <asp:HiddenField ID="hidRfpID" runat="server" />
            <asp:HiddenField ID="hidRfpDes" runat="server" />
            <asp:HiddenField ID="hidRfpIT" runat="server" />
            <asp:HiddenField ID="hidOwnerEID" runat="server" />
        </div>
    </form>
</body>
</html>
