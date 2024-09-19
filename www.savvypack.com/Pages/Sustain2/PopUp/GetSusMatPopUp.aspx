<%@ Page Language="VB" MasterPageFile="~/Masters/S2PopUp.master" AutoEventWireup="false" CodeFile="GetSusMatPopUp.aspx.vb" Inherits="Pages_Sustain2_PopUp_GetSusMatPopUp" title="S2-Get Sustainable Material" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <script type="text/JavaScript">
         function MaterialDet(MatDes, MatId) {
             var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value
             var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value
//             alert(hidMatdes);
//              alert(hidMatId);
             window.opener.document.getElementById(hidMatdes).innerText = MatDes
             window.opener.document.getElementById(hidMatId).value = MatDes
             window.close();
         }
     </script> 
   <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Sustainable Material Description:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtSusMatDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
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
                        Sustainable Material Des1
                    </td>
                     <td>
                       
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdMat" DataKeyNames="TYPEID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="TYPEID" HeaderText="TYPEID" SortExpression="TYPEID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Sustainable Material Des1" SortExpression="TYPE">                              
                                <ItemTemplate>
                                   <a href="#" onclick="MaterialDet('<%#Container.DataItem("TYPE")%>','<%#Container.DataItem("TYPEID")%>')" class="Link"><%#Container.DataItem("TYPEID")%>:<%#Container.DataItem("TYPE")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                       
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidMatdes" runat="server" />
          <asp:HiddenField  ID="hidMatid" runat="server" />
   </div>
</asp:Content>

