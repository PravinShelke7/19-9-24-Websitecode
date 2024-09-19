<%@ Page Title="E1-Profit and Loss Statement" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="Resultspl2.aspx.vb" Inherits="Pages_Econ1_Results_Resultspl2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
     <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js" ></script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
              <table style="width:700px;">
                <tr>
                   <td style="width:200px;">
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td style="width:200px;">
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                      <td>
                        <asp:Label id="lblNewSalesValue" runat="server" style="font-family:Optima;font-size:12px;height:12px;width:100px;margin-right:0px;margin-top:2px;margin-bottom:2px;	margin-left:5px;text-align:right;font-weight:bold"></asp:Label>
                        <asp:TextBox ID="txtNewSaleValue" runat="Server"  CssClass="MediumTextBox" style="width:74px;" ></asp:TextBox>
                        <asp:DropDownList ID="ddlCustUnit" runat="server" CssClass="DropDownConT"  ></asp:DropDownList>
                    </td>                   
                </tr>
             </table>
             <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />   
             <asp:TextBox ID="txthiddien" Style="visibility:hidden;" runat="server" Text="0"></asp:TextBox>
         </div> 
         <asp:HiddenField ID="hdnVolume" runat="server" />
         <asp:HiddenField ID="hdnUnit" runat="server" />  
     </div>
</asp:Content>


