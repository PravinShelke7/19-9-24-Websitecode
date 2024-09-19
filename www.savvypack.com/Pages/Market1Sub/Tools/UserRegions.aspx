<%@ Page Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="UserRegions.aspx.vb" Inherits="Pages_Market1_Tools_UserRegions" title="Market1-User Regions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

<div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
            <asp:HyperLink ID="hypNew" runat="server" Text="Add New" CssClass="Link"></asp:HyperLink>
          <br />
           <br />
            <asp:Table ID="tbl" runat="server" CellPadding="0" CellSpacing="1" Width="800px" ></asp:Table>
            <br />
          </div>
</div>
</asp:Content>
