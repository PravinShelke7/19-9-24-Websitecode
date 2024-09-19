<%@ Page Language="VB" MasterPageFile="~/Masters/VChain.master" AutoEventWireup="false"
    CodeFile="AddEditVChainItem.aspx.vb" Inherits="Pages_ValueChain_Tools_AddEditVChainItem"
    Title="Add/Edit Value Chain Items" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VChainContentPlaceHolder" runat="Server">

    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>

    <script type="text/JavaScript">
          function ShowPopWindow(Page) {
                //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
                var width = 550;
                var height = 400;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=no';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                newwin = window.open(Page, 'Chat', params);
              

            }
            function ShowModPopWindow(Page,modTypeId) {
             
               Page=Page+"&ModId="+document.getElementById(modTypeId).value;
                var width = 550;
                var height = 400;
                var left = (screen.width - width) / 2;
                var top = (screen.height - height) / 2;
                var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
                params += ', location=no';
                params += ', menubar=no';
                params += ', resizable=no';
                params += ', scrollbars=yes';
                params += ', status=yes';
                params += ', toolbar=no';
                newwin = window.open(Page, 'Chat', params);

            }
            
    
        function MakeVisible(id) {

            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            if (id == "renamediv") {
                var combo1 = document.getElementById("ctl00_Econ3ContentPlaceHolder_SavedComp");
                var val = combo1.options[combo1.selectedIndex].text;
                var Index1 = val.indexOf('-') + 2;
                var val2 = val.substring(Index1, val.length);
                var Index2 = val2.indexOf(':') - 1;
                var val3 = val2.substring(0, Index2);
                document.getElementById("ctl00_Econ3ContentPlaceHolder_txtrename").focus();
                document.getElementById("ctl00_Econ3ContentPlaceHolder_txtrename").value = val3;
            }
            else {
                document.getElementById("ctl00$Econ3ContentPlaceHolder$ComparisonName").focus()
            }
            return false;

        } 
        function MessageWindow()
        {
          var msg="";
            msg = "-----------------------------------------------------------------------------------------------------------------------------\n";
            msg += "A User account already exists for this email address..\n Please enter different email address to create a new account.\n Or, if you have forgotten your password to your original account, let us know and we will email it to you.\n";
            msg += "-----------------------------------------------------------------------------------------------------------------------------\n";
            alert(msg);
        }


        function MakeInVisible(id) {
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "none"
            return false;


        }
        function deleteconfirmation() {
            return confirm("Are you sure,you want to delete saved Comparison? ");

        }

        function valList() {

            var selCount = 0;
            var namelength = document.getElementById("ctl00$Econ3ContentPlaceHolder$ComparisonName").value
            for (var i = 0; i < document.getElementById("ctl00$Econ3ContentPlaceHolder$CaseComp").length; i++) {
                if (document.getElementById("ctl00$Econ3ContentPlaceHolder$CaseComp")[i].selected) {
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
          return true;

        }

	  

	  
    </script>

    <table class="ContentPage" id="ContentPage" runat="server" style="width: 839px">
     
        <tr>
            <td>
                <div id="ContentPagemargin">
                    <div id="PageSection1" style="text-align: left; margin-left: 0px;">                      
                    
                     <table width="750px" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="height: 12px;">
                                </td>
                            </tr>
                            <tr class="AlterNateColor4" style="height: 9px;">
                                <td style="height: 0px;">
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <table>
                                        <tr>
                                            <td class="PageSHeading" style="font-size: 18px;" colspan="2" onmouseover="Tip('Add/Edit Value Chain Items')"
                    onmouseout="UnTip()">
                                               Add/Edit Value Chain Items
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px;" colspan="2">
                                            </td>
                                        </tr>
                                          <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Value Chain:
                                </td>
                                <td style="width: 70%">
                                    <asp:DropDownList ID="ddlVchain" runat="server"  CssClass="DropDown" Width="200" style="font-size:10px;font-family:Optima"
                                        AutoPostBack="true">
                                      
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                     Result Module:
                                </td>
                                <td style="width: 70%">
                                    <asp:DropDownList ID="ddlPageName" runat="server" CssClass="DropDown" Width="200" style="font-size:10px;font-family:Optima"
                                        AutoPostBack="true">
                                        
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td style="width: 15%" align="right">
                                    Case Id:
                                </td>
                                <td style="width: 70%">
                                    <asp:DropDownList ID="ddlCaseId" runat="server" CssClass="DropDown" Width="500px" style="font-size:10px;font-family:Optima"
                                        AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="ButtonWMarigin"
                                        Style="width: 80px;" onmouseover="Tip('Submit')"
                                        onmouseout="UnTip()" />
                                </td>
                            </tr>
                             <tr class="AlterNateColor1" id="trTable" runat="server" visible="false">
                                <td colspan="2">
                                
                                    <asp:Table ID="tblComp" runat="server">
                                    </asp:Table>
                                </td>
                            </tr>
                            <tr class="AlterNateColor1" id="trButton" runat="server" visible="false">
                                <td>
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="ButtonWMarigin"
                                        Style="width: 80px;" onmouseover="Tip('Update')" onmouseout="UnTip()"  />
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="ButtonWMarigin"
                                        Style="width: 80px;" onmouseover="Tip('Cancel')" onmouseout="UnTip()" />
                                </td>
                            </tr>
                                        <tr>
                                            <td style="height: 12px;" colspan="2">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hdnPrevCount" runat="server" />
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
