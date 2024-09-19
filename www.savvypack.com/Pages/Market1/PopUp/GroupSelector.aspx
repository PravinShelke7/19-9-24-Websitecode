<%@ Page Title="Group Selector" Language="VB" MasterPageFile="~/Masters/M1PopUp.master" AutoEventWireup="false" CodeFile="GroupSelector.aspx.vb" Inherits="Pages_Market1_PopUp_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/JavaScript">
    function CategorySelection(RowDes, RowValue, RowID) {


        var hidCatDes = document.getElementById('<%= hidCatDes.ClientID%>').value
        var hidCatDes1 = document.getElementById('<%= hidCatDes1.ClientID%>').value
        var hidCatId = document.getElementById('<%= hidCatID.ClientID%>').value
        //alert(hidCatDes);
        //alert(hidCatId);
        window.opener.document.getElementById(hidCatId).value = RowValue
        window.opener.document.getElementById(hidCatDes1).value = RowDes
        window.opener.document.getElementById(hidCatDes).innerText = RowDes
        //window.opener.document.getElementById(hidCatDes).value = RowDes

        //window.opener.document.getElementById(hidRowDes).style.color = 'white';
        //             alert(RowValue);
        //             alert(RowDes);
        window.close();
    }
 </script>
 <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>

        <asp:TreeView ID="grpTree" runat="server" ShowExpandCollapse="true" Font-Size="14px" ShowLines="true" ForeColor="Black"   BorderWidth="1px"   >
                   </asp:TreeView>
        </div>
        </div>
        <asp:HiddenField  ID="hidCatID" runat="server" />
        <asp:HiddenField  ID="hidCatDes" runat="server" />
         <asp:HiddenField  ID="hidCatDes1" runat="server" />
</asp:Content>

