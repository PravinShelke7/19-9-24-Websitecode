<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetDeptPlantConfig.aspx.vb" Inherits="Pages_Echem1_PopUp_GetDeptPlantConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Echem1-Get Department</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function MaterialDet(MatDes, MatId) {
             var hidMatdes = document.getElementById('<%= hidDepdes.ClientID%>').value
             var hidMatId = document.getElementById('<%= hidDepId.ClientID%>').value
             window.opener.document.getElementById(hidMatdes).innerText = MatDes;
             window.opener.document.getElementById(hidMatId).value = MatId
             window.close();
         }
     </script> 
     <script type="text/javascript">

  var _gaq = _gaq || [];
  _gaq.push(['_setAccount', 'UA-16991293-1']);
  _gaq.push(['_trackPageview']);

  (function() {
    var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
    ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + 
'.google-analytics.com/ga.js';
    var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
  })();

</script>
</head>
<body>
<form id="form1" runat="server">
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
                                   <a href="#" onclick="MaterialDet('<%#Container.DataItem("PROCDE")%>','<%#Container.DataItem("PROCID")%>')" class="Link"><%#Container.DataItem("PROCDE1")%></a>
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
  
    </form>
 
</body>
</html>
