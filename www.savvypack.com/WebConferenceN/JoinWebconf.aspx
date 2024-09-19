<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="JoinWebconf.aspx.vb" Inherits="WebConferenceN_JoinWebconf" Title="Join Web Conference" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="background-color: #000000; color: White; height: 22px; width: 97.5%;
        font-weight: bold; font-size: 14px; text-align: center; margin-top: 3px; margin-left: 5px;
        font-family: Verdana">
        Allied Development Web Conferencing
    </div>
    <div id="ContentPagemargin" runat="server">
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table cellspacing="2" style="vertical-align: middle; width: 100%">
            <tr>
                <td style="height: 5px;">
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold; color: Black; font-family: Verdana; font-size: 9.6pt">
                    <div style="margin-left: 7px; margin-right: 7px;">
                        Allied Development utilizes several web conferencing options. Please select the
                        web conferencing option for your web conference from the list below. If needed,
                        please refer to the instructions provided by your Allied Development representative
                        to determine which option to select, or call us at [1] 952-898-2000.
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 20px;">
                </td>
            </tr>
             <tr>
                <td align="left" style="width:100%">
                    <table width="100%">
                    
                  
            <tr class="AlterNateColor1">
                <td style="width: 100%;" valign="middle" align="left">
                    <a href="https://allieddevelopmentcorp.webex.com" class="LinkStudy" style="text-decoration: none;
                        margin-left: 230px;">Web Conferencing by Cisco / WebEx </a>
                </td>
            </tr>
            <tr class="AlterNateColor2">
                <td style="width: 100%;" valign="middle" align="Left">
                    <a href="https://allieddevelopmentcorp1.webex.com" class="LinkStudy" style="text-decoration: none;
                        margin-left: 230px;">Web Conferencing by Cisco / WebEx (1) </a>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td style="width: 100%;" valign="middle" align="Left">
                    <a href="https://allieddevelopmentcorp2.webex.com" class="LinkStudy" style="text-decoration: none;
                        margin-left: 230px;">Web Conferencing by Cisco / WebEx (2) </a>
                </td>
            </tr>
          
              </table>
                </td>
            </tr> 
            <tr>
                <td style="height: 90px;">
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
