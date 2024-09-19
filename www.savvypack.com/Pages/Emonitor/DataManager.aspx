<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DataManager.aspx.vb" Inherits="Pages_DataManager_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>GHG releases by SKU</title>
    <link href="../../App_Themes/SkinFile/SavvyPackNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        javascript: window.history.forward(1);

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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Universal Uploadfile Popup
        function ShowPopWindow2(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 750;
            var height = 450;
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        //Advance Status popup
        function ShowPopWindowStatus(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 440;
            var height = 360;
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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

        function ShowPopWindowCreate(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 635;
            var height = 360;
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
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
    <form id="form1" runat="server">
    <center>
        <div>
            <div>
                <div id="AlliedLogo">
                    <table>
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
                <div id="error">
                    <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                </div>
            </div>
            <table class="ContentPage" id="ContentPage" runat="server">             
                <tr style="height: 20px">
                    <td>
                        <div id="SavvyContentPagemargin" runat="server">
                            <div id="SavvyPageSection1" style="text-align: left;">
                                <table style="width: 100%">
                                    <tr>
                                        <td align="center" style="font-weight: bold; font-size: 20px">
                                            GHG releases by SKU
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="font-size: 11px; width: 30%;">
                                            <asp:Label ID="lblPageSizeC" runat="server" Text="Page Size:" Font-Bold="true" Style="text-align: right;
                                                padding-left: 5px;"></asp:Label>
                                            <asp:DropDownList ID="ddlPageCountC" runat="server" Width="55px" CssClass="DropDown"
                                                AutoPostBack="true">
                                                <asp:ListItem Value="1">10</asp:ListItem>
                                                <asp:ListItem Value="2">25</asp:ListItem>
                                                <asp:ListItem Value="3">50</asp:ListItem>
                                                <asp:ListItem Value="4" Selected="True" >100</asp:ListItem>
                                                <asp:ListItem Value="5">200</asp:ListItem>
                                                <asp:ListItem Value="6">300</asp:ListItem>
                                                <asp:ListItem Value="7">400</asp:ListItem>
                                                <asp:ListItem Value="8">500</asp:ListItem>
                                                <asp:ListItem Value="9">1000</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:Label ID="lblRF" runat="server" Text="SKUs:" Font-Bold="true" Style="text-align: right;
                                                padding-left: 10px;"></asp:Label>
                                            <asp:Label ID="lblRecondCnt" runat="server" CssClass="NormalLabel" ForeColor="Red"
                                                Font-Bold="true"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr class="AlterNateColor1">
                                        <td>
                                            <asp:Label ID="lblMsg" runat="server" CssClass="CalculatedFeilds" Visible="false"
                                                Text="You currently have no data to display." Font-Size="11" Style="margin-top: 30px;
                                                margin-left: 500px; color: red; font-weight: bold;"></asp:Label>
                                            <asp:GridView Width="1340px" CssClass="grdProject" runat="server" ID="grdSpecs" DataKeyNames="SPECSDATAID"
                                                AllowPaging="true" PageSize="10" AllowSorting="True" AutoGenerateColumns="False"
                                                Font-Size="11px" CellPadding="4" ForeColor="Black" GridLines="Vertical" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px">
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
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SKU" HeaderStyle-HorizontalAlign="center" SortExpression="SKU">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSKU" runat="server" Visible="true" Text='<%# bind("SKU")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="50px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="SPEC#" HeaderStyle-HorizontalAlign="center" SortExpression="SPECNUM">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSPEC" runat="server" Visible="true" Text='<%# bind("SPECNUM")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Package Type" HeaderStyle-HorizontalAlign="center"
                                                        SortExpression="PACKTYPE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblPACKTYPE" runat="server" Visible="true" Text='<%# bind("PACKTYPE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="90px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Package Discription" HeaderStyle-HorizontalAlign="center" SortExpression="LABELTYPE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLABELTYPE" runat="server" Visible="true" Text='<%# bind("LABELTYPE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="180px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Business Unit" HeaderStyle-HorizontalAlign="center"
                                                        SortExpression="BUSINESSUNIT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBUSINESSUNIT" runat="server" Visible="true" Text='<%# bind("BUSINESSUNIT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="180px" Wrap="true" HorizontalAlign="left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Brand" HeaderStyle-HorizontalAlign="center" SortExpression="BRAND">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBRAND" runat="server" Visible="true" Text='<%# bind("BRAND")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" Wrap="true" HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Volume (number)" HeaderStyle-HorizontalAlign="center"
                                                        SortExpression="VOLUME">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblVOLUME" runat="server" Visible="true" Text='<%# bind("VOLUME")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Length (in)" HeaderStyle-HorizontalAlign="center" SortExpression="LNGTH">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblLNGTH" runat="server" Visible="true" Text='<%# bind("LNGTH")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Width (in)" HeaderStyle-HorizontalAlign="center" SortExpression="WIDTH">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWIDTH" runat="server" Visible="true" Text='<%# bind("WIDTH")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Area (in<sup>2)" HeaderStyle-HorizontalAlign="center" SortExpression="AREA">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblAREA" runat="server" Visible="true" Text='<%# bind("AREA")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="60px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Weight (lb)" HeaderStyle-HorizontalAlign="center"
                                                        SortExpression="WEIGHT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblWEIGHT" runat="server" Visible="true" Text='<%# bind("WEIGHT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="70px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Greenhouse Gases (lb GHG/lb Package)" HeaderStyle-HorizontalAlign="center"
                                                        SortExpression="GHGRELEASE">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGHGRELEASE" runat="server" Visible="true" Text='<%# bind("GHGRELEASE")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" Wrap="true" HorizontalAlign="right" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Greenhouse Gases (lb)" HeaderStyle-HorizontalAlign="center"
                                                        SortExpression="GHGTOT">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblGHGTOT" runat="server" Visible="true" Text='<%# bind("GHGTOT")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="140px" Wrap="true" HorizontalAlign="right" />
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
                <tr class="AlterNateColor3">
                    <td class="PageSHeading" align="center">
                        <asp:Label ID="lblTag" runat="Server"></asp:Label>
                    </td>
                </tr>
            </table>          
            <asp:HiddenField ID="hidSortId" runat="server" />
            <asp:HiddenField ID="hidDesc" runat="server" />
            <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </center>
    </form>
</body>
</html>
