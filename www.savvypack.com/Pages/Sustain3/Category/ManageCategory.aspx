<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false"
    CodeFile="ManageCategory.aspx.vb" Inherits="Pages_Sustain3_Category_ManageCategory"
    Title="S3-Manage Category" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" runat="Server">

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

    <script type="text/javascript" language="javascript">
      function requireCategoryCheck()
    {
        var catSet=document.getElementById("<%=ddlCatSet.ClientId%>");
        var val = catSet.options[catSet.selectedIndex].value;
        
         var txtCount=document.getElementById("<%=txtCatCount.ClientId%>").value;
      // alert(val);
        if(val=="-1")
        {
          alert("Please select Category Set name!");
          return false;
        }
        else
        {
          if(txtCount=="")
          {
             alert("Please enter Category count!");
             return false;
          }
          else
          {
               if (IsNumeric(txtCount) == false) 
              { 
                 alert("Please enter numeric value for Category count!!!");
                 return false;
               }
               else
               {
                return true;
                }
          }
          
        }
    }
    function IsNumeric(sText)

{
   var ValidChars = "0123456789.";
   var IsNumber=true;
   var Char;

 
   for (i = 0; i < sText.length && IsNumber == true; i++) 
      { 
      Char = sText.charAt(i); 
      if (ValidChars.indexOf(Char) == -1) 
         {
         IsNumber = false;
         }
      }
   return IsNumber;
   
   }

    function requiredField()
    {
        var txtCount=document.getElementById("<%=txtCatSet.ClientId%>").value;
        if(txtCount=="")
        {
          alert("Please enter Category Set name!");
          return false;
        }
    }
     function checkDeletedDrop()
    {
        var delCom=document.getElementById("<%=ddlCatSetD.ClientId%>");
         var val = delCom.options[delCom.selectedIndex].value;
        if(val=="-1")
        {
          alert("Please select Category Set name!");
          return false;
        }
        var  msg = "You are going to delete Category set " + delCom.options[delCom.selectedIndex].text + ". Do you want to proceed?";
          if (confirm(msg)) 
          {
                return true;
          }
          else 
          {
                return false;
          }
    }
      function CheckCount()
      {       
        var txtCount=document.getElementById("<%=txtCatCount.ClientId%>").value;
        //var PrevCount=document.getElementById("ctl00$Sustain3ContentPlaceHolder$hdnPrevCount").value;
        var PrevCount=document.getElementById("<%=hdnPrevCount.ClientId%>").value;
        if(txtCount=="")
        {
          alert("Please enter Category Count!");
          return false;
        }
       else if(eval(txtCount)>8)
        {
          alert("You can not create more than 8 category!");
          return false; 
        }
        else
        {
            //return true;
             
           if(PrevCount>txtCount)
           {
            
              var answer=confirm("Previosly you added "+PrevCount+" categories,Are you sure to make it "+txtCount+"?");
              if(answer==true)
                {   
                    return true;
                }
                else if(answer==false)
                {       
                  return false;
                }
           }
        }
      }
      
      function CheckCategory()
      {
           var txtCount=document.getElementById("<%=txtCatCount.ClientId%>").value;
           var i=0;
           for(i=1;i<=txtCount;i++)
           {
             var txtCat=document.getElementById("ctl00_Sustain3ContentPlaceHolder_CATNAME"+i).value;
          
             if(txtCat=="")
             {
               alert("Please enter all category name properly!");
               return false;
             }            
           }
      }
    </script>

    <table class="ContentPage" id="ContentPage" runat="server" style="width: 820px">
        <tr style="height: 20px">
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip('S3 - Manage Category Set')"
                    onmouseout="UnTip()" style="width: 820px;">
                    Sustain3 - Manage Category Set
                </div>
                <div style="text-align: right">
                    <asp:HyperLink ID="HyperLink1" runat="server" Text="Go to Charts" CssClass="Link"
                        Style="font-size: 15px;" Font-Bold="true" NavigateUrl="~/Charts/S3Charts/CErgyGhg.aspx"></asp:HyperLink>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="ContentPagemargin">
                    <div id="PageSection1" style="text-align: left; margin-left: 0px;">
                        <table width="820px">
                            <tr>
                                <td style="height: 10px; width: 100%" align="left" colspan="2">
                                    
                                </td>
                            </tr> 
                            <tr>
                               <td style="width:40%">
                                  <table>
                                        <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                Add Category Set
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="width: 15%" align="right">
                                                Page Name:
                                            </td>
                                            <td style="width:70%">
                                                <asp:DropDownList ID="ddlPName" runat="server" CssClass="DropDown" Width="200">
                                                    <asp:ListItem Text="Energy" Value="ERGY"></asp:ListItem>
                                                    <asp:ListItem Text="GHG" Value="GHG"></asp:ListItem>
                                                    <asp:ListItem Text="Water" Value="WATER"></asp:ListItem>
                                                  
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="width: 15%" align="right">
                                                Category Set:
                                            </td>
                                            <td style="width:70%">
                                                <asp:TextBox ID="txtCatSet" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                    width: 230px" MaxLength="90"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Add" CssClass="ButtonWMarigin" Style="width: 50px;"
                                                    OnClientClick="return requiredField();" onmouseover="Tip('Create a Category Set')"
                                                    onmouseout="UnTip()" />
                                            </td>
                                        </tr>
                                  </table>
                               </td>
                               <td style="width:40%" align="left">
                                  <table>
                                       <tr class="AlterNateColor4">
                                            <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                                Delete Category Set
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="width: 15%" align="right">
                                                Page Name:
                                            </td>
                                            <td style="width:70%">
                                                <asp:DropDownList ID="ddlPNameD" runat="server" CssClass="DropDown" Width="200" AutoPostBack="true">
                                                     <asp:ListItem Text="Energy" Value="ERGY"></asp:ListItem>
                                                    <asp:ListItem Text="GHG" Value="GHG"></asp:ListItem>
                                                    <asp:ListItem Text="Water" Value="WATER"></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="width: 15%" align="right">
                                                Category Set:
                                            </td>
                                            <td style="width:70%">
                                                <asp:DropDownList ID="ddlCatSetD" CssClass="DropDown" Width="200" runat="server">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="ButtonWMarigin"
                                                    Style="width: 80px;" onmouseover="Tip('delete Category Set')" onmouseout="UnTip()"
                                                    OnClientClick="return checkDeletedDrop();" />
                                            </td>
                                        </tr>
                                  </table>
                               </td>
                            </tr>
                        </table>
                        <table width="820px">
                            <tr>
                                <td style="height: 10px;">
                                </td>
                            </tr>
                            <tr class="AlterNateColor4">
                                <td class="PageSHeading" style="font-size: 14px;" colspan="2">
                                    Add Category to Category Set
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Page Name:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlPNameE" runat="server" CssClass="DropDown" Width="200" AutoPostBack="true">
                                         <asp:ListItem Text="Energy" Value="ERGY"></asp:ListItem>
                                        <asp:ListItem Text="GHG" Value="GHG"></asp:ListItem>
                                        <asp:ListItem Text="Water" Value="WATER"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Category Set:
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlCatSet" CssClass="DropDown" Width="35%" runat="server" AutoPostBack="true">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td style="width: 10%" align="right">
                                    Category Count:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCatCount" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                        width: 90px" MaxLength="230"></asp:TextBox>
                                    <asp:Label ID="lblMsg" runat="server" Style="color: Red"></asp:Label>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButtonWMarigin"
                                        Style="width: 80px;" OnClientClick="return requireCategoryCheck();" onmouseover="Tip('Create a Category')"
                                        onmouseout="UnTip()" />
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" id="trTable" runat="server" visible="false">
                                <td>
                                </td>
                                <td>
                                    <asp:Table ID="tblComp" runat="server">
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" id="trButton" runat="server" visible="false">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnAddCat" runat="server" Text="Update" CssClass="ButtonWMarigin"
                                        Style="width: 80px;" onmouseover="Tip('Submit')" onmouseout="UnTip()" OnClientClick="return CheckCategory();" />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                        Style="width: 80px;" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                </td>
                            </tr>
                        </table>
                          <table width="100%">
                                 <tr>
                                    <td style="height:3px;">
                                    </td>
                                </tr>
                                <tr class="AlterNateColor4">
                                    <td class="PageSHeading" style="font-size:14px;">
                                            Manage Category
                                    </td>
                                 </tr>
                                  <tr class="AlterNateColor1">
                                    <td>  
                                        <asp:Button ID="btnAddCategory" runat="server"  CssClass="Button" Width="170px" Text="Go To Add/Edit Category" style="margin-left:0px;" />                                        
                                    </td>
                                 </tr>   
                                
                                 
                            </table>  
                        <asp:HiddenField ID="hdnPrevCount" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
