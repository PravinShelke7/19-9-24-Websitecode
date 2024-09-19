<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="WebConf.aspx.vb" Inherits="WebConference_WebConf" Title="Web Conferences - Scheduled Web Conferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/JavaScript" src="../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../JavaScripts/tip_balloon.js"></script>
    <script type="text/javascript">
        javascript: window.history.forward(1);
        function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, 'NewWindow', params);

        }

        function AlertMessage(type) {
            //var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

            if (userId != "") {
                msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                if (type == "L") {
                    ShowPopWindow("../Users_Login/LoginS.aspx");
                }
                return true;
            }
        }

        function ConfirmMessage() {

            // var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; 
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;
            //alert(userId);
            var message = "register for a web conference"



            if (userId != "") {
                return true;
            }

            else {
                msg = "--------------------------------------------------------------------------\n To " + message + ", you must first login.\n If you do not have an account, please create an account using Login link.\n--------------------------------------------------------------------------\n";
                alert(msg);
                return false;
            }

        }

    </script>
    <div class="MnContentPage">
        <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Scheduled Web Conferences')"
            onmouseout="UnTip()" style="text-align: center;">
            Scheduled Web Conferences
            <br />
        </div>
        <div style="color: Black; font-family: Verdana; font-size: 12px; text-align: justify">
            To register for a web conference, you must first login.<%--<a onclick="return AlertMessage('L');" id="lnkCreate" style="font-style:normal;font-family:Verdana ;font-size:12px;" class="Link" href="#">login</a>.--%><br />
            If you do not have an account, please create an account using Login link.<%--<a onclick="return AlertMessage();" style="font-style:normal;font-family:Verdana ;font-size:12px;" target="_blank" class="Link" href="../Users_Login/AddEditAccount.aspx">create an account</a> then  <a onclick="return AlertMessage('L');" style="font-style:normal;font-family:Verdana ;font-size:12px;" class="Link" href="#">login</a>.--%>
        </div>
        <br />
        <asp:Repeater ID="rptWebConf" runat="server">
            <HeaderTemplate>
                <table style="width: 100%;" cellpadding="2" cellspacing="0">
                    <tr style="height: 20px;">
                        <td class="WebHeaderTd" style="width: 100px;">
                            Date
                        </td>
                        <td class="WebHeaderTd" style="width: 150px;">
                            Time
                        </td>
                        <td class="WebHeaderTd" style="width: 50px; font">
                            Cost
                        </td>
                        <td class="WebHeaderTd">
                            Conference Topic
                        </td>
                    </tr>
            </HeaderTemplate>
            <ItemTemplate>
                <tr class="AlterNateColor1">
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "CONFDATE")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "CONFTIME")%>
                    </td>
                    <td class="WebInnerTd" style="color: Red; font-weight: bold;">
                        <%#DataBinder.Eval(Container.DataItem, "CONFCOST")%>
                    </td>
                    <td class="WebInnerTd" style="color: Green; font-weight: bold;">
                        <%#DataBinder.Eval(Container.DataItem, "CONFTOPIC")%>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td>
                    </td>
                    <td colspan="3" class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "CONFDES")%>
                        You must <b>
                            <asp:LinkButton ID="hyp" runat="server" Text="register" CssClass="Link" ></asp:LinkButton></b>
                        to attend.
                    </td>
                </tr>
            </ItemTemplate>
            <AlternatingItemTemplate>
                <tr class="AlterNateColor2">
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "CONFDATE")%>
                    </td>
                    <td class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "CONFTIME")%>
                    </td>
                    <td class="WebInnerTd" style="color: Red; font-weight: bold;">
                        <%#DataBinder.Eval(Container.DataItem, "CONFCOST")%>
                    </td>
                    <td class="WebInnerTd" style="color: Green; font-weight: bold;">
                        <%#DataBinder.Eval(Container.DataItem, "CONFTOPIC")%>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td>
                    </td>
                    <td colspan="3" class="WebInnerTd">
                        <%#DataBinder.Eval(Container.DataItem, "CONFDES")%>
                        You must <b>
                            <asp:LinkButton ID="hyp" runat="server" Text="register" CssClass="Link" ></asp:LinkButton></b>
                        to attend.
                    </td>
                </tr>
            </AlternatingItemTemplate>
            <FooterTemplate>
                </table>
            </FooterTemplate>
        </asp:Repeater>
        <div style="margin-top: 15px;">
            <b style="font-size: 12px; color: #ff7b3c;">Questions?</b></div>
        <div style="font-size: 12px; text-align: justify">
            Call us at [1] 952-405-7500
            <br />
            or email us at <a class="LinkSupp" href="mailto:sales@savvypack.com" style="text-decoration: none; font-weight: bold; font-size: 12px;">sales@savvypack.com</a>
        </div>
    </div>
      <asp:HiddenField ID="hdnUserId" runat="Server" />
	  <asp:Button ID="btnUser" runat="server" Style="visibility: hidden;"></asp:Button>

</asp:Content>
