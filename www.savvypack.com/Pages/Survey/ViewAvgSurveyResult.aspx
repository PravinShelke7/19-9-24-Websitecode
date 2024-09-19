<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewAvgSurveyResult.aspx.vb"
    Inherits="Pages_Survey_ViewAvgSurveyResult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Avg Survey Result</title>
    <script type="text/javascript">

        function Count(text) {
            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)  //get your object
            if (document.getElementById(text.id).value.match(a) != null) {
                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Comment. Please choose alternative characters.");
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
    <style type="text/css">
        .PSavvyModule
        {
            margin-top: 2px;
            background: url('../../Images/SavvypackSurvey.jpg') no-repeat;
            height: 45px;
            width: 681px;
            text-align: center;
            vertical-align: middle;
        }
        .PageHeading
        {
            font-size: 18px;
            font-weight: bold;
        }
        .ContentPage
        {
            margin-top: 2px;
            margin-left: 1px;
            width: 680px;
            font-family: Verdana;
            background-color: #FFFFFF;
        }
        #SavvyPageSection1
        {
            background-color: #D3E7CB;
        }
        .AlterNateColor3
        {
            background-color: #D3DAD0;
            height: 20px;
        }
        .PageSHeading
        {
            font-size: 12px;
            font-weight: bold;
        }
        .InsUpdMsg
        {
            font-family: Verdana;
            font-size: 12px;
            font-style: italic;
            color: Red;
            font-weight: bold;
        }
        .AlterNateColor1
        {
            background-color: #C0C9E7;
        }
        
        .AlterNateColor2
        {
            background-color: #D0D1D3;
        }
        .TblSurvey
        {
            background-color: rgb(255,224,224);
            border: 2px ridge rgb(160,0,0);
            padding: .857em;
        }
        .BorderAns
        {
            background-color: rgb(220,220,220);
            border: 1px none rgb(160,0,0);
            padding: .357em;
        }
    </style>
</head>
<body style="background-color: #DDDDDD;">
    <center>
        <form id="form1" runat="server">
        <div class="ContentPage" id="ContentPage">
            <table class="PSavvyModule" id="Savvytable" runat="server" cellpadding="0" cellspacing="0"
                style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                    </td>
                </tr>
            </table>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
            <table id="Table1" runat="server">
                <tr>
                    <td align="left">
                        <div id="Div1" runat="server" style="width: 600px;">
                            <table>
                                <tr>
                                    <td style="width: 700px; text-align: center; font-size: 22px; font-weight: bold;
                                        color: Black;">
                                        <asp:Label ID="lblSName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr id="trUser" runat="server" visible="false">
                                    <td align="right" style="font-weight: bold;">
                                        User Name:
                                    </td>
                                    <td>
                                        <asp:Label ID="lblUName" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td>
                        <div id="SavvyContentPagemargin" runat="server">
                            <div style="text-align: center;">
                                <table style="width: 100%">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNoQuestion" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Table ID="tblSurvey" runat="server" CellPadding="2" CellSpacing="4">
                                            </asp:Table>
                                            <asp:Label ID="lblMsg" runat="server" Text="Thank you for taking our survey. Your response is very important to us."
                                                Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnsend" runat="server" Text="Submit" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidRowNum" runat="server" />
        </form>
    </center>
</body>
</html>
