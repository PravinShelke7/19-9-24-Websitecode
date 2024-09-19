<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PackSpecGroup.aspx.vb" Inherits="Pages_Econ1_PopUp_PackSpecGroup" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1-Package Specification Group</title> 
     <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function CaseSearch(GroupDes, GroupID) {
             // alert(CaseDes+' '+CaseId);
             var hidGroupdes = document.getElementById('<%= hidGroupDes.ClientID%>').value;
             // alert(hidCasedes);
             var hidGroupId = document.getElementById('<%= hidGroupId.ClientID%>').value;
             //alert(GroupID);

             GroupDes = GroupDes.replace(/##/g, '"');
            // alert(hidGroupdes);
           
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
                           <asp:label id="lblStatus" runat="server" >Package Specification Group</asp:label>
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
                        <b><asp:LinkButton ID="lnkGroup"  runat="server" ForeColor ="Black">Add Group</asp:LinkButton></b>
                   </td>
               </tr>
           </table>
                <table cellspacing="0" style="text-align:left;display:none;width:100%" id="NewGrp" runat ="server" >
                    <tr class="AlterNateColor4" >
                        <td style="text-align:left;" >
                        </td>
                        <td align="left">
                             <b> <asp:label id="Label1" runat="server" Text ="Package Specification Group : " ></asp:label></b> 
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtGrpName" runat="server" CssClass="MediumTextBox" style="text-align: left;width:280px;"></asp:TextBox>
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
        
           <asp:GridView Width="450px" runat="server" ID="grdCaseSearch" DataKeyNames="PACKSPECGRPID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="true"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="PACKSPECGRPID" HeaderText="PACKSPECGRPID" SortExpression="PACKSPECGRPID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Package Specification Group" SortExpression="GRPDETAIL">                              
                                <ItemTemplate>
                                   <a href="#" onclick="CaseSearch('<%#Container.DataItem("GRPDETAIL")%>','<%#Container.DataItem("PACKSPECGRPID")%>')" class="Link">
                                      <%#Container.DataItem("GRPDETAIL")%></a>
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