<%@ Page Language="VB" MasterPageFile="~/Masters/SavvyPackMenu.master" AutoEventWireup="false"
    CodeFile="ContactUsN.aspx.vb" Inherits="ContactUsN" Title="Contact Us" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        function AlertMessage(type) {
            //var userId = document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value;
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

            if (userId != "") {
                msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                if (type == "L") {
                    ShowPopWindow("Users_Login/LoginS.aspx");
                }
                return true;
            }
        }
        function ConfirmMessage() {
            //var userId= document.getElementById('ctl00$ContentPlaceHolder1$hdnUserId').value; 
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;
            var txtQues = document.getElementById('<%= txtQuestion.ClientID%>').value;
            if (txtQues == "") {
                msg = "------------------------------------------------------\n Please enter question for submission\n------------------------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                if (userId != "") {
                    return true;
                }
                else {
                    //                    msg = "-------------------------------------------------------------------------------\n To submit your question,you must first login.\n If you do not have an account, please create an account using Login link.\n--------------------------------------------------------------------------------\n";
                    //                    alert(msg);
                    //OpenLoginPopup('N');
					OpenLoginPopup('YC');
                    return false;
                    //                var answer = confirm(msg);
                    //               
                    //                if(answer==true)
                    //                {   
                    //                        newwin = window.open('Users_Login/Login.aspx');    
                    //                  return false;
                    //                }
                    //                else if(answer==false)
                    //                {       
                    //                  return false;
                    //                }
                }
            }


        }
        function openwindow1() {
            //alert(document.getElementById('ctl00_ContentPlaceHolder1_lnkLogin').Enabled);
            window.open('Users_Login/Login.aspx');
            return false;
        }
        function openwindow2() {
            //alert(document.getElementById('ctl00_ContentPlaceHolder1_lnkCreate'));
            var userId = document.getElementById('<%=hdnUserId.ClientID%>').value;

            if (userId != "") {
                msg = "------------------------------------\n You are already logged in.\n------------------------------------\n";
                alert(msg);
                return false;
            }
            else {
                window.open('Users_Login/AddEditAccount.aspx?Acc=Y');
            }

        }

        function ShowPopWindow(Page) {
            //window.open('ItemSearch.aspx', 'ItemSearch', 'status=0,toolbar=0,location=0,menubar=0,directories=0,resizable=0,scrollbars=0,height=400,width=600');  
            var width = 700;
            var height = 300;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            newwin = window.open(Page, 'Chat', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }

        function CheckSP(text) {

            var a = /\<|\>|\&#|\\/;
            var object = document.getElementById(text.id)//get your object
            if ((document.getElementById(text.id).value.match(a) != null)) {

                alert("You cannot use the following Characters: < > \\ \nYou cannot use the following Combination: &# \nPlease choose alternative characters or combination.");
                object.focus(); //set focus to prevent jumping
                object.value = text.value.replace(new RegExp("<", 'g'), "");
                object.value = text.value.replace(new RegExp(">", 'g'), "");
                object.value = text.value.replace(/\\/g, '');
                object.value = text.value.replace(new RegExp("&#", 'g'), "");
                object.scrollTop = object.scrollHeight; //scroll to the end to prevent jumping
                return false;
            }
        }

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
            newwin = window.open(Page, 'winName', params);            
        }        
	</script>
    <div style="height: 22px; width: 97.5%; font-weight: bold; font-size: 23px; text-align: center;
        margin-top: 2px; margin-left: 5px; color: #825f05;">
        Contact Us
    </div>
    <div id="error">
        <asp:Label ID="lblError" runat="server" CssClass="Error"></asp:Label>
    </div>
    <div id="ContentPagemargin1" runat="server" style="vertical-align: top; margin-left: 7px;
        margin-right: 7px;">
        <table cellspacing="0" cellpadding="5">
            <tr>
                <td colspan="2" style="height: 20px; color: Red; font-size: 14px; vertical-align: top;
                    font-weight: bold; font-family: Arial">
                    <asp:Label ID="lblMessage" runat="Server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="font-weight: bold; color: Black; font-family: Optima; font-size: 15px;
                    text-align: justify" colspan="2">
                    Thank you for visiting our web site. We welcome your inquiries, so please feel free
                    to contact us by submitting your comments or questions.
                </td>
            </tr>
            <tr>
                <td style="color: Black; font-family: Verdana; font-size: 12px; text-align: justify">
                    <div id="divLog" runat="server" visible="true">
                        To submit your question,you must first login.
                        <%--<a onclick="return AlertMessage('L');" id="lnkCreate"
                        style="font-style: normal; font-family: Verdana; font-size: 12px;" class="Link"
                        href="#">login</a>.--%><br />
                        If you do not have an account, please create an account using Login link.
                        <%--<asp:LinkButton ID="LinkButton1" runat="Server" Text="create an account " OnClientClick="return openwindow2();"
                        CssClass="Link"></asp:LinkButton>
                    then <a onclick="return AlertMessage('L');" style="font-style: normal; font-family: Verdana;
                        font-size: 12px;" class="Link" href="#">login</a>.--%>
                    </div>
                </td>
                <%-- <td style="color:Black ;font-family:Verdana;font-size:12px;text-align:justify">
                    To submit your question,you must first  <asp:LinkButton ID="lnkLogin" runat="Server" Text="login" OnClientClick="return openwindow1();" CssClass="Link"></asp:LinkButton>.<br />
                    If you do not have an account, please  <asp:LinkButton ID="lnkCreate" runat="Server" Text="create an account " OnClientClick="return openwindow2();" CssClass="Link"></asp:LinkButton>then  <asp:LinkButton ID="lnkLogin1" runat="Server" Text="login" CssClass="Link" OnClientClick="return openwindow1();"></asp:LinkButton>.
                  </td>--%>
            </tr>
            <tr>
                <td>
                    <span style="font-weight: bold; color: Black; font-family: Optima; font-size: 13px;">
                        Submit Questions </span>
                    <div id="dvQuestions" runat="Server">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtQuestion" runat="Server" TextMode="MultiLine" Rows="4" MaxLength="400"
                                        Width="300px" Style="font-size: 12px; font-family: Optima;" onchange="javascript:CheckSP(this);"></asp:TextBox>
                                    <%-- <asp:RequiredFieldValidator ID="rfvQuestion" runat="Server" ControlToValidate="txtQuestion" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSubmit" runat="Server" Text="Submit" ToolTip="Submit Question"
                                        OnClientClick="return ConfirmMessage();" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                </td>
                <td style="color: Black; font-family: Optima;" align="center" valign="top">
                    <div style="text-align: left; font-size: 14px; font-weight: bold; margin-top: 4px;">
                        <span style="margin-top: 4px">SavvyPack Corporation.</span><br />
                        <span style="margin-top: 4px">1850 East 121st Street</span><br />
                        <span style="margin-top: 4px">Suite 106B</span><br />
                        <span style="margin-top: 4px">Burnsville, MN 55337</span><br />
                        <span style="margin-top: 4px">[1] 952-405-7500</span><br />
                        <%--Email: <a style="TEXT-DECORATION: none;font-style:italic;font-weight:bold;font-size:16px " class="Link" href="mailto:sales@allied-dev.com">sales@allied-dev.com</a>--%>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="height: 15px" colspan="2">
                    <span style="font-weight: normal; color: Black; font-family: Verdana; font-size: 13px;">
                        SavvyPack Corporation is looking for packaging professionals to join our staff.
                        Please submit your resume <a style="font-style: normal; font-weight: bold; font-size: 13px;"
                            class="Link" href="mailto:vmw@allied-dev.com">here</a>. All submissions are
                        confidential.</span>
                </td>
            </tr>
            <tr>
                <td style="height: 15px" colspan="2">
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="hdnUserId" runat="Server" />
        <asp:Button ID="btnRefresh" runat="server" Style="display: none;" />
        <asp:HiddenField ID="hdnUserLoginPath" runat="server" />
    </div>
</asp:Content>
