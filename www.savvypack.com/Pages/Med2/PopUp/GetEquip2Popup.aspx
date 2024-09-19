<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetEquip2Popup.aspx.vb" Inherits="Pages_MedEcon2_PopUp_GetEquip2Popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Med2-Get Support Equipment</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <script type="text/JavaScript">
         function EquipmentDet(EquipDes, EquipId) {
              var hypEqDes = document.getElementById('<%= hidEqdes.ClientID%>').value;
             var hidEqid = document.getElementById('<%= hidEqid.ClientID%>').value;           
             EquipDes=EquipDes.replace(/##/g,'"');
           
             window.opener.document.getElementById(hypEqDes).innerText = EquipDes;
             window.opener.document.getElementById(hidEqid).value = EquipId
             window.close();
         }
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
                            <b>Equipment De1:</b>
                        </td>
                        <td>
                          <asp:TextBox ID="txtMatDe1" runat="server" CssClass="SearchTextBox" Width="200px"></asp:TextBox>
                        </td>
                    </tr>
                      <tr>
                         <td align="right">
                            <b>Equipment De2:</b>
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
                        Equipment Des1
                    </td>
                     <td>
                        Equipment Des2
                    </td>
                </tr>
           </table>
           <div style="width:450px;height:230px;overflow:auto;">
        
           <asp:GridView Width="400px" runat="server" ID="grdEquipment" DataKeyNames="equipID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="equipID" HeaderText="equipID" SortExpression="equipID" Visible="false">  
                        </asp:BoundField>
                          <asp:TemplateField HeaderText="Equipment Des1" SortExpression="MATDE1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="EquipmentDet('<%#Container.DataItem("equipDES1")%>','<%#Container.DataItem("equipID")%>')" class="Link"><%#Container.DataItem("equipID")%>:<%#Container.DataItem("equipDE1")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>
                         <asp:BoundField DataField="equipDE2" HeaderText="Equipment Des2" SortExpression="equipDE2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                      
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidEqdes" runat="server" />
          <asp:HiddenField  ID="hidEqid" runat="server" />
   </div>
  
    </form>
 
</body>
</html>