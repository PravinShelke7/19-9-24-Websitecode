<%@ Page Language="VB" MasterPageFile="~/Masters/M1PopUp.master" AutoEventWireup="false" CodeFile="ColSelector.aspx.vb" Inherits="Pages_Market1_PopUp_ColSelector" title="M1-Column Selector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<script type="text/JavaScript">
         function ColSelection(ColDes,ColValue,ColID,seq) {
         
             var hidColDes = document.getElementById('<%= hidColDes.ClientID%>').value;
             var hidColId=document.getElementById('<%= hidColId.ClientID%>').value ;            
             window.opener.document.getElementById(hidColId).value = ColValue;             
             window.opener.document.getElementById(hidColDes).innerText = ColDes;
             window.opener.document.getElementById(hidColDes).style.color = 'white';
             window.opener.checkUint(seq);
             window.close();
         }
     </script>
  <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
        
            <table style="width:300px">
                <tr>
                    <td style="width:80px">
                        <b>Column Type:</b>
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlColType" runat="server" AutoPostBack="true">
                           <%-- <asp:ListItem Text="Year" Value="Year" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="CAGR" Value="CAGR" Selected="False"></asp:ListItem>--%>
                        </asp:DropDownList>
                    </td>
                </tr>                
            </table>
            
            <div id="divYear" runat="server" visible="false">
                  <table style="width:300px">
                    <tr>
                            <td style="width:80px">
                                <b>Year:</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlYear" runat="server" CssClass="Smalldropdown"></asp:DropDownList>
                            </td>
                        </tr>
                       
                    </table>
            </div>
            
            <div id="divCAGR" runat="server" visible="false">
                  <table style="width:300px">
                        <tr>
                            <td style="width:80px">
                                <b>Begin Year:</b>
                            </td>
                            <td>
                                 <asp:DropDownList ID="ddlBYear" runat="server" CssClass="Smalldropdown"></asp:DropDownList>
                            </td>
                        </tr>
                        
                        
                         <tr>
                            <td style="width:80px">
                                <b>End Year:</b>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlEYear" runat="server" CssClass="Smalldropdown"></asp:DropDownList>
                            </td>
                        </tr>
                       
                    </table>
            </div>
        
        
            <table style="width:300px">
                <tr>
                    <td style="width:80px">
                        
                    </td>
                    <td>
                       <asp:Button ID="btnSumitt" runat="server" Text="Submit" CssClass="ButtonWMarigin" />
                    </td>
                </tr>                
            </table>
        
        </div>
        
          <asp:HiddenField  ID="hidColDes" runat="server" />
          <asp:HiddenField  ID="hidColId" runat="server" />
  </div>
</asp:Content>

