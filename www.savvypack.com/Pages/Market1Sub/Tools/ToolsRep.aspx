<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ToolsRep.aspx.vb" Inherits="Pages_Market1_Tools_ToolsRep"
    Title="Untitled Page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                            CheckSPMul("500");
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

        function ShowPopWindow(Page, HidID) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  

            var width = 500;
            var height = 180;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var Hid = document.getElementById(HidID).value
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            Page = Page + '&hidValue=' + Hid
            //            alert(Page);
            newwin = window.open(Page, 'PopUp', params);

        }
        
    
    </script>
    <script type="text/javascript">


        function Message(Case, Flag) {

            if (Flag == 'CC') {
                var Case1 = document.getElementById(Case)
                var msg = "You are going to create a copy Report#" + Case1.value + ". Do you want to proceed?"
            }
            if (Flag == 'NC') {
                var msg = "You are going to create a new report. Do you want to proceed?"
            }

            if (Flag == 'DC') {
                var Case1 = document.getElementById(Case)
                msg = "You are going to delete Report#" + Case1.value + ". Do you want to proceed?"
            }

            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

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
</head>
<body>
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
                                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/GlobalManager.gif"
                                        Text="Global Manager" CssClass="ButtonWMarigin" PostBackUrl="~/Pages/Market1/Default.aspx"
                                        onmouseover="Tip('Return to Global Manager')" onmouseout="UnTip()" CausesValidation="false" />
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
        <table class="ContentPage" id="ContentPage" runat="server" style="width: 845px">
            <tr style="height: 20px">
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Econ1-Tools')"
                        onmouseout="UnTip()" style="width: 840px;">
                        Market - Tools
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left">
                            <div id="div" runat="server" visible="true">
                                <table width="820px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Source Proprietary Reports
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="600px" runat="server">
                                            </asp:DropDownList>
                                            <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                Text="You currently have no Proprietary Report to display. You can create a Report with the Tools below."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="height: 30px">
                                        <td>
                                            <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="ButtonWMarigin" OnClientClick="return Message('ddlPCases','CC');"
                                                onmouseover="Tip('Create a copy this Report')" onmouseout="UnTip()" Style="margin-left: 10px"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="ButtonWMarigin" onmouseover="Tip('Edit this Report')"
                                                onmouseout="UnTip()" Style="margin-left: 10px; width: 37px;" CausesValidation="false" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                                OnClientClick="return Message('ddlPCases','DC');" onmouseover="Tip('Delete this Report')"
                                                onmouseout="UnTip()" Style="margin-left: 10px" CausesValidation="false" />
                                            <asp:Button ID="btnCreate" runat="server" Text="Create" CssClass="ButtonWMarigin"
                                                onmouseover="Tip('Create A New Report')" onmouseout="UnTip()" Style="margin-left: 10px"
                                                CausesValidation="false" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div id="divCreate" style="margin-top: 0px; margin-left: 2px;" runat="server" visible="false">
                                <table style="width: 600px;">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            Report Details
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Report Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRptName" runat="server" CssClass="LongTextBox" Style="text-align: left;"
                                                MaxLength="25"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtRep" runat="server" ControlToValidate="txtRptName"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td align="right">
                                            Report Rows:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRw" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtRw" runat="server" ControlToValidate="txtRw"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Report Columns:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCol" runat="server" CssClass="SmallTextBox" Style="text-align: left;"
                                                MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtCol" ControlToValidate="txtCol" runat="server"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td align="right">
                                            Report Type:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlReportType" runat="Server" Width="180px" AutoPostBack="true"
                                                CssClass="DropDown">
                                                <asp:ListItem Text="1-Dimensional" Value="1D" Selected="True"></asp:ListItem>
                                                <asp:ListItem Text="2-Dimensional" Value="2D"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Filter Type:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilterType" runat="Server" Width="180px" AutoPostBack="true"
                                                CssClass="DropDown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td align="right">
                                            <asp:Label ID="lblFilterName" runat="server"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFilter" runat="Server" Width="263px" CssClass="DropDown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" id="rw_Fact" runat="server" visible="false">
                                        <td align="right">
                                            Fact
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFact" runat="Server" Width="180px" CssClass="DropDown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
            </tr>
            <tr class="AlterNateColor2" id="rw_Curr" runat="server" visible="false">
                <td align="right">
                    Currency
                </td>
                <td>
                    <asp:DropDownList ID="ddlCurr" runat="Server" Width="180px" CssClass="DropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td align="right">
                </td>
                <td>
                    <asp:Button ID="btnSubmitt" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />
                    <asp:Button ID="btnSubmitt2" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;"
                        Visible="false" />
                    &nbsp;<asp:Button ID="btnCancle" runat="server" CssClass="Button" Text="Cancel" Style="margin: 0 0 0 0;"
                        CausesValidation="false" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divReportFrameWork" style="margin-top: 0px; margin-left: 2px;" runat="server"
            visible="true">
            <asp:Table ID="tblCAGR" runat="server" CellPadding="0" CellSpacing="1">
            </asp:Table>
            <br />
            <asp:Button ID="btnAddNew" runat="server" CssClass="Button" Text="Save" Style="margin: 0 0 0 0;"
                Visible="false" />
        </div>
    </div>
    <br />
    </div> </div> </td> </tr> </table> </div>
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
