<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GHGPUnitCC.aspx.vb" Inherits="Charts_Sustain2_GreenHousePerUnitCaseComp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
<title>GHG Per Unit Comparison</title>
<meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
<meta name="CODE_LANGUAGE" content="Visual Basic 7.0" />
<meta name="vs_defaultClientScript" content="JavaScript" />
<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
<link rel="stylesheet" href="../../App_Themes/SkinFile/Econ3Style.css" />
</head>
<body>

<!-- Begin Embedder Code -->
 <table>
            <tr>
                <td align="Center">
                   <asp:Image ID="Logo" ImageUrl="~/Images/Packaging_InformationLeft.gif" runat="server" height="87" width="212" ToolTip="Allied Logo" />
                   
                </td>
            </tr>
            <tr>    
                    <td>
                     <asp:Image ID="line" ImageUrl="~/Images/Line.jpg"  width="750" height="9" runat="server" ToolTip="Allied Logo" /> 
                    </td>
            </tr>
            <tr>
                <td>
                      <asp:Label ID="lblsession" Text="Your Username and/or Password are not currently valid. Your session may have timed out. Please close your current windows accessing SavvyPack Corporation's Economic Service and sign in again"  Visible="false" runat="server"></asp:Label>

                </td>
    </table>
<div id="Charts" runat="server">
<div>
<a class="Logoff" href="../ChartPreferences/ChartPreferences.aspx" target="_blank">Chart Preferences</a>
<br />
<b style="font-family:Verdana"> </b><asp:Label ID="mat" runat="server" Font-Names="Verdana" Font-Size="14Px"></asp:Label>
<br />
<br />
<table cellpadding="0" cellspacing="0" border="0" style="height:400;"  >
    <tr>
    <td align="center" style="background-color:#5B5B5B;height:50px; ">
        
         <asp:Label ID="lblyear" runat="server" ForeColor="White" Font-Bold="true" Font-Size="18px" Text="GHG Per Unit Comparison"></asp:Label>
    </td>
    <td style="width:10px;">&nbsp;</td>
    </tr>
    <tr>
        <td colspan="2">
           <div id="GHGComp" runat="server">
          </div>
        </td>
    </tr>
    <tr>
        <td colspan="2">
           <div id="htmtable" runat="server">
          </div>
        </td>
    </tr>
<tr>
<td style="height: 219px" valign="top">
     <form id="GHGCmp" action="GreenHousePerUnitCaseComp.aspx" runat="server" method="post">
          <table  border="0"  cellpadding="0" cellspacing="0" style="height:200px;background-color:#5B5B5B; width:700px; color:White; ">
        <tr>
            <td valign="top">
               
                <input  name="Chart" type="hidden" value="1"  />
                </td>
                </tr>
             </table>
    </form>

</td>
</tr>
</table>

</div>
</div>

<!-- End Embedder Code -->

</body>
</html>
