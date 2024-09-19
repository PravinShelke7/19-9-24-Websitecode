<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DownloadDetails.aspx.vb"
    Inherits="DownloadDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body style="margin-top:-5px;margin-left:-5px;background-color:#F1F1F2;width:540px;">
    <form id="form1" runat="server" style="margin: 0px; padding: 0px;">
    <div id="divContentD">
        <asp:Table ID="tblDwnldList" runat="server" Width="540px">
        </asp:Table>
        <asp:Label ID="lblError" runat="server"></asp:Label>
    </div>
    </form>
</body>
</html>
