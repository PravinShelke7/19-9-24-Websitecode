<%@ Page Language="VB" AutoEventWireup="false" CodeFile="QuickPricePopup.aspx.vb"
    Inherits="OnlineForm_Popup_QuickPricePopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Quick Price Popup</title>
    <link href="../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        javascript: window.history.forward(1); 

        function OpenLoginPopup() {
           
            var Page = "https://www.savvypack.com/OnlineForm/LoginS.aspx?Savvy=N";
            var width = 700;
            var height = 300;

            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Login_QP', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
                //alert("*****************Ankita****************.");
            }

            return false;
        }

        function OpenLoginPopupSavvy() {

            var Page = "https://www.savvypack.com/OnlineForm/LoginS.aspx?Savvy=Y";
            var width = 700;
            var height = 300;

            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Login_Savvy', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function OpenSavvyPack() {
            
            window.open("https://www.savvypack.com/OnlineForm/ProjectManager.aspx");
            return false;

        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 420;
            var height = 200;
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

    </script>
    <style type="text/css">
        #PageSection1
        {
            background-color: #D3E7CB;
            margin-left: 2px;
            height: 600px;
            width: 1150px;
        }
        .COLUMN1
        {
            width: 20%;
        }
        .COLUMN2
        {
            width: 15%;
        }
        .COLUMN3
        {
            width: 40%;
        }
        .PSavvyModule
        {
            margin-left: 1px;
            background: url('../../Images/SavvyPackProject1268x45.gif');
            height: 45px;
            width: 1150px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <center>
        <div id="PageSection1" style="text-align: left;">
            <div style="text-align: center">
                <table class="PSavvyModule" id="Savvytable" runat="server" cellpadding="0" cellspacing="0"
                    style="border-collapse: collapse;">
                    <tr>
                        <td style="padding-left: 400px">
                        </td>
                        <td style="margin-left: 0px">
                            <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                                <tr>
                                    <td>
                                        <asp:Button ID="BtnLoginQuickP" runat="server" Width="65px" Text="Login" ForeColor="black"
                                            Font-Bold="true" Style="border-radius: 15px;" Height="29px" Visible="false"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnLogoutQuickP" runat="server" Width="65px" Text="Logout" ForeColor="black"
                                            Font-Bold="true" Style="border-radius: 15px;" Height="29px" Visible="false"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="BtnSavvy" runat="server" Width="165px" Text="Go to Project Manager"
                                            ForeColor="black" Font-Bold="true" Style="border-radius: 15px; margin-left: 15px;"
                                            Height="29px" Visible="false"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="text-align: center; height: 30px;">
                <asp:Label ID="lblQuickPrice" runat="server" Font-Bold="true" Font-Italic="true"
                    Style="margin-left: 140px;" Font-Size="14px"></asp:Label>
            </div>
            <%--<table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                <tr>
                    <td>
                        <asp:Button ID="BtnLoginQuickP" runat="server" Width="65px" Text="Login" ForeColor="black"
                            Font-Bold="true" Style="border-radius: 15px;" Height="29px" Visible="false"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="BtnLogoutQuickP" runat="server" Width="65px" Text="Logout" ForeColor="black"
                            Font-Bold="true" Style="border-radius: 15px;" Height="29px" Visible="false"></asp:Button>
                    </td>
                    <td>
                        <asp:Button ID="BtnSavvy" runat="server" Width="160px" Text="Go to Project Manager"
                            ForeColor="black" Font-Bold="true" Style="border-radius: 15px; margin-left: 20px;"
                            Height="29px" Visible="false"></asp:Button>
                    </td>
                </tr>
            </table>--%>
            <div id="error">
                <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
            </div>
            <table width="90%" style="margin-left: 30px;">
                <tr class="AlterNateColor4" style="height: 20px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelSpec1" runat="server" Text="Specification" Font-Bold="true" Font-Size="15px">
                        </asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LabelUnit1" Text="Unit" runat="server" Font-Bold="true" Font-Size="15px">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="labelValue1" Text="Value" MaxLength="100" runat="server" Font-Bold="true"
                            Font-Size="15px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelResultPrice" runat="server" Font-Bold="true" Font-Size="18px"
                            Text="Results Price:" Style="margin-left: 30px;"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblUnit" Text="$/1000 carton" runat="server" Font-Bold="true" Font-Size="18px"
                            Style="margin-left: 5px;"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblRPrice1" runat="server" Font-Bold="true" Font-Size="18px" Style="margin-left: 5px;">
                        </asp:Label>
                    </td>
                </tr>
                <tr style="height: 13px;">
                    <td>
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="AlterNateColor4" style="height: 20px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelSpec" runat="server" Text="Specification" Font-Bold="true" Font-Size="15px">
                        </asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LabelUnit" Text="Unit" runat="server" Font-Bold="true" Font-Size="15px">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="labelValue" Text="Value" MaxLength="100" runat="server" Font-Bold="true"
                            Font-Size="15px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelAQ" runat="server" Text="Annual order quantity:" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter no of Annual order quantity."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LblAQ" runat="server" Font-Bold="true" ToolTip="Enter no of Annual order quantity."
                            Font-Size="13px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblOQ" MaxLength="100" runat="server" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelOS" runat="server" Text="Order size:" Font-Bold="true" ToolTip="Enter Order size"
                            Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LblOS" runat="server" Font-Bold="true" ToolTip="(number)." Font-Size="13px">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblOS1" MaxLength="100" runat="server" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelQ4" runat="server" Text="Flat blank dimensions:" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter Flat blank dimensions."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblddlFlat_BD" runat="server" Font-Bold="true" Width="55px" ToolTip="Enter Flat blank dimensions."
                            Font-Size="13px"></asp:Label>
                        <%-- <asp:DropDownList ID="ddlFlat_BD" CssClass="DropDown" Width="55px" runat="server"
                            AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:Label ID="Labelwidth" runat="server" Text="Width:" Font-Bold="true"></asp:Label>
                        <asp:Label ID="lblWidth" ToolTip="Enter width." width="40px" MaxLength="100" runat="server"
                            Font-Size="14px"></asp:Label>
                        <asp:Label ID="lbl" runat="server" Text="" Font-Bold="true" ToolTip="" style="visibility: hidden;">
                        </asp:Label>
                        <asp:Label ID="Labelheight" runat="server" Text="Length:" width="40px" Font-Bold="true">
                        </asp:Label>
                        <asp:Label ID="lblHeight" ToolTip="Enter height." width="40px" MaxLength="100" runat="server"
                            Font-Size="14px"></asp:Label>
                        <asp:Label ID="lbl3" runat="server" Text="" Font-Bold="true" ToolTip="" style="visibility: hidden;">
                        </asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelCOD" runat="server" Text="Carton outside dimensions:" Font-Bold="true"
                            ToolTip="Enter Carton outside dimensions" Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblddl_COD" runat="server" Font-Bold="true" Width="55px" Font-Size="13px">
                        </asp:Label>
                        <%--<asp:DropDownList ID="ddl_COD" CssClass="DropDown" Width="55px" runat="server" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:Label ID="Labelwidth_COD" runat="server" Text="Width:" Font-Bold="true" ToolTip="Enter Width for Flat blank dimensions.">
                        </asp:Label>
                        <asp:Label ID="lblWidth_COD" MaxLength="100" width="40px" runat="server" Font-Size="14px">
                        </asp:Label>
                        <asp:Label ID="lbl1" runat="server" Text="" Font-Bold="true" ToolTip="" style="visibility: hidden;">
                        </asp:Label>
                        <asp:Label ID="LabelLength_COD" runat="server" width="40px" Text="Length:" Font-Bold="true">
                        </asp:Label>
                        <asp:Label ID="lblHeight_COD" ToolTip="Enter height." width="40px" MaxLength="100"
                            Font-Size="14px" runat="server"></asp:Label>
                        <asp:Label ID="lbl2" runat="server" Text="" Font-Bold="true" ToolTip="" style="visibility: hidden;">
                        </asp:Label>
                        <asp:Label ID="Labelheight_COD" runat="server" width="40px" Text="Height:" Font-Bold="true">
                        </asp:Label>
                        <asp:Label ID="lblLength_COD" MaxLength="100" runat="server" width="40px" Font-Size="14px">
                        </asp:Label>
                        <asp:Label ID="lbl5" runat="server" Text="" Font-Bold="true" ToolTip="" style="visibility: hidden;">
                        </asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelWEC" runat="server" Text="Weight of empty case:" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter Weight of empty case."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblwc_BD" runat="server" Font-Bold="true" Width="55px" Font-Size="13px">
                        </asp:Label>
                        <%--<asp:DropDownList ID="wc_BD" CssClass="DropDown" Width="55px" runat="server" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblWEC" MaxLength="100" runat="server" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelWPP" runat="server" Text="Weight of product packaged:" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter Weight of product packaged."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblwpp_BD" runat="server" Font-Bold="true" Width="55px" Font-Size="13px">
                        </asp:Label>
                        <%--  <asp:DropDownList ID="wpp_BD" CssClass="DropDown" Width="55px" runat="server" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblWPP" MaxLength="100" runat="server" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelPrinted" runat="server" Text="Printed :" Font-Bold="true" ToolTip="Enter Printed Y/N."
                            Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblPrinted_DDL" Width="86px" MaxLength="100" runat="server" Font-Size="14px">
                        </asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelECT" runat="server" Text="ECT:" Font-Bold="true" ToolTip="Enter ECT."
                            Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LblECT" runat="server" Font-Bold="true" ToolTip="" Font-Size="13px"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblECT_DDL" Width="120px" MaxLength="100" runat="server" Font-Size="14px">
                        </asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelMul" runat="server" Text="Mullens Rating:" Font-Bold="true" Font-Size="14px">
                        </asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LblmULLENS" runat="server" Font-Bold="true" ToolTip="" Font-Size="13px">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblmULLENS_DDL" Width="120px" MaxLength="100" runat="server" Font-Size="14px">
                        </asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelPQ" runat="server" Text="Print quality :" Font-Bold="true" ToolTip="Enter Print quality (pre or post printed)"
                            Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblPQ_DDL" Width="135px" MaxLength="100" runat="server" Font-Size="14px">
                        </asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelPC" runat="server" Text="Print :" Font-Bold="true" ToolTip="Enter Print (number of colors)."
                            Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblPC1" MaxLength="100" runat="server" Font-Bold="true" Font-Size="13px">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblPC" MaxLength="100" runat="server" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelBCom" runat="server" Text="Board combination:" Font-Bold="true"
                            ToolTip="Enter Board combination." Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="LblBcom" runat="server" Font-Bold="true" ToolTip="" Font-Size="13px">
                        </asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblBCom1" runat="server" Width="120px" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelBW" runat="server" Text="Overall board weight:" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter Overall board weight."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        <asp:Label ID="lblBw_BD" runat="server" Font-Bold="true" Width="55px" Font-Size="13px">
                        </asp:Label>
                        <%--<asp:DropDownList ID="Bw_BD" CssClass="DropDown" Width="55px" runat="server" AutoPostBack="true">
                        </asp:DropDownList>--%>
                    </td>
                    <td>
                        <asp:Label ID="lblBW" MaxLength="100" Width="55px" runat="server" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelFS" runat="server" Text="Container size :" Font-Bold="true" ToolTip="Enter Flute size."
                            Font-Size="14px"></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblFS_DDL" runat="server" Width="120px" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor1" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelBStyle" runat="server" Text="Container style :" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter Box style."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblBStyle_DDL" runat="server" Width="120px" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <tr class="AlterNateColor2" style="height: 23px;">
                    <td class="COLUMN1">
                        <asp:Label ID="LabelSFormat" runat="server" Text="Ship format :" Font-Bold="true"
                            Font-Size="14px" ToolTip="Enter Ship format."></asp:Label>
                    </td>
                    <td class="COLUMN2">
                    </td>
                    <td class="COLUMN3">
                        <asp:Label ID="lblSFormat_DDL" runat="server" Width="120px" Font-Size="14px"></asp:Label>
                    </td>
                </tr>
                <%--<tr style="height: 40px;">
                    <td class="COLUMN1">
                    </td>
                    <td class="COLUMN2">
                    </td>
                    <td style="width:55%;">
                        <%-- <asp:Button ID="btnnxtQP" runat="server" Text="Next" />--%>
                <%--  </td>
                </tr>--%>
            </table>
        </div>
        <div style="width: 100%;">
            <table width=" 1150px">
                <tr class="AlterNateColor3">
                    <td colspan="2" class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div id="AlliedLogo">
            <table>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 1150px; background-color: #edf0f4;">
                            <tr>
                                <td align="left">
                                    <%--<asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />--%>
                                    <asp:ImageButton ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" Enable="false"/>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:HiddenField ID="hiPrevUserId" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </center>
    </form>
</body>
</html>
