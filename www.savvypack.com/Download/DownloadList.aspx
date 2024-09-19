<%@ Page Title="Download" Language="VB" MasterPageFile="~/Masters/AlliedMasterMenu.master"
    AutoEventWireup="false" CodeFile="DownloadList.aspx.vb" Inherits="DownLoad_DownloadList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
    function OpenBrochure(path) {

        alert(path);
        OpenNewWindow('DownloadedFiles/'+path, 'BRCPAGE');
        return false;
    }
    function OpenNewWindow(Page, PageName) {

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
        newwin = window.open(Page, PageName, params);
        
    }
    </script>

    <div style="height: 5px;">
    </div>
    <div style="width: 100%">
      <%--  <center>
            <iframe id="ifrDownloadPage" runat="server" width="558px" height="90%" style="border: 0px solid white;
                margin-left: 15px;"></iframe>
        </center>--%>
         <table cellpadding="4" cellspacing="4" style="margin-left: 70px; width: 460px">
            <tr>
                <td>
                    <asp:Label ID="lblMessage" runat="server" Style="color: Red; font-size:13px; font-family: Optima;">
There are no file available for you to download. If you have any questions contact your Administrator or contact SavvyPack Corporation at [1] 952-405-7500 or at sales@savvypack.com.
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <iframe id="ifrDownloadPage" runat="server" width="558px" height="90%" style="border: 0px solid white;
                        margin-left: 15px;"></iframe>
                </td>
            </tr>
        </table>
    </div>
    <div style="height: 20px;">
    </div>
</asp:Content>
