<%@ Page Title="S1,S2-Energy Use Wizard" Language="VB" MasterPageFile="~/Masters/Sustain1.master"
    AutoEventWireup="false" CodeFile="SustainWizard.aspx.vb" Inherits="Pages_SustainWizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain1ContentPlaceHolder" runat="Server">
    <script src="../JavaScript/Common.js" type="text/javascript"></script>


    <div id="SContentPagemargin">
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divUpdateprogress">
                            <table>
                                <tr>
                                    <td>
                                        <img alt="" src="../Images/Loading3.gif" height="50px" />
                                    </td>
                                    <td>
                                        <b style="color: Red;">Updating</b>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <table width="100%">
                    <tr align="left">
                        <td style="width: 33%" class="PageHeading" onmouseover="Tip('Sustain Use Wizard')"
                            onmouseout="UnTip()">
                            Sustain Use Wizard
                        </td>
                        <td style="width: 40%" class="PageSHeading">
                            <table>
                                <tr>
                                    <td>
                                        Geographic Region:
                                    </td>
                                    <td>
                                        <asp:DropDownList AutoPostBack="true" ID="ddlGeographic" CssClass="NormalDropDown"
                                            runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 23%" class="PageSHeading">
                            <table>
                                <tr>
                                    <td>
                                        Date:
                                    </td>
                                    <td>
                                        <asp:DropDownList AutoPostBack="true" ID="ddlEffdate" runat="server" CssClass="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <div id="PageSection1">
                    <br />
                    <asp:Table ID="tblWizard" runat="server" Width="98%" CellPadding="0" CellSpacing="2">
                    </asp:Table>
                    <br />
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
