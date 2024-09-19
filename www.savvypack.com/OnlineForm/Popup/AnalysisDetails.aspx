<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnalysisDetails.aspx.vb"
    Inherits="SavvyPack_Popup_TypeDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Analysis Type</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        function UserSearch(UserDes, UserId) {

            var hidCaseid = document.getElementById('hdnUserId').value;
            var hidCaseLinkid = document.getElementById('<%= hdnUserDes.ClientID%>').value;
            var hidAnalysisDes = document.getElementById('hdnUserDes1').value;

            UserDes = UserDes.replace(new RegExp("&#", 'g'), "'");

            window.opener.document.getElementById(hidCaseLinkid).innerText = UserDes;
            window.opener.document.getElementById(hidCaseid).value = UserId;
            window.opener.document.getElementById(hidAnalysisDes).value = UserDes;
            window.close();
        }        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <div id="ContentPage" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; margin: 0 0 0 0">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <br />
            <div class="divMainHeading" style="font-size: 17px; font-weight: bold; text-align: center;
                margin-bottom: 5px; width: 100%; height: 20px;">
                Analysis Type
            </div>
            <div style="height: 320px; overflow: auto;">
                <asp:GridView Width="550px" runat="server" ID="grdUserDetails" DataKeyNames="ANALYSISID"
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
                                <a href="#" onclick="UserSearch('<%#Container.DataItem("TYPE")%>','<%#Container.DataItem("ANALYSISID")%>')"
                                    class="Link">
                                    <%# Container.DataItem("TYPE")%></a>
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
        <asp:HiddenField ID="hdnUserDes1" runat="server" />
    </div>
    </form>
</body>
</html>
