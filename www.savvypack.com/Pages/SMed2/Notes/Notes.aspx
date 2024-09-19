<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Notes.aspx.vb" Inherits="Pages_MedSustain2_Notes_Notes" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMed2-Page Notes</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
          
            height: 20px;
        }
        .style2
        {
            height: 20px;
        }
        .style3
        {
            height: 20px;
        }
    </style>
</head>
<body style="margin:0 0 0 0">
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left;">
       <div style="margin-left:10px;">
       <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="0" style="width:500px;height:400px;" border="0">
                <tr>
                    <td class="style1">
                        <b>Notes For :</b><asp:Label id="lblNotesFor" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" class="style3">
                        <b>Notes :</b>     
                    </td>
                </tr>
                 <tr>
                    <td colspan="2" valign="top">
                        <asp:TextBox TextMode="MultiLine" Rows="10" Columns="10" runat="server"  id="txtNotes"
                            Height="220px" Width="450px"></asp:TextBox>   
                            <br /> <br />
                        <asp:Button ID="btnsubmit" runat="server" Text="Submit" CssClass="ButtonWMarigin" />
                    </td>
                </tr>
                 
                </table>
                </div>
                </div>
    </div>
    </form>
</body>
</html>
