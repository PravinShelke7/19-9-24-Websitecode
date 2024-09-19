<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tool.aspx.vb" Inherits="Pages_Market1_Tools_Tool"
    Title="Market Analysis-Tools" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
        function ValidateData() {
            var rowCount, colCount, filterCount;
            var flag1, flag2, flag3;
            var i = 0;
            //  alert(document.getElementById("hidReportType").value);
            if (document.getElementById("hidReportType").value == "MIXED") {
                rowCount = document.getElementById("txtRw").value;
                colCount = document.getElementById("txtCol").value;
                filterCount = document.getElementById("txtFilter").value;
                //Checking Row Items
                for (i = 1; i <= rowCount; i++) {

                    if (document.getElementById("Row_" + i.toString()).innerText == "Row_" + i.toString()) {
                        flag1 = "true";
                    }
                }

                //Checking Columns Items
                for (i = 1; i <= colCount; i++) {


                    if (document.getElementById("Column_" + i.toString()).innerText == "Column_" + i.toString()) {
                        flag2 = "true";
                    }
                }

                //Checking Filter Items
                for (i = 1; i <= filterCount; i++) {

                    if (document.getElementById("Filter_" + i.toString()).innerText == "Filter_" + i.toString()) {
                        flag3 = "true";
                    }
                }

                if (flag1 == "true" || flag2 == "true" || flag3 == "true") {
                    alert("Please select proper Rows,Columns and Filters selection for saving this Mixed Report.");
                    return false;
                }
                else {
                    return true;
                }
            }
            else if (document.getElementById("hidReportType").value == "UNIFORM") {

                colCount = document.getElementById("txtSpecialCols").value;
                filterCount = document.getElementById("txtSpecialFilters").value;


                //Checking Columns Items
                for (i = 1; i <= colCount; i++) {


                    if (document.getElementById("Column_" + i.toString()).innerText == "Column_" + i.toString()) {
                        flag2 = "true";
                    }
                }

                //Checking Filter Items
                for (i = 1; i <= filterCount; i++) {

                    if (document.getElementById("Filter_" + i.toString()).innerText == "Filter_" + i.toString()) {
                        flag3 = "true";
                    }
                }

                if (flag1 == "true" || flag2 == "true" || flag3 == "true") {
                    alert("Please select proper Columns and Filters selection for saving this Uniform Report.");
                    return false;
                }
                else {
                    return true;
                }
            }



        }
        function rowChange() {
            document.getElementById("<%=hidRowC.ClientID%>").value = "Y";
        }
        function colChange() {
            document.getElementById("<%=hidColumnC.ClientID%>").value = "Y";
        }
        function filterChange() {
            document.getElementById("<%=hidFilterC.ClientID%>").value = "Y";
        }
    </script>
    <script type="text/javascript">
        javascript: window.history.forward(1);
        function ShowPopWindow1(Page) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 880;
            var height = 500;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);

        }
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

        function checkUint(colSeq) {
            // alert(colSeq);
            if (document.getElementById("hidReportType").value == 'UNIFORM') {

                var str = document.getElementById("Column_" + colSeq).innerText;
                if (str.indexOf('CAGR') != -1) {
                    document.getElementById("lbl_" + colSeq).innerText = '(%)';
                }
                else {
                    var txt = document.getElementById("hidUnitShort").value;
                    //var txt = e.options[e.selectedIndex].value; 
                    document.getElementById("lbl_" + colSeq).innerText = txt;
                }


            }

        }

        function checkNumeric() {
            if (checkNumeric1('txtRw') == true) {
                if (checkNumeric1('txtCol') == true)
                    if (checkNumeric1('txtFilter') == true)
                        return true;
            }
            return false;


        }
        function checkNumeric1(id) {
            var IsNumber = true;
            var ValidChars = "0123456789";
            var length;
            var rownum = document.getElementById(id).value;
            var i;
            {
                for (i = 0; i < rownum.length; i++) {
                    Char = rownum.charAt(i);
                    if (ValidChars.indexOf(Char) == -1) {
                        IsNumber = false;
                        break;
                    }
                }
                if (IsNumber == true) {
                    return true;
                }
                else {
                    alert("Please enter valid number");
                    return false;
                }
            }
        }

        function ShowPopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  

            newwin = window.open(Page, 'Chat', "");
            return false;

        }

        function alertMessage() {
            alert('Please select Region/Country with Product & Package from Filters, Choose more than 1 filter to proceed');
        }

    </script>
    <script type="text/javascript">


        function Message(Case, Flag, Type) {

            if (Flag == 'CC') {
                var Case1 = document.getElementById(Case)

                if (Case1.value == "0") {
                    alert("Please select " + Type + " Report");
                    return false;
                }
                else
                    var msg = "You are going to create a copy of " + Type + " Report#" + Case1.value + ". Do you want to proceed?"
            }
            if (Flag == 'NC') {
                var msg = "You are going to create a new report. Do you want to proceed?"
            }

            if (Flag == 'DC') {
                var Case1 = document.getElementById(Case)
                if (Case1.value == "0") {
                    alert("Please select " + Type + " Report");
                    return false;
                }
                else
                    msg = "You are going to delete Report#" + Case1.value + ". Do you want to proceed?"
            }

            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }

        }
        function Validation(type) {
            if (type = 'Prop') {
                var repText = document.getElementById("lnkReports").innerText;
                document.getElementById("hidPropRptDes").value = repText;
            }
            else if (type = 'Base') {
                var repText = document.getElementById("lnkBReports").innerText;
                document.getElementById("hidBaseRptDes").value = repText;
            }
            else if (type = 'PropRen') {
                var repText = document.getElementById("lnkReports").innerText;
                document.getElementById("hidPropRptDes").value = repText;
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
        <%--<div id="AlliedLogo">
                <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif"
                    runat="server" />
            </div>--%>
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
        <div>
            <table class="M1Module" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="Button1" runat="server" ImageUrl="~/Images/GlobalManager.gif"
                                        Text="Global Manager" CssClass="ButtonWMarigin" PostBackUrl="~/Pages/Market1Sub/Default.aspx"
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
                    <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('Market - Tools')"
                        onmouseout="UnTip()" style="width: 840px;">
                        Market Analysis - Tools
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left">
                            <br />
                            <div>
                                <table>
                                    <tr>
                                        <td>
                                            <div id="Div1" runat="server" style="margin: 0px 5px 5px 5px;">
                                                <asp:HyperLink ID="lnkGroupTool" runat="server" Text="Manage Groups" Visible="False"
                                                    CssClass="Link" Target="_blank" Font-Bold="true" NavigateUrl="~/Pages/Market1Sub/Tools/ManageGroups.aspx">
                                                </asp:HyperLink>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="div" runat="server" visible="true">
                                <table width="820px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Source Base Reports
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <%-- <asp:DropDownList ID="ddlBCases" CssClass="DropDown" Width="96%" runat="server">
                                        </asp:DropDownList>--%>
                                            <asp:LinkButton ID="lnkBReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                                Width="300px" OnClientClick="return ShowPopup('../PopUp/ReportDetails.aspx?Des=lnkBReports&Id=hidBaseRpt&Type=Base&GrpId=0');">Display Base Report List</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Button ID="btnCopyBcase" runat="server" Text="Copy" CssClass="ButtonWMarigin"
                                                OnClientClick="return Message('hidBaseRpt','CC','Base');" Style="margin-left: 10px;
                                                width: 90px" onmouseover="Tip('Create a Copy of this Report')" onmouseout="UnTip()" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <table width="820px">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;">
                                            Source Proprietary Reports
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <%-- <asp:DropDownList ID="ddlPCases" CssClass="DropDown" Width="600px" runat="server">
                                                </asp:DropDownList>--%>
                                            <asp:LinkButton ID="lnkReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                                Width="300px" OnClientClick="return ShowPopup('../PopUp/ReportDetails.aspx?Des=lnkReports&Id=hidPropRpt&Type=Prop&GrpId=0');">Display Proprietary Report List</asp:LinkButton>
                                            <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                Text="Currently you have no Proprietary Report defined. You can create a Report in the Toolbox."></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" style="height: 30px">
                                        <td>
                                            <asp:Button ID="btnCopy" runat="server" Text="Copy" CssClass="ButtonWMarigin" OnClientClick="return Message('hidPropRpt','CC','Proprietary');"
                                                onmouseover="Tip('Create a copy this Report')" onmouseout="UnTip()" Style="margin-left: 10px;
                                                height: 25px; width:90px; margin-right: 0px;" CausesValidation="false" />
                                            <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="ButtonWMarigin" onmouseover="Tip('Edit this Report')"
                                                onmouseout="UnTip()" Style="margin-left: 10px; height: 25px; width:90px; margin-right: 0px;"
                                                OnClientClick="return Validation('Prop')" CausesValidation="false" />
                                            <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                                OnClientClick="return Message('hidPropRpt','DC','Proprietary');" onmouseover="Tip('Delete this Report')"
                                                onmouseout="UnTip()" Style="margin-left: 10px; height: 25px; width:90px; margin-right: 0px;"
                                                CausesValidation="false" />
                                            <asp:Button ID="btnCreate" runat="server" Text="Mixed Report Tool" CssClass="ButtonWMarigin"
                                                onmouseover="Tip('Mixed Report Tool')" onmouseout="UnTip()" Style="margin-left: 10px;
                                                height: 25px; width: 130px; margin-right: 0px;" CausesValidation="false" />
                                            <asp:Button ID="btnCreateSpecialReport" runat="server" Text="Uniform Report Tool"
                                                CssClass="ButtonWMarigin" onmouseover="Tip('Uniform Report Tool')" onmouseout="UnTip()"
                                                Style="margin-left: 10px; height: 25px; width: 130px; margin-right: 0px;" CausesValidation="false" />
                                                <asp:Button ID="btnPivotReport" runat="server" Text="Pivot Report Tool"
                                                    CssClass="ButtonWMarigin" onmouseover="Tip('Pivot Report Tool')" onmouseout="UnTip()"
                                                    Style="margin-left: 10px; height: 25px; width: 110px; margin-right: 0px;" CausesValidation="false" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <br />
                            <div id="divPivot" style="margin-top: 0px; margin-left: 2px;" runat="server"
                                    visible="false">
                                    <table style="width: 820px;">
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">Pivot Report Details
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td align="right" width="20%">Report Name:
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtPivotReportName" runat="server" value="Test Report" CssClass="SmallTextBox"
                                                    Style="text-align: left; width: 280px;" MaxLength="100"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPivotReportName"
                                                    ErrorMessage="*"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2">
                                            <td align="right">Report Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPivotType" CssClass="DropDown" Width="250px" runat="server"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="Create Report By RegionSet" Value="REGION"></asp:ListItem>
                                                    <asp:ListItem Text="Create Report By Country" Value="CNTRY"></asp:ListItem>
                                                    <%-- <asp:ListItem Text="Create Report By Package Group" Value="PACKGRP"></asp:ListItem>--%>
                                                    <asp:ListItem Text="Create Report By Product" Value="PROD"></asp:ListItem>
                                                    <asp:ListItem Text="Create Report By Material " Value="MAT"></asp:ListItem>
                                                    <asp:ListItem Text="Create Report By Package" Value="PACK"></asp:ListItem>
                                                    <asp:ListItem Text="Create Report By Group" Value="GROUP"></asp:ListItem>
                                                    <asp:ListItem Text="Create Report By Component" Value="COMPONENT"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" id="Pivotpackgroup" visible="false" runat="server">
                                            <td align="right">Select Package Group:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPivotPackType" CssClass="DropDown" Width="150px" runat="server"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" id="Pivotregionset" visible="true" runat="server">
                                            <td align="right">Select RegionSet:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPivotRegionSet" CssClass="DropDown" Width="250px" runat="server"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                                &nbsp;&nbsp;&nbsp;
                                            <asp:HyperLink ID="HyperLink1" runat="server">RegionSet Details</asp:HyperLink>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1" id="PivotrowRegions" runat="server" visible="false">
                                            <td align="right">Select Region:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPivotRegion" CssClass="DropDown" Width="150px" AutoPostBack="true"
                                                    runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2" id="PivotprodGroup" visible="false" runat="server">
                                            <td align="right">Select Product Group:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPivotProdGrp" CssClass="DropDown" Width="150px" runat="server"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td align="right">Select Unit:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlPivotUnit" CssClass="DropDown" Width="100px" runat="server">
                                                </asp:DropDownList>
                                                <span style="padding-left: 30px;">Report Columns:</span>
                                                <asp:TextBox ID="txtPivotCols" runat="server" value="1" CssClass="SmallTextBox"
                                                    Style="text-align: right;" MaxLength="5"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtSpecialCols"
                                                    runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                <span style="padding-left: 30px;">Report Filters:</span>

                                                <asp:DropDownList ID="ddlPivotFilters" CssClass="DropDown" Width="50px" runat="server"
                                                    AutoPostBack="true">
                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor2">
                                            <td align="right"></td>
                                            <td>
                                                <asp:Button ID="btnPivotSubmit" runat="server" CssClass="Button" Text="Submit"
                                                    Style="margin: 0 0 0 0;" />
                                                <%--      <asp:Button ID="btnSpecialSubmit2" runat="server" CssClass="Button" Text="Submit"
                                                Style="margin: 0 0 0 0; height: 26px;" Visible="false" />--%>
                                            &nbsp;<asp:Button ID="btnPivotCancel" runat="server" CssClass="Button" Text="Cancel"
                                                Style="margin: 0 0 0 0;" CausesValidation="false" />
                                                &nbsp;
                                            <asp:Button ID="btnPivotSave" runat="server" CssClass="Button" Text="Save" Width="60px"
                                                ToolTip="Validate and Save" Style="margin: 0 0 0 0;" OnClientClick="return ValidateData();" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            <br />
                            <div id="divCreateSpecial" style="margin-top: 0px; margin-left: 2px;" runat="server"
                                visible="false">
                                <table style="width: 820px;">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            Special Report Details
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right" width="20%">
                                            Report Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtSpecialReportName" runat="server" value="Test Report" CssClass="SmallTextBox"
                                                Style="text-align: left; width: 280px;" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtSpecialReportName"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td align="right">
                                            Report Type:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlSpecialType" CssClass="DropDown" Width="250px" runat="server"
                                                AutoPostBack="true">
                                                <asp:ListItem Text="Create Report By RegionSet" Value="REGION"></asp:ListItem>
                                                <asp:ListItem Text="Create Report By Country" Value="CNTRY"></asp:ListItem>
                                                <%-- <asp:ListItem Text="Create Report By Package Group" Value="PACKGRP"></asp:ListItem>--%>
                                                <asp:ListItem Text="Create Report By Product" Value="PROD"></asp:ListItem>
                                                <asp:ListItem Text="Create Report By Material " Value="MAT"></asp:ListItem>
                                                <asp:ListItem Text="Create Report By Package" Value="PACK"></asp:ListItem>
                                                <asp:ListItem Text="Create Report By Group" Value="GROUP"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" id="packgroup" visible="false" runat="server">
                                        <td align="right">
                                            Select Package Group:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlPackageGrp" CssClass="DropDown" Width="150px" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" id="regionset" visible="true" runat="server">
                                        <td align="right">
                                            Select RegionSet:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegionSet" CssClass="DropDown" Width="250px" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:HyperLink ID="lnkRigiondetails" runat="server">RegionSet Details</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1" id="rowRegions" runat="server" visible="false">
                                        <td align="right">
                                            Select Region:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlRegions" CssClass="DropDown" Width="150px" AutoPostBack="true"
                                                runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" id="prodGroup" visible="false" runat="server">
                                        <td align="right">
                                            Select Product Group:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlProdGrp" CssClass="DropDown" Width="150px" runat="server"
                                                AutoPostBack="true">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Select Unit:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlUnits" CssClass="DropDown" Width="100px" runat="server">
                                            </asp:DropDownList>
                                            <span style="padding-left: 30px;">Report Columns:</span>
                                            <asp:TextBox ID="txtSpecialCols" runat="server" value="1" CssClass="SmallTextBox"
                                                Style="text-align: right;" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ControlToValidate="txtSpecialCols"
                                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <span style="padding-left: 30px;">Report Filters:</span>
                                            <asp:TextBox ID="txtSpecialFilters" runat="server" value="1" CssClass="SmallTextBox"
                                                Style="text-align: right;" MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" ControlToValidate="txtSpecialFilters"
                                                runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2">
                                        <td align="right">
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSpecialSubmit" runat="server" CssClass="Button" Text="Submit"
                                                Style="margin: 0 0 0 0;" />
                                            <%--      <asp:Button ID="btnSpecialSubmit2" runat="server" CssClass="Button" Text="Submit"
                                                Style="margin: 0 0 0 0; height: 26px;" Visible="false" />--%>
                                            &nbsp;<asp:Button ID="btnSpecialCancel" runat="server" CssClass="Button" Text="Cancel"
                                                Style="margin: 0 0 0 0;" CausesValidation="false" />
                                            &nbsp;
                                            <asp:Button ID="btnSaveUni" runat="server" CssClass="Button" Text="Save" Width="60px"
                                                ToolTip="Validate and Save" Style="margin: 0 0 0 0;" OnClientClick="return ValidateData();" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divCreate" style="margin-top: 0px; margin-left: 2px;" runat="server" visible="false">
                                <table style="width: 820px;">
                                    <tr class="AlterNateColor4">
                                        <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                            Mixed Report Details
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                            Report Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRptName" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                                width: 280px;" MaxLength="100"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtRep" runat="server" ControlToValidate="txtRptName"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor2" id="rowRows" runat="server" visible="true">
                                        <td align="right">
                                            Report Rows:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRw" runat="server" value="1" CssClass="SmallTextBox" Style="text-align: left;"
                                                MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtRw" runat="server" ControlToValidate="txtRw"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <span style="padding-left: 30px;">Report Columns: </span>
                                            <asp:TextBox ID="txtCol" runat="server" value="1" CssClass="SmallTextBox" Style="text-align: left;"
                                                MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtCol" ControlToValidate="txtCol" runat="server"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                            <span style="padding-left: 30px;">Report Filters: </span>
                                            <asp:TextBox ID="txtFilter" runat="server" value="1" CssClass="SmallTextBox" Style="text-align: left;"
                                                MaxLength="5"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="reqtxtFilter" ControlToValidate="txtFilter" runat="server"
                                                ErrorMessage="*"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td align="right">
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSubmitt" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;" />
                                            &nbsp;
                                            <%--   <asp:Button ID="btnSubmitt2" runat="server" CssClass="Button" Text="Submit" Style="margin: 0 0 0 0;"
                                                Visible="false" />--%>
                                            &nbsp;<asp:Button ID="btnCancle" runat="server" CssClass="Button" Text="Cancel" Style="margin: 0 0 0 0;"
                                                CausesValidation="false" />
                                            &nbsp;
                                            <asp:Button ID="btnSaveMixed" runat="server" CssClass="Button" Text="Save" Width="60px"
                                                ToolTip="Validate and Save" Style="margin: 0 0 0 0; height: 26px;" OnClientClick="return ValidateData();" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                            </div>
                            <div id="divReportFrameWork" style="margin-top: 0px; margin-left: 4px;" runat="server"
                                visible="true">
                                <asp:Table ID="tblCAGR" runat="server" CellPadding="0" CellSpacing="1">
                                </asp:Table>
                                <br />
                                <asp:Button ID="btnAddNew" runat="server" CssClass="Button" Text="Save" Style="margin: 0 0 0 0;"
                                    Visible="false" />
                            </div>
                            <br />
                        </div>
                    </div>
                </td>
            </tr>
            <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidBaseRpt" runat="server" />
        <asp:HiddenField ID="hidBaseRptDes" runat="server" />
        <asp:HiddenField ID="hidPropRpt" runat="server" />
        <asp:HiddenField ID="hidPropRptDes" runat="server" />
        <asp:HiddenField ID="hidReportType" runat="server" />
        <asp:HiddenField ID="hidUnitShort" runat="server" />
        <asp:HiddenField ID="hidRowC" runat="server" />
        <asp:HiddenField ID="hidColumnC" runat="server" />
        <asp:HiddenField ID="hidFilterC" runat="server" />
          <asp:HiddenField ID="hidRptType" runat="server" />
    </div>
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
