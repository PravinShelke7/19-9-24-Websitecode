<%@ Page Title="E1-Material and Structure Results" Language="VB" MasterPageFile="~/Masters/Econ1.master" AutoEventWireup="false" CodeFile="ExtrusrionOut.aspx.vb" Inherits="Pages_Econ1_IntResults_ExtrusrionOut" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Econ1ContentPlaceHolder" Runat="Server">
    
    <script type="text/JavaScript" src="../../../JavaScripts/collapseableDIV.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/wz_tooltip.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/tip_balloon.js"></script>
    <script type="text/JavaScript" src="../../../JavaScripts/E1Comman.js"></script>
     
    <script type="text/JavaScript">
        function checkNumericAllForResult() {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            var anum = /(^\d+$)|(^\d+\.\d+$)/
            for (var i = 0; i < txtarray.length; i++) {
                if (txtarray[i].type == "text") {
                    var id = txtarray[i].id;
                    //alert(txtarray[i].value);
                    // if (anum.test(txtarray[i].value.replace(/,/g, "")))
                    if (txtarray[i].value == "") {
                        flag = true;

                    }
                    else {
                        if (IsNumeric(txtarray[i].value.replace(/,/g, ""))) {

                            flag = true;
                        }
                        else {
                            inlineMsg(id, "Invalid Number");
                            flag = false;
                            break;
                        }

                    }

                }

            }

            return flag;
        }
    </script>
   
    <div id="ContentPagemargin" runat="server">
       <div id="PageSection1" style="text-align:left;width:1700px;"  >
             <br />
                <asp:Table ID="tblComparision" runat="server" CellPadding="0" CellSpacing="2"  ></asp:Table>
             <br />   
              <asp:Table ID="tblComparision2" runat="server" CellPadding="0" CellSpacing="2">
                                    </asp:Table>         
         </div>   
     </div>
</asp:Content>

