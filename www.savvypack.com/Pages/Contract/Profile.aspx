<%@ Page Language="VB" MasterPageFile="~/Masters/Contract.master" AutoEventWireup="false" CodeFile="Profile.aspx.vb" Inherits="Pages_Contract_Profile" title="Contract-Company Profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContractContentPlaceHolder" Runat="Server">

    <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../JavaScripts/E1Comman.js"></script>
    <script type="text/javascript">
    
        function UserPref(PageUrl) 
         {            
           location.href=PageUrl;
         }
    </script>
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
                 <br />
                <div id="divCompanyProf" runat="server" style="margin-left:10px;font-size:14px;">
                    
                </div>
             <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>
             <br />               
            
         </div>
         
         
   
   </div>
</asp:Content>

