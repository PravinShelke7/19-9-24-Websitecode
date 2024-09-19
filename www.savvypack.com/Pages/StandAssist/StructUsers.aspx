<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StructUsers.aspx.vb" Title="SA -User Management" Inherits="Dashboard_StructUsers" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .SBAModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../Images/SavvyPackStructureAssistantR01.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function clickButton(e, buttonid) {
            debugger;
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        }

        function CompareCount(chkbox) {
            debugger;
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
                                if (confirm('You are going to assign Structure Assistant access to user ' + userName + ' . Do you want to proceed?')) {
                                    document.getElementById('<%= btnCall.ClientID%>').click();
                                    return true;
                                }
                                else {
                                    return false;
                                }

                            }
                            else {
                                alert('You can not assign Structure Assistant access to user ' + userName + ' because you have reached your maximum license limit of ' + totalCount + '.');
                                return false;
                            }
                        }

                    }
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
            //alert(table.rows.length);
            for (var i = 1; i < table.rows.length; i++) {
                var Row = table.rows[i];
                var CellValue = Row.cells[1];
                for (j = 0; j < CellValue.childNodes.length; j++) {
                    if (CellValue.childNodes[j].type == "radio") {
                       // alert(CellValue.childNodes[j].checked);
                        if (CellValue.childNodes[j].checked == true) {
                            //alert(Row.cells[2].innerText);
                            var page = "Popup/transferSLicense.aspx?usr=" + Row.cells[2].innerText + "&Type=SA";
                            showConfirmationWindow(page);
                            return false;
                        }
                        else {
                            //  alert('Sud2');
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

        function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
//                    return false;
                }
            }
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
</head>
<body>
    <form id="form1" runat="server">
    <script type="text/javascript" src="../../JavaScripts/wz_tooltip.js"></script>
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <center>
    
                 
        <div id="MasterContent">
            <div>
                <table class="SBAModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px; padding-bottom: 15px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                            runat="server" ToolTip="Update" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif"
                                            runat="server" ToolTip="Log Off" Visible="false" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=MContract" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                            runat="server" ToolTip="Instructions" Visible="false" OnClientClick="return Help();" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgChart" ImageAlign="Middle" ImageUrl="~/Images/ChartN.gif"
                                            runat="server" ToolTip="Charts" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgFeedback" ImageAlign="Middle" ImageUrl="~/Images/FeedbackN.gif"
                                            runat="server" ToolTip="Feedback" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
                                            runat="server" ToolTip="Notes" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
             <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
              <table class="ContentPage" id="ContentPage" runat="server">
                <tr>
                    <td>
                          <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <div>
                            
                                <div class="AlterNateColor4">
                                <div class="PageSHeading" style="font-size:16px;">
                                  Manage Structure Assistant Users
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
                                                    <asp:TextBox CssClass="SmallTextBox" ID="txtsrchUsername" MaxLength="60" Width="200px" style="text-align:left;" runat="server"></asp:TextBox>
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
                                                <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White"  />
                                                <RowStyle CssClass="AlterNateColor1" />
                                                <Columns>
                                                    <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" Visible="false">
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <HeaderTemplate>
                                                            <asp:Label ID="lblSAUsers" runat="server" Text='Assign Users' ToolTip="Check the box to assign privileges to Structure Assistant to a user"></asp:Label>
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
                                                            <asp:Label ID="lblTransfer" runat="server" Text='Transfer Users' ToolTip="Check the box to transfer privileges to Structure Assistant to a user"></asp:Label>
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
                                                <HeaderStyle Height="25px" Font-Bold="True" BackColor="Black" ForeColor="White"/>
                                                <EditRowStyle BackColor="#7C6F57" />
                                               <AlternatingRowStyle CssClass="AlterNateColor2" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                          
                       </div>
                       </div>
                       </div>
                    </td>
                </tr>
                <tr class="AlterNateColor3">
                    <td class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>
     
   
                    
    </center>
    <asp:HiddenField ID="hvUserGrd" runat="server" />
    <asp:HiddenField ID="hidUserCount" runat="server" />
    <asp:HiddenField ID="hidTotalCount" runat="server" />
    <asp:HiddenField ID="hidLimitCount" runat="server" />
    <asp:HiddenField ID="hidChkCount" runat="server" />
    <asp:Button ID="btnCall" Text="" runat="server" CssClass="Button" Style="margin-left: 5px;
        display: none;" />
    <asp:Button ID="btnRefresh" Text="" runat="server" Style="display: none;" />
    </form>
</body>
</html>
