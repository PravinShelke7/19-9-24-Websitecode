<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CreateRFP.aspx.vb" Inherits="Pages_SavvyPackPro_CreateRFP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Create </title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
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
        
        .breakword
        {
            word-wrap: break-word;
            word-break: break-all;
        }
        
        a.SavvyLink:link
        {
            font-family: Verdana;
            color: black;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            font-family: Verdana;
            color: Black;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            font-family: Verdana;
            color: Red;
            font-size: 11px;
        }
        
        .SingleLineTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 14px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }
        
        .AlternateColorAct1
        {
            font-family: Verdana;
            background-color: #dfe8ed;
        }
        
        .MultiLineTextBoxG
        {
            font-family: Verdana;
            font-size: 10px;
            width: 320px;
            height: 50px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
        
        .SingleLineTextBox_G
        {
            font-family: Verdana;
            font-size: 10px;
            width: 240px;
            height: 15px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: Left;
            background-color: #FEFCA1;
        }
        
        .divUpdateprogress_SavvyPro
        {
            left: 210px;
            top: 300px;
            position: absolute;
        }
    </style>
    <script type="text/JavaScript">
        //javascript: window.history.forward(1);

        function RefreshPage() {
            window.location.reload="/default.aspx";

            
        }

        function ClosePage() {
            // alert("RFP created successfully.");
            window.opener.document.getElementById('btnRefresh').click();
            var TypeRFP = document.getElementById("tabCreateRFP_tabBMConfigVendor_rdbtnRFP").checked;
            var TypeRFI = document.getElementById("tabCreateRFP_tabBMConfigVendor_rdbtnRFI").checked;

            if (TypeRFP == true) {
                alert("RFP created successfully.");
            }
            else if (TypeRFI == true) {
                alert("RFI created successfully.");
            }
            window.location.reload();
        }

        function ClosePage1() {

            var TypeRFP = document.getElementById("tabCreateRFP_tabBMConfigVendor_rdbtnRFP").checked;
            var TypeRFI = document.getElementById("tabCreateRFP_tabBMConfigVendor_rdbtnRFI").checked;

            if (TypeRFP == true) {
                alert("RFP Updated successfully.");
            }
            else if (TypeRFI == true) {
                alert("RFI Updated successfully.");
            }
            window.location.reload();
        }


        function ValidateList() {

            var name = document.getElementById("tabCreateRFP_tabBMConfigVendor_txtRFPNm").value;
            var TypeRFP = document.getElementById("tabCreateRFP_tabBMConfigVendor_rdbtnRFP").checked;
            var TypeRFI = document.getElementById("tabCreateRFP_tabBMConfigVendor_rdbtnRFI").checked;



            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;

            }
            else if ((TypeRFP == false) && (TypeRFI == false)) {
                var msg1 = "-------------------------------------------------\n";
                msg1 += "    Please Select Type.\n";
                msg1 += "-------------------------------------------------\n";
                alert(msg1);
                return false;

            }

            else {

                return true;
            }

        }

        function CheckSP(text) {
            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }
       
    </script>
     <%--<meta http-equiv="refresh" content="3" />--%> <%--'dt--%>
