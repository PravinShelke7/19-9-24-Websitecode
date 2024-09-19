<%@ Page Language="VB" MasterPageFile="~/Masters/SMed1PopUp.master" AutoEventWireup="false" CodeFile="GetDeptPlantConfig.aspx.vb" Inherits="Pages_MedSustain1_PopUp_GetDeptPlantConfig" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <script type="text/JavaScript">
         function DeptDet(DepDes, DepId) {
             var hidDepdes = document.getElementById('<%= hidDepdes.ClientID%>').value
             var hidDepid = document.getElementById('<%= hidDepid.ClientID%>').value
//             alert(hidMatdes);
//              alert(hidMatId);
             window.opener.document.getElementById(hidDepdes).innerText = DepDes
             window.opener.document.getElementById(hidDepid).value = DepId
             window.close();
         }
     </script> 
     <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Dept. De1:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtDepDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                         <td align="right">
                            <b>Dept. De2:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtDepDe2" runat="server" CssClass="SearchTextBox"  Width="200px"></asp:TextBox>
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
            <table cellspacing="0" ce>
                    <tr>
                        <td>
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                        </td>
                  </tr>
                </table>
              <table style="width:400px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td>
                        Department Des1
                    </td>
                     <td>
                        Department Des2
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdDepartment" DataKeyNames="PROCID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="MATID" HeaderText="MATID" SortExpression="MATID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Dept. Des1" SortExpression="PROCDE1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="DeptDet('<%#Container.DataItem("PROCDE")%>','<%#Container.DataItem("PROCID")%>')" class="Link"><%#Container.DataItem("PROCDE1")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="PROCDE2" HeaderText="Dept. Des2" SortExpression="PROCDE2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidDepdes" runat="server" />
          <asp:HiddenField  ID="hidDepid" runat="server" />
   </div>
</asp:Content>

