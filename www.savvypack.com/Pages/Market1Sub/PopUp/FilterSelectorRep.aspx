<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/M1PopUp.master" AutoEventWireup="false" CodeFile="FilterSelectorRep.aspx.vb" Inherits="Pages_Market1Sub_PopUp_FilterSelectorRep" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/JavaScript">
    function FilterSelection(FilterDes, FilterValue, ColID) {

        var hidFilterDes = document.getElementById('<%= hidFilterDes.ClientID%>').value
        var hidFilterId = document.getElementById('<%= hidFilterId.ClientID%>').value

        window.opener.document.getElementById(hidFilterId).value = FilterValue

        window.opener.document.getElementById(hidFilterDes).innerText = FilterDes
        //             alert(window.opener.document.getElementById(hidColDes).innerText);
        window.opener.document.getElementById(hidFilterDes).style.color = 'white';
        window.opener.document.getElementById('ctl00_Market1ContentPlaceHolder_hidReportIDD').value = "1";
        window.close();
    }
    function ShowPopup(Page) {

        var RptId = '<%=Session("GrpRptId")%>';
        var isTemp = '<%=Session("isTemp")%>';
        var width = 300;
        var height = 330;
        var left = (screen.width - width) / 2;
        var top = (screen.height - height) / 2;
        var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
        params += ', location=no';
        params += ', menubar=no';
        params += ', resizable=no';
        params += ', scrollbars=yes';
        params += ', status=yes';
        params += ', toolbar=no';
        var ddl = document.getElementById("ctl00_ContentPlaceHolder1_ddlFilterType");
        if (ddl.options[ddl.selectedIndex].text == 'Category') {
            Page = 'CategorySelector.aspx?Des=ctl00_ContentPlaceHolder1_lnkProductTree&Id=ctl00_ContentPlaceHolder1_hidCatID&CatDes=ctl00_ContentPlaceHolder1_hidCatDes1';
        }
        else if (ddl.options[ddl.selectedIndex].text == 'Group') {
            Page = 'GroupSelector.aspx?Des=ctl00_ContentPlaceHolder1_lnkProductTree&Id=ctl00_ContentPlaceHolder1_hidCatID&CatDes=ctl00_ContentPlaceHolder1_hidCatDes1&RptId=' + RptId + '&isTemp=' + isTemp + '';
        }
        newwin = window.open(Page, 'Chat', params);

        return false;

    }
    function Validation() {
        var ddl = document.getElementById("ctl00_ContentPlaceHolder1_ddlFilterType");
        // alert(ddl.options[ddl.selectedIndex].text);
        if (ddl.options[ddl.selectedIndex].text == 'Category') {
            var hidCatIdValue = document.getElementById('<%= hidCatID.ClientID%>').value;
            // alert(hidCatIdValue);
            if (hidCatIdValue == "0") {
                alert("Please select Category");
                return false;
            }
        }
        return true;
    }
     </script>
      <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
        
            <table style="width:400px">
                <tr>
                    <td style="width:100px">
                        <b>Filter Type:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFilterType" runat="server" AutoPostBack="true">                         
                        </asp:DropDownList>
                    </td>
                </tr> 
                 <tr id="rwRegionSet" runat="server" visible="false">
                    <td style="width:100px">
                       <asp:Label id="lblRegionSet" runat="server"></asp:Label>
                    </td>
                    <td id="Td3">
                        <asp:DropDownList ID="ddlRegionSet" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>    
                 <tr id="rwFilterValue" runat="server" visible="false">
                            <td style="width:100px">
                               <asp:Label ID="lblFilterValue" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlFilterValue" runat="server" CssClass="Smalldropdown"></asp:DropDownList>
                                <asp:LinkButton ID="lnkProductTree" Style="font-size: 14px" runat="server" CssClass="Link" Width="180px" 
                           OnClientClick="return ShowPopup('CategorySelector.aspx?Des=ctl00_ContentPlaceHolder1_lnkProductTree&Id=ctl00_ContentPlaceHolder1_hidCatID&CatDes=ctl00_ContentPlaceHolder1_hidCatDes1');">Select Cateogry</asp:LinkButton>
                            </td>
                        </tr>               
            </table>
            
          
        
            <table style="width:400px">
                <tr>
                    <td style="width:80px">
                        
                    </td>
                    <td>
                       <asp:Button ID="btnSumitt" runat="server" Text="Submit" CssClass="ButtonWMarigin" OnClientClick="return Validation();" />
                    </td>
                </tr>                
            </table>
        
        </div>
        
          <asp:HiddenField  ID="hidFilterDes" runat="server" />
          <asp:HiddenField  ID="hidFilterId" runat="server" />
          <asp:HiddenField  ID="hidCatID" runat="server" />
          <asp:HiddenField  ID="hidCatDes" runat="server" />
          <asp:HiddenField  ID="hidCatDes1" runat="server" />
  </div>
</asp:Content>

