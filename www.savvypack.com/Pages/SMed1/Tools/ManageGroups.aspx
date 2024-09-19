<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ManageGroups.aspx.vb" Inherits="Pages_MedSustain1_Tools_ManageGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMed1-Manage Groups</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .S1CompModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../../Images/Sustain1Fulcrum.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
    <script type="text/javascript">
        javascript: window.history.forward(1);
         function clickButton(e, buttonid) {
            var evt = e ? e : window.event;
            var bt = document.getElementById(buttonid);
            if (bt) {
                if (evt.keyCode == 13) {
                    bt.click();
                    return false;
                }
            }
        } 
         function ShowGroupPopup(Page) {
            var width = 1020;
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
        function OpenEGroupPopup(page) {
            var width = 735;
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
            newwin = window.open(page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }
        function OpenGroupPopup(page) {
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
            newwin = window.open(page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }
        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 98; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            return true;
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
            <table class="SMed1Module" id="SMed1Table" runat="server"  cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                        <table cellpadding="0" cellspacing="5" style="border-collapse: collapse">
                            <tr>
                                <td>
                                    <asp:ImageButton ID="imgUpdate" ImageAlign="Middle" ImageUrl="~/Images/Update.gif"
                                        runat="server" ToolTip="Update" Visible="false" />
                                </td>
                                <td>
                                    <%--<asp:ImageButton ID="btnGlobalManager" runat="server" ImageUrl="~/Images/GlobalManager.gif"
                                        Text="Global Manager" CssClass="ButtonWMarigin" onmouseover="Tip('Return to Global Manager')"
                                        onmouseout="UnTip()" />--%>
                                          <asp:ImageButton ID="btnGlobalManager" runat="server" ImageUrl="~/Images/Close.gif" OnClientClick="window.close();"
                                        Text="Close window" CssClass="ButtonWMarigin" onmouseover="Tip('Close window')"
                                        onmouseout="UnTip()" />
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
    <table class="ContentPage" id="ContentPage" runat="server" cellpadding="0"
        cellspacing="0">
        <tr style="height: 20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align: left">
                        <table width="1215px">
                            <tr class="AlterNateColor4">
                                <td colspan="3" class="PageSHeading" style="font-size: 14px;">
                                    Manage Groups Tool
                                </td>
                            </tr>
                         <tr class="AlterNateColor2">
                                <td style="width: 900px; padding-left: 5px; margin-right:0px;" align="left" colspan="3">
                                     <span ><b>Groups Found = </b></span>
                                    <asp:Label ID="lblGroupCnt" runat="server" ForeColor="Red" Font-Bold="true" Width="60"></asp:Label>
                                    <span style="margin-left: 15px; text-align: left"><b>Cases Found = </b></span>
                                     <asp:Label ID="lblCF" runat="server" Text="5" ForeColor="Red" Font-Bold="true" Width="80"></asp:Label>
                                        <asp:LinkButton ID="lnkGroups" runat="server" Width="140px" Style="color: Black; font-family: Verdana;
                                                                        font-size: 12px;" Text='Display All Groups' OnClientClick="return ShowGroupPopup('../Popup/AllGroupsDetails.aspx');"  ></asp:LinkButton>                                 
                                     <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel" style="margin-left:10px;" Font-Bold="true"></asp:Label>
                                                <asp:TextBox ID="txtKey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                                    width: 280px" MaxLength="100"></asp:TextBox>
                                                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin"
                                                    onmouseover="Tip('Search')" onmouseout="UnTip()" />
                                </td>
                                
                            </tr>
                            <tr class="AlterNateColor2" id="trmsg" runat="server" visible="false">
                                <td colspan="3">
                                    <asp:Label ID="lblmsg" runat="server" Width="1070px" Style="text-align: center; font-size: 16px;
                                        font-weight: bold; color: Red;"> </asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor2">
                                <td colspan="3">
                                    <div style="overflow: auto; height: 450px; margin-left:0px;">
                                        <asp:GridView Width="1190px" CssClass="GrdUsers" runat="server" ID="grdCase" DataKeyNames="CASEID"
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
                                                <asp:TemplateField HeaderText="GROUP NAME" HeaderStyle-HorizontalAlign="center" SortExpression="GROUPNAME"
                                                    ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lnkGroId" runat="server" Width="140px" style="color:Black ;Font-family:Verdana;font-size:12px;text-decoration:underline;" Text='<%# bind("GROUPNAME")%>'></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CASE ID" HeaderStyle-HorizontalAlign="Left" SortExpression="CASEID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseID" runat="server" Text='<%# bind("CASEID")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PACKAGE FORMAT" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="CASEDE1">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPFormat" runat="server" Text='<%# bind("CASEDE1")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UNIQUE FEATURES" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="CASEDE2">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUfeature" runat="server" Text='<%# bind("CASEDE2")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Group" Visible="false" HeaderStyle-HorizontalAlign="center"
                                                    SortExpression="GROUPNAME" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:HyperLink Width="150px" ID="lnkGroup" Target="_blank" Enabled="true" runat="server"
                                                            ToolTip='<%# bind("GROUPNAME")%>' Text='<%# bind("GROUPNAME")%>' Style="color: Black;
                                                            font-weight: normal" AutoPostBack="false" NavigateUrl='<%# "~/Popup/CaseGroupDetails.aspx?groupId="+  Eval("GROUPID").toString()%>'></asp:HyperLink>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CASE DESCRIPTION" HeaderStyle-HorizontalAlign="Left"
                                                    Visible="true" SortExpression="CASEDE3">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCaseDes3" runat="server" Text='<%# bind("CASEDE3")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="CREATION DATE" HeaderStyle-HorizontalAlign="Left"
                                                    SortExpression="CREATIONDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbCDate" Width="70px" runat="server" Text='<%# bind("CREATIONDATE")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="80px" Wrap="true" HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="LAST UPDATE" HeaderStyle-HorizontalAlign="Left" SortExpression="SERVERDATE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbUDate" Width="70px" runat="server" Text='<%# bind("SERVERDATE")%>'></asp:Label>
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
                            <tr class="AlterNateColor1">
                                <td colspan="3">
                                    <asp:Button ID="btnCGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                        ToolTip="Create Group and Description" OnClientClick="return MakeVisible('trCreate');" />
                                    <asp:Button ID="btnEditGroup" runat="server" Text="Edit Group" CssClass="ButtonWMarigin"
                                        ToolTip="Edit Group and Description" OnClientClick="return OpenEGroupPopup('../Popup/EditGroup.aspx')" />
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" style="display: none;" id="trCreate" runat="Server">
                                <td colspan="3">
                                    <table>
                                        <tr>
                                            <td>
                                                <b>Group Name :</b>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtGroupDe1" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                    width: 250px" MaxLength="25"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <b>Group Description :</b>
                                            </td>
                                            <td>
                                                <asp:TextBox TextMode="MultiLine" ID="txtGroupDe2" runat="server" CssClass="MediumTextBox"
                                                    Style="text-align: left; height: 40px; width: 400px" MaxLength="100" onKeyUp="javascript:Count(this);"
                                                    onChange="javascript:Count(this);"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:Button ID="btnCreateGrp" runat="server" Text="Create Group" CssClass="ButtonWMarigin"
                                                    OnClientClick="return ValidateList();" />
                                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                                    OnClientClick="return MakeInVisible('trCreate');" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    <asp:HiddenField ID="hidGroupID" runat="server" />
    <asp:HiddenField ID="hidGroupDes" runat="server" />
    <asp:HiddenField ID="hvUserGrd" runat="server" />
    </form>
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
</body>
</html>