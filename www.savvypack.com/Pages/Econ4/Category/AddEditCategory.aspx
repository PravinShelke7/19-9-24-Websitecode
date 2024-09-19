<%@ Page Language="VB" MasterPageFile="~/Masters/Econ4.master" AutoEventWireup="false" CodeFile="AddEditCategory.aspx.vb" 
Inherits="Pages_Econ4_Category_AddEditCategory" title="Add/Edit Category Item" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ4ContentPlaceHolder" Runat="Server">
<script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>    
    <script language="javascript" type="text/javascript">
    function checkDeletedDrop()
    {

           var comboSet = document.getElementById('<%=ddlCatSet.ClientID%>'); 
           var val1 = comboSet.options[comboSet.selectedIndex].value;
           var comboCat = document.getElementById('<%=ddlCat.ClientID%>'); 
           var val2 = comboCat.options[comboCat.selectedIndex].value;
     
           if(val1>0 && val2>0)
           {
            
              var  msg = "You are going to delete Category " + comboCat.options[comboCat.selectedIndex].text + ". Do you want to proceed?";
              if (confirm(msg)) 
              {
                    return true;
              }
              else 
              {
                    return false;
              }
           }
           else
           {
             if(val1<0)
             {
                var msg = "-----------------------------------------------------\n";
                msg += "Please select Category set.\n";
                msg += "-----------------------------------------------------\n";
                alert(msg);         
             }
             else
             {
                  var msg = "-----------------------------------------------------\n";
                  msg += "Please select Category.\n";
                  msg += "-----------------------------------------------------\n";
                  alert(msg);
             }       
             return false;
           }
       
      
       
    }
    function ValidateData()
    {
     
       var comboSet = document.getElementById('<%=ddlCatSet.ClientID%>'); 
       var val1 = comboSet.options[comboSet.selectedIndex].value;
       var comboCat = document.getElementById('<%=ddlCat.ClientID%>'); 
        var val2 = comboCat.options[comboCat.selectedIndex].value;
     
       if(val1>0 && val2>0)
       {
        
         return true;
       }
       else
       {
         if(val1<0)
         {
            var msg = "-----------------------------------------------------\n";
            msg += "Please select Category set.\n";
            msg += "-----------------------------------------------------\n";
            alert(msg);         
         }
         else
         {
              var msg = "-----------------------------------------------------\n";
              msg += "Please select Category.\n";
              msg += "-----------------------------------------------------\n";
              alert(msg);
         }       
         return false;
       }
    }
    function renameCheck(id)
    {
     
               var comboSet = document.getElementById('<%=ddlCatSet.ClientID%>');                
               var val1 = comboSet.options[comboSet.selectedIndex].value;
               
              var comboCat = document.getElementById('<%=ddlCat.ClientID%>');          
              var val2 = comboCat.options[comboCat.selectedIndex].value;
           
               if(val1>0 && val2>0)
               {
                   MakeVisible();
                   return false;
               }
               else
               {
                 if(val1<0)
                 {
                    var msg = "-----------------------------------------------------\n";
                    msg += "Please select Category set.\n";
                    msg += "-----------------------------------------------------\n";
                    alert(msg);         
                 }
                 else
                 {
                      var msg = "-----------------------------------------------------\n";
                      msg += "Please select Category.\n";
                      msg += "-----------------------------------------------------\n";
                      alert(msg);
                 }       
                 return false;
               }
    
    }
    
      function MakeVisible() 
      {          
            objItemElement = document.getElementById("<%=divRename.ClientID%>");  
            objItemElement.style.display = "inline";
            GetCatDes();
            return false;
        }
         function GetCatDes() 
         {

            var combo1 = document.getElementById("<%=ddlCat.ClientID%>");
            var val = combo1.options[combo1.selectedIndex].text;           
            document.getElementById('<%=txtCat.ClientID%>').value = trimAll(val);
        }
        function trimAll(sString) 
        {
            while (sString.substring(0, 1) == ' ') {
                sString = sString.substring(1, sString.length);
            }
            while (sString.substring(sString.length - 1, sString.length) == ' ') {
                sString = sString.substring(0, sString.length - 1);
            }
            return sString;
        }
       function MakeInVisible() 
        {
            objItemElement = document.getElementById("<%=divRename.ClientId %>");
            objItemElement.style.display = "none";
            return false;
        }
     function getCountForward()
     {
         var lst1= document.getElementById('<%=lstRegion1.ClientID%>');
                 
         var count1= 0;
               
         for (i = 0; i < lst1.options.length; i++) 
         {
          if (lst1[i].selected) 
          {
            count1=count1+1;
          } 
        }
        if(count1>0)
        {
           return true;
        }
        else
        {
       
           var msg = "-----------------------------------------------------\n";
            msg += "Please select atleast one item to transfer.\n";
            msg += "-----------------------------------------------------\n";
            alert(msg);
           return false;
         }
       
       
     }
     function getCountBackward()
     {
      
          var lst1= document.getElementById('<%=lstRegion2.ClientID%>');
         var count1= 0;
          var count2 = lst1.options.length;
         
               
         for (i = 0; i < lst1.options.length; i++) 
         {
          if (lst1[i].selected) 
          {
            count1=count1+1;
          } 
        }
        if(count1==count2)
        {
            var msg = "-----------------------------------------------------\n";
            msg += "You can not delete all the items.\n";
            msg += "-----------------------------------------------------\n";
            alert(msg);
           return false;
        }
        
        if(count1>0)
        {
           return true;
        }
        else
        {
       
           var msg = "-----------------------------------------------------\n";
            msg += "Please select atleast one item to transfer.\n";
            msg += "-----------------------------------------------------\n";
            alert(msg);
           return false;
         }
       
       
     }
    </script>
    
    <table class="ContentPage" id="ContentPage" runat="server" style="width: 840px">
        <tr style="height: 20px">
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('E4 - Manage Category Items')"
                    onmouseout="UnTip()" style="width: 800px;">
                    Econ4 - Manage Category Items
                </div>
                <div style="text-align:right ">
                   <asp:HyperLink ID="lnkEditCategory" runat="server" Text="Go to Charts" CssClass="Link" style="font-size:13px;"
                                    Font-Bold="true" NavigateUrl="~/Charts/E4Charts/CPrftCost.aspx"></asp:HyperLink>
                                    
                                    <asp:HyperLink ID="lnkPrev" runat="server" Text="Go to Prev. Page" CssClass="Link" style="font-size:13px;margin-left:10px;"
                                    Font-Bold="true" NavigateUrl="~/Pages/Econ4/Category/ManageCategory.aspx"  ></asp:HyperLink>
                </div>
            
            </td>
        </tr>
        <tr>
            <td>
                <div id="ContentPagemargin">
                    <div id="PageSection1" style="text-align: left; margin-left: 0px;">
                        <table width="810px">
                            <tr>
                                <td style="height: 10px;width:100%" align="right" colspan="2" >
                               
                                </td>
                            </tr>
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                    Add/Edit Category Items
                                </td>
                            </tr>
                              <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Page Name:
                                </td>
                                <td>
                                     <asp:DropDownList ID="ddlPName" runat="server" CssClass="DropDown"  Width="200" AutoPostBack="true" >
                                   <asp:ListItem  Text="Profit and Loss" Value="PFT"></asp:ListItem>
                                   <asp:ListItem  Text="Profit and Loss with Depreciation" Value="PFTD"></asp:ListItem>
                                     <asp:ListItem  Text="Cost" Value="COST"></asp:ListItem>
                                   <asp:ListItem  Text="Cost with Depreciation" Value="COSTD"></asp:ListItem>
                                 </asp:DropDownList>                               
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Category set:
                                </td>
                                <td>
                                      <asp:DropDownList ID="ddlCatSet" CssClass="DropDown" Width="35%" runat="server" AutoPostBack="true">
                                     </asp:DropDownList>                                   
                                </td>
                            </tr>
                             <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Category:
                                </td>
                                <td>
                                      <asp:DropDownList ID="ddlCat" CssClass="DropDown" Width="35%" runat="server" AutoPostBack="false">
                                     </asp:DropDownList>                                   
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnAdd" runat="server" Text="Add Items" CssClass="ButtonWMarigin" Style="width: 80px;" OnClientClick="return ValidateData();"
                                                                        onmouseover="Tip('Add Items')" onmouseout="UnTip()" />
                                          <asp:Button ID="btnRename" runat="server" Text="Rename Category" OnClientClick="return renameCheck('divRename')" CssClass="ButtonWMarigin" Style="width: 120px;" 
                                        onmouseover="Tip('Rename Category')" onmouseout="UnTip()" />
                                           <asp:Button ID="btnDelete" runat="server" Text="Delete Category" CssClass="ButtonWMarigin" Style="width: 120px;" 
                                        onmouseover="Tip('Delete Category')" onmouseout="UnTip()" OnClientClick="return checkDeletedDrop();"  />
                                </td>
                            </tr>
                           <tr id="trList" runat="server" visible="false" >
                             <td colspan="3">
                                <table>
                                    <tr>
                                        <td valign="middle">
                                            <asp:Panel ID="pnlRegion1" runat="server" Width="360px">
                                                <asp:ListBox ID="lstRegion1" runat="server" Width="360px" Rows="20" SelectionMode="Multiple"
                                                    Style="font-family: Verdana; font-size: 11px;"></asp:ListBox>
                                            </asp:Panel>
                                        </td>
                                        <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                                            <asp:Button ID="btnFwd" runat="server" Text=">" Width="50px" OnClientClick="return getCountForward()" />
                                            <br />
                                            <br />
                                            <asp:Button ID="btnRew" runat="server" Text="<" Width="50px" OnClientClick="return getCountBackward()" />
                                             <br />
                                            <br />
                                            <asp:ImageButton ID="btnOk" runat="server" ImageUrl="~/Images/button_ok.png" style="margin-left:5px;" Width="32px" Height="20px" ToolTip="Ok" />

                                        </td>
                                        <td valign="middle">
                                            <asp:Panel ID="pnlRegion2" runat="server" Width="360px">
                                                <asp:ListBox ID="lstRegion2" runat="server" Width="360px" Rows="20" SelectionMode="Multiple"
                                                    Style="font-family: Verdana; font-size: 11px;"></asp:ListBox>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                             </td>
                           </tr>
                             <tr id="divRename" runat="server" style="display: none; text-align: left; width:200px;"
                                class="AlterNateColor1">
                                <td style="width:20%">
                                    <b>Category</b>
                                </td>
                                <td colspan="2" style="width:60%">
                                    <asp:TextBox ID="txtCat" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                        width: 230px" MaxLength="25"></asp:TextBox>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="Button" Style="margin-left: 0px;">
                                    </asp:Button>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancle" CssClass="Button" OnClientClick="return MakeInVisible()"
                                        Style="margin-left: 0px;"></asp:Button>
                                </td>
                            </tr>
                            
                        </table>
                    
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

