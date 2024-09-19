<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateChart.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_Tool_CreateChart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
   <title> Price Chart </title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <link rel="stylesheet" href="../../../App_Themes/SkinFile/AlliedNew.css" />
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
           if (document.getElementById("<%=hidPriceId.ClientID %>").value == "") {
               alert("Please Select Dataset first.");
               return false;
           }
           var width = 650;
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
           var grpId = document.getElementById("<%=hidPriceId.ClientID %>").value;

           pn = Page + "&RfpPriceID=" + grpId + " ";
           newwin = window.open(pn, 'Chat', params);
           //newwin = window.open(Page, 'Chat', params);
           return false;
       }
       function ShowPopupBatch(Page) {
           if (document.getElementById("<%=hidPriceId.ClientID %>").value == "") {
               alert("Please Select Dataset first.");
               return false;
           }
           //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
           var width = 700;
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
           var grpId = document.getElementById("<%=hidPriceId.ClientID %>").value;

           pn = Page + "&RfpPriceID=" + grpId + " ";
           newwin = window.open(pn, 'Chat', params);
           //newwin = window.open(Page, 'Chat', params);
           return false;
       }
       function ShowPricePopup(Page) {
           //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
           var width = 300;
           var height = 260;
           var left = (screen.width - width) / 2;
           var top = (screen.height - height) / 2;
           var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
           params += ', location=no';
           params += ', menubar=no';
           params += ', resizable=no';
           params += ', scrollbars=yes';
           params += ', status=yes';
           params += ', toolbar=no';

           var grpId = document.getElementById("<%=hidPriceId.ClientID %>").value;

           pn = Page + "&RfpPriceID=" + grpId + " ";

           newwin = window.open(pn, 'Chat', params);
           return false;
       }
       function ShowPriceColumnPopup(Page) {
           //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
           if (document.getElementById("<%=hidPriceId.ClientID %>").value == "") {
               alert("Please Select Dataset first.");
               return false;
           }
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
           var grpId = document.getElementById("<%=hidPriceId.ClientID %>").value;

           pn = Page + "&RfpPriceID=" + grpId + " ";
           newwin = window.open(pn, 'Chat', params);
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
       //graph
       function Validatelink() {
           var price = document.getElementById("lnkprice").innerText;
           var vendor = document.getElementById("lnkvendor").innerText;
           var group = document.getElementById("lnkGroup").innerText;
           var pricecol = document.getElementById("lnkPriceCol").innerText;
           var msg = "Please"
           if (price == 'Select Price') {
               msg += " Select Price \n";
           }
           if (vendor == 'Select Vendor') {
               msg += " Select Vendor \n";
           }
           if (group == 'Select Group') {
               msg += " Select Group \n";
           }
           if (pricecol == 'Select Price column') {
               msg += " Select Price column";
           }
           if (msg == 'Please') {
               return true;
           }
           else {
               alert(msg);
               return false;
           }
       }
       function ValidatelinkSave() {
           var txtchart = document.getElementById("TxtChartN").VALUE;
           var price = document.getElementById("lnkprice").innerText;
           var vendor = document.getElementById("lnkvendor").innerText;
           var group = document.getElementById("lnkGroup").innerText;
           var pricecol = document.getElementById("lnkPriceCol").innerText;
           var msg = "Please"
           if (txtchart == '') {
               msg += " enter Chartname \n";
           }
           if (price == 'Select Price') {
               msg += " select price \n";
           }
           if (vendor == 'Select Vendor') {
               msg += " select vendor \n";
           }
           if (group == 'Select Group') {
               msg += " select group \n";
           }
           if (pricecol == 'Select Price column') {
               msg += " select price column";
           }
           if (msg == 'Please') {
               return true;
           }
           else {
               alert(msg);
               return false;
           }
       }
       //line graph
       function ValidatelinkLine() {
           var price = document.getElementById("lnkpriceLine").innerText;
           var vendor = document.getElementById("lnkvendorLine").innerText;
           var pricecol = document.getElementById("lnkPriceColumnLine").innerText;
           var msg = "Please"
           if (price == 'Select Price') {
               msg += " Select Price \n";
           }
           if (vendor == 'Select Vendor') {
               msg += " Select Vendor \n";
           }
           if (pricecol == 'Select Price column') {
               msg += " Select Price column";
           }
           if (msg == 'Please') {
               return true;
           }
           else {
               alert(msg);
               return false;
           }
       }
       function ValidatelinkSaveLine() {

           var txtchart = document.getElementById("txtLineChart").VALUE;
           var price = document.getElementById("lnkpriceLine").innerText;
           var vendor = document.getElementById("lnkvendorLine").innerText;
           var pricecol = document.getElementById("lnkPriceColLine").innerText;
           var msg = "Please"
           if (txtchart == '') {
               msg += " enter Chartname \n";
           }
           if (price == 'Select Price') {
               msg += " select price \n";
           }
           if (vendor == 'Select Vendor') {
               msg += " select vendor \n";
           }
           if (pricecol == 'Select Price column') {
               msg += " select price column";
           }
           if (msg == 'Please') {
               return true;
           }
           else {
               alert(msg);
               return false;
           }
       }

       //Batch Graph
       function ValidatelinkBatch() {
           var price = document.getElementById("lnkPriceSelectionB").innerText;
           var vendor = document.getElementById("lnkVendorB").innerText;
           var group = document.getElementById("lnkGroupB").innerText;
           var msg = "Please"
           if (price == 'Select Price') {
               msg += " Select Price \n";
           }
           if (vendor == 'Select Vendor') {
               msg += " Select Vendor \n";
           }
           if (group == 'Select Group') {
               msg += " Select Group \n";
           }
           if (msg == 'Please') {
               return true;
           }
           else {
               alert(msg);
               return false;
           }
       }
       function ValidatelinkSaveBatch() {
           var txtchart = document.getElementById("txtcn").VALUE;
           var price = document.getElementById("lnkPriceSelectionB").innerText;
           var vendor = document.getElementById("lnkVendorB").innerText;
           var group = document.getElementById("lnkGroupB").innerText;
           var msg = "Please"
           if (txtchart == '') {
               msg += " enter Chartname \n";
           }
           if (price == 'Select Price') {
               msg += " select price \n";
           }
           if (vendor == 'Select Vendor') {
               msg += " select vendor \n";
           }
           if (group == 'Select Group') {
               msg += " select group \n";
           }
           if (msg == 'Please') {
               return true;
           }
           else {
               alert(msg);
               return false;
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
    <form runat="server" id="form1" action="CreateChart.aspx">
        <div id="MasterContent">          
            <div>
                <asp:Image ImageAlign="AbsMiddle" Width="1200px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
            </div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server" style="width: 1200px">
            <tr>
                <td>
                    <div id="PageSection1" runat="server" style="text-align: left; width: 1200px;">
                        <table>
                            <tr>
                                <td>
                                    <table width="1200px">
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;">Select from Available Chart Reports
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td id="tdPropCases" runat="server">
                                                <asp:LinkButton ID="lnkCReports" Style="font-size: 14px; margin-left: 10px;" runat="server"
                                                    CssClass="Link" Width="300px" OnClientClick="return ShowPopWindowTool('../PopUp/ChartDetails.aspx?Des=lnkCReports&Id=hidCRptId&Des1=hidCRptDes&Des2=hidchartgrp&type=hidtype');">Display Chart Report List</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                                <asp:Button ID="btnCopy" runat="server" CssClass="Button" Text="Copy" Style="margin-left: 50px;"
                                                    OnClientClick="return Message('hidCRptId','Copy');" />
                                                <asp:Button ID="btCEdit" runat="server" Text="Edit" CssClass="Button" Style="margin-left: 50px;"
                                                    OnClientClick="return Message('hidCRptId','Edit');"></asp:Button>
                                                <asp:Button ID="btnCreate" runat="server" Text="Create Bar/Pie Chart" CssClass="Button" OnClientClick="return MakeVisible('divM');"
                                                    Style="margin-left: 50px;"></asp:Button>
                                                <asp:Button ID="btnCreateLine" runat="server" Text="Create Line chart" CssClass="Button" OnClientClick="return MakeVisible('divML');"
                                                    Style="margin-left: 50px;"></asp:Button>
                                                <asp:Button ID="btnlinegrp" runat="server" Visible="false"  Text="Create Line chart for Batches" CssClass="Button" OnClientClick="return MakeVisible('divMLG');"
                                                    Style="margin-left: 50px;"></asp:Button>
                                            
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="divMainHeading" runat="server" style="width: 500px; font-size: 15px; font-family: Verdana">
                                         <table style="width: 710px">
                                            <tr>
                                                <td align="center" style="height: 50px;">
                                                    <asp:Label ID="lblHeading" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="18px">                
                                                    </asp:Label>
                                                    <br />
                                                    <asp:Label ID="lblHeadingS" runat="server" ForeColor="Black" Font-Bold="true" Font-Size="14px">                
                                                    </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
            
                                        <table style="width:710px">
                                            <tr>
                                                <td>
                                                     <div id="ChartComp" runat="server">
                                                     </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div id="MaterialPrice" runat="server">
                                    </div>
                                </td>
                            </tr>
                            <tr style="height: 20px">
                                <td>
                                    <div id="divM" runat="server" style="display: none;">
                                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: Black; width: 100%; color: White;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblN" Text="Enter Chart's Name:" Font-Bold="true" runat="server"></asp:Label>
                                                    <asp:TextBox ID="TxtChartN" MaxLength="100" Style="background-color: #FEFCA1;" Width="200px"
                                                        runat="server" Text ="TestChart"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" cellpadding="3" cellspacing="3" style="width: 1000px">
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Dataset Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkprice" runat="server" OnClientClick="return ShowPricePopup('../Popup/PriceSelection.aspx?Des=lnkprice&Id=hidPriceId&Des1=hidPriceDes');"
                                                                    Style="color: White;">Select Dataset</asp:LinkButton>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Vendor Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkvendor" runat="server" OnClientClick="return ShowPopup('../Popup/VendorSelection.aspx?Des=lnkvendorLine&Id=hidvendorid&Des1=hidvendordes');"
                                                                    Style="color: White;">Select Vendor</asp:LinkButton>
                                                            </td>
                                                         
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Group Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkGroup" runat="server" OnClientClick="return ShowPopup('../Popup/GroupSelection.aspx?Des=lnkGroup&Id=hidGroupId&Des1=hidGroupDes');"
                                                                    Style="color: White;">Select Group</asp:LinkButton>
                                                            </td>
                                                         </tr>
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Price Column Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkPriceCol" runat="server" OnClientClick="return ShowPriceColumnPopup('../Popup/PriceColumnSelection.aspx?Des=lnkPriceCol&Id=hidPriceColID&Des1=hidPriceColDes');"
                                                                    Style="color: White;">Select Price column</asp:LinkButton>
                                                            </td>
                                                         </tr>
                                                         
                                                         <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Chart Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                            <asp:DropDownList ID="ddlChartType" runat="server" CssClass="DropDown" Width="200"  >
                                                                <asp:ListItem Text="Regular Bar Chart" Value="RBC"></asp:ListItem>
                                                                <asp:ListItem Text="Pie Chart" Value="PIE"></asp:ListItem>                                           
                                                            </asp:DropDownList>
                                                            </td>
                                                         </tr>
                                                        <tr>
                                                            <td colspan="6" style="text-align: left;">
                                                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclientclick="return Validatelink();" />
                                                                <asp:Button ID="btnCancel" Style="margin-left: 30px;" runat="server" Text="Cancel"
                                                                    Width="61px" />
                                                                <asp:Button ID="btnSave" Style="margin-left: 30px;" runat="server" Text="Save" Width="61px" onclientclick="return ValidatelinkSave();" />
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    <input name="Chart" type="hidden" value="1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>


                              <tr style="height: 20px">
                                <td>
                                    <div id="divML" runat="server" style="display: none;">
                                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: Black; width: 100%; color: White;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" Text="Enter Chart's Name:" Font-Bold="true" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtLineChart" MaxLength="100" Style="background-color: #FEFCA1;" Width="200px"
                                                        runat="server" Text ="TestChart"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" cellpadding="3" cellspacing="3" style="width: 1000px">
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Dataset Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkpriceLine" runat="server" OnClientClick="return ShowPricePopup('../PopUp/PriceSelectionForLine.aspx?Des=lnkpriceLine&Id=hidPriceId&Des1=hidPriceDes');"
                                                                    Style="color: White;">Select Dataset</asp:LinkButton>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Vendor Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkvendorLine" runat="server" OnClientClick="return ShowPopup('../Popup/VendorSelection.aspx?Des=lnkvendorLine&Id=hidvendorid&Des1=hidvendordes');"
                                                                    Style="color: White;">Select Vendor</asp:LinkButton>
                                                            </td>
                                                         
                                                        </tr>
                                                      
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Price Column Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkPriceColumnLine" runat="server" OnClientClick="return ShowPriceColumnPopup('../Popup/PriceColumnSelection.aspx?Des=lnkPriceColumnLine&Id=hidPriceColID&Des1=hidPriceColDes');"
                                                                    Style="color: White;">Select Price column</asp:LinkButton>
                                                            </td>
                                                         </tr>
                                                          <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Set X-Axis:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                              <asp:DropDownList ID="ddlAxis" runat="server" CssClass="DropDown" Width="200"  >
                                                                <asp:ListItem Text="Group" Value="LINEGRP" Selected ="True" ></asp:ListItem>
                                                                <asp:ListItem Text="Vendor" Value="LINEVENDOR"></asp:ListItem>                                           
                                                            </asp:DropDownList>
                                                            </td>
                                                         </tr>
                                                        <tr>                                                         
                                                       
                                                            <td colspan="6" style="text-align: left;">
                                                                <asp:Button ID="btnsubmitLine" runat="server" Text="Submit" onclientclick="return ValidatelinkLine();"/>
                                                                <asp:Button ID="btncancleline" Style="margin-left: 30px;" runat="server" Text="Cancel"
                                                                    Width="61px" />
                                                                <asp:Button ID="btnSaveLine" Style="margin-left: 30px;" runat="server" Text="Save" Width="61px" onclientclick="return ValidatelinkSaveLine();"/>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    <input name="Chart" type="hidden" value="1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                              <tr style="height: 20px">
                              <td>
                               <div id="divMLG" runat="server" style="display: none;">
                                        <table border="0" cellpadding="0" cellspacing="0" style="background-color: Black; width: 100%; color: White;">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblcn" Text="Enter Chart's Name:" Font-Bold="true" runat="server"></asp:Label>
                                                    <asp:TextBox ID="txtcn" MaxLength="100" Style="background-color: #FEFCA1;" Width="200px"
                                                        runat="server" Text ="TestChart"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td valign="top">
                                                    <table border="0" cellpadding="3" cellspacing="3" style="width: 1000px">
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Price Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkPriceSelectionB" runat="server" OnClientClick="return ShowPricePopup('../PopUp/PriceSelectionForBatch.aspx?Des=lnkPriceSelectionB&Id=hidPriceId&Des1=hidPriceDes');"
                                                                    Style="color: White;">Select Price</asp:LinkButton>
                                                            </td>
                                                           
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Vendor Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkVendorB" runat="server" OnClientClick="return ShowPopup('../Popup/VendorSelection.aspx?Des=lnkVendorB&Id=hidvendorid&Des1=hidvendordes');"
                                                                    Style="color: White;">Select Vendor</asp:LinkButton>
                                                            </td>
                                                         
                                                        </tr>
                                                        <tr style="height: 30px">
                                                            <td style="height: 22px; width: 20%; color: White; font-size: 13px; font-family: Verdana;"
                                                                align="left">
                                                                <b>Group Selection:</b>
                                                            </td>
                                                            <td style="width: 70%" valign="middle">
                                                                <asp:LinkButton ID="lnkGroupB" runat="server" OnClientClick="return ShowPopupBatch('../Popup/Groupbatchselection.aspx?Des=lnkGroupB&Id=hidGroupId&Des1=hidGroupDes&Type=Graph');"
                                                                    Style="color: White;">Select Group</asp:LinkButton>
                                                            </td>
                                                         </tr>
                                                      
                                                        <tr>                                                         
                                                       
                                                            <td colspan="6" style="text-align: left;">
                                                                <asp:Button ID="btnsubmitb" runat="server" Text="Submit"  onclientclick="return ValidatelinkBatch();"/>
                                                                <asp:Button ID="btncanelb" Style="margin-left: 30px;" runat="server" Text="Cancel"
                                                                    Width="61px" />
                                                                <asp:Button ID="btnsaveb" Style="margin-left: 30px;" runat="server" Text="Save" Width="61px"  onclientclick="return ValidatelinkSaveBatch();"/>
                                                            </td>
                                                        </tr>
                                                        </table>
                                                    <input name="Chart" type="hidden" value="1" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidvendorid" runat="server" />
        <asp:HiddenField ID="hidvendordes" runat="server" />     
        <asp:HiddenField ID="hidPriceId" runat="server" />
        <asp:HiddenField ID="hidPriceDes" runat="server" />       
        <asp:HiddenField ID="hidPriceColId" runat="server" />
        <asp:HiddenField ID="hidPriceColDes" runat="server" />
        <asp:HiddenField ID="hidGroupId" runat="server" />
        <asp:HiddenField ID="hidGroupDes" runat="server" />
        <asp:HiddenField ID="hidbtnId" runat="server" />
        <asp:HiddenField ID="hidTab1" runat="server" />        
        <asp:HiddenField ID="hidCRptId" runat="server" />
        <asp:HiddenField ID="hidCRptDes" runat="server" />
        <asp:HiddenField ID="hidCount" runat="server" />
        <asp:HiddenField ID="hidCCnt" runat="server" />
        <asp:HiddenField ID="hidchartgrp" runat="server" />
        <asp:HiddenField ID="hidtype" runat="server" />

        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
          <asp:Button ID="btnRefresh1" runat="server" Style="display: none;" />
        <asp:Button ID="btnRefreshGrp" runat="server" Style="display: none;" />

    </form>
</body>
</html>
