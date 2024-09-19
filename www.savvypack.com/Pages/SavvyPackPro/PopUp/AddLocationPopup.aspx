<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddLocationPopup.aspx.vb"
    Inherits="AddLocationPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <link href="../../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
       <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 600px; text-align: center;">
                        <asp:Label ID="lblHeading" Text="Add location Information" runat="server"></asp:Label>
                    </div>
                    <div id="error">
                        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <div id="PageSection1" style="text-align: left;">
                            <table width="90%">
                                <tr class="AlterNateColor1" id="trNum" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="lblNumber" runat="server" Text="Company:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="TextBox1" ToolTip="Create an easy-to-remember name for your project"
                                            CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 25%">
                                        <asp:Label ID="lblUserN" runat="server" Text="Bussiness Unit:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="TextBox2" ToolTip="Create an easy-to-remember name for your project"
                                            CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 25%">
                                        <asp:Label ID="lblWord0" runat="server" Text="Brief Description:" Font-Bold="true"
                                            ToolTip="Provide some unique features, such as a rare material or a special design."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="TextBox3" runat="server" CssClass="SavvyMediumTextBox" TextMode="MultiLine"
                                            MaxLength="1000" Width="70%" Height="80px" ToolTip="Provide a detail description for the project if you like."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 25%">
                                        <asp:Label ID="lblWord" runat="server" Text="Address:" Font-Bold="true" ToolTip="Provide some unique features, such as a rare material or a special design."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="txtDesc0" runat="server" CssClass="SavvyMediumTextBox" Height="80px"
                                            MaxLength="1000" TextMode="MultiLine" ToolTip="Provide a detail description for the project if you like."
                                            Width="70%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" id="trAnalysis" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label3" runat="server" Text="Country" Font-Bold="true" ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <%--<asp:TextBox ID="TextBox4" ToolTip="Create an easy-to-remember name for your project"
                                        CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>--%>
                                        <%--<asp:LinkButton ID="lnkState" runat="server" Text="Select State" Style="color: Black;"
                                OnClientClick=""></asp:LinkButton>--%>
                                        <asp:DropDownList ID="ddlCountryN" runat="server" CssClass="DropDown" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" id="tr1" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label1" runat="server" Text="State" Font-Bold="true" ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <%--<asp:LinkButton ID="lnkCity" runat="server" Text="Select City" Style="color: Black;"
                                            OnClientClick=""></asp:LinkButton>--%>
                                            <asp:DropDownList ID="ddlStateD" runat="server" CssClass="DropDown" AutoPostBack="true" >
                                                                   </asp:DropDownList> 
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" id="tr2" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label2" runat="server" Text="City:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="TextBox4" ToolTip="Create an easy-to-remember name for your project"
                                            CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" id="tr3" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label4" runat="server" Text="Zipcode:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="TextBox5" ToolTip="Create an easy-to-remember name for your project"
                                            CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 15%">
                                    </td>
                                    <td style="width: 88%">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </td>
            </tr>
            <%--<tr class="AlterNateColor3">
            <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server"></asp:Label>
            </td>
        </tr>--%>

        </table>
    </div>
    </form>
</body>
</html>
