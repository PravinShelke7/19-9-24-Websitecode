<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PriceColumnSelection.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_PopUp_PriceColumnSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Price Column Selection</title>
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
         <script type="text/JavaScript">
             function CaseSearch(CaseDes, CaseId) {
                 var hidCounDes = document.getElementById('<%= hidPriceColDes.ClientID%>').value;
                 var hidCounId = document.getElementById('<%= hidPriceColId.ClientID%>').value;
                 var hidLinkId = document.getElementById('<%= hidlnkdes.ClientID%>').value;
                 window.opener.document.getElementById(hidLinkId).innerText = CaseDes;
                 window.opener.document.getElementById("hidPriceColId").value = CaseId;
                 window.opener.document.getElementById("hidPriceColDes").value = CaseDes;
                 //window.opener.document.getElementById("btnRefresh").click(); 

                 window.close();
             }
        
         
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:GridView Width="220px"  CssClass="grdProject" runat="server" ID="grdCountry" DataKeyNames="PRICEREQID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                     <FooterStyle BackColor="#CCCC99" />
                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                    <Columns>
                        <asp:BoundField DataField="PRICEREQID" HeaderText="Price Column Id" SortExpression="PRICEREQID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Price Column Name" SortExpression="DESCRIPTION">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("DESCRIPTION")%>','<%#Container.DataItem("PRICEREQID")%>')"
                                    class="Link">
                                    <%#Container.DataItem("PRICEREQID")%>:<%# Container.DataItem("DESCRIPTION")%></a>
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
        <asp:HiddenField ID="hidPriceColId" runat="server" />
        <asp:HiddenField ID="hidPriceColDes" runat="server" />
        <asp:HiddenField ID="hidlnkdes" runat="server" />

    </div>

    </form>
</body>
</html>
