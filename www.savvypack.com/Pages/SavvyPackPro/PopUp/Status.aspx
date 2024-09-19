<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Status.aspx.vb" Inherits="Pages_SavvyPackPro_PopUp_Status" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Status</title>
      <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="overflow: auto;" runat="server">
      <asp:Label ID="lblPPC" runat="server" text="Status:" Font-Bold="True" Style="text-align: right;
            padding-left: 20px;"></asp:Label>
            <asp:GridView Width="250px" CssClass="grdProject" runat="server" ID="grdstatus"
            DataKeyNames="DATEID" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
            Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" >
            <PagerSettings Position="Top" />
            <FooterStyle BackColor="#CCCC99" />
            <RowStyle BackColor="#F7F7DE" CssClass="row" />
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <asp:Label ID="lblstatusid" runat="server" Text='<%# Bind("DATEID")%>'></asp:Label>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                </asp:TemplateField>
               

                <asp:TemplateField HeaderText="Status Order">
                    <ItemTemplate>
                        <asp:Label ID="lblstatusdes" runat="server" Text='<%# Bind("DES")%>'></asp:Label>
                    </ItemTemplate>
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
    </form>
</body>
</html>
