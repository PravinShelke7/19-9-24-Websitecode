<%@ Page Title="E3-Comparison Manager" Language="VB" MasterPageFile="~/Masters/Econ3.master"
    AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="Pages_Econ3_Default" %>

    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Econ3ContentPlaceHolder" runat="Server">

    <script type="text/JavaScript">
     function OpenNewWindow(Page) {

            var width = 800;
            var height = 600;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=yes';
            params += ', location=yes';
            params += ', menubar=yes';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=yes';
            newwin = window.open(Page, 'NewWindow', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }


        }

     function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
             
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
           
           
            newwin = window.open(Page, params);
            return false;
        }
        function ShowPopWindowUnit(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 640;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
                      
            newwin = window.open(Page, params);
            return false;
        }

        function confirmMessage(msg) {
            if (confirm(msg)) {
           // alert("Confirm Msg if");
                window.open('Assumptions/CaseManager.aspx', 'new_Win');
               // alert("Confirm Msg if End");
                return false;
            }
            else {
           //  alert("Confirm Msg else");
                return false;
            }
        }
        function Message() {
        //alert(" Msg ");
            var modType ="<%=Session("SavvyModType")%>";
            var cnt = 0;
            if (modType == "2") {
             //alert(" Msg if mod=2");
                var SisterCases ="<%=Session("E3SisterCases")%>";
                //alert(SisterCases);
                var sisCases = "";
                var sisStatus = "";
                var statusId = SisterCases.split(',');
                // alert(statusId[0]);
                // alert(SisterCases);
                // alert(" Msg if mod=2 after sis case declare");
                var x = document.getElementById("ctl00_Econ3ContentPlaceHolder_tabNew_modelanalysis_CaseComp");
               //  alert(" Msg if mod=2 after sis case declare x declare ");
                 // alert(x);

                for (var i = 0; i < x.options.length; i++) {
               // alert(" Msg if mod=2 after sis case declare for Loopppppppppp");
                    if (x.options[i].selected == true) {
                        //  alert(x.options[i].value);
                        if (statusId[i] == "5") {
//                         alert("  for Loop if statusid=5");
                            // alert(sisCases=x.options[i].value);
                            if (cnt == 0) {
                                sisCases = x.options[i].value;
                            }
                            else {
                                sisCases = sisCases + "," + x.options[i].value;
                            }
                            cnt = cnt + 1;

                        }
                    }
                }
                //alert(sisCases);
                if (sisCases == "") {
//                alert("  if sisCases");
                    var msg = "----------------------------------------------------------------------------------\n"
                    msg = msg + "You are going to create a new comparison. Do you want to proceed?\n----------------------------------------------------------------------------------\n"
                }
                else {
//                alert("  End else sisCases");
                    var msg = "----------------------------------------------------------------------------------\n"
                    msg = msg + " Following cases are Sister Case(s) " + sisCases + ".\nThe Sister Case(s) probably need to be upgraded to Approved \n cases in order for the comparision to make sense.\n\n You are going to create a new comparison. Do you want to proceed?\n"
                    msg = msg + "----------------------------------------------------------------------------------\n"
                }
            }
            else {
//            alert(" Msg Else mod=2");
                //alert("SisterCases:"+sisCases);
                var msg = "You are going to create a new comparison. Do you want to proceed?"
            }

            if (confirm(msg)) {
//             alert(" confirm(msg)");
                return true;
            }
            else {
                return false;
            }
        }

        function MakeVisible(id) {
       // alert("1");
            objItemElement = document.getElementById(id)
            objItemElement.style.display = "inline"
            if (id == "renamediv") {
          //  alert("renamediv");
                var combo1 = document.getElementById('<%=SavedComp.ClientID%>');
                var val = combo1.options[combo1.selectedIndex].text;
                var Index1 = val.indexOf('-') + 2;
                var val2 = val.substring(Index1, val.length);
                var Index2 = val2.indexOf(':') - 1;
                var val3 = val2.substring(0, Index2);
                document.getElementById('<%=txtrename.ClientID%>').focus();
                document.getElementById('<%=txtrename.ClientID%>').value = val3;
              //  alert("renamedivEnd");
            }
            else {
          //  alert("else");
                document.getElementById('<%=ComparisonName.ClientID%>').focus();
                   // alert("elseEnd");
            }
            return false;

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
            //alert(document.getElementById("ctl00$Econ3ContentPlaceHolder$ComparisonName").value);
            return true;

        }

        function ShowGroupPopup(Page) {
            var width = 980;
            var height = 320;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'GrpSelection', params);
            return false;
        }

        function displayView() {
            window.open('BulkTransferCost.aspx', 'new_Win001N');
            return false;
        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 640;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
                      
            newwin = window.open(Page, params);
            return false;
        }

         function ShowPopupFP(Page) {

            var width = 1260;
            var height = 655;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'RLM', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }
            return false;
        }
    </script>
    <style type="text/css">
        .Comparison_GrpSel {
            font-family: Optima;
            font-size: 12px;
            color: black;
            margin-top: 30px;
            margin-left: 20px;
            width: 648px;
            background-color: #d5d5d5;
            height: 20px;
            overflow: auto;
        }
    </style>
    <div id="header" class="PageHeading" style="margin-left: 5px;">
        Comparison Manager
    </div>
    <div id="ContentPagemargin" style="width: 830px;">
         <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
        <table>
          <tr valign="top" class="AlterNateColor2">
                        <td style="padding-left: 10px; padding-top: 15px;">
                         <ajaxToolkit:TabContainer ID="tabNew"  runat="server" 
                                AutoPostBack="true" >
                                <ajaxToolkit:TabPanel runat="server" ToolTip="model analysis"
                                    ID="modelanalysis" TabIndex="0">
                                    <HeaderTemplate>
                                      Model Analysis
                                    </HeaderTemplate>
                                    <ContentTemplate>
                                      <div id="label">
            <asp:Label ID="Displaynote" CssClass="label" runat="server" Visible="false"></asp:Label>
        </div>
        <table class="Comparison" cellpadding="0" cellspacing="0" border="0" style="padding-left: 10px;">
            <tr>
                <td class="PageHeading" style="padding-left: 10px;">Existing Comparisons</td>
            </tr>
            <tr>
                <td align="right" valign="top">&nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr id="ddtr1" runat="server">
                <td style="height: 22px" colspan="2">
                    <asp:Label ID="nodatatr1" runat="server" Text="There is no Proprietary Comparisons please start with new comaprison"></asp:Label>
                    <asp:DropDownList ID="SavedComp" CssClass="DropDown" runat="server" Width="536px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr id="buttontr1" runat="server" style="height: 50px">
                <td>
                    <asp:Button ID="SavedButton" Text="Start Comparison" CssClass="Button" runat="server"
                        CausesValidation="False" Width="140px" />
                    <asp:Button ID="btnEditComp" Text="Edit Comparison" CssClass="Button" runat="server"
                        CausesValidation="False" Width="140px" />
                    <asp:Button ID="DeleteButton" runat="server" CssClass="Button" Text="Delete Comparison"
                        OnClientClick="return deleteconfirmation()" CausesValidation="False" Width="140px" />

                    <%--     <input type="button" class="Button" name="rename" value="Rename Comparision" onclick="return MakeVisible('renamediv')" style="width:140px;" visible="false"  />--%>
                </td>
            </tr>
            <tr id="buttontr11" runat="server">
                <td>
                    <div id="renamediv" style="display: none">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtrename" runat="server" CssClass="Button"></asp:TextBox>
                                    <br />
                                </td>
                                <td>
                                    <asp:Button ID="RenameButton" runat="server" CssClass="Button" Text="Rename Comparision"
                                        CausesValidation="False" />
                                    <br />
                                </td>
                                <td>
                                    <input type="button" class="Button" name="cancle" value="Cancle" onclick="return MakeInVisible('renamediv')" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:Label ID="RenameError" runat="server" Visible="false" ForeColor="red"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="Comparison_GrpSel" cellpadding="0" cellspacing="0" border="0" style="width: 648px; padding-left: 10px;">
            <tr>
                <td>Select Group:
                    <asp:LinkButton ID="lnkSelGrpE3" runat="server" Text="Nothing Selected" OnClientClick="return ShowGroupPopup('Popup/GroupSelection.aspx');"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td style="width: 600px; padding-left: 15px;">If group is selected then you can see only cases that are connected with selected group(s).
                    <br />
                    You can select more than one group. 
                </td>
            </tr>
        </table>
        <table class="Comparison" cellpadding="0" cellspacing="0" border="0" style="width: 648px; padding-left: 10px;">
            <tr>
                <td style="width: 600px; padding-top: 5px;" class="PageHeading">Create a New Comparison</td>
            </tr>
            <tr>
                <td style="width: 600px; padding-left: 15px;">Hold control key to select multiple cases.
                    <br />
                    Maximum 10 cases per comparison
                </td>
            </tr>
            <tr style="height: 20px">
                <td align="right" valign="top">
                    <asp:Label ID="CreateCompError" runat="server" Visible="false" ForeColor="red"></asp:Label>
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 600px">
                    <asp:ListBox CssClass="DropDown" ID="CaseComp" EnableViewState="true" runat="server"
                        SelectionMode="Multiple" Width="552px" Height="193px"></asp:ListBox>
                </td>
            </tr>
            <tr style="height: 50px">
                <td style="width: 600px">
                    <input class="Button" type="button" value="Create Comparison" onclick="return MakeVisible('newcomparisiondiv')" />
                    <input class="ResetButton" type="reset" value="Reset" />
                    <asp:Button ID="btnView" runat="server" ForeColor="Blue" Text="View" Enabled="false" OnClientClick="return displayView();" />
                </td>
            </tr>
            <tr>
                <td>
                    <div id="newcomparisiondiv" style="display: none">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="ComparisonName" runat="server" CssClass="Button" MaxLength="50"></asp:TextBox>
                                    <br />
                                </td>
                                <td>
                                    <asp:Button ID="StartComp" runat="server" CausesValidation="true" CssClass="Button" OnClientClick="return Message();"
                                        Text="Create Comparison" />
                                    <br />
                                </td>
                                <td align="left">
                                    <input type="button" class="Button" name="cancle" value="Cancel" onclick="return MakeInVisible('newcomparisiondiv')" />
                                    <br />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
        </table>
        <table class="Comparison" cellpadding="0" cellspacing="0" border="0" style="width: 648px; padding-left: 10px;">
            <tr>
                <td style="padding-left: 10px;" class="PageHeading">Share an Existing Comparison</td>
            </tr>
            <tr id="nodatatr2" runat="server">
                <td style="font-size: 15px; font-weight: bold; padding-left: 10px;">&nbsp;There is no Proprietary Comparisons please start with new comaprison
                    <br />
                </td>
            </tr>
            <tr id="headingtr1" runat="server">
                <td style="font-size: 15px">&nbsp;&nbsp;Existing Comparisons</td>
            </tr>
            <tr id="ddtr2" runat="server">
                <td style="height: 22px">
                    <asp:DropDownList ID="SharedComp" CssClass="DropDown" runat="server" Width="548px">
                    </asp:DropDownList>
                    <br />
                </td>
            </tr>
            <tr id="headingtr2" runat="server">
                <td style="font-size: 15px">&nbsp;&nbsp;Coworker with whom to share:</td>
            </tr>
            <tr id="ddtr3" runat="server">
                <td>
                    <asp:DropDownList ID="Coworker" CssClass="DropDown" runat="server" Width="250px">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr style="height: 50px" id="buttontr3" runat="server">
                <td style="height: 50px">
                    <asp:Button ID="SharedButton" Text="Share Comparison" CssClass="Button" runat="server"
                        CausesValidation="False" /></td>
            </tr>
        </table>
                                    </ContentTemplate>

