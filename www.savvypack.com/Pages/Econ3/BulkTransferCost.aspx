<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BulkTransferCost.aspx.vb" Inherits="Pages_Econ3_BulkTransferCost" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <title>Bulk Transfer Report</title>
    <style type="text/css">
        .divUpdateprogress {
            left: 410px;
            top: 200px;
            position: absolute;
        }
    </style>
    <script type="text/JavaScript">
        function CheckSP(text) {

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <div>
            <div id="ContentPagemargin" runat="server">
                <div id="PageSection1" style="text-align: left;">
                    <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                        <img id="loading-image" src="../../Images/Loading2.gif" alt="Loading..." width="100px"
                            height="100px" />
                    </div>
                    <b>Enter File name :</b>
                    <asp:TextBox ID="txtName" CssClass="MediumTextBox" onchange="javascript:CheckSP(this);"
                        runat="server"></asp:TextBox><b>.CSV</b>
                    <asp:Button ID="btnExport" runat="server" Text="Export Data" />
                    <br />
                    <asp:Table ID="tblComparision" Width="2000px" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
