<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FileUpload.aspx.vb" Inherits="Pages_PopUp_FileUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>File Upload</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    
     <script type="text/javascript">

         function CheckFileExists() {
            var existfile = "<%=Session("FUExistingFile")%>";
            var newFile = document.getElementById('fuSheet').value;
            if (newFile != "") {
                newFile = newFile.substring(newFile.lastIndexOf("\\") + 1, newFile.length);
                var overwrite = false;

                var ExFile = existfile.split(",");

                for (i = 0; i < ExFile.length; i++) {
                    if (newFile == ExFile[i]) {
                        overwrite = true;
                    }
                }

                if (overwrite) {
                    if (confirm("This file already exists. Do you want to overwrite it?")) {
                        document.getElementById("hidFileAction").value = "Update";
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                else {
                    document.getElementById("hidFileAction").value = "Insert";
                    return true;
                }
            }
            else {
                alert("Please select file to upload.");
                return false;
            }
        }

    </script>
    <style type="text/css">
        .divUpdateprogress {
            left: 250px;
            top: 50px;
            position: absolute;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server" AsyncPostBackTimeout="4500">
        </asp:ScriptManager>
        <script type="text/javascript">
            window.onsubmit = function () {

                var updateProgress = $find("<%= UpdateProgress1.ClientID %>");
                window.setTimeout(function () {
                    updateProgress.set_visible(true);
                }, 100);

            }
        </script>
        <asp:UpdatePanel ID="upd1" runat="server" ChildrenAsTriggers="true">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd1" DynamicLayout="true">
                    <ProgressTemplate>
                        <div class="divUpdateprogress">
                            <table>
                                <tr>
                                    <td>
                                        <img alt="" src="../../Images/Loading4.gif" height="50" />
                                    </td>
                                    <td>
                                        <b style="color: Red;">Uploading...</b>
                                    </td>
                                    <td></td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
                    <div id="PageSection1" style="text-align: left; width: 615px; margin-left: 3px; margin-top: -7px; height: 100%;">
                        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="Select a File to Upload:" Font-Size="12px"
                            Font-Bold="true"></asp:Label>
                        <br />
                        <br />
                        <div style="width: 98%; text-align: center;">
                            <asp:FileUpload ID="fuSheet" runat="server" Width="100%" />
                            <br />
                        </div>
                        <div style="margin-top: 5px; text-align: center;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClientClick="return CheckFileExists()" />
                        </div>
                        <br />
                    </div>
                    <asp:HiddenField ID="hidProjectId" runat="server" />
                    <asp:HiddenField ID="hidType" runat="server" />
                    <asp:HiddenField ID="hidUserId" runat="server" />
                    <asp:HiddenField ID="hidFileAction" runat="server" />
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:PostBackTrigger ControlID="btnSubmit" />
            </Triggers>
        </asp:UpdatePanel>        
    </form>
</body>
</html>
