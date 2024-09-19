<%@ Page Title="E1-Profit and Loss Statement with Depreciation" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="A1.aspx.vb" Inherits="Pages_Econ1_Results_A1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
     <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js" ></script>
       
        <div class="divMargin">  
         <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
      


    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
             <table style="width:786px;">
             
                <tr>
                 
                    <td style="width: 210px">
                        <asp:Label id="lblSalesVol" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                     <td style="width: 245px">
                        <asp:Label id="lblSalesVolUnit" runat="server" CssClass="NormalLable"></asp:Label>
                    </td>
                      <td>
                 
                   <asp:CheckBox id="checkbox1" runat="server" CssClass="NormalLable"  AutoPostBack ="true"/>Deperatiation
             </td>
            
                </tr>
             </table>
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />  
             <asp:TextBox ID="txthiddien" Style="visibility:hidden;" runat="server" Text="0"></asp:TextBox>       
         </div>   
     </div>
      </ContentTemplate>
        </asp:UpdatePanel>
       <asp:HiddenField ID="hdnVolume" runat="server" />
         <asp:HiddenField ID="hdnUnit" runat="server" /> .
       </div>   

</asp:Content>


