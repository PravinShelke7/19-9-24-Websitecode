<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProjectSummary.aspx.vb"
    Inherits="Pages_ProjectSummary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>Project Summary</title>
    <link href="../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    <script src="../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
       <script type="text/javascript">
           $(document).ready(function () {

               var txtarray = document.getElementsByTagName("input");
               var flag;
               var key;
               if (txtarray.length != 0) {
                   for (var i = 0; i < txtarray.length; i++) {
                       if (txtarray[i].type == "text") {
                           var id = txtarray[i].id;
                           $('#' + id).change(function () {
                               CheckSP();
                               var ControlID = $(this).attr('id');
                               var ControlValue = $(this).val();

                               if (ControlID == "txtWord") {
                                   key = 'Brief Description';
                               }
                               else if (ControlID == "txtTitle") {
                                   key = 'Project Title';
                               }
                               
                               PageMethods.UpdateCase(key, ControlValue, onSucceed, onError);

                           });
                       }
                   }
               }

               //To check Multiline Textbox
               var txtMularray = document.getElementsByTagName("textarea");
               if (txtMularray.length != 0) {
                   for (var i = 0; i < txtMularray.length; i++) {
                       if (txtMularray[i].type == "textarea") {
                           var idMul = txtMularray[i].id;
                           $('#' + idMul).change(function () {
                               CheckSPMul("1000");
                               var ControlID = $(this).attr('id');
                               var ControlValue = $(this).val();

                               if (ControlID == "txtDesc") {
                                   key = 'Project Description';
                               }

                               PageMethods.UpdateCase(key, ControlValue, onSucceed, onError);
                           });
                       }
                   }
               }

               function onSucceed(result) {

               }

               function onError(result) {

               }
           });
    </script>
    <%--   <script type="text/javascript">
        function Count(text) {

            var a = /\<|\>|\&#|\\/;
            var key;
            if ((document.getElementById("txtWord").value.match(a) != null) || (document.getElementById("txtDesc").value.match(a) != null)) {
                if (text.id == "txtWord") {
                    key = 'Brief Description';
                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Unique Features. Please choose alternative characters.");
                }
                else if (text.id == "txtDesc") {
                    key = 'Project Description';
                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Project Description. Please choose alternative characters.");
                }
                var object = document.getElementById(text.id)  //get your object
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                if (text.id == "txtWord") {
                    key = 'Brief Description';
                }
                else if (text.id == "txtDesc") {
                    key = 'Project Description';
                }
                //var oValue = text.oldvalue;
                PageMethods.UpdateCase(key, text.value, onSucceed, onError);
                return false;
            }
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 1000; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            if (object.value.length > maxlength) {
                object.focus(); //set focus to prevent jumping
                object.value = text.value.substring(0, maxlength); //truncate the value
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
            if (text.id == "txtWord") {
                key = 'Brief Description';
            }
            else if (text.id == "txtDesc") {
                key = 'Project Description';
            }
            //var oValue = text.oldvalue;
            PageMethods.UpdateCase(key, text.value, onSucceed, onError);

        }

        function CheckSP(text) {

            var a = /\<|\>|\&#|\\/;
            var key;
            if ((document.getElementById(text.id).value.match(a) != null)) {
                var object = document.getElementById(text.id)  //get your object
                if (text.id == "txtTitle") {
                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Project Title. Please choose alternative characters.");
                }
		else if (text.id == "txtWord") {
                    key = 'Brief Description';
                    alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Unique Features. Please choose alternative characters.");
                }

                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                if (text.id == "txtWord") {
                    key = 'Brief Description';
                }
                else if (text.id == "txtTitle") {
                    key = 'Project Title';
                }
                //var oValue = text.defaultValue;
                PageMethods.UpdateCase(key, text.value, onSucceed, onError);
                return false;
            }
            if (text.id == "txtWord") {
                key = 'Brief Description';
            }
            else if (text.id == "txtTitle") {
                key = 'Project Title';
            }
            //var oValue = text.defaultValue;
            PageMethods.UpdateCase(key, text.value, onSucceed, onError);
        }


        function onSucceed(result) {

        }

        function onError(result) {

        }

    </script>--%>
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
            newwin = window.open(Page, 'NewWindow2', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }


        }

        function OpenWindow(Page) {

            var width = 640;
            var height = 420;
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
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

        function ClosePage() {

            window.opener.document.getElementById('btnRefresh').click();
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
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        </asp:ScriptManager>
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 600px; text-align: center;">
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>
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
                                        <asp:Label ID="lblNumber" runat="server" Text="Project ID:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:Label ID="lblNum" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 25%">
                                        <asp:Label ID="lblUserN" runat="server" Text="Owner:" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:Label ID="lblUser" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label2" runat="server" Text="Project Title:" Font-Bold="true" ToolTip="Create an easy-to-remember name for your project"></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="txtTitle" ToolTip="Create an easy-to-remember name for your project"
                                            CssClass="SavvyMediumTextBox" Width="50%" MaxLength="100" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 25%">
                                        <asp:Label ID="lblWord" runat="server" Text="Brief Description:" Font-Bold="true"
                                            ToolTip="Provide some unique features, such as a rare material or a special design."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:TextBox ID="txtWord" CssClass="SavvyMediumTextBox" MaxLength="1000" Width="50%"
                                            runat="server" ToolTip="Provide a brief description for the project by highlighting unique features."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" id="trAnalysis" runat="server">
                                    <td style="width: 25%">
                                        <asp:Label ID="Label3" runat="server" Text="Type of Analysis" Font-Bold="true" ToolTip="Choose between Economic analysis or Environmental analysis, or you can pick both."></asp:Label>
                                    </td>
                                    <td style="width: 45%">
                                        <asp:LinkButton ID="lnkAnalysis" runat="server" Width="140px" Style="color: Black; font-family: Verdana; font-size: 12px;"
                                            Text='Select Analysis' OnClientClick="return OpenWindow('Popup/AnalysisDetails.aspx?Des=lnkAnalysis&Id=hidAnalysisId&Des1=hidAmalysisDes');"></asp:LinkButton>
                                    </td>
                                </tr>
                                <tr style="height: 100px;" class="AlterNateColor1">
                                    <td style="width: 25%;">
                                        <asp:Label ID="lblDesc" runat="server" Text="Project Description:" Font-Bold="true"
                                            ToolTip="Provide a full description for the project by providing all necessary details."></asp:Label>
                                    </td>
                                    <td style="width: 88%">
                                        <asp:TextBox ID="txtDesc" runat="server" CssClass="SavvyMediumTextBox" TextMode="MultiLine"
                                            MaxLength="1000" Width="70%" Height="80px" ToolTip="Provide a detail description for the project if you like."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1">
                                    <td style="width: 15%"></td>
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
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:HiddenField ID="hidAnalysisId" runat="server" />
        <asp:HiddenField ID="hidAmalysisDes" runat="server" />
    </form>
</body>
</html>
