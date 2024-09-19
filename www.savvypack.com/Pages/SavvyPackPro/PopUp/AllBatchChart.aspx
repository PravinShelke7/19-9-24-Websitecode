<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AllBatchChart.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_AllBatchChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>All Chart</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV1.js"></script> 
    <script type="text/javascript" language="javascript">       
        function UnLoafGif() {            
            window.parent.document.getElementById('loading').style.display = "none";
        }
    </script>   
</head>
<body>
    <form id="form1" runat="server">
        <div id="PageSection1" style="text-align: center">

            <asp:Table ID="tblChart" runat="server" CellPadding="0" CellSpacing="1"
                Style="margin-left: 50px;">
            </asp:Table>

        </div>
    </form>
</body>
</html>
