<%@ Page Language="VB" MasterPageFile="~/MasterPage.master" AutoEventWireup="false" CodeFile="DiffrentCharts.aspx.vb" Inherits="Charts_Default" title="Different Chart Module" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="header">
        Price Chart Module
    </div>
    <br />
     <%--<a class="Logoff" href="file:///J:/Econ/Logoff.asp">Log Off</a> --%>
     <a class="Logoff" href="../Universal_loginN/Pages/ULogOff.aspx?Type=E1">Log Off</a>
     
     <div style="width:700px;text-align:right"> 
      <a class="Logoff" href="ChartPreferences/ChartPreferences.aspx" target="_blank">Preferences</a>      
     </div>
      <table class="FinTable" cellspacing="0" cellpadding="0" border="0">
        <tr style="height:40px;font-weight:bold" align="center" class="HeaderTR">
            <td>Different Charts </td>
        </tr>
        <tr class="ColorTR">
             <td>
                 &nbsp;&nbsp;<a class="link" href="MaterialPrice.aspx" target="_blank">Material Price Chart</a> 
             </td>
        </tr>
          <tr class="ColorTR">
             <td>
                 &nbsp;&nbsp;<a class="link" href="SalaryPrice.aspx" target="_blank">Salary Chart</a> 
             </td>
        </tr>
          <tr class="ColorTR">
             <td>
                 &nbsp;&nbsp;<a class="link" href="EnergyPrice.aspx" target="_blank">Energy Price Chart</a> 
             </td>
        </tr>
        <tr class="noncolored">
            <td></td>   
        </tr>
    </table>
</asp:Content>

