<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditGroupPopup.aspx.vb"
    Inherits="Pages_SavvyPackPro_Popup_EditGroupPopup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Create </title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

        function CheckSP(text) {

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
        }

       

        }

        function ValidateGroup() {
        debugger;

            var name = document.getElementById("txtGrpName").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter Group Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }

        function ValidateList1() {

            var Desc = document.getElementById("tabSupplierManager_tabMasterGroup_txtDes").value;
            var name = document.getElementById("tabSupplierManager_tabMasterGroup_txtMGNm").value;
            var TypeSel = document.getElementById("tabSupplierManager_tabMasterGroup_ddlMasterGroup").value; //tabSupplierManager_tabMasterGroup_ddlMasterGroup

            if (name == "") {
                var msg1 = "-------------------------------------------------\n";
                msg1 += "    Please enter Master Group Name.\n";
                msg1 += "-------------------------------------------------\n";
                alert(msg1);
                return false;
            }
            else if (Desc == "") {
                var msg2 = "-------------------------------------------------\n";
                msg2 += "    Please enter Description.\n";
                msg2 += "-------------------------------------------------\n";
                alert(msg2);
                return false;
            }
            else if (TypeSel == "0") {
                var msg3 = "-------------------------------------------------\n";
                msg3 += "    Please select Type.\n";
                msg3 += "-------------------------------------------------\n";
                alert(msg3);
                return false;

            }
            return true;
        }


        function ClosePage() {
           
            alert("Group created successfully");
//            window.opener.document.getElementById('btnrefreshEG').click();
//            window.close();
        }


        function SelectAllCheckboxes(spanChk) {
            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?

                spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
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
    </style>
    <%--<style type="text/css">
        a.SavvyLink:link
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            color: Red;
            font-size: 11px;
        }
        
        .PSavvyModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background: url('../../Images/AdminHeader.gif') no-repeat;
            height: 45px;
            width: 1220px;
            text-align: center;
            vertical-align: middle;
        }
        
        .PageHeading
        {
            font-size: 18px;
            font-weight: bold;
        }
        
        .ContentPage
        {
            margin-top: 2px;
            margin-left: 1px;
            width: 1220px;
            background-color: #F1F1F2;
        }
        
        #SavvyPageSection1
        {
            background-color: #D3E7CB;
        }
        
        .AlterNateColor3
        {
            background-color: #D3DAD0;
            height: 20px;
        }
        
        .PageSHeading
        {
            font-size: 12px;
            font-weight: bold;
        }
        
        .InsUpdMsg
        {
            font-family: Verdana;
            font-size: 12px;
            font-style: italic;
            color: Red;
            font-weight: bold;
        }
        
        .LongTextBox
        {
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
        
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
        
        .style2
        {
            width: 9px;
        }
        
        .Comments
        {
            font-family: Optima;
            font-size: 12px;
            height: 16px;
            width: 265px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            background-color: #FEFCA1;
            text-align: left;
        }
        
        .Amount
        {
            font-family: Optima;
            font-size: 11.5Px;
            height: 17px;
            width: 50px;
            background-color: #FEFCA1;
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
        
        .NormalLable_Term
        {
            font-family: Verdana;
            font-size: 18px;
            height: 15px;
            width: 100px;
            margin-right: 3px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 5px;
            text-align: left;
        }
    </style>--%>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <%--<asp:Image ImageAlign="AbsMiddle" Width="1350px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />--%>
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <table style="width: 100%; font-family: Verdana; font-size: 14px;">
        <tr valign="top" style="background-color: #dfe8ed;">
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td class="PageSHeading" style="font-size: 16px; width: 800px;">
                            <div style="width: 800px; text-align: center;">
                                Group Details
                            </div>
                        </td>
                    </tr>
                    <tr runat="server">
                        <td id="Td10" runat="server">
                            <table style="width: 100%; background-color: #7F9DB9;">
                                <tr>
                                    <td>
                                        <b>Group Name:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGrpName" runat="server" Width="250px" CssClass="MediumTextBox"
                                            Style="text-align: left;" BackColor="#FEFCA1"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Group Description:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox TextMode="MultiLine" ID="txtGrpDescription" runat="server" MaxLength="499"
                                            CssClass="MediumTextBox" Style="text-align: left; height: 40px; width: 400px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <b>Select Master Group:</b>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlMGrp" runat="server" Width="120px" CssClass="DropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCreateGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin" />
                                        <asp:Button ID="btnEditRFP" runat="server" Text="Update " Visible="False" CssClass="ButtonWMarigin" />
                                        <asp:Button ID="btnCancleGrp" runat="server" Text="Cancel" Visible="False" CssClass="ButtonWMarigin" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 10px;">
                            <table>
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox
                                                        ID="txtKeyWordGM" runat="server" CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button
                                                            ID="btnSrchGM" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right;
                                                        font-size: 12px;"></asp:Label><asp:DropDownList ID="ddlPageCountGM" runat="server"
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
                                                    <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right;
                                                        padding-left: 20px;"></asp:Label><asp:Label ID="lblRcrdCountGM" runat="server" CssClass="NormalLabel"
                                                            ForeColor="Red" Font-Bold="True"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblSpecNoFound" runat="server" Visible="False" Text="No records found."></asp:Label>
                                        <asp:GridView Width="800px" CssClass="grdProject" runat="server" ID="grdAllGrpMngr"
                                            DataKeyNames="MGROUPDETID" AllowPaging="True" PageSize="10" AllowSorting="True"
                                            AutoGenerateColumns="False" AutoGenerateSelectButton="True" Font-Size="11px"
                                            Font-Names="Verdana" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" EnableModelValidation="True">
                                            <FooterStyle BackColor="#CCCC99" />
                                            <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                            <Columns>
                                                <asp:TemplateField Visible="false">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="GRPID" Visible="false" Value='<%#bind("GRPID")%>' runat="server">
                                                        </asp:HiddenField>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Group Id"   SortExpression="GRPID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrpId" runat="server" Text='<%# Bind("GRPID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Group Name" SortExpression="GRPNM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrpNm" runat="server" Text='<%# Bind("GRPNM")%>'></asp:Label>
                                                        <%-- <asp:Label ID="lblMGrpId" runat="server" Visible="false" Text='<%# Bind("MASTERGID")%>'></asp:Label>--%>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Group Des" SortExpression="GRPDES">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" runat="server" Visible="false" Text='<%# bind("USERID")%>'></asp:Label>
                                                        <asp:Label ID="lblGrpDes" runat="server" Text='<%# Bind("GRPDES")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Master Group" SortExpression="MASTER_TYPE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMGTId" runat="server" Text='<%# Bind("MASTER_TYPE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner" SortExpression="OWNER">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOwner" runat="server" Text='<%# Bind("OWNER")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="120px" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Creation Date" SortExpression="CREATIONDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCD" runat="server" Text='<%# Bind("CREATIONDATE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="True" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Update Date" SortExpression="UPDATEDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUpD" runat="server" Text='<%# Bind("UPDATEDATE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" Width="180px" Wrap="True" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                                HorizontalAlign="Left" />
                                           <%-- <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />--%>
                                            <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidSortIdGrpMngr" runat="server" />
    <asp:HiddenField ID="hidGID" runat="server" />
    <asp:Button ID="btnrefreshEG" runat="server" Style="display: none;" />
    </form>
</body>
</html>
