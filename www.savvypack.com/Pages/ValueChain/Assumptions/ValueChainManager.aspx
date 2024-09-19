<%@ Page Language="VB" MasterPageFile="~/Masters/VChain.master" AutoEventWireup="false" CodeFile="ValueChainManager.aspx.vb" Inherits="Pages_ValueChain_Assumptions_ValueChainManager" title="Value Chain Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VChainContentPlaceHolder" Runat="Server">
  <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
   <script type="text/JavaScript">
   function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, '_parent', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

        }
        </script>
    <table width="840px"  style="text-align:left;">
            
            <tr align="left">
                <td style="width:33%" class="PageHeading" onmouseover="Tip('Value Chain Manager')" onmouseout="UnTip()" >
                  Value Chain Manager
                </td>
                
                <td style="width:18%" class="PageSHeading">
                    <table>
                        <tr>
                            <td>
                                Value Chain Id:
                            </td>
                            
                            <td>
                                  <asp:Label ID="lblAID"  CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
                
                
                <td style="width:50%" class="PageSHeading">
                     <table>
                        <tr>
                            <td>
                               Description:
                            </td>
                            
                            <td>
                                 <asp:Label ID="lblAdes" CssClass="LableFonts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
   <br />

   
   <div id="ContentPagemargin">
   <div id="PageSection1"  style="text-align:left;" >
         <%--<table width="98%" style="text-align:left;">
            <tr style="height:20px;">
                    <td style="width: 56px">
                    
                    </td>
                    <td style="width: 77px">
                        
                    </td>
                    <td style="width: 456px">
                      <b><a href="Preferences.aspx" class="Link" target="_blank">Preferences</a></b>                       
                    </td>
                    <td>
                        
                    </td>
                </tr>
                <tr class="PageSSHeading" style="height:20px;">
                    <td class="TdHeading">
                    
                    </td>
                    <td class="TdHeading" style="width: 77px">
                        Case Ids
                    </td>
                    <td class="TdHeading" style="width: 456px">
                       Case Descriptions
                    </td>
                    <td class="TdHeading">
                        Review Final Results	
                    </td>
                </tr>
                <tr class="AlterNateColor1"> 
                    <td class="StaticTdLeft">
                        &nbsp;</td>
                    <td class="StaticTdLeft" style="width: 77px">
                        &nbsp;</td>
                    <td class="StaticTdLeft" style="width: 456px">
                        
                    </td>
                    <td class="StaticTdLeft"> 
                        
                    </td>
                </tr>
            </table>--%>
            <asp:Table ID="tblCaseMngr"  runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
         <br />
     </div>
   
   </div>
</asp:Content>

