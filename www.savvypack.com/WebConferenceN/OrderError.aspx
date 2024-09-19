<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="OrderError.aspx.vb" Inherits="ShoppingCart_OrderError" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="MnContentPage">
        Your Order is not confirmed because of following problems:
        <div style="font-family: Optima; font-size: 20px; font-weight: bold;">
            <asp:Label ID="lblText" runat="server"></asp:Label><br />
        </div>
    </div>
</asp:Content>
