<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TypeDetails.aspx.vb" Inherits="SavvyPack_Popup_TypeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Analysis Type</title> 
        <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UserSearch(UserDes, UserId) {
        
            var hidCaseid = document.getElementById('hdnUserId').value;
            var hidCaseLinkid = document.getElementById('<%= hdnUserDes.ClientID%>').value;

            var str = UserDes.replace(/##/g, "'");
            str = str.replace(/$#/g, '"');
            window.opener.document.getElementById(hidCaseLinkid).innerText = UserDes;
            PageMethods.UpdateType(UserId, hidCaseid, onSucceedAll, onErrorAll);
        }

        function onSucceedAll(result) {

            if (result) {
                window.close();
            }

        }

        function onErrorAll(result) {
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <table class="ContentPage" id="Table1" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" style="width: 280px; text-align: center">
                    <asp:Label ID="Label1" Text="Analysis Type" runat="server"></asp:Label>
                </div>
                <div id="error">
                    <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
                </div>
            </td>
        </tr>
        <tr style="height: 20px;">
            <td>
                <div id="ContentPage" style="width: 100%; margin: 0 0 0 0">
                    <div id="PageSection1" style="text-align: left; margin: 0 0 0 0">
                        <div style="height: 180px; overflow: auto;">
                            <asp:GridView Width="270px" runat="server" ID="grdUserDetails" DataKeyNames="ANALYSISID"
                                AutoGenerateSelectButton="false" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="False"
                                CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="12px" Font-Names="verdana">
                                <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                                <RowStyle CssClass="AlterNateColor1" />
                                <AlternatingRowStyle CssClass="AlterNateColor2" />
                                <HeaderStyle CssClass="AlterNateColor4" />
                                <Columns>
                                    <asp:BoundField DataField="ANALYSISID" HeaderText="ANALYSISID" SortExpression="ANALYSISID"
                                        Visible="false"></asp:BoundField>
                                    <asp:TemplateField HeaderText="TYPE NAME" SortExpression="TYPE">
                                        <ItemTemplate>
                                            <a href="#" id="lnkType" onclick="UserSearch('<%#Container.DataItem("TYPE")%>','<%#Container.DataItem("ANALYSISID")%>')"
                                                class="Link">
                                                <%# Container.DataItem("TYPE")%></a>                                                
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TYPE NAME" SortExpression="TYPE" Visible="false" >
                                        <ItemTemplate>
                                            <asp:Label ID="lblType" Width="200px" runat="server" Text='<%# bind("TYPE")%>'></asp:Label>
                                            <asp:Label ID="lblTypeID" Width="200px" runat="server" Text='<%# bind("ANALYSISID")%>' Visible="false" ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <PagerStyle CssClass="AlterNateColor4" ForeColor="White" HorizontalAlign="Center" />
                            </asp:GridView>
                        </div>
                    </div>
                    <asp:HiddenField ID="hdnUserId" runat="server" />
                    <asp:HiddenField ID="hdnUserDes" runat="server" />
                    <asp:HiddenField ID="hdnAnalysisID" runat="server" />
                </div>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
