<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditComparision.aspx.vb"
    Inherits="Pages_Sustain1_Tools_EditComparision" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Sustain1-Edit Group</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" language="javascript">
        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 98; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }
        function trim(str) {
            while (str.substring(0, 1) == ' ') {
                str = str.substring(1, str.length);
            }
            while (str.substring(str.length - 1, str.length) == ' ') {
                str = str.substring(0, str.length - 1);
            }
            return str;
        }
        function ValidateGroup() {
            var grpName = document.getElementById("txtGName1").value;

            if (trim(grpName) == "") {
                alert('Please enter Group Name');
                return false;
            }
            else {
                return true;
            }

        }
        function getCountForward() {

            var lst1 = document.getElementById("lstRegion1");
            var lst2 = document.getElementById("lstRegion2");
            var count1 = 0;
            var count2 = lst2.options.length;
            var countSum = 0;
            for (i = 0; i < lst1.options.length; i++) {
                if (lst1[i].selected) {
                    count1 = count1 + 1;
                }
            }
            countSum = count1 + count2;
            if (count2 == 1000) {
                var msg = "-----------------------------------------------------\n";
                msg += "maximum number of cases already transferred.\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }
            if (count1 < 1) {
                var msg = "-----------------------------------------------------\n";
                msg += "Please select atleast one case for transfer.\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }
            if (countSum <= 1000) {
                return true;
            }
            else {
                var msg = "-----------------------------------------------------\n";
                msg += "You cannot transfer more than " + (1000 - count2) + " cases .\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);
                return false;
            }

        }
        function getCountBackward() {

            var lst1 = document.getElementById("lstRegion1");
            var lst2 = document.getElementById("lstRegion2");
            var count1 = 0;
            var count2 = lst2.options.length;
            var countSum = 0;
            for (i = 0; i < lst2.options.length; i++) {
                if (lst2[i].selected) {
                    count1 = count1 + 1;
                }
            }
            //        if(count1==count2)
            //        {
            //            var msg = "-----------------------------------------------------\n";
            //            msg += "You can not delete all the cases.\n";
            //            msg += "-----------------------------------------------------\n";
            //          alert(msg);
            //           return false;
            //        }
            if (count1 == 0) {
                var msg = "-----------------------------------------------------\n";
                msg += "Please select atleast one case for transfer.\n";
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
<body>
    <form id="form1" runat="server">
    <div>
        <div id="MasterContent">
            <div id="AlliedLogo">
                <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                    runat="server" />
            </div>
            <div>
                <table class="S1Module" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                    <tr>
                        <td style="padding-left: 490px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                            runat="server" ToolTip="Update" Visible="false" />
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/Globaltoolbox.gif"
                                            Text="Group Toolbox" CssClass="ButtonWMarigin" PostBackUrl="~/Pages/Sustain1/Tools/ManageGroup.aspx"
                                            onmouseover="Tip('Return to Group Tools')" onmouseout="UnTip()" />
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
                                        <asp:ImageButton ID="imgNotes" ImageAlign="Middle" ImageUrl="~/Images/NotesN.gif"
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
        <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px" cellpadding="0"
            cellspacing="0">
            <tr style="height: 20px">
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Sustain1-Edit Group')"
                        onmouseout="UnTip()" style="width: 840px;">
                        Sustain1-Edit Group
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left">
                            <table>
                                <tr>
                                    <td colspan="3">
                                        <table>
                                            <tr>
                                                <td>
                                                    <b>Group Name :</b>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGName1" runat="server" Style="text-align: left;" CssClass="SmallTextBox"
                                                        Width="250px" MaxLength="25"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>Group Description :</b>
                                                </td>
                                                <td align="left">
                                                    <asp:TextBox ID="txtGName2" runat="server" Style="text-align: left;" CssClass="SmallTextBox"
                                                        Width="400px" MaxLength="98" onKeyUp="javascript:Count(this);" onChange="javascript:Count(this);"
                                                        TextMode="MultiLine" Height="40px"></asp:TextBox>
                                                </td>
                                                <td align="right">
                                                    <asp:Button ID="btnUpdateDes" runat="server" CssClass="Button" Text="Update Case Description"
                                                        ToolTip="Update Case Description" Width="170px" Height="30px" OnClientClick="return ValidateGroup();" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
            <tr>
                <td valign="middle">
                    <span style="font-family: Arial; font-size: 14px; font-weight: bold">Cases:</span>
                </td>
                <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                </td>
                <td valign="middle">
                    <span style="font-family: Arial; font-size: 14px; font-weight: bold">Current Cases:</span>
                </td>
            </tr>
            <tr>
                <td valign="middle">
                    <asp:Panel ID="pnlRegion1" runat="server" Width="360px">
                        <asp:ListBox ID="lstRegion1" runat="server" Width="360px" Rows="20" SelectionMode="Multiple"
                            Style="font-family: Verdana; font-size: 11px;"></asp:ListBox>
                    </asp:Panel>
                </td>
                <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                    <asp:Button ID="btnFwd" runat="server" Text=">" Width="50px" OnClientClick="return getCountForward()" />
                    <br />
                    <br />
                    <asp:Button ID="btnRew" runat="server" Text="<" Width="50px" OnClientClick="return getCountBackward()"
                        Style="height: 26px" />
                </td>
                <td valign="middle">
                    <asp:Panel ID="pnlRegion2" runat="server" Width="360px">
                        <asp:ListBox ID="lstRegion2" runat="server" Width="360px" Rows="20" SelectionMode="Multiple"
                            Style="font-family: Verdana; font-size: 11px;"></asp:ListBox>
                    </asp:Panel>
                </td>
            </tr>
            </table>
            <br />
            <br />
            <br />
            <br />
            <br />
            </div> </div> </td> </tr>
        </table>
    </div>
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
