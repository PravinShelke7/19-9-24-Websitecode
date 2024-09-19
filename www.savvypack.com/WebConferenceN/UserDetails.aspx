<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false" CodeFile="UserDetails.aspx.vb" Inherits="WebConferenceN_UserDetails" title="Web Conferences - User Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<div class="MnContentPage">
    <div style="font-family:Optima;font-size:12px">
    Please check your registration information.<br />
        <br />
    </div>
<table style="width:580px;text-align:left" cellpadding="2" cellspacing="2">
    <tr>
        <td class="WebInnerTd">
           <b>Topic:</b> <asp:Label ID="lblTopic" runat="server" CssClass="NormalLabel"></asp:Label>         
        </td>
        <td class="WebInnerTd">
            <asp:HyperLink ID="hypUser" runat="server" Text="Edit Profile" CssClass="Link" Target="_blank" ></asp:HyperLink>
        </td>
     </tr>
     <tr>
        <td class="WebInnerTd" colspan="2">
          <b>Cost:</b> <asp:Label ID="lblAmt" runat="server" CssClass="NormalLabel"></asp:Label>         
        </td>
     </tr>
    </table>
        
    <br />
    <div style="font-family:Optima;font-size:12px">
        <b>Note:</b> Please resfresh the page to see updated profile.
    </div> 

    <table style="width:580px;text-align:left" cellpadding="2" cellspacing="2">
        <tr>
            <td style="width:80%">
                <table cellpadding="0" cellspacing="2" style="width:100%">
                    <tr class="AlterNateColor4">
                        <td align="center" class="TdHeading" colspan="2"> 
                            <b>User Information</b>
                        </td>
                    </tr>
                    <tr class="AlterNateColor1">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Name:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblName" runat="server" CssClass="NormalLabel"></asp:Label>
                            
                        </td>
                    </tr>
                    
                    <tr class="AlterNateColor2" >
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Email:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblEmail" runat="server" CssClass="NormalLabel"></asp:Label>
                            
                        </td>
                    </tr>
                    
                    <tr class="AlterNateColor1">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Phone:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblphne" runat="server" CssClass="NormalLabel"></asp:Label>
                            
                        </td>
                    </tr>
                    
                     
                    <tr class="AlterNateColor2" class="WebInnerTd">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Fax:
                        </td>
                        <td align="left">
                           <asp:Label ID="lblFax" runat="server" CssClass="NormalLabel"></asp:Label>
                            
                        </td>
                    </tr>
                    
                      <tr class="AlterNateColor1" class="WebInnerTd">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Company Name:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblCompName" runat="server" CssClass="NormalLabel"></asp:Label>
                            
                        </td>
                    </tr>
                    
                    <tr class="AlterNateColor2" class="WebInnerTd">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Position:
                        </td>
                        <td align="left">
                           <asp:Label ID="lblPosition" runat="server" CssClass="NormalLabel"></asp:Label>
                            
                        </td>
                    </tr>
                    
                    
                       <tr class="AlterNateColor1" class="WebInnerTd">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Street Address:
                        </td>
                        <td align="left">
                           <asp:Label ID="lblAdd" runat="server" CssClass="NormalLabel"></asp:Label>                            
                        </td>
                    </tr>
                    
                       
                    <tr class="AlterNateColor2" class="WebInnerTd">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            City:
                        </td>
                        <td align="left">
                           <asp:Label ID="lblCity" runat="server" CssClass="NormalLabel"></asp:Label>                            
                        </td>
                    </tr>
                    
                     <tr class="AlterNateColor1">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            State:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblState" runat="server" CssClass="NormalLabel"></asp:Label>                            
                        </td>
                    </tr>
                    
                     <tr class="AlterNateColor2">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Zip Code:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblZipCode" runat="server" CssClass="NormalLabel"></asp:Label>                            
                        </td>
                    </tr>
                    
                    <tr class="AlterNateColor1">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                            Country:
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Label ID="lblCntry" runat="server" CssClass="NormalLabel"></asp:Label>                            
                        </td>
                    </tr>
                    
              
                     <tr class="AlterNateColor1">
                        <td style="font-weight:bold;width:30%" class="WebInnerTd">
                           
                        </td>
                        <td align="left" class="WebInnerTd">
                           <asp:Button ID="btnOrder" runat="server" CssClass="ButtonWMarigin" Text="Continue Registration Process" />                    
                        </td>
                    </tr>
                    
                </table>
            </td>            
            
        </tr>
    </table>
    

</div>
</asp:Content>

