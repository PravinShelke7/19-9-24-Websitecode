<%@ Page Title="E1-Preferences" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="Preferences.aspx.vb" Inherits="Pages_Econ1_Assumptions_Preferences" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript">

        function FromDate(effdate, effdattefrm) {

            var todate = document.getElementById("ctl00_Econ1ContentPlaceHolder_ddlEffdate");
            var effdate = todate.options[todate.selectedIndex].text
            var fromdate = document.getElementById("ctl00_Econ1ContentPlaceHolder_ddlEffdatefrom");
            var effdatefrom = fromdate.options[fromdate.selectedIndex].text

            var toeff = new Date(effdate);
            var fromeff = new Date(effdatefrom);


            if (toeff < fromeff) {
                // alert('1');
                alert("From Date must be less than or equal to To Date.");
                return false;
            }

            else {
                //    alert('2');
                return true;
            }



        }
    </script>
    <script type="text/JavaScript">
        function ShowBulkMMPopWindow(Page) {
            document.getElementById("ctl00_Econ1ContentPlaceHolder_lnkSelBulkModel").style.display = "none";
            var SCaseID = "<%=Session("E1CaseId")%>";
            var width = 550;
            var height = 400;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            Page = Page + "?PageNm=Preferences&SCaseID=" + SCaseID;
            newwin = window.open(Page, 'BulkTool', '');
            return false;
        }
    </script>
    <style type="text/css">
        .divUpdateprogress {
            left: 410px;
            top: 200px;
            position: absolute;
        }
    </style>
    <div id="ContentPagemargin" runat="server">
        <div style="margin: 0px 200px 5px 5px; text-align: right">
            <asp:HyperLink ID="lnkAPref" runat="server" Text="Additional Preferences" CssClass="Link" Target="_blank"
                Font-Bold="true" NavigateUrl="~/Pages/Econ1/Assumptions/APreferences.aspx"></asp:HyperLink>
        </div>
        <div id="PageSection1" style="text-align: left">
            <div style="text-align: left; vertical-align: top;">
                <asp:LinkButton ID="lnkSelBulkModel" runat="server" Text="Bulk Transfer" Visible="false" OnClientClick="return ShowBulkMMPopWindow('../BulkModelManagement.aspx');"></asp:LinkButton>
            </div>
            <div id="loading" runat="server" class="divUpdateprogress" style="display: none;">
                <img id="loading-image" src="../../../Images/Loading2.gif" alt="Loading..." width="100px"
                    height="100px" />
            </div>
            <br />
            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Country Selections
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td style="width: 125px">Country of Manufacture:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlMfg" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                    </td>
                </tr>

                <tr class="AlterNateColor1">
                    <td style="width: 125px">Country of Destination:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlDesti" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                    </td>
                </tr>
            </table>
            <br />
            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Preferred Units
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdEnglish" GroupName="Unit" runat="server" Text="English units" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdMetric" GroupName="Unit" runat="server" Text="Metric units" />
                    </td>
                </tr>

            </table>
            <br />
            <%--<table width="60%">
                    <tr class="AlterNateColor4">
                        <td class="PageSHeading" style="font-size:14px;" colspan="2">
                            Date</td>
                    </tr>
                      <tr class="AlterNateColor1">
                        <td colspan="2">
                            <asp:DropDownList ID="ddlEffdate" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                        </td>                        
                     </tr> 
                                            
                    
                 </table>--%>
            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Date</td>
                </tr>
                <tr class="AlterNateColor1" runat ="server" visible ="false" >
                    <td style="width: 75px">From Date:
                    </td>
                    <td colspan="2">
                        <asp:DropDownList ID="ddlEffdatefrom" CssClass="DropDown" Width="125px" runat="server" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                   
                    <td colspan="3">
                        <asp:DropDownList ID="ddlEffdate" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                    </td>
                </tr>


            </table>
            <br />
            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Preferred Currency
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:DropDownList ID="ddlCurrancy" CssClass="DropDown" Width="125px" runat="server"></asp:DropDownList>
                    </td>
                </tr>


            </table>
            <br />


            <table width="60%">
                <tr class="AlterNateColor4">
                    <td class="PageSHeading" style="font-size: 14px;" colspan="2">Discrete Calculations
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdNewDis" GroupName="DISCRETE" runat="server" Text="Shipped together" />
                    </td>
                </tr>
                <tr class="AlterNateColor1">
                    <td colspan="2">
                        <asp:RadioButton ID="rdOldDis" GroupName="DISCRETE" runat="server" Text="Shipped separately" />
                    </td>
                </tr>

            </table>
            <br />
        </div>
        <asp:Button ID="btnUpdateBulk" runat="server" Style="display: none;" />
        <asp:Button ID="btnUpdateBulk1" runat="server" Style="display: none;" />
    </div>
</asp:Content>

