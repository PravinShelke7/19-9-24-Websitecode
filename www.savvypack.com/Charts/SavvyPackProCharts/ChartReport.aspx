<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ChartReport.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_ChartReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title> Price Chart </title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="stylesheet" href="../../App_Themes/SkinFile/AlliedNew.css" />
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

        function ShowPopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1250;
            var height = 700;
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
            return false;
        }
        function ShowPricePopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 270;
            var height = 230;
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
            return false;
        }
        function ShowPriceColumnPopup(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 300;
            var height = 150;
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
            return false;
        }

        function MakeVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;
        }
        function MakeGraphVisible(id, GId) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            objItemElement1 = document.getElementById(GId)
            objItemElement1.style.display = "inline"
            return false;
        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;
        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 400;
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
        function ShowPopWindowTool(Page) {
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
        function Message(Case, Flag) {

            if (Flag == 'Copy') {
                var Case1 = document.getElementById(Case)
                if (Case1.value == "0") {
                    alert("Please select Chart Report");
                    return false;
                }
                else
                    var msg = "You are going to create a copy of Chart Report#" + Case1.value + ". Do you want to proceed?"
            }
            if (Flag == 'Edit') {
                var Case1 = document.getElementById(Case)
                if (Case1.value == "0") {
                    alert("Please select Chart Report");
                    return false;
                }
                else
                    return true;
            }
            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }
        }

        function CheckReport(Report) {
            var Case1 = document.getElementById(Case)
            if (Case1.value == "0") {
                alert("Please select Chart Report");
                return false;
            }
            else {
                return true;
            }
        }
    </script>
    <style type="text/css">
        .style1 {
            width: 15%;
        }
    </style>
</head>
<body style="margin-top: 0px;">
    <form runat="server" id="form1">
    <div id="MasterContent">
        <div>
            <asp:Image ImageAlign="AbsMiddle" Width="1200px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <table class="ContentPage" id="ContentPage" runat="server" style="width: 1400px">
        <tr>
            <td>
                <div id="divMainHeading" runat="server" style="width: 840px; font-size: 15px; font-family: Verdana">
                    <b>Chart For: </b>
                    <asp:Label ID="mat" runat="server"></asp:Label>
                </div>
            </td>
        </tr>
        <tr style="width: 100%; font-size: 13px; font-family: Verdana; height: 30px;">
            <%--<td colspan="1" style="height: 22px; width: 18%; color: black; font-size: 13px; font-family: Verdana;"
                align="left">
                
            </td>--%>
            <td>
                <%--<asp:Label ID="lblN" Text="Chart's Name:" Font-Bold="true" runat="server"></asp:Label>
                <asp:Label ID="lblName" Font-Bold="true" runat="server"></asp:Label>--%>
                <%--<asp:Label ID="lblyear" runat="server" Style="padding-left: 500px;"></asp:Label>--%>
            </td>
        </tr>
        <tr>
            <td>
                <div id="PageSection1" style="text-align: center">
                    <table>
                        <tr>
                            <td>
                            <asp:Label ID="lblHeadingS" runat="server"></asp:Label>
                                <div id="MaterialPrice" runat="server">
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </td>
        </tr>
    </table>
          
        <asp:HiddenField ID="hidCRptId" runat="server" />
        <asp:HiddenField ID="hidCRptDes" runat="server" />
        <asp:HiddenField ID="hidCount" runat="server" />
        <asp:HiddenField ID="hidCCnt" runat="server" />
        <asp:HiddenField ID="hidchartgrp" runat="server" />
        <asp:HiddenField ID="hidtype" runat="server" />

       
    </form>
</body>
</html>