</ajaxToolkit:TabPanel>
 <ajaxToolkit:TabPanel runat="server" ToolTip="Result analysis"
        ID="TabPanel1" TabIndex="1">
        <HeaderTemplate>
            Result Analysis
        </HeaderTemplate>
            <ContentTemplate>
                <asp:LinkButton ID="lnknew" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLM.aspx','_blank')">Revenue Price by weight</asp:LinkButton>
                <br />

                <asp:LinkButton ID="LinkButton1" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMUnit.aspx','_blank')">Revenue Price by Unit</asp:LinkButton>
            <br />

            <asp:LinkButton ID="LinkButton2" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMQ.aspx','_blank')">Revenue Price by weight Quarterly</asp:LinkButton>
                <br />

                <asp:LinkButton ID="LinkButton3" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMUnitQ.aspx','_blank')">Revenue Price by Unit Quarterly</asp:LinkButton>
<br/>
<asp:LinkButton ID="lnknewD" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMDep.aspx','_blank')">Revenue Price With Depreciation by weight</asp:LinkButton>
                <br />

                <asp:LinkButton ID="LinkButton1D" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMUnitDep.aspx','_blank')">Revenue Price With Depreciation by Unit</asp:LinkButton>
            <br />

            <asp:LinkButton ID="LinkButton2D" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMQDep.aspx','_blank')">Revenue Price With Depreciation by weight Quarterly</asp:LinkButton>
                <br />

                <asp:LinkButton ID="LinkButton3D" Style="font-size: 14px" runat="server"  
            OnClientClick="window.open('../Econ3/Results/ResultPLMUnitQDep.aspx','_blank')">Revenue Price With Depreciation by Unit Quarterly</asp:LinkButton>


        </ContentTemplate>
        </ajaxToolkit:TabPanel> 
</ajaxToolkit:TabContainer>
</td> 
</tr>

        </table>
        </ContentTemplate> 
        </asp:UpdatePanel>  
       <asp:HiddenField ID="hidselgrpID" runat="server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
       
    </div>
</asp:Content>
