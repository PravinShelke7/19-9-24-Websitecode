<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelectProjTitle.aspx.vb"
    Inherits="OnlineForm_Popup_SelectProjTitle" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Project Title</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">
        function UserSearch(UserDes, UesrId) {
            var hidCaseid = document.getElementById('<%= hdnUserId.ClientID%>').value;
            var hidCaseLinkid = document.getElementById('<%= hdnUserDes.ClientID%>').value;
            var hidPrjDes = document.getElementById('<%= hidPrjDes.ClientID%>').value;

            var str = UserDes.replace(/##/g, "'");
            str = str.replace(/$#/g, '"');
            window.opener.document.getElementById(hidCaseLinkid).innerText = UserDes.replace("&#", "'");
            window.opener.document.getElementById(hidPrjDes).value = UserDes.replace("&#", "'");
            window.opener.document.getElementById(hidCaseid).value = UesrId;
            window.close();
        }
//        function CheckSP(text, e) {
//            var a = /\<|\>|\&#|\\/;
//            var object = document.getElementById(text.id)//get your object
//            if ((document.getElementById(text.id).value.match(a) != null)) {

//                alert("You cannot use the following COMBINATIONS of characters:'<', '>', '\\', '&#' in Search Text. Please choose alternative characters.");
//                object.focus(); //set focus to prevent jumping
//                object.value = text.value.replace(new RegExp("<", 'g'), "");
//                object.value = text.value.replace(new RegExp(">", 'g'), "");
//                object.value = text.value.replace(/\\/g, '');
//                object.value = text.value.replace(new RegExp("&#", 'g'), "");
//                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
//                return false;
//            }
//        }

        

    </script>
    <style type="text/css">
        .style1
        {
            width: 138px;
        }
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; margin: 0 0 0 0; width: 580px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <br />
            <table cellpadding="0" cellspacing="2" style="width: 480px;">
                <tr style="height: 20px;">
                    <td colspan="3" style="font-size: 17px; font-weight: bold; text-align: center; margin-bottom: 5px;
                        width: 100%; height: 25px; left: 10px; text-align: center;">
                        Project Title
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style1">
                        <b>Project :</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtUser" runat="server" CssClass="SavvyTextBox" Width="252px" autocomplete="off"
                            ></asp:TextBox>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                    </td>
                </tr>
                <tr>
                </tr>
            </table>
            <div style="height: 340px; overflow: auto; width: 570px;">
                <asp:GridView Width="500px" runat="server" ID="grdUserDetails" DataKeyNames="PROJECTID"
                    AutoGenerateSelectButton="false" AllowPaging="true" AllowSorting="false" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None" Font-Size="12px" Font-Names="verdana">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="PROJECTID" HeaderText="PROJECT ID" SortExpression="COMPANY">
                            <ItemStyle HorizontalAlign="Left" Width="150px" CssClass="breakword" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="TITLE" SortExpression="PROJECTID">
                            <ItemTemplate>
                                <a href="#" onclick="UserSearch('<%#Container.DataItem("TITLE1")%>','<%#Container.DataItem("PROJECTID")%>')"
                                    class="Link">
                                    <%# Container.DataItem("TITLE")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="200px" CssClass="breakword" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle CssClass="AlterNateColor4" ForeColor="White" HorizontalAlign="Center" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnUserId" runat="server" />
    <asp:HiddenField ID="hdnUserDes" runat="server" />
    <asp:HiddenField ID="hidPrjDes" runat="server" />
    </form>
</body>
</html>
