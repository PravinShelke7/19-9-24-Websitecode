<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetAllGroups.aspx.vb" Inherits="Pages_StandAssist_PopUp_GetAllGroups" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Group Details</title>
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FixedHeader
        {
            position: absolute;
            margin-right: 19px;
            margin-left: 0px;
        }
        .ALNITEM
        {
            padding-left: 3px;
            padding-top: 10px;
            padding-bottom: 10px;
        }
    </style>

    <script type="text/JavaScript">
         function CaseSearch(GroupId,Des1,Des2,App,Sponsor,notes, groupId,Filename) {
             
             var hidgrpdes = document.getElementById('<%= hidgrpDes.ClientID%>').value
             var hidgrpid = document.getElementById('<%= hidgrpId.ClientID%>').value
             var hidGrpIdD = document.getElementById('<%= hidGrpIdD.ClientID%>').value;
             var hidFile = document.getElementById('<%= hidFile.ClientID%>').value;
             
               var hidGNotes = document.getElementById('<%= hidGNotes.ClientID%>').value;
             var hidSNotes = document.getElementById('<%= hidSNotes.ClientID%>').value;
             
             Des1 = Des1.replace(new RegExp("&#", 'g'), "'");
             Des2 = Des2.replace(new RegExp("&#", 'g'), "'");
             App = App.replace(new RegExp("&#", 'g'), "'");
             notes = notes.replace(new RegExp("&#", 'g'), "'");
             Sponsor = Sponsor.replace(new RegExp("&#", 'g'), "'");  
               
             var PType = document.getElementById("hidPtype").value
             if (PType == "DEFAULTB") {
                    window.opener.document.getElementById(hidGNotes).value = notes;
                 if (GroupId == "0") {
                     window.opener.document.getElementById(hidgrpdes).innerHTML = Des1 + " " + Des2 + " " + App + " " + Sponsor;
                     window.opener.document.getElementById(hidGrpIdD).value = Des1 + " " + Des2 + " " + App + " " + Sponsor;
                 }

                 else {
                     window.opener.document.getElementById(hidgrpdes).innerHTML = GroupId + ":" + Des1 + " " + Des2 + " " + App + " " + Sponsor;
                     window.opener.document.getElementById(hidGrpIdD).value = GroupId + ":" + Des1 + " " + Des2 + " " + App + " " + Sponsor;
                 }
                 window.opener.document.getElementById(hidgrpid).value = groupId;

                 if (notes == "") {
                     opener.ShowToolTip('btnNotes', "No Notes Available");
                 }
                 else {
                    
                     
                     opener.ShowToolTip('btnNotes', notes);
                 }
                 window.opener.document.getElementById(hidFile).value = Filename;
                 //alert(Filename);
                 if (Filename == "") {
                     window.opener.document.getElementById(hidFile).value=""
                     opener.ShowToolTip('btnSponsorM', "No Sponsor Message Available");
                 }
                 else {
                     window.opener.document.getElementById(hidFile).value=Filename;
                     opener.ShowToolTip('btnSponsorM', "Sponsor Message");
                 }
                 window.opener.document.getElementById(hidFile).value = Filename;
                 
                 window.opener.document.getElementById("hidBFileName").value = "";
                 
                 window.opener.document.getElementById("hidApprovedCase").value = "0";
                 window.opener.document.getElementById("hidApprovedCaseD").value = "Select Structure";
                 window.opener.document.getElementById("lnkApprovedCase").innerHTML = "Select Structure";
                 
               opener.ShowToolTipN('btnSNotes', "No Notes Available");
               window.opener.document.getElementById("hidNotes").value = "";
               opener.ShowToolTipN('btnSSMessage', "No Sponsor Message Available");
               
             }
             else if (PType == "DEFAULTP") 
             {
                window.opener.document.getElementById(hidGNotes).value = notes;
                 if (GroupId == "0") {
                     window.opener.document.getElementById(hidgrpdes).innerHTML = Des1 + " " + Des2 + " " + App;
                     window.opener.document.getElementById(hidGrpIdD).value = Des1 + " " + Des2 + " " + App;
                 }

                 else {
                     window.opener.document.getElementById(hidgrpdes).innerHTML = GroupId + ":" + Des1 + " " + Des2 + " " + App
                     window.opener.document.getElementById(hidGrpIdD).value = GroupId + ":" + Des1 + " " + Des2 + " " + App;
                 }
                 window.opener.document.getElementById(hidgrpid).value = groupId;

                 if (notes == "") {
                     opener.ShowToolTip('btnPGNotes', "No Notes Available");
                      window.opener.document.getElementById("hidGPNotes").value ="No Notes Available";
                      window.opener.document.getElementById("hidPNotes").value = "No Notes Available";
                 }
                 else {
                     
                      window.opener.document.getElementById("hidGPNotes").value =notes;
                     opener.ShowToolTip('btnPGNotes', notes);
                     window.opener.document.getElementById("hidPNotes").value = "No Notes Available";
                 }
                 window.opener.document.getElementById("hidPropCase").value = "0";
                 window.opener.document.getElementById("hidPropCaseD").value = "Select Structure";
                 window.opener.document.getElementById("lnkPropCase").innerHTML = "Select Structure";
                 
                   opener.ShowToolTipN('btnPNotes', "No Notes Available");
                  
                 

             }
             else {
                 if (GroupId == "0") {
                     window.opener.document.getElementById(hidgrpdes).innerHTML = Des1 + " " + Des2 + " " + App + " " + Sponsor;
                     window.opener.document.getElementById(hidGrpIdD).value = Des1 + " " + Des2 + " " + App + " " + Sponsor;
                 }

                 else {
                     window.opener.document.getElementById(hidgrpdes).innerHTML = GroupId + ":" + Des1 + " " + Des2 + " " + App + " " + Sponsor;
                     window.opener.document.getElementById(hidGrpIdD).value = GroupId + ":" + Des1 + " " + Des2 + " " + App + " " + Sponsor;
                 }
                // alert(notes);
                  if (notes == "") {
                     opener.ShowToolTip('btnCompGNotes', "No Notes Available");
                     window.opener.document.getElementById(hidGNotes).value = "No Notes Available";
                 }
                 else {

                     window.opener.document.getElementById(hidGNotes).value = notes;
                     opener.ShowToolTip('btnCompGNotes', notes);
                 }
                

		         window.opener.document.getElementById("hidCompnyCase").value = "0";
                 window.opener.document.getElementById("hidCompnyCaseD").value = "Select Structure";
                 window.opener.document.getElementById("lnkCompCase").innerHTML = "Select Structure";
                 
                 window.opener.document.getElementById(hidgrpid).value = groupId;
                 
                   opener.ShowToolTipN('btnCompNotes', "No Notes Available");
             }

             document.getElementById('<%= hidGroup.ClientID%>').value = GroupId;
           
             window.close();
             return false;

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
    <form id="form1" runat="server" defaultbutton="btnSearch">
    <div id="ContentPagemargin" style="width: 1147px; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left; margin-left: 3px; margin-right: 3px;
            margin-bottom: 7px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <%--<div class="divHeader" style="font-size:17px;font-weight:bold;text-align:center;margin-bottom:10px;margin-top:10px;width:700px;height:30px;">
                      Group Details
</div>--%>
            <table cellpadding="0" cellspacing="2">
                <tr>
                    <td colspan="2">
                        <div class="PageHeading" id="divMainHeading" style="width: 100%; margin-left: 470px;">
                            Group Selector</div>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <b>Search:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtkey" runat="server" CssClass="SmallTextBox" Style="text-align: left;
                            width: 200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 0px" />
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblGGroup" runat="server" CssClass="CalculatedFeilds" Visible="true"
                            Style="font-size: 16px; color: Red;" Text="Currently you have no Groups defined. You can create a Group through Manage Groups."></asp:Label>
                    </td>
                </tr>
            </table>
            <div id="container" style="width: 1140px; height: 485px; overflow: auto;">
                <asp:GridView runat="server" ID="grdGroupSearch" DataKeyNames="GROUPID" AutoGenerateSelectButton="false"
                    Width="1110px" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    Style="margin-left: 5px;" CellPadding="0" CellSpacing="2" ForeColor="#333333"
                    GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle BackColor="Black" ForeColor="White" CssClass="FixedHeader" Width="1106px" />
                    <Columns>
                        <asp:BoundField DataField="CaseId" HeaderText="CaseId" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="GROUPID" HeaderText="GROUPID" SortExpression="GROUPID"
                            Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Descriptor1" SortExpression="GROUPIDD">
                            <ItemTemplate>
                                <a href="#" onclick="CaseSearch('<%#Container.DataItem("GROUPID")%>','<%#Container.DataItem("DESAPOS1")%>','<%#Container.DataItem("DESAPOS2")%>','<%#Container.DataItem("APPLICATIONAPOS")%>','<%#Container.DataItem("NAMEAPOS")%>','<%#Container.DataItem("DESAPOS3")%>','<%#Container.DataItem("GROUPID")%>','<%#Container.DataItem("FILENAME")%>')"
                                    class="Link">
                                    <%# Container.DataItem("GROUPID")%>:<%# Container.DataItem("DES1")%></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="DES2" HeaderText="Descriptor2" SortExpression="DES2" Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CASEIDS" HeaderText="Structure(s)" SortExpression="CASEIDS"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="APPLICATION" HeaderText="Application" SortExpression="APPLICATION"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Notes" SortExpression="DES3">
                            <ItemTemplate>
                                <asp:Label ID="lblNotess" Width="190px" runat="server" Style="word-wrap: break-word;"><%#Container.DataItem("DES3")%></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NAME" HeaderText="Sponsored By" SortExpression="NAME"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="Sponsor Message" Visible="true">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkFileName" runat="server" CssClass="Link" CausesValidation="false"
                                    CommandArgument='<%#Eval("FILENAME")%>' OnClick="OpenPDF">Click Here</asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            <br />
            <asp:Button ID="btnPostback" runat="server" Style="display: none;" />
        </div>
        <asp:HiddenField ID="hidgrpId" runat="server" />
        <asp:HiddenField ID="hidgrpDes" runat="server" />
        <asp:HiddenField ID="hidGrpIdD" runat="server" />
        <asp:HiddenField ID="hidPtype" runat="server" />
        <asp:HiddenField ID="hidFile" runat="server" />
        <asp:HiddenField ID="hvUserGrd" runat="server" />
        <asp:HiddenField ID="hidGNotes" runat="server" />
        <asp:HiddenField ID="hidSNotes" runat="server" />
        <asp:HiddenField ID="hidGroup" runat="server" />
    </div>
    </form>
</body>
</html>
