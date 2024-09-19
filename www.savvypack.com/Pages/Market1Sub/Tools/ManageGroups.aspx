<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManageGroups.aspx.vb" Inherits="Pages_Market1Sub_Tools_ManageGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Manage Groups</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
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
    <style type="text/css">
        .M1GrpModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../../Images/MarketAnalysisBarNew.png' );
            height: 54px;
            width: 1230px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        javascript: window.history.forward(1);
        function clickButton(e, buttonid) {

            var bt = document.getElementById(buttonid);
            if (bt) {


                if (event.keyCode == 13) {
                    document.getElementById(buttonid).focus();
                    // alert(buttonid);
                    //document.getElementById(buttonid).click();  

                }


            }

        }
        function Message() {


            if (document.getElementById("txtGDES1").value.split(' ').join('').length == 0) {
                alert("Group Descriptor1 cannot be blank");
                return false;
            }
            else if (document.getElementById("txtGDES2").value.split(' ').join('').length == 0) {
                alert("Group Descriptor2 cannot be blank");
                return false;
            }
            else {
                msg = "You are going to create a new Group. Do you want to proceed?"

            }



            if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }


        }

        function OpenCGroupPopup(page) {

            var width = 500;
            var height = 260;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            var type = document.getElementById('<%= hidtype.ClientID%>').value;
            var page = page + "?Type=" + type + "";
            // alert(page);
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

        function OpenEGroupPopup(page) {
            var width = 705;
            var height = 490;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            var type = document.getElementById('<%= hidtype.ClientID%>').value;
            var page = page + "?Type=" + type + "";
            // alert(page);
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }
        function OpenGroupPopup(page) {
            var width = 805;
            var height = 430;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';

            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(page, 'Chat1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

        function Validation() {
            var grpID = document.getElementById('<%= hidGroupID.ClientID%>').value;
            if (grpID == "0") {
                alert("Please select group for editing");
                return false;
            }
            else {
                return true;
            }


        }

        function ShowPopup(Page) {
            var width = 720;
            var height = 520;
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
            return false;
        }

        function ShowGroupPopup(Page) {

            var width = 1020;
            var height = 520;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            var type = document.getElementById('<%= hidtype.ClientID%>').value;

            Page = Page + "?Type=" + type + "";

            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
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
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }
        function ValidateList() {


            var name = document.getElementById("txtGroupDe1").value;

            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter Group Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }

        }
        function CloseWindow() {
            window.open('', '_self', '');
            window.close();
        }

        function Count(text) {
            var a = /\<|\>|\&#|\\/;
            if ((document.getElementById("txtGDES3").value.match(a) != null)) {
                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Group Notes . Please choose alternative characters.");
                var object = document.getElementById(text.id)  //get your object
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 500; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
        }

        //        function CheckSP(text) {

        //            var a = /\<|\>|\&#|\\/;
        //            if ((document.getElementById("txtGDES1").value.match(a) != null) || (document.getElementById("txtGDES2").value.match(a) != null)) {
        //                var object = document.getElementById(text.id)  //get your object
        //                if (text.id == "txtGDES1") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Group Descriptor1. Please choose alternative characters.");
        //                }
        //                else if (text.id == "txtGDES2") {
        //                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Group Descriptor2. Please choose alternative characters.");
        //                }
        //                object.focus(); //set focus to prevent jumping
        //                object.value = text.value.replace(new RegExp("<", 'g'), "");
        //                object.value = text.value.replace(new RegExp(">", 'g'), "");
        //                object.value = text.value.replace(/\\/g, '');
        //                object.value = text.value.replace(new RegExp("&#", 'g'), "");
        //                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
        //                return false;
        //            }
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
</head>
<body style="margin-top: 0px;">
    <%-- <center>--%>
    <form id="form1" runat="server">
    <div id="MasterContent">
        <%--  <div id="Div1">
            <asp:Image ImageAlign="AbsMiddle" ID="Image1" ImageUrl="~/Images/AlliedLog_PackagingIntell1.gif" Width="1230px" height="65px"
                runat="server" />
        </div>--%>
        <div>
            <table class="M1GrpModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse;">
                <tr>
                    <td style="padding-left: 1020px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnGlobalManager" runat="server" ImageUrl="~/Images/Close.gif"
                                        OnClientClick="javascript:CloseWindow();" Text="Close Window" CssClass="ButtonWMarigin"
                                        onmouseover="Tip('Close this Window')" onmouseout="UnTip()" />
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
    <table class="ContentPage" id="ContentPage" runat="server" cellpadding="0" cellspacing="0">
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left;">
                        <table width="1215px">
                            <tr class="AlterNateColor4">
                                <td colspan="3" class="PageSHeading" style="font-size: 14px;">
                                    Manage Group Tools
                                </td>
                            </tr>
                            <tr class="AlterNateColor2">
                                <td style="width: 900px; padding-left: 5px; margin-right: 0px;" align="left" colspan="3">
                                    <span><b>Groups Found = </b></span>
                                    <asp:Label ID="lblGroupCnt" runat="server" ForeColor="Red" Font-Bold="true" Width="60"></asp:Label>
                                    <span style="margin-left: 15px; text-align: left"><b></b></span>
                                    <asp:Label ID="lblCF" runat="server" Text="5" ForeColor="Red" Font-Bold="true" Width="60"
                                        Visible="false"></asp:Label>
                                    <asp:LinkButton ID="lnkGroups" runat="server" Width="140px" Style="color: Black;
                                        font-family: Verdana; font-size: 12px;" Text='Display All Groups' OnClientClick="return ShowGroupPopup('../Popup/AllGroupsDetails.aspx');"></asp:LinkButton>
                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                        Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtKey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                        width: 280px" MaxLength="100"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                    &nbsp;&nbsp;<asp:Button ID="btnGCreate" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                        Style="margin-left: 50px;" ToolTip="Create Group and Description" OnClientClick="return OpenCGroupPopup('../Popup/CreateGroup.aspx')" />
                                    &nbsp;&nbsp;<asp:Button ID="btnGCancel" runat="server" Text="Edit Group" CssClass="ButtonWMarigin"
                                        ToolTip="Edit Group and Description" OnClientClick="return OpenEGroupPopup('../Popup/EditGroup.aspx')" />
                                </td>
                                <%-- <td style="width: 100px; padding-left: 5px;" align="right">
                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel"></asp:Label>
                                </td>
                                 <td style="width: 900px" colspan="2">
                                    <table cellpadding="0" cellspacing="0">
                                        <tr>
                                            <td style="width: 460px">
                                                <asp:TextBox ID="txtKey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                                    width: 280px" MaxLength="100"></asp:TextBox>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin"
                                                    onmouseover="Tip('Search')" onmouseout="UnTip()" />
                                            </td>
                                            <td style="width: 340px">
                                                <span style="margin-left: 15px; text-align: right"><b>Cases Found = </b></span>
                                                <asp:Label ID="lblCF" runat="server" Text="5" ForeColor="Red" Font-Bold="true"></asp:Label>
                                                 <span style="margin-left: 30px;"><b>Groups Found = </b></span>
                                    <asp:Label ID="lblGroupCnt" runat="server" ForeColor="Red" Font-Bold="true"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>--%>
                            </tr>
                            <tr class="AlterNateColor2" id="trmsg" runat="server" visible="false">
                                <td colspan="3">
                                    <asp:Label ID="lblmsg" runat="server" Width="1070px" Style="text-align: center; font-size: 16px;
                                        font-weight: bold; color: Red;"> </asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2">
                                <td colspan="3">
                                    <div style="overflow: auto; height: 440px;">
                                        <asp:GridView Width="1190px" CssClass="GrdUsers" runat="server" ID="grdReport" DataKeyNames="GROUPID"
                                            AllowPaging="false" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                            Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                            <FooterStyle BackColor="#CCCC99" />
                                            <RowStyle BackColor="#F7F7DE" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="GROUPID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                                                    SortExpression="GROUPID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGroupID" runat="server" Text='<%# bind("GROUPID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROUP DESCRIPTOR1" HeaderStyle-HorizontalAlign="center"
                                                    SortExpression="GROUPNAME">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrpName" runat="server" Text='<%# bind("GROUPNAME")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROUP DESCRIPTOR2" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="DES2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDescriptor2" runat="server" Text='<%# bind("DES2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="REPORT ID" HeaderStyle-HorizontalAlign="Left" SortExpression="REPORTID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblReportID" runat="server" Text='<%# bind("REPORTID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ADD REPORTS" HeaderStyle-HorizontalAlign="center"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkGroId" runat="server" Width="90px" Style="color: Black; font-family: Verdana;
                                                            font-size: 12px;" Text="Add Reports"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="90px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROUP CREATION DATE" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="CREATIONDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbCDate" Width="70px" runat="server" Text='<%# bind("CREATIONDATE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="GROUP LAST UPDATED" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="UPDATEDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbUDate" Width="70px" runat="server" Text='<%# bind("UPDATEDATE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle Font-Underline="false" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="true"
                                                HorizontalAlign="Left" />
                                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                            <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                            <%--<tr class="AlterNateColor1">
                                <td colspan="3">
                                    <asp:Button ID="btnCGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                        ToolTip="Create Group and Description"  />
                                    <asp:Button ID="btnEditGroup" runat="server" Text="Edit Group" CssClass="ButtonWMarigin"
                                        ToolTip="Edit Group and Description" OnClientClick="return OpenEGroupPopup('../Popup/EditGroup.aspx')" />
                                </td>
                            </tr>  --%>
                        </table>
                        <%-- <div style="margin-top: 15px;" id="divGModify" runat="server" visible="false">
                            <table width="700px">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                        <asp:Label ID="lblTitle" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Group Descriptor1:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGDES1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25" onchange="javascript:CheckSP(this);" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td align="right">
                                        Group Descriptor2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtGDES2" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                            width: 230px" MaxLength="25" onchange="javascript:CheckSP(this);" ></asp:TextBox>
                                    </td>
                                </tr>                              
                               
                                <tr class="AlterNateColor1">
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnGCreate" runat="server" Text="Create" CssClass="ButtonWMarigin" OnClientClick="return Message();"
                                            onmouseover="Tip('Create new Group')" onmouseout="UnTip()" />
                                        <asp:Button ID="btnGCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                            Style="margin-left: 10px" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                    </td>
                                </tr>
                            </table>
                        </div>--%>
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
    <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    <asp:HiddenField ID="hidGroupID" runat="server" />
    <asp:HiddenField ID="hidGroupDes" runat="server" />
    <asp:HiddenField ID="hvUserGrd" runat="server" />
    <asp:HiddenField ID="hidType" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>
