<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PackSpecCompany.aspx.vb" Inherits="Pages_Econ1_PopUp_PackSpecCompany" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1-Package Specification Company</title> 
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function CaseSearch(GroupDes, GroupID) {
             var hidGroupdes = document.getElementById('<%= hidGroupDes.ClientID%>').value;
             var hidGroupId = document.getElementById('<%= hidGroupId.ClientID%>').value;            
             GroupDes = GroupDes.replace(/##/g, '"');
             
                 window.opener.document.getElementById(hidGroupdes).innerText = GroupDes;
                 window.opener.document.getElementById(hidGroupId).value = GroupID
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
    <div id="ContentPagemargin" style="width:100%;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
           
             <table cellspacing="0" style="text-align:center;width:90%">
            <tr>
                        <td align="center" style="font-family:Arial;font-size:14px;font-weight:bold;height:15px;">
                          
                        </td>
                  </tr>
                    <tr>
                        <td align="center" style="font-family:Arial;font-size:15px;font-weight:bold;">
                           <asp:label id="lblStatus" runat="server" >Package Specification Company</asp:label>
                        </td>
                  </tr>
                   <tr>
                        <td align="center" style="font-family:Arial;font-size:14px;font-weight:bold;height:15px;">
                          
                        </td>
                  </tr>
                </table>
           <table>
               <tr>
                    <td>
                        <b><asp:LinkButton ID="lnkCmpny"  runat="server" ForeColor ="Black">Add Company</asp:LinkButton></b>
                   </td>
               </tr>
           </table>
                <table cellspacing="0" style="text-align:left;display:none;width:100%" id="NewGrp" runat ="server" >
                    <tr class="AlterNateColor4" >
                        <td style="text-align:left;" >
                        </td>
                        <td align="left">
                            <b> <asp:label id="Label1" runat="server" Text ="Package Specification Company : " ></asp:label></b> 
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGrpName" runat="server" CssClass="MediumTextBox" style="text-align: left;width:260px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr class="AlterNateColor4">
                        <td style="text-align:left;" >
                        </td>
                        <td align="left">
                        </td>
                        <td align="left">
                            <asp:Button ID="btnadd" runat="server" Text="Add" CssClass="Button"  />
                        </td>
                    </tr>
            </table>
            <br />
           <div style="width:500px;height:370px;overflow:auto;">
        
           <asp:GridView Width="450px" runat="server" ID="grdCaseSearch" DataKeyNames="COMPANYID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="true"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="COMPANYID" HeaderText="COMPANYID" SortExpression="COMPANYID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Package Specification Company" SortExpression="COMPANYNAME">                              
                                <ItemTemplate>
                                   <a href="#" onclick="CaseSearch('<%#Container.DataItem("COMPANYNAME")%>','<%#Container.DataItem("COMPANYID")%>')" class="Link">
                                      <%#Container.DataItem("COMPANYNAME")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="30%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                      
                        
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidGroupId" runat="server" />
          <asp:HiddenField ID="hidGroupDes" runat="server" />
           <asp:HiddenField  ID="hidGroupidD" runat="server" />
   </div>
    </form>
</body>
</html>
