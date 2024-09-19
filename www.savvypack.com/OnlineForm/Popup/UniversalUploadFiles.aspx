<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UniversalUploadFiles.aspx.vb"
    Inherits="Savvypack_Popup_UploadFiles" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <title>File Upload</title>
    <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/SavvyPack.css" rel="stylesheet" type="text/css" />
    
     <script type="text/javascript">
       
         function CheckFileExists() {            
            var existfile = "<%=Session("UUFExistingFile")%>"
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
            left: 320px;
            top: 90px;
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
        <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width: 700px; text-align: center">
                        <asp:Label ID="Label1" Text="Upload Files" runat="server"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr style="height: 30px">
                <td>
                    <div id="PageSection1" style="text-align: left; width: 697px; margin-left: -7px; margin-top: -7px;">
                        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                        <br />
                        <asp:Label ID="lbl1" runat="server" Text="Select a File to Upload:" Font-Size="12px"
                            Font-Bold="true"></asp:Label>
                        <br />
                        <br />

                        <asp:UpdatePanel ID="upd1" runat="server" ChildrenAsTriggers="true">
                            <ContentTemplate>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="upd1" DynamicLayout="true">
                                    <ProgressTemplate>
                                        <div class="divUpdateprogress">
                                            <table>
                                                <tr>
                                                    <td>
                                                        <img alt="" src="../../Images/Loading4.gif" height="50px" />
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
                                <div style="width: 98%; text-align: center;">
                                    <asp:FileUpload ID="fuSheet" runat="server" Width="100%" />
                                    <asp:FileUpload ID="fuSheet1" runat="server" Width="100%" Style="visibility: hidden;" />
                                    <br />
                                </div>
                                <div style="margin-top: 5px; text-align: left;">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" value="Upload" OnClientClick="return CheckFileExists()" />
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnSubmit" />
                            </Triggers>
                        </asp:UpdatePanel>
                        <div id="Div1" style="text-align: left; width: 690px; height: 300px; overflow: auto;">
                            <asp:Label ID="Label2" runat="server"></asp:Label>
                            <br />
                            <asp:Table ID="tblDwnldList" runat="server" Width="670px">
                            </asp:Table>
                            <asp:Label ID="lblError" runat="server"></asp:Label>
                            <br />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
        <br />
        <asp:HiddenField ID="hidProjectId" runat="server" />
        <asp:HiddenField ID="hidType" runat="server" />
        <asp:HiddenField ID="hidFileAction" runat="server" />
    </form>
</body>
</html>
