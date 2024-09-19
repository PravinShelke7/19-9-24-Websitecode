<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EditTerms.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_EditTerms" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Edit Term</title>
    <script type="text/JavaScript">
        function ShowPopUpWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');
            var width = 650;
            var height = 520;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat1', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.allied-dev.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

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
                document.getElementById('btnrefresh').click();
                return true;
            }
        }

    </script>
    <style type="text/css">
        #SavvyMasterContent
        {
            width: 604px;
        }
        .SavvyModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url(../../../Images/SavvyPackPro.gif);
            height: 44px;
            width: 604px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
        .ContentPage
        {
            margin-top: 2px;
            margin-left: 1px;
            width: 604px;
            background-color: #F1F1F2;
        }
        .PageHeading
        {
            font-size: 18px;
            font-weight: bold;
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
            height: 20px;
        }
        .AlterNateColor3
        {
            background-color: #D3DAD0;
            height: 20px;
        }
        .PageSHeading
        {
            font-size: 12px;
            font-weight: bold;
        }
        .style1
        {
            width: 25%;
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="SavvyMasterContent">
        <div>
            <table class="SavvyModule" cellpadding="0" cellspacing="0" style="border-collapse: collapse">
                <tr>
                    <td style="padding-left: 490px">
                    </td>
                </tr>
            </table>
        </div>
        <div id="error">
            <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
        </div>
    </div>
    <div id="PAlliedLogo">
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 600px; text-align: center;
                        background-color: Black; color: White;">
                        <asp:Label ID="lblHeading" runat="server" Text="Edit Terms"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                </td>
            </tr>
            <tr style="height: 20px">
                <td>
                    <div id="ContentPagemargin" runat="server">
                        <asp:ScriptManager ID="scrpt1" runat="server">
                        </asp:ScriptManager>
                        <div id="PageSection1" style="text-align: left;">
                            <asp:Table Width="100%" ID="tblEditQ" runat="server">
                            </asp:Table>
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
    </div>
    <asp:HiddenField ID="hidSurveyName" runat="server" />
    <asp:HiddenField ID="hidSurveyId" runat="server" />
    <asp:Button ID="btnrefresh" runat="server" Style="display: none;" />
    </form>
</body>
</html>
