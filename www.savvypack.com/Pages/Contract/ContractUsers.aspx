<%@ Page Title="Contract Packager Knowledgebase-User Management" Language="VB"
    MasterPageFile="~/Masters/MContract.master" AutoEventWireup="false" CodeFile="ContractUsers.aspx.vb"
    Inherits="ContractUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContractContentPlaceHolder" runat="Server">
    <script language="javascript" type="text/javascript">
        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }

        function RadioCheck(rb) {

            var gv = document.getElementById("<%=GrdUsers.ClientID%>");
            var rbs = gv.getElementsByTagName("input");

            var row = rb.parentNode.parentNode;
            for (var i = 0; i < rbs.length; i++) {
                if (rbs[i].type == "radio") {
                    if (rbs[i].checked && rbs[i] != rb) {
                        rbs[i].checked = false;
                        break;
                    }
                }
            }

            var table = document.getElementById('<%=GrdUsers.ClientID %>');
            //  alert(table.rows.length);
          //  debugger;
            for (var i = 1; i < table.rows.length ; i++) {
                var Row = table.rows[i];
                var CellValue = Row.cells[1];
                for (j = 0; j < CellValue.childNodes.length; j++) {
                    if (CellValue.childNodes[j].type == "radio") {
                        if (CellValue.childNodes[j].checked == true) {
                            //alert(Row.cells[2].innerText);
                            var page = "Popup/transferLicense.aspx?usr=" + Row.cells[2].innerText + "";
                            showConfirmationWindow(page);
                            return false;
                        }
                        else {

                        }
                    }
                }
            }

            //alert('Sud');
        }
        function showConfirmationWindow(page) {
            var width = 500;
            var height = 255;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=no';
            params += ', titlebar=no';
            params += ', toolbar=no';
            params += ', border: 10px solid #ccc';
            params += ', border-radius: 3px 3px 3px 3px';
            params += ', WS_BORDER=0x800000';
            newwin = window.open(page, "chat", params);
        }

        function CompareCountORG(chkbox) {
            var totalCount = document.getElementById('<%=hidTotalCount.ClientID%>').value;
            var Count = document.getElementById('<%=hidUserCount.ClientID%>').value;
            var limit = document.getElementById('<%=hidLimitCount.ClientID%>').value;

            //  alert('ssss' + totalCount + ' ' + Count);

            if (chkbox.checked == true) {
                if (eval(Count) < eval(totalCount)) {
                    if (limit == "1") {
                        if (confirm('You are going to assign Contract Packager Knowledgebase access to this user. Do you want to proceed?')) {
                            document.getElementById('<%= btnCall.ClientID%>').click();
                            return true;
                        }
                        else {
                            return false;
                        }
                    }
                    else {
                        if (limit == "0") {
                            alert('You can not assign Contract Packager Knowledgebase access to this user because you have reached your maximum license limit of ' + totalCount + '. As well the transfering limit');
                            return false;
                        }
                        else {
                            if (limit == "") {
                                if (confirm('You are going to assign Contract Packager Knowledgebase access to this user. Do you want to proceed?')) {
                                    document.getElementById('<%= btnCall.ClientID%>').click();
                                    return true;
                                }
                                else {
                                    return false;
                                }
                            }
                            else {
                                if (limit == "2") {
                                    alert('You must remove Contract Packager Knowledgebase access from any user because you have already used the transfer for the previous one.');
                                    return false;
                                }
                            }
                        }
                    }
                }
                else {
                    alert('You can not assign Contract Packager Knowledgebase access to this user because you have reached your maximum license limit of ' + totalCount + '.');
                    return false;
                }
            }
            else {

            }
        }

        function CompareCount(chkbox) {

            var totalCount = document.getElementById('<%=hidTotalCount.ClientID%>').value;
            var Count = document.getElementById('<%=hidUserCount.ClientID%>').value;
            var limit = document.getElementById('<%=hidLimitCount.ClientID%>').value;

            //  alert('ssss' + totalCount + ' ' + Count);
            var table = document.getElementById('<%=grdUsers.ClientID %>');
            for (var i = 1; i < table.rows.length; i++) {
                var Row = table.rows[i];
                var CellValue = Row.cells[0];
                for (j = 0; j < CellValue.childNodes.length; j++) {
                    if (CellValue.childNodes[j].type == "checkbox") {
                        if (CellValue.childNodes[j].checked == true) {
                            var userName = Row.cells[2].innerText;
                            if (eval(Count) < eval(totalCount)) {
                                if (confirm('You are going to assign Contract Packager Knowledgebase access to user ' + userName + ' . Do you want to proceed?')) {
                                    document.getElementById('<%= btnCall.ClientID%>').click();
                                    return true;
                                }
                                else {
                                    return false;
                                }

                            }
                            else {
                                alert('You can not assign Contract Packager Knowledgebase access to user ' + userName + ' because you have reached your maximum license limit of ' + totalCount + '.');
                                return false;
                            }
                        }

                    }
                }
            }

        }
    </script>
    <div class="divMargin" style="width:830px">
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
    
                                <div class="AlterNateColor4">
                                <div class="PageSHeading" style="font-size:16px;">
                                  Manage Contract Packager Knowledgebase Users
                                </div>
                                  
                                </div>
                               
                           <table>
                             <tr>
                                 <td>
                                     <div style="margin-left:0px">
                                        <table style="width: 350px">
                                            <tr>
                                                <td align="right">
                                                   Search User Name:
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtsrchUsername" MaxLength="60" Width="200px" CssClass="SmallTextBox" style="text-align:left;" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUserSearch" CausesValidation="false" runat="server" Text="Search" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                 </td>
                             </tr>
                           </table>
                                <br />
             
                <div style="text-align: center; margin-top: 20px; margin-bottom: 20px">
                    <asp:Label ID="lblmsg" runat="server" Visible="false"></asp:Label>
                </div>
                <table cellspacing="6">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="color: Red; font-weight: bold; padding-left: 15px;">
                                        User Licenses Purchased:
                                    </td>
                                    <td style="color: Black; font-weight: bold;">
                                        <asp:Label ID="lblLicCount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                    <tr>
                        <td>
                            <asp:GridView CssClass="GrdUsers" runat="server" ID="grdUsers" DataKeyNames="UserId"
                                Width="770px" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                                <FooterStyle Font-Bold="True" BackColor="Black" ForeColor="White" />
                              <RowStyle CssClass="AlterNateColor1" />
                                <Columns>
                                    <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" Visible="false">
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblSAUsers" runat="server" Text='Assign Users' ToolTip="Click the check box to assign Contract Packager Knowledgbase privileges to a user"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUser" runat="server" Text='<%# bind("USERID")%>' Style="display: none"></asp:Label>
                                            <asp:CheckBox ID="Add" runat="server" OnClick="return CompareCount(this);"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="25px" Wrap="true" HorizontalAlign="Center" />
                                        <HeaderStyle Width="25px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="lblTransfer" runat="server" Text='Transfer Users' ToolTip="Click the radio button to assign Contract Packager Knowledgbase privileges from one user to another user."></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblUsers" runat="server" Text='<%# bind("USERID")%>' Style="display: none"></asp:Label>
                                            <asp:RadioButton ID="rdTransfer" runat="server" onclick="RadioCheck(this);" />
                                        </ItemTemplate>
                                        <ItemStyle Width="25px" Wrap="true" HorizontalAlign="Center" />
                                        <HeaderStyle Width="25px" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="UserName" HeaderText="User Name" SortExpression="UserName">
                                        <ItemStyle Width="300px" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FIRSTNAME" HeaderText="First Name" SortExpression="FIRSTNAME">
                                        <ItemStyle Width="100px" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="LASTNAME" HeaderText="Last Name" SortExpression="LASTNAME">
                                        <ItemStyle Width="100px" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Company" HeaderText="Company" SortExpression="Company">
                                        <ItemStyle Width="150px" Wrap="true" />
                                    </asp:BoundField>
                                    <%--  <asp:BoundField DataField="ISADMIN" HeaderText="License Admin" SortExpression="ISADMIN">
                                        <ItemStyle Width="50px" Wrap="true" />
                                    </asp:BoundField>--%>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label ID="LicAdm" runat="server" Text="License Admin" SortExpression="ISADMIN"
                                                ToolTip=" The License Administrator is the only user that can assign privileges and transfer privileges"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label1" runat="server" Text='<%# Bind("ISADMIN") %>'> </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25px" Wrap="true" HorizontalAlign="Center" />
                                        <HeaderStyle Width="50px" Wrap="true" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle BackColor="Black" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle Height="25px" Font-Bold="True" BackColor="Black" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                                 <AlternatingRowStyle CssClass="AlterNateColor2" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td style="color: Red; font-weight: bold; padding-left: 20px; padding-top: 25px;
                            padding-bottom: 10px;">
                            <asp:Label ID="msgT" runat="server" ToolTip=" The allowable transfers are automatically reset at the start of each subscription period">
                     One transfer is allowed per License per Year.
                            </asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView CssClass="GrdUsers" runat="server" ID="grdTUser" DataKeyNames="LICENSEID"
                                Width="520px" AllowPaging="True" AllowSorting="false" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="Vertical">
                                <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                               <RowStyle CssClass="AlterNateColor1" />
                                <Columns>
                                    <asp:BoundField DataField="LICENSE" HeaderText="License" SortExpression="LICENSE">
                                        <ItemStyle Width="100px" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName1" HeaderText="Original User" SortExpression="UserName1">
                                        <ItemStyle Width="200px" Wrap="true" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="UserName2" HeaderText="Transferred to User" SortExpression="UserName2">
                                        <ItemStyle Width="200px" Wrap="true" />
                                    </asp:BoundField>
                                </Columns>
                                <PagerStyle BackColor="#32659A" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle Height="25px" Font-Bold="True" BackColor="Black" ForeColor="White" />
                                <EditRowStyle BackColor="#7C6F57" />
                               <AlternatingRowStyle CssClass="AlterNateColor2" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="hvUserGrd" runat="server" />
                <asp:HiddenField ID="hidUserCount" runat="server" />
                <asp:HiddenField ID="hidTotalCount" runat="server" />
                <asp:HiddenField ID="hidLimitCount" runat="server" />
                <asp:HiddenField ID="hidChkCount" runat="server" />
         
        <br />
    </div>
    <asp:Button ID="btnCall" Text="" runat="server" CssClass="Button" Style="margin-left: 5px;
        display: none;" />
    <asp:Button ID="btnRefresh" Text="" runat="server" Style="display: none;" />
</asp:Content>
