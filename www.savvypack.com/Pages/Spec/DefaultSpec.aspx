<%@ Page Title="SPEC-Specification" Language="VB" MasterPageFile="~/Masters/Spec.master" AutoEventWireup="false" CodeFile="DefaultSpec.aspx.vb" Inherits="Pages_Spec_Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">

    <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../JavaScripts/tip_balloon.js"></script>
  
    <table width="840px"  style="text-align:left;">
        <tr>
              <td class="PageHeading" onmouseover="Tip('Packaging Specification')" onmouseout="UnTip()" >
                  Packaging Specification
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
                <td class="PageSHeading" style="padding-left:5px;">
                    Select Packaging Specification
                </td>
            </tr>
             <tr class="AlterNateColor1">
                <td> 
                    <asp:DropDownList ID="ddlSpec" runat="server" CssClass="DropDown" Width="700px" Visible="false"></asp:DropDownList>
                    <asp:Label ID="lblNoSpec" Text="No Specification" CssClass="Error" Font-Size="15px" Visible="false" runat="server"></asp:Label>
                </td>
            </tr>
            <tr class="AlterNateColor1">
                <td>
                    <asp:Button id="btnSelect" Text="Start Specification" runat="server" CssClass="Button" style="margin-left:0px"/>
                    <asp:Button id="btnCreate" Text="Create Specification" runat="server"  CssClass="Button" />
                    <asp:Button id="btnUpdate" Text="Update Specification" runat="server"  CssClass="Button" />
                </td>
            </tr>
        </table>
        <br />
        <div id="divPack" runat="server" visible="false">
            <table style="width:98%">
               <tr class="AlterNateColor2">
                <td class="PageSHeading" colspan="2" style="padding-left:5px;">
                    Packaging Specifiaction Details
                </td>
               </tr>
             <tr class="AlterNateColor1">
                <td align="right"> 
                   <b>Packaging Company:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlPackComp" runat="server" CssClass="DropDown" Width="200px"></asp:DropDownList>
                </td>
            </tr>
              <tr class="AlterNateColor1">
                <td align="right"> 
                   <b><span style="color:Red;">*</span>Name:</b>
                </td>
                <td>
                    <asp:TextBox ID="txtPackName" runat="server" CssClass="NormalTextBox" Style="text-align:left;"
                        Height="17px" Width="195px"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="reqName" runat="server" ControlToValidate="txtPackName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
              <tr class="AlterNateColor1">
                <td align="right"> 
                   <b>E1S1 CaseId:</b>
                </td>
                <td>
                    <asp:DropDownList ID="ddlE1S1CaseID" runat="server" CssClass="DropDown" Width="500px"></asp:DropDownList>
                </td>
            </tr>   
            <tr class="AlterNateColor1">
                <td></td>
                <td>
                    <asp:Button id="btnCreatePkg" runat="server" Text="Create" CssClass="Button" style="margin-left:0px" />
                    <asp:Button id="btnCancle" runat="server" Text="Cancel" CssClass="Button" CausesValidation="false"  style="margin-left:10px"  /> 
                </td>
            </tr>
            </table>
            <br />
        </div>
        
        <div id="divAdmin" runat="server" visible="true">
              <table style="width:98%">
               <tr class="AlterNateColor2">
                <td class="PageSHeading" style="padding-left:5px;">
                    Administration Links
                </td>
               </tr>
               <tr class="AlterNateColor1">
                <td class="PageSSHeading"> 
                    <a href="Uploads/MassSpecUpload.aspx" class="Link" style="font-size:11px;">Upload Specification</a>
                </td>
               </tr>
               </table>
        </div>
   </div>
        
   </div>
   
   </div>
</asp:Content>
