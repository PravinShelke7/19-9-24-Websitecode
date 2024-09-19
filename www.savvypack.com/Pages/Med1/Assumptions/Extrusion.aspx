<%@ Page Title="Med1-Material And Structure" Language="VB" MasterPageFile="~/Masters/Med1.master" AutoEventWireup="false" CodeFile="Extrusion.aspx.vb" Inherits="Pages_MedEcon1_Assumptions_Extrusion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Med1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/javascript" src="../../../javascripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/Med1Barr.js"></script>
  
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
        function ShowModPopWindow(Page, modTypeId) {

            Page = Page + "&ModId=" + document.getElementById(modTypeId).value;
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
        function CallBackBarrier() {
            
            var msgBarr = ""
            var msgBarr1 = ""
            if (document.getElementById("ctl00_Med1ContentPlaceHolder_hidBarrier").value == "0") 
            {
                document.getElementById("ctl00_Med1ContentPlaceHolder_hidBarrier").value = "1";
                document.getElementById('ctl00_imgBarProp').title = "Hide Barrier Properties";
                showDisplay();
                msgBarr = document.getElementById("ctl00_Med1ContentPlaceHolder_hidMessageBarr").value;
                msgBarr = msgBarr.split("#").join("\n")
                if (msgBarr != "") 
                {
                    alert(msgBarr);                 
                }
            }
            else
            {
                document.getElementById("ctl00_Med1ContentPlaceHolder_hidBarrier").value = "0";
                document.getElementById('ctl00_imgBarProp').title = "Show Barrier Properties";
                showhide();
            }
            
            return false;
        }
       

        function showhide() {

            var obj1;
            var obj2;
            var obj3;
            var obj4;
            //Grade
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeHeader");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeHeader2");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeHeader1");

            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeVal" + i);
                objDep.style.display = "none";
            }

            //OTR
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRHeader");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRHeader2");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRSHeader1");
            obj4 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRPHeader1");

            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";
            obj4.style.display = "none";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRSVal" + i);
                objDep.style.display = "none";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRPVal" + i);
                objDep.style.display = "none";
            }

            //WVTR
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRHeader");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRHeader2");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRSHeader1");
            obj4 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRPHeader1");

            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";
            obj4.style.display = "none";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRSVal" + i);
                objDep.style.display = "none";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRPVal" + i);
                objDep.style.display = "none";
            }

            //OTR/WVTR TEMP and HUMIDITY  
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_tblBarrier");
            obj1.style.display = "none";

            //Total
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_Total5");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_Total7");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_Total3");
            // alert(obj1 + '-' + obj2 + '-' + obj3 );
            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";

        }
        function showDisplay() {

            var obj1;
            var obj2;
            var obj3;
            var obj4;
            //Grade
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeHeader");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeHeader2");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeHeader1");

            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_GradeVal" + i);
                objDep.style.display = "";
            }

            //OTR
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRHeader");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRHeader2");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRSHeader1");
            obj4 = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRPHeader1");

            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
            obj4.style.display = "";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRSVal" + i);
                objDep.style.display = "";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_OTRPVal" + i);
                objDep.style.display = "";
            }

            //WVTR
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRHeader");
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRHeader2");
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRSHeader1");
            obj4 = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRPHeader1");

            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
            obj4.style.display = "";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRSVal" + i);
                objDep.style.display = "";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Med1ContentPlaceHolder_WVTRPVal" + i);
                objDep.style.display = "";
            }

            //OTR/WVTR TEMP and HUMIDITY  
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_tblBarrier");
            obj1.style.display = "";

            //Total 
            obj1 = document.getElementById("ctl00_Med1ContentPlaceHolder_Total5");           
            obj2 = document.getElementById("ctl00_Med1ContentPlaceHolder_Total7");         
            obj3 = document.getElementById("ctl00_Med1ContentPlaceHolder_Total3");
           // alert(obj1 + '-' + obj2 + '-' + obj3 );
            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
       

        }
        function ShowGradePopWindow(Page, MatId) {

            var width = 760;
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
            var matId = document.getElementById(MatId).value;
            Page = Page + "&MatId=" + matId;

            newwin = window.open(Page, 'Chat', params);

        } 
           
    </script>
    <div id="ContentPagemargin" runat="server">
        <div id="PageSection1" style="text-align: left">
            <%--<div style="margin-left:10px;padding-top:5px;">
                <span class="PageSHeading">Date:</span><asp:Label ID="lblEffDate" CssClass="NormalLable" runat="server"></asp:Label>
            </div>--%>
            <br />
            <table cellspacing="10" style="margin-top:-25px">
                <tr>
                    <td style="width: 30%">
                        <table>
                            <tr>
                                  <td align="left">
                                    <table id="tblBarrier" runat="server" style="display:none;">
                                        <tr>
                                            <td style="width: 223px; text-align: right;" title="Oxygen Transformation Rate">
                                                <asp:Label ID="lblOTRTemp" runat="server" Style="font-family: Optima; font-size: 12px;
                                                    height: 12px; width: 100px; margin-right: 9px; margin-top: 2px; margin-bottom: 2px;
                                                    margin-left: 5px; text-align: right; font-weight: bold">OTR Temprature(�C):</asp:Label>
                                                &nbsp;<asp:TextBox ID="txtOTRTemp" runat="server" CssClass="MediumTextBox" Width="36px"></asp:TextBox>
                                            </td>
                                            <td style="width: 27px">
                                            </td>
                                            <td style="width: 523px; text-align: left;" title="Relative Humidity">
                                                <asp:Label ID="lblOTRHumidity" runat="server" Style="font-family: Optima; font-size: 12px;
                                                    height: 12px; width: 100px; margin-right: 0px; margin-top: 2px; margin-bottom: 2px;
                                                    margin-left: 5px; text-align: right; font-weight: bold">Relative Humidity(%):</asp:Label>
                                                <asp:TextBox ID="txtOTRHumidity" runat="server" CssClass="MediumTextBox" Width="35px"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="width: 223px; text-align: right;" title="Water Vapour Transmission Rate">
                                                <asp:Label ID="lblWVTRTemp" runat="server" Style="font-family: Optima; font-size: 12px;
                                                    height: 12px; width: 100px; margin-right: 1px; margin-top: 2px; margin-bottom: 2px;
                                                    margin-left: 5px; text-align: right; font-weight: bold">WVTR Temprature(�C):</asp:Label>
                                                <asp:TextBox ID="txtWVTRTemp" runat="server" CssClass="MediumTextBox" Width="36px"></asp:TextBox>
                                            </td>
                                            <td style="width: 27px">
                                            </td>
                                            <td style="width: 523px; text-align: left;" title="Relative Humidity">
                                                <asp:Label ID="lbWVTRHumidity" runat="server" Style="font-family: Optima; font-size: 12px;
                                                    height: 12px; width: 100px; margin-right: 0px; margin-top: 2px; margin-bottom: 2px;
                                                    margin-left: 5px; text-align: right; font-weight: bold">Relative Humidity(%):</asp:Label>
                                                <asp:TextBox ID="txtWVTRHumidity" runat="server" CssClass="MediumTextBox" Width="35px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" valign="top">
                        <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" valign="top">
                        <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%">
                        <table>
                            <tr>
                                <td style="width: 50%" valign="top">
                                    <asp:Table ID="tblComparision2" runat="server" CellPadding="0" CellSpacing="2">
                                    </asp:Table>
                                </td>
                                <td style="width: 30%" valign="top">
                                    <asp:Table ID="tblComparision3" runat="server" CellPadding="0" CellSpacing="2">
                                    </asp:Table>
                                </td>
                            </tr>
                        </table>
                        
                    </td>
                </tr>
            </table>
        </div>
        <asp:HiddenField ID="hidUpdate" runat="server" />
        <asp:HiddenField ID="hidBarrier" runat="server" />
            <asp:HiddenField ID="OTRTEMP1" runat="server" />    
                     <asp:HiddenField ID="OTRTEMP2" runat="server" />	      
                    <asp:HiddenField ID="RH1" runat="server" /> 	
                    <asp:HiddenField ID="RH2" runat="server" />
                     <asp:HiddenField ID="hidMessageBarr" runat="server" />
                         <asp:HiddenField ID="hidBarrVal" runat="server" />
    </div>
</asp:Content>

