<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditGroup.aspx.vb" Inherits="Pages_Market1Sub_PopUp_EditGroup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group Details</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("98");
                        });
                    }
                }
            }
        });               
    </script>
    <script language="JavaScript" type="text/javascript">
        var isClose = false;
        document.onkeydown = checkKeycode
        function checkKeycode(e) {
            var keycode;
            if (window.event)
                keycode = window.event.keyCode;
            else if (e)
                keycode = e.which;
            if (keycode == 116) {
                isClose = true;
            }
        }
        function mousefunction() {
            isClose = true;
        }
        function doUnload() {
            if (!isClose) {

                if (document.getElementById("hdnUpdate").value == "1") {
                    window.opener.document.getElementById('btnRefresh').click();
                }

            }
        }
    </script>
    <script type="text/JavaScript">
        function GroupSearch() {
            if (document.getElementById("hdnUpdate").value == "1") {
                window.opener.document.getElementById('btnRefresh').click();
            }
            window.close();
        }
        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 98; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }

        function Delete() {
            var count = 0;
            var table = document.getElementById('<%=grdGrpReports.ClientID %>');
            // alert(table);
            var grps = "";
            for (var i = 1; i < table.rows.length; i++) {
                var Row = table.rows[i];
                var CellValue = Row.cells[0];
                for (j = 0; j < CellValue.childNodes.length; j++) {
                    if (CellValue.childNodes[j].type == "checkbox") {
                        if (CellValue.childNodes[j].checked == true) {
                            count = count + 1;
                            //alert(Row.cells[1].children[0].value);
                            if (grps != "") { grps = grps + "\n "; }
                            grps = grps + Row.cells[1].children[0].value + " " + Row.cells[2].children[0].value;

                        }
                    }
                }
            }

            if (count == 0) {
                alert("Please select atleast one group to Delete.");
                return false;
            }
            else {
                if (confirm("Do you want to delete following group(s) ?\n----------------------------------------------\n " + grps + "\n---------------------------------------------- ")) {
                    return true;
                }
                else {
                    return false;
                }

            }
        }
         
    </script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body onbeforeunload="doUnload()" onmousedown="mousefunction()">
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; width: 670px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <%--   <div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:650px;height:30px;">
                      Group Details
</div>--%>
            <table style="text-align: center;">
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="PageSHeading" style="font-size: 16px; width: 708px; text-align: center;
                        margin-left: 100px;">
                        Edit Group Details
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblmsg" runat="server" Visible="False" Width="708px" Style="text-align: cente;
                            font-size: 16px; font-weight: bold; color: Red;"></asp:Label>
                    </td>
                </tr>
            </table>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <table cellpadding="2" cellspacing="2">
                <tr>
                    <td>
                        <div style="width: 661px; height: 350px; overflow: auto;">
                            <asp:GridView Width="640px" runat="server" ID="grdGrpReports" DataKeyNames="GROUPID"
                                AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None">
                                <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="AlterNateColor1" />
                                <AlternatingRowStyle CssClass="AlterNateColor2" />
                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" Font-Size="12px"
                                    ForeColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="GROUPID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                        SortExpression="GROUPID">
                                        <ItemTemplate>
                                            <asp:Label ID="lblGroupID" runat="server" Text='<%# bind("GROUPID")%>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <%-- <HeaderTemplate>
                                    <input id="HeaderLevelCheckBox" onclick="javascript:SelectAllCheckboxes(this);" 
                                        runat="server" type="checkbox" name="delete" />
                                </HeaderTemplate>--%>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="delete" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Descriptor1" HeaderStyle-HorizontalAlign="center"
                                        SortExpression="GROUPNAME" Visible="true" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGroupName" CssClass="SmallTextBox" MaxLength="25" Style="text-align: left;"
                                                Font-Size="8" Width="200px" Enabled="true" runat="server" Text='<%# bind("GROUPNAME")%>'
                                                AutoPostBack="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="210px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Group Descriptor2" HeaderStyle-HorizontalAlign="center"
                                        SortExpression="GROUPDES" Visible="true" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGroupDes" CssClass="SmallTextBox" MaxLength="25" Style="text-align: left;"
                                                Font-Size="8" Width="200px" Enabled="true" runat="server" Text='<%# bind("GROUPDES")%>'
                                                AutoPostBack="false" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Width="400px" Wrap="true" HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 100px;">
                                    <asp:Button ID="btnUpdate" Height="25px" Width="90px" runat="server" Text="Update"
                                        CssClass="ButtonWMarigin" />
                                </td>
                                <td style="width: 100px">
                                    <asp:Button ID="btnDelete" Height="25px" Width="90px" runat="server" Text="Delete"
                                        CssClass="ButtonWMarigin" OnClientClick="return Delete();" />
                                </td>
                                <td style="width: 100px">
                                    <asp:Button ID="btnClose" Height="25px" Width="90px" runat="server" Text="Close"
                                        CssClass="ButtonWMarigin" />
                                </td>
                                <td style="width: 100px;">
                                    <asp:Button ID="btnClose2" Height="25px" Width="90px" runat="server" Text="Close"
                                        CssClass="ButtonWMarigin" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <asp:HiddenField ID="hidReportId" runat="server" />
        <asp:HiddenField ID="hidReportDes" runat="server" />
        <asp:HiddenField ID="hidReportDes1" runat="server" />
        <asp:HiddenField ID="hvReportGrd" runat="server" />
        <asp:HiddenField ID="hdnUpdate" runat="server" />
    </div>
    </form>
</body>
</html>
