<%@ Page Language="VB" MasterPageFile="~/Masters/Med2.master" AutoEventWireup="false" CodeFile="PersonnelIN.aspx.vb" Inherits="Pages_MedEcon2_Assumptions_PersonnelIN" title="Med2-Personnel Assumptions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Med2ContentPlaceHolder" Runat="Server">
<script type="text/JavaScript" src="../../../JavaScripts/Med2Comman.js"></script>   
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    
    <script type="text/JavaScript">
         function ShowPopWindow(Page) {
                //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
                var width = 550;
                var height = 400;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=yes';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                newwin = window.open(Page, 'Chat', params);

            }
     </script><div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
              <table cellspacing="2" style="width:230px;">
                 <tr>                          
                             <td>
                               <asp:Label id="lblPrefRatio" Text="Preferred Ratio:" runat="server" style="font-family:Optima;font-size:12px;height:12px;width:100px;margin-right:0px;margin-top:2px;margin-bottom:2px;	margin-left:5px;font-weight:bold"></asp:Label>
                            </td>
                            <td>
                              <asp:TextBox ID="txtPrefRatio" runat="Server" CssClass="MediumTextBox" style="margin-right:50px;" Text="0" MaxLength="6"></asp:TextBox>
                            </td>
                        </tr>
                  
                </table>
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />
         </div>
         
         
   
   </div>
</asp:Content>

