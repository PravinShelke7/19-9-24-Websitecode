<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetGradeDetails.aspx.vb"
    Inherits="Pages_MoldEcon1_PopUp_GradeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E1 Mold-Get Grades</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function GradeDetails(GradeDes, GradeId, Weight, SG) {
            var hidGradedes = document.getElementById('<%= hidGradedes.ClientID%>').value
            var hidGradeId = document.getElementById('<%= hidGradeId.ClientID%>').value

            var hidSGV = document.getElementById('<%= hidSG.ClientID%>').value;

            // alert(window.opener.document.getElementById(hidSGV).value + '@@@' + Weight + '###' + SG);
            if (Weight != SG) {
                window.opener.document.getElementById(hidSGV).value = Weight;
            }
            else {
                window.opener.document.getElementById(hidSGV).value = "0.000";
            }
            //alert(MatDes.length);
            window.opener.document.getElementById(hidGradedes).innerText = GradeDes
            window.opener.document.getElementById(hidGradeId).value = GradeId
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Grade De1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGradeDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Grade De2:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtGradeDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
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
            <table style="width: 400px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td>
                        Grade Des1
                    </td>
                    <td>
                        Grade Des2
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 230px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdGrade" DataKeyNames="GRADEID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="GRADEID" HeaderText="GRADEID" SortExpression="GRADEID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="GRADE NAME" SortExpression="GRADENAME">
                            <ItemTemplate>
                                <a href="#" onclick="GradeDetails('<%#Container.DataItem("GRADENAME")%>','<%#Container.DataItem("GRADEID")%>','<%#Container.DataItem("WEIGHT")%>','<%#Container.DataItem("SG")%>')"
                                    class="Link">
                                    <%#Container.DataItem("GRADEID")%>:<%#Container.DataItem("GRADENAME")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="50%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidGradedes" runat="server" />
        <asp:HiddenField ID="hidGradeId" runat="server" />
        <asp:HiddenField ID="hidSG" runat="server" />
    </div>
    </form>
</body>
</html>
