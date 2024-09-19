<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DownTimeInfo.aspx.vb"
    Inherits="Pages_Econ1_DownTimeInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Equipment Info</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <style type="text/css">
        .MatSmallTextBox {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 50px;
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
        <div id="ContentPagemargin" style="width: 75%; margin: 0 0 0 0;">
            <div id="PageSection1" style="text-align: left">
                <div id="divMainHeading" style="text-align: center; font-weight: bold; font-size: large;">
                    <asp:Label ID="lblTitle" runat="server"></asp:Label>
                </div>
                </br>
            <div id="div1" style="text-align: center; margin-left: 20px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <asp:Table ID="tblData" runat="server"></asp:Table>
            </div>
                <br />
            </div>
            <asp:HiddenField ID="hidMatId" runat="server" />
        </div>
    </form>
</body>
</html>
