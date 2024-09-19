<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetInjectionMaterial.aspx.vb" Inherits="Pages_MedEcon1_PopUp_GetInjectionMaterial" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Med1-Get Injection Material</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function ColorDet(MatDes, MatId) {
             var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value
             var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value
             //alert(ColDes.length);
             //alert(hidMatdes);
             window.opener.document.getElementById(hidMatdes).innerText = MatDes
             window.opener.document.getElementById(hidMatId).value = MatId
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
                            <asp:LinkButton CssClass="Link" CausesValidation="false" ID="InjShowAll" runat="server" />
                        </td>
                  </tr>
                </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="900px" runat="server" ID="grdInject" DataKeyNames="MATID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False" ShowHeader="true" CellSpacing="2"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" ForeColor="White" />
                  
                    <Columns>
                        <asp:BoundField DataField="MATID" HeaderText="MATID" SortExpression="MATID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Material Des" SortExpression="MATDE2" >                              
                                <ItemTemplate>
                                   <a href="#" onclick="ColorDet('<%#Container.DataItem("MATDES")%>','<%#Container.DataItem("MATID")%>')" class="Link"><%# Container.DataItem("MATDE1")%>:<%# Container.DataItem("MATDE2")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="250px" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="PRICE" HeaderText="PRICE" SortExpression="PRICE" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="120px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="SG" HeaderText="SG" SortExpression="SG" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="SERVERDATE" HeaderText="SERVERDATE" SortExpression="SERVERDATE" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="BW" HeaderText="BW" SortExpression="BW" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="70px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="THICK" HeaderText="THICK" SortExpression="THICK" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="MATDE3" HeaderText="MATDE3" SortExpression="MATDE3" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="INJMOLD" HeaderText="INJMOLD" SortExpression="INJMOLD" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="MELTDENSITY" HeaderText="MELTDENSITY" SortExpression="MELTDENSITY" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="INJTEMPMELT" HeaderText="INJTEMPMELT" SortExpression="INJTEMPMELT" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="INJTEMPTOOL" HeaderText="INJTEMPTOOL" SortExpression="INJTEMPTOOL" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="120px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="INJTEMPEJECT" HeaderText="INJTEMPEJECT" SortExpression="INJTEMPEJECT" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>

                         <asp:BoundField DataField="EFFDIFFUSIVITY" HeaderText="EFFDIFFUSIVITY" SortExpression="EFFDIFFUSIVITY" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                        <asp:BoundField DataField="CFFACTOR" HeaderText="CFFACTOR" SortExpression="CFFACTOR" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="60px" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
          
          
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidMatdes" runat="server" />
          <asp:HiddenField  ID="hidMatid" runat="server" />
            <asp:HiddenField ID="hvMatGrd" runat="server" />
   </div>
  
    </form>
 
</body>
</html>
