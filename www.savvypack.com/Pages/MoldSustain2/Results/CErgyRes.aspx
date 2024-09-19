<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CErgyRes.aspx.vb" Inherits="Pages_MoldSustain2_Results_CErgyRes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>S2 Mold-Energy Comparison</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .S2MoldModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../../Images/S2MoldHeader.gif');
            height: 44px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>
        <div>
            <table class="S2MoldModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                        Sustain2 Mold - Energy Comparison
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                            </asp:Table>
                            <br />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
