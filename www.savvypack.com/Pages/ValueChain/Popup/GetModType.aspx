<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetModType.aspx.vb" Inherits="Pages_ValueChain_Popup_GetModType" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Get Module Type</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function MaterialDet(MatDes, MatId) {
             var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value
             var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value
              var hidModId = document.getElementById('<%= hidModId.ClientID%>').value
            
             if(hidModId!=MatId)
             {
                var hidCasedes = document.getElementById('<%= hCaseDes.ClientID%>').value
                var hidCaseId = document.getElementById('<%= hCaseId.ClientID%>').value
             //alert(MatDes.length);
                window.opener.document.getElementById(hidMatdes).innerText = MatDes
                window.opener.document.getElementById(hidMatId).value = MatId
                window.opener.document.getElementById(hidCasedes).innerText = 'Nothing Selected';
                window.opener.document.getElementById(hidCaseId).value = 0;
              }
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
                            <b>Module Type:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtMatDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
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
                        Module Type
                    </td>
                     <td>
                        
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdMaterials" DataKeyNames="TYPEID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="TYPEID" HeaderText="TYPEID" SortExpression="TYPEID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Material Des1" SortExpression="MATDE1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="MaterialDet('<%#Container.DataItem("DES")%>','<%#Container.DataItem("TYPEID")%>')" class="Link"><%#Container.DataItem("TYPEID")%>:<%#Container.DataItem("DES")%></a>
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
          
           <asp:HiddenField  ID="hCaseDes" runat="server" />
          <asp:HiddenField  ID="hCaseId" runat="server" />
           <asp:HiddenField  ID="hidModId" runat="server" />
   </div>
  
    </form>
 
</body>
</html>