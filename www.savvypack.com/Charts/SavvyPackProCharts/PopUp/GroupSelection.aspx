<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupSelection.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_PopUp_Group_Selection" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
  
    <title></title>

 <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/SDistComman.js"></script>
         <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
             <script type="text/JavaScript">
                 function CaseSearch(CaseDes, CaseId) {
                  
                var hidCounDes = document.getElementById('<%= hidGroupDes.ClientID%>').value;
                var hidCounId = document.getElementById('<%= hidGroupId.ClientID%>').value;
                var hidLinkId = document.getElementById('<%= hidlnkdes.ClientID%>').value;
                     window.opener.document.getElementById(hidLinkId).innerText = CaseDes;
                window.opener.document.getElementById("hidGroupId").value = CaseId;
                window.opener.document.getElementById("hidGroupDes").value = CaseDes;
//                window.opener.document.getElementById("btnRefresh").click(); 
                window.close();
            }
           
            function ClosePage(vendors) {
                alert("hi");
                window.opener.document.getElementById("lnkvendor").innerText = vendors;
                //location.reload();
                alert("1");
                window.close();
            }
    </script>
    <script type="text/JavaScript">
        function CheckSP(text, hidvalue) {
            var sequence = document.getElementById(text.id).value;
            if (sequence != "") {
                if (isNaN(sequence)) {
                    alert("Sequence must be in number");
                    document.getElementById(text.id).value = "";
                    document.getElementById(text.id).value = hidvalue;
                    return false;
                }
            }
            else {
                alert("Please enter sequence");
                document.getElementById(text.id).value = hidvalue;
                return false;
            }

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            else {
                document.getElementById('btnSeq').click();
                return true;
            }
        }

    </script>
    <style type="text/css">
        tr.row
        {
            background-color: #fff;
        }
        
        tr.row td
        {
        }
        
        tr.row:hover td, tr.row.over td
        {
            background-color: #eee;
        }
        
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
        
        a.SavvyLink:link
        {
            font-family: Verdana;
            color: black;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            font-family: Verdana;
            color: Black;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            font-family: Verdana;
            color: Red;
            font-size: 11px;
        }
        
        .SingleLineTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 14px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }
        
        .AlternateColorAct1
        {
            font-family: Verdana;
            background-color: #dfe8ed;
        }
        
        .MultiLineTextBoxG
        {
            font-family: Verdana;
            font-size: 10px;
            width: 320px;
            height: 50px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
        
        .SingleLineTextBox_G
        {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 15px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }
        
        .divUpdateprogress_SavvyPro
        {
            left: 610px;
            top: 400px;
            position: absolute;
        }
        .style1
        {
            width: 100%;
        }
    </style>
    <script type="text/JavaScript">
        javascript: window.history.forward(1);
        function MakeVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;
        }

        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;
        }

        function ValidateList() {
            var name = document.getElementById("txtRFPNm").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter RFP Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }

    </script>

</head>
<body>
    <form id="form1" runat="server">
      
    <div>
   <%--   <asp:GridView Width="220px" runat="server" ID="grdGroup" DataKeyNames="FORMGROUPID"
                    AutoGenerateSelectButton="false" AllowPaging="false" 
            AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" ForeColor="#333333" 
            GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="FORMGROUPID" HeaderText="Price Column Id" SortExpression="FORMGROUPID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Price Column Name" SortExpression="FORMAT">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("FORMAT")%>','<%#Container.DataItem("FORMGROUPID")%>')"
                                    class="Link">
                                    <%#Container.DataItem("FORMGROUPID")%>:<%# Container.DataItem("FORMAT")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Price Column Name" SortExpression="FORMAT">
                            <ItemTemplate>
                                
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>--%>
                <table class="PageSection1" id="PageSection1" runat="server" style="margin-top: 15px; width: 600px;">
                    <tr>
                        <td>
                         <asp:Label ID="lblGroupNoFound" runat="server" Text="No record found." Visible="false"></asp:Label>
                <asp:Table ID="tblPRICE" runat="server" CellPadding="0" CellSpacing="1" Style="margin-left: 50px;">
                    </asp:Table>
                        </td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
        </table>
        <asp:HiddenField ID="hidGroupId" runat="server" />
        <asp:HiddenField ID="hidGroupDes" runat="server" />
        <asp:HiddenField ID="hidlnkdes" runat="server" />

    </div>
    </form>
</body>
</html>
