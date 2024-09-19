<%@ Page Language="VB" MasterPageFile="~/Masters/PackProdPopUp.master" AutoEventWireup="false" CodeFile="GetCompany.aspx.vb" Inherits="Pages_PackProd_Popup_GetCompany" title="Pack. Prod.-Get Company" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/JavaScript">
        function SearchItem(itemId) 
         {            
             var hidCatId = document.getElementById('<%= hidCatId.ClientID%>').value;            
             window.opener.document.getElementById('ctl00$PackProdContentPlaceHolder$'+hidCatId).value = itemId;
             window.close();
         }
     </script> 
     <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                          Company:
                        </td>
                        <td>
                          <asp:TextBox ID="txtCompany" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                     
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                             <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" style="margin-left:0px" />
                        </td>
                    </tr>
                </table>
            <table cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                        </td>
                  </tr>
                </table>
              <table style="width:400px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td>
                       Company:
                    </td>
                    
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdCompany" DataKeyNames="ID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="ID" SortExpression="ID" Visible="false">  
                        </asp:BoundField>
                           <asp:TemplateField HeaderText="Company" SortExpression="DATA">                              
                                <ItemTemplate>
                                   <a href="#" onclick="SearchItem('<%#Container.DataItem("ID")%>')" class="Link"><%#Container.DataItem("Name")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>              
                                             
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidCates" runat="server" />
          <asp:HiddenField  ID="hidCatId" runat="server" />
   </div>
</asp:Content>

