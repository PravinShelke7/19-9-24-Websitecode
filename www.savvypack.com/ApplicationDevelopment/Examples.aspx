<%@ Page Title="Examples" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master"
    AutoEventWireup="false" CodeFile="Examples.aspx.vb" Inherits="ApplicationDevelopment_Examples" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 2px; margin-left: 5px; color: #825f05;">
        Examples
    </div>
    <div style="margin-left: 50px; font-size: 13px; text-align: justify; margin-top: 10px;
        margin-left: 8px;margin-right:7px; color: #666666; color: Black;">
        <span style="font-family: Verdana; color: Black">Allies Development has developed sophisticated
            packaging centric web applications for itself and clients. </span><br /><br/>
    </div>
    <div id="Div1" runat="server" style="vertical-align: top; margin-left: 7px; margin-right: 7px;
       ">
        <table cellspacing="2" >
            <tr>
                <td colspan="2">
                    <div style="font-weight: bold; font-size: 16px; text-align: left; text-decoration: underline;
                        color: Black; font-family: Optima;">
                        Internal Applications
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify;" valign="top" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">SavvyPack Corporation's internal applications are brandnamed
                            SavvyPack® and include world class analytical tools and knowledgebases. </font></font>
                        <asp:LinkButton ID="LinkButton4" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                            ToolTip="Click here to get more info" Style="font-size: 14px; margin-left: 10px;"
                            PostBackUrl="~/ApplicationDevelopment/AppExmple.aspx"></asp:LinkButton>
                    </span><br /><br />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </div>
    <div id="Div2" runat="server" style="vertical-align: top; margin-left: 7px; margin-right: 7px;
        ">
        <table cellspacing="2" ">
            <tr>
                <td colspan="2">
                    <div style="font-weight: bold; font-size: 16px; text-align: left; text-decoration: underline;
                        color: Black; font-family: Optima;">
                        Client Applications
                    </div>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify;" valign="top" colspan="2">
                    <span>
                        <font face="Verdana"><font size="2">The Simplified Environmental Impacts Calculator
                            (SEIC) tool created for Dow Chemical Company is a typical client web application.
                            This tool was developed extensively by SavvyPack Corporation. It includes complex calculations,
                            content served from an Oracle database, and complete graphic development. </font>
                        </font>
                        <asp:LinkButton ID="LinkButton1" runat="Server" CssClass="InteractiveLink" Text="more Info.."
                            ToolTip="Click here to get more info" Style="font-size: 14px; margin-left:2px;"
                            PostBackUrl="~/ApplicationDevelopment/ClientApp.aspx"></asp:LinkButton>
                    </span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
