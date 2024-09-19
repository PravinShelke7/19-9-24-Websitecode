<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CResultsplDep.aspx.vb" Inherits="Pages_Econ2_Results_CResultsplDep" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>E2-Profit and Loss With Depreciation Comparison</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
</head>

<body>
    <form id="form2" runat="server">
    <div id="MasterContent">
       
      <%--<div id="AlliedLogo">
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>   --%>     
      
      <div>
        <table class="E2Module" cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
            <tr>
               <td style="padding-left:490px">
                    
               </td>
            </tr>
        </table>
       </div>
            
      <div id="error">
            <asp:Label ID="_lErrorLble" runat="server" CssClass="Error"></asp:Label>
          </div>
    
       <table class="ContentPage" id="ContentPage" runat="server">
            <tr>
                <td>
                     <div class="PageHeading" id="divMainHeading" style="width:840px;">
                         Econ2 - Profit and Loss With Depreciation Comparison
                     </div>
    
                   
                                    
                </td>
            </tr>
             <tr style="height:20px">
                <td>
                <div id="ContentPagemargin" runat="server">
                   <div id="PageSection1" style="text-align:left;" >
                    <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                   </div>
                 </div>
                 </td>
             </tr>
              <tr class="AlterNateColor3">
             <td class="PageSHeading" align="center">
                <asp:Label ID="lblTag" runat="Server" ></asp:Label>
            </td>
           </tr>
          </table>
   
   </div>
   <div id="AlliedLogo">
                      <table>
            <tr>
                <td class="PageSHeading" align="center">
                    <table style="width: 845px; background-color: #edf0f4;">
                        <tr>
                            <td align="left">
                                <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
           
            </table>
        </div>
    </form>
</body>

</html>
