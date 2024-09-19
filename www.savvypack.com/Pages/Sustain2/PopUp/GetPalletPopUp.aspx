<%@ Page Language="VB" MasterPageFile="~/Masters/S2PopUp.master" AutoEventWireup="false" CodeFile="GetPalletPopUp.aspx.vb" Inherits="Pages_Sustain2_PopUp_GetPalletPopUp" title="S2-Get Pallets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/JavaScript">
         function PalletDet(PalletDes, PalId) {
            
             var hypPalDes = document.getElementById('<%= hypPalDes.ClientID%>').value;
             var hidPalid = document.getElementById('<%= hidPalid.ClientID%>').value;
             //alert(PalletDes);
             window.opener.document.getElementById(hypPalDes).innerText = PalletDes.replace(/##/g,'"');
             window.opener.document.getElementById(hidPalid).value = PalId
             window.close();
         }
     </script> 
   <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Pallet De1:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtPalDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                         <td align="right">
                            <b>Pallet De2:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtPalDe2" runat="server" CssClass="SearchTextBox"  Width="200px"></asp:TextBox>
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
                        Pallet Des1
                    </td>
                     <td>
                        Pallet Des2
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdPallets" DataKeyNames="PALLETID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="PALLETID" HeaderText="PALLETID" SortExpression="PALLETID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Pallet Des1" SortExpression="palletde1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="PalletDet('<%#Container.DataItem("PALLETDES1")%>','<%#Container.DataItem("PALLETID")%>')" class="Link">
                                      <%#Container.DataItem("PalletId")%>:<%#Container.DataItem("palletde1")%>
                                  </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="PALLETDE2" HeaderText="Pallet Des2" SortExpression="PALLETDE2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hypPalDes" runat="server" />
          <asp:HiddenField  ID="hidPalid" runat="server" />
   </div>
</asp:Content>

