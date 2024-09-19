<%@ Page Language="VB" MasterPageFile="~/Masters/Schem1.master" AutoEventWireup="false" CodeFile="PrintCaseSummary.aspx.vb" Inherits="Pages_Schem1_Results_PrintCaseSummary" title="Schem1-Print Case Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Schem1ContentPlaceHolder" Runat="Server">
 <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/javascript">
         function visible(divname) {

             document.getElementById("divEnergy").className = "PrintCaseSummaryPrint"
             document.getElementById("divMaterilInput").className = "PrintCaseSummaryPrint"
             document.getElementById("divMatRes").className = "PrintCaseSummaryPrint"
             document.getElementById("divEquipment").className = "PrintCaseSummaryPrint"
             document.getElementById("divOperations").className = "PrintCaseSummaryPrint"
             document.getElementById("divPersonnel").className = "PrintCaseSummaryPrint"
             document.getElementById("divGHG").className = "PrintCaseSummaryPrint"
               document.getElementById("divWater").className = "PrintCaseSummaryPrint"


             document.getElementById(divname).className = "PrintCaseSummary"
        }
        function visibleAll() {

            document.getElementById("divEnergy").className = "PrintCaseSummary"
            document.getElementById("divMaterilInput").className = "PrintCaseSummary"
            document.getElementById("divMatRes").className = "PrintCaseSummary"
            document.getElementById("divEquipment").className = "PrintCaseSummary"
            document.getElementById("divOperations").className = "PrintCaseSummary"
            document.getElementById("divPersonnel").className = "PrintCaseSummary"
            document.getElementById("divGHG").className = "PrintCaseSummary"
             document.getElementById("divWater").className = "PrintCaseSummary"


          
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
       <div id="PageSection1" style="text-align:left" >                       
            <br />
            <div id="divEnergy" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Energy Statement</div>
                <asp:Table ID="tblEnergy" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                 <br />
            </div>
           
            <div id="divGHG" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Greenhouse Gas Statement </div>
                <asp:Table ID="tblGHG" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
              <div id="divWater" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Water Statement </div>
                <asp:Table ID="tblWater" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
           
            <div id="divMaterilInput" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Material Assumptions</div>
                <asp:Table ID="tblMatInput" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                  <%-- <table cellspacing="0" cellpadding="0" style="border-collapse:collapse;width:80%;">           
                        <tr>
                            <td style="padding-left:5px" valign="top">
                               <asp:Table ID="tblFixComparision" runat="server" CellPadding="0" CellSpacing="1" style="border-collapse:collapse;"></asp:Table>
                               <input type="radio" checked="checked" name="rdScroll" id="rdScrollOn" onclick="ScrollOption('rdScrollOn');" value="1" />Scroll <input id="rdScrollOff" name="rdScroll" type="radio" onclick="ScrollOption('rdScrollOff');" value="0" />No Scroll

                            </td>
                            <td valign="top">
                                <div id="divVariable" class="DivScrollPrint">
                                 <asp:Table ID="tblMatInput" runat="server" CellPadding="0" CellSpacing="1"></asp:Table>
                                </div>
                            </td> 
                        </tr>
               </table> --%>
                <br />
            </div>
            
            <div id="divMatRes" class="PrintCaseSummaryPrint" >
                <div class="PageHeading">Material Results</div>
                <asp:Table ID="tblMatResults" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                 <br />
            </div>
             
            <div id="divEquipment" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Equipment Assumptions</div>
                <asp:Table ID="tblEquipment" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
           
            <div id="divOperations" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Operating Assumptions</div>
                <asp:Table ID="tblOperations" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
            
            <div id="divPersonnel" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Personnel Assumptions </div>
                <asp:Table ID="tblPersonal" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
            
            
                
                
         </div>   
     </div>
</asp:Content>

