<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectManager.aspx.vb"
    Inherits="Pages_ProjectManager_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Project Manager</title>
    <link href="../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
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
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

        //Quick Price Popup
        function ShowPopWindowQP(Page) {
            
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 1300;
            var height = 780;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
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
            newwin = window.open(Page, 'NewWindow1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

        }



        function ShowToolTip(ControlId, Message) {

            document.getElementById(ControlId).onmouseover = function () { Tip(Message); };
            document.getElementById(ControlId).onmouseout = function () { UnTip(''); };

        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 630;
            var height = 275;
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
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Universal Uploadfile Popup
        function ShowPopWindow2(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 750;
            var height = 470;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Datetype Popup
        function ShowPopWindow1(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 500;
            var height = 300;
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
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Type Details Popup
        function ShowPopWindow3(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 310;
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
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }


        //Date Selection
        function ShowPopWindow4(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 300;
            var height = 230;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Advance Status popup
        function ShowPopWindowStatus(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 480;
            var height = 380;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Advance Milestone popup
        function ShowPopWindowMilestone(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 445;
            var height = 465;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function Help() {
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
            URL = "help/SavvyPackProjectInstructions.pdf"
            newwin = window.open(URL, 'NewWindow1', params);
            return false

        }

        //        function CheckSP(text) {

        //            var a = /\<|\>|\&#|\\/;
        //            var object = document.getElementById(text.id)//get your object
        //            if ((document.getElementById(text.id).value.match(a) != null)) {

        //                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
        //                object.focus(); //set focus to prevent jumping
        //                object.value = text.value.replace(new RegExp("<", 'g'), "");
        //                object.value = text.value.replace(new RegExp(">", 'g'), "");
        //                object.value = text.value.replace(/\\/g, '');
        //                object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //                return false;
        //            }
        //        }

        function ShowPopWindowCreate(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 950;
            var height = 540;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function OpenEmailWindow() {

            window.open("MessageManager.aspx?Message=Nothing");
            return false;

        }

        function ShowPopWindow6(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 710;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
    </script>
    <style type="text/css">
        tr.row
        {
            background-color: #fff;
        }
        
        
        tr.row td
        {
        }
        
        tr.row:hover td, tr.row.over td
        {
            background-color: #eee;
        }
    </style>
    <style type="text/css">
        .PSavvyModule
        {
            margin-top: 2px;
            margin-left: 0px;
            background: url('../Images/SavvyPackProject1268x45.gif') no-repeat;
            height: 45px;
            width: 1350px;
            text-align: center;
        }
        .style4
        {
            width: 444px;
        }
        .style5
        {
            width: 132px;
        }
    </style>
</head>
<body>
    <script type="text/javascript" src="../javascripts/wz_tooltip.js"></script>
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <center>
        <div>
            <div>
                <%--<table class="PSavvyModule" id="Savvytable" runat="server" cellpadding="0" cellspacing="0"
                style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgInstructions" ImageAlign="Middle" ImageUrl="~/Images/Instruction.gif"
                                        runat="server" ToolTip="Instructions" Visible="true" OnClientClick="return Help();" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>--%>
                <div id="Div1">
                    <asp:Image ImageAlign="AbsMiddle" Width="1350px" ID="Image1" ImageUrl="~/Images/SavvyPackProject1268x45.gif"
                        runat="server" />
                </div>
            </div>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
        </div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;">
                        Project Manager
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="SavvyContentPagemargin" runat="server">
                        <div id="SavvyPageSection1" style="text-align: left;">
                            <table style="width: 100%">
                                <tr>
                                    <td>
                                        <table style="width: 98%">
                                            <tr>
                                                <td style="text-align: left; vertical-align: middle; width: 100px;">
                                                    <asp:Label ID="Label2" runat="server" Text="Projects Found:" CssClass="NormalLabel"
                                                        Font-Bold="true" Style="text-align: right; font-size: 12px; vertical-align: middle;
                                                        font-family: Optima"></asp:Label>
                                                    <asp:Label ID="lblPCount" runat="server" Style="font-weight: bold; font-size: 14px;
                                                        vertical-align: middle; font-family: Optima" ForeColor="Red" CssClass="NormalLabel">
                                                    </asp:Label>
                                                </td>
                                                <td style="text-align: left; vertical-align: middle; width: 135px;">
                                                    <asp:Label ID="lblPageSize" runat="server" Text="Page Size:" CssClass="NormalLabel"
                                                        Font-Bold="true" Style="margin-left: 5px; text-align: right; vertical-align: middle;
                                                        font-size: 12px; font-family: Optima"></asp:Label>
                                                    <asp:DropDownList ID="ddlSize" runat="server" Width="55px" CssClass="DropDown" Style="height: 18px;
                                                        font-size: 12px; font-family: Optima;" AutoPostBack="true">
                                                        <asp:ListItem Value="1">10</asp:ListItem>
                                                        <asp:ListItem Value="2">25</asp:ListItem>
                                                        <asp:ListItem Value="3">50</asp:ListItem>
                                                        <asp:ListItem Value="4">100</asp:ListItem>
                                                        <asp:ListItem Value="5">500</asp:ListItem>
                                                        <asp:ListItem Value="6">1000</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="text-align: left; height: 20px; vertical-align: middle; width: 135px;">
                                                    <asp:Label ID="lblMode" runat="server" Text="Display Mode:" CssClass="NormalLabel"
                                                        Font-Bold="true" Height="20px"></asp:Label>
                                                    <asp:ImageButton ID="imgPls" ImageUrl="../Images/plus.png" CommandArgument="Full"
                                                        runat="server" Height="20px" Width="20px" OnClick="ImageButton_Click" ToolTip="Click to unfold your projects" />
                                                    <asp:ImageButton ID="imgMin" ImageUrl="../Images/minus.png" CommandArgument="Min"
                                                        runat="server" Height="20px" Width="20px" OnClick="ImageButton_Click" ToolTip="Click to fold your projects " />
                                                </td>
                                                <td style="font-size: 12px; width: 250px;" align="center">
                                                    <asp:Button ID="Button2" Height="40px" runat="server" ToolTip="Click Here to create a new project."
                                                        Width="190px" Style="color: Black; font-family: @Arial Unicode; font-size: 13px;"
                                                        Font-Bold="true" Text='Create a New Project' OnClientClick="return ShowPopWindowCreate('CreateProject.aspx?Id=hidProjectId&PType=N');">
                                                    </asp:Button>
                                                    <asp:ImageButton ID="ImageButton1" runat="server" Style="margin-left: 40px; margin-top: 0px;
                                                        margin-bottom: 5px;" ImageAlign="Middle" ImageUrl="../Images/read.png" Height="35px"
                                                        Width="35px" OnClientClick="return OpenEmailWindow();" ToolTip="Click Here to manage your messages." />
                                                    <%--<asp:ImageButton ID="ImageButton1" runat="server" Style="margin-left: 6px; margin-bottom: 5px;"
                                                    ImageAlign="Middle" ImageUrl="../Images/unread.png" Height="35px" Width="35px"
                                                    OnClientClick="OpenEmailWindow()" ToolTip="Unread Message" />--%>
                                                </td>
                                                <td style="font-size: 12px; width: 400px;" align="right">
                                                    <asp:Label ID="lblSearch" runat="server" Text="Search Text:" CssClass="NormalLabel"
                                                        Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                                                    <asp:TextBox ID="txtSearch" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                                        width: 250px" MaxLength="100">
                                                    </asp:TextBox>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin"
                                                        Width="59px" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr id="trRow" runat="server" visible="true" style="background-color: #6B696B; font-weight: bold;
                                    font-family: Verdana; font-size: 11px;">
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 40px; color: White; text-decoration: underline;">
                                                    Project ID
                                                </td>
                                                <td style="width: 20px;">
                                                </td>
                                                <td style="width: 140px; color: White; text-decoration: underline;">
                                                    Project Title
                                                </td>
                                                <td style="width: 140px; color: White; text-decoration: underline;">
                                                    Description
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                    Type of Analysis
                                                </td>
                                                <td style="width: 100px; color: White; text-decoration: underline;">
                                                    Analyst
                                                </td>
                                                <td style="width: 100px; color: White; text-decoration: underline;">
                                                    Owner
                                                </td>
                                                <td style="width: 110px; color: White; text-decoration: underline;">
                                                    <asp:Label ID="lblSt" runat="server" onClick="return ShowPopWindowStatus('Popup/StatusSelectionPopup.aspx');"
                                                        Text="Status" Style="cursor: pointer;"></asp:Label>
                                                </td>
                                                <td style="width: 110px; color: White; text-decoration: underline;">
                                                    <asp:Label ID="lblMl" runat="server" onClick="return ShowPopWindowMilestone('Popup/MilestoneSelectionPopup.aspx');"
                                                        Text="Milestone" Style="cursor: pointer;"></asp:Label>
                                                </td>
                                                <td style="width: 110px; color: White; text-decoration: underline;">
                                                    Project Files
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                    Project Results
                                                </td>
                                                <td style="width: 75px; color: White; text-decoration: underline;">
                                                    Submit
                                                </td>
                                                <td style="width: 60px; color: White; text-decoration: underline;">
                                                    Model Grid
                                                </td>
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr id="tr1" runat="server" visible="false" style="background-color: #6B696B; font-weight: bold;">
                                    <td>
                                        <table>
                                            <tr>
                                                <td style="width: 125px;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px; color: White; text-decoProject Results
                                                </td>
                                                <td style="width: 125px; color: White; text-decoration: underline;">
                                                </td>
                                                <td style="width: 125px;">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td>
                                        <asp:Label ID="lblMsg" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                            Text="You currently have no SavvyPack Projects to display." Font-Size="11" Style="margin-top: 30px;
                                            margin-left: 500px; color: red; font-weight: bold;"></asp:Label>
                                        <asp:GridView Width="1340px" CssClass="grdProject" runat="server" ID="grdProject"
                                            DataKeyNames="PROJECTID" AllowPaging="true" PageSize="10" AllowSorting="True"
                                            AutoGenerateColumns="False" Font-Size="11px" CellPadding="4" ForeColor="Black"
                                            GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                            BorderWidth="1px">
                                            <PagerSettings Position="Top" />
                                            <PagerTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                    Style="color: Black;">First</asp:LinkButton>
                                                <asp:Label ID="pmore" runat="server" Text="..."></asp:Label>
                                                <asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;" CommandName="Page"
                                                    CommandArgument="Prev">Previous</asp:LinkButton>
                                                <asp:LinkButton ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:Label ID="CurrentPage" runat="server" Text="Label" Style="color: Red;"></asp:Label>
                                                <asp:LinkButton ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next"
                                                    Style="color: #284E98;">Next</asp:LinkButton>
                                                <asp:Label ID="nmore" runat="server" Text="..."></asp:Label>
                                                <asp:LinkButton ID="LinkButton4" runat="server" CommandName="Page" CommandArgument="Last"
                                                    Style="color: Black;">Last</asp:LinkButton>
                                            </PagerTemplate>
                                            <FooterStyle BackColor="#CCCC99" />
                                            <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                           <%--  <HeaderStyle CssClass="AlterNateColor4" Font-Size="12px" />--%>
                                            <Columns>
                                                <asp:TemplateField HeaderText="USERID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                    SortExpression="USERID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUserID" Width="50px" runat="server" Text='<%# bind("USERID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="20px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project ID" HeaderStyle-HorizontalAlign="Left" SortExpression="PROJECTID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProjId" runat="server" Visible="true" Text='<%# bind("PROJECTID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Type Of Analysis" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="TYPE">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkAnalysis" runat="server" Width="80px" Style="color: Black;
                                                        font-family: Verdana; font-size: 11px; text-decoration: underline;" Text='<%# bind("TYPE")%>'
                                                        CommandName="EditProject" CommandArgument='<%# Eval("PROJECTID") %>'></asp:LinkButton>
                                                    <asp:Label ID="lblAnalysis" runat="server" Visible="false" Text='<%# bind("TYPE")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Project Title" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="TITLE">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkTitle" runat="server" Width="100px" Style="color: Black; font-family: Verdana;
                                                            font-size: 11px; text-decoration: underline;" Text='<%# bind("TITLE1")%>' CommandName="EditProject"
                                                            CommandArgument='<%# Eval("PROJECTID") %>'></asp:LinkButton>
                                                        <asp:Label ID="lblTitle" runat="server" Visible="false" Text='<%# bind("TITLE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brief Description" HeaderStyle-HorizontalAlign="Left"
                                                    Visible="false" SortExpression="KEYWORD">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkWord" runat="server" Width="110px" Style="color: Black; font-family: Verdana;
                                                            font-size: 11px; text-decoration: underline;" Text='<%# bind("KEYWORD1")%>' CommandName="EditProject"
                                                            CommandArgument='<%# Eval("PROJECTID") %>'></asp:LinkButton>
                                                        <asp:Label ID="lblWord" runat="server" Visible="false" Text='<%# bind("KEYWORD")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description" HeaderStyle-HorizontalAlign="Left" SortExpression="DESCRIPTION">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDesc" runat="server" Width="180px" Style="color: Black; font-family: Verdana;
                                                            font-size: 11px; text-decoration: underline;" Text='<%# bind("DESCRIPTION1") %>'
                                                            CommandName="EditProject"></asp:LinkButton>
                                                        <asp:Label ID="lblDesc" runat="server" Visible="false" Text='<%# bind("DESCRIPTION") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Type Of Analysis" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="TYPE">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAnalysis" runat="server" Width="85px" Style="color: Black;
                                                            font-family: Verdana; font-size: 11px; text-decoration: underline;" Text='<%# bind("TYPE")%>'
                                                            CommandName="EditProject" CommandArgument='<%# Eval("PROJECTID") %>'></asp:LinkButton>
                                                        <asp:Label ID="lblAnalysis" runat="server" Visible="false" Text='<%# bind("TYPE")%>'></asp:Label>
                                                        <asp:Label ID="lblAnalysID" runat="server" Visible="false" Text='<%# bind("ANALYSISID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="85px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Analyst" HeaderStyle-HorizontalAlign="Left" SortExpression="Analyst">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkAnalyst" runat="server" Style="color: Black; font-family: Verdana;
                                                            font-size: 11px; text-decoration: underline;" Text='<%# bind("ANALYST")%>'></asp:LinkButton>
                                                        <asp:Label ID="lblAEmail" runat="server" Visible="false" Text='<%# bind("ANALYSTEMAILID")%>'></asp:Label>
                                                        <asp:Label ID="lblANum" runat="server" Visible="false" Text='<%# bind("ANALYST1")%>'></asp:Label>
                                                        <asp:Label ID="lblAnlId" runat="server" Visible="false" Text='<%# bind("ISANALYST")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Owner" HeaderStyle-HorizontalAlign="Left" SortExpression="Owner">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUser" runat="server" Text='<%# bind("OWNER1")%>'></asp:Label>
                                                        <asp:Label ID="lblEmail" runat="server" Visible="false" Text='<%# bind("OWNER")%>'></asp:Label>
                                                        <asp:Label ID="lblNum" runat="server" Visible="false" Text='<%# bind("PHONENUMBER")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField HeaderText="Current Status" HeaderStyle-HorizontalAlign="Left"
                                                SortExpression="STATUS">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblStatus" runat="server" Text='<%# bind("STATUS")%>'></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right">
                                                    <HeaderTemplate>
                                                        <table style="border-spacing: 0px; width: 50px;">
                                                            <tr>
                                                                <td onclick="return ShowPopWindowStatus('Popup/StatusSelectionPopup.aspx');" style="border-width: 0px 0px 0px 0px;
                                                                    text-decoration: underline;">
                                                                    <asp:Label CommandName="Sort" ID="lnkStatus" runat="server" Style="color: White;
                                                                        cursor: pointer;" Text="Status"></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblStatus" runat="server" Text='<%# bind("STATUS")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="50px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderStyle-HorizontalAlign="Right">
                                                <HeaderTemplate>
                                                    <table style="border-spacing: 0px; width: 100px;">
                                                        <tr>
                                                            <td style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                <asp:LinkButton CommandName="Sort" ID="lnkMile" runat="server" Style="color: White;"
                                                                    Text="Milestones"></asp:LinkButton>
                                                            </td>
                                                            <td style="border-width: 0px 0px 0px 0px; float: right; margin-right: 0px;">
                                                                 <asp:ImageButton ID="imgD" ImageUrl="../Images/set.png" runat="server" ToolTip="Click to select the date to be displayed in the column"
                                                                    Height="20px" Width="20px" OnClientClick="return ShowPopWindow4('Popup/DateSelectionPopup.aspx');"
                                                                    Style="float: right;" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkDate" runat="server" Width="60px" Style="color: Black; font-family: Verdana;
                                                        font-size: 11px; text-decoration: underline;" Text='<%# bind("VALUE")%>'></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Width="10px" Wrap="true" HorizontalAlign="Left" />
                                            </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Right">
                                                    <HeaderTemplate>
                                                        <table style="border-spacing: 0px; width: 100px;">
                                                            <tr>
                                                                <td style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    <asp:LinkButton ID="lnkMile" runat="server" Style="color: White;" Text="Milestones"
                                                                        OnClientClick="return ShowPopWindowMilestone('Popup/MilestoneSelectionPopup.aspx');"></asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkDate" runat="server" Width="110px" Style="color: Black; font-family: Verdana;
                                                            font-size: 11px; text-decoration: underline;" Text='<%# bind("VALUE")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="110px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Project Files" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkUplF" runat="server" Width="80px" Style="color: Black; font-family: Verdana;
                                                            font-size: 11px; text-decoration: underline;" Text="View Files" CommandName="Upload"
                                                            CommandArgument='<%# Eval("PROJECTID") %>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <table width="100%" border="1" style="border-width: 0px 0px 0px 0px; text-align: center;">
                                                            <tr style="width: 50%">
                                                                <td colspan="3" align="center" style="border-width: 0px 0px 0px 0px; text-decoration: underline;color:White;font-size:12px;">
                                                                    Project Results
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 50%">
                                                                <td align="center" style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    Results
                                                                </td>
                                                                <td style="border-width: 0px 0px 0px 0px;">
                                                                </td>
                                                                <td width="49%" align="center" style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    Return on Investment
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" border="1" style="border-width: 0px 0px 0px 0px; text-align: center;">
                                                            <tr>
                                                                <td width="49.6%" class="tr1normal4" style="border-width: 0px 0px 1px 0px;">
                                                                    <asp:LinkButton ID="lnkCRes" runat="server" Width="80px" Style="color: Black; font-family: Verdana;
                                                                        font-size: 11px; text-decoration: underline;" Text='Results' >
                                                                        </asp:LinkButton>
                                                                    <asp:Label ID="lblCRes" runat="server" Visible="false" Text='Results'></asp:Label>
                                                                </td>
                                                                <td width="0.8%" style="border-width: 0px 0px 0px 0px; text-align: center;">
                                                                </td>
                                                                <td width="49.6%" class="tr1normal3" style="border-width: 0px 0px 1px 1px;">
                                                                    <asp:LinkButton ID="lnkROI" runat="server" Width="80px" Style="color: Black; font-family: Verdana;
                                                                        font-size: 11px; text-decoration: underline;" Text='ROI' Wrap="true"></asp:LinkButton>
                                                                    <asp:Label ID="lblROI" runat="server" Visible="false" Text='ROI'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%--<asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <table width="100%" border="1" style="border-width: 0px 0px 0px 0px; text-align: center;">
                                                            <tr style="width: 50%">
                                                                <td colspan="3" align="center" style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    Project Results
                                                                </td>
                                                            </tr>
                                                            <tr style="width: 50%">
                                                                <td align="center" style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    Quantitative
                                                                </td>
                                                                <td style="border-width: 0px 0px 0px 0px;">
                                                                </td>
                                                                <td width="49%" align="center" style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    Qualitative
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <table width="100%" border="1" style="border-width: 0px 0px 0px 0px; text-align: center;">
                                                            <tr>
                                                                <td width="49.6%" class="tr1normal4" style="border-width: 0px 0px 1px 0px;">
                                                                    <asp:LinkButton ID="lnkQuan" runat="server" Width="80px" Style="color: Black; font-family: Verdana;
                                                                        font-size: 11px; text-decoration: underline;" Text='<%# bind("QUANTBNF1")%>'></asp:LinkButton>
                                                                    <asp:Label ID="lblQuan" runat="server" Visible="false" Text='<%# bind("QUANTBNF")%>'></asp:Label>
                                                                </td>
                                                                <td width="0.8%" style="border-width: 0px 0px 0px 0px; text-align: center;">
                                                                </td>
                                                                <td width="49.6%" class="tr1normal3" style="border-width: 0px 0px 1px 1px;">
                                                                    <asp:LinkButton ID="lnkQual" runat="server" Width="80px" Style="color: Black; font-family: Verdana;
                                                                        font-size: 11px; text-decoration: underline;" Text='<%# bind("QUALBNF1")%>'></asp:LinkButton>
                                                                    <asp:Label ID="lblQual" runat="server" Visible="false" Text='<%# bind("QUALBNF")%>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                               
                                                <asp:TemplateField HeaderText="Submit" HeaderStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSubmit" runat="server" Visible="false" Text='<%# bind("STATUSID")%>'></asp:Label>
                                                        <asp:Button ID="btnSubmit" runat="server" CausesValidation="false" CommandName="Submit"
                                                            Text="Submit" CommandArgument='<%# Eval("PROJECTID") %>' />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="60px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <table style="border-spacing: 0px; width: 60px;">
                                                            <tr>
                                                                <td style="border-width: 0px 0px 0px 0px; text-decoration: underline;">
                                                                    Model Grid
                                                                </td>
                                                                <td style="border-width: 0px 0px 0px 0px; float: right; margin-right: 0px;">
                                                                    <asp:ImageButton ID="Question" ImageUrl="../Images/info.png" runat="server" ToolTip="Inputs on Model Grid page are not necessary. You may add further details for your economic or environmental analysis projects there."
                                                                        Height="20px" Width="20px" OnClientClick="" Style="float: right;" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkGrid" runat="server" Style="color: Black; font-family: Verdana;"
                                                            Text="Details" NavigateUrl=<%# "Javascript:OpenNewWindow('ModelInput.aspx?ProjectId=" + DataBinder.Eval(Container.DataItem, "PROJECTID").ToString() + "&UserId="+DataBinder.Eval(Container.DataItem, "USERID").ToString() + "')"%>>
                                                        </asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="60px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
                                                HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
          
        </table>
        <div id="AlliedLogo">
            <table>
              <tr class="AlterNateColor3">
                <td class="PageSHeading" align="center">
                    <asp:Label ID="lblTag" runat="Server"></asp:Label>
                </td>
            </tr>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 1350px; background-color: #edf0f4;">
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
        <asp:HiddenField ID="hidSortId" runat="server" />
        <asp:HiddenField ID="hidDesc" runat="server" />
        <asp:HiddenField ID="hidAnalysisId" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </center>
    </form>
</body>
</html>
