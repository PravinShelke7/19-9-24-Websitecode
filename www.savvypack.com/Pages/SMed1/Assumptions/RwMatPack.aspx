<%@ Page Title="SMed1-Raw Material Packaging Assumptions" Language="VB" MasterPageFile="~/Masters/SMed1.master" AutoEventWireup="false" CodeFile="RwMatPack.aspx.vb" Inherits="Pages_MedSustain1_Assumptions_RwMatPack" %>

<asp:Content ID="Content1" ContentPlaceHolderID="SMed1ContentPlaceHolder" Runat="Server">
  <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
   <script type="text/JavaScript" src="../../../JavaScripts/SMed1Comman.js"></script>
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
                params += ', resizable=no';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                newwin = window.open(Page, 'Chat', params);

            }
            function ScrollOption(Id) {
                var rd = document.getElementById(Id)
                var div = document.getElementById("divVariable")
               
                if (rd.value == "1") {
                    div.className = "DivScrolles";
                }
                else {
                    div.className = "DivWScrolles";
                }
                
            }
     </script>
<div id="ContentPagemargin" runat="server">
      
      
       <table cellpadding="0" cellspacing="0" style="text-align:center;padding-left:55px;">
            <tr>
                <td> 
                   <asp:Table ID="tblTab" runat="server" CellPadding="0" CellSpacing="0"></asp:Table>
                </td>
            </tr>
       </table> 
       
       <div id="PageSection1" style="text-align:left;height:520px;" >
            <div style="margin-top:10px;margin-bottom:10px;margin-top:0px">
                <asp:Label ID="lblMaterialName" runat="server" Style="font-size:14px;"></asp:Label>
            </div>
            <table cellspacing="0" cellpadding="0" style="width:100%">
                <tr>
                    <td valign="top">
                        <asp:Table ID="tblFixComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                         <input type="radio" checked="checked" name="rdScroll" id="rdScrollOn" onclick="ScrollOption('rdScrollOn');" value="1" />Scroll <input id="rdScrollOff" name="rdScroll" type="radio" onclick="ScrollOption('rdScrollOff');" value="0" />No Scroll

                    </td>
                    <td valign="top">
                        <div id="divVariable" class="DivScrolles" style="height:430px;">
                            <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                        </div>
                    </td>
                </tr>
                <tr>
                   <td valign="top" colspan="2" align="left" style="height: 15px">  
                      <br />
                       <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                   </td> 
                </tr> 
            </table>
            <br />
           
       </div>
 </div>
</asp:Content>

