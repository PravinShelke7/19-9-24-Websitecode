<%@ Page Title="E2-Print Case Summary" Language="VB" MasterPageFile="~/Masters/Econ2.master" AutoEventWireup="false" CodeFile="PrintCaseSummary.aspx.vb" Inherits="Pages_Econ2_Results_PrintCaseSummary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ2ContentPlaceHolder" Runat="Server">
    
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
           <%--<div class="divshowlables">
            <table>
                <tr>
                
                     
                     <td><a href="#" class="Link" onclick="visible('divProfitAndLoss');">Profit and Loss Results</a></td>
                     <td><a href="#" class="Link" onclick="visible('divMaterilInput');">Material Assumptions</a></td>
                     <td><a href="#" class="Link" onclick="visible('divMatRes');">Material Results</a></td>
                </tr>
                <tr>
                     <td><a href="#" class="Link" onclick="visible('divEquipment');">Equipment Assumptions</a></td>
                     <td><a href="#" class="Link" onclick="visible('divOperations');">Operating Assumptions</a></td>
                     <td><a href="#" class="Link" onclick="visible('divPersonnel');">Personnel Assumptions</a></td>
                     <td><a href="#" class="Link" onclick="visible('divFixCost');">Fixed Cost Assumptions</a></td>
                </tr>
            </table>
            </div>--%> 
            
            <br />
            <div id="divSalesVol" class="PrintCaseSummaryPrint">
               <div class="PageHeading"></div>
                <asp:Table ID="tblSalesVol" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
                 <br />
            </div>
            
            <div id="divProfitAndLoss" class="PrintCaseSummaryPrint">
                <div class="PageHeading">Profit and Loss Results</div>
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
