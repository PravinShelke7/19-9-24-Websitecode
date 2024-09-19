<%@ Page Language="VB" MasterPageFile="~/Masters/SDistPopUp.master" AutoEventWireup="false" CodeFile="GetPositionPopup.aspx.vb" Inherits="Pages_SDistribution_PopUp_GetPositionPopup" title="SDist-Get Postion Description" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script type="text/JavaScript">
         function PositionDet(PosDes, PosId) {
            
             var hypPosDes = document.getElementById('<%= hypPosDes.ClientID%>').value;
             var hidPosid = document.getElementById('<%= hidPosid.ClientID%>').value;
             //alert(PalletDes);
             window.opener.document.getElementById(hypPosDes).innerText = PosDes.replace(/##/g,'"');
             window.opener.document.getElementById(hidPosid).value = PosId
             window.close();
         }
     </script> 
  <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Position Des1:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtPosDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                     <tr>
                        <td align="right">
                            <b>Position Des2:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtPosDe2" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
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
                        Position Des1
                    </td>
                     <td>
                       Position Des2
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdPos" DataKeyNames="persid" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="persid" HeaderText="persid" SortExpression="persid" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Pos Des1" SortExpression="palletde1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="PositionDet('<%#Container.DataItem("persde1")%>','<%#Container.DataItem("persid")%>')" class="Link">
                                      <%#Container.DataItem("persid")%>:<%#Container.DataItem("persde1")%>
                                  </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="persde2" HeaderText="Position Des2" SortExpression="persde2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hypPosDes" runat="server" />
          <asp:HiddenField  ID="hidPosid" runat="server" />
          <asp:HiddenField ID="hdnCOUNTRY" runat="Server" />
   </div>
</asp:Content>

