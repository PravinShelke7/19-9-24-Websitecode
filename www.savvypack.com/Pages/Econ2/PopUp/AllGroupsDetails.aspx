<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AllGroupsDetails.aspx.vb" Inherits="Pages_Econ1_PopUp_AllGroupsDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group details</title>
   
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>
<body style="width: 100%; margin: 0 0 0 0;background-color: #D3E7CB">
    <form id="form1" runat="server" style="width: 100%; margin: 0 0 0 0">
   <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection12" style="width: 100%;text-align: left;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <br />
            <div  style="width: 100%;font-size: 16px;font-weight:bold;text-align:center;">
                Group Details
            </div>
            <div style="width: 100%;text-align:center;"> <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" style="text-align:cente;font-size:16px;font-weight:bold;color:Red;"> </asp:Label></div>
            <div style="width:990px; overflow: auto;">
                <asp:GridView  CssClass="GrdUsers" runat="server" ID="grdGroup" Width="940px" DataKeyNames="GROUPID"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" CellPadding="4" Font-Size="11px"
                    ForeColor="#333333" GridLines="None" CellSpacing="1">
                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                     <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <Columns>
                        <asp:BoundField DataField="GROUPID" HeaderText="GROUPID" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="GROUPNAME" HeaderText="Group Name" SortExpression="GROUPNAME">
                            <ItemStyle Width="150px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GROUPDES" HeaderText="Group Description" SortExpression="GROUPDES">
                            <ItemStyle Width="200px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CASEID" HeaderText="Case(s)" SortExpression="CASEID">
                            <ItemStyle Width="200px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="CASEID" HeaderStyle-HorizontalAlign="Left" Visible="false"
                            SortExpression="CASEID">
                            <ItemTemplate>
                                <asp:Label ID="lblCaseID" runat="server" Text='<%# bind("CASEID")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Width="120px" Wrap="true" HorizontalAlign="Left" />
                        </asp:TemplateField>
                           <asp:BoundField DataField="UserName" HeaderText="Group Owner" SortExpression="UserName">                          
                            <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Left"  CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CREATIONDATE" HeaderText="Creation Date" SortExpression="CREATIONDATE">
                            <ItemStyle Width="80px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="UPDATEDATE" HeaderText="Last Update" SortExpression="UPDATEDATE">
                            <ItemStyle Width="80px" Wrap="true" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    <HeaderStyle Height="25px" BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                   
                     
                </asp:GridView>
                <asp:HiddenField ID="hvUserGrd" runat="server" />
            </div>
            <br />
        </div>
        <asp:HiddenField ID="hidCaseid" runat="server" />
        <asp:HiddenField ID="hidCaseDes" runat="server" />
    </div>
    </form>
</body>
</html>
