<%@ Page Language="VB" MasterPageFile="~/Masters/Market1.master" AutoEventWireup="false" CodeFile="GeographicRegionSet.aspx.vb" 
Inherits="Pages_Market1_GeographicRegionSet" title="Geographic Region Set" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Market1ContentPlaceHolder" Runat="Server">
<div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:500px;height:30px;">
                      Geographic Region Set
</div>
<div style="height:40px;">
</div>
<div>
                   <asp:GridView CssClass="GrdUsers" runat="server" ID="grdRegionSet" PageSize="30" 
                    DataKeyNames="REGIONSETID" Width="500px" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                    <RowStyle BackColor="#EFF3FB" />
                    <Columns>
                        <asp:BoundField DataField="REGIONSETID" HeaderText="REGIONSETID" Visible="false" >
                        </asp:BoundField>    
                        
                        <asp:TemplateField HeaderText="Region Set Name" HeaderStyle-HorizontalAlign="center" SortExpression="REGIONSETNAME" ItemStyle-HorizontalAlign="Left" >
                            <ItemTemplate>                             
                               <asp:HyperLink Width="150px" ID="lnkRegionSet" Target="_blank"  Enabled="true" runat="server" 
                               ToolTip='<%# bind("REGIONSETNAME")%>' Text='<%# bind("REGIONSETNAME")%>'  style="color:Black;font-weight:normal" 
                               AutoPostBack="false"  
                               NavigateUrl='<%# "~/Pages/Market1Sub/RegionSetsDetails.aspx?RegionSetId="+  Eval("REGIONSETID").toString()%>'>
                               </asp:HyperLink>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Left" />
                             <ItemStyle Width="120px"  Wrap="true" HorizontalAlign="Left"  />   
                        </asp:TemplateField>
                     
                      <asp:BoundField DataField="REGIONCOUNT" HeaderText="Number of Regions" SortExpression="REGIONCOUNT">
                            <ItemStyle Width="110px" Wrap="true" HorizontalAlign="Left" />
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
    <asp:HiddenField ID="hidsortreg" runat="server" />
</div>
</asp:Content>

