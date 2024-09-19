<%@ Page Language="VB" MasterPageFile="~/Masters/E1S1E2S2.master" AutoEventWireup="false" CodeFile="DesignWaste.aspx.vb" Inherits="Pages_DesignWaste" Title="Design Waste Wizard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain1ContentPlaceHolder" runat="Server">

    <script type="text/javascript" language="javascript">
function GetCupDiameter()
{

 document.getElementById("ctl00_Sustain1ContentPlaceHolder_txtCD2").value= document.getElementById("ctl00_Sustain1ContentPlaceHolder_txtCD1").value;
}
    </script>
    <script src="../JavaScript/Common.js" type="text/javascript"></script>
    
    <div id="ContentPagemargin">
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divUpdateprogress">
                            <center>
                                <table>
                                    <tr>
                                        <td>
                                            <img alt="" src="../Images/Loading3.gif" height="50px" />
                                        </td>
                                        <td>
                                            <b style="color: Red;">Updating</b>
                                        </td>
                                    </tr>
                                </table>
                            </center>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
                <table style="width: 790px; text-align: left;">
                     <tr align="left">
                              <td style="width:33%" class="PageHeading" onmouseover="Tip('Thermoforming Design Waste Wizard')" onmouseout="UnTip()" >
                                 Thermoforming Design Waste Wizard 
                                </td>
                         </tr>
                </table>
                <div id="PageSection1" style="text-align: left">
                    <br />
                    <table style="width:60%; text-align: center">
                        <tr align="left">
                            <td class="AlterNateColor3" colspan="3">
                                Calculation of design waste based on area
                            </td>
                        </tr>
                    </table>
                    <table style="width: 60%; text-align: center">
                        <tr align="left">
                            <td class="PageSHeading" colspan="3">
                                Product Arrangement on the Tool
                            </td>
                        </tr>
                        <tr class="TdHeading1" style="height: 20px">
                            <td style="width: 140px;" onmouseover="Tip('Enter the number of products that fit within the width of the tool')"
                                onmouseout="UnTip()">
                                Width
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Enter the number of products that fit within the index of the tool')"
                                onmouseout="UnTip()">
                                Index
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Total number of products produced in one cycle of the machine')"
                                onmouseout="UnTip()">
                                Total
                            </td>
                        </tr>
                        <tr class="TdHeading2" style="height: 20px">
                            <td>
                                <asp:Label ID="lblWidth" runat="server" Text="(number)"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblIndex" runat="server" Text="(number)"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCav" runat="server" Text="(number)"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <%-- <td>
                                <asp:TextBox ID="txtCWeight" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrav" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>--%>
                            <td>
                                <asp:TextBox ID="txtWidth" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                              
                            </td>
                            <td>
                                <asp:TextBox ID="txtIndex" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblCavVal" runat="server" Text="0.000"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 80%; text-align: center">
                        <tr align="left">
                            <td class="PageSHeading" colspan="4">
                                Calculation of required sheet width and index length
                            </td>
                        </tr>
                        <tr class="TdHeading1" style="height: 20px;">
                            <td style="width: 120px;">
                                Orientation
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Enter the diameter or width of the product')"
                                onmouseout="UnTip()">
                                Product Size
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Enter the distance between products')"
                                onmouseout="UnTip()">
                                Shelf Distance
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Enter the distance from the outermost product to the edge of the packaging raw material')"
                                onmouseout="UnTip()">
                                Edge Distance
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Total width or index length of the packaging raw material')"
                                onmouseout="UnTip()">
                                Total
                            </td>
                        </tr>
                        <tr class="TdHeading2" style="height: 20px">
                             <td>
                               
                            </td>
                            <td>
                                <asp:Label ID="lblCD" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblShelf" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblEdge" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblTot" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                             <td>
                                 <asp:Label ID="Label1" Text="Sheet Width" runat="server"></asp:Label>
                             </td>
                            <td>
                                <asp:TextBox ID="txtCD1" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtShelf1" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEdge1" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblTot1" runat="server" Text="0.000"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>                            
                                <asp:Label ID="Label3" Text="Index Length" runat="server"></asp:Label>
                            </td>
                            <td>
                                <%--<asp:Label ID="txtCD2" runat="server" Text="0.000"></asp:Label>--%>
                                <asp:TextBox ID="txtCD2" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtShelf2" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtEdge2" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblTot2" runat="server" Text="0.000"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 80%; text-align: center">
                        <tr align="left">
                            <td class="PageSHeading" colspan="4">
                                Calculation of design waste based on area
                            </td>
                        </tr>
                        <tr class="TdHeading1" style="height: 20px;">
                            <td style="width: 120px;" onmouseover="Tip('Total area of raw material for one index of the machine')"
                                onmouseout="UnTip()">
                                Raw Material Area
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Total area of raw material utilized for product')"
                                onmouseout="UnTip()">
                                Product Area
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Area of raw material not used for product measuring in area')"
                                onmouseout="UnTip()">
                                Design Waste 
                            </td>
                            <td style="width: 120px;" onmouseover="Tip('Design waste measured in percent based on area')"
                                onmouseout="UnTip()">
                                Design waste
                            </td>
                        </tr>
                        <tr class="TdHeading2" style="height: 20px">
                            <td>
                                <asp:Label ID="lblRMAreaU" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProdAreaU" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDWaste1U" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDWaste2U" runat="server" Text="(%)"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>
                                <asp:Label ID="lnlRMArea" runat="server" Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblProdArea" runat="server" Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDWaste1" runat="server" Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDWaste2" runat="server" Text="0.000"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <table style="width: 70%; text-align: center">
                        <tr align="left">
                            <td class="AlterNateColor3" colspan="3">
                                Validation of product weight and design waste based on weight (optional)
                            </td>
                        </tr>
                    </table>
                    <table style="width: 80%; text-align: center">
                        <tr align="left">
                            <td class="PageSHeading" colspan="4">
                                Calculation of machine output
                            </td>
                        </tr>
                        <tr class="TdHeading1" style="height: 20px;">
                            <td style="width: 140px;" onmouseover="Tip('Instantaneous run rate of the production machine')"
                                onmouseout="UnTip()">
                                Instantaneous Rate
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Instantaneous run rate of the production machine')"
                                onmouseout="UnTip()">
                                Instantaneous Rate
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Specific Gravity of the raw material')"
                                onmouseout="UnTip()">
                                Specific Gravity
                            </td>                            
                        </tr>
                        <tr class="TdHeading2" style="height: 20px">
                            <td>
                                (cycles per minute)
                            </td>
                            <td>
                                (feet per minute)
                            </td>
                            <td>
                                (unitless)
                            </td>                            
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>
                                   <asp:TextBox ID="txtCycle" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox> 
                            </td>
                            <td>
                                <asp:Label ID="lblFpm" runat="server" Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtGrav" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox> 
                             
                            </td>                           
                        </tr>
                    </table>
                    <table style="width: 80%; text-align: center">
                        <tr align="left">
                            <td class="PageSHeading" colspan="4">
                                Calculation of product weight
                            </td>
                        </tr>
                        <tr class="TdHeading1" style="height: 20px;">
                            <td style="width: 140px;" onmouseover="Tip('Raw material thickness')"
                                onmouseout="UnTip()">
                                Raw Material Thickness
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Individual Product volume based on product area and raw material thickness')"
                                onmouseout="UnTip()">
                                Product Volume
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Individual Product weight based on product volume and raw material specific gravity')"
                                onmouseout="UnTip()">
                                Calculated Product Weight
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('For reference only! Not used in calculations')"
                                onmouseout="UnTip()">
                                Measured Product Weight
                            </td>
                        </tr>
                        <tr class="TdHeading2" style="height: 20px">
                            <td>
                                <asp:Label ID="lblTh" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDV11" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDW11" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblCWeight" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr class="AlterNateColor1">
                            <td>
                                <asp:TextBox ID="txtThick" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblDV1" runat="server" Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDW1" runat="server" Text="0.00000"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtCWeight" runat="server" Text="0.000" CssClass="SmallTextBox"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                    
                    <table style="width: 80%; text-align: center">
                        <tr align="left">
                            <td class="PageSHeading" colspan="4">
                                Calculation of design waste based on weight
                            </td>
                        </tr>
                        <tr class="TdHeading1" style="height: 20px;">
                            <td style="width: 140px;" onmouseover="Tip('Instantaneous run rate of the production machine')"
                                onmouseout="UnTip()">
                                Total Machine Output
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Total Product output based on weight')"
                                onmouseout="UnTip()">
                                Product Output
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Design waste based on weight')"
                                onmouseout="UnTip()">
                                Design Waste
                            </td>
                            <td style="width: 140px;" onmouseover="Tip('Design waste measured in percent based on weight')"
                                onmouseout="UnTip()">
                                Design Waste
                            </td>
                        </tr>
                        <tr class="TdHeading2" style="height: 20px">
                            <td>
                                <asp:Label ID="lblMOutPutU" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPOutU" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDesWU1" runat="server"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDesWU2" runat="server" Text="(%)"></asp:Label>
                            </td>
                        </tr>
                          <tr class="AlterNateColor1" style="height: 20px">
                            <td>
                                <asp:Label ID="lblMOutPut" runat="server" Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblPOut" runat="server"  Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDesW1" runat="server"  Text="0.000"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDesW2" runat="server"  Text="0.000"></asp:Label>
                            </td>
                        </tr>
                       
                    </table>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
