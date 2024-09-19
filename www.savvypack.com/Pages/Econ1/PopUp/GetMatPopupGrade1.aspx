<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GetMatPopupGrade1.aspx.vb" Inherits="Pages_Econ1_PopUp_GetMatPopupGrade1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>E1-Get Materials</title>
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

            document.getElementById('<%= hvGroupQ.ClientID%>').value = window.opener.document.getElementById("ctl00_Econ1ContentPlaceHolder_hidMatCat" + i).value;

            if (document.getElementById('<%= hvGroupQ.ClientID%>').value == "") {

            }
            else {

                document.getElementById('<%= btnCall.ClientID%>').click();
            }



        }

        function MaterialDet(MatDes, MatId, GradeName, GradeId, Weight, SG, MatDe3, mouseovervalue) {
            alert("---matdes" + MatDes + "---MatID : :" + MatId + "---Gradename : :" + GradeName + '-----Gradeid : :' + GradeId + '----Weight : : ' + Weight + "---SG : : " + SG + "----Matde3 : :" + MatDe3 + "-------------------------" + mouseovervalue);

            var hidMatdes = document.getElementById('<%= hidMatdes.ClientID%>').value;
            var hidMatId = document.getElementById('<%= hidMatId.ClientID%>').value;
            var hidGradedes = document.getElementById('<%= hidGradedes.ClientID%>').value;
            var hidGradeId = document.getElementById('<%= hidGradeid.ClientID%>').value;
            var hidSGV = document.getElementById('<%= hidSG.ClientID%>').value;
            var MatName = document.getElementById('<%= hidNew.ClientID%>').value;
           
            var i = hidMatId;
            i = i.match(/\d+$/)[0];

            if (Weight != SG) {
                window.opener.document.getElementById(hidSGV).value = "0.000";
            }
            else {
                window.opener.document.getElementById(hidSGV).value = "0.000";
            }
           

            if (MatName == "") {
                if (MatDes.length > 38) {
           
                    var Name = MatDes.substring(0, 20);
                    var Name1 = MatDes.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
           
                    var Name = MatDes.substring(0, 20);
                    var Name1 = MatDes.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                if (MatDe3 != "") {
           
                    window.opener.document.getElementById(hidMatdes).innerText = MatId+":"+MatDe3.replace(new RegExp("&#", 'g'), "'");
           
                
                }
                else {

                    window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + Name;
           
                    
                }
           
                window.opener.document.getElementById(hidMatId).value = MatId;
            }
            else {
                if (MatName.length > 38) {
           
                    var Name = MatName.substring(0, 20);
                    var Name1 = MatName.substring(20, 38);
                    Name = Name.concat(" ", Name1, "...");
                }
                else {
           
                    var Name = MatName.substring(0, 20);
                    var Name1 = MatName.substring(20, 38);
                    Name = Name.concat(" ", Name1);
                }
                if (MatDe3 != "") {

                    window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + MatDe3.replace(new RegExp("&#", 'g'), "'");
           
                
                }
                else {

                    window.opener.document.getElementById(hidMatdes).innerText = MatId + ":" + Name;
           
               
                }
           
                window.opener.document.getElementById(hidMatId).value = MatId;
            }
           

            if (document.getElementById('<%= hidMod.ClientID%>').value == 'E1') {

                if (MatId == "0") {
                    window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                    window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
                }
                else {
                    if (MatDe3 != "") {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "none";
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "inline";
                    }
                    else {
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgDis' + i).style.display = "inline";
                        window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_imgBut' + i).style.display = "none";
                    }
                }
            }
           

            window.opener.document.getElementById(hidGradedes).innerText = GradeName;
            window.opener.document.getElementById(hidGradeId).value = GradeId;
           // alert(":v: "+GradeId);
            opener.ShowToolTip('ctl00_Econ1ContentPlaceHolder_hypGradeDes' + i, mouseovervalue);
            //window.opener.document.getElementById('ctl00_Econ1ContentPlaceHolder_SG' + i).innerText = "0.000"
            //opener.ShowToolTip(hidGradedes, 'View Supplier Grade');
           
           //alert("b");
            document.getElementById('<%= hidMatdes.ClientID%>').value = '';
            document.getElementById('<%= hidMatId.ClientID%>').value = '';
            document.getElementById('<%= hidGradedes.ClientID%>').value = '';
            document.getElementById('<%= hidGradeid.ClientID%>').value = '';
            document.getElementById('<%= hidNew.ClientID%>').value = '';
            document.getElementById('<%= hidSG.ClientID%>').value = '';
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
            <table style="width: 600px;" cellpadding="4" cellspacing="0">
                
            </table>
           <div id="Materials" runat="Server" style="width:1240px; height: 550px; overflow: auto;margin-right:10px">              

                <asp:GridView Width="1200px" runat="server" ID="grdMaterials" DataKeyNames="MATID"
                    AutoGenerateSelectButton="false" AllowPaging="false" AllowSorting="true" AutoGenerateColumns="False"
                    ShowHeader="true" CellPadding="4" CellSpacing="2" ForeColor="#333333" GridLines="None" style="margin-left:15px;margin-right:10px; margin-bottom:10px;" >
                    <FooterStyle BackColor="#32659A" Font-Bold="True" ForeColor="White"  />
                    <RowStyle CssClass="AlterNateColor1"  />
                    <AlternatingRowStyle CssClass="AlterNateColor2" />
                    <HeaderStyle BackColor="Black" ForeColor="White" CssClass="FixedHeader" Width="1195px"  />
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
        <asp:HiddenField ID="hidNew" runat="server" />
        <asp:HiddenField ID="hidMod" runat="server" />
        <asp:HiddenField ID="hidDMatId" runat="server" />


    </div>
<asp:Button ID="btnCall" Text="" Width="1px" runat="server" CssClass="Button" Style="margin-left: 5px;display:none;" />
    </form>
</body>
</html>
