<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupDetailsFinal.aspx.vb"
    Inherits="Pages_Market1Sub_PopUp_GroupDetailsFinal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Structure Details</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
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
    <style type="text/css">
        .FixedHeader
        {
            position: absolute;
            font-weight: bold;
            margin-left: 0px;
            margin-right: 35px;
        }
    </style>
    <script type="text/JavaScript">
        function clickButton(e, buttonid) {

            var bt = document.getElementById(buttonid);
            if (bt) {

                if (event.keyCode == 13) {
                    document.getElementById(buttonid).focus();
                    // alert(buttonid);
                    //document.getElementById(buttonid).click();  

                }
            }

        }

        function GroupSearch() {
            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }

        function SelectAllCheckboxes(spanChk) {

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?

              spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
            }
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
    <div id="ContentPagemargin" style="width: 60%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; width: 780px; height: 410px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table style="text-align: center;">
                <tr>
                    <td style="height: 10px;" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td class="PageSHeading" style="font-size: 16px;" colspan="2">
                        <div style="width: 782px; text-align: center;">
                            Report Details
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" Style="text-align: center;
                            font-size: 16px; font-weight: bold; color: Red;"> </asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="width: 500px">
                        <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel"
                            Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                        <asp:TextBox ID="txtKey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                            width: 280px" MaxLength="100"></asp:TextBox>
                        <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            <div id="container" style="width: 720px; height: 296px; overflow: auto; margin-left: 10px;">
                <asp:GridView Width="700px" runat="server" ID="grdGrpReports" DataKeyNames="USERREPORTID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <HeaderStyle Height="25px" Width="580px" BackColor="#6B696B" Font-Size="11px" Font-Bold="True"
                        ForeColor="White" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <Columns>
                        <asp:BoundField DataField="USERREPORTID" HeaderText="REPORTID" SortExpression="USERREPORTID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <input id="HeaderLevelCheckBox" onclick="javascript:SelectAllCheckboxes(this);" style="width: 30px"
                                    runat="server" type="checkbox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblREPORT" runat="server" Text='<%# bind("USERREPORTID")%>' Style="display: none"></asp:Label>
                                <asp:CheckBox ID="SelREPORT" runat="server"></asp:CheckBox>
                            </ItemTemplate>
                            <ItemStyle Width="30px" Wrap="true" HorizontalAlign="Center" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="USERREPORTID" HeaderText="REPORTID" SortExpression="USERREPORTID">
                            <ItemStyle Width="50px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CASEDES1" HeaderText="REPORT DESCRIPTOR" SortExpression="CASEDES1">
                            <ItemStyle Width="350px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" Width="350px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CREATEDDATE" HeaderText="REPORT CREATION DATE" SortExpression="CREATEDDATE">
                            <ItemStyle Width="150px" Wrap="true" CssClass="NormalLabel" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:Button runat="server" ID="btnSubmitReg" Text="Submit" />
            <br />
        </div>
        <asp:HiddenField ID="hidReportId" runat="server" />
        <asp:HiddenField ID="hidReportDes" runat="server" />
        <asp:HiddenField ID="hidReportDes1" runat="server" />
        <asp:HiddenField ID="hvREPORTGrd" runat="server" />
        <asp:HiddenField ID="hidGroupId" runat="server" />
    </div>
    </form>
</body>
</html>
