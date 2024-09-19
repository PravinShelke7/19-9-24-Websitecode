<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetMatPopUpGrade.aspx.vb"
    Inherits="Pages_StandAssist_PopUp_GetMatPopUpGrade" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Stand Alone Barrier Assistant-Get Materials</title>
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
        function testData() {

            var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value;
            var i = hidMatId;
            i = i.match(/\d+$/)[0];

            document.getElementById('<%= hvGroupQ.ClientID%>').value = window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_hidMatCat" + i).value;

            if (document.getElementById('<%= hvGroupQ.ClientID%>').value == "") {

            }
            else {

                document.getElementById('<%= btnCall.ClientID%>').click();
            }



        }
       
 function MaterialDetail(Category, MatId, Material, Mouseover, Bmat) {
            
            var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value;
            var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value;
            var j; 
            var i = hidMatId;
            i = i.match(/\d+$/)[0];

            //For Blend
            if ((Bmat == "Blend1") || (Bmat == "Blend2") || (Bmat == "Blend3") || (Bmat == "Blend4") || (Bmat == "Blend5")) {
                for (j = 1; j <= 10; j++) {
                    if (window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidMatid' + j).value == MatId) {
                        alert("Please select another Blend as " + Bmat + " is already selected.");
                        return false;
                    }
                }
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblGradeDes' + i).innerHTML = Material;
                for (j = 1; j <= 2; j++) {
                    window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_Blendrow' + i + "_" + j).style.display = "table-row";
                }
                window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + Bmat;
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_T' + i).style.display = "none";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_OTRSVal' + i).innerHTML = "";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_WVTRSVal' + i).innerHTML = "";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblWtPer' + i).innerHTML = "0.00";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblSg' + i).innerHTML = "0.00";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidSGradeId' + i).value = "0";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgBut' + i).style.display = 'none';
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgDis' + i).style.display = 'none';
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hypSGradeDes' + i).style.display = "none";
                window.opener.document.getElementById(hidMatId).value = MatId;
            }

            //For Other materials
            else 
            {
                window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + Category;
                window.opener.document.getElementById(hidMatId).value = MatId;
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblGradeDes' + i).innerHTML = Material;
                for (j = 1; j <= 2; j++) {
                    window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_Blendrow' + i + "_" + j).style.display = "none";
                }
                if (Category == "Resin")
                {
                    opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblGradeDes' + i, Mouseover);
                }
                else 
                {
                    opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblGradeDes' + i, Mouseover);
                    //opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_lblGradeDes' + i, "" + Material + "");
                }
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hypSGradeDes' + i).style.display = "table-row";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hypSGradeDes' + i).innerHTML = "Nothing Selected";
                opener.ShowToolTip('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hypSGradeDes' + i, "Supplier Grade");
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_T' + i).style.display = "inline";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblT' + i).style.display = "none";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_OTRSVal' + i).innerHTML = "";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_WVTRSVal' + i).innerHTML = "";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblWtPer' + i).innerHTML = "0.00";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_lblSg' + i).innerHTML = "0.00";
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidSGradeId' + i).value = "0";
                window.opener.document.getElementById("ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidMatCat" + i).value = Category;
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgBut' + i).style.display = 'none';
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_imgDis' + i).style.display = 'none';
                window.opener.document.getElementById('ctl00_StandAssistContentPlaceHolder_tabSDesigner_tabpnl1_hidSupId' + i).value = "0";
                document.getElementById('<%= hidMatVal.ClientID%>').value = MatId;
            }       
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
            <table style="width: 600px;" cellpadding="4" cellspacing="0">
                <%--<tr class="AlterNateColor4">
                    <td>
                        Material Des1
                    </td>
                    <td>
                        Material Des2
                    </td>   
                    <%--<td>
                        <asp:Label ID="lblDe3" runat="server" Text="Label" Visible="false"></asp:Label> 
                    </td>  
                    <td>
                        <asp:Label ID="lblDe4" runat="server" Text="Label" Visible="false"></asp:Label> 
                    </td>  
                    <td>
                        <asp:Label ID="lblDe5" runat="server" Text="Label" Visible="false"></asp:Label> 
                    </td> 
                                    
                </tr>--%>
            </table>
           <div id="Materials" runat="Server" style="width:1240px; height: 550px; overflow: auto;margin-right:10px">              

               <%--<asp:GridView Width="1200px" runat="server" ID="grdMaterials" DataKeyNames="MATID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None" style="margin-left:15px;margin-right:10px; margin-bottom:5px;" >
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White"  />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle BackColor="Black" ForeColor="White" CssClass="FixedHeader" Width="1195px" />
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
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DENSITY" HeaderText="DENSITY" SortExpression="DENSITY"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MELTINDEX" HeaderText="MELT INDEX" SortExpression="MELTINDEX"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="80" />
                            <HeaderStyle HorizontalAlign="Center" Width="160" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MELTFRATE" HeaderText="MELT FLOW RATE" SortExpression="MELTFRATE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="280" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PROCESS" HeaderText="PROCESS" SortExpression="PROCESS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                            <HeaderStyle HorizontalAlign="Center" Width="280" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POLYMERSTRUCT" HeaderText="POLYMER STRUCTURE" SortExpression="POLYMERSTRUCT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                            <HeaderStyle HorizontalAlign="Center" Width="260" />
                        </asp:BoundField>
                        <asp:BoundField DataField="POLYMERDESC" HeaderText="POLYMER DESCRIPTION" SortExpression="POLYMERDESC"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="140" />
                            <HeaderStyle HorizontalAlign="Center" Width="160" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ETHYLENE" HeaderText="% ETHYLENE" SortExpression="ETHYLENE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="120" />
                            <HeaderStyle HorizontalAlign="Center" Width="70" />
                        </asp:BoundField>
                        <asp:BoundField DataField="VISC" HeaderText="VISCOSITY" SortExpression="VISC" Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="60" />
                            <HeaderStyle HorizontalAlign="Left" Width="80" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TYPE" HeaderText="TYPE" SortExpression="TYPE" Visible="false">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="ANHYDRIDEMOD" HeaderText="ANHYDRIDE MODIFICATION" SortExpression="ANHYDRIDEMOD"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="WATERACTIVITY" HeaderText="WATER ACTIVITY OF PRODUCT TO  BE APPLIED"
                            SortExpression="WATERACTIVITY" Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="200" />
                            <HeaderStyle HorizontalAlign="Center" Width="200" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ADHESIVE" HeaderText="ADHESIVE" SortExpression="ADHESIVE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="80" />
                            <HeaderStyle HorizontalAlign="Center" Width="80" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DILUENT" HeaderText="DILUENT" SortExpression="DILUENT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="SOLVENT" HeaderText="SOLVENT" SortExpression="SOLVENT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="RESINFAMILY" HeaderText="RESIN FAMILY" SortExpression="RESINFAMILY"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="140" />
                            <HeaderStyle HorizontalAlign="Center" Width="120" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TECHDESC" HeaderText="TECHNICAL DESCRIPTION" SortExpression="TECHDESC"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="350" />
                            <HeaderStyle HorizontalAlign="Center" Width="450" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ALLOY" HeaderText="ALLOY" SortExpression="ALLOY" Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="150" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEMPER" HeaderText="TEMPER" SortExpression="TEMPER" Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBSTRATE" HeaderText="SUBSTRATE" SortExpression="SUBSTRATE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="200" />
                            <HeaderStyle HorizontalAlign="Center" Width="180" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FUNCTION" HeaderText="FUNCTION" SortExpression="FUNCTION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="250" />
                            <HeaderStyle HorizontalAlign="Center" Width="380" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FEATURE" HeaderText="FEATURE" SortExpression="FEATURE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PARTICLESIZE" HeaderText="PARTICLESIZE" SortExpression="PARTICLESIZE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="250" />
                            <HeaderStyle HorizontalAlign="Center" Width="280" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBSTRATES" HeaderText="SUBSTRATE" SortExpression="SUBSTRATES"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                            <HeaderStyle HorizontalAlign="Center" Width="150" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CURING" HeaderText="CURING" SortExpression="CURING" Visible="false">
                            <ItemStyle HorizontalAlign="Center"  />
                            <HeaderStyle HorizontalAlign="Center"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="THICKNESS" HeaderText="THICKNESS" SortExpression="THICKNESS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                            <HeaderStyle HorizontalAlign="Center" Width="180" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TECHDESCRIPTION" HeaderText="TECHNICAL DESCRIPTION" SortExpression="TECHDESCRIPTION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="300" />
                            <HeaderStyle HorizontalAlign="Center" Width="330" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RECYCLE" HeaderText="RECYCLE" SortExpression="RECYCLE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="250" />
                            <HeaderStyle HorizontalAlign="Center" Width="290" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBLAYERS" HeaderText="SUBSTRATE LAYERS" SortExpression="SUBLAYERS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PREOUTSIDE" HeaderText="PRETREAT OUTSIDE" SortExpression="PREOUTSIDE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center"  Width="100"/>
                            <HeaderStyle HorizontalAlign="Center"  Width="120"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PREPRODOUTSIDE" HeaderText="PRETREAT PRODUCT SIDE" SortExpression="PREPRODOUTSIDE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="110" />
                            <HeaderStyle HorizontalAlign="Center" Width="120"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="MODIFIERS" HeaderText="MODIFIERS" SortExpression="MODIFIERS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center"  Width="100"/>
                            <HeaderStyle HorizontalAlign="Center"  Width="100"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OUTCOATING" HeaderText="OUTSIDE COATING" SortExpression="OUTCOATING"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center"  Width="100"/>
                            <HeaderStyle HorizontalAlign="Center"  Width="120"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODOUTCOATING" HeaderText="PRODUCT SIDE COATING" SortExpression="PRODOUTCOATING"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="110" />
                            <HeaderStyle HorizontalAlign="Center" Width="120" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OXYGENBARR" HeaderText="OXYGEN BARRIER" SortExpression="OXYGENBARR"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100"/>
                            <HeaderStyle HorizontalAlign="Center" Width="100"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="MOISTUREBARR" HeaderText="MOISTURE BARRIER" SortExpression="MOISTUREBARR"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="APPLICATION" HeaderText="APPLICATION" SortExpression="APPLICATION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FORMULATION" HeaderText="FORMULATION" SortExpression="FORMULATION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="150" />
                            <HeaderStyle HorizontalAlign="Center" Width="180" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REACTIVE" HeaderText="REACTIVE" SortExpression="REACTIVE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Center" Width="100" />
                            <HeaderStyle HorizontalAlign="Center" Width="100" />
                        </asp:BoundField>
                  
                    </Columns>
                </asp:GridView>--%>
                <asp:GridView Width="1200px" runat="server" ID="grdMaterials" DataKeyNames="MATID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None" style="margin-left:15px;margin-right:10px; margin-bottom:5px;" >
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White"  />
                    <RowStyle CssClass="AlterNateColor1" />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle BackColor="Black" ForeColor="White" CssClass="FixedHeader" Width="1195px" />
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
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DENSITY" HeaderText="DENSITY" SortExpression="DENSITY"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="MELTINDEX" HeaderText="MELT INDEX" SortExpression="MELTINDEX"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="MELTFRATE" HeaderText="MELT FLOW RATE" SortExpression="MELTFRATE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="PROCESS" HeaderText="PROCESS" SortExpression="PROCESS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="POLYMERSTRUCT" HeaderText="POLYMER STRUCTURE" SortExpression="POLYMERSTRUCT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="POLYMERDESC" HeaderText="POLYMER DESCRIPTION" SortExpression="POLYMERDESC"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="ETHYLENE" HeaderText="% ETHYLENE" SortExpression="ETHYLENE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="VISC" HeaderText="VISCOSITY" SortExpression="VISC" Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="TYPE" HeaderText="TYPE" SortExpression="TYPE" Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="ANHYDRIDEMOD" HeaderText="ANHYDRIDE MODIFICATION" SortExpression="ANHYDRIDEMOD"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="WATERACTIVITY" HeaderText="WATER ACTIVITY OF PRODUCT TO  BE APPLIED"
                            SortExpression="WATERACTIVITY" Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="ADHESIVE" HeaderText="ADHESIVE" SortExpression="ADHESIVE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                         <asp:BoundField DataField="RESINFAMILY" HeaderText="RESIN FAMILY" SortExpression="RESINFAMILY"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="DILUENT" HeaderText="DILUENT" SortExpression="DILUENT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="SOLVENT" HeaderText="SOLVENT" SortExpression="SOLVENT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                       
                        <asp:BoundField DataField="TECHDESC" HeaderText="TECHNICAL DESCRIPTION" SortExpression="TECHDESC"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="ALLOY" HeaderText="ALLOY" SortExpression="ALLOY" Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="TEMPER" HeaderText="TEMPER" SortExpression="TEMPER" Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBSTRATE" HeaderText="SUBSTRATE" SortExpression="SUBSTRATE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="FUNCTION" HeaderText="FUNCTION" SortExpression="FUNCTION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="FEATURE" HeaderText="FEATURE" SortExpression="FEATURE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PARTICLESIZE" HeaderText="PARTICLESIZE" SortExpression="PARTICLESIZE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBSTRATES" HeaderText="SUBSTRATE" SortExpression="SUBSTRATES"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="CURING" HeaderText="CURING" SortExpression="CURING" Visible="false">
                            <ItemStyle HorizontalAlign="Left"  />
                            <HeaderStyle HorizontalAlign="Left"  />
                        </asp:BoundField>
                        <asp:BoundField DataField="THICKNESS" HeaderText="THICKNESS" SortExpression="THICKNESS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="TECHDESCRIPTION" HeaderText="TECHNICAL DESCRIPTION" SortExpression="TECHDESCRIPTION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="RECYCLE" HeaderText="RECYCLE" SortExpression="RECYCLE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="SUBLAYERS" HeaderText="SUBSTRATE LAYERS" SortExpression="SUBLAYERS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="PRESIDE" HeaderText="PRETREAT SIDES" SortExpression="PRESIDE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"/>
                            <HeaderStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRETYPE" HeaderText="PRETREAT TYPE" SortExpression="PRETYPE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="MODIFIERS" HeaderText="MODIFIERS" SortExpression="MODIFIERS"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"/>
                            <HeaderStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="OUTCOATING" HeaderText="OUTSIDE COATING" SortExpression="OUTCOATING"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"/>
                            <HeaderStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="PRODOUTCOATING" HeaderText="PRODUCT SIDE COATING" SortExpression="PRODOUTCOATING"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="OXYGENBARR" HeaderText="OXYGEN BARRIER" SortExpression="OXYGENBARR"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left"/>
                            <HeaderStyle HorizontalAlign="Left"/>
                        </asp:BoundField>
                        <asp:BoundField DataField="MOISTUREBARR" HeaderText="MOISTURE BARRIER" SortExpression="MOISTUREBARR"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="APPLICATION" HeaderText="APPLICATION" SortExpression="APPLICATION"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="DILUENT" HeaderText="DILUENT" SortExpression="DILUENT"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
                        </asp:BoundField>
                        <asp:BoundField DataField="REACTIVE" HeaderText="REACTIVE" SortExpression="REACTIVE"
                            Visible="false">
                            <ItemStyle HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Left" />
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
		  <asp:HiddenField ID="hidMatVal" runat="server" />
		   <asp:HiddenField ID="hidSEQ" runat="server" />
    </div>
<asp:Button ID="btnCall" Text="" Width="1px" runat="server" CssClass="Button" Style="margin-left: 5px;display:none;" />
    </form>
</body>
</html>
