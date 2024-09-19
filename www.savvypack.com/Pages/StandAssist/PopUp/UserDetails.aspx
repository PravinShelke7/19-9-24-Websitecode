<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserDetails.aspx.vb" Inherits="Pages_StandAssist_PopUp_UserDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SA-User Details</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function CaseSearch(Username, UserId) {
            //alert(Username+' '+UserId);
            var hidUserDes = document.getElementById('<%= hidUserDes.ClientID%>').value
            var hidUserId = document.getElementById('<%= hidUserId.ClientID%>').value
            var hidUsernameD = document.getElementById('<%= hidUsernameD.ClientID%>').value;
            //alert(hidUserId);
            //alert(hidUsernameD);
            //alert(window.opener.document.getElementById(hidgrpid).value);
            window.opener.document.getElementById(hidUserDes).innerText = Username;
            window.opener.document.getElementById(hidUserId).value = UserId;
            window.opener.document.getElementById(hidUsernameD).value = Username;
            
            document.getElementById('<%= hidUser.ClientID%>').value = Username;
                        window.close();
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
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>            
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="width: 500px; height:550px; overflow: auto;">
                <asp:GridView Width="450px" runat="server" ID="grdUserSearch" DataKeyNames="UserId"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="USERID" HeaderText="USERID" SortExpression="USERID" Visible="false">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="User Name" SortExpression="USERNAME">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("USERNAME")%>','<%#Container.DataItem("USERID")%>')"  class="Link">                           
                                 <%#Container.DataItem("USERNAME")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="30%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>                        
                        
                    </Columns>
                </asp:GridView>
            </div>
            <br />
			   <asp:Button ID="btnPostback" runat="server" style="display:none;" />
        </div>
        <asp:HiddenField ID="hidUserId" runat="server" />
        <asp:HiddenField ID="hidUserDes" runat="server" />       
       <asp:HiddenField ID="hidUsernameD" runat="server" /> 
	      <asp:HiddenField ID="hidUser" runat="server" />
    </div>
    </form>
</body>
</html>
