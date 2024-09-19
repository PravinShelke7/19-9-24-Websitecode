<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VendorSelection.aspx.vb" Inherits="Pages_SavvyPackPro_Charts_PopUp_VendorSelection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title> Select Vendors</title>
     <link href="../../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function CheckAllTab(Checkbox) {
            var grid1 = document.getElementById("<%=grdUsers.ClientID %>");
            for (i = 1; i < grid1.rows.length; i++) {
                grid1.rows[i].cells[0].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
            return false;
        }
        function ClosePage(vendors,ids) {

            window.opener.document.getElementById("lnkvendor").innerText = vendors;
            window.opener.document.getElementById("lnkvendorLine").innerText = vendors;
            window.opener.document.getElementById("lnkVendorB").innerText = vendors;

            window.opener.document.getElementById("hidvendordes").value = vendors;
            window.opener.document.getElementById("hidvendorid").value = ids;
            //window.opener.document.getElementById("btnRefresh").click(); 

            window.close();
        }
    </script>
    <script type="text/JavaScript">
        function CheckSP(text, hidvalue) {
            var sequence = document.getElementById(text.id).value;
            if (sequence != "") {
                if (isNaN(sequence)) {
                    alert("Sequence must be in number");
                    document.getElementById(text.id).value = "";
                    document.getElementById(text.id).value = hidvalue;
                    return false;
                }
            }
            else {
                alert("Please enter sequence");
                document.getElementById(text.id).value = hidvalue;
                return false;
            }

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
            else {
                document.getElementById('btnSeq').click();
                return true;
            }
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
            left: 610px;
            top: 400px;
            position: absolute;
        }
    </style>
    <script type="text/JavaScript">
        javascript: window.history.forward(1);
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
            var name = document.getElementById("txtRFPNm").value;
            if (name == "") {
                var msg = "-------------------------------------------------\n";
                msg += "    Please enter RFP Name.\n";
                msg += "-------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                return true;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
    <div>
     <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <div class="PageHeading" id="div10" runat="server" style="text-align: center; height: 35px;
                                            width: 600px; background-color: #F1F1F2;">
                                            Select vendors
                                        </div>
                                        <table id="tblBMConfigVendor" runat="server" style="margin-top: 5px; background-color: #F1F1F2;">
                                            <tr id="Tr1" runat="server">
                                                <td id="Td1" runat="server">
                                                    
                                                </td>
                                            </tr>
                                            <tr id="Tr2" runat="server">
                                                <td id="Td2" runat="server">
                                                    <table style="width: 100%;">
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
                                                                    padding-left: 20px;"></asp:Label><asp:Label ID="lblRecondCnt" 
                                                                    runat="server" CssClass="NormalLabel"
                                                                        ForeColor="Red" Font-Bold="True"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" Style="margin-left: 80px;"
                                                                    Font-Bold="True" Font-Size="13px"></asp:Label><asp:TextBox ID="txtKey" runat="server"
                                                                        ToolTip="Enter EmailID/CompanyName/FirstName/LastName to search vendor."
                                                                        CssClass="LongTextBox" MaxLength="50"></asp:TextBox><asp:Button ID="btnSearch1" runat="server"
                                                                            Text="Search" CssClass="ButtonWMarigin" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="Tr3" runat="server">
                                                <td id="Td3" style="padding-top: 10px;" runat="server">
                                                    <div id="ContentPagemargin" style="overflow: auto; height: 350px;" runat="server">
                                                        <asp:Label ID="lblNOVendor" runat="server" CssClass="CalculatedFeilds" Visible="False"
                                                            Text="No record found."></asp:Label>
                                                        <asp:GridView Width="550px" CssClass="grdProject"
                                                                runat="server" ID="grdUsers" DataKeyNames="VENDORID" AllowPaging="True"
                                                                AllowSorting="True" AutoGenerateColumns="False" Font-Size="11px" CellPadding="4"
                                                                ForeColor="Black" GridLines="Vertical" BackColor="White" BorderColor="#DEDFDE"
                                                                BorderStyle="None" BorderWidth="1px">
                                                                <PagerSettings Position="Top" />
                                                                <PagerTemplate>
                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Page" CommandArgument="First"
                                                                        Style="color: Black;">First</asp:LinkButton><asp:Label ID="pmore" runat="server"
                                                                            Text="..."></asp:Label><asp:LinkButton ID="LinkButton2" runat="server" Style="color: #284E98;"
                                                                                CommandName="Page" CommandArgument="Prev">Previous</asp:LinkButton><asp:LinkButton
                                                                                    ID="p0" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                        ID="p1" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                            ID="p2" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:Label
                                                                                                ID="CurrentPage" runat="server" Text="Label" Style="color: Red;"></asp:Label><asp:LinkButton
                                                                                                    ID="p4" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                        ID="p5" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                            ID="p6" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                                ID="p7" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                                    ID="p8" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                                        ID="p9" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                                            ID="p10" runat="server" Style="color: Blue;">LinkButton</asp:LinkButton><asp:LinkButton
                                                                                                                                ID="LinkButton3" runat="server" CommandName="Page" CommandArgument="Next" Style="color: #284E98;">Next</asp:LinkButton><asp:Label
                                                                                                                                    ID="nmore" runat="server" Text="..."></asp:Label><asp:LinkButton ID="LinkButton4"
                                                                                                                                        runat="server" CommandName="Page" CommandArgument="Last" Style="color: Black;">Last</asp:LinkButton>
                                                                </PagerTemplate>
                                                                <FooterStyle BackColor="#CCCC99" />
                                                                <RowStyle BackColor="#F7F7DE" CssClass="row" />
                                                                <Columns>
                                                                    <asp:TemplateField>
                                                                        <HeaderTemplate>
                                                                            <input id="HeaderLevelCheckBox" onclick="javascript: CheckAllTab(this);"
                                                                                runat="server" type="checkbox" />
                                                                        </HeaderTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="select" runat="server"  >
                                                                            </asp:CheckBox>
                                                                        </ItemTemplate>
                                                                        <ItemStyle Width="10px" Wrap="True" HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="VENDORID" SortExpression="VENDORID" Visible="False">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblMUsrID" runat="server" Visible="False" Text='<%# Bind("VENDORID")%>'></asp:Label>
                                                                            <asp:Label ID="lblEmailID" runat="server" Visible="False" Text='<%# Bind("EMAILADDRESS")%>'></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:TemplateField>
                                                                    <asp:BoundField DataField="EMAILADDRESS" HeaderText="User Name" SortExpression="EMAILADDRESS">
                                                                        <ItemStyle Width="200px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="FULLNAME" HeaderText="Contact Name" SortExpression="FULLNAME">
                                                                        <ItemStyle Width="140px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="COMPANYNAME" HeaderText="Company" SortExpression="COMPANYNAME">
                                                                        <ItemStyle Width="180px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="COUNTRY" HeaderText="Country" SortExpression="COUNTRY">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="STATE" HeaderText="State" SortExpression="STATE">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="CITY" HeaderText="City" SortExpression="CITY">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                    <asp:BoundField DataField="ZIPCODE" HeaderText="ZIPCODE" SortExpression="ZIPCODE">
                                                                        <ItemStyle Width="80px" CssClass="breakword" Wrap="True" HorizontalAlign="Left" />
                                                                        <HeaderStyle HorizontalAlign="Left" />
                                                                    </asp:BoundField>
                                                                </Columns>
                                                                <PagerStyle Font-Underline="False" BackColor="#F7F7DE" ForeColor="DarkBlue" Font-Bold="True"
                                                                    HorizontalAlign="Left" />
                                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                                <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                                <AlternatingRowStyle BackColor="White" />
                                                            </asp:GridView>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" />
        <asp:HiddenField ID="hidSortIdBMVendor" runat="server" />
        <asp:HiddenField ID="hidVendorId" runat="server" />
        <asp:HiddenField ID="hidVendorDes" runat="server" />
        <asp:HiddenField ID="hidlnkDes" runat="server" />
    </div>
    </form>
</body>
</html>
