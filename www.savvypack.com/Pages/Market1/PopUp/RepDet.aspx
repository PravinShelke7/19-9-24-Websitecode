<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RepDet.aspx.vb" Inherits="Pages_Market1_PopUp_RepDet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
  <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <title>Get Report Details</title>
</head>
 <form id="form1" runat="server">
        <div id="MasterContent">
            <div id="AlliedLogo">
                <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                    runat="server" />
            </div>
            <div>
                <table class="M1Module" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                       
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="Div1">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
            <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
                <tr style="height: 20px">
                    <td>
                        <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Market1 - Report Details')"
                            onmouseout="UnTip()" style="width: 840px;">
                          
                            <asp:Label id="lblheading" runat="Server"></asp:Label>
                            
                        </div>
                    </td>
                </tr>
                <tr>
                   <td>
                        <table border="0" id="tblRepDes" runat="server" cellpadding="0" cellspacing="0">
                        <tr style="height:20px">
                            <td style="width:350px;text-align:Left;">
                                <b>Report Id:</b><asp:Label ID="lblRepID" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                              <td style="width:70px;text-align:Left;" >
                                 <b>Report Type:</b>
                            </td>
                             <td style="width:50%">
                                <asp:Label ID="lblRepType" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>  
                        </tr>
                        <tr style="height:20px">
                            <td colspan="2" >
                                 <span id="caseDe3" runat="server"><b>Report Brief:</b></span>
                                <asp:Label ID="lblRepDes" runat="server" CssClass="NormalLable"></asp:Label>
                            </td>
                        </tr>
                    </table>
                   </td>
                </tr>
                <tr style="height: 20px">
                    <td>
                        <div id="ContentPagemargin" runat="server">
                            <div id="PageSection1" style="text-align: left">
                                    <div id="divReportFrameWork" style="margin-top: 0px; margin-left: 4px;" runat="server"
                                        visible="true">
                                        <asp:Table ID="tblReportDetails" runat="server"></asp:Table>
                                  </div>
                                <br />
                            </div>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script

</html>
