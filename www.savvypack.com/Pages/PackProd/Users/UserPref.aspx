<%@ Page Language="VB" MasterPageFile="~/Masters/PackProd.master" AutoEventWireup="false" CodeFile="UserPref.aspx.vb" Inherits="Pages_PackProd_Users_UserPref" title="Pack. Prod.-User Intelligence" %>
<asp:Content ID="Content1" ContentPlaceHolderID="PackProdContentPlaceHolder" Runat="Server">

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script> 
    <script type="text/javascript">
        function CloseWindow()
        {
            
              window.opener.location.reload();
              window.close();
          
          
        }
         function CheckAnnualSaleValue()
        {
          var errorMsg1="";
          var errorMsg="";
           var msg="";
           var catVal = document.getElementById('<%= hdncatg.ClientID%>').value;          
           if(catVal==2)
           {         
             var saleVal= document.getElementById('<%= txtComm.ClientID%>').value; 
             if(saleVal=="")
             {
                 msg = "----------------------------------------------------------------------------------------------\n";
                     msg += "\You have not entered a numeric Annual (Sales) value.  Do you wish to continue?\n";  
                     msg += "---------------------------------------------------------------------------------------------\n";
                    var answer = confirm(msg);
                    
                    if(answer==true)
                    {        
                      return true;
                    }
                    else if(answer==false)
                    {       
                      return false;
                    }
             }
             else
             {
                if(IsNumeric(saleVal))
                 {
                    return true;
                 }
                 else
                 {
                                     
                 errorMsg1 += "\Please enter numeric Annual(Sales) value for UI Section.\n";                
                  msg = "------------------------------------------------------------------------------\n";
                  msg += "\t Please correct the problem(s).\n";
                  msg += "------------------------------------------------------------------------------\n";
                 errorMsg += alert(msg+errorMsg1+"\n\n");
                 return false;
                 }
             }
           
             return false;
           }
           else
           {
              return true;
           }
         
        }
        
        function IsNumeric(sText)
            {
               var ValidChars = "0123456789.";
               var IsNumber=true;
               var Char;

             
               for (i = 0; i < sText.length && IsNumber == true; i++) 
                  { 
                  Char = sText.charAt(i); 
                  if (ValidChars.indexOf(Char) == -1) 
                     {
                     IsNumber = false;
                     }
                  }
               return IsNumber;
               
               }
      
        
    </script>
    
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left" >
       <br />
          <asp:HiddenField ID="hdncatg" runat="Server" />
                <table width="75%" cellpadding="0px" cellspacing ="0px">
                  <tr class="AlterNateColor4" >
                        <td class="PageSHeading" style="font-size:14px;text-align:center" colspan="2"> 
                            
                        </td>
                    </tr>
                     <tr class="AlterNateColor1" id="trCatDropdown" runat="server" visible="false"    >
                        <td style="padding-left:10px;width:15%">
                            Select Category:
                        </td>
                        <td>
                            <asp:DropDownList id="ddlCat" runat="server" CssClass="DropDown" Width="200px" Enabled="false"></asp:DropDownList>
                        </td>
                    </tr>
                    
                    <tr  class="AlterNateColor2">
                        <td colspan="2" style="padding-left:5px">
                            <div id="divCatDetails" runat="server" style="margin-bottom:15px;font-weight:bold;margin-right:10px;" visible="false">
                               
                                  <fieldset>
                                    <legend style="color:black;font-weight:bold">
                                        <asp:Label ID="lblSelect" runat="server"></asp:Label>
                                    </legend>
                                    <table style="width:600px">
                                        <tr style="height:20px;text-align:center">
                                            <td class="TdHeading" style="width:5%" onmouseover="Tip('SavvyPack Corporation Intelligence ')" onmouseout="UnTip()">
                                                AI
                                            </td>
                                            <td class="TdHeading" style="text-align:left;padding-left:5px;" onmouseover="Tip('User Intelligence ')" onmouseout="UnTip()">
                                                User Intelligence
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor3">
                                            <td valign="top">
                                                <asp:CheckBoxList Enabled="false" Style="font-weight:normal;border:solid 1 black" id="chkDetailsSugg" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" ></asp:CheckBoxList> 
                                            </td>
                                            <td valign="top">
                                                <asp:CheckBoxList Style="font-weight:normal;border:solid 1 black" id="chkDetails" runat="server" RepeatColumns="1" RepeatDirection="Horizontal" ></asp:CheckBoxList> 
                                            </td>
                                        </tr>
                                    </table>
                                    
                               </fieldset> 
                            </div>
                             <div id="divCatComments" runat="server" style="font-weight:bold" visible="false">
                             <fieldset>
                                    <legend style="color:black;font-weight:bold">
                                         <asp:Label ID="lblComment" runat="server"></asp:Label>
                                    </legend>
                            
                                  <table style="width:600px">
                                        <tr style="height:20px;text-align:center">
                                            <td class="TdHeading" style="width:50%" onmouseover="Tip('SavvyPack Corporation Intelligence ')" onmouseout="UnTip()">
                                                SavvyPack Corporation Intelligence
                                            </td>
                                            <td class="TdHeading" onmouseover="Tip('User Intelligence ')" onmouseout="UnTip()">
                                                User Intelligence
                                            </td>
                                        </tr>
                                         <tr class="AlterNateColor3">
                                            <td valign="top" style="height: 104px">
                                                <asp:TextBox TextMode="MultiLine" Rows="10" Height="100px" Width="300px" ID="txtCommSugg" runat="server" Enabled="false"></asp:TextBox>
                                            </td>
                                                
                                            <td valign="top" style="height: 104px">
                                                <asp:TextBox TextMode="MultiLine" Rows="10" Height="100px" Width="300px" ID="txtComm" runat="server"></asp:TextBox>
                                            </td>
                                            </tr>
                                        </table>
                                        
                                </fieldset> 
                            </div>
                            <br />
                        </td>
                    </tr>
                </table>
                <br />
       </div>
    
    </div>
</asp:Content>

