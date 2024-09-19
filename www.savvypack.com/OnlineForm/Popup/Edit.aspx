<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Edit.aspx.vb" Inherits="Pages_PopUp_Edit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Details</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">

        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            var key;
            var type = document.getElementById("hidType").value;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                            var ControlID = $(this).attr('id');
                            var ControlValue = $(this).val();

                            var str = String(ControlID);
                            var matches = str.match(/(\d+)/);
                            key = matches[0] - 1;                           
                            
                            if (type == 'PROD') {
                                key = "Product to Pack " + String(matches[0] - 1);
                            }
                            else if (type == 'SIZE') {
                                key = "Package Size " + String(matches[0] - 1);
                            }
                            else if (type == 'TYPE') {
                                key = "Package Type " + String(matches[0] - 1);
                            }
                            else if (type == 'GEOG') {
                                key = "Geography " + String(matches[0] - 1);
                            }
                            else if (type == 'VCHAIN') {
                                key = "Value Chain " + String(matches[0] - 1);
                            }
                            else if (type == 'SPFET1') {
                                key = "Special Features 1 " + String(matches[0] - 1);
                            }
                            else if (type == 'SPFET2') {
                                key = "Special Features 2 " + String(matches[0] - 1);
                            }
                            PageMethods.UpdateCase(key, ControlValue, String(matches[0] - 1), onSucceed, onError);

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

            function onSucceed(result) {

            }

            function onError(result) {

            }
        });

        //function CheckSP(text) {

        //    var a = /\<|\>|\&#|\\/;
        //    var key;
        //    var type = document.getElementById("hidType").value;
        //    var object = document.getElementById(text.id)//get your object
        //    if ((document.getElementById(text.id).value.match(a) != null)) {

        //        alert("You cannot use the following COMBINATIONS of characters:< > \\  &# . Please choose alternative characters.");

        //        object.focus(); //set focus to prevent jumping
        //        object.value = text.value.replace(new RegExp("<", 'g'), "");
        //        object.value = text.value.replace(new RegExp(">", 'g'), "");
        //        object.value = text.value.replace(/\\/g, '');
        //        object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //        object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //        var str = String(text.id);
        //        var matches = str.match(/(\d+)/);
        //        key = matches[0] - 1;
        //        if (type == 'PROD') {
        //            key = "Product to Pack " + String(matches[0] - 1);
        //        }
        //        else if (type == 'SIZE') {
        //            key = "Package Size " + String(matches[0] - 1);
        //        }
        //        else if (type == 'TYPE') {
        //            key = "Package Type " + String(matches[0] - 1);
        //        }
        //        else if (type == 'GEOG') {
        //            key = "Geography " + String(matches[0] - 1);
        //        }
        //        else if (type == 'VCHAIN') {
        //            key = "Value Chain " + String(matches[0] - 1);
        //        }
        //        else if (type == 'SPFET1') {
        //            key = "Special Features 1 " + String(matches[0] - 1);
        //        }
        //        else if (type == 'SPFET2') {
        //            key = "Special Features 2 " + String(matches[0] - 1);
        //        }
        //        PageMethods.UpdateCase(key, text.value, String(matches[0] - 1), onSucceed, onError);
        //        return false;
        //    }
        //    var str = String(text.id);
        //    var matches = str.match(/(\d+)/);
        //    key = matches[0] - 1;
        //    if (type == 'PROD') {
        //        key = "Product to Pack " + String(matches[0] - 1);
        //    }
        //    else if (type == 'SIZE') {
        //        key = "Package Size " + String(matches[0] - 1);
        //    }
        //    else if (type == 'TYPE') {
        //        key = "Package Type " + String(matches[0] - 1);
        //    }
        //    else if (type == 'GEOG') {
        //        key = "Geography " + String(matches[0] - 1);
        //    }
        //    else if (type == 'VCHAIN') {
        //        key = "Value Chain " + String(matches[0] - 1);
        //    }
        //    else if (type == 'SPFET1') {
        //        key = "Special Features 1 " + String(matches[0] - 1);
        //    }
        //    else if (type == 'SPFET2') {
        //        key = "Special Features 2 " + String(matches[0] - 1);
        //    }
        //    PageMethods.UpdateCase(key, text.value, String(matches[0] - 1), onSucceed, onError);
        //}

        //function onSucceed(result) {

        //}

        //function onError(result) {

        //}
    </script>
    <script language="JavaScript" type="text/javascript">

        var isClose = false;
        document.onkeydown = checkKeycode
        function checkKeycode(e) {
            var keycode;
            if (window.event)
                keycode = window.event.keyCode;
            else if (e)
                keycode = e.which;
            if (keycode == 116) {
                isClose = true;
            }
        }
        function mousefunction() {
            isClose = true;
        }
        function doUnload() {
            if (!isClose) {

                if (document.getElementById("hdnUpdate").value == "1") {
                    window.opener.document.getElementById('btnGrid').click();
                }

            }
        }
    </script>
    <script type="text/JavaScript">
        function GroupSearch() {
            window.close();
        }

        function Delete() {
            var count = 0;
            var table = document.getElementById('<%=grdProducts.ClientID %>');
            // alert(table);
            var grps = "";
            for (var i = 1; i < table.rows.length; i++) {
                var Row = table.rows[i];
                var CellValue = Row.cells[0];
                for (j = 0; j < CellValue.childNodes.length; j++) {
                    if (CellValue.childNodes[j].type == "checkbox") {
                        if (CellValue.childNodes[j].checked == true) {
                            count = count + 1;
                            if (grps != "") { grps = grps + "\n "; }
                            grps = grps + Row.cells[1].children[0].value;
                        }
                    }
                }
            }

            if (count == 0) {
                alert("Please select atleast one group to Delete.");
                return false;
            }
            else {
                if (confirm("Do you want to delete following group(s) ?\n----------------------------------------------\n " + grps + "\n---------------------------------------------- ")) {
                    return true;
                }
                else {
                    return false;
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
<body onbeforeunload="doUnload()" onmousedown="mousefunction()">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
        <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
            <div id="PageSection1" style="text-align: left; width: 470px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <%--   <div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:650px;height:30px;">
                      Group Details
</div>--%>
                <table style="text-align: center;">
                    <tr>
                        <td style="height: 10px;"></td>
                    </tr>
                    <tr>
                        <td class="PageSHeading" style="font-size: 16px; width: 700px; text-align: center; margin-left: 100px;">Edit Details
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" Style="text-align: cente; font-size: 16px; font-weight: bold; color: Red;"> </asp:Label>
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
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <div style="width: 470px; overflow: auto; height: 225px;">
                                <asp:GridView Width="450px" runat="server" ID="grdProducts" DataKeyNames="PROJCATEGORYID"
                                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                                    CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                                    <RowStyle CssClass="AlterNateColor1" />
                                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                                    <HeaderStyle Height="25px" BackColor="#6B696B" Font-Size="X-Small" Font-Bold="True"
                                        ForeColor="White" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="GROUPID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                            SortExpression="CATEGORYID">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProjCatID" runat="server" Text='<%# bind("PROJCATEGORYID")%>'></asp:Label>
                                                <asp:Label ID="lblCategoryId" runat="server" Text='<%# bind("CATEGORYID")%>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="70px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete" HeaderStyle-HorizontalAlign="left">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="delete" runat="server"></asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-HorizontalAlign="center" SortExpression="VALUE" Visible="true"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# bind("VALUE")%>'></asp:Label>
                                                <asp:TextBox ID="txtName" CssClass="SmallTextBox" MaxLength="100" Style="text-align: left;"
                                                    Font-Size="8" Width="350px" Enabled="true" runat="server" Text='<%#Bind("VALUE")%>'
                                                    AutoPostBack="false" />
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Width="210px" Wrap="true" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr style="text-align: center;">
                        <td>
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnUpdate" Height="25px" Width="90px" runat="server" Text="Update"
                                            CssClass="ButtonWMarigin" />
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnDelete" Height="25px" Width="90px" runat="server" Text="Delete"
                                            CssClass="ButtonWMarigin" OnClientClick="return Delete();" />
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnClose" Height="25px" Width="90px" runat="server" Text="Close"
                                            CssClass="ButtonWMarigin" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <br />
            </div>
            <asp:HiddenField ID="hidProjectId" runat="server" />
            <asp:HiddenField ID="hidType" runat="server" />
            <asp:HiddenField ID="hidReportDes1" runat="server" />
            <asp:HiddenField ID="hvCaseGrd" runat="server" />
            <asp:HiddenField ID="hdnUpdate" runat="server" />
        </div>
    </form>
</body>
</html>
