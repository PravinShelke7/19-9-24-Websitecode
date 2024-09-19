<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompanyCaseDetails.aspx.vb" Inherits="Pages_StandAssist_PopUp_CompanyCaseDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>SA-Structure Details</title>
     <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
   .FixedHeader 
   {                     
            position: absolute;           
            font-weight: bold;
            margin-left:0px;
            margin-right:35px;
            height:22px;
            
    }  
     .ALNITEM
    {
      padding-left:3px;
       padding-top:4px;
       padding-bottom:1px;
          } 
</style>

    <script type="text/JavaScript">
        function CaseSearch(CaseDes, CaseId, Sponsor, notes, file) {
            // alert(CaseDes+' '+CaseId);

            var hidCasedes = document.getElementById('<%= hidCaseDes.ClientID%>').value;
            // alert(hidCasedes);
            var hidCaseId = document.getElementById('<%= hidCaseId.ClientID%>').value;
            var hidCaseIdD = document.getElementById('<%= hidCaseIdD.ClientID%>').value;
            var hidSponsor = document.getElementById('<%= hidSponsB.ClientID%>').value;
            //alert(hidCaseId);
            // alert(window.opener.document.getElementById(hidCasedes));
            //alert(window.opener.document.getElementById(hidCaseId));
            notes = notes.replace(new RegExp("&#", 'g'), "'");
            CaseDes = CaseDes.replace(new RegExp("&#", 'g'), "'");
            //  alert(hidCasedes);
            var PType = document.getElementById("hidPtype").value

            window.opener.document.getElementById(hidCasedes).innerHTML = CaseDes;
            window.opener.document.getElementById(hidCaseIdD).value = CaseDes;
            //  alert('hi');
            window.opener.document.getElementById(hidSponsor).value = Sponsor;
            window.opener.document.getElementById(hidCaseId).value = CaseId;
            
               var hidNotes = document.getElementById('<%= hidNotes.ClientID%>').value;
               
            // alert(PType);
            if (PType == "DEFAULTBS") {
             window.opener.document.getElementById(hidNotes).value = notes;
                var hidFile = document.getElementById('<%= hidBFile.ClientID%>').value;
                window.opener.document.getElementById(hidFile).value = file;
                if (file == "") {

                    opener.ShowToolTip('btnSSMessage', "No Sponsor Message Available");
                }
                else {
                    opener.ShowToolTip('btnSSMessage', "Sponsor Message");
                }
                if (notes == "") {
                    opener.ShowToolTip('btnSNotes', "No Notes Available");
                }
                else {
                                        
                    opener.ShowToolTip('btnSNotes', notes);
                }

            }
            else if (PType == "DEFAULTPS") {
            // alert(notes+'---'+hidNotes);
                 if (notes == "") {
                    opener.ShowToolTip('btnPNotes', "No Notes Available");
                    window.opener.document.getElementById(hidNotes).value = "No Notes Available";
                }
                else {
                    
                    window.opener.document.getElementById(hidNotes).value = notes;
                    opener.ShowToolTip('btnPNotes', notes);
                }
            }
            else if (PType == "DEFAULTCPS") {

                if (notes == "") {
                    opener.ShowToolTip('btnCompNotes', "No Notes Available");
                    window.opener.document.getElementById(hidNotes).value = "No Notes Available";
                }
                else {

                    window.opener.document.getElementById(hidNotes).value = notes;
                    opener.ShowToolTip('btnCompNotes', notes);
                }
            }
			document.getElementById('<%= hidCase.ClientID%>').value = CaseId;
            window.close();
			return false;
        }
        function CaseStatus(CaseId) {
            // alert('sud')
            var width = 800;
            var height = 250;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            var page = 'StatusDetails.aspx?CaseId=' + CaseId + ' ';
            newwin = window.open(page, 'CaseStatus', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
        function CaseViewer(groupID) {
            var CaseDe1 = document.getElementById("txtCaseDe1")
            var CaseDe2 = document.getElementById("txtCaseDe2")
            var hidCaseid = document.getElementById('<%= hidCaseid.ClientID%>').value;
            var width = 800;
            var height = 500;
            var left = (screen.width - width) / 2;
            var top = (screen.height - height) / 2;
            var URL
            var params = 'width=' + width + ',height=' + height; params += ',top=' + top + ', left=' + left; params += ', directories=no';
            params += ', location=no';
            params += ', menubar=no';
            params += ', resizable=yes';
            params += ', scrollbars=yes';
            params += ', status=yes';
            params += ', toolbar=no';
            if (hidCaseid == "hidApprovedCase") {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=B&GrpID=" + groupID;
            }
            else if (hidCaseid == "hidPropCase") {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=P&GrpID=" + groupID;
            }
            else {
                URL = "CaseViewer.aspx?CaseDe1=" + CaseDe1.value + "&CaseDe2=" + CaseDe2.value + "&CType=C";
            }
            //alert(URL);
            newwin = window.open(URL, 'CaseViewer', params);
            return false
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
    <div id="ContentPagemargin" style=" width:1153px; margin: 0 0 0 0">
        <div id="PageSection1" style="text-align: left;margin-left:3px;margin-right:3px;margin-bottom:7px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table cellpadding="0" cellspacing="2">
            <tr>
            <td colspan="2">
               <div class="PageHeading" id="divMainHeading" style="width:1100px;text-align:center;">
                    Structure Selector</div>
            </td>
            </tr>
                <tr>
                    <td align="right">
                        <b>Search:</b>
                    </td>
                    <td>
                        <asp:TextBox ID="txtCaseDe1" runat="server" CssClass="SmallTextBox" Style="text-align: left;Width:200px"></asp:TextBox>
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
            <table cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton CssClass="Link" CausesValidation="false" ID="lnkShowAll" runat="server" />
                    </td>
                </tr>
            </table>
            <table>
            <tr>
            <td>
             <asp:Label ID="lblPcase" runat="server" CssClass="CalculatedFeilds" Visible="true" style="font-size:14px;color:Red;"
              Text="Currently you have no Company Structures defined. You can create a Structure through Manage Structures. Only an Administrator can create Structures in the Company Library."></asp:Label>
            </td>
            </tr>
            </table>
             <div id="container" style="width:1140px; height:485px; overflow: auto;">
        
             <asp:GridView runat="server" ID="grdCaseSearch" DataKeyNames="CaseId" Width="1105px"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    style="margin-left:5px;"  ShowHeader="true"
                    CellPadding="0" CellSpacing="2" ForeColor="#333333" GridLines="None">
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle BackColor="Black" ForeColor="White" CssClass="FixedHeader" Width="1100px" />
                    <Columns>
                        <asp:BoundField DataField="CaseId" HeaderText="CaseId" Visible="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="Descriptor1" SortExpression="CASEID,CaseDE1">
                            <ItemTemplate>
                            <a href="#" onclick="CaseSearch('<%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CASEDEAPOS1")%> <%#Container.DataItem("CASEDEAPOS2")%> <%#Container.DataItem("APPLICATIONAPOS")%> <%#Container.DataItem("NAMEAPOS")%>','<%#Container.DataItem("CaseId")%>','<%#Container.DataItem("SUPPLIERID")%>','<%#Container.DataItem("CASEDEAPOS3")%>','<%#Container.DataItem("FILENAME")%>')"
                                    class="Link">
                                    <%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDE1")%></a>   
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CaseDe2" HeaderText="Descriptor2" SortExpression="CaseDe2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="APPLICATION" HeaderText="Application" SortExpression="APPLICATION"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                       <asp:TemplateField HeaderText="Notes" SortExpression="CaseDe3">
                            <ItemTemplate>
                                     <p style="word-wrap: break-word;width:190px;white-space: pre-wrap;word-break: keep-all;"> <%#Container.DataItem("CaseDe3")%></p>
                                     </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" width="190" CssClass="ALNITEM"  />
                            <HeaderStyle HorizontalAlign="Left" width="190" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="NAME" HeaderText="Sponsored By" SortExpression="NAME"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="USERNAME" HeaderText="Structure Owner" SortExpression="USERNAME"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="STRUCTTYPE" HeaderText="Structure Location" SortExpression="STRUCTTYPE"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" CssClass="ALNITEM"/>
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                      
                    </Columns>
                </asp:GridView>
               
            </div>
            <br />
               <asp:Button ID="btnPostback" runat="server" style="display:none;" />
        </div>
        <asp:HiddenField ID="hidCaseid" runat="server" />
        <asp:HiddenField ID="hidCaseDes" runat="server" />
        <asp:HiddenField ID="hidCaseidD" runat="server" />
        <asp:HiddenField ID="hidGrpId" runat="server" />
        <asp:HiddenField ID="hidSponsB" runat="server" />
          <asp:HiddenField ID="hidPtype" runat="server" />
		  <asp:HiddenField ID="hidBFile" runat="server" />
             <asp:HiddenField ID="hvUserGrd" runat="server" />
             
               <asp:HiddenField ID="hidNotes" runat="server" />  
 <asp:HiddenField ID="hidCase" runat="server" />     			   
             
    </div>
    </form>
</body>
</html>
