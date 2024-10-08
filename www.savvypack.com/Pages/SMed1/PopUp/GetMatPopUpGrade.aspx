﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetMatPopUpGrade.aspx.vb" Inherits="Pages_MedSustain1_PopUp_GetMatPopUpGrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SMed1-Get Materials</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function MaterialDet(MatDes, MatId, GradeName, GradeId, Weight, SG) {
             var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value;
             var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value;

             var hidGradedes = document.getElementById('<%= hidGradedes.ClientID%>').value;
             var hidGradeId = document.getElementById('<%= hidGradeid.ClientID%>').value;

             var hidSGV = document.getElementById('<%= hidSG.ClientID%>').value;
             // alert(window.opener.document.getElementById(hidSGV).value + '@@@' + Weight + '###' + SG);
             if (Weight != SG) {
                 window.opener.document.getElementById(hidSGV).value = Weight;
             }
else {
                window.opener.document.getElementById(hidSGV).value = "0.000";
            }
             //alert(MatDes.length);
             window.opener.document.getElementById(hidMatdes).innerText = MatDes;
             window.opener.document.getElementById(hidMatId).value = MatId;

             window.opener.document.getElementById(hidGradedes).innerText = GradeName;
             window.opener.document.getElementById(hidGradeId).value = GradeId;

             //alert(GradeName + '-----' + GradeId);
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
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Material De1:</b>
                        </td>
                        <td> 
                          <asp:TextBox ID="txtMatDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                         <td align="right">
                            <b>Material De2:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtMatDe2" runat="server" CssClass="SearchTextBox"  Width="200px"></asp:TextBox>
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
                        Material Des1
                    </td>
                     <td>
                        Material Des1
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdMaterials" DataKeyNames="MATID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="MATID" HeaderText="MATID" SortExpression="MATID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Material Des1" SortExpression="MATDE1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="MaterialDet('<%#Container.DataItem("MATDES")%>','<%#Container.DataItem("MATID")%>','<%#Container.DataItem("GRADENAME")%>','<%#Container.DataItem("GRADEID")%>','<%#Container.DataItem("WEIGHT")%>','<%#Container.DataItem("SG")%>')" class="Link"><%#Container.DataItem("MATID")%>:<%#Container.DataItem("MATDE1")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="MATDE2" HeaderText="Material Des2" SortExpression="MATDE2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                        
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidMatdes" runat="server" />
          <asp:HiddenField  ID="hidMatid" runat="server" />
           
           <asp:HiddenField  ID="hidGradedes" runat="server" />
          <asp:HiddenField  ID="hidGradeid" runat="server" />

             <asp:HiddenField  ID="hidSG" runat="server" />
   </div>
  
    </form>
 
</body>
</html>