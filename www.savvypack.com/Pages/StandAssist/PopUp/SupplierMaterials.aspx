<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SupplierMaterials.aspx.vb"
    Inherits="Pages_StandAssist_PopUp_SupplierMaterials" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stand Alone Barrier Assistant-Get Grades</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .SBAModule1
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url('../../../Images/savvypackstructureassistanttds-02.gif');
            height: 55px;
            width: 1000px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        
        .NoDesc
        {
            text-align: center;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left; overflow: auto; height: 650px;">
            <div id="SBA" runat="server">
                <table width="100%">
                    <tr>
                        <td style="width: 100%; font-size: 10px" valign="top">
                            <%--<table class="SBAModule1" id="SBATable" runat="server" visible="true" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:420px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>
                                <td  >
                                      
                                </td>                                                                                                                       
                                
                        </tr>
                    </table>
               </td>
               
            </tr>
        </table>--%>
                            <table style="width: 100%; height: 60px">
                                <tr>
                                    <td align="center">
                                        <div id="SavvyBar" style="width: 100%; height: 55px">
                                            <asp:Image ImageAlign="AbsMiddle" ID="Image1" ImageUrl="~/Images/SavvyPackStructureAssistantTDS-R05.png"
                                                Width="100%" Height="55px" runat="server" />
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%;">
                            <table id="tblrow" runat="server">
                                <tr style="width: 100%;">
                                    <td style="width: 34%; vertical-align: top;">
                                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse; margin-left: 5px;">
                                            <tr style="height: 20px; padding-left: 5px">
                                                <td style="text-align: Left; font-size: 15px;">
                                                    <b>Company:</b><asp:Label ID="lblCompany" runat="server" CssClass="NormalLable"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 20px; padding-left: 5px;">
                                                <td style="text-align: Left; font-size: 15px;">
                                                    <b>Product Grade:</b><asp:Label ID="lblGrade" runat="server" CssClass="NormalLable"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="height: 20px; padding-left: 5px;">
                                                <td style="text-align: Left; font-size: 15px;">
                                                    <b>Product Type:</b><asp:Label ID="lblGradeType" runat="server" CssClass="NormalLable"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td style="width: 5%; vertical-align: top;">
                                        <asp:Label ID="lblDesc" runat="server" Text="Description: " Font-Bold="true" Font-Size="15px">
                                        </asp:Label>
                                    </td>
                                    <td style="width: 52%; vertical-align: top; margin-left: 1px; text-align: justify;">
                                        <asp:Label ID="lblDescription" runat="server" Font-Size="13px"></asp:Label>
                                        <asp:Label ID="lblNoDesc" runat="server" Font-Size="14px" CssClass="NoDesc"></asp:Label>
                                    </td>
                                    <td style="width: 9%; vertical-align: top;">
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <table cellspacing="0" style="text-align: center; width: 90%">
                <tr>
                    <td align="center" style="font-family: Arial; font-size: 14px; font-weight: bold;
                        height: 15px;">
                    </td>
                </tr>
            </table>
            <table cellspacing="10" style="">
                <tr>
                    <td style="width: 40%; font-size: 10px" valign="top">
                        <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr class="AlterNateColor3">
                    <td class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                   <td class="PageSHeading" align="center">
                        <table style="width: 1350px; background-color: #edf0f4;">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="imgFoot2" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
           <%-- <table style="width: 100%; height: 50px">
                <tr>
                    <td align="center">
                        <div id="AlliedLogo" style="width: 100%; height: 50px">
                            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/SavvyPackLogoB.gif"
                                Width="100%" Height="50px" runat="server" />
                        </div>
                    </td>
                </tr>
            </table>--%>
        </div>
        <asp:HiddenField ID="hidSupplierId" runat="server" />
        <asp:HiddenField ID="hidMatId" runat="server" />
        <asp:HiddenField ID="hidSponsor" runat="server" />
        <asp:HiddenField ID="hidGradeId" runat="server" />
    </div>
    </form>
</body>
</html>
