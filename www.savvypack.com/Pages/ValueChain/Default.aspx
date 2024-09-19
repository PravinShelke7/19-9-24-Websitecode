<%@ Page Language="VB" MasterPageFile="~/Masters/VChain.master" AutoEventWireup="false"
    CodeFile="Default.aspx.vb" Inherits="Pages_ValueChain_Default" Title="Value Chain - Global Manager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VChainContentPlaceHolder" runat="Server">

    <script type="text/JavaScript" src="../../JavaScripts/collapseableDIV.js"></script>

    <script type="text/JavaScript" src="../../JavaScripts/wz_tooltip.js"></script>

    <script type="text/JavaScript" src="../../JavaScripts/tip_balloon.js"></script>

    <script type="text/JavaScript">
            function requiredField()
        {
            var txtCount=document.getElementById("<%=txtVChain.ClientId%>").value;
            if(txtCount=="")
            {
              alert("Please enter Value Chain name!");
              return false;
            }
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
            return confirm("Are you sure,you want to delete saved Value Chain? ");

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

    <table class="ContentPage" id="ContentPage" runat="server" style="width: 840px">
        <tr style="height: 10px">
            <td>
                <div class="PageHeading" id="divMainHeading" runat="server" onmouseover="Tip(' Value Chain - Global Manager')"
                    onmouseout="UnTip()" style="width: 820px;">
                    Value Chain - Global Manager
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <div id="ContentPagemargin">
                    <div id="PageSection1" style="text-align: left; margin-left: 0px;">
                        <table width="750px" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="height: 10px;">
                                </td>
                            </tr>
                            <tr class="AlterNateColor4" style="height: 9px;">
                                <td >
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <table>
                                        <tr>
                                            <td class="PageSHeading" style="font-size: 18px;">
                                                Existing Comparison
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px;">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:DropDownList ID="ddlVChain" runat="server" CssClass="DropDown" Width="500px">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px;">
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                                <table>
                                                    <tr>
                                                        <td style="width: 150px;">
                                                            <asp:Button ID="btnStart" runat="server" Text="Start Value Chain" CssClass="ButtonWMarigin"
                                                                Style="width: 140px;" onmouseover="Tip('Start Value Chain')" onmouseout="UnTip()" />
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btnDelete" runat="server" Text="Delete Value Chain" CssClass="ButtonWMarigin"
                                                                Style="width: 140px;" OnClientClick="return deleteconfirmation();" onmouseover="Tip('Delete Value Chain')" onmouseout="UnTip()"  />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 12px;">
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="750px" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="height: 12px;">
                                </td>
                            </tr>
                            <tr class="AlterNateColor4" style="height: 9px;">
                                <td >
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <table>
                                        <tr>
                                            <td class="PageSHeading" style="font-size: 18px;" colspan="2">
                                                Create a New Value Chain
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
                                                <asp:TextBox ID="txtVChain" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                    width: 230px" MaxLength="90"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="width: 15%" align="right">
                                               Result Module:
                                            </td>
                                            <td style="width: 70%">
                                                <asp:DropDownList ID="ddlResPage" runat="server" CssClass="DropDown" Width="230px"
                                                    Style="font-size: 10px; font-family: Optima" AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td style="width: 15%" align="right">
                                                Results Case Id:
                                            </td>
                                            <td style="width: 70%">
                                                <asp:DropDownList ID="ddlResCaseId" runat="server" CssClass="DropDown" Width="500px"
                                                    Style="font-size: 10px; font-family: Optima" AutoPostBack="false">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr class="AlterNateColor1">
                                            <td>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnAdd" runat="server" Text="Create Value Chain" CssClass="ButtonWMarigin"
                                                    Style="width: 150px;" OnClientClick="return requiredField();" onmouseover="Tip('Create a Value Chain')"
                                                    onmouseout="UnTip()" />
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
                        <table width="750px" cellpadding="2" cellspacing="2">
                            <tr>
                                <td style="height: 12px;">
                                </td>
                            </tr>
                            <tr class="AlterNateColor4" style="height: 9px;">
                                <td >
                                </td>
                            </tr>
                            <tr class="AlterNateColor1">
                                <td>
                                    <asp:Button ID="btnAddEdit" runat="server" CssClass="Button" Width="220px" Text="Go To Add/Edit Value Chain Items"
                                        Style="margin-left: 0px;" />
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
