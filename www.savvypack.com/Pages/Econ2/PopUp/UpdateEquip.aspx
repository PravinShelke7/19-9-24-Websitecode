<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UpdateEquip.aspx.vb"
    Inherits="Pages_Econ2_PopUp_UpdateEquip" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>E1-Edit Material Name</title>
    <meta http-equiv="X-UA-Compatible" content="IE=edge,chrome=1" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script src="../../../JavaScripts/jquery-1.4.1.js" type="text/javascript"></script>
    <script src="../../../JavaScripts/SpecialCharacters.js" type="text/javascript"></script>
    <style type="text/css">
        .MatSmallTextBox
        {
            font-family: Verdana;
            font-size: 10px;
            height: 15px;
            width: 50px;
            background-color: #FEFCA1;
            margin-top: 2px;
            margin-bottom: 2px;
            margin-left: 2px;
            margin-right: 2px;
            border-right: #7F9DB9 1px solid;
            border-top: #7F9DB9 1px solid;
            border-left: #7F9DB9 1px solid;
            border-bottom: #7F9DB9 1px solid;
            text-align: left;
        }
    </style>
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
                            CheckSPMul("790");
                        });
                    }
                }
            }
        });               
    </script>
    <script type="text/JavaScript">

        function Count(text) {
            //asp.net textarea maxlength doesnt work; do it by hand
            var maxlength = 790; //set your value here (or add a parm and pass it in)
            var object = document.getElementById(text.id)  //get your object
            var flag = CheckDesc(text.id, 'Text1');
            if (flag) {
                if (object.value.length > maxlength) {
                    object.focus(); //set focus to prevent jumping
                    object.value = text.value.substring(0, maxlength); //truncate the value
                    object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                    return false;
                }
                else {
                    return true;
                }
            }
        }

        function CloseWindow() {
            var hidEqLname = document.getElementById('<%= lblName.ClientID%>').innerHTML;
            var hidEqName = document.getElementById('<%= txtName.ClientID%>').value;
            var hidEqDes1 = document.getElementById('<%= hidEqDes1.ClientID%>').value;
            var i = hidEqDes1;
            i = i.match(/\d+$/)[0];

            if (hidEqName != "") {
                if (hidEqName.length > 38) {
                    var Name = hidEqName.substring(0, 20);
                    var Name1 = hidEqName.substring(20, 33);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = hidEqName.substring(0, 20);
                    var Name1 = hidEqName.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                window.opener.document.getElementById(hidEqDes1).innerHTML = Name;
                window.opener.document.getElementById(hidEqDes1).title = Name;
                window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + i).style.display = "inline";
                window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + i).style.display = "none";
                for (j = 1; j < 31; j++) {
                    var hidAssetId = window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_hidAssetId' + j).value;

                    if (j != i) {
                        if (document.getElementById('<%= hidAssetId.ClientID%>').value == hidAssetId) {
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_hypAssetDes' + j).innerHTML = Name;
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_hypAssetDes' + j).title = Name;
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + j).style.display = "inline";
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + j).style.display = "none";                           
                                                     
                        }
                    }
                }
            }
            else {
                if (hidEqLname.length > 38) {
                    var Name = hidEqLname.substring(0, 20);
                    var Name1 = hidEqLname.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
                    var Name = hidEqLname.substring(0, 20);
                    var Name1 = hidEqLname.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                window.opener.document.getElementById(hidEqDes1).innerHTML = Name;
                window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + i).style.display = "none";
                window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + i).style.display = "inline";                           
                           
                for (j = 1; j < 31; j++) {
                    var hidAssetId = window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_hidAssetId' + j).value;
                    if (j != i) {
                        if (document.getElementById('<%= hidAssetId.ClientID%>').value == hidAssetId) {
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_hypAssetDes' + j).innerHTML = Name;
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgBut' + j).style.display = "none";
                            window.opener.document.getElementById('ctl00_Econ2ContentPlaceHolder_imgDis' + j).style.display = "inline";                           
                                                      
                        }
                    }
                }
            }
            document.getElementById('<%= lblName.ClientID%>').innerHTML = '';
            document.getElementById('<%= txtName.ClientID%>').value = '';
            window.close();
        }
                

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left">
            <div id="divMainHeading" style="text-align: center; font-weight: bold; font-size: large;">
                Edit Equipment
            </div>
            </br>
            <div id="div1" style="text-align: center; margin-left: 80px;">
                <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
                <table cellpadding="0" cellspacing="2" width="360px">
                    <tr>
                        <td align="left" class="AlterNateColor2">
                            <b>Existing Equipment Name:</b>
                        </td>
                        <td align="left" class="AlterNateColor1">
                            <asp:Label ID="lblName" runat="server" Font-Bold="true" Font-Size="15px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="AlterNateColor2">
                            <b>New Equipment Name:</b>
                        </td>
                        <td align="left" class="AlterNateColor1">
                            <asp:TextBox ID="txtName" runat="server" CssClass="MatSmallTextBox" Width="200px"
                                MaxLength="100"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td align="left">
                            <asp:Button ID="btnSubmit" Text="Submit" runat="server" CssClass="Button" Style="margin-left: 0px" />
                        </td>
                    </tr>
                </table>
            </div>
            <br />
        </div>
        <asp:Button ID="btnCall" runat="server" Style="margin-left: 5px; display: none;" />
        <asp:HiddenField ID="hidEqdes" runat="server" />
        <asp:HiddenField ID="hidEqName" runat="server" />
        <asp:HiddenField ID="hidAssetId" runat="server" />
        <asp:HiddenField ID="hidEqDes1" runat="server" />
        <asp:HiddenField ID="hidId" runat="server" />
       
    </div>
    </form>
</body>
</html>
