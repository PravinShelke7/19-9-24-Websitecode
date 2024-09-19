<%@ Page Title="SPEC-Upload Packaging Specification" Language="VB" MasterPageFile="~/Masters/Spec.master" AutoEventWireup="false" CodeFile="MassSpecUpload.aspx.vb" Inherits="Pages_Spec_Uploads_MassSpecUpload" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
       <table width="840px"  style="text-align:left;">
        <tr>
              <td class="PageHeading" onmouseover="Tip('Upload Packaging Specification')" onmouseout="UnTip()" >
                  Upload Packaging Specification
              </td>
        </tr>
    </table>
    <br />
    <br />
    
<div id="ContentPagemargin">
   <div id="PageSection1" style="text-align:left;">
   <br />
   <div style="margin-left:5px;margin-right:5px;">
       <table width="98%" style="text-align:left;">
            <tr class="AlterNateColor2">
                 <td class="PageSHeading" style="padding-left:5px;" align="center">
                    <a href="../Default.aspx" class="Link">Return To Specification Selection</a>
                </td>
                <td class="PageSHeading" align="center">
                   <asp:Label ID="lblStatus" runat="server" CssClass="Error" Visible="false" Text="Uploaded Specification Successfully"></asp:Label>
                </td>
            </tr>
              <tr class="AlterNateColor1">
                <td style="width:30%" align="right">
                    Please Select File For Upload:
                </td>
                  <td align="left">
                    <asp:FileUpload ID="CommanUpload" runat="server" BorderStyle="Ridge" BorderColor="white" ToolTip="Please Select file for upload"  Width="500px"/>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td style="width:30%" align="right">
                    
                </td>
                  <td align="left">
                   <asp:Button ID="btnUpload" Text="Upload" CssClass="Button" runat="server" Style="margin-left:0px" />
                </td>
            </tr>
           </table>
  </div>    
  <br />
   <br />
  </div>
 </div>
</asp:Content>
