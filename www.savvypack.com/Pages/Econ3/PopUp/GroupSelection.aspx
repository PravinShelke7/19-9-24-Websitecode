<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupSelection.aspx.vb" Inherits="Pages_Econ3_PopUp_GroupSelection" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Group Selector</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function toggleCheckBoxes(elem) {
            var div = document.getElementById('PageSection12');
            var chk = div.getElementsByTagName('input');
            var len = chk.length;

            for (var i = 0; i < len; i++) {
                if (chk[i].type === 'checkbox') {
                    chk[i].checked = elem.checked;
                }
            }
        }

        function CheckAlldelete(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var spanList = GridView.getElementsByClassName("SelectAll");
            for (var i = 0; i < spanList.length; i++) {
                var input = spanList[i].childNodes[0];
                if (objRef.checked && !input.disabled) {
                    input.checked = true;
                }
                else {
                    input.checked = false;
                }
            }
        }

        function CloseAndUpdate()
        {
            var grpID = document.getElementById("hidCaseid").value            
            window.opener.document.getElementById('ctl00_Econ3ContentPlaceHolder_hidselgrpID').value = grpID;
            window.opener.document.getElementById('ctl00_Econ3ContentPlaceHolder_btnRefresh').click();
            window.close();
        }
    </script>
</head>
<body style="background-color: #D3E7CB;">
    <form id="form1" runat="server" style="width: 100%; margin: 0 0 0 0">
        <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
            <div id="PageSection12" style="width: 100%; text-align: left;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <br />
                <div style="width: 100%; font-size: 16px; font-weight: bold; text-align: center;">
                    Group Selector
                </div>
                <div style="width: 100%; text-align: center;">
                    <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" Style="text-align: cente; font-size: 16px; font-weight: bold; color: Red;"> </asp:Label></div>
                <div style="width: 960px; overflow: auto; margin-left:10px;">
                    <asp:GridView CssClass="GrdUsers" runat="server" ID="grdGroup" Width="940px" DataKeyNames="GROUPID"
                        AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" CellPadding="4" Font-Size="11px"
                        ForeColor="#333333" GridLines="None" CellSpacing="1">
                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <RowStyle CssClass="AlterNateColor1" />
                        <AlternatingRowStyle CssClass="AlterNateColor2" />
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="HeaderLevelCheckBox" onclick="javascript: toggleCheckBoxes(this);" runat="server"
                                        type="checkbox" />
                                </HeaderTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="SelForGrp" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="GROUPID" HeaderText="GROUPID" Visible="false"></asp:BoundField>
                            <asp:BoundField DataField="GROUPNAME" HeaderText="Group Name" SortExpression="GROUPNAME">
                                <ItemStyle Width="150px" Wrap="true" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="GROUPDES" HeaderText="Group Description" SortExpression="GROUPDES">
                                <ItemStyle Width="200px" Wrap="true" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CASEID" HeaderText="Case(s)" SortExpression="CASEID">
                                <ItemStyle Width="200px" Wrap="true" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="CASEID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                SortExpression="CASEID">
                                <ItemTemplate>
                                    <asp:Label ID="lblCaseID" runat="server" Text='<%# bind("CASEID")%>'></asp:Label>
                                    <asp:Label ID="lblGroupID" runat="server" Text='<%# bind("GROUPID")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="UserName" HeaderText="Group Owner" SortExpression="UserName">
                                <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CREATIONDATE" HeaderText="Creation Date" SortExpression="CREATIONDATE">
                                <ItemStyle Width="80px" Wrap="true" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                            <asp:BoundField DataField="UPDATEDATE" HeaderText="Last Update" SortExpression="UPDATEDATE">
                                <ItemStyle Width="80px" Wrap="true" />
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:BoundField>
                        </Columns>
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />


                    </asp:GridView>
                    <asp:HiddenField ID="hvUserGrd" runat="server" />
                </div>
                <br />
                <div style="margin-left:10px;">
                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
                </div>                
            </div>
            <asp:HiddenField ID="hidCaseid" runat="server" />
            <asp:HiddenField ID="hidCaseDes" runat="server" />
        </div>
    </form>
</body>
</html>
