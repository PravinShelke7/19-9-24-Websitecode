<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ViewFiles.aspx.vb" Inherits="Pages_PopUp_ViewFiles" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>View Details</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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
    <div id="ContentPagemargin" >
      
        <div id="PageSection1" style="text-align: left; width: 610px; height: 250px; overflow: auto;">
            <asp:Label ID="Label2" runat="server"></asp:Label>
            <br />
            <asp:Table ID="tblDwnldList" runat="server" Width="590px">
            </asp:Table>
            <asp:Label ID="lblError" runat="server"></asp:Label>
            <br />
        </div>
        </div>
    </form>
</body>
</html>
