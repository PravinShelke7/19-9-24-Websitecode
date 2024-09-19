<%@ Page Language="VB" MasterPageFile="~/Masters/S1PopUp.master" AutoEventWireup="false" CodeFile="GetEquipmentPopUp.aspx.vb" Inherits="Pages_Sustain1_PopUp_GetAssetPopUp" title="S1-Get Equipment" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   <script type="text/JavaScript">
//         function EquipmentDet(EquipDes, EquipId) {
//              var hypEqDes = document.getElementById('<%= hidEqdes.ClientID%>').value;
//             var hidEqid = document.getElementById('<%= hidEqid.ClientID%>').value;           
//             EquipDes=EquipDes.replace(/##/g,'"');
//           
//             window.opener.document.getElementById(hypEqDes).innerText = EquipDes;
//             window.opener.document.getElementById(hidEqid).value = EquipId
//             window.close();
       //         }

       function EquipmentDet(EquipDes, EquipId, EQUIPDES3) {
           var hidEqdes = document.getElementById('<%= hidEqdes.ClientID%>').value;
           var hidEid = document.getElementById('<%= hidEid.ClientID%>').value;
           var EqName = document.getElementById('<%= hidNew.ClientID%>').value;
           var i = hidEid;
           i = i.match(/\d+$/)[0];
           if (EqName == "") {
               if (EquipDes.length > 38) {
                   var Name = EquipDes.substring(0, 20);
                   var Name1 = EquipDes.substring(20, 38);
                   Name = Name.concat(" ", Name1, "...");
               }
               else {
                   var Name = EquipDes.substring(0, 20);
                   var Name1 = EquipDes.substring(20, 38);
                   Name = Name.concat(" ", Name1);
               }
               if (EQUIPDES3 != "") {
                   window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
               }
               else {
                   window.opener.document.getElementById(hidEqdes).innerText = Name;
               }
               window.opener.document.getElementById(hidEid).value = EquipId;
           }
           else {
               if (EqName.length > 38) {
                   var Name = EqName.substring(0, 20);
                   var Name1 = EqName.substring(20, 38);
                   Name = Name.concat(" ", Name1, "...");
               }
               else {
                   var Name = EqName.substring(0, 20);
                   var Name1 = EqName.substring(20, 38);
                   Name = Name.concat(" ", Name1);
               }
               if (EQUIPDES3 != "") {
                   window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
               }
               else {
                   window.opener.document.getElementById(hidEqdes).innerText = Name;
               }
               window.opener.document.getElementById(hidEid).value = EquipId;
           }
           if (document.getElementById('<%= hidMod.ClientID%>').value == 'S1') {
               if (EquipId == "0") {
                   window.opener.document.getElementById('ctl00_Sustain1ContentPlaceHolder_imgBut' + i).style.display = "none";
                   window.opener.document.getElementById('ctl00_Sustain1ContentPlaceHolder_imgDis' + i).style.display = "none";
               }
               else {
                   if (EQUIPDES3 != "") {
                       window.opener.document.getElementById('ctl00_Sustain1ContentPlaceHolder_imgDis' + i).style.display = "none";
                       window.opener.document.getElementById('ctl00_Sustain1ContentPlaceHolder_imgBut' + i).style.display = "inline";
                   }
                   else {
                       window.opener.document.getElementById('ctl00_Sustain1ContentPlaceHolder_imgDis' + i).style.display = "inline";
                       window.opener.document.getElementById('ctl00_Sustain1ContentPlaceHolder_imgBut' + i).style.display = "none";
                   }
               }
           }
           document.getElementById('<%= hidEqdes.ClientID%>').value = '';
           document.getElementById('<%= hidEid.ClientID%>').value = '';
           document.getElementById('<%= hidNew.ClientID%>').value = '';
           window.close();
       }
     </script> 
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
              <table style="width:500px;" cellpadding="4" cellspacing="0">
                <tr class="AlterNateColor4">
                    <td width="200">
                        Equipment Des1
                    </td>
                     <td width="200">
                        Equipment Des2
                    </td>
                     <td width="100">
                        Equipment Label
                    </td>
                </tr>
           </table>
           <div style="width:520px;height:230px;overflow:auto;">
        
           <asp:GridView Width="500px" runat="server" ID="grdEquipment" DataKeyNames="equipID" AutoGenerateSelectButton="false"
                    AllowPaging="false" AllowSorting="false" AutoGenerateColumns="False" ShowHeader="false"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle CssClass="AlterNateColor4" />
                  
                    <Columns>
                        <asp:BoundField DataField="equipID" HeaderText="equipID" SortExpression="equipID" Visible="false">  
                        </asp:BoundField>
                        <%--  <asp:TemplateField HeaderText="Equipment Des1" SortExpression="MATDE1">                              
                                <ItemTemplate>
                                   <a href="#" onclick="EquipmentDet('<%#Container.DataItem("equipDES1")%>','<%#Container.DataItem("equipID")%>')" class="Link"><%#Container.DataItem("equipID")%>:<%#Container.DataItem("equipDE1")%></a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" Width="50%" />
                                <HeaderStyle HorizontalAlign="Left" /> 
                          </asp:TemplateField>--%>

                          
                        <asp:TemplateField HeaderText="Equipment Des1" SortExpression="MATDE1">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnval" runat="server" CssClass="Link">
                                 <%# Container.DataItem("equipID")%>:<%# Container.DataItem("equipDE1")%></asp:LinkButton>

                                <asp:Label ID="lblName" runat="server" Visible="false" Text='<%# bind("ELabel")%>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" Width="40%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>


                         <asp:BoundField DataField="equipDE2" HeaderText="Equipment Des2" SortExpression="equipDE2" Visible="true"> 
                         <ItemStyle HorizontalAlign="Left" Width="40%" /> 
                         <HeaderStyle HorizontalAlign="Left" /> 
                        </asp:BoundField>
                      
                         <asp:BoundField DataField="ELabel" HeaderText="Equipment Label" SortExpression="ELabel"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="20%" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
               </asp:GridView>
           </div>
           <br />
         </div>
   
          <asp:HiddenField  ID="hidEqdes" runat="server" />
          <asp:HiddenField  ID="hidEqid" runat="server" />
          <asp:HiddenField ID="hidEid" runat="server" />
        <asp:HiddenField ID="hidMod" runat="server" />
        <asp:HiddenField ID="hidNew" runat="server" />
   </div>

</asp:Content>

