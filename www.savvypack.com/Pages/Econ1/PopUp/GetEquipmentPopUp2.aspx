<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetEquipmentPopUp2.aspx.vb"
    Inherits="Pages_Econ1_PopUp_GetEquipmentPopUp2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1-Get Equipment</title>
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
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">
        function EquipmentDet(EquipDes, EquipId) {
            var hypEqDes = document.getElementById('<%= hidEqdes.ClientID%>').value;
            var hidEqid = document.getElementById('<%= hidEqid.ClientID%>').value;
            EquipDes = EquipDes.replace(/##/g, '"');

            window.opener.document.getElementById(hypEqDes).innerText = EquipDes;
            window.opener.document.getElementById(hidEqid).value = EquipId
            window.close();
        }
        function testData() {

            var hidMatId = document.getElementById('<%= hidEqid.ClientID%>').value;
            var i = hidEqid;
            i = i.match(/\d+$/)[0];

            document.getElementById('<%= hvGroupQ.ClientID%>').value = window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_hidEqCat" + i).value;

            if (document.getElementById('<%= hvGroupQ.ClientID%>').value == "") {

            }
            else {

                document.getElementById('<%= btnCall.ClientID%>').click();
            }



        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>

              <table style="width: 325px">
                <tr>
                    <td style="width: 60%">
                        <asp:Table ID="tblMaterials" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </td>
                    </table> 
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
            <table style="width: 400px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td>
                        Equipment Des1
                    </td>
                    <td>
                        Equipment Des2
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 230px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdEquipment" DataKeyNames="equipID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="equipID" HeaderText="equipID" SortExpression="equipID"
                            Visible="false"></asp:BoundField>
                     

                        <asp:BoundField DataField="equipDE1" HeaderText="Equipment Des1" SortExpression="equipDE1"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>



                        <asp:BoundField DataField="equipDE2" HeaderText="Equipment Des2" SortExpression="equipDE2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidEqdes" runat="server" />
        <asp:HiddenField ID="hidEqid" runat="server" />
          <asp:HiddenField ID="hidLinkId" runat="server" />
           <asp:HiddenField ID="hidEqCat" runat="server" />
            <asp:HiddenField ID="hvGroupQ" runat="server" />
               <asp:HiddenField ID="hidEqGrp" runat="server" />
    </div>
    <asp:Button ID="btnCall" Text="" Width="1px" runat="server" CssClass="Button" Style="margin-left: 5px;display:none;" />
 
    </form>
</body>
</html>
