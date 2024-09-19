Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Pages_SavvyPackPro_Popup_AllBatchChart
    Inherits System.Web.UI.Page
    Dim objGetdata As New SavvyProGetData
    Dim objUpIns As New SavvyProUpInsData
    'Dim dsvendor As New DataSet()    'Dim strvendor As String = ""    'Dim DsBatch As New DataSet()    'Dim odbutil As New DBUtil()    'Dim dsChartSetting As New DataTable()    'Dim StrSqlChartSetting As String = String.Empty    'Dim MyConfigConnection As String = String.Empty

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub


    'Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '    'CODEGEN: This method call is required by the Web Form Designer
    '    'Do not modify it using the code editor.
    '    InitializeComponent()
    'End Sub

#End Region

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        Try
            Dim rfpprice As String = Request.QueryString("RfpPriceID").ToString()
            Dim dsgroup As DataSet
            Dim trHeader As TableRow
            Dim tdHeader As TableCell
            Dim div1 As HtmlGenericControl
            Dim lbl As Label

            dsgroup = objGetdata.GetGrpDETByBatchLine(rfpprice.ToString(), "", Session("UserId"), Session("hidRfpID"))
            For i As Integer = 0 To dsgroup.Tables(0).Rows.Count - 1
                div1 = New HtmlGenericControl
                lbl = New Label
                lbl.ID = "lbl" + i.ToString()
                div1.ID = "div1" + i.ToString()
                tdHeader = New TableCell
                trHeader = New TableRow
                lbl.Font.Bold = True
                lbl.Visible = False
                tdHeader.Controls.Add(lbl)
                tdHeader.Controls.Add(div1)
                trHeader.Controls.Add(tdHeader)
                tblChart.Controls.Add(trHeader)
                GenerateGraphLineBatch(dsgroup.Tables(0).Rows(i).Item("OTHERPREFRFPID").ToString(), dsgroup.Tables(0).Rows(i).Item("CODE").ToString(), div1, lbl)
            Next
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "unloadgif", "UnLoafGif();", True)
        Catch ex As Exception
            Page.ClientScript.RegisterStartupScript(Me.GetType(), "unloadgif", "UnLoafGif();", True)
        End Try
    End Sub

    Protected Sub GenerateGraphLineBatch(ByVal mgroupid As String, ByVal groupdes As String, ByVal divs As HtmlGenericControl, ByVal lbl As Label)        Dim pcScript = ""        Dim odbutil As New DBUtil()        Dim Graphtype As String = ""        Dim GraphName As String = ""        'InitializeComponent()        Dim UserName As String = ""        UserName = Session("UserName")        Graphtype = "Material_Price"        GraphName = "MaterialPrice1_NEW"        Dim Count As Integer        Dim conwt As String = ""        Dim unit As String = ""        Dim curr As String = ""        Dim dsvendor As DataSet        dsvendor = objGetdata.GetNoOfVendorBatchByRFPIDPO(Session("hidRfpID"), Request.QueryString("RfpPriceID").ToString())        Dim strvendor As String = ""        Dim dsprice As DataSet        Dim dsgroup As DataSet        For i As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1            strvendor = dsvendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "," + strvendor        Next        dsprice = objGetdata.GetPriceByBatchLine(mgroupid, strvendor.ToString().Remove(strvendor.Length - 1))        dsgroup = objGetdata.GetGrpByBatchLine()        Count = dsgroup.Tables(0).Rows.Count        Dim Group(Count) As String        Dim Groupid(Count) As String        For i = 0 To Count - 1            Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()        Next        Dim strTit(20) As String        Dim price As New Double        Dim dvC As New DataView        Dim dtC As New DataTable        Dim strvendors As String = ""        If dsprice.Tables(0).Rows.Count > 0 Then            lbl.Visible = True            lbl.Text = groupdes + "<br>"            If dsvendor.Tables(0).Rows.Count > 0 Then                For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1                    Dim strv As String = dsvendor.Tables(0).Rows(m).Item("VENDORNM").ToString()                    strvendors = strvendors + strv.Replace(",", "") + ";"                Next            End If            strvendors = strvendors.Remove(strvendors.Length - 1)            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"            ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"            Dim strHov As String = ""            Dim i As New Integer            For i = 0 To Count - 1                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"                For j = 0 To dsvendor.Tables(0).Rows.Count - 1                    dvC = dsprice.Tables(0).DefaultView                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "                    dtC = dvC.ToTable()                    If dtC.Rows.Count > 0 Then                        ' strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)                            pcScript &= "(STYP_13)" & price & ";"                        End If                    Else                        pcScript &= "(STYP_13);"                    End If                Next                pcScript &= ")"                'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"                For j = 0 To dsvendor.Tables(0).Rows.Count - 1                    dvC = dsprice.Tables(0).DefaultView                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "                    dtC = dvC.ToTable()                    If dtC.Rows.Count > 0 Then                        strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then                        End If                    Else                        strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "                    End If                    pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"                Next            Next        Else
            lbl.Visible = True            lbl.Text = groupdes + "<br>"            If dsvendor.Tables(0).Rows.Count > 0 Then                For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1                    Dim strv As String = dsvendor.Tables(0).Rows(m).Item("VENDORNM").ToString()                    strvendors = strvendors + strv.Replace(",", "") + ";"                Next            End If            strvendors = strvendors.Remove(strvendors.Length - 1)            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"            ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"            Dim strHov As String = ""            Dim i As New Integer            For i = 0 To Count - 1                pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"                For j = 0 To dsvendor.Tables(0).Rows.Count - 1                    dvC = dsprice.Tables(0).DefaultView                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "                    dtC = dvC.ToTable()                    If dtC.Rows.Count > 0 Then                        ' strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then                            price = FormatNumber(dtC.Rows(0).Item("Price"), 4)                            pcScript &= "(STYP_13)" & price & ";"                        End If                    Else                        pcScript &= "(STYP_13);"                    End If                Next                pcScript &= ")"                'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"                For j = 0 To dsvendor.Tables(0).Rows.Count - 1                    dvC = dsprice.Tables(0).DefaultView                    dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "                    dtC = dvC.ToTable()                    If dtC.Rows.Count > 0 Then                        strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()                        If dtC.Rows(0).Item("PRICE").ToString() <> "" Then                        End If                    Else                        strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "                    End If                    pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"                Next            Next        End If        divs.Visible = True        Dim dsChartSetting As New DataTable        Dim StrSqlChartSetting As String = String.Empty        Dim MyConfigConnection As String = String.Empty        MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")        StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"        dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)        Dim myImage As CordaEmbedder = New CordaEmbedder()        myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()        myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()        myImage.imageTemplate = GraphName + ".itxml"        myImage.userAgent = Request.UserAgent        myImage.width = 400        myImage.height = 253        myImage.returnDescriptiveLink = True        myImage.language = "EN"        myImage.pcScript = pcScript + "Y-axis.SetText()"        myImage.outputType = "FLASH"        myImage.fallback = "STRICT"        divs.InnerHtml = myImage.getEmbeddingHTML    End Sub

    'Protected Sub GenerateGraphLineBatch(ByVal mgroupid As String, ByVal groupdes As String, ByVal divs As HtmlGenericControl, ByVal lbl As Label)    '    Dim pcScript = ""    '    Dim odbutil As New DBUtil()    '    Dim Graphtype As String = ""    '    Dim GraphName As String = ""    '    Graphtype = "Material_Price"    '    GraphName = "MaterialPrice1_NEW"    '    Dim Count As Integer    '    Dim conwt As String = ""    '    Dim unit As String = ""    '    Dim curr As String = ""    '    Dim dsvendor As DataSet    '    dsvendor = objGetdata.GetNoOfVendorBatchByRFPIDPO(Session("hidRfpID"), Request.QueryString("RfpPriceID").ToString())    '    Dim strvendor As String = ""    '    Dim dsprice As DataSet    '    Dim dsgroup As DataSet    '    For i As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1    '        strvendor = dsvendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "," + strvendor    '    Next    '    dsprice = objGetdata.GetPriceByBatchLine(mgroupid, strvendor.ToString().Remove(strvendor.Length - 1))    '    dsgroup = objGetdata.GetGrpByBatchLine()    '    Count = dsgroup.Tables(0).Rows.Count    '    Dim Group(Count) As String    '    Dim Groupid(Count) As String    '    For i = 0 To Count - 1    '        Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()    '    Next    '    Dim strTit(20) As String    '    Dim price As New Double    '    Dim dvC As New DataView    '    Dim dtC As New DataTable    '    Dim strvendors As String = ""    '    lbl.Visible = True    '    lbl.Text = groupdes + "<br>"    '    If dsvendor.Tables(0).Rows.Count > 0 Then    '        For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1    '            Dim strv As String = dsvendor.Tables(0).Rows(m).Item("VENDORNM").ToString()    '            strvendors = strvendors + strv.Replace(",", "") + ";"    '        Next    '    End If    '    strvendors = strvendors.Remove(strvendors.Length - 1)    '    pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"    '    ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"    '    Dim strHov As String = ""    '    Dim i As New Integer    '    For i = 0 To Count - 1    '        pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"    '        For j = 0 To dsvendor.Tables(0).Rows.Count - 1    '            dvC = dsprice.Tables(0).DefaultView    '            dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "    '            dtC = dvC.ToTable()    '            If dtC.Rows.Count > 0 Then    '                ' strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()    '                If dtC.Rows(0).Item("PRICE").ToString() <> "" Then    '                    price = FormatNumber(dtC.Rows(0).Item("Price"), 4)    '                    pcScript &= "(STYP_13)" & price & ";"    '                End If    '            Else    '                pcScript &= "(STYP_13);"    '            End If    '        Next    '        pcScript &= ")"    '        'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"    '        For j = 0 To dsvendor.Tables(0).Rows.Count - 1    '            dvC = dsprice.Tables(0).DefaultView    '            dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "    '            dtC = dvC.ToTable()    '            If dtC.Rows.Count > 0 Then    '                strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()    '                If dtC.Rows(0).Item("PRICE").ToString() <> "" Then    '                End If    '            Else    '                strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "    '            End If    '            pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"    '        Next    '    Next    '    divs.Visible = True    '    Dim dsChartSetting As New DataTable    '    Dim StrSqlChartSetting As String = String.Empty    '    Dim MyConfigConnection As String = String.Empty    '    MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")    '    StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"    '    dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)    '    Dim myImage As CordaEmbedder = New CordaEmbedder()    '    myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()    '    myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()    '    myImage.imageTemplate = GraphName + ".itxml"    '    myImage.userAgent = Request.UserAgent    '    myImage.width = 400    '    myImage.height = 253    '    myImage.returnDescriptiveLink = True    '    myImage.language = "EN"    '    myImage.pcScript = pcScript + "Y-axis.SetText()"    '    myImage.outputType = "FLASH"    '    myImage.fallback = "STRICT"    '    divs.InnerHtml = myImage.getEmbeddingHTML    'End Sub

    'Protected Sub GenerateGraphLineBatch(ByVal mgroupid As String, ByVal groupdes As String, ByVal divs As HtmlGenericControl, ByVal lbl As Label, ByVal cnt As Integer)    '    Dim pcScript = ""    '    Dim Graphtype As String = ""    '    Dim GraphName As String = ""    '    Graphtype = "Material_Price"    '    GraphName = "MaterialPrice1_NEW"    '    Dim Count As Integer    '    Dim conwt As String = ""    '    Dim unit As String = ""    '    Dim curr As String = ""    '    Dim dsprice As DataSet    '    If cnt = 0 Then    '        MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")    '        StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"    '        dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)    '        DsBatch = objGetdata.GetGrpByBatchLine()    '        dsvendor = objGetdata.GetNoOfVendorBatchByRFPIDPO(Session("hidRfpID"), Request.QueryString("RfpPriceID").ToString())
    '        For i As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1    '            strvendor = dsvendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "," + strvendor    '        Next
    '    End If    '    dsprice = objGetdata.GetPriceByBatchLine(mgroupid, strvendor.ToString().Remove(strvendor.Length - 1))    '    Count = DsBatch.Tables(0).Rows.Count    '    Dim Group(Count) As String    '    Dim Groupid(Count) As String    '    For i = 0 To Count - 1    '        Group(i) = DsBatch.Tables(0).Rows(i).Item("BATCHVALUE").ToString()    '    Next    '    Dim strTit(20) As String    '    Dim price As New Double    '    Dim dvC As New DataView    '    Dim dtC As New DataTable    '    Dim strvendors As String = ""    '    If dsprice.Tables(0).Rows.Count > 0 Then    '        lbl.Visible = True    '        lbl.Text = groupdes + "<br>"    '        If dsvendor.Tables(0).Rows.Count > 0 Then    '            For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1    '                Dim strv As String = dsvendor.Tables(0).Rows(m).Item("VENDORNM").ToString()    '                strvendors = strvendors + strv.Replace(",", "") + ";"    '            Next    '        End If    '        strvendors = strvendors.Remove(strvendors.Length - 1)    '        pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"    '        ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"    '        Dim strHov As String = ""    '        Dim i As New Integer    '        For i = 0 To Count - 1    '            pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1    '                dvC = dsprice.Tables(0).DefaultView    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "    '                dtC = dvC.ToTable()    '                If dtC.Rows.Count > 0 Then    '                    ' strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then    '                        price = FormatNumber(dtC.Rows(0).Item("Price"), 4)    '                        pcScript &= "(STYP_13)" & price & ";"    '                    End If    '                Else    '                    pcScript &= "(STYP_13);"    '                End If    '            Next    '            pcScript &= ")"    '            'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1    '                dvC = dsprice.Tables(0).DefaultView    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "    '                dtC = dvC.ToTable()    '                If dtC.Rows.Count > 0 Then    '                    strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then    '                    End If    '                Else    '                    strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "    '                End If    '                pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"    '            Next    '        Next    '        divs.Visible = True    '        Dim myImage As CordaEmbedder = New CordaEmbedder()    '        myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()    '        myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()    '        myImage.imageTemplate = GraphName + ".itxml"    '        myImage.userAgent = Request.UserAgent    '        myImage.width = 400    '        myImage.height = 253    '        myImage.returnDescriptiveLink = True    '        myImage.language = "EN"    '        myImage.pcScript = pcScript + "Y-axis.SetText()"    '        myImage.outputType = "FLASH"    '        myImage.fallback = "STRICT"    '        divs.InnerHtml = myImage.getEmbeddingHTML    '    End If    'End Sub

    'Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
    '    Dim rfpprice As String = Request.QueryString("RfpPriceID").ToString()
    '    Dim dsgroup As DataSet
    '    Dim trHeader As TableRow
    '    Dim tdHeader As TableCell
    '    Dim div1 As HtmlGenericControl
    '    Dim lbl As Label
    '    Dim dsvendor As DataSet    '    Dim strvendor As String = ""    '    Dim DsBatch As New DataSet()    '    Dim odbutil As New DBUtil()    '    Dim dsChartSetting As New DataTable()    '    Dim StrSqlChartSetting As String = String.Empty    '    Dim MyConfigConnection As String = String.Empty    '    MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")    '    StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"    '    dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)    '    DsBatch = objGetdata.GetGrpByBatchLine()    '    dsvendor = objGetdata.GetNoOfVendorBatchByRFPIDPO(Session("hidRfpID"), Request.QueryString("RfpPriceID").ToString())
    '    For i As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1    '        strvendor = dsvendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "," + strvendor    '    Next

    '    dsgroup = objGetdata.GetGrpDETByBatchLine(rfpprice.ToString(), "")
    '    For i As Integer = 0 To dsgroup.Tables(0).Rows.Count - 1
    '        div1 = New HtmlGenericControl
    '        lbl = New Label
    '        lbl.ID = "lbl" + i.ToString()
    '        div1.ID = "div1" + i.ToString()
    '        tdHeader = New TableCell
    '        trHeader = New TableRow
    '        lbl.Font.Bold = True
    '        lbl.Visible = False
    '        tdHeader.Controls.Add(lbl)
    '        tdHeader.Controls.Add(div1)
    '        trHeader.Controls.Add(tdHeader)
    '        tblChart.Controls.Add(trHeader)
    '        GenerateGraphLineBatch(dsgroup.Tables(0).Rows(i).Item("OTHERPREFRFPID").ToString(), dsgroup.Tables(0).Rows(i).Item("CODE").ToString(), div1, lbl, strvendor, _
    '                               dsvendor, DsBatch, dsChartSetting)
    '    Next
    'End Sub

    'Protected Sub GenerateGraphLineBatch(ByVal mgroupid As String, ByVal groupdes As String, ByVal divs As HtmlGenericControl, ByVal lbl As Label, _    '                                     ByVal strvendor As String, ByVal dsvendor As DataSet, ByVal DsBatch As DataSet, ByVal dsChartSetting As DataTable)    '    Dim pcScript = ""    '    Dim Graphtype As String = ""    '    Dim GraphName As String = ""    '    Graphtype = "Material_Price"    '    GraphName = "MaterialPrice1_NEW"    '    Dim Count As Integer    '    Dim conwt As String = ""    '    Dim unit As String = ""    '    Dim curr As String = ""    '    Dim dsprice As DataSet    '    dsprice = objGetdata.GetPriceByBatchLine(mgroupid, strvendor.ToString().Remove(strvendor.Length - 1))    '    Count = DsBatch.Tables(0).Rows.Count    '    Dim Group(Count) As String    '    Dim Groupid(Count) As String    '    For i = 0 To Count - 1    '        Group(i) = DsBatch.Tables(0).Rows(i).Item("BATCHVALUE").ToString()    '    Next    '    Dim strTit(20) As String    '    Dim price As New Double    '    Dim dvC As New DataView    '    Dim dtC As New DataTable    '    Dim strvendors As String = ""    '    If dsprice.Tables(0).Rows.Count > 0 Then    '        lbl.Visible = True    '        lbl.Text = groupdes + "<br>"    '        If dsvendor.Tables(0).Rows.Count > 0 Then    '            For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1    '                Dim strv As String = dsvendor.Tables(0).Rows(m).Item("VENDORNM").ToString()    '                strvendors = strvendors + strv.Replace(",", "") + ";"    '            Next    '        End If    '        strvendors = strvendors.Remove(strvendors.Length - 1)    '        pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"    '        ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"    '        Dim strHov As String = ""    '        Dim i As New Integer    '        For i = 0 To Count - 1    '            pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1    '                dvC = dsprice.Tables(0).DefaultView    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "    '                dtC = dvC.ToTable()    '                If dtC.Rows.Count > 0 Then    '                    ' strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then    '                        price = FormatNumber(dtC.Rows(0).Item("Price"), 4)    '                        pcScript &= "(STYP_13)" & price & ";"    '                    End If    '                Else    '                    pcScript &= "(STYP_13);"    '                End If    '            Next    '            pcScript &= ")"    '            'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1    '                dvC = dsprice.Tables(0).DefaultView    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "' "    '                dtC = dvC.ToTable()    '                If dtC.Rows.Count > 0 Then    '                    strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then    '                    End If    '                Else    '                    strHov = dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("VENDORNM").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "    '                End If    '                pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"    '            Next    '        Next    '        divs.Visible = True    '        Dim myImage As CordaEmbedder = New CordaEmbedder()    '        myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()    '        myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()    '        myImage.imageTemplate = GraphName + ".itxml"    '        myImage.userAgent = Request.UserAgent    '        myImage.width = 400    '        myImage.height = 253    '        myImage.returnDescriptiveLink = True    '        myImage.language = "EN"    '        myImage.pcScript = pcScript + "Y-axis.SetText()"    '        myImage.outputType = "FLASH"    '        myImage.fallback = "STRICT"    '        divs.InnerHtml = myImage.getEmbeddingHTML    '    End If    'End Sub    

    'Protected Sub GenerateGraphLineBatch(ByVal mgroupid As String, ByVal groupdes As String, ByVal divs As HtmlGenericControl, ByVal lbl As Label)
    '    Dim pcScript = ""
    '    Dim odbutil As New DBUtil()
    '    Dim Graphtype As String = ""
    '    Dim GraphName As String = ""
    '    InitializeComponent()

    '    Dim UserName As String = ""
    '    UserName = Session("UserName")
    '    Graphtype = "Material_Price"
    '    GraphName = "MaterialPrice1_NEW"
    '    Dim Count As Integer

    '    Dim conwt As String = ""
    '    Dim unit As String = ""
    '    Dim curr As String = ""
    '    Dim dsvendor As DataSet
    '    dsvendor = objGetdata.GetVendorListByUserID("", Session("UserId"))
    '    Dim strvendor As String = ""
    '    Dim dsprice As DataSet
    '    Dim dsgroup As DataSet
    '    For i As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
    '        strvendor = dsvendor.Tables(0).Rows(i).Item("VENDORID").ToString() + "," + strvendor
    '    Next

    '    dsprice = objGetdata.GetPriceByBatchLine(mgroupid, strvendor.ToString().Remove(strvendor.Length - 1))
    '    dsgroup = objGetdata.GetGrpByBatchLine()

    '    Count = dsgroup.Tables(0).Rows.Count
    '    Dim Group(Count) As String
    '    Dim Groupid(Count) As String
    '    For i = 0 To Count - 1
    '        Group(i) = dsgroup.Tables(0).Rows(i).Item("BATCHVALUE").ToString()
    '    Next
    '    Dim strTit(20) As String
    '    Dim price As New Double
    '    Dim dvC As New DataView
    '    Dim dtC As New DataTable
    '    Dim strvendors As String = ""
    '    If dsprice.Tables(0).Rows.Count > 0 Then
    '        lbl.Visible = True
    '        lbl.Text = groupdes + "<br>"
    '        If dsvendor.Tables(0).Rows.Count > 0 Then
    '            For m As Integer = 0 To dsvendor.Tables(0).Rows.Count - 1
    '                Dim strv As String = dsvendor.Tables(0).Rows(m).Item("FIRSTNAME").ToString()
    '                strvendors = strvendors + strv.Replace(",", "") + ";"
    '            Next
    '        End If
    '        strvendors = strvendors.Remove(strvendors.Length - 1)
    '        pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + strvendors + ")"
    '        ' pcScript &= "" + Graphtype + ".addHoverText(" + strTit(x - 1) + "," & effdate1(i) & ",PRICE: " & FormatNumber(price, 4).Replace(",", "") & "\n Date: " & effdate1(i) & "\n Material: " & strTit(x - 1) + ")"
    '        Dim strHov As String = ""
    '        Dim i As New Integer
    '        For i = 0 To Count - 1
    '            pcScript &= "" + Graphtype + ".setSeries(" & Group(i) & ";"
    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1
    '                dvC = dsprice.Tables(0).DefaultView
    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
    '                dtC = dvC.ToTable()
    '                If dtC.Rows.Count > 0 Then
    '                    ' strHov = dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()
    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then
    '                        price = FormatNumber(dtC.Rows(0).Item("Price"), 4)
    '                        pcScript &= "(STYP_13)" & price & ";"
    '                    End If
    '                Else
    '                    pcScript &= "(STYP_13);"
    '                End If
    '            Next
    '            pcScript &= ")"
    '            'pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"
    '            For j = 0 To dsvendor.Tables(0).Rows.Count - 1
    '                dvC = dsprice.Tables(0).DefaultView
    '                dvC.RowFilter = "BATCHVALUE='" + Group(i).ToString() + "' and FIRSTNAME='" + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "' "
    '                dtC = dvC.ToTable()

    '                If dtC.Rows.Count > 0 Then
    '                    strHov = dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : " + dtC.Rows(0).Item("PRICE").ToString()
    '                    If dtC.Rows(0).Item("PRICE").ToString() <> "" Then

    '                    End If
    '                Else
    '                    strHov = dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + ", " + Group(i).ToString() + ",Vendor: " + dsvendor.Tables(0).Rows(j).Item("FIRSTNAME").ToString() + "\nBatch Value :" + Group(i).ToString() + "\nprice : "
    '                End If
    '                pcScript &= "" + Graphtype + ".addHoverText(" + strHov + ")"

    '            Next
    '        Next
    '        divs.Visible = True
    '        'Dim dsChartSetting As New DataTable
    '        'Dim objGetData1 As New Configration.Selectdata()
    '        'dsChartSetting = objGetData1.GetChartSettings().Tables(0)

    '        'Dim myImage As CordaEmbedder = New CordaEmbedder()
    '        'myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
    '        'myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
    '        'myImage.imageTemplate = GraphName + ".itxml"
    '        'myImage.userAgent = Request.UserAgent
    '        'myImage.width = 500
    '        'myImage.height = 300
    '        'myImage.returnDescriptiveLink = False
    '        'myImage.language = "EN"
    '        Dim dsChartSetting As New DataTable
    '        Dim StrSqlChartSetting As String = String.Empty
    '        Dim MyConfigConnection As String = String.Empty
    '        MyConfigConnection = System.Configuration.ConfigurationManager.AppSettings("ConfigurationConnectionString")


    '        StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"
    '        dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyConfigConnection)
    '        Dim myImage As CordaEmbedder = New CordaEmbedder()
    '        myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
    '        myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
    '        myImage.imageTemplate = GraphName + ".itxml"
    '        myImage.userAgent = Request.UserAgent
    '        myImage.width = 400
    '        myImage.height = 265
    '        myImage.returnDescriptiveLink = True
    '        myImage.language = "EN"
    '        myImage.pcScript = pcScript + "Y-axis.SetText()"
    '        myImage.outputType = "FLASH"
    '        myImage.fallback = "STRICT"

    '        divs.InnerHtml = myImage.getEmbeddingHTML
    '    End If

    'End Sub

End Class
