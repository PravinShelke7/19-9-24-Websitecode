<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetAllGroups.aspx.vb" Inherits="Pages_Market1Sub_PopUp_GetAllGroups" %>

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
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">
        function CaseSearch(groupName, groupId, des1) {

            var hidgrpdes = document.getElementById('<%= hidgrpDes.ClientID%>').value
            var hidgrpid = document.getElementById('<%= hidgrpId.ClientID%>').value
            var hidGrpIdD = document.getElementById('<%= hidGrpIdD.ClientID%>').value;
            //alert(hidgrpid);
            //alert(window.opener.document.getElementById(hidgrpid).value);
            if (groupId == "0") {
                window.opener.document.getElementById(hidgrpdes).innerText = groupName;
                window.opener.document.getElementById(hidgrpid).value = groupId;
                //window.opener.document.getElementById(hidGrpIdD).value = groupId + ":" + groupName;
                window.opener.document.getElementById(hidGrpIdD).value = groupName;
            }
            else {
                window.opener.document.getElementById(hidgrpdes).innerText = groupId + ":" + groupName;
                window.opener.document.getElementById(hidgrpid).value = groupId;
                window.opener.document.getElementById(hidGrpIdD).value = groupId + ":" + groupName;
            }



            window.opener.document.getElementById("hidPropRpt").value = "0";
            window.opener.document.getElementById("hidPropRptDes").value = "Display Proprietary Report List";
            window.opener.document.getElementById("lnkReports").innerText = "Display Proprietary Report List";
            window.close();

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
<body>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <div class="divHeader" style="font-size: 17px; font-weight: bold; text-align: center;
                margin-bottom: 10px; margin-top: 10px; width: 700px; height: 30px;">
                Group Details
            </div>
            <br />
            <br />
            <br />
            <div style="width: 60%; margin-left: 25px;">
                <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <b>Search:</b>
                        </td>
                        <td>
                            <asp:TextBox ID="txtDes1" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                width: 200px"></asp:TextBox>
                            &nbsp;<asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
            </div>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblGGroup" runat="server" CssClass="CalculatedFeilds" Visible="true"
                            Style="font-size: 16px; color: Red; margin-left: 10px;" Text="Currently you have no Groups defined. You can create a Group through Manage Groups."></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="container" style="width: 720px; height: 390px; overflow: auto; margin-left: 10px;">
                <asp:GridView Width="700px" runat="server" ID="grdGroupSearch" DataKeyNames="GROUPID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" ForeColor="White" />
                    <Columns>
                        <asp:BoundField DataField="GROUPID" HeaderText="GROUPID" SortExpression="GROUPID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Descriptor1" SortExpression="GROUPNAME">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("GROUPNAME")%>','<%#Container.DataItem("GROUPID")%>','<%#Container.DataItem("DES1")%>')"
                                    class="Link">
                                    <%# Container.DataItem("GROUPID")%>:<%# Container.DataItem("DES1")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="350px" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DES2" HeaderText="Descriptor2" SortExpression="DES2" Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" Width="350px" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidgrpId" runat="server" />
        <asp:HiddenField ID="hidgrpDes" runat="server" />
        <asp:HiddenField ID="hidGrpIdD" runat="server" />
        <asp:HiddenField ID="hvUserGrd" runat="server" />
    </div>
    </form>
</body>
</html>
