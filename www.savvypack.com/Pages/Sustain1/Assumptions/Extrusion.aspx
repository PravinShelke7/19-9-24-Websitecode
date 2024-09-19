<%@ Page Language="VB" MasterPageFile="~/Masters/Sustain1.master" AutoEventWireup="false"
    CodeFile="Extrusion.aspx.vb" Inherits="Pages_Sustain1_Assumptions_Extrusion"
    Title="S1-Material Assumptions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Sustain1ContentPlaceHolder" runat="Server">
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/S1Barr.js"></script>
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <style type="text/css">
        a.LinkMatNew:link
        {
            color: White;
            font-family: Optima;
            font-size: 10px;
            font-weight: bold;
            text-decoration: underline;
        }
        a.LinkMatNew:visited
        {
            color: White;
            font-family: Optima;
            font-size: 10px;
            font-weight: bold;
            text-decoration: underline;
        }
        a.LinkMatNew:hover
        {
            color: Red;
            font-size: 10px;
            font-weight: bold;
        }
        a.LinkNewMat:link
        {
            color: Black;
            font-family: Optima;
            font-size: 12px;
            text-decoration: underline;
        }
        a.LinkNewMat:visited
        {
            color: Black;
            font-family: Optima;
            font-size: 12px;
            text-decoration: underline;
        }
        
        a.LinkNewMat:active
        {
            color: red;
            font-family: Optima;
            font-size: 12px;
        }
        a.LinkNewMat:focus
        {
            color: Red;
            font-family: Optima;
            font-size: 12px;
        }
    </style>
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
        function ShowPopMatWindow(Page) {
            newwin = window.open(Page);
        }

        //Edit Material
        function ShowEditPopWindow(Page, Id) {

            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 550;
            var height = 200;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
			var Mid = document.getElementById(Id).value;
            Page = Page + "&MatId=" + Mid
            newwin = window.open(Page, 'Chat', params);
        }

        //Get Material
        function ShowMatPopWindow(el, Page, Id) {

            // find all elements that have the linkActive class
            var elems = document.querySelectorAll(".linkActive");

            // loop through them and ...
            for (var i = 0; i < elems.length; i++) {
                // remove the linkActive class
                elems[i].classList.remove('linkActive');
                elems[i].style.color = 'Black';
            }

            // now add the class to the link that was just clicked
            el.classList.add('linkActive');
            // elems[i].style.color = 'red';
            el.style.color = 'red';



            var width = 550;
            var height = 540;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=no';
            params += ', scrollbars=no';
            params += ', status=yes';
            params += ', toolbar=no';
			var Mid = document.getElementById(Id).value;
            Page = Page + "&MatId=" + Mid
            newwin = window.open(Page, 'Chat', params);
			return false;
        }

           
    </script>
    <script type="text/javascript">
        function CallBackBarrier() {
            var msgBarr = ""
            var msgBarr1 = ""

            if (document.getElementById("ctl00_Sustain1ContentPlaceHolder_hidBarrier").value == "0") {
                document.getElementById("ctl00_Sustain1ContentPlaceHolder_hidBarrier").value = "1";
                document.getElementById('ctl00_imgBarProp').title = "Hide Barrier Properties";

                showDisplay();

                msgBarr = document.getElementById("ctl00_Sustain1ContentPlaceHolder_hidMessageBarr").value;
                msgBarr = msgBarr.split("#").join("\n")
                if (msgBarr != "") {
                    alert(msgBarr);
                }
            }
            else {
                document.getElementById("ctl00_Sustain1ContentPlaceHolder_hidBarrier").value = "0";
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
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeHeader");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeHeader2");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeHeader1");

            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeVal" + i);
                objDep.style.display = "none";
            }

            //OTR
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRHeader");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRHeader2");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRSHeader1");
            obj4 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRPHeader1");

            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";
            obj4.style.display = "none";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRSVal" + i);
                objDep.style.display = "none";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRPVal" + i);
                objDep.style.display = "none";
            }

            //WVTR
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRHeader");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRHeader2");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRSHeader1");
            obj4 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRPHeader1");

            obj1.style.display = "none";
            obj2.style.display = "none";
            obj3.style.display = "none";
            obj4.style.display = "none";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRSVal" + i);
                objDep.style.display = "none";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRPVal" + i);
                objDep.style.display = "none";
            }

            //OTR/WVTR TEMP and HUMIDITY  
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_tblBarrier");
            obj1.style.display = "none";

            //Total
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_Total5");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_Total7");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_Total3");
            //alert(obj1 + '-' + obj2 + '-' + obj3 );
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
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeHeader");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeHeader2");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeHeader1");

            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_GradeVal" + i);
                objDep.style.display = "";
            }

            //OTR
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRHeader");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRHeader2");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRSHeader1");
            obj4 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRPHeader1");

            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
            obj4.style.display = "";
            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRSVal" + i);
                objDep.style.display = "";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_OTRPVal" + i);
                objDep.style.display = "";
            }

            //WVTR
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRHeader");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRHeader2");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRSHeader1");
            obj4 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRPHeader1");

            obj1.style.display = "";
            obj2.style.display = "";
            obj3.style.display = "";
            obj4.style.display = "";

            var i;
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRSVal" + i);
                objDep.style.display = "";
            }
            for (i = 1; i <= 10; i++) {
                var objDep = document.getElementById("ctl00_Sustain1ContentPlaceHolder_WVTRPVal" + i);
                objDep.style.display = "";
            }


            //OTR/WVTR TEMP and HUMIDITY  
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_tblBarrier");
            obj1.style.display = "";
            //alert('Sud2.0');

            //Total 
            obj1 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_Total5");
            obj2 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_Total7");
            obj3 = document.getElementById("ctl00_Sustain1ContentPlaceHolder_Total3");
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
    <table cellpadding="0" cellspacing="0" style="text-align: center; padding-left: 55px;">
        <tr>
            <td>
                <asp:Table ID="tblTab" runat="server" CellPadding="0" CellSpacing="0">
                </asp:Table>
            </td>
        </tr>
    </table>
    <div id="PageSection1" style="text-align: left">
        <br />
        <table cellspacing="0" cellpadding="0" style="width: 100%;">
            <tr>
                <td align="left" valign="top">
                    <table id="tblBarrier" runat="server" style="display: none; height: 18px;">
                        <tr>
                            <td style="width: 223px; text-align: right;" title="Oxygen Transformation Rate">
                                <asp:Label ID="lblOTRTemp" runat="server" Style="font-family: Optima; font-size: 12px;
                                    height: 12px; width: 100px; margin-right: 9px; margin-top: 2px; margin-bottom: 2px;
                                    margin-left: 5px; text-align: right; font-weight: bold">OTR Temprature(°C):</asp:Label>
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
                                    margin-left: 5px; text-align: right; font-weight: bold">WVTR Temprature(°C):</asp:Label>
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
            <tr>
                <td valign="top">
                    <div id="divVariable" runat="Server">
                        <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </div>
                </td>
            </tr>
        </table>
        <div style="margin-left: 5px; margin-top: 5px;">
            <asp:Table ID="tblComparision1" runat="server" CellPadding="0" CellSpacing="2">
            </asp:Table>
        </div>
        <table cellspacing="4">
            <tr>
                <td valign="top">
                    <asp:Table ID="tblComparision2" runat="server" CellPadding="0" CellSpacing="2">
                    </asp:Table>
                </td>
                <td valign="top" align="left">
                    <asp:Table ID="tblComparision3" runat="server" CellPadding="0" CellSpacing="2">
                    </asp:Table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hidBarrier" runat="server" />
        <asp:HiddenField ID="OTRTEMP1" runat="server" />
        <asp:HiddenField ID="OTRTEMP2" runat="server" />
        <asp:HiddenField ID="RH1" runat="server" />
        <asp:HiddenField ID="RH2" runat="server" />
        <asp:HiddenField ID="hidMessageBarr" runat="server" />
    </div>
</asp:Content>
