<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetEquipmentPopupList.aspx.vb"
    Inherits="Pages_Econ1_PopUp_GetEquipmentPopUpList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Econ1-Equipment List</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            var txtarray = document.getElementsByTagName("input");
            var flag;
            if (txtarray.length != 0) {
                for (var i = 0; i < txtarray.length; i++) {
                    if (txtarray[i].type == "text") {
                        var id = txtarray[i].id;
                        $('#' + id).change(function () {
                            CheckSP();
                        });
                    }
                }
            }

            //To check Multiline Textbox
            var txtMularray = document.getElementsByTagName("textarea");
            if (txtMularray.length != 0) {
                for (var i = 0; i < txtMularray.length; i++) {
                    if (txtMularray[i].type == "textarea") {
                        var idMul = txtMularray[i].id;
                        $('#' + idMul).change(function () {
                            CheckSPMul("500");
                        });
                    }
                }
            }
        });
    </script>
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            font-size: 11px;
            margin-left: 0px;
        }

        a.LinkMat:link {
           color: Blue;
            font-family: Optima;
            font-size: 12px;
            text-decoration: underline;
        }

        a.LinkMat:active {
           color: Red;
            font-family: Arial Baltic;
            font-size: 12px;
        }

        a.LinkMat:visited {
            color: Blue;
            font-family: Optima;
            font-size: 12px;
            text-decoration: underline;
        }

        a.LinkMat:hover {
            color: Red;
            font-size: 12px;
        }
    </style>
    <script type="text/JavaScript">
        function rowno(rown) {
            alert(rown);
            return false;
        }
        function EquipmentDet(EquipDes, EquipId, EQUIPDES3) {
            var hidEqdes = document.getElementById('<%= hidEqdes.ClientID%>').value;
            var hidEid = document.getElementById('<%= hidEid.ClientID%>').value;
            var EqName = document.getElementById('<%= hidNew.ClientID%>').value;
            var i = hidEid;
            i = i.match(/\d+$/)[0];
//            if (EqName == "") {
//                if (EquipDes.length > 38) {
//                    var Name = EquipDes.substring(0, 20);
//                    var Name1 = EquipDes.substring(20, 38);
//                    Name = Name.concat(" ", Name1, "...");
//                }
//                else {
//                    var Name = EquipDes.substring(0, 20);
//                    var Name1 = EquipDes.substring(20, 38);
//                    Name = Name.concat(" ", Name1);
//                }
//                if (EQUIPDES3 != "") {
//                    window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
//                }
//                else {
//                    window.opener.document.getElementById(hidEqdes).innerText = Name;
//                }
//                window.opener.document.getElementById(hidEid).value = EquipId;
//            }
//            else {
//                if (EqName.length > 38) {
//                    var Name = EqName.substring(0, 20);
//                    var Name1 = EqName.substring(20, 38);
//                    Name = Name.concat(" ", Name1, "...");
//                }
//                else {
//                    var Name = EqName.substring(0, 20);
//                    var Name1 = EqName.substring(20, 38);
//                    Name = Name.concat(" ", Name1);
//                }
//                if (EQUIPDES3 != "") {
//                    window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
//                }
//                else {
//                    window.opener.document.getElementById(hidEqdes).innerText = Name;
//                }
//                window.opener.document.getElementById(hidEid).value = EquipId;
            //            }
            if (EQUIPDES3 != "") {
                window.opener.document.getElementById(hidEqdes).innerText = EQUIPDES3.replace(new RegExp("&#", 'g'), "'");
            }
            else {
                window.opener.document.getElementById(hidEqdes).innerText = EquipDes;

            }
            window.opener.document.getElementById(hidEid).value = EquipId;
            if (document.getElementById('<%= hidMod.ClientID%>').value == 'E1') {
                if (EquipId == "0") {
                    window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                    window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
                }
                else {
                    if (EQUIPDES3 != "") {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "inline";
                    }
                    else {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "inline";
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                    }
                }
            }
            document.getElementById('<%= hidEqdes.ClientID%>').value = '';
            document.getElementById('<%= hidEid.ClientID%>').value = '';
            document.getElementById('<%= hidNew.ClientID%>').value = '';
            window.close();
        }
    </script>
    <script type="text/javascript">

        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-16991293-1']);
        _gaq.push(['_trackPageview']);

        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') +
                '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();

    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" EnablePartialRendering="true">
        </asp:ScriptManager>
         <div id="ContentPagemargin" style="width: 980px; height: 160px; margin: 0 0 0 0">
            <div id="PageSection1" style="text-align: left; margin-left: 2px; margin-right: 2px; margin-bottom: 7px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <div style="margin-left: 170px">
                <table cellpadding="0" cellspacing="2" style="margin-left: 220px">
                <tr>
                     <td align="right">
                      <b>Keyword</b>
                     </td>
                     <td>:
                     </td>
                     <td style="width: 200px;">
                     <asp:TextBox ID="txtMatDe1" runat="server" CssClass="LongTextBoxSearch" Style="text-align: left"
                            Width="200px"></asp:TextBox>
                            <div id="gridMsg" runat="server" visible="false">
                                <asp:Label ID="lblEq" runat="server" Text="No Record Found"></asp:Label>
                             </div>
                     </td>
                     <td>
                      <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 5px" />
                       </td>
                 </tr>
                    <tr>
                      <td colspan="4">
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                          </td>
                    </tr>
                </table>
                </div>
                <table style="width: 800px">
                    <tr>
                        <td style="width: 25%">
                            <asp:LinkButton ID="lnkGroup" runat="server" Style="color: Blue; text-align: center; font-size: small;"></asp:LinkButton>
                            <div style="height: 540px; width: 150px; overflow: auto;">
                                <asp:Table ID="tblMaterials" runat="server" CellPadding="0" CellSpacing="2">
                                </asp:Table>
                            </div>
                        </td>
                        <td align="right" style="width: 40%; margin-top: 5px;">
                            <table>
                                <tr>
                                    <td colspan="4">
                                        <div id="grid" runat="Server" style="width: 800px; height: 550px; overflow: auto; margin-right: 20px; margin-top: 10px;">
                                            <headerstyle backcolor="Black" forecolor="White" cssclass="FixedHeader" width="1000px" />
                                            <asp:GridView ID="grdMaterials" runat="server" AutoGenerateColumns="false">
                                                <RowStyle CssClass="AlterNateColor1" />
                                                <AlternatingRowStyle CssClass="AlterNateColor2" />
                                                <HeaderStyle BackColor="Black" ForeColor="White" />
                                            </asp:GridView>
                                        </div>
                                     
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <table cellspacing="0">
                </table>
                <br />
            </div>
            <asp:HiddenField ID="hidEqdes" runat="server" />
            <asp:HiddenField ID="hidNew" runat="server" />
            <asp:HiddenField ID="hidEqid" runat="server" />
            <asp:HiddenField ID="hidMod" runat="server" />
            <asp:HiddenField ID="hidEid" runat="server" />
            <asp:HiddenField ID="hidMatGrp" runat="server" />
            <asp:HiddenField ID="hidMatVal" runat="server" />
            <asp:HiddenField ID="hvUserGrd" runat="server" />
            <asp:HiddenField ID="hvGroupQ" runat="server" />
            <asp:HiddenField ID="hidGroup" runat="server" />
        </div>
        <asp:Button ID="btnCall" Text="" Width="1px" runat="server" CssClass="Button" Style="margin-left: 5px; display: none;" />
    </form>
</body>
</html>
