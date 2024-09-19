<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AdminTool.aspx.vb" Inherits="Pages_Econ2_Tools_AdminTool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E2-Admin Tools</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("490");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/javascript">
        javascript: window.history.forward(1);

        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;


        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }
        
        
    
    </script>
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
    <script type="text/javascript">

        function CaseCopyCheck(fromCase, toCase) {
            var Case1 = document.getElementById(fromCase)
            var Case2 = document.getElementById(toCase)
            var msg = "You are going to transfer variables of case " + Case1.value + " to case " + Case2.value + ". Do you want to proceed?"
            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

        }

        function ShareMessage(Case, User, Flag) {


            if (Flag == 'SYN') {
                var Case1 = document.getElementById(Case)
                var msg = "You are going to synchronize case " + Case1.value + " from Sustain2 to Econ2. Do you want to proceed?"
            }

            if (confirm(msg)) {

                return true;
            }
            else {

                return false;
            }

        }


        function Message(Case, Flag) {





            if (Flag == 'NC') {
                var msg = "You are going to create a new case. Do you want to proceed?"
            }

            if (Flag == 'NCC') {
                var Case1 = document.getElementById(Case)
                msg = "You are going to create a copy of case " + Case1.value + ". Do you want to proceed?"
            }

            if (Flag == 'DC') {
                var Case1 = document.getElementById(Case)
                msg = "You are going to delete case " + Case1.value + ". Do you want to proceed?"
            }

            if (Flag == 'UC') {
                var Case1 = document.getElementById(Case)
                //msg = "You are going to update the case description for case " + Case1.value + ". Do you want to proceed?"
                if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) {
                    alert("Packaging Format cannot be blank");
                    return false;
                }
                else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) {
                    alert("Unique Features cannot be blank");
                    return false;
                }
                else {
                    msg = "You are going to update the case description for case " + Case1.value + ". Do you want to proceed?"

                }
            }


            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

        }

        function CreateMessage(Case, Flag) {

            if (Flag == 'NC') {
                var msg = "You are going to create a new case. Do you want to proceed?"
            }
            if (Flag == 'NCC') {
                var Case1 = document.getElementById(Case)
                msg = "You are going to create a copy of case " + Case1.value + ". Do you want to proceed?"
            }
            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }


            alert(msg);
            return false;

        }

     
                		       
		        
		       

		       
		    
    </script>
</head>
<body style="margin-top: 0px;">
    <%-- <center>--%>
    <form id="form1" runat="server">
    <div id="MasterContent">
        <%-- <div id="AlliedLogo">
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>     --%>
        <div>
            <table class="E2Module" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                        runat="server" ToolTip="Update" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/GlobalManager.gif"
                                        Text="Global Manager" CssClass="ButtonWMarigin" PostBackUrl="~/Pages/Econ2/Default.aspx"
                                        onmouseover="Tip('Return to Global Manager')" onmouseout="UnTip()" />
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
    <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
        <tr style="height: 20px">
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Econ2 - Admin Tools')"
                    onmouseout="UnTip()" style="width: 840px;">
                    Econ2 - Admin Tools
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left">
                        <table width="820px">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Source Base Case
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="96%" runat="server">
                                    </asp:DropDownList>
                                    <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                        Text="You currently have no Proprietary Cases to display. You can create a Case with the Tools below."></asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="height: 30px">
                                <td>
                                    <asp:Button ID="btnCopyPcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('ddlPCases','NCC');" onmouseover="Tip('Create a Copy of this Case')"
                                        onmouseout="UnTip()" />
                                    <asp:Button ID="btnPTransfer" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                        OnClientClick="return MakeVisible('divCopy');" onmouseover="Tip('Transfer Variables from One Case to Another Case')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnSynchronize" runat="server" Text="Synchronize" CssClass="ButtonWMarigin"
                                        OnClientClick="return ShareMessage('ddlPCases', '', 'SYN');" onmouseover="Tip('Synchronize this Case from Sustain2 to Econ2')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnPRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                        onmouseover="Tip('Rename this Case')" onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                        OnClientClick="return Message('ddlPCases','DC');" onmouseover="Tip('Delete this Case')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                    <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                        OnClientClick="return CreateMessage('ddlPCases','NC');" onmouseover="Tip('Create A New Case')"
                                        onmouseout="UnTip()" Style="margin-left: 10px" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div style="display: none; margin-top: 40px;" id="divCopy">
                            <table width="820px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;">
                                        Target Base Case
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:DropDownList ID="ddlTarget1" CssClass="DropDown" Width="96%" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Button ID="btnPS2T" runat="server" Text="Transfer" CssClass="ButtonWMarigin"
                                            OnClientClick="return CaseCopyCheck('ddlPCases','ddlTarget1');" onmouseover="Tip('Transfer On Target')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="Button11" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" OnClientClick="return MakeInVisible('divCopy');" onmouseover="Tip('Cancel')"
                                            onmouseout="UnTip()" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="margin-top: 40px;" id="divModify" runat="server" visible="false">
                            <table width="700px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                        Rename Case Details
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Packaging Format Definition:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 255px" MaxLength="25"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Unique Features Definition:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 255px" MaxLength="25"></asp:TextBox>
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
                                        <asp:Button ID="btnRename" runat="server" Text="Rename" CssClass="ButtonWMarigin"
                                            OnClientClick="return Message('ddlPCases','UC');" onmouseover="Tip('Rename this Case')"
                                            onmouseout="UnTip()" />
                                        <asp:Button ID="btnRenameC" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                    </td>
                                </tr>
                                <tr class="AlterNateColor3">
                                    <td class="PageSHeading" align="center">
                                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </td>
        </tr>
    </table>
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
    <asp:HiddenField ID="hidBackcheck" Value="0" runat="server" />
    <asp:HiddenField ID="hidTotalCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalSCount" runat="server" />
    <asp:HiddenField ID="hidUserMaxCaseCount" runat="server" />
    <asp:HiddenField ID="hidTotalShareCount" runat="server" />
    <asp:HiddenField ID="hidShareUserMaxCaseCount" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
