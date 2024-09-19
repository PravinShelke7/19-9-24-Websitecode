Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_SMed1Charts_CStackErgyChart
    Inherits System.Web.UI.Page
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


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If Session("back") = "" Then
                lblsession.Visible = True
                Charts.Visible = False
            Else
                'Begin Embedder Code
                InitializeComponent()
                Dim DsData As New DataTable
                Dim DsPref As New DataTable
                Dim objCryptoHelper As New CryptoHelper()
                'Session("C1") = "7420"
                'Session("C2") = "89"
                'Session("User") = "Administrator"
                Dim CaseId1 As String = Session("SMed1CaseId")
                Dim CaseId2 As String = objCryptoHelper.Decrypt(Request.QueryString("CaseId").Replace("!Plus!", "+").Replace("!Hash!", "#").Replace("!And!", "&"))
                GetData(DsData, CaseId1, CaseId2)
                GetPref(DsPref, Session("USERID"))



                If ddlCnType.SelectedValue = "Total" Then
                    GetTotal(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "PUnit" Then
                    GetPerUnit(DsData, DsPref)
                ElseIf ddlCnType.SelectedValue = "PWeight" Then
                    GetPerWeight(DsData, DsPref)
                End If



            End If
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetData(ByRef Ds As DataTable, ByVal CaseId1 As String, ByVal CaseId2 As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("Sustain1ConnectionString")


            StrSql = "SELECT CASEID, RMERGY,  "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "WHERE CASEID IN  (" + CaseId1 + "," + CaseId2 + ") "

            StrSql = "SELECT  DISTINCT  "
            StrSql = StrSql + "TOTAL.CASEID, "
            StrSql = StrSql + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
            StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
            StrSql = StrSql + "FROM BASECASES "
            StrSql = StrSql + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
            StrSql = StrSql + ") ELSE "
            StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
            StrSql = StrSql + "FROM PERMISSIONSCASES "
            StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
            StrSql = StrSql + ") "
            StrSql = StrSql + "END  ) AS CASEDE, "
            StrSql = StrSql + "PREF.TITLE3, "
            StrSql = StrSql + "RESULTSPL.FINVOLMSI,"
            StrSql = StrSql + "RESULTSPL.FINVOLMUNITS,"
            StrSql = StrSql + "RESULTSPL.VOLUME, "
            StrSql = StrSql + "RMERGY, "
            StrSql = StrSql + "RMPACKERGY, "
            StrSql = StrSql + "RMANDPACKTRNSPTERGY, "
            StrSql = StrSql + "PROCERGY, "
            StrSql = StrSql + "DPPACKERGY, "
            StrSql = StrSql + "DPTRNSPTERGY, "
            StrSql = StrSql + "TRSPTTOCUS, "
            StrSql = StrSql + "(RMERGY+RMPACKERGY+DPPACKERGY)PURMATERIALERGY, "
            StrSql = StrSql + "(RMANDPACKTRNSPTERGY+DPTRNSPTERGY+TRSPTTOCUS)TRNSPTERGY, "
            StrSql = StrSql + "(RMERGY+RMPACKERGY+RMANDPACKTRNSPTERGY+PROCERGY+DPPACKERGY+DPTRNSPTERGY+TRSPTTOCUS)TOTALENERGY "
            StrSql = StrSql + "FROM TOTAL "
            StrSql = StrSql + "INNER JOIN RESULTSPL "
            StrSql = StrSql + "ON RESULTSPL.CASEID = TOTAL.CASEID "
            StrSql = StrSql + "INNER JOIN PREFERENCES PREF "
            StrSql = StrSql + "ON PREF.CASEID = TOTAL.CASEID "
            StrSql = StrSql + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "


            Ds = odbutil.FillDataTable(StrSql, MyConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPref(ByRef DtsPref As DataTable, ByVal USERID As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            StrSql = "SELECT  USERID,  "
            StrSql = StrSql + "UNITS, "
            StrSql = StrSql + "CURRENCY, "
            StrSql = StrSql + "CURR, "
            StrSql = StrSql + "CONVERSIONFACTOR, "
            StrSql = StrSql + "TITLE1, "
            StrSql = StrSql + "TITLE2, "
            StrSql = StrSql + "TITLE3, "
            StrSql = StrSql + "TITLE4, "
            StrSql = StrSql + "TITLE5, "
            StrSql = StrSql + "TITLE6, "
            StrSql = StrSql + "CONVAREA, "
            StrSql = StrSql + "TITLE7, "
            StrSql = StrSql + "TITLE8, "
            StrSql = StrSql + "TITLE9, "
            StrSql = StrSql + "TITLE10, "
            StrSql = StrSql + "TITLE11, "
            StrSql = StrSql + "TITLE12, "
            StrSql = StrSql + "CONVWT, "
            StrSql = StrSql + "CONVAREA2, "
            StrSql = StrSql + "CONVTHICK, "
            StrSql = StrSql + "CONVTHICK2, "
            StrSql = StrSql + "CONVTHICK3 "
            StrSql = StrSql + "FROM CHARTPREFERENCES "
            StrSql = StrSql + "WHERE USERID='" + USERID.ToString() + "' "

            DtsPref = odbutil.FillDataTable(StrSql, MyEConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GenrateChart(ByVal PcScript As String, ByVal GraphName As String, ByVal Pref As String)
        Try
            'Dim control As HtmlGenericControl = FindControl("EnergyComp")
            'Dim myImage As CordaEmbedder = New CordaEmbedder()



            'myImage.externalServerAddress = "http://192.168.100.22:2001"
            'myImage.internalCommPortAddress = "http://192.168.100.22:2002"
            'myImage.imageTemplate = "Sargento_KraftPouchvsTray" + ".itxml"
            'myImage.userAgent = Request.UserAgent
            'myImage.width = 700
            'myImage.height = 400
            'myImage.returnDescriptiveLink = True
            'myImage.language = "EN"
            'myImage.pcScript = PcScript + "Y-axis.SetText(" + Pref + ")"
            'myImage.outputType = "JPEG"
            'myImage.fallback = "STRICT"
            'control.InnerHtml = myImage.getEmbeddingHTML

            Dim control As HtmlGenericControl = FindControl("EnergyComp")
            Dim MyEConnection As String = String.Empty
            Dim dsChartSetting As New DataTable
            Dim StrSqlChartSetting As String = String.Empty
            Dim odbutil As New DBUtil()


            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"
            dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyEConnection)

            Dim myImage As CordaEmbedder = New CordaEmbedder()
            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            myImage.imageTemplate = "Sargento_KraftPouchvsTray_Html" + ".itxml"
            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = True
            myImage.language = "EN"
            myImage.pcScript = PcScript + "Y-axis.SetText(" + Pref + ")"
            myImage.outputType = "JPEG"
            myImage.fallback = "STRICT"
            control.InnerHtml = myImage.getEmbeddingHTML
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPerUnit(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try

            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim HoverText As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1


            lblHeading.Text = "Energy Per Unit Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = FormatNumber(DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea"), 0)
                    HoverText = "MJ/" + DsPref.Rows(0).Item("TITLE3").ToString()
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                    HoverText = "MJ/Unit"
                End If
                If i = 0 Then
                    Pref = HoverText
                End If

                Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeUnit, 3)
                Process = FormatNumber(DsData.Rows(i).Item("PROCERGY") / SalesVolumeUnit, 3)
                Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeUnit, 3)
                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") + "<br/>(" + HoverText.ToString() + ");" & Transportation & ";" & Process & ";" & Purchased & ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(1," + (i + 1).ToString() + ",Value:" + Purchased.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(2," + (i + 1).ToString() + ",Value:" + Process.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"
                pcScript = pcScript + "" + Graphtype + ".addHoverText(3," + (i + 1).ToString() + ",Value:" + Transportation.ToString() + "<br/>Unit:" + HoverText.ToString() + ")"

            Next

            GenrateChart(pcScript, GraphName, Pref)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPerWeight(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim SalesVolumeLb As New Decimal
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ/" + DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = "Energy Per Weight Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt
                SalesVolumeLb = FormatNumber(DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT"), 0)

                Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY") / SalesVolumeLb, 3)
                Process = FormatNumber(DsData.Rows(i).Item("PROCERGY") / SalesVolumeLb, 3)
                Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY") / SalesVolumeLb, 3)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetTotal(ByVal DsData As DataTable, ByVal DsPref As DataTable)
        Try
            Dim Data As String = String.Empty
            Dim Text As String = String.Empty
            Dim Pref As String = String.Empty
            Dim pcScript As String = String.Empty
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Cnt As New Integer
            Dim i As New Integer
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = "MJ"
            lblHeading.Text = "Total Energy Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            For i = 0 To Cnt

                Transportation = FormatNumber(DsData.Rows(i).Item("TRNSPTERGY"), 3)
                Process = FormatNumber(DsData.Rows(i).Item("PROCERGY"), 3)
                Purchased = FormatNumber(DsData.Rows(i).Item("PURMATERIALERGY"), 3)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
            Next

            GenrateChart(pcScript, GraphName, Pref)
        Catch ex As Exception

        End Try
    End Sub

    Protected Sub bynUpdate_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bynUpdate.Click
        Try

        Catch ex As Exception

        End Try
    End Sub

End Class
