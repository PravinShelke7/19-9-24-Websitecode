<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CLciComp.aspx.vb" Inherits="Pages_Sustain1_Assumptions_CLciComp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>S1-LCI Comparison</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .S1CompModule
        {
            margin-top: 2px;
            margin-left: 1px;
            background-image: url( '../../../Images/Sustain1Fulcrum.gif' );
            height: 54px;
            width: 845px;
            background-repeat: no-repeat;
            text-align: center;
            vertical-align: middle;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
       
      <%--<div id="AlliedLogo">
            
            <asp:Image ImageAlign="AbsMiddle" ID="imgLogo" ImageUrl="~/Images/AlliedLogo.gif" runat="server" />
        </div>  --%>      
      
      <div>
        <table class="S1Module" id="S1Table" runat="server"  cellpadding="0" cellspacing="0"  style="border-collapse:collapse">
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
                         <asp:Label ID="lblHeaderText" runat="Server" ></asp:Label>
                     </div>
    
                   
                                    
                </td>
            </tr>
             <tr style="height:20px">
                <td>
                <div id="ContentPagemargin" runat="server">
                   <div id="PageSection1" style="text-align:left;" >
                        <asp:button ID="btnPrivious" runat="server" Text="Previous" />
                        <asp:button ID="btnNext" runat="server" Text="Next" />
                        <asp:button ID="btnShowall" runat="server" />
                    <br />
                    <asp:Table ID="tblComparision" runat="server" CellPadding="1" CellSpacing="2"></asp:Table>
                    <br />
                     <asp:HiddenField ID="hdnLatestDate" runat="Server" />
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

