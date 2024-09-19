<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SponsorDetails.aspx.vb"
    Inherits="Pages_StandAssist_PopUp_SponsorDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SA-User Details</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function SponsorSearch(name, SponsorId) {
            // alert(Username+' '+UserId);
            var hidSponsorDes = document.getElementById('<%= hidSponsorDes.ClientID%>').value
            var hidSponsorId = document.getElementById('<%= hidSponsorId.ClientID%>').value

            window.opener.document.getElementById(hidSponsorDes).innerText = name;
            window.opener.document.getElementById(hidSponsorId).value = SponsorId;

            window.close();
        }

        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;


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
            <table cellpadding="0" cellspacing="2" style="Height:100px;">
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td align="right">
                                    <b>Search:</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SmallTextBox" Style="text-align: left;Width:200px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnSponsor" Text="Add New Sponsor" runat="server" CssClass="Button"
                                        Style="margin-left: 0px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td>
                        <div id="divSponsor" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td align="right">
                                        <b>Sponsor Name:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtName" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <b>EmailAddress:</b>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtEmail" runat="server" CssClass="SmallTextBox" Width="200px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnSubmits" Text="Submit" runat="server" CssClass="Button" Width="65px"
                                            Style="margin-left: 0px" />&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                                            <asp:Button ID="btnCancel" Text="Cancel" runat="server" CssClass="Button" Width="65px"
                                            Style="margin-left: 0px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <table cellspacing="0">
                            <tr>
                                <td>
                                    <asp:LinkButton CssClass="Link" CausesValidation="false" ID="LinkButton1" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <div style="width: 600px; height: 280px; overflow: auto;">
                <asp:GridView Width="550px" runat="server" ID="grdSponsor" DataKeyNames="SUPPLIERID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="SUPPLIERID" HeaderText="SUPPLIERID" SortExpression="SUPPLIERID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Name" SortExpression="NAME">
                            <ItemTemplate>
                                <a href="#" onclick="SponsorSearch('<%#Container.DataItem("NAME")%>','<%#Container.DataItem("SUPPLIERID")%>')"
                                    class="Link">
                                    <%# Container.DataItem("NAME")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                       
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidSponsorId" runat="server" />
        <asp:HiddenField ID="hidSponsorDes" runat="server" />
    </div>
    </form>
</body>
</html>
