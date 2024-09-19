<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BulkModelManagement.aspx.vb" Inherits="Pages_Econ1_BulkModelManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Bulk Model Management</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <!-- ModalPopupExtender -->
    <script type="text/javascript" language="javascript">
        function ClosePopUp() {
            window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_loading').style.display = "inline";
            window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_lnkSelBulkModel').style.display = "none";
            window.close();
            window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_btnUpdateBulk').click();
        }
        function ClosePopUp1() {
            window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_loading').style.display = "inline";
            window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_lnkSelBulkModel').style.display = "none";
            window.close();
            window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_btnUpdateBulk1').click();
        }
    </script>
    <style type="text/css">
        .modalBackground {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }

        .modalPopup {
            background-color: #ffffdd;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 3px;
            width: 250px;
        }

        .breakword {
            word-wrap: break-word;
            word-break: break-all;
        }
    </style>
    <!-- ModalPopupExtender -->
    <script type="text/javascript" language="javascript">

        window.onbeforeunload = WindowCloseHanlder;
        function WindowCloseHanlder() {
            if (document.getElementById("hdnUpdate").value == "0") {
                window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_lnkSelBulkModel').style.display = "inline";
            }
        }

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked
                        //check all checkboxes
                        inputList[i].checked = true;
                    }

                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        inputList[i].checked = false;

                    }

                }

            }

        }
        function checkAllG(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked
                        //check all checkboxes
                        inputList[i].checked = true;
                    }

                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        inputList[i].checked = false;

                    }

                }

            }

        }
        function ShowToolTip(ControlId, Message) {

            document.getElementById(ControlId).onmouseover = function () { Tip(Message); };
            document.getElementById(ControlId).onmouseout = function () { UnTip(''); };

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div class="ContentPage" runat="server" style="width: 1350px;">
            <div id="AlliedLogo">
                <table>
                    <tr>
                        <td class="PageSHeading" align="center">
                            <table style="width: 845px; background-color: #edf0f4;">
                                <tr>
                                    <td align="left">
                                        <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:Label ID="ErrorLable" runat="server"></asp:Label>
            </div>

            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <fieldset id="fieldsetGroups" style="width: 1250px; height: 590px; vertical-align: top; overflow: auto; border-color: Black; border-width: 1px; margin-left: 5px;"
                            runat="server">
                            <table style="width: 100%;">
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size: 14px; padding-left: 5px;" colspan="2">Manage Groups
                                    </td>
                                </tr>
                                <tr class="AlterNateColor2">
                                    <td>
                                        <asp:Label ID="lblGroup" Text="Select Type:" CssClass="NormalLabel" Font-Bold="true"
                                            runat="server" Style="vertical-align: top;"> </asp:Label>
                                        <asp:DropDownList ID="ddlGroup" runat="server" CssClass="DropDown" AutoPostBack="true"
                                            Style="width: 200px; margin-left: 40px;">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor2">
                                    <td>
                                        <asp:Label ID="lblG" Text="Type Description:" CssClass="NormalLabel" Font-Bold="true"
                                            runat="server" Style="vertical-align: top;"> </asp:Label>

                                        <asp:TextBox ID="txtGrpDesc" runat="server" Enabled="false" Height="60px" Width="400px"
                                            TextMode="MultiLine" Style="margin-left: 10px;"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor2">
                                    <td style="padding-top: 10px;">
                                        <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="true" Style="text-align: right;"></asp:Label>
                                        <asp:DropDownList ID="ddlSizeG" runat="server" Width="55px" CssClass="DropDown"
                                            AutoPostBack="true">
                                            <asp:ListItem Value="1">10</asp:ListItem>
                                            <asp:ListItem Value="2">25</asp:ListItem>
                                            <asp:ListItem Value="3">50</asp:ListItem>
                                            <asp:ListItem Value="4" Selected="True">100</asp:ListItem>
                                            <asp:ListItem Value="5">500</asp:ListItem>
                                            <asp:ListItem Value="6">1000</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lblRF" runat="server" Text="Record(s):" Font-Bold="true" Style="text-align: right; padding-left: 10px;"></asp:Label>
                                        <asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel" ForeColor="Red"
                                            Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="AlterNateColor2">
                                        <div style="width: 1220px; overflow: auto; height: 400px; margin-left: 0px;">
                                            <asp:Label ID="lblMsg" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                Text="You currently have no item to display." Font-Size="11" Style="margin-top: 30px; margin-left: 0px; color: red; font-weight: bold;"></asp:Label>

                                            <asp:GridView CssClass="GrdUsers" runat="server" ID="GrdCase" DataKeyNames="CASEID"
                                                AllowPaging="true" PageSize="100" AllowSorting="True" AutoGenerateColumns="False"
                                                Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" Width="1200px">
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
                                                <RowStyle BackColor="#F7F7DE" />
                                                <PagerStyle CssClass="AlterNateColor1" Font-Bold="true" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="USERID" Visible="false" SortExpression="UserId">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUserId" runat="server" Text='<%# bind("USERID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SELECT CASES">
                                                        <HeaderTemplate>
                                                            <input id="HeaderLevelCheckBox" onclick="javascript: checkAllG(this);" runat="server"
                                                                type="checkbox" />
                                                        </HeaderTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="SelectGroup" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20px" Wrap="true" HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="USER" HeaderStyle-HorizontalAlign="Left" SortExpression="USERNAME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblUser" Width="140px" runat="server" Text='<%# bind("USERNAME")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CASE ID" HeaderStyle-HorizontalAlign="Left" SortExpression="CASEID">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseID" Width="60px" runat="server" Text='<%# bind("CASEID")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PACKAGE FORMAT" HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="CASEDE1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPFormat" Width="160px" runat="server" Text='<%# bind("CASEDE1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="160px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="CASE DESCRIPTION" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="true" SortExpression="CASEDE3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCaseDes3" Width="320px" runat="server" Text='<%# bind("CASEDE3")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="320px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Group Des" HeaderStyle-HorizontalAlign="Left"
                                                        Visible="true" SortExpression="DES1">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgepDES" Width="160px" runat="server" Text='<%# Bind("DES1")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="160px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="CREATION DATE" HeaderStyle-HorizontalAlign="Left"
                                                        SortExpression="CDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbCDate" runat="server" Width="140px" Text='<%# bind("CREATIONDATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="LAST UPDATE" HeaderStyle-HorizontalAlign="Left" SortExpression="SDATE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbUDate" Width="140px" runat="server" Text='<%# bind("SERVERDATE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr class="AlterNateColor2">
                    <td colspan="2">
                        <div>
                            <div style="text-align: center;">
                                <asp:Button ID="btnSubmit2" runat="server" Text="Submit" CssClass="ButtonWMarigin" />
                                <asp:Button ID="btn0" runat="server" Style="display: none;" />
                            </div>
                        </div>
                    </td>
                </tr>
                <tr class="AlterNateColor3">
                    <td colspan="2" class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>
            <div>
                <ajaxToolkit:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btn0"
                    BackgroundCssClass="modalBackground">
                </ajaxToolkit:ModalPopupExtender>
                <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" Width="700px" align="center" Font-Names="Verdana"
                    Style="display: none">
                    <div class="PageHeading" style="background-color: #F1F1F2;">
                        <asp:Label ID="lblHeading" runat="server">Confirmation Popup</asp:Label>
                    </div>
                    <div class="AlterNateColor4" style="vertical-align: middle; font-weight: bold; font-size: 16px; text-align: left;">
                        <asp:Label ID="lblLHeading" runat="server" Text="List of Cases:"></asp:Label>
                    </div>
                    <br />
                    <div>
                        <div style="vertical-align: middle; text-align: left; margin-left: 10px;">
                            <asp:Label ID="lblInfo" runat="server" Font-Size="14px"></asp:Label>
                        </div>
                        <br />
                        <div class="breakword" style="vertical-align: middle; text-align: left; margin-left: 10px; height: 120px; overflow: auto;">
                            <asp:Label ID="lblList" runat="server" Style="margin-top: 30px;"></asp:Label>
                        </div>
                        <br />
                        <div style="vertical-align: bottom; text-align: left; margin-left: 10px;">
                            <asp:Label ID="lblOption" runat="server" Font-Size="14px"></asp:Label>
                        </div>
                        <br />
                        <div style="vertical-align: bottom; font-weight: bold; text-align: left; margin-left: 10px;">
                            <asp:Label ID="lblWarning" runat="server" ForeColor="Red" Font-Size="14px"></asp:Label>
                        </div>
                        <br />
                        <div style="text-align: center; padding-bottom: 5px;">
                            <asp:Button ID="btnTransfer" runat="server" Text="Transfer" Width="80px" />
                            <ajaxToolkit:ConfirmButtonExtender ID="cnfTrans" ConfirmText="Are you sure you want to Transfer variables?"
                                runat="server" TargetControlID="btnTransfer" Enabled="True">
                            </ajaxToolkit:ConfirmButtonExtender>
                            <asp:Button ID="btnTransfCalc" runat="server" Text="Transfer and Calculate" Width="180px"
                                Style="margin-left: 20px;" />
                            <ajaxToolkit:ConfirmButtonExtender ID="cnfTransCalc" ConfirmText="Are you sure you want to Transfer & Calculate variables?"
                                runat="server" TargetControlID="btnTransfCalc" Enabled="True">
                            </ajaxToolkit:ConfirmButtonExtender>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="80px" Style="margin-left: 20px;" />
                        </div>
                    </div>
                </asp:Panel>
            </div>
        </div>
        <asp:HiddenField ID="hidSortIdC" runat="server" />
        <asp:HiddenField ID="hidSortId" runat="server" />
        <asp:HiddenField ID="hdnUpdate" runat="server" Value="0" />
    </form>
</body>
</html>
