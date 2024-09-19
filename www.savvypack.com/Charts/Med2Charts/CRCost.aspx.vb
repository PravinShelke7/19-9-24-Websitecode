Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_Med2Charts_CRCost
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
                Dim objCryptoHelper As New CryptoHelper()
                Dim DsData As New DataTable
                Dim DsPref As New DataTable
                Dim CaseId1 As String = Session("Med2CaseId")
                Dim CaseId2 As String = objCryptoHelper.Decrypt(Request.QueryString("CaseId"))
                'Session("C1") = "7420"
                'Session("C2") = "89"
                'Session("User") = "Administrator"
                If Not IsPostBack Then
                    GetTypes()
                End If
                GetData(DsData, CaseId1, CaseId2)
                GetPref(DsPref, Session("UserId"))



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

    Protected Sub GetTypes()
        Try
            Dim StrSql As String = String.Empty
            Dim ds As New DataTable()
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()
            Dim IsDep As String = String.Empty
            Dim objCryptoHelper As New CryptoHelper()

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")
            IsDep = objCryptoHelper.Decrypt(Request.QueryString("IsDep").ToString())

            If IsDep = "N" Then


                StrSql = "SELECT 'Variable Cost' AS TYPEDES,  "
                StrSql = StrSql + "'VARIABLECOST' AS TYPE, "
                StrSql = StrSql + "'1' AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Fixed Cost' AS TYPEDES, "
                StrSql = StrSql + "'FIXEDCOST' AS TYPE, "
                StrSql = StrSql + "'2' AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Cost' AS TYPEDES, "
                StrSql = StrSql + "'TOTALCOST' AS TYPE, "
                StrSql = StrSql + "'2' AS SEQ "
                StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"
            Else

                StrSql = "SELECT 'Variable Cost With Depreciation' AS TYPEDES,  "
                StrSql = StrSql + "'VARIABLECOST' AS TYPE, "
                StrSql = StrSql + "'1' AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Fixed Cost With Depreciation' AS TYPEDES, "
                StrSql = StrSql + "'FIXEDCOSTDEP' AS TYPE, "
                StrSql = StrSql + "'2' AS SEQ "
                StrSql = StrSql + "FROM DUAL "
                StrSql = StrSql + "UNION "
                StrSql = StrSql + "SELECT 'Total Cost With Depreciation' AS TYPEDES, "
                StrSql = StrSql + "'TOTALCOSTDEP' AS TYPE, "
                StrSql = StrSql + "'3' AS SEQ "
                StrSql = StrSql + "FROM DUAL ORDER BY  SEQ"

            End If
            ds = odbutil.FillDataTable(StrSql, MyConnection)
            With ddltype
                .DataSource = ds
                .DataTextField = "TYPEDES"
                .DataValueField = "TYPE"
                .DataBind()
            End With

        Catch ex As Exception

        End Try
    End Sub

    Protected Sub GetData(ByRef Ds As DataTable, ByVal CaseId1 As String, ByVal CaseId2 As String)
        Try
            Dim StrSql As String = String.Empty
            Dim MyConnection As String = String.Empty
            Dim MyEConnection As String = String.Empty
            Dim odbutil As New DBUtil()

            MyConnection = System.Configuration.ConfigurationManager.AppSettings("MedEcon2ConnectionString")



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

    Protected Sub GetPref(ByRef DtsPref As DataTable, ByVal UserId As String)
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
            StrSql = StrSql + "WHERE USERID='" + UserId + "' "

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
            Dim Cnt As New Integer
            Dim SalesVolumeUnit As New Decimal
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title6").ToString() + "/Unit"

            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt
                If DsData.Rows(i).Item("FINVOLMSI") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMSI") * DsPref.Rows(0).Item("ConvArea")
                ElseIf DsData.Rows(i).Item("FINVOLMUNITS") > 0 Then
                    SalesVolumeUnit = DsData.Rows(i).Item("FINVOLMUNITS")
                End If


                Data = FormatNumber((DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr")) * 100 / SalesVolumeUnit, 3)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
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
            Dim i As New Integer

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("Title2").ToString() + "/" + DsPref.Rows(0).Item("Title8").ToString()
            lblHeading.Text = ddltype.SelectedItem.Text + " Cost Chart"
            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt
                SalesVolumeLb = DsData.Rows(i).Item("VOLUME") * DsPref.Rows(0).Item("CONVWT")

                Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr") / SalesVolumeLb, 3)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
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

            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"
            Cnt = DsData.Rows.Count - 1
            Pref = DsPref.Rows(0).Item("TITLE2").ToString() + ""
            lblHeading.Text = ddltype.SelectedItem.Text + " Chart"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(" + ddltype.SelectedItem.Text + ")"
            For i = 0 To Cnt

                Data = FormatNumber(DsData.Rows(i).Item("" + ddltype.SelectedItem.Value + "") * DsPref.Rows(0).Item("curr"), 3)

                pcScript &= "" + Graphtype + ".setSeries(" & DsData.Rows(i).Item("CASEDE") & ";" & Data & ")"
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
