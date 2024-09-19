<%@ Page Title="Market1 Report" Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="CagrReport.aspx.vb" Inherits="Pages_Market1_CAGR_CagrReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script><script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script><script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
   <script type="text/javascript">
        function ShowPopWindow(Page, HidID) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  

            var width = 500;
            var height = 180;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var Hid = document.getElementById(HidID).value
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            Page = Page + '&hidValue=' + Hid
            //            alert(Page);
            newwin = window.open(Page, 'PopUp', params);

        }
        function checkUint(colSeq) {
            // alert(colSeq);
            if (document.getElementById("ctl00_Market1ContentPlaceHolder_hidReportType").value == 'UNIFORM') {
              

                                var str = document.getElementById("ctl00_Market1ContentPlaceHolder_Column_" + (colSeq-1)).innerText;
                if (str.indexOf('CAGR') != -1) {
                                    document.getElementById("ctl00_Market1ContentPlaceHolder_lbl_" + (colSeq)).innerText = '(%)';
                }
                else {
                    var txt = document.getElementById("ctl00_Market1ContentPlaceHolder_hidUnitShort").value;
                    //var txt = e.options[e.selectedIndex].value; 
                                    document.getElementById("ctl00_Market1ContentPlaceHolder_lbl_" + (colSeq)).innerText = txt;
                }

            }

        }

    </script>
<div id="ContentPagemargin" runat="server">
<div id="PageHeader" class="PageHeading">
            <asp:Label ID="lblheading" runat="Server"></asp:Label>
            <table border="0" id="tblCaseDes" runat="server" cellpadding="0" cellspacing="0">
                <tr style="height: 20px">
                    <td style="width: 350px; text-align: Left;">
                        <span style="font-size: 12px;"><b>Report Id:</b></span>
                        <asp:Label ID="lblReportID" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                    <td style="width: 70px; text-align: Left;">
                        <span style="font-size: 12px;"><b>Report Type:</b></span>
                    </td>
                    <td style="width: 50%">
                        <asp:Label ID="lblReportType" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
                <tr style="height: 20px">
                    <td colspan="2">
                        <span id="caseDe3" runat="server" style="font-size: 12px;"><b>Report Brief:</b></span>
                        <asp:Label ID="lblReportDe2" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
       <div id="PageSection1" style="text-align:left" >
          <br />
          
            <asp:Table ID="tblCAGR" runat="server" CellPadding="0" CellSpacing="1" ></asp:Table>
          </div>
</div>
  <asp:HiddenField ID="hidReportType" runat="server" />
    <asp:HiddenField ID="hidUnitShort" runat="server" />
</asp:Content>