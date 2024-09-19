<%@ Page Title="S3-Assumption Comparision" Language="VB" MasterPageFile="~/Masters/Sustain3.master" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Market1_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain3ContentPlaceHolder" Runat="Server">

    <script type="text/JavaScript">
     function confirmMessage(msg) {
            if (confirm(msg)) {
                window.open('Assumptions/CaseManager.aspx', 'new_Win');
                return false;
            }
            else {
                return false;
            }
        }
        function Message() 
        {
         var modType="<%=Session("SavvyModType")%>"; 
                 var cnt=0;
                if(modType=="2")
                {
                               var SisterCases="<%=Session("S3SisterCases")%>"; 
                               //alert(SisterCases);
                               var sisCases="";
                               var sisStatus="";
                               var statusId=SisterCases.split(',');
                              // alert(statusId[0]);
                             // alert(SisterCases);
                                var x = document.getElementById("ctl00_Sustain3ContentPlaceHolder_CaseComp");
                                for (var i = 0; i < x.options.length; i++) {
                                    if (x.options[i].selected == true) {
                                      //  alert(x.options[i].value);
                                        if(statusId[i]=="5")
                                        {
                                         // alert(sisCases=x.options[i].value);
                                          if(cnt==0)
                                          {
                                            sisCases=x.options[i].value;
                                          }
                                          else
                                          {
                                            sisCases=sisCases+","+ x.options[i].value;
                                          }
                                          cnt=cnt+1;
                                          
                                        }
                                    }
                                }
                                //alert(sisCases);
                    if(sisCases=="")
                    {
                        var msg = "----------------------------------------------------------------------------------\n"
                          msg = msg+"You are going to create a new comparison. Do you want to proceed?\n----------------------------------------------------------------------------------\n"
                    }
                    else
                    {
                         var msg = "----------------------------------------------------------------------------------\n"
                         msg  = msg+" Following cases are Sister Case(s) " + sisCases + ".\nThe Sister Case(s) probably need to be upgraded to Approved \n cases in order for the comparision to make sense.\n\n You are going to create a new comparison. Do you want to proceed?\n"
                         msg =msg+ "----------------------------------------------------------------------------------\n"
                    }
                }
                else
                {
                //alert("SisterCases:"+sisCases);
                   var msg = "You are going to create a new comparison. Do you want to proceed?"
                }

                 if (confirm(msg)) {
                return true;
            }
            else {
                return false;
            }
        }
     function MakeVisible(id) 
     {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            if (id == "renamediv") 
            {
                var combo1 = document.getElementById('<%=SavedComp.ClientID%>'); 
                var val = combo1.options[combo1.selectedIndex].text;
                var Index1 = val.indexOf('-') + 2;
                var val2 = val.substring(Index1, val.length);
                var Index2 = val2.indexOf(':') - 1;
                var val3 = val2.substring(0, Index2);
                document.getElementById('<%=txtrename.ClientID%>').focus();
                document.getElementById('<%=txtrename.ClientID%>').value = val3;
            }
            else 
            {
                          document.getElementById('<%=ComparisonName.ClientID%>').focus();
            }
            return false;

        } 
	  
	
	     function MakeInVisible(id)
     {
               objItemElement = document.getElementById(id)
               objItemElement.style.display = "none"
            return false;
           
            
	  }
	  function deleteconfirmation()
	  {
	             return confirm("Are you sure,you want to delete saved Comparison? ");
	        
	  }

	  function valList() {

	      var selCount = 0;
	      var namelength = document.getElementById("ctl00$Sustain3ContentPlaceHolder$ComparisonName").value
	      for (var i = 0; i < document.getElementById("ctl00$Sustain3ContentPlaceHolder$CaseComp").length; i++) {
	          if (document.getElementById("ctl00$Sustain3ContentPlaceHolder$CaseComp")[i].selected) {
	              selCount += 1;
	          }

	      }

	      if (selCount > 10) {
	          alert("You Cannot Select More Then 10 Cases!!!");
	          return false;
	      }

	      if (selCount < 2) {
	          alert("Please Select at Least Two Case!!");
	          return false;
	      }
	      if (namelength == "") {
	          alert("Please enter the text for new comparison!!");
	          return false;
	      }
	      //alert(document.getElementById("ctl00$Sustain3ContentPlaceHolder$ComparisonName").value);
	      return true;

	  }

	  

	  
