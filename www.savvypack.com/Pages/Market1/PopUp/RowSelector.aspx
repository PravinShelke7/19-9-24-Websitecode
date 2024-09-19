<%@ Page Language="VB" MasterPageFile="~/Masters/M1PopUp.master" AutoEventWireup="false" CodeFile="RowSelector.aspx.vb" Inherits="Pages_Market1_PopUp_RowSelector" title="M1-Row Selector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server" EnableViewState="false">
    <script type="text/JavaScript">
         function RowSelection(RowDes,RowValue,RowID) {
         
          
             var hidRowDes = document.getElementById('<%= hidRowDes.ClientID%>').value
             var hidRowId = document.getElementById('<%= hidRowID.ClientID%>').value            
             window.opener.document.getElementById(hidRowId).value = RowValue             
             window.opener.document.getElementById(hidRowDes).innerText = RowDes
             window.opener.document.getElementById(hidRowDes).style.color='white';
//             alert(RowValue);
//             alert(RowDes);
             window.close();
         }

         function RowSelected() 
         {
             var ddl = document.getElementById("ctl00_ContentPlaceHolder1_ddlRowType")
//             alert(ddl); 
             var hid = document.getElementById("ctl00_ContentPlaceHolder1_hidRow")
             var hidSql = document.getElementById("ctl00_ContentPlaceHolder1_hidRowSQL")
             
             var myindex = ddl.selectedIndex
             //  alert(myindex);
             //  alert(ddl.options[1].CODE + ' ' + ddl.options[2].CODE)
             var SelValue = ddl.options[myindex].CODE
             //  alert(SelValue);
             hid.value = SelValue;
             hidSql.value = ddl.options[myindex].SQL             
         }

       
       function ShowPopup(Page) 
        {
            
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
            var ddl = document.getElementById("ctl00_ContentPlaceHolder1_ddlRowType");
            if (ddl.options[ddl.selectedIndex].text == 'Category') {
                Page='CategorySelector.aspx?Des=ctl00_ContentPlaceHolder1_lnkProductTree&Id=ctl00_ContentPlaceHolder1_hidCatID&CatDes=ctl00_ContentPlaceHolder1_hidCatDes1';
            }
            else if (ddl.options[ddl.selectedIndex].text == 'Group') {
                Page = 'GroupSelector.aspx?Des=ctl00_ContentPlaceHolder1_lnkProductTree&Id=ctl00_ContentPlaceHolder1_hidCatID&CatDes=ctl00_ContentPlaceHolder1_hidCatDes1';
            }
            newwin = window.open(Page, 'Chat', params);
            
            return false;

        }
        function Validation() {
            var ddl = document.getElementById("ctl00_ContentPlaceHolder1_ddlRowType");
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
        
            <table style="width:500px">
                <tr>
                    <td style="width:100px">
                        <b>Select Row Type:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlRowType" AutoPostBack="true" runat="server" OnChange="RowSelected() ;">
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
                   <tr id="rwProducts" runat="server" visible="false">
                    <td style="width:100px">
                       <asp:Label id="lblProducts" runat="server"></asp:Label>
                    </td>
                    <td id="colProducts">
                        <asp:DropDownList ID="ddlProducts" runat="server">
                        </asp:DropDownList>
                      <%--  <asp:HyperLink ID="lnkProductTree" runat="server"  Target="_blank"  >Product Tree</asp:HyperLink>--%>
                         <asp:LinkButton ID="lnkProductTree" Style="font-size: 14px" runat="server" CssClass="Link" Width="180px" 
                           OnClientClick="return ShowPopup('CategorySelector.aspx?Des=ctl00_ContentPlaceHolder1_lnkProductTree&Id=ctl00_ContentPlaceHolder1_hidCatID&CatDes=ctl00_ContentPlaceHolder1_hidCatDes1');">Select Cateogry</asp:LinkButton>
                    </td>
                </tr>              
                <tr id="rwUnits" runat="server" visible="false">
                    <td style="width:100px">
                     <b>Select Units:</b>
                    </td>
                    <td id="colUnits">
                        <asp:DropDownList ID="ddlUnits" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>     
                
               <tr id="rwCapitaRow1" runat="server" visible="false">
                    <td style="width:100px">
                     <b>Select Row1:</b>
                    </td>
                    <td id="Td1">
                        <asp:DropDownList ID="ddlCapitaRow1" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>    
                 <tr id="rwCapitaRow2" runat="server" visible="false">
                    <td style="width:100px">
                     <b>Select Row2:</b>
                    </td>
                    <td id="Td2">
                        <asp:DropDownList ID="ddlCapitaRow2" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>    
                
                
            </table>
             
            <table style="width:300px">
                <tr>
                    <td style="width:80px">
                        
                    </td>
                    <td>
                       <asp:Button ID="btnSumitt" runat="server" Text="Submit" CssClass="ButtonWMarigin" OnClientClick="return Validation();"  />
                    </td>
                </tr>                
            </table>
        
        </div>
        
          <asp:HiddenField  ID="hidRowDes" runat="server" />          
          <asp:HiddenField  ID="hidRowID" runat="server" />
          <asp:HiddenField  ID="hidRow" runat="server" />
          <asp:HiddenField  ID="hidRowSQL" runat="server" />
          <asp:HiddenField  ID="hidCatID" runat="server" />
           <asp:HiddenField  ID="hidCatDes" runat="server" />
           <asp:HiddenField  ID="hidCatDes1" runat="server" />
  </div>

</asp:Content>