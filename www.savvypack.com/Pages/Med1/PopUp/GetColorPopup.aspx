<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetColorPopup.aspx.vb" Inherits="Pages_MedEcon1_PopUp_GetColorPopup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Med1-Get Color</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function ColorDet(ColDes, ColId) {
             var hidColdes = document.getElementById('<%= hidColdes.ClientID%>').value
             var hidColId = document.getElementById('<%= hidColId.ClientID%>').value
             //alert(ColDes.length);
           
             window.opener.document.getElementById(hidColdes).innerText = ColDes
             window.opener.document.getElementById(hidColId).value = ColId
             window.close();
         }
     </script> 
     <script type="text/javascript">

         var _gaq = _gaq || [];
         _gaq.push(['_setAccount', 'UA-16991293-1']);
         _gaq.push(['_trackPageview']);

         (function () {
             var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
             ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
             var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
         })();

</script>
</head>
<body>
<form id="form1" runat="server">
   <div id="ContentPagemargin" style="width:920px;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                   <table cellpadding="0" cellspacing="2" style="margin-left:10px">
                    <tr>
                        <td align="right">
                            <b>Keyword:</b>
                        </td>
                        <td align="left" >
                          <asp:TextBox ID="txtKeyword" runat="server" CssClass="MediumTextBox" Style="text-align: left;" Width="300px"></asp:TextBox>
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
          
           <div style="width:920px;height:230px;overflow:auto;">
        
           <asp:GridView Width="900px" runat="server" ID="grdColor" DataKeyNames="COLORNBR" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" ShowHeader="true" CellSpacing="2"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" ForeColor="White" />
                  
                    <Columns>
                        <asp:BoundField DataField="COLORNBR" HeaderText="COLORNBR" SortExpression="COLORNBR" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Color Des" SortExpression="COLORNAME" >                              
                                <ItemTemplate>
                                   <a href="#" onclick="ColorDet('<%#Container.DataItem("COLORNAME")%>','<%#Container.DataItem("COLORNBR")%>')" class="Link"><%# Container.DataItem("COLORNBR")%>:<%#Container.DataItem("COLORNAME")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="PMS" HeaderText="PMS#" SortExpression="PMS" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="120px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR1" HeaderText="2327 WHITE" SortExpression="BCOLOR1" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR2" HeaderText="2328 BLACK" SortExpression="BCOLOR2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR3" HeaderText="2329 H.R.YELLOW" SortExpression="BCOLOR3" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="70px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR4" HeaderText="2330 AADAYELL" SortExpression="BCOLOR4" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR5" HeaderText="2331 Y.S.RED" SortExpression="BCOLOR5" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR6" HeaderText="2332 B.S.RED" SortExpression="BCOLOR6" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR7" HeaderText="2333 BLUE" SortExpression="BCOLOR7" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR8" HeaderText="2770 PURPLE" SortExpression="BCOLOR8" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR9" HeaderText="2339 GREEN" SortExpression="BCOLOR9" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="120px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR10" HeaderText="P-89325 SILVER" SortExpression="BCOLOR10" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BCOLOR11" HeaderText="CR-6 VARNISH" SortExpression="BCOLOR11" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidColdes" runat="server" />
          <asp:HiddenField  ID="hidColid" runat="server" />
            <asp:HiddenField ID="hvColGrd" runat="server" />
   </div>
  
    </form>
 
</body>
</html>
