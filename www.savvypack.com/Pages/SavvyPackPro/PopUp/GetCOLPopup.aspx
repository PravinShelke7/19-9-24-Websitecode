<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetCOLPopup.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_GetCOLPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Column Types</title>
    <style type="text/css">
        a.SavvyLink:link
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:visited
        {
            color: Black;
            font-family: Verdana;
            font-size: 11px;
            text-decoration: underline;
        }
        
        a.SavvyLink:hover
        {
            color: Red;
            font-size: 11px;
        }
        
        #ContentPagemargin
        {
            margin-left: 20px;
            text-align: left;
        }
        
        #PageSection1
        {
            background-color: #D3E7CB;
        }
        
        .AlterNateColor1
        {
            background-color: #C0C9E7;
        }
        
        .AlterNateColor2
        {
            background-color: #D0D1D3;
        }
        
        .AlterNateColor4
        {
            background-color: #000000;
            color: White;
            height: 20px;
        }
        
        .CalculatedFeilds
        {
            color: black;
        }
        
        .LongTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 320px;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            background-color: #FEFCA1;
        }
    </style>
    <script type="text/javascript"">
        function ColumnDet(ColDes, ColId) {
         var hypCOLDes = document.getElementById('<%= hypCOLDes.ClientID%>').value;
         var hidCOLid = document.getElementById('<%= hidCOLid.ClientID%>').value;
         window.opener.document.getElementById(hypCOLDes).innerText = ColDes;
         window.opener.document.getElementById(hidCOLid).value = ColId
        
         window.close();
        }    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0;">
        <div id="PageSection1" style="text-align: left; margin-left: 5px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <br />
            <table style="width: 450px; margin-left: 20px;">
                <tr>
                    <td style="width: 90px; font-size: 12px; text-align: right;">
                        <b>Column Type:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColType" Style="font-size: 12px; width: 200px;" runat="server"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr id="rwCOLValue" runat="server" visible="false">
                    <td style="width: 100px; font-size: 12px; font-weight: bold; text-align: right;">
                        <asp:Label ID="lblColValue" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCOLValue" runat="server" CssClass="Smalldropdown" Style="font-size: 12px;
                            width: 200px;">
                        </asp:DropDownList>
                    </td>
                </tr>

                 <tr>
                    <td style="width: 90px">
                    </td>
                    <td>
                        <asp:Button ID="btnSumitt" runat="server" Text="Submit" CssClass="ButtonWMarigin" />
                    </td>
                </tr>
            </table>
           
            <br />
        </div>
        <asp:HiddenField  ID="hypCOLDes" runat="server" />
          <asp:HiddenField  ID="hidCOLid" runat="server" />
    </div>
    </form>
</body>
</html>
