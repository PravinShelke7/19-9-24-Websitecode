﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PriceSelectionGrp.aspx.vb" Inherits="Charts_SavvyPackProCharts_PopUp_PriceSelectionGrp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Price Selection</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
         <script type="text/JavaScript">
             function CaseSearch(CaseDes, CaseId) {
                 var hidCounDes = document.getElementById('<%= hidPriceDes.ClientID%>').value;
                 var hidCounId = document.getElementById('<%= hidPriceId.ClientID%>').value;
                 var hidLinkId = document.getElementById('<%= hidlnkdes.ClientID%>').value;
                 window.opener.document.getElementById(hidLinkId).innerText = CaseDes;
                 window.opener.document.getElementById("hidPriceId").value = CaseId;
                 window.opener.document.getElementById("hidPriceDes").value = CaseDes;
                 window.opener.document.getElementById("btnRefreshGrp").click();
                 window.close();
             }
        
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView Width="250px" CssClass ="grdProject" runat="server" ID="grdCountry" DataKeyNames="RFPPRICEID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#CCCC99" />
                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                 <Columns>
                        <asp:BoundField DataField="RFPPRICEID" HeaderText="Price Option Id" SortExpression="RFPPRICEID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Price  Name" SortExpression="PRICEDETAIL">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("PRICEDETAIL")%>','<%#Container.DataItem("RFPPRICEID")%>')"
                                    class="Link">
                                    <%#Container.DataItem("RFPPRICEID")%>:<%# Container.DataItem("PRICEDETAIL")%></a>
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
        <asp:HiddenField ID="hidPriceId" runat="server" />
        <asp:HiddenField ID="hidPriceDes" runat="server" />
        <asp:HiddenField ID="hidlnkdes" runat="server" />
    </div>
    </form>
</body>
</html>
