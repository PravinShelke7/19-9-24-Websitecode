<%@ Page Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="RegionSetsDetails.aspx.vb" 
Inherits="Pages_Market1_RegionSetsDetails" title="Region Set Details" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
<div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:600px;height:30px;">
                      Region Set Details
                </div>
             
            
                
                <table cellspacing="6">
                    <tr>
                        <td style="height:20px;">
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" style="display:none;" />
                        </td>
                     </tr>
                 </table> 
                     
                 <asp:GridView CssClass="GrdUsers" runat="server" ID="grdRegionSet" 
                    DataKeyNames="COUNTRYID" AllowPaging="false" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="COUNTRYID" HeaderText="COUNTRYID" Visible="false">
                        </asp:BoundField>    
                        <asp:BoundField DataField="REGIONNAME" HeaderText="Region" SortExpression="REGIONNAME" >
                            <ItemStyle Width="150px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="COUNTRYDES" HeaderText="Country" SortExpression="COUNTRYDES">
                            <ItemStyle Width="200px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                     
                       
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle Height="25px" BackColor="#507CD1" Font-Bold="True" 
                         ForeColor="White" />
                    <EditRowStyle BackColor="#2461BF" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
                <asp:HiddenField ID="hvUserGrd" runat="server" />
</asp:Content>

