<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Comparison.aspx.vb" Inherits="Pages_StructAssist_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Structure Comparison Manager</title>
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
    <script type="text/JavaScript">
        javascript: window.history.forward(1);
        
         function CheckSP(text) 
        {
            var a = /\<|\>|\&#|\\/;
            if ((document.getElementById("txtCompName").value.match(a) != null)) 
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
        
        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 790; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }
         function openWindowU(page) 
            {
                window.open(page);
                return false;
            }
        function ShowPopWindow(Page, Type) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 850;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height;
            params += ', top=' + top + ', left=' + left;
            params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';

            if (Type == "") {
                pn = Page + "&GrpId=0 ";
            }
            else if (Type == "BASE") {
                var grpId = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId + " ";
            }
            else if (Type == "PROP") {
                var grpId1 = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes1 = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId1 + " ";
            }
            newwin = window.open(pn, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;

        }
        function ShowPopup(Page, Type) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 700;
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
            if (Type == "") {
                pn = Page + "&GrpId=0 ";
            }
            else if (Type == "AP") {
                var grpId = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId + " ";
            }
            else if (Type == "PRP") {
                var grpId1 = document.getElementById("<%=hidGrpId.ClientID %>").value;
                var grpDes1 = document.getElementById("<%=hidGrpDes.ClientID %>").value;
                // alert(grpId);
                pn = Page + "&GrpId=" + grpId1 + " ";
            }
            //alert(pn);

            newwin = window.open(pn, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

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
            newwin = window.open(Page, 'NewWindow', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

        }


        function GetCompId() {
            objItemElement = document.getElementById('<%= hidCompID.ClientID%>').value
            if (document.getElementById('<%= txtCompName.ClientID%>').value.split(' ').join('').length == 0) {
                alert("Comparison Name cannot be blank");
                return false;
            }
            else {
                return confirm("Do you want to Rename Comparison " + objItemElement + " ?");
            }
            return false;
        }

        function Validation(type) {
            if (type == 'Prop') {

            }
            else if (type == 'PropRen') {
                var repText = document.getElementById('<%= lnkComparison.ClientID%>').innerText;
                document.getElementById('<%= hidRptDes.ClientID%>').value = repText;
            }
        }

        function MakeVisible1(id) {

            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
            else {
                objItemElement = document.getElementById(id)
                objItemElement.style.display = "inline"
                //                GetCaseDe();
                return false;
            }




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

        function GetCaseDe() {
            var Case = document.getElementById("<%=hidPropCase.ClientID %>").value;
            //alert(document.getElementById("<%=hidPropCaseSt.ClientID %>").value);
            if (Case == "0") {
                alert('Please select Proprietary case');
                return false;
            }
            else {
                var combo1 = document.getElementById("hidPropCase");

                var val = combo1.options[combo1.selectedIndex].text
                var CaseDe = val.split("=");

                document.getElementById("txtCaseDe1").value = trimAll(CaseDe[1].replace("UNIQUE FEATURES", ""));
                document.getElementById("txtCaseDe2").value = trimAll(CaseDe[2]);
            }
        }

        function GetCaseId() {

            objItemElement = document.getElementById("hidPropCase")

            document.getElementById("txtCaseid").value = objItemElement.value




            if (document.getElementById("txtCaseDe1").value.split(' ').join('').length == 0) {
                alert("PACKAGING FORMAT cannot be blank");
                return false;
            }
            else if (document.getElementById("txtCaseDe2").value.split(' ').join('').length == 0) {
                alert("UNIQUE FEATURES cannot be blank");
                return false;
            }
            else {
                return confirm("Do you want to rename Case " + objItemElement.value + " ?");

            }

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

        function Help() {
            var width = 800;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var URL
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            URL = "../Econ1/help/SavvyPackInstructions1.htm"
            newwin = window.open(URL, 'Chart', params);
            return false

        }
        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <center>
    <div id="MasterContent">
        
        <div>
            <table class="SBAModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px; padding-bottom: 15px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                        runat="server" ToolTip="Update" Visible="false" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgLogoff" ImageAlign="Middle" ImageUrl="~/Images/LogOff.gif"
                                        runat="server" ToolTip="Log Off" Visible="false" PostBackUrl="~/Universal_loginN/Pages/ULogOff.aspx?Type=StandAssist" />
                                </td>
                                <td>
                                    <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                        runat="server" ToolTip="Instructions" Visible="false" OnClientClick="return Help();" />
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
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
            <td>
                <div class="PageHeading" id="divMainHeading" style="width: 840px;">
                    Comparison Manager
                </div>
            </td>
        </tr>
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left;">
                        <table cellpadding="0" cellspacing="0" border="0" style="width: 90%; padding-left: 10px;
                            padding-right: 10px; margin-top: 5px;">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px; height: 20px; padding-left: 10px;">
                                    Select from Existing Comparisons
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td id="td3" runat="server">
                                    <asp:LinkButton ID="lnkComparison" Style="font-size: 14px; padding-left: 10px;" runat="server"
                                        CssClass="Link" Width="300px" OnClientClick="return ShowPopup('ComparisonDetails.aspx?Des=lnkComparison&Id=hidCompID&Des1=hidCompDes','');">Select Comparison</asp:LinkButton>
                                    <asp:Label ID="lblCompCase" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                        Text="Currently you have no Comparison Structure to display. You can create a Comparison Structure with the below ToolBox."></asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2" style="height: 30px;">
                                <td>
                                    <asp:Button ID="btnCompStart" runat="server" CssClass="Button" Text="Start a Comparison" ToolTip="Start a Comparison"
                                        Style="margin-left: 0px;" OnClientClick="return Validation('Prop')" />
                                    <asp:Button ID="btnCompRename" runat="server" Text="Rename" CssClass="Button" ToolTip="Rename Comparison" OnClientClick="return Validation('PropRen')"
                                        Enabled="true"></asp:Button>
                                    
                                    <asp:Button ID="btnToolBox" runat="server" ToolTip="Manage Comparisons" CssClass="Button" OnClientClick="return openWindowU('ComparisonTool.aspx') ;"
                                        Text="Manage Comparisons" Style="margin-left:20px;width:140px;" />
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <div style="margin-left: 20px;" id="divCompRename" runat="server" visible="false">
                                        <table width="80%" style="padding-left: 20px">
                                            <tr align="left">
                                                <td>
                                                    <b>Comparison Name:</b>
                                                    <asp:HiddenField ID="hidName" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCompName" onChange="javascript:CheckSP(this);" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                        width: 200px" MaxLength="25"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCUpdate" runat="server" Text="Update" CssClass="Button" Style="margin-left: 0px;"
                                                        OnClientClick="return GetCompId();"></asp:Button>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnCompCancel" runat="server" Text="Cancel" CssClass="Button" Style="margin-left: 0px;">
                                                    </asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table width="90%" style="display:none;">
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;">
                                    Toolbox
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnCompToolBox" runat="server" ToolTip="Manage Comparison Structures"
                                        CssClass="Button" Text="Go To Toolbox" Style="margin-left: 0px;" />
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                </td>
                            </tr>
                        </table>
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
       </center> 
    <asp:HiddenField ID="txtCaseid" runat="server" />
    <asp:HiddenField ID="hidApprovedCase" runat="server" />
    <asp:HiddenField ID="hidPropCase" runat="server" />
    <asp:HiddenField ID="hidApprovedCaseD" runat="server" />
    <asp:HiddenField ID="hidSpons" runat="server" />
    <asp:HiddenField ID="hidPropCaseD" runat="server" />
    <asp:HiddenField ID="hidPropCaseSt" runat="server" />
    <asp:HiddenField ID="hidCaseID" runat="server" />
    <asp:HiddenField ID="hidGrpId" runat="server" />
    <asp:HiddenField ID="hidGrpDes" runat="server" />
    <asp:HiddenField ID="hidGroupReportD" runat="server" />
    <asp:HiddenField ID="hidRptDes" runat="server" />
    <asp:HiddenField ID="hidCompID" runat="server" />
    <asp:HiddenField ID="hidCompDes" runat="server" />
    </form>
</body>
</html>
