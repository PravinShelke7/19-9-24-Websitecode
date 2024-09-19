<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title> Chart</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 640;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var grpId = document.getElementById("<%=hidchartgrp.ClientID %>").value;

            pn = Page + "&GrpId=" + grpId + " ";
            newwin = window.open(pn, 'Chat', params);
            return false;
        }


        function MakeVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            GetCaseDe();
            return false;
        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;
        }


        function trimAll(sString) {
            while (sString.substring(0, 1) == ' ') {
                sString = sString.substring(1, sString.length);
            }
            while (sString.substring(sString.length - 1, sString.length) == ' ') {
                sString = sString.substring(0, sString.length - 1);
            }
            return sString;
        }

        function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, '_blank');
        }
        function ShowPopup(Page, Type) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1165;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';

            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
        function openWindowU(page) {
            window.open(page);
            return false;
        }


        function ShowToolWindow(Page) {
            newwin = window.open(Page);
            return false;
        }


        function CheckSP(text) {
            var a = /\<|\>|\&#|\\/;
            if (document.getElementById(text.id).value.match(a) != null) {
                var object = document.getElementById(text.id)  //get your object
                if (text.id == "txtRptName") {
                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Report Name. Please choose alternative characters.");
                }

                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }

        //        function ClosePage() {

        //            window.opener.document.getElementById('btnRefresh').click();
        //            window.close();
        //        }
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
    <style type="text/css">
        .style1
        {
            height: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
        <div>
            <asp:Image ImageAlign="AbsMiddle" Width="1200px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                    Chart
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                        <div class="PageHeading" style="width: 80%">
                            <center>
                                Select from Existing Chart Reports
                            </center>
                        </div>
                        
                        <table width="80%">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Select from Available Chart Reports
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td id="tdPropCases" runat="server">
                                    <asp:LinkButton ID="lnkCReports" Style="font-size: 14px" runat="server" CssClass="Link"
                                        Width="300px" OnClientClick="return ShowPopWindow('PopUp/ChartDetails.aspx?Des=lnkCReports&Id=hidCRptId&Des1=hidCRptDes&Des2=hidchartgrp&type=hidtype');">Display Chart Report List</asp:LinkButton>
                                </td>
                            </tr>
                            <%--  <tr class="AlterNateColor1">
                                    <td>                                        
                                        <asp:Label ID="lblPCase" runat="server" CssClass="CalculatedFeilds" Visible="false" Text="Currently you have no Proprietary Report to display. You can create a Report with the Tools below."></asp:Label>
                                    </td>
                                 </tr> --%>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnPCase" runat="server" CssClass="Button" Text="Start a Proprietary Report"
                                        Style="margin-left: 0px;" />
                                    <asp:Button ID="btRename" runat="server" Text="Rename" CssClass="Button" Enabled="true">
                                    </asp:Button>
                                    <asp:Label ID="lblCReport" runat="server" CssClass="CalculatedFeilds" Font-Bold="true"
                                        Visible="false" Text="Currently you have no Chart Reports defined. You can create Reports in the ToolBox."></asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <div style="margin-left: 20px;" id="divRename" runat="server" visible="false">
                                        <table width="80%" style="padding-left: 20px">
                                            <tr align="left">
                                                <td>
                                                    <b>Report Name:</b>
                                                    <asp:HiddenField ID="hypReportId" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRptName" runat="server" CssClass="SingleLineTextBox" Style="text-align: left;
                                                        width: 200px" MaxLength="25" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="Button" Style="margin-left: 0px;">
                                                    </asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="Button" Style="margin-left: 0px;">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <div class="PageHeading" style="width: 80%">
                            <center>
                                Report Creation And Modification Tools</center>
                        </div>
                        <table width="80%">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Toolbox
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnToolBox" runat="server" CssClass="Button" Text="Go To Toolbox"
                                        OnClientClick="return ShowToolWindow('Tools/CreateChart.aspx');" Style="margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td class="style1">
                                </td>
                            </tr>
                        </table>
                        <br />
                        <asp:HiddenField ID="hidCRptId" runat="server" />
                        <asp:HiddenField ID="hidCRptDes" runat="server" />
                        <asp:HiddenField ID="hidchartgrp" runat="server" />
                        <asp:HiddenField ID="hidChartGrpDes" runat="server" />
                        <asp:HiddenField ID="hidRFPId" runat="server" />
                         <asp:HiddenField ID="hidtype" runat="server" />

                         <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
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
    </form>
</body>
</html>
