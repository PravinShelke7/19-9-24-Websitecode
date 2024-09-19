<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BagMaking.aspx.vb" Inherits="Pages_SavvyPackPro_BagMaking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="pnlSMInvest" runat="server">
                        <table class="ContentPage" id="tblSMInvest" runat="server" style="margin-top: 15px;">
                            <tr>
                                <td>
                                    <div class="PageHeading" id="div2" runat="server" style="text-align: center;">
                                        Bag Making Machine
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Table ID="tblBagM" runat="server" CellPadding="2" CellSpacing="2"></asp:Table>
                                    <br />
                                    <%--<asp:Button ID="btnupdate" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />--%>
                                </td>
                            </tr>
                              <tr>
                        <td>
                        
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" /></td>
                        </tr>
                            <tr class="AlterNateColor3">
                                <td class="PageSHeading" align="center">
                                    <asp:Label ID="Label3" runat="Server"></asp:Label>
                                </td>
                            </tr>
                      
                        </table>
                       
                    </asp:Panel>
    </div>
     <asp:HiddenField ID="hidSortIdSMPM" runat="server" />
        <asp:HiddenField ID="hidSortIdSpec" runat="server" />
        <asp:HiddenField ID="hidPMGrpId" runat="server" />
        <asp:HiddenField ID="hidPMGrpNm" runat="server" />
        <asp:HiddenField ID="hidRowNum" runat="server" Value="2" />
        <asp:HiddenField ID="hidEqId" runat="server" />
    </form>
</body>
</html>
