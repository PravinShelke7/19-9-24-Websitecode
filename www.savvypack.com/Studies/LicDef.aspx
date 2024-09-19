<%@ Page Title="License Definitions" Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master"
    AutoEventWireup="false" CodeFile="LicDef.aspx.vb" Inherits="Studies_LicDef" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table cellspacing="7" style="vertical-align: middle; width: 99%">
        <tr>
            <td>
                <asp:Label ID="lblheader" Text="License Definitions" runat="Server" Style="font-size: 18px;
                    color: #825f05; font-weight: bold; width: 100%"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px; color: Black">
                <b>Single User License:</b>
                <br />
                The Single Use License is for one employee of the purchasing company and doesn’t
                allow any copying, emailing or re-distribution.
                <br />
                <br />
                <b>Site License:</b>
                <br />
                The Site License is for employees (only) of the purchasing company at one physical
                location and allows copying, emailing, and
                <br />
                re-distribution among these employees.
                <br />
                <br />
                <b>Corporate License:</b>
                <br />
                The Corporate License is for employees (only) of the purchasing company and its
                100% owned subsidiaries and allows copying, emailing, and re-distribution among
                these employees.
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hdnUserLoginPath" runat="server" />
</asp:Content>
