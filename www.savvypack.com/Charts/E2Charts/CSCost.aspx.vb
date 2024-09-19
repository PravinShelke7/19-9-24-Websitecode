Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_E2Charts_CSCost
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
                'Session("C1") = "7420"
                'Session("C2") = "89"
                'Session("User") = "Administrator"
                GetData(DsData, Session("C1"), Session("C2"))
                GetPref(DsPref, Session("User"))



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

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("Econ2ConnectionString")


            StrSql = "SELECT RESULTSPL.CASEID,  "
            StrSql = StrSql + "( CASE WHEN RESULTSPL.CASEID <= 1000 THEN "
            StrSql = StrSql + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
            StrSql = StrSql + "FROM BASECASES "
            StrSql = StrSql + "WHERE  BASECASES.CASEID = RESULTSPL.CASEID "
            StrSql = StrSql + ") ELSE "
            StrSql = StrSql + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
            StrSql = StrSql + "FROM PERMISSIONSCASES "
            StrSql = StrSql + "WHERE  PERMISSIONSCASES.CASEID = RESULTSPL.CASEID "
            StrSql = StrSql + ") "
            StrSql = StrSql + "END  ) AS CASEDE, "
            StrSql = StrSql + "RESULTSPL.FINVOLMSI, "
            StrSql = StrSql + "RESULTSPL.FINVOLMUNITS, "
            StrSql = StrSql + "RESULTSPL.VOLUME, "
            StrSql = StrSql + "RESULTSPL.FIXEDCOSTDEP, "
            StrSql = StrSql + "RESULTSPL.TOTALCOSTDEP, "
            StrSql = StrSql + "RESULTSPL.VARIABLECOST, "
            StrSql = StrSql + "RESULTSPL.FIXEDCOST, "
            StrSql = StrSql + "RESULTSPL.TOTALCOST "
            StrSql = StrSql + "FROM RESULTSPL "
            StrSql = StrSql + "WHERE RESULTSPL.CASEID IN (" + CaseId1.ToString() + "," + CaseId2.ToString() + ") "



            Ds = odbutil.FillDataTable(StrSql, MyConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetPref(ByRef DtsPref As DataTable, ByVal UserName As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")

            StrSql = "SELECT  USERNAME,  "
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
            StrSql = StrSql + "WHERE USERNAME='" + UserName + "' "

            DtsPref = odbutil.FillDataTable(StrSql, MyEConnection)

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GenrateChart(ByVal PcScript As String, ByVal GraphName As String, ByVal Pref As String)
        Try
            Dim control As HtmlGenericControl = FindControl("CostComp")
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
            myImage.imageTemplate = "Sargento_KraftPouchvsTray" + ".itxml"
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
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer
            Dim VariableCost As New Decimal
            Dim FixedCost As New Decimal
            Dim TotalCost As New Decimal
            Dim IsDep As String = String.Empty

            IsDep = Request.QueryString("IsDep").ToString()

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title6").ToString() + "/Unit"

            lblHeading.Text = "Total Currency Per Unit Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Cost;Fixed Cost)"
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If

                If IsDep = "N" Then

                    VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)
                    FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)
                    TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)
                Else
                    VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)
                    FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)
                    TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)

                End If


                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
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
            Dim VariableCost As New Decimal
            Dim FixedCost As New Decimal
            Dim TotalCost As New Decimal
            Dim IsDep As String = String.Empty
            IsDep = Request.QueryString("IsDep").ToString()


            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = "Total Currency Per Weight Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Cost;Fixed Cost)"
            For i = 0 To Cnt
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")

                If IsDep = "N" Then
                    VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                    FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                    TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                Else
                    VariableCost = FormatNumber((DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                    FixedCost = FormatNumber((DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                    TotalCost = FormatNumber((DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr")) / SalesVolumeLb, 3)
                End If

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
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
            Dim VariableCost As New Decimal
            Dim FixedCost As New Decimal
            Dim TotalCost As New Decimal
            Dim IsDep As String = String.Empty
            IsDep = Request.QueryString("IsDep").ToString()


            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE2").ToString() + ""
            lblHeading.Text = "Total Currency Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Variable Cost;Fixed Cost)"
            For i = 0 To Cnt

                If IsDep = "N" Then
                    VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr"), 3)
                    FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOST") * DsPref.Rows(0).Item("curr"), 3)
                    TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOST") * DsPref.Rows(0).Item("curr"), 3)
                Else
                    VariableCost = FormatNumber(DsData.Rows(i).Item("VARIABLECOST") * DsPref.Rows(0).Item("curr"), 3)
                    FixedCost = FormatNumber(DsData.Rows(i).Item("FIXEDCOSTDEP") * DsPref.Rows(0).Item("curr"), 3)
                    TotalCost = FormatNumber(DsData.Rows(i).Item("TOTALCOSTDEP") * DsPref.Rows(0).Item("curr"), 3)
                End If

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & VariableCost & ";" & FixedCost & ")"
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
