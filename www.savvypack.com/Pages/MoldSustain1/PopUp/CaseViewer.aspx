<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CaseViewer.aspx.vb" Inherits="Pages_MoldSustain1_PopUp_CaseViewer" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="margin: 0 0 0 0; width: 100%;">
        <div id="PageSection1" style="text-align: left;">
            <div style="margin-left: 10px; width: 600px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <table border="0" id="tblCaseDes" runat="server" cellpadding="0" cellspacing="0">
                    <tr style="height: 20px">
                        <td style="width: 350px; text-align: Left;">
                            <b>Active Case Id:</b><asp:Label ID="lblCaseID" runat="server" CssClass="NormalLable"></asp:Label>
                        </td>
                        <td style="width: 80px; text-align: Left;">
                            <b>Case Type:</b>
                        </td>
                        <td style="width: 50%">
                            <asp:Label ID="lblCaseType" runat="server" CssClass="NormalLable"></asp:Label>
                        </td>
                    </tr>
                    <tr style="height: 20px">
                        <td colspan="2">
                            <span id="caseDe3" runat="server"><b>Case Brief:</b></span>
                            <asp:Label ID="lblCaseDe2" runat="server" CssClass="NormalLable"></asp:Label>
                        </td>
                    </tr>
                </table>
                <table width="90%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size: 14px;">
                            <asp:Label ID="lblCaseDes" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td>
                            <asp:DropDownList ID="ddlCases" AutoPostBack="true" CssClass="DropDown" Width="80%"
                                runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td>
                            <asp:Button ID="btnPrivious" runat="server" CssClass="Button" Text="Previous" Style="margin-left: 0px;" />
                            <asp:Button ID="btnNext" runat="server" Text="Next" CssClass="Button"></asp:Button>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="90%">
                    <tr class="AlterNateColor4">
                        <td style="width: 40%" class="PageSHeading">
                            Case Description
                        </td>
                        <td align="center" class="PageSHeading">
                            Case Characteristics
                            <table width="100%">
                                <tr>
                                    <td class="PageSHeading" style="width: 60%" align="left">
                                        Departments
                                    </td>
                                    <td class="PageSHeading" align="left">
                                        Energy(<asp:Label ID="lblEnergyPref" runat="server"></asp:Label>)
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1" valign="top">
                        <td style="width: 40%" class="PageSHeading">
                            <asp:Label ID="lblCaseDe3" runat="server"></asp:Label>
                        </td>
                        <td>
                            <table width="100%">
                                <tr>
                                    <td align="left" style="width: 60%">
                                        <div id="divDept" runat="server">
                                        </div>
                                    </td>
                                    <td class="PageSHeading" valign="top" align="left">
                                        <asp:Label ID="lblEnergy" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
                <table width="90%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size: 14px;">
                            Case Notes
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView Width="100%" runat="server" ID="grdCaseNotes" AutoGenerateSelectButton="false"
                                AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="true"
                                CellPadding="4" ForeColor="#333333" GridLines="Both">
                                <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="AlterNateColor1" />
                                <AlternatingRowStyle CssClass="AlterNateColor2" />
                                <HeaderStyle CssClass="AlterNateColor3" />
                                <Columns>
                                    <asp:BoundField DataField="Assumptiontypedes" HeaderText="Page Name" SortExpression="Assumptiontypedes"
                                        Visible="true">
                                        <ItemStyle Width="30%" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Note" HeaderText="Notes" SortExpression="Note" Visible="true">
                                        <ItemStyle HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:BoundField>
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trNotes" runat="server" visible="false" class="AlterNateColor1">
                        <td>
                            No Notes.
                        </td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
