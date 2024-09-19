<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditComparision.aspx.vb"
    Inherits="Pages_StandAssist_Tools_ComparisonTool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Comparison</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .SBAModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../../Images/SavvyPackStructureAssistantR01.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/JavaScript">
     function CheckSP(text) 
        {
            var a = /\<|\>|\&#|\\/;
            if ((document.getElementById("txtCName").value.match(a) != null)) 
            {
                alert("You cannot use the following COMBINATIONS of characters: < > \\  &# in Comparison Name . Please choose alternative characters.");
                var object = document.getElementById(text.id)  //get your object
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }
        
     function CloseWindow() {
            window.open('', '_self', '');
            window.close();
        }
        function getCountForward() {
            var lst1 = document.getElementById('<%=lstRegion1.ClientID%>');
            var lst2 = document.getElementById('<%=lstRegion2.ClientID%>');
            var count1 = 0;
            var count2 = lst2.options.length;
            var countSum = 0;
            for (i = 0; i < lst1.options.length; i++) {
                if (lst1[i].selected) {
                    count1 = count1 + 1;
                }
            }
            countSum = count1 + count2;
            if (count2 == 10) {
                var msg = "-----------------------------------------------------\n";
                msg += "maximum number of Structures already transferred\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }
            if (count1 < 1) {
                var msg = "-----------------------------------------------------\n";
                msg += "Please select Atleast one Structure for transfering\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }
            if (countSum <= 10) {
                return true;
            }
            else {
                var msg = "-----------------------------------------------------\n";
                msg += "You cannot transfer more than " + (10 - count2) + " Structures.\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }

        }

        function getCountBackward() {
            var lst1 = document.getElementById('<%=lstRegion1.ClientID%>');
            var lst2 = document.getElementById('<%=lstRegion2.ClientID%>');

            var count1 = 0;
            var count2 = lst2.options.length;
            var countSum = 0;
            for (i = 0; i < lst2.options.length; i++) {
                if (lst2[i].selected) {
                    count1 = count1 + 1;
                }
            }
            if (count1 == count2) {
                var msg = "-----------------------------------------------------\n";
                msg += "You can not delete all the Structures.\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }
            if (count1 == 0) {
                var msg = "-----------------------------------------------------\n";
                msg += "Please select Atleast one Structure for transfering\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }



        }
    </script>
</head>
<body style="margin-top: 0px;">
    <%-- <center>--%>
    <form id="form1" runat="server">
    <div id="MasterContent">
       <%-- <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                runat="server" />
        </div>--%>
        <div>
            <table class="SBAModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse; margin-bottom: 12px">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                        runat="server" ToolTip="Update" Visible="true" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="btnGlobalManager" runat="server" ImageUrl="~/Images/Close.gif" OnClientClick="javascript:CloseWindow();"
                                        Text="Close Window" CssClass="ButtonWMarigin" onmouseover="Tip('Close this Window')"
                                        onmouseout="UnTip()" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                        runat="server" ToolTip="Instructions" Visible="false" />
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
    </div>
    <div id="div1" style="margin-top: 0px; margin-left: 2px;" runat="server">
        <table width="840px">
            <tr align="left">
                <td style="width: 43%" class="PageHeading" onmouseover="Tip('Edit Comparison')"
                    onmouseout="UnTip()">
                    Edit Comparison
                </td>
            </tr>
        </table>
    </div>
    <div id="ContentPagemargin">
        <div id="PageSection1" style="text-align: left">
            <br />
            <table>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <b>Comparison Name</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCName" runat="server" CssClass="SearchTextBox" Width="150px" onChange="javascript:CheckSP(this);"
                                        MaxLength="25"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtCname" runat="server" ControlToValidate="txtCName"
                                        ErrorMessage="Comparison Name shouldnt be blank"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <span style="font-family: Arial; font-size: 14px; font-weight: bold">Structures:</span>
                    </td>
                    <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                    </td>
                    <td valign="middle">
                        <span style="font-family: Arial; font-size: 14px; font-weight: bold">Current Structures:</span>
                    </td>
                </tr>
                <tr>
                    <td valign="middle">
                        <asp:Panel ID="pnlRegion1" runat="server" Width="450px" Height="300px" Style="overflow: auto;">
                            <asp:ListBox ID="lstRegion1" runat="server" SelectionMode="Multiple" Height="280px"
                                Style="font-family: Verdana; font-size: 11px; min-width: 100%;"></asp:ListBox>
                        </asp:Panel>
                    </td>
                    <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                        <asp:Button ID="btnFwd" runat="server" Text=">" Width="50px" OnClientClick="return getCountForward()" />
                        <br />
                        <br />
                        <asp:Button ID="btnRew" runat="server" Text="<" Width="50px" OnClientClick="return getCountBackward()" />
                    </td>
                    <td valign="middle">
                        <asp:Panel ID="pnlRegion2" runat="server" Width="450px" Height="300px" Style="overflow: auto;">
                            <asp:ListBox ID="lstRegion2" runat="server" Height="280px" SelectionMode="Multiple"
                                Style="font-family: Verdana; font-size: 11px; min-width: 100%;"></asp:ListBox>
                        </asp:Panel>
                    </td>
                </tr>
               
            </table>
            <br />
        </div>
    </div>
     <div id="div2" style="margin-top: 0px; margin-left: 2px;" runat="server">
        <table width="845px">
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
     <div id="AlliedLogo">
          <table>
           <tr>
                <td class="PageSHeading" align="center">
                    <table style="width: 845px; background-color: #edf0f4;">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           </table>
        </div>
    <asp:HiddenField ID="hidCompID" runat="server" />
    <asp:HiddenField ID="hidCompDes" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
