<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Masters/SavvyPackMenu.master"
    CodeFile="PkgMrktDataBase.aspx.vb" Inherits="SavvyPack_PkgMrktDataBase" Title="Packaging Market Database" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 4px; margin-left: 5px; color: #825f05;">
        Packaging Market Database
    </div>
    <div id="ContentPagemargin" runat="server">
        <div id="error" style="height: 10px;">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
         <div style="font-size: 20px; color: Red; text-align: center; height: 100px; margin-top: 15px;">
            COMING SOON
        </div>
        <table cellspacing="7">
          <%--  <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span><font face="Verdana"><font size="2">The contract packager knowledgebase is the
                        only global database of contract packagers that includes actionable intelligence.
                    </font></font></span>
                </td>
            </tr>
            <tr>
                <td style="font-weight: 500; color: Black; font-family: Optima; font-size: 16px;
                    text-align: justify" colspan="2">
                    <span><font face="Verdana"><font size="2">Many of the companies in this database have
                        low profiles and can be difficult to locate even though they are key cogs in the
                        supply chain. This knowledgebase is a vital tool for locating a particular food
                        product manufacturer or a contract packager who fits your specific requirements.
                        This knowledgebase provides searchable information on companies that provide contract
                        manufacturing and/or contract packaging of food. </font></font></span>
                </td>
            </tr>
            <tr style="height: 5px;">
                <td style="width: 60%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                    margin-top: 0px; text-align: justify;" valign="top" colspan="2">
                    <span><font face="Verdana"><font size="2">The contract packager knowledgebase isn't
                        just a list of names. It is an interactive tool that makes it possible for you to
                        search for a contract packager by : </font></font></span>
                    <ul>
                        <li><font face="Verdana"><font size="2">Product capabilities.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Process equipment.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Development capabilities.</font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Quality control.</font></font> </li>
                        <li><font face="Verdana"><font size="2">and many others.</font></font> </li>
                    </ul>
                </td>
            </tr>
            <tr>
                <td style="width: 126%; font-weight: 500; color: Black; font-family: Verdana; font-size: 14px;
                    margin-top: 0px; text-align: justify;" valign="top">
                    <br />
                    <span><font face="Verdana"><font size="2">Who benefits from this knowledgebase: </font>
                    </font></span>
                    <ul>
                        <li><font face="Verdana"><font size="2">Procurement Personnel.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Packaging and Processing Equipment Manufacturers.</font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Food Manufacturers.</font></font> </li>
                        <li><font face="Verdana"><font size="2">Organic and Natural Food Marketers.</font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Supply Chain Participants. </font></font>
                        </li>
                        <li><font face="Verdana"><font size="2">Packaging Suppliers. </font></font></li>
                        <li><font face="Verdana"><font size="2">Analysts, Investors and Consultants. </font>
                        </font></li>
                    </ul>
                </td>
            </tr>--%>
        </table>
    </div>
    <div id="dvCompanyInfo" runat="server" style="vertical-align: top; margin-left: 7px;
        text-align: left; margin-right: 7px;">
        <table cellspacing="2">
            <tr>
                <td>
                    <span style="font-weight: bold; color: #825f05; font-family: Verdana; font-size: 14px;">
                        Questions?</span>
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        Call us at [1] 952-405-7500 or</span>
                    <br />
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        email us at <a style="text-decoration: none; font-style: italic; font-weight: bold"
                            class="Link" href="mailto:sales@savvypack.com">sales@savvypack.com</a></span>
                </td>
            </tr>
            <tr>
                <td style="height: 10px">
                </td>
            </tr>
            <%-- <tr>
                <td>
                
                <asp:Image ID="imgBack" runat="Server" ImageUrl="~/Images/ArrowRight.jpg"/>
                <asp:LinkButton ID="lnkBack" runat="Server" Text="Back to Interactive List" CssClass="InteractiveLink" style="font-size:14px" PostBackUrl="~/InteractiveServices/InteractiveServices.aspx"></asp:LinkButton>
                
                
                </td>
              </tr>--%>
        </table>
        <asp:HiddenField ID="hdnUserId" runat="Server" />
        <asp:HiddenField ID="hdnRepId" runat="Server" />
    </div>
</asp:Content>
