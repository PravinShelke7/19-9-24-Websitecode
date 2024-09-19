<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RFPSearch.aspx.vb" Inherits="Pages_Spec_CasesSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function CaseSearch(RFPDes, RFPid) 
        {           
            var hidRFPDes = document.getElementById('<%= hidRFPDes.ClientID%>').value;
            var hidRFPid = document.getElementById('<%= hidRFPid.ClientID%>').value;
            var hidRFPidD = document.getElementById('<%= hidRFPidD.ClientID%>').value;

            if (RFPid == "0") 
            {
                window.opener.document.getElementById(hidRFPDes).innerText = "Select RFP";
                window.opener.document.getElementById(hidRFPidD).value = "Select RFP";
            }
            else 
            {
                 window.opener.document.getElementById(hidRFPDes).innerText = RFPDes ;
                window.opener.document.getElementById(hidRFPidD).value = RFPDes ;
            }
           window.opener.document.getElementById(hidRFPid).value = RFPid;
            window.close();
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
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td align="right">
                        <b>Description 1:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Description 2:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 50px" />
                    </td>
                </tr>
            </table>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="width: 750px; height: 280px; overflow: auto;">
                <asp:GridView Width="400px" runat="server" ID="grdRFPSearch" DataKeyNames="RFPID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                    <Columns>
                        <asp:BoundField DataField="RFPID" HeaderText="RFPID" SortExpression="RFPID" Visible="false">
                        </asp:BoundField>
                         <asp:TemplateField HeaderText="DESCRIPTION 1" SortExpression="DES1" ItemStyle-Width="150px">                              
                                <ItemTemplate>
                                   <a href="#" onclick="CaseSearch('<%#Container.DataItem("DES1")%>','<%#Container.DataItem("RFPID")%>')" class="Link"><%# Container.DataItem("RFPID")%>:<%#Container.DataItem("DES1")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                        <asp:BoundField DataField="DES2" HeaderText="DESCRIPTION 2" SortExpression="DES2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="50%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidRFPid" runat="server" />
        <asp:HiddenField ID="hidRFPDes" runat="server" />
        <asp:HiddenField ID="hidRFPidD" runat="server" />
    </div>
    </form>
</body>
</html>
