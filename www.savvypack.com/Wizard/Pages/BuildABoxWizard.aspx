<%@ Page Title="E1S1E2S2-Build A Box" Language="VB" MasterPageFile="../Masters/E1S1E2S2.master" AutoEventWireup="false" CodeFile="BuildABoxWizard.aspx.vb" Inherits="Pages_BuildABoxWizard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain1ContentPlaceHolder" Runat="Server">
    <div id="ContentPagemargin">
         <asp:UpdatePanel ID="upd1" runat="server" >
            <ContentTemplate>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <div class="divUpdateprogress">
                            <center>
                            <table >
                                <tr>
                                    <td><img alt="" src="../Images/Loading3.gif" height="50px" /></td>
                                    <td><b style="color:Red;">Updating</b></td>
                                </tr>
                            </table>
                            </center>
                            
                            </div>
                       </ProgressTemplate>
                    </asp:UpdateProgress>
                    
                      <table style="width:790px;text-align:left;">
                        <tr align="left">
                              <td style="width:33%" class="PageHeading" onmouseover="Tip('Build A Box Wizard')" onmouseout="UnTip()" >
                                 Build A Box Wizard
                                </td>
                         </tr>
                       </table>
                    
                         <div id="PageSection1">
                           <br />
                            <table style="width:98%">
                                <tr class="AlterNateColor2" style="height:20px">
                                     <td class="PageSHeading" colspan="5" align="left" style="padding-left:5px">    
                                        Package Details
                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" style="height:20px">
                                    <td class="PageSHeading">                                    
                                       Select Primary Package Format
                                    </td>
							<td class="PageSHeading" colspan="3">Enter Primary Package Dimension                                    </td>
							<%--  <td class="PageSHeading">
                                        Package Area	
                                    </td>--%></tr>
                                 <tr class="AlterNateColor1" style="height:20px">
                                    <td>                                    
                                       
                                    </td>
                                    <td class="PageSSHeading">
                                         <asp:Label ID="lblPM1" runat="server" Text="Width"></asp:Label>
                                    </td>
                                     <td class="PageSSHeading">
                                         <asp:Label ID="lblPM2" runat="server" Text="Length"></asp:Label>
                                    </td>
							<td class="PageSSHeading">
                                          <asp:Label ID="lblPM3" runat="server" Text="Height"></asp:Label>
                                    </td>
							<%--  <td class="PageSSHeading">
                                         
                                    </td>--%></tr>
                                <tr class="AlterNateColor1" style="height:20px">
                                    <td>                                    
                                       
                                    </td>
                                    <td>
                                         <asp:Label ID="lblUnitM1" runat="server"></asp:Label>
                                    </td>
                                     <td>
                                         <asp:Label ID="lblUnitM2" runat="server"></asp:Label>
                                    </td>
							<td>
                                          <asp:Label ID="lblUnitM3" runat="server"></asp:Label>
                                    </td>
							<%--<td>
                                         <asp:Label ID="lblUnitArea" runat="server"></asp:Label>
                                    </td>--%></tr>
                                  <tr class="AlterNateColor1">
                                    <td>                                    
                                       <asp:DropDownList runat="server" CssClass="DropDown" ID="ddlPackFormat" Width="150px" AutoPostBack="true">
                                       </asp:DropDownList>	
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPM1" runat="server" Text="0.00" CssClass="SmallTextBox"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="txtPM2" runat="server" Text="0.00" CssClass="SmallTextBox"></asp:TextBox>
                                    </td>
							<td>
                                        <asp:TextBox ID="txtPM3" runat="server" Text="0.00" CssClass="SmallTextBox"></asp:TextBox>
                                    </td>
							<%-- <td>
                                        <asp:Label ID="lblArea" runat="server" Text="0.00"></asp:Label>
                                    </td>--%></tr>
                            </table>
                            
                            <br />
                            
                            <table style="width:98%">
                                <tr class="AlterNateColor2" style="height:20px">
                                     <td class="PageSHeading" colspan="5" align="left" style="padding-left:5px">    
                                        Layer Details	
                                    </td>
                                </tr>
                                 <tr class="AlterNateColor1" style="height:20px">
                                     <td class="PageSSHeading" colspan="2">    
                                        Layer configuration
                                    </td>
                                      <td class="PageSSHeading">Number Of Primary Packages per Layer                                    </td>
                                      <td class="PageSSHeading">    
                                       Number of layers per carton	
                                    </td>
                                    <td class="PageSSHeading">Number of Primary Packages per carton	

                                    </td>
                                </tr>
                                <tr class="AlterNateColor1" style="height:20px">
                                     <td colspan="2">    
                                        (Number by number)
                                    </td>
                                      <td>    
                                        
                                    </td>
                                      <td>    
                                       
                                    </td>
                                    <td>                                         	

                                    </td>
                                </tr>
                                  <tr class="AlterNateColor1">
                                    <td>
                                        <asp:TextBox ID="txtlayerN1" runat="server" CssClass="SmallTextBox" Text="0.00"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:TextBox ID="txtlayerN2" runat="server" CssClass="SmallTextBox" Text="0.00"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblNPPL" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNLPC" runat="server" CssClass="SmallTextBox" Text="0.00"></asp:TextBox>
                                    </td>
                                     <td>
                                        <asp:Label ID="lblNPPC" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                 </tr>
                                </table>
                            <br />

                            
                            <table style="width:98%">
                                <tr class="AlterNateColor2" style="height:20px">
                                     <td class="PageSHeading" colspan="5" align="left" style="padding-left:5px">    
                                        Corrugated Carton Details	
                                    </td>
                                </tr>
                                 <tr class="AlterNateColor1" style="height:20px">
                                    <td class="PageSHeading" colspan="3">
                                         Corrugated Carton Dimensions	
                                    </td>
                                     <td class="PageSHeading">
                                         Corrugated Carton Area	
                                    </td>
                                </tr>
                                 <tr class="AlterNateColor1" style="height:20px">
                                   
                                    <td class="PageSSHeading">
                                        Width
                                    </td>
                                     <td class="PageSSHeading">
                                         Length
                                    </td>
                                     <td class="PageSSHeading">
                                         Height
                                    </td>
                                    <td class="PageSSHeading">
                                         
                                    </td>
                                    
                                </tr>
                                 <tr class="AlterNateColor1" style="height:20px">
                                     <td>
                                         <asp:Label ID="lblUnitCCM1" runat="server"></asp:Label>
                                    </td>
                                     <td>
                                          <asp:Label ID="lblUnitCCM2" runat="server"></asp:Label>
                                    </td>
                                     <td>
                                         <asp:Label ID="lblUnitCCM3" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                          <asp:Label ID="lblUnitCCArea" runat="server"></asp:Label>
                                    </td>
                                
                                </tr>
                                 <tr class="AlterNateColor1" style="height:20px">
                                     <td>
                                         <asp:Label ID="lblCCM1" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                     <td>
                                          <asp:Label ID="lblCCM2" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                     <td>
                                         <asp:Label ID="lblCCM3" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    <td>
                                          <asp:Label ID="lblCCArea" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                
                                </tr>
                             </table>
                             <br /> 
                              <table style="width:98%">
                                <tr class="AlterNateColor2" style="height:20px">
                                     <td class="PageSHeading" colspan="5" align="left" style="padding-left:5px">    
                                        Corrugated Carton Flute Details	
                                    </td>
                                </tr>
                                  <tr class="AlterNateColor1" style="height:20px">
                                     <td class="PageSSHeading">
                                        Corrugated Carton Flute Type
                                     </td>
                                       <td class="PageSSHeading">
                                        Corrugated Carton Weight
                                     </td>
                                      <td class="PageSSHeading" runat="server" visible="false" id="tdCost1">
                                        Corrugated Carton Cost
                                     </td>
                                      <td class="PageSSHeading" runat="server" visible="false" id="tdLCI1">
                                          LCI Details
                                     </td>
                                  </tr>
                                   <tr class="AlterNateColor1" style="height:20px">
                                    <td>
                                        
                                    </td>
                                     <td>
                                          <asp:Label ID="lblUnitCCWt" runat="server"></asp:Label>
                                    </td>
                                     <td  runat="server" visible="false" id="tdCost2">
                                         <asp:Label ID="lblUnitCCost" runat="server"></asp:Label>
                                    </td>
                                    <td  runat="server" visible="false" id="tdLCI2">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <b>GHG</b> <asp:Label ID="lblGHGU" runat="server"></asp:Label>                                               
                                                </td>
                                                <td>
                                                   <b>Energy</b> <asp:Label ID="lblErgyU" runat="server"></asp:Label>  
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                  
                                  </tr>
                                  <tr class="AlterNateColor1" style="height:20px">
                                    <td>
                                         <asp:DropDownList runat="server" CssClass="DropDown" ID="ddlCFluteType" 
                                             Width="100px">
                                       </asp:DropDownList>	
                                    </td>
                                     <td>
                                          <asp:Label ID="lblCCWt" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                     <td runat="server" visible="false" id="tdCost3">
                                         <asp:Label ID="lblCCCost" runat="server" Text="0.00"></asp:Label>
                                    </td>
                                    
                                     <td  runat="server" visible="false" id="tdLCI3">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="lblGHG" runat="server" Text="0.00"></asp:Label>                                              
                                                </td>
                                                <td>
                                                   <asp:Label ID="lblErgy" runat="server" Text="0.00"></asp:Label>    
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                  
                                  </tr>
                                  
                                  
                               </table>
                               
                               <br /> 
                         </div>
                    
                    </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

