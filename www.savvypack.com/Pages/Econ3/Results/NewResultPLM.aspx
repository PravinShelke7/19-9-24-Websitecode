<%@ Page AutoEventWireup="false" CodeFile="NewResultPLM.aspx.vb" Inherits="Pages_Econ3_Results_NewResultPLM"
    Language="VB" MasterPageFile="~/Masters/Econ3.master" Title=" " %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" Runat="Server">
     <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
  <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
	 <script type="text/javascript">
	     function removeSession() {
	         // localStorage.removeItem("R7");
	         document.cookie = "W7=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
	     }

	     window.setInterval(function () {

	         //if (localStorage.getItem("R7") != null) {
	         //  localStorage.removeItem("R7");
	         //  location.reload();
	         // }
	         if (document.cookie.length != 0) {

	             var ca = document.cookie.split(";");
	             for (var i = 0; i < ca.length; i++) {
	                 var c = ca[i].trim();

	                 if (c.indexOf("W7") == 0) {

	                     document.cookie = "W7=;domain=.savvypack.com;path=/;expires=Fri, 19 Jun 2000 20:47:11 UTC;";
	                     location.reload();
	                 }
	             }
	         }

	     }, 1500);
</script>
    <table width="840px">
            <tr align="left">
                <td style="width:43%" class="PageHeading" onmouseover="Tip('Econ3 - Revenue Price by Weight')" onmouseout="UnTip()" >
                  Econ3 - 
                </td>
            </tr>
        </table>
     <br /> 
  
  
   <br />
   <div id="ContentPagemargin">
   <div id="PageSection1" style="text-align:left">
        <br />
           <%-- <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"></asp:Table>--%>
           <div id="grid" runat="Server" style="width:1450px; height: 650px; overflow: auto; margin-right: 20px; margin-top: 10px;">
        <headerstyle backcolor="Black" forecolor="White" cssclass="FixedHeader" width="1400px" />
        <asp:GridView ID="grdMaterials" runat="server" AutoGenerateColumns="false" AllowSorting ="true" CellPadding="4" >
            <RowStyle CssClass="AlterNateColor1" />
            <AlternatingRowStyle CssClass="AlterNateColor2" />
            <HeaderStyle BackColor="Black" ForeColor="White" />
        </asp:GridView>
               </div> 
         <br />
     </div>
   </div>

    <asp:HiddenField ID="hidSortId" runat="server" />
   </asp:Content>

