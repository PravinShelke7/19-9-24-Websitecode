<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UniversalDeliveryFiles.aspx.vb"
    Inherits="Savvypack_Popup_DeliveryFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Deliverable files</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
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
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" style="width: 700px; text-align: center">
                    <asp:Label ID="Label1" Text="Deliverable Files" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr style="height: 30px">
            <td>
                <div id="PageSection1" style="text-align: left; width: 697px; margin-left: -7px;
                    margin-top: -7px;">
                    <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                    <br />
                    
                    <div id="Div1" style="text-align: left; width: 690px; height: 300px; overflow: auto;">
                        <asp:Label ID="Label2" runat="server"></asp:Label>
                        <br />
                        <asp:Table ID="tblDwnldList" runat="server" Width="670px">
                        </asp:Table>
                         <br />
                        <asp:Label ID="lblMsg" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                            Text="You currently have no Delivery files to display." Font-Size="11" Style="margin-top: 30px;
                                            margin-left: 200px; color: red; font-weight: bold;"></asp:Label>
                        <asp:Label ID="lblError" runat="server"></asp:Label>
                        <br />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <br />
    <asp:HiddenField ID="hidProjectId" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
    </form>
</body>
</html>
