<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GroupDetails.aspx.vb" Inherits="Pages_StandAssist_PopUp_GroupDetails" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Structure Details</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        
   .FixedHeader 
   {
                     
            position: absolute;           
            font-weight: bold;
            margin-left:0px;
            margin-right:35px;
            
    }   
</style>
    <script type="text/JavaScript">
    function clickButton(e, buttonid) {

            var bt = document.getElementById(buttonid);
            if (bt) {

                    if (event.keyCode == 13) {
                        document.getElementById(buttonid).focus();
                        // alert(buttonid);
                        //document.getElementById(buttonid).click();  

                    }
            }

        }
        
        function GroupSearch() {
            window.opener.document.getElementById('btnRefresh').click();
            window.close();
        }

        function SelectAllCheckboxes(spanChk) {

            var oItem = spanChk.children;
            var theBox = (spanChk.type == "checkbox") ?

              spanChk : spanChk.children.item[0];
            xState = theBox.checked;
            elm = theBox.form.elements;
            for (i = 0; i < elm.length; i++) {
                if (elm[i].type == "checkbox" && elm[i].id != theBox.id) {
                    if (elm[i].checked != xState)
                        elm[i].click();
                }
            }
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
    <div id="ContentPagemargin" style="width: 100%; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; width: 810px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <%--   <div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:650px;height:30px;">
                      Group Details
</div>--%>
            <table style="text-align: center;">
                <tr>
                    <td style="height: 10px;">
                    </td>
                </tr>
                <tr>
                    <td class="PageSHeading" style="font-size: 16px;" colspan="2">
                        <div style="width:1100px; text-align: center;">
                            Structure Details
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblmsg" runat="server" Visible="false" Width="700px" Style="text-align: center;
                            font-size: 16px; font-weight: bold; color: Red;"> </asp:Label>
                    </td>
                </tr>
                <tr>
                <td style="width:500px">
                  <asp:Label ID="lblKeyWord" runat="server" Text="Keyword:" CssClass="NormalLabel"
                                        Style="margin-left: 10px;" Font-Bold="true"></asp:Label>
                                    <asp:TextBox ID="txtKey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                                        width: 280px" MaxLength="100"></asp:TextBox>
                                    <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="ButtonWMarigin"/>
                </td>
                <td></td>
                </tr>
            </table>
            <div style="height: 10px; width: 650px">
            </div>
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <div style="width:1150px; height:450px; overflow: auto;">
         
                <asp:GridView Width="1150px" runat="server" ID="grdGrpCases" DataKeyNames="CASEID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    CellPadding="4" CellSpacing="2"  ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle Height="15px"  CssClass="FixedHeader" Font-Size="11px" BackColor="#6B696B" ForeColor="White" Width="1150px" />
                   
                    <Columns>
                        <asp:BoundField DataField="CASEID" HeaderText="CASEID" SortExpression="CASEID"
                            Visible="false"></asp:BoundField>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <input id="HeaderLevelCheckBox" onclick="javascript:SelectAllCheckboxes(this);" 
                                        runat="server" type="checkbox" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                     <asp:Label ID="lblCase" runat="server" Text='<%# bind("CASEID")%>' style="display:none" ></asp:Label>
                                    <asp:CheckBox id="SelCase" runat="server">
                                    </asp:CheckBox>
                                </ItemTemplate>
                          </asp:TemplateField>
                        <asp:BoundField DataField="CASEDES" HeaderText="STRUCTURE DESCRIPTOR1" SortExpression="CASEDES">
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CASEDE2" HeaderText="STRUCTURE DESCRIPTOR2" SortExpression="CASEDE2">
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="APPLICATION" HeaderText="STRUCTURE APPLICATION" SortExpression="APPLICATION">
                            <ItemStyle Width="250px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="NOTES" HeaderText="STRUCTURE NOTES" SortExpression="NOTES">
                            <ItemStyle Width="200px" Wrap="true" HorizontalAlign="Left" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        
                        <asp:BoundField DataField="CREATIONDATE" HeaderText="STRUCTURE CREATION DATE" SortExpression="CREATIONDATE">
                            <ItemStyle Width="110px" Wrap="true" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SERVERDATE" HeaderText="STRUCTURE UPDATE DATE" SortExpression="SERVERDATE">
                            <ItemStyle Width="110px" Wrap="true" CssClass="NormalLabel" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                    </Columns>
                </asp:GridView>
            </div>
            <asp:Button runat="server" ID="btnSubmitReg"  Text="Submit" />
            <br />
        </div>
        <asp:HiddenField ID="hidReportId" runat="server" />
        <asp:HiddenField ID="hidReportDes" runat="server" />
        <asp:HiddenField ID="hidReportDes1" runat="server" />
        <asp:HiddenField ID="hvCaseGrd" runat="server" />
        <asp:HiddenField ID="hidGroupId" runat="server" />
    </div>
    </form>
</body>
</html>
