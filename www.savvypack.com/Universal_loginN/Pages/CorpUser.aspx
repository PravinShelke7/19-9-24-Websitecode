<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CorpUser.aspx.vb" Inherits="Universal_loginN_Pages_CorpUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Corporate User Management</title>
       <link href="../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <link href="../../App_Themes/SkinFile/E1Print.css" rel="stylesheet" type="text/css" media="print" />
    <script type="text/javascript">
   
        javascript: window.history.forward(1); 
    
        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            return false;


        }
        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }
        function ShowEditWindow(Page) {
         
        }
        function AddRemove(txt,id)
        {
            document.getElementById('hfBtnText').value=txt;
            document.getElementById('hfUserId').value=id;
    
            var btnAddRemove=document.getElementById('btnAddRemove'); 
            
            btnAddRemove.click();
            return false;
        }
    
    </script>
</head>
<body style="margin-top:5px">
    <form id="form1" runat="server">
    <div id="MasterContent">
    
    <div id="AlliedLogo">
    <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
    </div>
    
    <div>
    <table class="ULoginModule1" cellpadding="0" cellspacing="0" style="border-collapse:collapse">
        <tr>
             <td style="padding-left:490px">
                    <table cellpadding="0" cellspacing="5"   style="border-collapse:collapse">
                        <tr>                
                                <td>
                                      <asp:ImageButton ID="btnLogOff" runat="server" ImageUrl="~/Images/LogOff.gif" 
                                        ToolTip="Return To SavvyPack Corporation Home Page"  />
                                      
                                </td> 
                                         
                                                                                          
                                
                        </tr>
                    </table>
               </td>
        </tr>
    </table>
    </div>
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    
    </div>
    <table class="ContentPage" id="ContentPage" runat="server">
        <tr>
                <td style="text-align:left;">                            
                  <br />              
                     <div class="PageHeading" id="div2" style="width:840px;">
                        User Management
                     </div>                                 
                </td>
            </tr>
             <tr>
                <td style="text-align:left;">                            
                  <br />              
                     <div class="PageHeading" id="div1" style="width:840px;">
                         <asp:LinkButton ID="lnkAddEditUser" runat="Server" CssClass="Link" Font-Size="14px" style="margin-left:10px;" PostBackUrl="~/Universal_loginN/Pages/AddEditUser.aspx?Type=Add">
                             Add New User
                         </asp:LinkButton>                     
                         <asp:Button ID="btnAddRemove" runat="server" style="display:none"/></div>                                 
                </td>
            </tr>
      
          <tr>
              <td>
                 <div id="PageSection1" style="text-align:left">
                      <div>
                         <asp:Table ID="tblComparision" runat="server" CellPadding="0">
                        
                        </asp:Table>
                      </div>
                      <div class="DivScrollesU" >
                          <asp:Table ID="tblComparisionInner" runat="server" CellPadding="0" style="margin-top:-3px;">
                        
                        </asp:Table>
                      </div>
                      
                       
                 </div>
              </td>
            </tr>
        
    
      
       
        <tr  class="AlterNateColor3">
            <td class="PageSHeading" align="center">
             <asp:Label ID="lblTag" runat="Server" ></asp:Label>
            </td>
       </tr>
       <tr>
       <td>
       <asp:HiddenField ID="hfBtnText" runat="server" value=""/>
       <asp:HiddenField ID="hfUserId" Value="" runat="server" />
       </td>
       </tr>
    </table>
    </form>
</body>
</html>
