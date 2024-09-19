<%@ Page Title="" Language="VB" MasterPageFile="~/Masters/E1S1E2S2.master" AutoEventWireup="false" CodeFile="Messages.aspx.vb" Inherits="Pages_Messages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain1ContentPlaceHolder" Runat="Server">
    <div id="ContentPagemargin">
    
     <asp:UpdatePanel ID="upd1" runat="server" >
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div class="divUpdateprogress">
                            <center>
                            <table >
                                <tr>
                                    <td><img alt="" src="../Images/Loading3.gif" height="50px" /></td>
                                    <td><b style="color:Red;">Updating</b></td>
                                </tr>
                            </table>
                            </center>
                            
                            </div>
                       </ProgressTemplate>
                    </asp:UpdateProgress>
     
                             <br />
                               <table style="width:790px;text-align:left;">
                                 <tr align="left">
                                  <td style="width:33%" class="PageHeading" onmouseover="Tip('SavvyPack® Announcements')" onmouseout="UnTip()" >
                                        SavvyPack® Announcements
                                        
                                    </td>
                                 </tr>
                               </table>
                               <br />
                               
                                <div id="PageSection1">
                                    <br />
                                    <div style="margin-left:5px;text-align:left;width:98%">
                                    <table style="width:98%" border="0" cellpadding="0" cellspacing="0">
                                        <tr>
                                          
                                            <td>
                                                
                                                <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete Announcements" CssClass="Link"></asp:LinkButton>&nbsp;  
                                                    <ajaxToolkit:ConfirmButtonExtender ID="ConfirmButtonExtender1"  ConfirmText="Do you want to delete selected announcement" TargetControlID="lnkDelete" runat="server"></ajaxToolkit:ConfirmButtonExtender>
                                               <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />   
                                            </td>
                                            <td align="center" style="width:50%" >
                                                <asp:Label ID="lblMsg" runat="server" CssClass="Error"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                    </div>
                                    <br />
                                    <asp:GridView  ID="grdMessages"  runat="server" Width="98%" AllowPaging="true" PageSize="3" AutoGenerateColumns="false" DataKeyNames="MESSAGEID" AllowSorting="true">
                                    
                                         <Columns>
                                                 <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <input id="HeaderLevelCheckBox" onclick="javascript:SelectAllCheckboxes(this);" 
                                                            runat="server" type="checkbox" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox id="delete" runat="server">
                                                        </asp:CheckBox>
                                                    </ItemTemplate>
                                                
                                             </asp:TemplateField>
                                                <asp:BoundField DataField="MESSAGEID" Visible="false" />
                                                
                                                <asp:BoundField DataField="MESSAGEHEAD" HeaderText="Announcement" HeaderStyle-ForeColor="White" SortExpression="MESSAGEHEAD" ItemStyle-Width="20%" ItemStyle-CssClass="GrdItemtext" />
                                                <asp:BoundField DataField="SERVERDATE" HeaderText="Date" SortExpression="SERVERDATE2"  Visible="true" ItemStyle-Width="10%"  />
                                                <asp:BoundField DataField="MESSAGETEXT" HeaderText="Announcement Details" HeaderStyle-ForeColor="White" SortExpression="MESSAGETEXT"  ItemStyle-CssClass="GrdItemtext" />
                                                <asp:TemplateField HeaderText="Feedback">
                                                    <ItemTemplate>
                                                        <%--<asp:ImageButton ImageUrl="~/Images/Feedback.gif" ID="imgFeedBack" runat="server" ToolTip="FeedBack" />--%>
                                                        <a href='<%#bind("FEEDBACK") %>' runat="server" id="fedd" target="_parent">
                                                            <img src="../Images/Feedback.gif" alt="Feedback" border="0"  />
                                                        </a>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                         </Columns>
                                        <AlternatingRowStyle CssClass="AlterNateColor1" />
                                        <RowStyle CssClass="AlterNateColor2" Height="20px" />
                                        <HeaderStyle CssClass="TdHeadind" Height="25px" Font-Bold="true" Font-Size="12px" ForeColor="White"  />  
                                        <PagerStyle CssClass="TdHeadind" Height="25px" Font-Bold="true" Font-Size="10px" ForeColor="White"  /> 
                                      
                                        
                                    </asp:GridView>
                                    <br />
                                </div>
                            
                            
                            
                </ContentTemplate>
        </asp:UpdatePanel>             

<asp:HiddenField ID="hvUserGrd" runat="server" />
    
    </div>
</asp:Content>

