<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Notes.aspx.vb" Inherits="Pages_Sustain1_Notes_Notes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>S1-Page Notes</title>
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
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
        .style2
        {
            height: 20px;
        }
        .style3
        {
            height: 20px;
        }
    </style>
</head>
<body style="margin: 0 0 0 0">
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left;">
            <div style="margin-left: 10px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <table cellpadding="0" cellspacing="0" style="width: 500px; height: 400px;" border="0">
                    <tr>
                        <td class="style1">
                            <b>Notes For :</b><asp:Label ID="lblNotesFor" runat="server" CssClass="NormalLable"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" class="style3">
                            <b>Notes :</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" valign="top">
                            <asp:TextBox TextMode="MultiLine" Rows="10" Columns="10" runat="server" ID="txtNotes"
                                Height="220px" Width="450px"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="ButtonWMarigin" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
