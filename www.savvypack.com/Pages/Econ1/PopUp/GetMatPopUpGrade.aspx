<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetMatPopUpGrade.aspx.vb"
    Inherits="Pages_Econ1_PopUp_GetMatPopUpGrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1-Get Materials</title>
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
        function MaterialDet(MatDes, MatId, GradeName, GradeId, Weight, SG, MatDe3) {

            var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value;
            var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value;
            var hidGradedes = document.getElementById('<%= hidGradedes.ClientID%>').value;
            var hidGradeId = document.getElementById('<%= hidGradeid.ClientID%>').value;
            var hidSGV = document.getElementById('<%= hidSG.ClientID%>').value;
            var MatName = document.getElementById('<%= hidNew.ClientID%>').value;
            var i = hidMatId;
            i = i.match(/\d+$/)[0];

            if (Weight != SG) {
                window.opener.document.getElementById(hidSGV).value = Weight;
            }
            else {
                window.opener.document.getElementById(hidSGV).value = "0.000";
            }
            if (MatName == "") {
                if (MatDes.length > 38) {
                    var Name = MatDes.substring(0, 20);
                    var Name1 = MatDes.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = MatDes.substring(0, 20);
                    var Name1 = MatDes.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                if (MatDe3 != "") {
                    window.opener.document.getElementById(hidMatdes).innerText = MatDe3.replace(new RegExp("&#", 'g'), "'");
                }
                else {
                    window.opener.document.getElementById(hidMatdes).innerText = Name;
                }

                window.opener.document.getElementById(hidMatId).value = MatId;
            }
            else {
                if (MatName.length > 38) {
                    var Name = MatName.substring(0, 20);
                    var Name1 = MatName.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = MatName.substring(0, 20);
                    var Name1 = MatName.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                if (MatDe3 != "") {
                    window.opener.document.getElementById(hidMatdes).innerText = MatDe3.replace(new RegExp("&#", 'g'), "'");
                }
                else {
                    window.opener.document.getElementById(hidMatdes).innerText = Name;
                }
                window.opener.document.getElementById(hidMatId).value = MatId;
            }
            if (document.getElementById('<%= hidMod.ClientID%>').value == 'E1') {
                if (MatId == "0") {
                    window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                    window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
                    if (document.getElementById('<%= hidAdmin.ClientID%>').value == 'AADMIN') {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgPriceBut' + i).style.display = "none";
                    }
                }
                else {
                    if (MatDe3 != "") {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "inline";
                    }
                    else {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "inline";
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                    }
                    
                    if (document.getElementById('<%= hidAdmin.ClientID%>').value == 'AADMIN') {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgPriceBut' + i).style.display = "inline";
                    }
                }
            }
            window.opener.document.getElementById(hidGradedes).innerText = GradeName;
            window.opener.document.getElementById(hidGradeId).value = GradeId;

            document.getElementById('<%= hidMatdes.ClientID%>').value = '';
            document.getElementById('<%= hidMatId.ClientID%>').value = '';
            document.getElementById('<%= hidGradedes.ClientID%>').value = '';
            document.getElementById('<%= hidGradeid.ClientID%>').value = '';
            document.getElementById('<%= hidNew.ClientID%>').value = '';
            document.getElementById('<%= hidSG.ClientID%>').value = '';

            //alert(GradeName + '-----' + GradeId);
            window.close();
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
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Material De1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtMatDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Material De2:</b>
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
                        Material Des1
                    </td>
                    <td>
                        Material Des2
                    </td>
                    <td>
                        Material Label
                    </td>
                </tr>
            </table>
            <div style="width: 450px; height: 380px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdMaterials" DataKeyNames="MATID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="false" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="MATID" HeaderText="MATID" SortExpression="MATID" Visible="false">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Material Des1" SortExpression="MATDE1">
                            <ItemTemplate>
                                <%--<a href="#" onclick="MaterialDet('<%#Container.DataItem("MATDES")%>','<%#Container.DataItem("MATID")%>','<%#Container.DataItem("GRADENAME")%>','<%#Container.DataItem("GRADEID")%>','<%#Container.DataItem("WEIGHT")%>','<%#Container.DataItem("SG")%>')"
                                    class="Link">
                                    <%#Container.DataItem("MATID")%>:<%#Container.DataItem("MATDE1")%></a>--%>
                                <asp:LinkButton ID="lnkBtnval" runat="server" CssClass="Link"> <%#Container.DataItem("MATID")%>:<%# Container.DataItem("MATDE1")%></asp:LinkButton>
                                <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# bind("MATDES")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="33%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="MATDE2" HeaderText="Material Des2" SortExpression="MATDE2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="33%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MATDE3" HeaderText="Material Label" SortExpression="MATDE3"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="33%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidMatdes" runat="server" />
        <asp:HiddenField ID="hidMatid" runat="server" />
        <asp:HiddenField ID="hidGradedes" runat="server" />
        <asp:HiddenField ID="hidGradeid" runat="server" />
        <asp:HiddenField ID="hidSG" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidDMatId" runat="server" />
        <asp:HiddenField ID="hidMod" runat="server" />
        <asp:HiddenField ID="hidNew" runat="server" />
<asp:HiddenField ID="hidAdmin" runat="server" />
    </div>
    </form>
</body>
</html>
