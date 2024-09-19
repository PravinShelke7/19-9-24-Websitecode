<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetEquipmentPopUp.aspx.vb"
    Inherits="Pages_Econ2_PopUp_GetEquipmentPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E2-Get Equipment</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
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
                            CheckSPMul("490");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">
        function EquipmentDet(EquipDes, EquipId, EQUIPDES3) {
            var hidEqdes = document.getElementById('<%= hidEqdes.ClientID%>').value;
            var hidEid = document.getElementById('<%= hidEid.ClientID%>').value;
            var EqName = document.getElementById('<%= hidNew.ClientID%>').value;
            var i = hidEid;
            i = i.match(/\d+$/)[0];
            if (EqName == "") {
                if (EquipDes.length > 38) {
                    var Name = EquipDes.substring(0, 20);
                    var Name1 = EquipDes.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = EquipDes.substring(0, 20);
                    var Name1 = EquipDes.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                if (EQUIPDES3 != "") {
                    window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
                }
                else {
                    window.opener.document.getElementById(hidEqdes).innerText = Name;
                }
                window.opener.document.getElementById(hidEid).value = EquipId;
            }
            else {
                if (EqName.length > 38) {
                    var Name = EqName.substring(0, 20);
                    var Name1 = EqName.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = EqName.substring(0, 20);
                    var Name1 = EqName.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                if (EQUIPDES3 != "") {
                    window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
                }
                else {
                    window.opener.document.getElementById(hidEqdes).innerText = Name;
                }
                window.opener.document.getElementById(hidEid).value = EquipId;
            }
            if (document.getElementById('<%= hidMod.ClientID%>').value == 'E2') {
                if (EquipId == "0") {
                    window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + i).style.display = "none";
                    window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + i).style.display = "none";
                }
                else {
                    if (EQUIPDES3 != "") {
                        window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + i).style.display = "none";
                        window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + i).style.display = "inline";
                    }
                    else {
                        window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + i).style.display = "inline";
                        window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + i).style.display = "none";
                    }
                }
            }
            document.getElementById('<%= hidEqdes.ClientID%>').value = '';
            document.getElementById('<%= hidEid.ClientID%>').value = '';
            document.getElementById('<%= hidNew.ClientID%>').value = '';
            window.close();
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Equipment De1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMatDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Equipment De2:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMatDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                    </td>
                </tr>
            </table>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 500px;" cellpadding="4" cellspacing="0">
                 <tr class="AlterNateColor4">
                   <td width="200">
                        Equipment Des1
                    </td>
                   <td width="200">
                        Equipment Des2
                    </td>
                     <td width="100">
                        Equipment Label
                    </td>
                </tr>
            </table>
            <div style="width: 520px; height: 230px; overflow: auto;">
                <asp:GridView Width="500px" runat="server" ID="grdEquipment" DataKeyNames="equipID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="equipID" HeaderText="equipID" SortExpression="equipID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Equipment Des1" SortExpression="MATDE1">
                            <ItemTemplate>
                              <%--  <a href="#" onclick="EquipmentDet('<%#Container.DataItem("equipDES1")%>','<%#Container.DataItem("equipID")%>')"
                                    class="Link">
                                    <%#Container.DataItem("equipID")%>:<%#Container.DataItem("equipDE1")%></a>--%>
                                     <asp:LinkButton ID="lnkBtnval" runat="server" CssClass="Link">
                                 <%# Container.DataItem("equipID")%>:<%# Container.DataItem("equipDE1")%></asp:LinkButton>
                                 <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# bind("ELabel")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="equipDE2" HeaderText="Equipment Des2" SortExpression="equipDE2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left"  Width="40%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ELabel" HeaderText="Equipment Label" SortExpression="ELabel"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
       <asp:HiddenField ID="hidEqdes" runat="server" />
        <asp:HiddenField ID="hidEqid" runat="server" />
        <asp:HiddenField ID="hidEid" runat="server" />
        <asp:HiddenField ID="hidMod" runat="server" />
        <asp:HiddenField ID="hidNew" runat="server" />

    </div>
    </form>
</body>
</html>
