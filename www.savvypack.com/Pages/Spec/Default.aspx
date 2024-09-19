<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Spec_Default2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>RFP</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .RFP
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../Images/SavvyPackPRO.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        function ShowPopWindow(PageN) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 500;
            var height = 450;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var pn = "";

            //alert(pn);
            newwin = window.open(PageN, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            //alert(pn);
            newwin = window.open(pn, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">        
            <div>
                <table class="RFP" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                            runat="server" ToolTip="Update" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif"
                                            runat="server" ToolTip="Log Off" Visible="true" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=SPEC" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                            runat="server" ToolTip="Instructions" Visible="true" OnClientClick="return Help();" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgChart" ImageAlign="Middle" ImageUrl="~/Images/ChartN.gif"
                                            runat="server" ToolTip="Charts" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgFeedback" ImageAlign="Middle" ImageUrl="~/Images/FeedbackN.gif"
                                            runat="server" ToolTip="Feedback" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/Notes.gif"
                                            runat="server" ToolTip="Notes" Visible="false" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
            <table class="Comparison" width="100%" style="background-color: #D3E7CB; height: 150px;width:820px;">
                <tr>
                    <td>
                        <div class="PageHeading" style="margin-top: 10px; padding-left: 10px;">
                            RFP
                        </div>
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 648px; padding-left: 10px;
                            padding-right: 10px; margin-top: 5px;">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px; height: 20px; padding-left: 10px;">
                                    Select from Existing RFP
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td id="tdPropCases" runat="server">
                                    <asp:LinkButton ID="lnkRFPs" Style="font-size: 14px; padding-left: 10px" runat="server"
                                        CssClass="Link" Width="300px" OnClientClick="return ShowPopWindow('PopUp/RFPSearch.aspx?Des=lnkRFPs&Id=hidRFP&IdD=hidRFPD');">Select RFP</asp:LinkButton>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2" style="height: 30px;">
                                <td>
                                    <asp:Button ID="btnStart" runat="server" CssClass="Button" Text="Start RFP" Style="margin-left: 0px;"
                                        OnClientClick="return Validation('Prop')" />
                                    <asp:Button ID="btRename" runat="server" Text="Rename" CssClass="Button" Enabled="true">
                                    </asp:Button>
                                    <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                        Style="margin-left: 10px"></asp:Button>
                                    <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                        Text="Currently you have no RFP to display. You can create a RFP with the below CREATE."></asp:Label>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px;">
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="margin-top: 40px;" id="divModify" runat="server" visible="false">
                            <table width="700px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                        Rename RFP
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 1:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 3:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe3" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                            TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnRenameR" runat="server" Text="Rename" CssClass="ButtonWMarigin" />
                                        <asp:Button ID="btnRenameC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div style="margin-top: 40px;" id="divCreate" runat="server" visible="false">
                            <table width="700px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                        Create RFP
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 1:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDes1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDes2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Description 3:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDes3" runat="server" CssClass="MediumTextBox" Style="text-align: left;"
                                            TextMode="MultiLine" Height="100px" Width="489px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnCreateR" runat="server" Text="Create" CssClass="ButtonWMarigin" />
                                        <asp:Button ID="btnCreateC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table width="845px" style="background-color: #D3E7CB;">
                <tr class="AlterNateColor3">
                    <td class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
         </div>
    <asp:HiddenField ID="hidRFP" runat="server" />
    <asp:HiddenField ID="hidRFPD" runat="server" />
    </form>
</body>
</html>
