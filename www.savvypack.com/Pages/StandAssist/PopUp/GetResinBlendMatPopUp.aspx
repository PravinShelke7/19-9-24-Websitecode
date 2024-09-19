<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetResinBlendMatPopUp.aspx.vb" Inherits="Pages_StandAssist_PopUp_GetResinBlendMatPopUp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Structure Assistant-Get Blend Materials</title>
    <link href="../../../App_Themes/SkinFile/AlliedNew.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .FixedHeader {
            position: absolute;
            font-weight: bold;
            font-size: 11px;            
            margin-left:0px;
        }   
   a.LinkMat:link
{
    color: Black;
    font-family: Optima;
    font-size: 12px;
    text-decoration: underline;
}

a.LinkMat:active
{
    color:Black;
    font-family:Arial Baltic;
    font-size:12px;
}

a.LinkMat:visited
{
    color: Black;
    font-family: Optima;
    font-size: 12px;
    text-decoration: underline;
}
a.LinkMat:hover
{
    color: Red;
    font-size: 12px;
}
     </style>
    <script type="text/JavaScript">
        function rowno(rown) {
            alert(rown);
            return false;
        }
              
        function BlendDetail(Category, MatId, Material, Mouseover, Bmat, Mcnt) {

            var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value;
            var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value;
            var j;
            var i = hidMatId;
            i = i.match(/\d+$/)[0];

            //alert(Mouseover);

            window.opener.document.getElementById(hidMatId).value = MatId;

            window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + Bmat;
            window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + Category;
            window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblBGradeDes' + Mcnt + "_" + i).innerHTML = Material;
            if (Category == "Resin") {
                opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblBGradeDes' + Mcnt + "_" + i, Mouseover);

            }
            else {
                opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblBGradeDes' + Mcnt + "_" + i, Mouseover);
                //opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_lblGradeDes' + i, "" + Material + "");
            }
            window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidBlendCat" + Mcnt + "_" + i).value = Category;
	    window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidSGradeId" + Mcnt + "_" + i).value = "0";
            window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hypSGradeDes' + Mcnt + "_" + i).innerHTML = "Nothing Selected";
            window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgBut" + Mcnt + "_" + i).style.display = 'none';
            window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgDis" + Mcnt + "_" + i).style.display = 'none';
            window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidSupId' + Mcnt + "_" + i).value = "0";
        
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
    <div id="ContentPagemargin" style="width: 1246px; margin: 0 0 0 0">
     <div id="PageSection1" style="text-align: left;margin-left:2px;margin-right:2px;margin-bottom:7px;">
            <asp:Label ID="_lErrorLble" runat="server"></asp:Label>
            <table style="width: 800px">
                <tr>
                    <td style="width: 60%">
                        <asp:Table ID="tblMaterials" runat="server" CellPadding="0" CellSpacing="2">
                        </asp:Table>
                    </td>
                    <td align="right" style="width: 40%">
                        <table cellpadding="0" cellspacing="2">
                            <tr>
                                <td align="right">
                                    <b>Keyword</b>
                                </td>
                                <td>  : </td>
                                <td>
                                    <asp:TextBox ID="txtMatDe1" runat="server" CssClass="LongTextBoxSearch" Style="text-align: left" Width="200px"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnSearch" Text="Search" runat="server" CssClass="Button" Style="margin-left: 5px" />
                                </td>
                            </tr>
                        </table>
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
            
           <div id="Materials" runat="Server" style="width:1240px; height: 550px; overflow: auto;margin-right:10px">              
                         
                <asp:GridView Width="1200px" runat="server" ID="grdMaterials" DataKeyNames="MATID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None" style="margin-left:15px;margin-right:10px; margin-bottom:5px;" >
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White"  />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle BackColor="Black" ForeColor="White" CssClass="FixedHeader" Width="1150px" />
                    <Columns>
                        <asp:BoundField DataField="MATID" HeaderText="MATID" SortExpression="MATID" Visible="false">
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="CATEGORY" SortExpression="MATID" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkBtnval" runat="server" CssClass="Link"> <%#Container.DataItem("MATID")%>:<%# Container.DataItem("MATDES")%></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="MATDE1" HeaderText="MATERIAL NAME" SortExpression="MATDE1"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="MATDE2" HeaderText="MATERIAL" SortExpression="MATDE2"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DENSITY" HeaderText="DENSITY" SortExpression="DENSITY"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                            <HeaderStyle HorizontalAlign="Left" Width="100px"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="MELTINDEX" HeaderText="MELT INDEX" SortExpression="MELTINDEX"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="100px" />
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MELTFRATE" HeaderText="MELT FLOW RATE" SortExpression="MELTFRATE"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left"  Width="100px"/>
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PROCESS" HeaderText="PROCESS" SortExpression="PROCESS"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="150px" />
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POLYMERSTRUCT" HeaderText="POLYMER STRUCTURE" SortExpression="POLYMERSTRUCT"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="150px"/>
                            <HeaderStyle HorizontalAlign="Left" Width="150px" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POLYMERDESC" HeaderText="POLYMER DESCRIPTION" SortExpression="POLYMERDESC"
                            Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="150px"/>
                            <HeaderStyle HorizontalAlign="Left"  Width="150px"/>
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="VISC" HeaderText="VISCOSITY" SortExpression="VISC" Visible="true">
                            <ItemStyle HorizontalAlign="Left" Width="100px"  />
                            <HeaderStyle HorizontalAlign="Left" Width="100px" />
                        </asp:BoundField>
                      <%--  ss--%>
                    </Columns>
                </asp:GridView>
                
            </div>             <br />
        </div>
        <asp:HiddenField ID="hidMatdes" runat="server" />
        <asp:HiddenField ID="hidMatid" runat="server" />
        <asp:HiddenField ID="hidGradedes" runat="server" />
        <asp:HiddenField ID="hidGradeid" runat="server" />
        <asp:HiddenField ID="hidSG" runat="server" />
        <asp:HiddenField ID="hidMatD" runat="server" />
        <asp:HiddenField ID="hidMatGrp" runat="server" />
        <asp:HiddenField ID="hidMat" runat="server" />
        <asp:HiddenField ID="hidLinkId" runat="server" />
        <asp:HiddenField ID="hidGradeLbl" runat="server" />
        <asp:HiddenField ID="hvUserGrd" runat="server" />
        <asp:HiddenField ID="hvGroupQ" runat="server" />
        <asp:HiddenField ID="hidMatCat" runat="server" />
        <asp:HiddenField ID="hidSession" runat="server" />
        <asp:HiddenField ID="hidMcnt" runat="server" />
       
       
    </div>

    </form>
</body>
</html>
