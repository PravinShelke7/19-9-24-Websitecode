<%@ Page Title="Product Tree" Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="ProductTree.aspx.vb" Inherits="Pages_Market1_ProductTree" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
<div  >
<div style="font-size:20px;font-weight:bold; text-align:center;margin-bottom:10px;margin-top:10px;width:600px;height:20px;">
                     <span style="color:Black">Product Tree</span> 
                      </div>
                      <div style="font-size:12px; font-family:Verdana; color:black;font-weight:bold; padding-left:10px; " >
                        <asp:TreeView ID="prdTree" runat="server" ShowExpandCollapse="true" ShowLines="true" ForeColor="Black"  BackColor="#C0C9E7" BorderWidth="1px" >
                   </asp:TreeView>
                      </div>
                </div>
</asp:Content>

