<%@ Page Language="VB" MasterPageFile="~/Masters/MoldEcon2.master" AutoEventWireup="false"
    CodeFile="CaseManager.aspx.vb" Inherits="Pages_MoldEcon2_Assumptions_CaseManager"
    Title="E2 Mold-Case Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MoldEcon2ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <table width="98%" style="text-align: left;">
                <tr style="height: 20px;">
                    <td style="width: 56px">
                    </td>
                    <td style="width: 223px">
                    </td>
                    <td style="width: 169px">
                        <b><a href="Preferences.aspx" class="Link" target="_blank">Preferences</a></b>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr class="PageSSHeading" style="height: 20px;">
                    <td class="TdHeading" style="width: 56px">
                    </td>
                    <td class="TdHeading" style="width: 223px">
                        Specify Assumptions
                    </td>
                    <td class="TdHeading" style="width: 169px">
                        Review Intermediate Result
                    </td>
                    <td class="TdHeading">
                        Review Final Results
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step1
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="Extrusion.aspx" class="Link" target="_blank">Material and Structure</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        <a href="../IntResults/ExtrusrionOut.aspx" class="Link" target="_blank">Material and
                            Structure</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/Resultspl.aspx" class="Link" target="_blank">Profit And Loss</a>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step2
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="ProductFormatIn.aspx" class="Link" target="_blank">Product Format</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/ResultsPLwithDEP.aspx" class="Link" target="_blank">Profit And Loss
                            With Depreciation</a>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step3
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="TruckPalletIN.aspx" class="Link" target="_blank">Pallet and Truck Configuration</a>&nbsp;
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/Resultcost.aspx" class="Link" target="_blank">Cost</a>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step4
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PalletIn.aspx" class="Link" target="_blank">Pallet Packaging</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/ResultcostWithDep.aspx" class="Link" target="_blank">Cost With Depreciation</a>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step5
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PlantConfig.aspx" class="Link" target="_blank">Department Configuration</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/PrintCaseSummary.aspx" class="Link" target="_blank">Print Case Summary</a>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step6
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="Efficiency.aspx" class="Link" target="_blank">Material Efficiency</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                        &nbsp;
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step7
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="EquipmentIN.aspx" class="Link" target="_blank">Process Equipment</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/Resultspl2.aspx" class="Link" target="_blank">Customer Profit And
                            Loss</a>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step8
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="Equipment2IN.aspx" class="Link" target="_blank">Support Equipment</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        &nbsp;
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/ResultsPLwithDEP2.aspx" class="Link" target="_blank">Customer Profit
                            And Loss With Depreciation </a>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step9
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="OperationsIN.aspx" class="Link" target="_blank">Operating Parameters</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        <a href="../IntResults/OperationsIN2.aspx" class="Link" target="_blank">Operating Results</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a href="../Results/Resultcost2.aspx" class="Link" target="_blank">Customer Cost</a>
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step10
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PersonnelIn.aspx" class="Link" target="_blank">Personnel</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                        <a href="../IntResults/PersonnelOut.aspx" class="Link" target="_blank">Personnel Results</a>
                    </td>
                    <td class="StaticTdLeft">
                        <a class="Link" href="../Results/ResultcostWithDep2.aspx" target="_blank">Customer Cost
                            With Depreciation</a>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step11
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="PlantConfig2.aspx" class="Link" target="_blank">Plant Space Requirements</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                        &nbsp;
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step12
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="EnergyIN.aspx" class="Link" target="_blank">Energy Assumptions</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                        &nbsp;
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step13
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="CustomerIN.aspx" class="Link" target="_blank">Customer Specifications</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step14
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="FixedCost.aspx" class="Link" target="_blank">Fixed Cost Assumptions</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td class="StaticTdLeft" style="width: 56px">
                        Step15
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        <a href="DepreciationAssumption.aspx" class="Link" target="_blank">Depreciation Assumptions</a>
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                    </td>
                </tr>
                <tr class="AlterNateColor2">
                    <td class="StaticTdLeft" style="width: 56px">
                        &nbsp;
                    </td>
                    <td class="StaticTdLeft" style="width: 223px">
                        &nbsp;
                    </td>
                    <td class="StaticTdLeft" style="width: 169px">
                    </td>
                    <td class="StaticTdLeft">
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
