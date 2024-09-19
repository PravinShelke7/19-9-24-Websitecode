<%@ Page Title="S3-Edit Comparison" Language="VB" AutoEventWireup="false" MasterPageFile="~/Masters/Sustain3.master" CodeFile="EditComparision.aspx.vb" Inherits="Pages_Sustain3_Tools_EditComparision" %>

<asp:Content ID="Content2" ContentPlaceHolderID="Sustain3ContentPlaceHolder" runat="Server">

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

    <script type="text/javascript" language="javascript">
     function getCountForward()
     {
        var lst1 = document.getElementById('<%=lstRegion1.ClientID%>'); 
         var lst2 = document.getElementById('<%=lstRegion2.ClientID%>'); 
         var count1=0;
         var count2 = lst2.options.length;
         var countSum=0;       
         for (i = 0; i < lst1.options.length; i++) 
         {
          if (lst1[i].selected) 
          {
            count1=count1+1;
          } 
        }
        countSum=count1+count2;
         if(count2==10)
        {
           var msg = "-----------------------------------------------------\n";
            msg += "maximum number of cases already transferred\n";
            msg += "-----------------------------------------------------\n";
          alert(msg);
           return false;
        }
        if(count1<1)
        {
           var msg = "-----------------------------------------------------\n";
            msg += "Please select Atleast one case for transfering\n";
            msg += "-----------------------------------------------------\n";
          alert(msg);
           return false;
        }
        if(countSum<=10)
        {
          return true;
        }
        else
        {
           var msg = "-----------------------------------------------------\n";
            msg += "You cannot transfer more than "+(10-count2)+" cases .\n";
            msg += "-----------------------------------------------------\n";
          alert(msg);
           return false;
        }
       
     }
     
     function getCountBackward()
     {
         var lst1 = document.getElementById('<%=lstRegion1.ClientID%>'); 
         var lst2 = document.getElementById('<%=lstRegion2.ClientID%>'); 
 
         var count1=0;
         var count2 = lst2.options.length;
         var countSum=0;       
         for (i = 0; i < lst2.options.length; i++) 
         {
          if (lst2[i].selected) 
          {
            count1=count1+1;
          } 
        }
        if(count1==count2)
        {
            var msg = "-----------------------------------------------------\n";
            msg += "You can not delete all the cases.\n";
            msg += "-----------------------------------------------------\n";
          alert(msg);
           return false;
        }
        if(count1==0)
        {
            var msg = "-----------------------------------------------------\n";
            msg += "Please select Atleast one case for transfering\n";
            msg += "-----------------------------------------------------\n";
          alert(msg);
           return false;
        }
        else
        {
          return true;
        }       
     }
    </script>

    <table width="840px">
        <tr align="left">
            <td style="width: 43%" class="PageHeading" onmouseover="Tip('S3-Edit Comparison')"
                onmouseout="UnTip()">
                Sustain3 - Edit Comparison
            </td>
        </tr>
    </table>
    <br />
    <div id="ContentPagemargin">
        <div id="PageSection1" style="text-align: left">
            <br />
            <table>
                <tr>
                    <td colspan="3">
                        <table>
                            <tr>
                                <td>
                                    <b>Comparison Name</b>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCName" runat="server" CssClass="SearchTextBox" Width="150px"
                                        MaxLength="20"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="reqtxtCname" runat="server" ControlToValidate="txtCName"
                                        ErrorMessage="Comparison Name shouldnt be blank"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                  <tr>
                    <td valign="middle">
                         <span style="font-family:Arial;font-size:14px;font-weight:bold ">Cases:</span>
                    </td>
                    <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                    
                    </td>
                    <td valign="middle">
                         <span style="font-family:Arial;font-size:14px;font-weight:bold ">Current Cases:</span>
                    </td>
                </tr>
                 <tr>
                    <td valign="middle">
                        <asp:Panel ID="pnlRegion1" runat="server" Width="450px" Height="300px" style="overflow:auto;">
                            <asp:ListBox ID="lstRegion1" runat="server" SelectionMode="Multiple" Height="280px"
                                Style="font-family: Verdana; font-size: 11px;"></asp:ListBox>
                        </asp:Panel>
                    </td>
                    <td valign="middle" style="padding-left: 10px; padding-right: 10px;">
                        <asp:Button ID="btnFwd" runat="server" Text=">" Width="50px" OnClientClick="return getCountForward()" />
                        <br />
                        <br />
                        <asp:Button ID="btnRew" runat="server" Text="<" Width="50px" OnClientClick="return getCountBackward()" />
                    </td>
                    <td valign="middle">
                        <asp:Panel ID="pnlRegion2" runat="server" Width="450px" Height="300px" style="overflow:auto;">
                            <asp:ListBox ID="lstRegion2" runat="server" Height="280px" SelectionMode="Multiple"
                                Style="font-family: Verdana; font-size: 11px;"></asp:ListBox>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>
</asp:Content>
