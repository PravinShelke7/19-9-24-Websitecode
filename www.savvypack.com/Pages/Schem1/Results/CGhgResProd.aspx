<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CGhgResProd.aspx.vb" Inherits="Pages_Schem1_Results_CGhgResProd" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Schem1-GHG Product Comparison</title>
    <link href ="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet"  type = "text/css" />
        <script type="text/javascript">
            var _gaq = _gaq || [];
            _gaq.push(['_setAccount', 'UA-16991293-1']);
            _gaq.push(['_trackPageview']);

            (function () {
                var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
                ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
'.google-analytics.com/ga.js';
                var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
            })();

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="MasterContent">
         <div id="AlliedLogo">
            <table>
                <tr>
                    <td class="PageSHeading" align="center">
                        <table style="width: 845px; background-color: #edf0f4;">
                            <tr>
                                <td align="left">
                                    <asp:Image ID="imgFoot" runat="server" ImageUrl="~/Images/SavvyPackLogoB.gif" Height="45px" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>    
        <div>
             <table cellpadding="0" cellspacing="0" class="Schem1Module" style="border-collapse:collapse">
                 <tr>
                    <td style="padding-left:490px">
                     </td>
                </tr>
            </table>
        </div>

       
        <div id="error">
            <asp:Label CssClass="Error"  id="_lErrorLble" runat="server" ></asp:Label>
           </div>
       
        <table id="ContentPage" class="ContentPage" runat="server" >
            <tr>
                <td>
                    <div class="PageHeading" id="divMainHeading" style="width:840px;">
                    Schem1 - GHG Product Comparison
            
                    </div>
                </td>
            </tr>
            <tr style="height:20px">
            <td>
                <div id="ContentPagemargin" runat="server">
                    <div id="PageSection1" style="text-align:left;" >
                        <asp:Table ID="tblComparision" runat="server" CellSpacing="2" CellPadding="0">
         
                        </asp:Table>
                        <br />
                    </div>

                </div> 
            </td>
            </tr>
        </table>

    </div>     
   
    

    </form>
</body>
</html>
