<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AddPriceOption.aspx.vb" Inherits="Pages_SavvyPackPro_Popup_AddPriceOption" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Add Price Option</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <script type="text/JavaScript">
        javascript: window.history.forward(1);
        function ClosePage() {
            window.opener.document.getElementById('btnRefreshP').click();
        }
        function ClosePage1() {
            window.opener.document.getElementById('btnRefreshP').click();
        }
        function ClosePopup() {
            //alert("hii");
            window.opener.document.getElementById('btnRefreshP').click();
            window.opener
            window.close();
        
           
        }

        function CheckSP(text,hidvalue) {
            //alert(text);

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
//            else {
//                document.getElementById('btnrefreshSeq').click();
//                return true;

//            }
        }

//        function CheckSP(text, hidvalue) {
//            var sequence = document.getElementById(text.id).value;
//           

//            var a = /\<|\>|\&#|\\/;
//            var object = document.getElementById(text.id)//get your object
//            alert(object);
//            if ((document.getElementById(text.id).value.match(a) != null)) {

//                alert("You cannot use the following COMBINATIONS of characters:< > \\  &# in Search Text. Please choose alternative characters.");
//                object.focus(); //set focus to prevent jumping
//                object.value = text.value.replace(new RegExp("<", 'g'), "");
//                object.value = text.value.replace(new RegExp(">", 'g'), "");
//                object.value = text.value.replace(/\\/g, '');
//                object.value = text.value.replace(new RegExp("&#", 'g'), "");
//                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
//                return false;
//            }
//            else {
//                //document.getElementById('btnSeq').click();
//                return true;
//            }
//        }

    </script>
    
</head>
<body>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="scrpt1" runat="server">
    </asp:ScriptManager>
    <asp:Image ImageAlign="AbsMiddle" Width="900px" Height="55px" ID="Image1" ImageUrl="~/Images/SavvyPackPRO.gif"
        runat="server" />
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
   <table class="ContentPage" id="Table1" runat="server" style="margin-top: 15px; width: 900px;">
                                            <tr id="Tr2" runat="server">
                                                <td id="Td2" runat="server">
                                                    <div class="PageHeading" id="div3" runat="server" style="text-align: center;">
                                                        Set Price Option
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr id="Tr1" style="height: 20px" runat="server">
                                                <td id="Td1" runat="server">
                                                    <div id="Div1" runat="server">
                                                        <div id="Div4" style="text-align: left; height: 400px; width: 800px;">
                                                            <div id="Div2">
                                                                <asp:Label ID="Label1" runat="server" CssClass="Error"></asp:Label>
                                                            </div>
                                                            <div id="PageSection1" style="text-align: left;">
                                                                <table style="width: 100%">
                                                                    <tr>
                                                                        <tr>
                                                                    <td style="width: 20%;">
                                                                        <b>Price Option Name:</b>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtPriceOption" runat="server" CssClass="MediumTextBox" Style="text-align: left;
                                                                            width: 250px" MaxLength="25" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                    </tr>
                                                                </table>
                                                                <asp:Table Width="100%" ID="tblEditQ" runat="server">
                                                                </asp:Table>
                                                                <table style="width: 100%">
                                                                 <tr>
                                                                        <td style="width: 20%;">
                                                                        </td>
                                                                        <td style="width: 80%;">
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="width: 20%;">
                                                                            <asp:Button ID="btnPriceOption" runat="server" Text="Submit"  />
                                                                        
                                                                        </td>
                                                                        <td style="width: 80%;">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>     
                                        <asp:Button ID="btnRefreshP" runat="server" Style="display: none;" />                     
    </form>
</body>
</html>
