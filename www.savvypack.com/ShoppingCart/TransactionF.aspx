<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"  CodeFile="TransactionF.aspx.vb" Inherits="ShoppingCart_TransactionF" Title="Shopping Cart - Order Failed" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script language="JavaScript" src="https://seals.networksolutions.com/siteseal/javascript/siteseal.js" type="text/javascript"></script>--%>
    <script language="JavaScript" src="https://seal.networksolutions.com/siteseal/javascript/siteseal.js"
        type="text/javascript"></script>
    <script type="text/JavaScript">
        function ShowEditPopup(ShipToID) {

            if (ShipToID == null) {
                ShipToID = document.getElementById('ctl00_ContentPlaceHolder1_hidBillToID').value;
            }
            if (ShipToID.toString() == "LogUserID") {
                ShipToID = document.getElementById('ctl00_ContentPlaceHolder1_hdnUserID').value;
            }

            var width = 500;
            var height = 480;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var Page = "../Users_Login/UserInfoEdit.aspx?ShipToId=" + ShipToID + "";
            newwin = window.open(Page, 'EditShpInfo', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function Showcardpopup() {
            var refnum = document.getElementById('ctl00_ContentPlaceHolder1_hdnReffNumber').value;
            var width = 470;
            var height = 198;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var Page = "../Users_Login/UserCardDetails1.aspx?RefNum=" + refnum + "&Type=Order";
            newwin = window.open(Page, 'Card', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;
        }

    </script>
    <div class="MnContentPage" style="padding-left:100px" >
        <center>
            <div class="PageHeading">
                <asp:Label ID="lblMainHeader" runat="server"></asp:Label>
            </div>
        </center>
       
        <center >
        <table class="style1">
            <tr>
                <td>    
                    <center >
                     <asp:Image ID="imgs" runat="server" ImageUrl="~/Images/paymentf.png" Height="65px"
                            Style="margin-top: 10px; margin-left: 5px; margin-bottom: -20px; margin-left: -1px;" />
                     <asp:Label ID="lblTrans" runat="server" Text ="Transaction Failed!!" 
                         Font-Bold="True" Font-Size="20pt" Height ="65"></asp:Label>
                    </center>            
                </td>
                
            </tr>
          
            <tr>
               <td>
                    <center>                                       
                     <asp:Label ID="lblreason" runat="server" height="50" Text ="Your order has been failed due to incorrect Credit Card details." ></asp:Label>                    
                     </center>
               </td>
               </tr>
               <tr></tr>
               <tr>
                    <td>
                    <center >
                        <input type="button" onclick="javascript:window.close();" class="ButtonWMarigin" value="KEEP SHOPPING" />
                        </center>
                    </td>
               </tr>
        </table>
        
        </center>
        
        <br />
       
        <asp:HiddenField ID="hidFName" runat="server" />
        <asp:HiddenField ID="hidLName" runat="server" />
        <asp:HiddenField ID="hidCompanyName" runat="server" />
        <asp:HiddenField ID="hidExpDate" runat="server" />
        <asp:HiddenField ID="hidItemNum" runat="server" />
        <asp:HiddenField ID="hidBillToID" runat="server" />
        <asp:HiddenField ID="hidlnkEdit" runat="server" />
        <asp:HiddenField ID="hdnReffNumber" runat="server" />
        <asp:HiddenField ID="hdnUserID" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
    </div>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>

