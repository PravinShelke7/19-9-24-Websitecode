<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DateDetails.aspx.vb" Inherits="SavvyPack_Popup_DateDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Milestones</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
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
        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }
    </script>
    <style type="text/css">
        .TdHeadingNew
        {
            background-color: #58595B;
            color: White;
            font-family: Optima;
            font-size: 12px;
        }
        
        .MediumTextBox1
        {
            font-family: Optima;
            font-size: 11.5Px;
            height: 17px;
            width: 80px;
            background-color: #FEFCA1;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <cc1:ToolkitScriptManager ID="toolScriptManageer1" runat="server">
    </cc1:ToolkitScriptManager>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" style="width: 480px; text-align: center">
                    <asp:Label ID="Label1" Text="Milestones" runat="server"></asp:Label>
                </div>
                <div id="error">
                    <asp:Label ID="Label2" runat="server" CssClass="Error"></asp:Label>
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
                    <div id="PageSection1" style="text-align: center">
                        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                        <br />
                        <div style="vertical-align: middle; font-weight: bold; text-align: center; margin-left: 40px;
                            height: 150px; overflow: auto;">
                            <asp:Table ID="tblDate" runat="server" Width="400px">
                            </asp:Table>
                        </div>
                        <br />
                        <table style="margin-left: 160px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" Width="150px" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="hidProjectId" runat="server" />
    </form>
</body>
</html>
