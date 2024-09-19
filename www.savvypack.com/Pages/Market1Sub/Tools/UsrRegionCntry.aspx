<%@ Page Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="UsrRegionCntry.aspx.vb" Inherits="Pages_Market1_Tools_UsrRegionCntry" title="Market1-User Region Countries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

<div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
            <br />
                <table style="width:820px;">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" colspan="2" style="padding-left:5px;">
                            <b>Region Details</b>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td style="width:80px;padding-left:5px;">
                            <b>Region Name:</b>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtRegionName" runat="server" CssClass="SearchTextBox" Width="250px" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Panel ID="pnl" GroupingText="Region Countries" runat="server" Width="830px">
                                <div style="overflow:auto;height:400px;overflow-x: hidden">
                                <asp:CheckBoxList ID="chkListCntries" runat="server" RepeatColumns="4"  
                                    RepeatDirection="Horizontal"></asp:CheckBoxList>
                                </div>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
            
            <br />
       </div>
</div>
</asp:Content>
