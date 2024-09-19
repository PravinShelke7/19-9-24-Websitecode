Imports Corda
Imports System.Web.UI.HtmlControls
Imports System.Data
Imports System.Data.OleDb
Imports System.Web.HttpBrowserCapabilities
Partial Class Charts_Sustain2_GreenHousePerWeightCaseComp
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

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            If Session("back") = "" Then
                lblsession.Visible = True
                Charts.Visible = False
            Else

                'Begin Embedder Code
                InitializeComponent()


                Dim odbutil As New DBUtil()
                Dim CaseId1 As String = String.Empty
                Dim CaseId2 As String = String.Empty
                Dim YearValue As String = String.Empty
                Dim UserName As String = String.Empty
                Dim Unit As String = String.Empty
                Dim StrSqlPref As String = String.Empty
                Dim MyConnection As String = String.Empty
                Dim MyEConnection As String = String.Empty
                Dim StrSQl As String = String.Empty
                Dim DtsPref As New DataTable()
                Dim Dts As New DataTable()


                UserName = Session("User")
                CaseId1 = Session("C1")
                CaseId2 = Session("C2")

                MyConnection = System.Configuration.ConfigurationManager.AppSettings("Sustain2ConnectionString")
                MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")







                StrSQl = "SELECT  DISTINCT  "
                StrSQl = StrSQl + "TOTAL.CASEID, "
                StrSQl = StrSQl + "( CASE WHEN TOTAL.CASEID <= 1000 THEN "
                StrSQl = StrSQl + "( SELECT (BASECASES.CASEID||':'||BASECASES.CASEDE1||' '||BASECASES.CASEDE2 ) "
                StrSQl = StrSQl + "FROM BASECASES "
                StrSQl = StrSQl + "WHERE  BASECASES.CASEID = TOTAL.CASEID "
                StrSQl = StrSQl + ") ELSE "
                StrSQl = StrSQl + "( SELECT DISTINCT (PERMISSIONSCASES.CASEID||':'||NVL(PERMISSIONSCASES.CASEDE1,'') || ' ' || NVL(PERMISSIONSCASES.CASEDE2,'') ) "
                StrSQl = StrSQl + "FROM PERMISSIONSCASES "
                StrSQl = StrSQl + "WHERE  PERMISSIONSCASES.CASEID = TOTAL.CASEID "
                StrSQl = StrSQl + ") "
                StrSQl = StrSQl + "END  ) AS CASEDE, "
                StrSQl = StrSQl + "(CASE WHEN RESULTSPL.PVOLUSE = 0 THEN "
                StrSQl = StrSQl + "RESULTSPL.VOLUME "
                StrSQl = StrSQl + "ELSE "
                StrSQl = StrSQl + "RESULTSPL.SVOLUME "
                StrSQl = StrSQl + "END "
                StrSQl = StrSQl + ")SALESVOLUMELB, "
                StrSQl = StrSQl + "(RMGRNHUSGAS+RMPACKGRNHUSGAS+DPPACKGRNHUSGAS+PACKGEDPRODPACKGRNHUSGAS) AS ""Purchased Materials"", "
                StrSQl = StrSQl + "(PROCGRNHUSGAS+PROCGRNHUSGASS2) AS ""Process"", "
                StrSQl = StrSQl + "(RMANDPACKTRNSPTGRNHUSGAS+DPTRNSPTGRNHUSGAS+TRSPTTOCUSGRNHUSGAS+PPPTRNSPTGRNHUSGAS+PACKGEDPRODTRNSPTGRNHUSGAS) AS ""Transportation"" "
                StrSQl = StrSQl + "FROM TOTAL "
                StrSQl = StrSQl + "INNER JOIN RESULTSPL "
                StrSQl = StrSQl + "ON RESULTSPL.CASEID = TOTAL.CASEID "
                StrSQl = StrSQl + "WHERE TOTAL.CASEID IN (" + CaseId1 + "," + CaseId2 + ") "

                Dts = odbutil.FillDataTable(StrSQl, MyConnection)



                StrSqlPref = "SELECT USERID, UNITS, CURRENCY, CURR, CONVERSIONFACTOR, TITLE1, TITLE2, TITLE3, TITLE4, TITLE5, TITLE6 FROM  CHARTPREFERENCES WHERE USERID='" + Session("USERID").ToString() + "'"
                DtsPref = odbutil.FillDataTable(StrSqlPref, MyEConnection)

                If Dts.Rows.Count > 0 And DtsPref.Rows.Count > 0 Then
                    DrawChart(Dts, DtsPref)
                End If




            End If



        Catch ex As Exception
            Response.Write("Error:" + ex.Message.ToString())


        End Try


    End Sub

    Private Sub DrawChart(ByVal Dts As DataTable, ByVal DtsPref As DataTable)
        Try
            Dim pcScript = String.Empty
            Dim control As HtmlGenericControl = FindControl("GHGComp")
            Dim myImage As CordaEmbedder = New CordaEmbedder()
            Dim Graphtype As String = String.Empty
            Dim GraphName As String = String.Empty
            Dim Count As String = String.Empty
            Dim SalesVolumeLb As New Decimal
            Dim Transportation As New Decimal
            Dim Process As New Decimal
            Dim Purchased As New Decimal


            Dim MyEConnection As String = String.Empty
            Dim dsChartSetting As New DataTable
            Dim StrSqlChartSetting As String = String.Empty
            Dim odbutil As New DBUtil()

            MyEConnection = System.Configuration.ConfigurationManager.AppSettings("EconConnectionString")
            StrSqlChartSetting = "SELECT EXTSERVERADD, INTCOMPORTADD FROM CHARTSETTING"
            dsChartSetting = odbutil.FillDataTable(StrSqlChartSetting, MyEConnection)





            Count = Dts.Rows.Count
            GraphName = "Sargento_KraftPouchvsTray"
            Graphtype = "graph"

            pcScript &= "" + Graphtype + ".transposed(true)" + Graphtype + ".setCategories(Transportation;Process;Purchased Materials)"
            Dim i As New Integer
            For i = 0 To Count - 1
                SalesVolumeLb = FormatNumber(Dts.Rows(i).Item("SALESVOLUMELB") * DtsPref.Rows(0).Item("CONVERSIONFACTOR"), 0)

                Transportation = FormatNumber((Dts.Rows(i).Item("Transportation") * DtsPref.Rows(0).Item("CONVERSIONFACTOR")) / SalesVolumeLb, 3)
                Process = FormatNumber((Dts.Rows(i).Item("Process") * DtsPref.Rows(0).Item("CONVERSIONFACTOR")) / SalesVolumeLb, 3)
                Purchased = FormatNumber((Dts.Rows(i).Item("Purchased Materials") * DtsPref.Rows(0).Item("CONVERSIONFACTOR")) / SalesVolumeLb, 3)

                pcScript &= "" + Graphtype + ".setSeries(" & Dts.Rows(i).Item("CASEDE") & ";" & Transportation & ";" & Process & ";" & Purchased & ")"
            Next


            myImage.externalServerAddress = dsChartSetting.Rows(0)("EXTSERVERADD").ToString()
            myImage.internalCommPortAddress = dsChartSetting.Rows(0)("INTCOMPORTADD").ToString()
            myImage.imageTemplate = GraphName + ".itxml"
            myImage.userAgent = Request.UserAgent
            myImage.width = 700
            myImage.height = 400
            myImage.returnDescriptiveLink = True
            myImage.language = "EN"
            myImage.pcScript = pcScript + "Y-axis.SetText(" + DtsPref.Rows(0).Item("Title4").ToString() + " gr gas /" + DtsPref.Rows(0).Item("Title4").ToString() + ")"
            myImage.outputType = "JPEG"
            myImage.fallback = "STRICT"
            control.InnerHtml = myImage.getEmbeddingHTML
        Catch ex As Exception
            Response.Write("DrawChart:Error:" + ex.Message.ToString())
        End Try
    End Sub

End Class