</head>
<body>

    <script type="text/javascript" src="../../javascripts/wz_tooltip.js"></script>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <asp:Image ImageAlign="AbsMiddle" Width="1350px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                <ProgressTemplate>
                    <div class="divUpdateprogress_SavvyPro">
                        <table>
                            <tr>
                                <td>
                                    <img alt="" src="../../Images/Loading4.gif" height="50px" />
                                </td>
                                <td>
                                    <b style="color: Red;">Updating the Record</b>
                                </td>
                            </tr>
                        </table>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <table style="width: 100%; height: 932px;">
                <tr class="AlterNateColor2">
                    <td style="padding-left: 10px; padding-top: 15px;">
                        <ajaxToolkit:TabContainer ID="tabCreateRFP" Height="850px" runat="server" ActiveTabIndex="0"
                            AutoPostBack="true">
                            <ajaxToolkit:TabPanel runat="server" HeaderText="Create" ID="tabBMConfigVendor">
                                <ContentTemplate>
                                    <table style="width: 100%; font-family: Verdana; font-size: 14px;">
                                        <tr valign="top" style="background-color: #dfe8ed;">
                                            <td>
                                                <table style="width: 100%;">
                                                    <tr id="trCreate" runat="server">
                                                        <td id="Td1" runat="server">
                                                            <table style="width: 97%; background-color: #7F9DB9; margin-left: 10px;">
                                                                <tr>
                                                                    <td style="width: 20%;">
                                                                        <b>Name:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtRFPNm" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                                            width: 250px" MaxLength="25" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%;">
                                                                        <b>Description:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox TextMode="MultiLine" ID="txtRfpDes" runat="server" CssClass="MediumTextBox"
                                                                            Style="text-align: left; height: 40px; width: 380px" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%;">
                                                                        <b>Select Type:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="rdbtnRFP" Font-Size="12px" runat="server" GroupName="Create">
                                                                        </asp:RadioButton><asp:Label ID="lblRFP" runat="server" Text="RFP" Font-Bold="True"
                                                                            Font-Size="13px"></asp:Label><asp:RadioButton ID="rdbtnRFI" Font-Size="12px" runat="server"
                                                                                GroupName="Create"></asp:RadioButton><asp:Label ID="lblRFI" runat="server" Text="RFI"
                                                                                    Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%;">
                                                                        <b>Select Preferred Units:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:RadioButton ID="rdEnglish" Font-Size="12px" runat="server" GroupName="Pref">
                                                                        </asp:RadioButton><asp:Label ID="lblEngUnits" runat="server" Text="English units "
                                                                            Font-Bold="True" Font-Size="13px"></asp:Label><asp:RadioButton ID="rdMetric" Font-Size="12px"
                                                                                runat="server" GroupName="Pref"></asp:RadioButton><asp:Label ID="lblMetricUnits"
                                                                                    runat="server" Text="Metric units " Font-Bold="True" Font-Size="13px"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 23%;">
                                                                        <b>Select Preferred Currency:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlCurrancy" CssClass="DropDown" Width="125px" runat="server">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="width: 20%;">
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btnCreateRFP" runat="server" Text="Create " CssClass="ButtonWMarigin"
                                                                            OnClientClick="return ValidateList();" /><asp:Button ID="btnEditRFP" runat="server"
                                                                                Text="Update " Visible="False" CssClass="ButtonWMarigin" OnClientClick="return ValidateList();" /><asp:Button
                                                                                    ID="btnCancel" runat="server" Text="Cancel" Visible="False" CssClass="ButtonWMarigin" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding-left: 10px;">
                                                            <table>
                                                                <tr>
                                                                    <td>
                                                                        <table style="width: 100%;">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox
                                                                                        ID="txtKey" runat="server" CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button
                                                                                            ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin" />
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="True" Style="text-align: right;
                                                                                        font-size: 12px;"></asp:Label><asp:DropDownList ID="ddlPageCountC" runat="server"
                                                                                            Width="55px" CssClass="DropDown" AutoPostBack="True">
                                                                                            <asp:ListItem Value="1">10</asp:ListItem>
                                                                                            <asp:ListItem Value="2">25</asp:ListItem>
                                                                                            <asp:ListItem Value="3">50</asp:ListItem>
                                                                                            <asp:ListItem Value="4">100</asp:ListItem>
                                                                                            <asp:ListItem Value="5">200</asp:ListItem>
                                                                                            <asp:ListItem Value="6">300</asp:ListItem>
                                                                                            <asp:ListItem Value="7">400</asp:ListItem>
                                                                                            <asp:ListItem Value="8">500</asp:ListItem>
                                                                                            <asp:ListItem Value="9">1000</asp:ListItem>
                                                                                        </asp:DropDownList>
                                                                                    <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="True" Style="text-align: right;
                                                                                        padding-left: 20px;"></asp:Label><asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel"
                                                                                            ForeColor="Red" Font-Bold="True"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblSpecNoFound" runat="server" Visible="False" Text="No records found."></asp:Label>
                                                                        <asp:GridView Width="950px" CssClass="grdProject" runat="server" ID="grdTDetails"
                                                                            DataKeyNames="ID" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                                                                            Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
                                                                            <PagerSettings Position="Top" />
                                                                            <FooterStyle BackColor="#CCCC99" />
                                                                            <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                                            <Columns>
                                                                                <asp:TemplateField HeaderText="USERID" Visible="False" SortExpression="USERID">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUserID" Width="50px" runat="server" Text='<%# bind("USERID")%>'></asp:Label>
                                                                                        <asp:Label ID="lblStatusId" runat="server" Visible="false" Text='<%# bind("STATUSID")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="20px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField>
                                                                                    <ItemTemplate>
                                                                                        <asp:LinkButton ID="lnkSelect" runat="server" Width="100px" Style="color: Black;
                                                                                            font-family: Verdana; font-size: 11px; text-decoration: underline;" Text="Select"
                                                                                            CommandName="Select" CommandArgument='<%# Eval("ID") %>'></asp:LinkButton>
                                                                                        <asp:Label ID="lblSelect" runat="server" Visible="false" Text='<%# bind("ID")%>'></asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="140px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Name" SortExpression="NAME">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRfpNm" runat="server" Text='<%# Bind("NAME")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="130px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Type" SortExpression="TYPE">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblT" runat="server" Text='<%# Bind("TYPE")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="100px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Description" SortExpression="DES">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblRFPDes" runat="server" Text='<%# Bind("DES")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="260px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Owner" SortExpression="OWNER">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblOwner" runat="server" Text='<%# Bind("OWNER")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="260px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Creation Date" SortExpression="CREATIONDATE">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCD" runat="server" Text='<%# Bind("CREATIONDATE")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="260px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Update Date" SortExpression="UPDATEDATE">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUpD" runat="server" Text='<%# Bind("UPDATEDATE")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="260px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Units" SortExpression="UNITS">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblUnits" runat="server" Text='<%# Bind("UNITS")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="280px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField HeaderText="Currency" SortExpression="CURR">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="lblCurr" runat="server" Text='<%# Bind("CURR")%>'></asp:Label></ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle Width="280px" Wrap="True" HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                                                                HorizontalAlign="Left" />
                                                                            <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                            <AlternatingRowStyle BackColor="White" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </ContentTemplate>
                            </ajaxToolkit:TabPanel>
                        </ajaxToolkit:TabContainer>
                    </td>
                </tr>
            </table>
            <br />
            <asp:HiddenField ID="hidSortIdBSpec" runat="server" />
            <asp:Button ID="btnRefreshCreate" runat="server" Style="display: none;" />
            <asp:HiddenField ID="hidRfpID" runat="server" />
            <asp:HiddenField ID="hidRfpNm" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    </form>
</body>
</html>