</script>
    <div id="header" class="PageHeading" style="margin-left:5px;">
        Comparison Manager
     </div>
    <div id="ContentPagemargin" style="width:830px;">
      <br />
    <div id="label">
            <asp:Label ID="Displaynote" CssClass="label" runat="server" Visible="false"></asp:Label>
        </div>
       <table class="Comparison" cellpadding="0" cellspacing="0" border="0" style="padding-left:10px;">
                
                <tr>
                    <td class="PageHeading" style="padding-left: 10px;">Existing Comparisons</td>
                </tr> 
                 <tr>
                    <td align="right" valign="top">
                             
                            &nbsp;&nbsp;&nbsp;     
                    </td>
                 </tr>   
                
                
                <tr id="ddtr1" runat="server">
                       <td style="height: 22px" colspan="2">
                            <asp:Label ID="nodatatr1" runat="server" Text="There is no Proprietary Comparisons please start with new comaprison"></asp:Label>
                            <asp:DropDownList ID="SavedComp" CssClass="DropDown" runat="server" 
                                Width="536px" ></asp:DropDownList>
                        </td>
                </tr>
                
                <tr id="buttontr1" runat="server" style="height:50px">
                    <td ><asp:Button ID="SavedButton" Text="Start Comparison" CssClass="Button" runat="server" CausesValidation="False"/>
                     <asp:Button ID="btnEditComp" Text="Edit Comparison" CssClass="Button" runat="server"
                        CausesValidation="False" Width="140px" />
                        <asp:Button ID="DeleteButton" runat="server" CssClass="Button" Text="Delete Comparison" OnClientClick="return deleteconfirmation()" CausesValidation="False" />
                       <%-- <%--<asp:Button ID="RenameButton" runat="server" CssClass="Button" Text="Rename Comparision" />--%>
                       <%-- <input type="button" class="Button" name="rename" value="Rename Comparision" onclick="return MakeVisible('renamediv')"  />--%>
                        </td>     
                    
                 </tr>
                
                <tr id="buttontr11" runat="server">
                    <td>
                        <div id="renamediv" style="display:none">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:TextBox ID="txtrename" runat="server" CssClass="Button" ></asp:TextBox>
                                  <br />
                                </td>
                                <td> <asp:Button ID="RenameButton" runat="server" CssClass="Button" Text="Rename Comparision" CausesValidation="False" />
                                  <br />
                                </td>
                                 <td>  <input type="button" class="Button" name="cancle" value="Cancle" onclick="return MakeInVisible('renamediv')" />
                                  <br />
                                </td>
                              
                            </tr>
                        </table>
                        </div>
                        
                        <asp:Label ID="RenameError" runat="server" Visible="false" ForeColor="red"></asp:Label>
                    </td>  
                    
                 </tr>      
                 
             </table>   
             
          <table class="Comparison" cellpadding="0" cellspacing="0" border="0" style="width: 648px;padding-left:10px;">
                <tr>
                    <td style="width: 600px; padding-top: 5px;" class="PageHeading">Create a New Comparison</td>
                </tr>
                <tr>
                    <td style="width: 600px; padding-left: 15px;">Hold control key to select multiple cases.
                              <br />
                    Maximum 10 cases per comparison    
                    </td>
                </tr> 
                
                   <tr style="height:20px">
                    <td align="right" valign="top">
                             <asp:Label ID="CreateCompError" runat="server" Visible="false" ForeColor="red"></asp:Label>
                            &nbsp;&nbsp;&nbsp;     
                    </td>
                 </tr>      
                
                <tr>
                       <td style="width: 600px">
                            <asp:ListBox CssClass="DropDown" ID="CaseComp" EnableViewState="true" 
                                runat="server" SelectionMode="Multiple" Width="552px" Height="193px" > </asp:ListBox>
                            
                       </td>
                </tr>
                
                <tr style="height:50px">
                     <td style="width: 600px">
                     <input class="Button" type="button" value="Create Comparison" onclick="return MakeVisible('newcomparisiondiv')"/> 
                     <input class="ResetButton" type="reset" value="Reset" /> </td>
                 </tr> 
                     
                <tr>
                    <td>
                        <div id="newcomparisiondiv" style="display:none">
                         <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td><asp:TextBox ID="ComparisonName" runat="server" CssClass="Button" ></asp:TextBox>
                                  <br />
                                    </td>
                                <td> <asp:Button ID="StartComp" runat="server" CausesValidation="true" CssClass="Button" OnClientClick="return Message();" Text="Create Comparison" />
                                  <br />
                                </td>
                                 <td align="left">  <input type="button" class="Button" name="cancle" value="Cancel" onclick="return MakeInVisible('newcomparisiondiv')" />
                                  <br />
                                </td>
                              
                            </tr>
                        </table>
                        </div>
                       
                        
                    </td>  
                    
                 </tr>   
                      
                 
             </table>
               
               
         <table class="Comparison" cellpadding="0" cellspacing="0" border="0" style="width: 648px;padding-left:10px;">
                
                <tr>
                    <td style="padding-left: 10px;" class="PageHeading">Share an Existing Comparison</td>
                </tr> 
             <tr id="nodatatr2" runat="server">
                 <td style="font-size: 15px;font-weight:bold; padding-left: 10px;">
                     &nbsp;There is no Proprietary Comparisons please start with new comaprison
                     <br />
                 </td>
             </tr>
                <tr id="headingtr1" runat="server">
                        <td style="font-size:15px">&nbsp;&nbsp;Existing Comparisons</td>
                </tr>
                <tr id="ddtr2" runat="server">
                       <td style="height: 22px">
                            <asp:DropDownList ID="SharedComp" CssClass="DropDown" runat="server" 
                                Width="548px" ></asp:DropDownList>          
                        <br />
                       </td>
                </tr>
                
                 <tr id="headingtr2" runat="server">
                        <td style="font-size:15px">&nbsp;&nbsp;Coworker with whom to share:</td>
                </tr>
                <tr  id="ddtr3" runat="server">
                       <td>
                            <asp:DropDownList ID="Coworker" CssClass="DropDown" runat="server" 
                                Width="250px"></asp:DropDownList>          
                       </td>
                </tr>
                <tr style="height:50px"  id="buttontr3" runat="server">
                    <td style="height: 50px"><asp:Button ID="SharedButton" Text="Share Comparison" CssClass="Button" runat="server" CausesValidation="False" /></td>     
                    
                 </tr>  
                                
                 
             </table>  
             <BR />
   </div> 
</asp:Content>

