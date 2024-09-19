<%@ Page Language="VB" MasterPageFile="~/Masters/S1PopUp.master" AutoEventWireup="false" CodeFile="GetShipPopUp.aspx.vb" Inherits="Pages_Sustain1_PopUp_GetShipPopUp" title="S1-Get Shiping Selector" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/JavaScript">
         function ShipDet(ShipDes, ShipId) {
             var hidShipdes = document.getElementById('<%= hidShipdes.ClientID%>').value
             var hidShipid = document.getElementById('<%= hidShipid.ClientID%>').value
//             alert(hidMatdes);
//              alert(hidMatId);
             window.opener.document.getElementById(hidShipdes).innerText = ShipDes
             window.opener.document.getElementById(hidShipid).value = ShipId
             window.close();
         }
     </script> 
 <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Ship Description:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtShipDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                         <td align="right">
                          
                        </td>
                        <td>
                         
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
                        Ship Des1
                    </td>
                     <td>
                       
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdShip" DataKeyNames="shipId" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="shipId" HeaderText="shipId" SortExpression="shipId" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Ship Des1" SortExpression="matDISde1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="ShipDet('<%#Container.DataItem("shipDes")%>','<%#Container.DataItem("shipId")%>')" class="Link"><%#Container.DataItem("shipId")%>:<%#Container.DataItem("shipDes")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                       
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidShipdes" runat="server" />
          <asp:HiddenField  ID="hidShipid" runat="server" />
   </div>
</asp:Content>

