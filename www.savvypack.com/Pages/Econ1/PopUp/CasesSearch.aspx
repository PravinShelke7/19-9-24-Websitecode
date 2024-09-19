<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CasesSearch.aspx.vb" Inherits="Pages_Econ1_PopUp_CasesSearch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
   <title>E1-CaseDetails</title> 
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <meta http-equiv="X-UA-Compatible" content="IE=10,chrome=1" />
    <style type="text/css">
    .divUpdateprogress
{
    left:400px;
    top:300px;
    position:absolute;
}

    </style>
    <script type="text/JavaScript">
        function CaseSearch(CaseDes, CaseId, StatusId, Status) {
            // alert(CaseDes+' '+CaseId);
            var hidCaseDes = document.getElementById('hidCaseDes').value;
            // alert(hidCasedes);
            var hidCaseid = document.getElementById('hidCaseid').value;
            var hidCaseidD = document.getElementById('hidCaseidD').value;

            var str = CaseDes.replace(/##/g, "'");
            str = str.replace(/$#/g, '"');


            //alert(hidCaseId); 
            // alert(window.opener.document.getElementById(hidCasedes));
            //alert(window.opener.document.getElementById(hidCaseId));

            if (CaseId == "0") {

                window.opener.document.getElementById(hidCaseDes).innerText = "Select case";
                window.opener.document.getElementById(hidCaseidD).value = "Select case";

            }
            else {
                window.opener.document.getElementById(hidCaseDes).innerText = str + "   " + Status;
                window.opener.document.getElementById(hidCaseidD).value = str + "   " + Status;
            }
            //alert(hidCaseId + "   " + CaseId);
            window.opener.document.getElementById(hidCaseid).value = CaseId;
            //  alert(window.opener.document.getElementById(hidCaseId).value);

            if (hidCaseid = "hidPropCase") {
                window.opener.document.getElementById("hidPropCaseSt").value = StatusId;
                //alert(StatusId);
                var Button = window.opener.document.getElementById("btnAdminSubmit");
                if (Button != null) {
                    if (StatusId == "5") {
                        Button.style.display = 'none';
                    }
                    else {
                        Button.style.display = 'none';
                    }
                }

            }
            window.close();
        }
        function CaseStatus(CaseId, Owner) {
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
            var page = 'StatusDetails.aspx?CaseId=' + CaseId + '&Owner=' + Owner + ' ';
            newwin = window.open(page, 'CaseStatus', params);
            if (newwin == null || typeof (newwin) == "undefined") {
                alert("Pop-ups are being blocked. Please add www.savvypack.com to your trusted sites and disable pop-up blocking.");
            }

            return false;

        }
        function CaseViewer(groupID) {
            var KeyWord = document.getElementById("txtKeyWord")
            var hidCaseid = document.getElementById('hidCaseid').value;
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
            if (hidCaseid == "hidPropCase") {
                URL = "CasesViewer.aspx?KeyWord=" + KeyWord.value + "&CType=P&GrpID=" + groupID;
            }
            else {
                URL = "CasesViewer.aspx?KeyWord=" + KeyWord.value + "&CType=AP&GrpID=" + groupID;
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
    <form id="form1" runat="server">
        <asp:ScriptManager ID="scrpt1" runat="server">
        </asp:ScriptManager>
          <div id="ContentPagemargin" style="width:1320px;margin:0 0 0 0">
       <div id="PageSection1" style="text-align:left">
        <asp:UpdatePanel ID="upd1" runat="server">
            <ContentTemplate>
        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <div class="divUpdateprogress">
                            <table>
                                <tr>
                                    <td>
                                        <img alt="" src="../../../Images/Loading4.gif" height="50px" />
                                    </td>
                                    <td>
                                        <b style="color: Red;">Updating the Record</b>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
        <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
             <table cellpadding="0" cellspacing="2">
                    <tr>
                        <td align="right">
                            <b>Keyword Search:</b>
                        </td>
                        <td>  
                          <asp:TextBox ID="txtKeyWord" runat="server" CssClass="MediumTextBox" Width="300px" style="text-align:left;"></asp:TextBox>
                        </td>  
                    </tr>
                                       
                    <tr>
                        <td>
                            
                        </td>
                        <td>
                             <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" style="margin-left:0px" />
                             <asp:Button ID="btnCaseViewer" Text="Case Viewer" runat="server" CssClass="Button" Visible="false"  />
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
        <ajaxToolkit:TabContainer ID="tabCase" Height="500px" runat="server" ActiveTabIndex="0"
            AutoPostBack="false"   >
            <ajaxToolkit:TabPanel runat="server" HeaderText="Models" ToolTip="Models"
                ID="tabModels">
                <ContentTemplate>
                <asp:Panel ID="pnlModels" runat="server" >
                <asp:Label ID="lblRes" runat="server" ></asp:Label>
                <div style="width:1300px;height:480px;overflow:auto;">
                <asp:GridView Width="1280px" runat="server" ID="grdCaseSearch" DataKeyNames="CaseId" 
                                AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <Columns>
                    <asp:BoundField DataField="CaseId" HeaderText="CaseId" SortExpression="CaseId" 
        Visible="False"></asp:BoundField>
         <asp:BoundField DataField="GNAME" HeaderText="Group" 
                SortExpression="GNAME">
        <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="10%" />
            </asp:BoundField>
        <asp:TemplateField HeaderText="Description1" SortExpression="CaseDe1">
    <ItemTemplate>
    <a href="#" onclick="CaseSearch('<%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDES1")%>','<%#Container.DataItem("CaseId")%>','<%#Container.DataItem("STATUSID")%>','<%#Container.DataItem("STATUS")%>')" class="Link">
        <%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDE1")%></a>                                
        </ItemTemplate>

        <HeaderStyle HorizontalAlign="Left" />

        <ItemStyle HorizontalAlign="Left" Width="10%" />
        </asp:TemplateField>
        <asp:BoundField DataField="CaseDe2" HeaderText="Description2" 
                SortExpression="CaseDe2">
        <HeaderStyle HorizontalAlign="Left" />

            <ItemStyle HorizontalAlign="Left" Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="COMPANYNAME" HeaderText="Packspec Company" 
                    SortExpression="COMPANYNAME">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="10%" />
            </asp:BoundField>
            <asp:BoundField DataField="GRPDETAIL" HeaderText="PackSpec Group" 
                    SortExpression="GRPDETAIL">
            <HeaderStyle HorizontalAlign="Left" />
            <ItemStyle HorizontalAlign="Left" Width="10%" />
            </asp:BoundField>
<asp:BoundField DataField="COUNTRYDES" HeaderText="Country of Manufacture" 
        SortExpression="COUNTRYDES">
<HeaderStyle HorizontalAlign="Left" />

<ItemStyle HorizontalAlign="Left" Width="10%" />
</asp:BoundField><asp:BoundField DataField="UNITNAME" HeaderText="Currency" 
        SortExpression="UNITNAME">
<HeaderStyle HorizontalAlign="Left" />

<ItemStyle HorizontalAlign="Left" Width="5%" />
</asp:BoundField><asp:BoundField DataField="EFFDATEN" HeaderText="Effective Date" 
        SortExpression="EFFDATE">
<HeaderStyle HorizontalAlign="Left" />

<ItemStyle HorizontalAlign="Left" Width="5%" />
</asp:BoundField><asp:BoundField DataField="FORMATDE" HeaderText="Product Format" 
        SortExpression="FORMATDE">
<HeaderStyle HorizontalAlign="Left" />

<ItemStyle HorizontalAlign="Left" Width="10%" />
</asp:BoundField><asp:BoundField DataField="CaseOwner" HeaderText="Owner" SortExpression="CaseOwner">
<HeaderStyle HorizontalAlign="Left" />

<ItemStyle HorizontalAlign="Left" Width="10%" />
</asp:BoundField><asp:TemplateField HeaderText="Status" SortExpression="STATUS">
    <ItemTemplate>
            <a href="#" onclick="CaseStatus('<%#Container.DataItem("CaseId")%>','<%#Container.DataItem("CaseOwnerID")%>')" class="Link">
                <%#Container.DataItem("STATUS")%>
            </a>
                                
</ItemTemplate>

<HeaderStyle HorizontalAlign="Left" />

<ItemStyle HorizontalAlign="Left" Width="10%" />
</asp:TemplateField></Columns>
<FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
<HeaderStyle Height="25px" BackColor="Black" Font-Size="11px" Font-Bold="True"
                                    ForeColor="White" />
                                    <RowStyle CssClass="AlterNateColor1" />
                                    </asp:GridView></div></asp:Panel></ContentTemplate>            
</ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel runat="server" HeaderText="Operations " ToolTip="Operations"
                ID="tabOperations">
                <ContentTemplate>
                <asp:Panel ID="pnlOperation" runat="server" >
                <asp:Label ID="lblRes1" runat="server" ></asp:Label>
                <div style="width:920px;height:480px;overflow:auto;">
                   <asp:GridView Width="900px" runat="server" ID="grdCaseSearch1" DataKeyNames="CaseId" 
                                AllowSorting="True" AutoGenerateColumns="False"
                    CellPadding="4" ForeColor="#333333" GridLines="None">
                   <AlternatingRowStyle CssClass="AlterNateColor2" />
                <Columns>
                    <asp:BoundField DataField="CaseId" HeaderText="CaseId" SortExpression="CaseId" Visible="False">
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Description1" SortExpression="CaseDe1">
                <ItemTemplate>
                <a href="#" onclick="CaseSearch('<%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDES1")%>','<%#Container.DataItem("CaseId")%>','<%#Container.DataItem("STATUSID")%>','<%#Container.DataItem("STATUS")%>')" class="Link">
                    <%#Container.DataItem("CaseId")%>:<%#Container.DataItem("CaseDE1")%></a>                                
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Left" />
                    <ItemStyle HorizontalAlign="Left" Width="30%" />
                    </asp:TemplateField>

                    <asp:BoundField DataField="CaseDe2" HeaderText="Description2" 
                            SortExpression="CaseDe2">
                    <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="BTYPE" HeaderText="Board Type" 
                            SortExpression="BTYPE">
                    <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="30%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="GSM" HeaderText="Weight/Area" 
                            SortExpression="GSM">
                    <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                    <asp:BoundField DataField="UNIT" HeaderText="Weight/Area(Unit)" 
                            SortExpression="UNIT">
                    <HeaderStyle HorizontalAlign="Left" />
                        <ItemStyle HorizontalAlign="Left" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle Height="25px" BackColor="Black" Font-Size="11px" Font-Bold="True"
                            ForeColor="White" /><RowStyle CssClass="AlterNateColor1" />
                    </asp:GridView></div></asp:Panel></ContentTemplate>           
            </ajaxToolkit:TabPanel>           
                    </ajaxToolkit:TabContainer>        
                   <asp:HiddenField  ID="hidCaseid" runat="server" />
                      <asp:HiddenField ID="hidCaseDes" runat="server" />
                       <asp:HiddenField  ID="hidCaseidD" runat="server" />
                       <asp:HiddenField  ID="hidsortCase" runat="server" />
                       <asp:HiddenField  ID="hidsortCase1" runat="server" />
                    </ContentTemplate>
        </asp:UpdatePanel>
         </div>         
                   </div> 
                   

    </form>
</body>
</html>
