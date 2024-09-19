<%@ Page Language="VB" AutoEventWireup="true" CodeFile="Index.aspx.vb" Inherits="_Index"
    MasterPageFile="~/Masters/SavvyPackIndex.master" Title="SavvyPack Corporation packaging industry studies, research, consulting, economic, and environmental analysis." %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/JavaScript">
    function ShowPopRedirectMsg(Page) {
        //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
        var width = 80;
        var height = 90;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=no';
        params += ', status=yes';
        params += ', toolbar=no';
        newwin = window.open(Page, 'Chat', params);
        if (newwin == null || typeof (newwin) == "undefined") {
            alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
        }

        return false;

    }

    function ShowPopWindow(Page) {
        //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
        var width = 355;
        var height = 185;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=yes';
        params += ', status=yes';
        params += ', toolbar=no';
        newwin = window.open(Page, 'Chat', params);
        if (newwin == null || typeof (newwin) == "undefined") {
            alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
        }

        return false;

    }
        </script>
    <div class="MnContentPage">
        
        <div class="PageHeading" id="divMainHeading" runat="server" style="text-align: center;
            width: 100%;">
        </div>
        <asp:HiddenField ID="hidTId" runat="server" />
        <asp:Button ID="btnServ" runat="server" Style="visibility: hidden;"></asp:Button>
        <asp:Button ID="btnMrkt" runat="server" Style="visibility: hidden;"></asp:Button>
        <asp:Button ID="btnCntr" runat="server" Style="visibility: hidden;"></asp:Button>
        <asp:Button ID="btnSavvy" runat="server" Style="visibility: hidden;"></asp:Button>
        <asp:Button ID="btnOData" runat="server" Style="visibility: hidden;"></asp:Button>
    </div>
</asp:Content>
