<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdateMaterial.aspx.vb"
    Inherits="Pages_MoldEcon1_PopUp_UpdateMaterial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E1 Mold-Edit Material Name</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        function MaterialDet() {

            var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value;
            var i = hidMatdes;
            i = i.match(/\d+$/)[0];
            document.getElementById('<%= hidId.ClientID%>').value = i;
            document.getElementById('<%= hidMatId.ClientID%>').value = window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_hidMatid' + i).value;
            document.getElementById('<%= btnCall.ClientID%>').click();
        }

        function CloseWindow() {
            
            var hidMatLname = document.getElementById('<%= lblName.ClientID%>').innerHTML;
            var hidMatName = document.getElementById('<%= txtName.ClientID%>').value;
            var hidMatDes1 = document.getElementById('<%= hidMatDes1.ClientID%>').value;
            var i = document.getElementById('<%= hidId.ClientID%>').value;

            if (hidMatName != "") {
                if (hidMatName.length > 38) {
                    var Name = hidMatName.substring(0, 20);
                    var Name1 = hidMatName.substring(20, 33);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = hidMatName.substring(0, 20);
                    var Name1 = hidMatName.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                window.opener.document.getElementById(hidMatDes1).innerHTML = Name;
                //window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_hypMatDes' + i).innerHTML = Name;
                window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "inline";
                window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
            }
            else {
                if (hidMatLname.length > 38) {
                    var Name = hidMatLname.substring(0, 20);
                    var Name1 = hidMatLname.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = hidMatLname.substring(0, 20);
                    var Name1 = hidMatLname.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                window.opener.document.getElementById(hidMatDes1).innerHTML = Name;
                //window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_hypMatDes' + i).innerHTML = Name;
                window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "inline";
            }

            //window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_btnRefresh').click();
            window.close();
        }
                

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <div id="divMainHeading" style="text-align: center; font-weight: bold; font-size: large;">
                Edit Material
            </div>
            </br>
            <div id="div1" style="text-align: center; margin-left: 80px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <table cellpadding="0" cellspacing="2" width="360px">
                    <tr>
                        <td align="left" class="AlterNateColor2">
                            <b>Existing Material Name:</b>
                        </td>
                        <td align="left" class="AlterNateColor1">
                            <asp:Label ID="lblName" runat="server" Font-Bold="true" Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="AlterNateColor2">
                            <b>New Material Name:</b>
                        </td>
                        <td align="left" class="AlterNateColor1">
                            <asp:TextBox ID="txtName" runat="server" CssClass="MatSmallTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="Button" Style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </div>
        <asp:Button ID="btnCall" runat="server" Style="margin-left: 5px; display: none;" />
        <asp:HiddenField ID="hidMatdes" runat="server" />
        <asp:HiddenField ID="hidMatName" runat="server" />
        <asp:HiddenField ID="hidMatId" runat="server" />
        <asp:HiddenField ID="hidMatDes1" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />
        <asp:HiddenField ID="hidSG" runat="server" />
    </div>
    </form>
</body>
</html>
