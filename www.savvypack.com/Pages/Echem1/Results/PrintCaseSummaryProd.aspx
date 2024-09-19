<%@ Page Language="VB" MasterPageFile="~/Masters/Echem1.master" AutoEventWireup="false" CodeFile="PrintCaseSummaryProd.aspx.vb" 
Inherits="Pages_Echem1_Results_PrintCaseSummaryProd" title="Echem1-Print Case Summary Product" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Echem1ContentPlaceHolder" Runat="Server">
 <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/javascript">
         function visible(divname) {

             document.getElementById("divProfitAndLoss").className = "PrintCaseSummaryPrint"
             document.getElementById("divMaterilInput").className = "PrintCaseSummaryPrint"
             document.getElementById("divMatRes").className = "PrintCaseSummaryPrint"
             document.getElementById("divEquipment").className = "PrintCaseSummaryPrint"
             document.getElementById("divOperations").className = "PrintCaseSummaryPrint"
             document.getElementById("divPersonnel").className = "PrintCaseSummaryPrint"
             document.getElementById("divFixCost").className = "PrintCaseSummaryPrint"


             document.getElementById(divname).className = "PrintCaseSummary"
        }
        function visibleAll() {

            document.getElementById("divSalesVol").className = "PrintCaseSummary"
            document.getElementById("divProfitAndLoss").className = "PrintCaseSummary"
            document.getElementById("divMaterilInput").className = "PrintCaseSummary"
            document.getElementById("divMatRes").className = "PrintCaseSummary"
            document.getElementById("divEquipment").className = "PrintCaseSummary"
            document.getElementById("divOperations").className = "PrintCaseSummary"
            document.getElementById("divPersonnel").className = "PrintCaseSummary"
            document.getElementById("divFixCost").className = "PrintCaseSummary"


          
        }
            
    </script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
                     
            <br />

            <div id="divSalesVol" class="PrintCaseSummaryPrint">
               <div class="PageHeading"></div>
                <asp:Table ID="tblSalesVol" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                 <br />
            </div>

            <div id="divProfitAndLoss" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Profit and Loss Results Product</div>
                <asp:Table ID="tblPandL" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                 <br />
            </div>
           
            <div id="divMaterilInput" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Material Assumptions</div>
                <asp:Table ID="tblMatInput" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
           
            <div id="divMatRes" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Material Results</div>
                <asp:Table ID="tblMatResults" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
            
            <div id="divEquipment" class="PrintCaseSummaryPrint" >
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
                <div class="PageHeading">Personnel Assumptions</div>
                <asp:Table ID="tblPersonnel" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
            
            <div id="divFixCost" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Fixed Cost Assumptions</div>
                <asp:Table ID="tblFixCost" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                <br />
            </div>
            
            
                
                
         </div>   
     </div>
</asp:Content>

