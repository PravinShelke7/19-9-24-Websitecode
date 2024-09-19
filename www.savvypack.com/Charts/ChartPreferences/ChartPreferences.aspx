<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false"
    CodeFile="ChartPreferences.aspx.vb" Inherits="Charts_ChartPreferences_Default"
    Title="Chart Preferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="PageSection1" style="text-align: left">
        <br />
        <div id="header" class="PageHeading" style="font-family: Optima">
            Chart Preferences
        </div>
        <table class="SubHeadding" border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td style="height: 10px;">
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 14px; font-family: Optima; font-weight: bold; margin-left: 10px">
                        UserName:</span>
                    <asp:Label ID="lblUser" CssClass="NormalLabel" Style="color: Red; font-weight: bold"
                        runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <div>
            <table cellspacing="0" cellpadding="5" border="0" style="margin-left: 10px;">
                <tr style="height: 20px;">
                    <td align="right" style="font-size: 12px; font-family: Optima; font-weight: bold;
                        width: 40%">
                        Preferred Units:
                    </td>
                    <td style="width: 60%" align="left">
                        <asp:RadioButton ID="rdEngUni" runat="server" GroupName="Units" Text="English units"
                            Checked="True" />
                        <asp:RadioButton ID="rdMetUni" runat="server" GroupName="Units" Text="Metric units" />
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td align="right" style="font-size: 12px; font-family: Optima; font-weight: bold;">
                        Preferred Currency:
                    </td>
                    <td colspan="2" align="left">
                        <asp:DropDownList ID="ddlCurrecnyType" runat="server" CssClass="DropDownCon" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td align="right" style="font-size: 12px; font-family: Optima; font-weight: bold;">
                        Exchange Rate Date:
                    </td>
                    <td colspan="2" align="left">
                        <asp:DropDownList ID="ddlCurrEffdate" runat="server" CssClass="DropDownCon" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr style="height: 20px;">
                    <td>
                    </td>
                    <td colspan="2" align="left">
                        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
